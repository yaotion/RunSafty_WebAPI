<%@ WebHandler Language="C#" Class="PUBJieShi" %>

using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using ThinkFreely.DBUtility;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using TF.Api.Utilities;

#region
public class ResponseEntity
{
    public int result { get; set; }
    public string resultStr { get; set; }
    public List<PubJieShiEntity> pubJieShiList { get; set; }
}

public class PubJieShiEntity
{
    public string strPUBJieShiGUID { get; set; }
    public string strTitle { get; set; }
    public string strFileName { get; set; }
    public int nFileSize { get; set; }
    public string dtUpTime { get; set; }
    public string strOrginName { get;set;}
    public List<SectionEntity> sectionList { get; set; }
}

public class SectionEntity
{
    public string sectionID { get; set; }
}
#endregion


public class PUBJieShi : IHttpHandler
{
    ResponseEntity responseEntity;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        responseEntity = new ResponseEntity();
        responseEntity.pubJieShiList = new List<PubJieShiEntity>();

        string clientID = PageBase.static_ext_string(context.Request["cid"]);
        if (clientID == string.Empty)
        {
            responseEntity.result = 1;
            responseEntity.resultStr = "未设置客户端ID";
            context.Response.Write(new JavaScriptSerializer().Serialize(responseEntity));
            return;
        }
        context.Response.Write(GetJson(clientID));
    }


    public string GetJson(string clientID)
    {
        string strSql = @"select * from dbo.TAB_PUBJieShi_File";
        DataTable dtJieShiFile = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];

        foreach (DataRow drJieShi in dtJieShiFile.Rows)
        {
            PubJieShiEntity pubJieShiEntity = new PubJieShiEntity();
            pubJieShiEntity.strPUBJieShiGUID = PageBase.static_ext_string(drJieShi["strPUBJieShiGUID"]);
            pubJieShiEntity.nFileSize = PageBase.static_ext_int(drJieShi["nFileSize"]);
            pubJieShiEntity.strFileName = PageBase.static_ext_string(drJieShi["strFileName"]);
            pubJieShiEntity.strTitle = PageBase.static_ext_string(drJieShi["strTitle"]);
            pubJieShiEntity.dtUpTime = PageBase.static_ext_date(drJieShi["dtUpTime"]).Value.ToString("yyyy-MM-dd HH:mm:ss");
            pubJieShiEntity.strOrginName = PageBase.static_ext_string(drJieShi["strOrginName"]);
            pubJieShiEntity.sectionList = new List<SectionEntity>();

            string strSqlSection = "select distinct strSectionGUID from TAB_PUBJieShi_PublishSection where strPubJieShiGUID = @JieShiID";
            SqlParameter sqlParamJieShiGUID = new SqlParameter("@JieShiID", PageBase.static_ext_string(drJieShi["strPUBJieShiGUID"]));
            DataTable dtSections = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlSection, sqlParamJieShiGUID).Tables[0];

            foreach (DataRow drSection in dtSections.Rows)
            {
                SectionEntity sectionEntity = new SectionEntity();
                sectionEntity.sectionID = PageBase.static_ext_string(drSection["strSectionGUID"]);
                pubJieShiEntity.sectionList.Add(sectionEntity);
            }
            responseEntity.pubJieShiList.Add(pubJieShiEntity);
        }
        responseEntity.resultStr = "返回成功";
        return new JavaScriptSerializer().Serialize(responseEntity);

    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}