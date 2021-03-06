﻿<%@ WebHandler Language="C#" Class="TF.RunSaftyAPI.App_Api.Public.DutyUser.Login" %>
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

namespace TF.RunSaftyAPI.App_Api.Public.DutyUser
{
    /// <summary>
    /// 1.14	获取值班员登陆信息
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Login : IHttpHandler
    {
        public string sRequest = "";
        
        string strcid = "";
        string userNumber = "";
        string userPWD = "";

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

                if (!string.IsNullOrEmpty(context.Request["userNumber"]))
                {
                    userNumber = context.Request["userNumber"];   
                }
                else
                {
                    sRequest += sRetErrJSON("userNumber不能为空");
                    return;
                }

                if (!string.IsNullOrEmpty(context.Request["userPWD"]))
                    userPWD = context.Request["userPWD"];
                else
                {
                    sRequest += sRetErrJSON("userPWD不能为空");
                    return;
                }          
                sRequest += gotoLogin();
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
        /// 1.14	获取值班员登陆信息
        /// </summary>
        private string gotoLogin()
        {
            string strSql = "SELECT strPassword, strDutyName, strDutyNumber, strDutyGUID FROM TAB_Org_DutyUser WHERE (strDutyNumber = '" + userNumber + "')";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
                return sRetErrJSON("没有查询到该userNumber的信息"); ;

            string sPWD = dt.Rows[0]["strPassword"].ToString();
            if (string.IsNullOrEmpty(sPWD))
                return sRetErrJSON("没有获取到strPassword");

            string sUid = dt.Rows[0]["strDutyGUID"].ToString();
            if (string.IsNullOrEmpty(sUid))
                return sRetErrJSON("没有获取到strDutyGUID");

            string sNum = dt.Rows[0]["strDutyNumber"].ToString();
            if (string.IsNullOrEmpty(sNum))
                return sRetErrJSON("没有获取到strDutyNumber");

            string sName = dt.Rows[0]["strDutyName"].ToString();
            if (string.IsNullOrEmpty(sName))
                return sRetErrJSON("没有获取到strDutyName");

            if (sPWD == userPWD)
                return "{\"result\":0,\"resultStr\":\"返回成功\",\"userID\":\"" + sUid + "\",\"userNumber\":\"" + sNum + "\",\"userName\":\"" + sName + "\"}";
            else
                return sRetErrJSON("密码不正确");
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
            string strHelp = @"1.14	获取值班员登陆信息
 调用参数
     参数格式
        cid=xxx&userNumber=&userPWD=

     参数说明
        cid：为客户端编号
        userNumber:为客户端编号
        userPWD:登陆密码

 返回参数
     参数格式

    {'result':0,'resultStr':'返回成功','userID':'','userNumber':'','userName':''}

    {'result':1,'resultStr':'返回错误'}

 参数说明
    userID：以后用户标示身份的ID
    userName：用户显示名称
    userNumber：用户登陆编号
";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}
