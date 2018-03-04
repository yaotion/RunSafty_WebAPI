<%@ WebHandler Language="C#"  Class="TF.RunSaftyAPI.App_Api.Public.DutyReading.GetDutyReading" %>
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
using System.IO;
using ThinkFreely.DBUtility;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace TF.RunSaftyAPI.App_Api.Public.DutyReading
{
    
    public class DutyReadingInfo
    {
        public string strDescription{get;set;}
        public string strReadingGUID{get;set;}
        public string strFileName{get;set;}
        public int nFileSize{get;set;}
        public int nReadTimeCount{get;set;}
        public int nReadingType{get;set;}
        public int nReadMode{get;set;}
        public string strOrginName{get;set;}
    }
    public class RltDutyReadingList
    {
        public RltDutyReadingList()
        {
            readingList = new List<DutyReadingInfo>(); 
        }
        public int result { get; set; }
        public string resultStr { get; set; }
        public List<DutyReadingInfo> readingList{get;set;}
    }
    
    /// <summary>
    /// 1.8	获取记名式传达
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetDutyReading : IHttpHandler
    {
        string sRequest = "";

        string strcid = "0";

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string strQuery = context.Request.QueryString.ToString().ToLower();
                if (strQuery == "help" || strQuery == "h") //帮助
                {
                    sRequest += gotoHelp();
                    return;
                }

                if (!string.IsNullOrEmpty(context.Request["cid"]))
                {
                    strcid = context.Request["cid"];
                    sRequest += gotoGetDutyReading();
                }
                else
                {
                    sRequest += sRetErrJSON("cid不能为空");
                    return;
                }
            }
            finally
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(sRequest);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 1.8	获取记名式传达
        /// </summary>
        private string gotoGetDutyReading()
        {
            ///依据cid从TAB_Base_Site获取客户端所属机务段的编号:strWorkShopGUID
            ///从表TAB_DutyReading_Information中依据字段strWorkShopGUID字段获取strFileName信息并返回

            string strSql = "SELECT strDescription, strReadingGUID, strFileName, nFileSize, nReadTimeCount, nReadingType, nReadMode,strOrginName "
                + "FROM TAB_DutyReading_Information WHERE (strWorkShopGUID = (SELECT strWorkShopGUID FROM TAB_Base_Site "
                +"WHERE (strSiteGUID = '" + strcid + "')))";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];

            RltDutyReadingList readingList = new RltDutyReadingList();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            
            if (dt.Rows.Count < 1)
            {
                readingList.result = 0;
                readingList.resultStr = "无数据";
                return jsonSerializer.Serialize(readingList);
            }
            
            

            string strJson = "";
            string strnID = "";
            int y = dt.Rows.Count - 1;

            ///组织数据为JSON
            for (int x = 0; x <= y; x++)
            {
                string strDescription = dt.Rows[x]["strDescription"].ToString();
                string strReadingGUID = dt.Rows[x]["strReadingGUID"].ToString();
                string strFileName = dt.Rows[x]["strFileName"].ToString();
                string strnFileSize = dt.Rows[x]["nFileSize"].ToString();
                string strnReadTimeCount = dt.Rows[x]["nReadTimeCount"].ToString();
                string strnReadingType = dt.Rows[x]["nReadingType"].ToString();
                string strnReadMode = dt.Rows[x]["nReadMode"].ToString();
                int nReadingType = 0;
                Int32.TryParse(strnReadingType,out nReadingType);
                DutyReadingInfo readingInfo = new DutyReadingInfo();
                readingInfo.strDescription = strDescription;
                readingInfo.strReadingGUID = strReadingGUID;
                readingInfo.strFileName = strFileName;
                readingInfo.nFileSize = Int32.Parse(strnFileSize);
                readingInfo.nReadTimeCount = Int32.Parse(strnReadTimeCount);

                readingInfo.nReadingType = nReadingType;
                readingInfo.nReadMode = Int32.Parse(strnReadMode);
                readingInfo.strOrginName = dt.Rows[x]["strOrginName"].ToString();
                readingList.readingList.Add(readingInfo);
            }
            readingList.result = 0;
            readingList.resultStr = "返回成功";
            return jsonSerializer.Serialize(readingList);
        }

        //返回错误信息
        string sRetErrJSON(string strErr)
        {
            return "{\"result\":1,\"resultStr\":\"" + strErr + "\",\"readingList\":[]}";
        }

        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <returns></returns>
        private string gotoHelp()
        {
            string strHelp = @"1.8	获取记名式传达
 调用参数

     参数格式
        cid=xxx

     参数说明
        cid:为客户端编号

 返回参数

     参数格式

        {'result':0,'resultStr':'返回成功',readingList':[{'strDescription':'','strReadingGUID':'','strFileName':'','nFileSize':'','nReadTimeCount':'','nReadingType':'','nReadMode':'','strOrginName'},…]}
        
        {'result':1,'resultStr':'错误原因','readingList':[]}

    参数说明
        readingList：阅读资料列表
        strDescription：资料描述
        strReadingGUID：资料编号
        strFileName：资料路径
        nFileSize：资料大小
        nReadTimeCount：阅读时长
        nReadingType：阅读类型
        nReadMode：阅读模式
";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}

