<%@ WebHandler Language="C#"  Class="TF.RunSaftyAPI.App_Api.Public.Trainman.Picture" %>
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

namespace TF.RunSaftyAPI.App_Api.Public.Trainman
{
    /// <summary>
    /// 1.15	获取乘务员照片信息
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Picture : IHttpHandler
    {
        string sRequest = "";

        string strcid = "";
        string struserID = "";

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
                    strcid = context.Request["cid"];

                if (!string.IsNullOrEmpty(context.Request["userID"]))
                {
                    struserID = context.Request["userID"];
                    sRequest += gotoPicture();
                }
                else
                {
                    sRequest += sRetErrJSON("userID不能为空");
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

        //返回错误信息
        string sRetErrJSON(string strErr)
        {
            return "{\"result\":1,\"resultStr\":\"" + strErr + "\"}";
        }

        /// <summary>
        /// 1.15	获取乘务员照片信息
        /// </summary>
        private string gotoPicture()
        {
            string strSql = "SELECT top 1 Picture FROM TAB_Org_Trainman  WHERE (strTrainmanGUID = '" + struserID + "')";
            string strPicture = "";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
                return "{\"result\":1,\"resultStr\":\"没有找到该userID的信息\"}";
            else
            {
                if (!DBNull.Value.Equals(dt.Rows[0]["Picture"]))
                    strPicture = Convert.ToBase64String((byte[])dt.Rows[0]["Picture"]);
            }

            return "{\"result\":0,\"resultStr\":\"返回成功\",\"pic\":\"" + strPicture + "\"}";
        }

        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <returns></returns>
        private string gotoHelp()
        {
            string strHelp = @"1.15	获取乘务员照片信息
 参数格式

     cid=xxx&useID

     参数说明

        cid：为客户端编号
        userID:为乘务员GUID

 返回参数
     参数格式

        {'result':0,'resultStr':'返回成功','pic':''}

        {'result':1,'resultStr':'返回错误'}

 参数说明
    pic：为乘务员的标准照片(采用base64编码)";

            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}
