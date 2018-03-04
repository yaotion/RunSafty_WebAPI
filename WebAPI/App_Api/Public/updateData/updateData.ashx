<%@ WebHandler Language="C#" Class="updateData" %>

using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using ThinkFreely.DBUtility;
using System.Configuration;
using System.Reflection;
using System.IO;

public class updateData : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("sssss");
    }




    /// <summary>
    /// 1.3	获取客户端基础信息
    /// </summary>
    /// <param name="hct"></param>
    /// <returns></returns>
    private string gotoSiteInfo()
    {
        string strSql = "SELECT *  FROM TAB_Org_Trainman";
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];

        string strSiteIP = dt.Rows[0]["strSiteIP"].ToString();
        string strSiteName = dt.Rows[0]["strSiteName"].ToString();
        string strcnum = dt.Rows[0]["cnum"].ToString();
        string strnSiteJob = dt.Rows[0]["nSiteJob"].ToString();
        string strValue = dt.Rows[0]["cnum"].ToString();
        return "";
    }
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}