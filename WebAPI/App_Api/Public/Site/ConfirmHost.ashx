<%@ WebHandler Language="C#"  Class="TF.RunSaftyAPI.App_Api.Public.Site.ConfirmHost" %>
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

namespace TF.RunSaftyAPI.App_Api.Public.Site
{
    /// <summary>
    /// 1.16	获取值班员确认端IP
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ConfirmHost : IHttpHandler
    {
        string sRequest = "";
        string strcid = "";

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
                if (!string.IsNullOrEmpty(context.Request["cid"]))    //客户端ID(siteID)
                {
                    strcid = context.Request["cid"];
                    sRequest += gotoConfirmHost();
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
        /// 1.16	获取值班员确认端IP
        /// </summary>
        /// <param name="hct"></param>
        /// <returns></returns>
        private string gotoConfirmHost()
        {
            string strSql = "SELECT  strConfirmIP FROM TAB_Base_Site_Additional WHERE (strSiteGUID = '"+strcid+"')";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
                return sRetErrJSON("该cid未找到对应数据");

            string strSiteIP = dt.Rows[0]["strConfirmIP"].ToString();

            string strJson = "{\"result\":0,\"resultStr\":\"返回成功\",\"host\":\"" + strSiteIP + "\"}}";
            return strJson;
        }

        //返回错误信息
        string sRetErrJSON(string strErr)
        {
            return "{\"result\":1,\"resultStr\":\"" + strErr + "\"}";
        }

        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <returns></returns>
        private string gotoHelp()
        {
            string strHelp = @"1.16	获取值班员确认端IP
参数格式
    cid=xxx

 参数说明
    cid：为客户端编号

 返回参数
    参数格式

        {'result':0,'resultStr':'返回成功','host':''}

        {'result':1,'resultStr':'返回错误'}

 参数说明
    host：为管理端的IP
";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}

