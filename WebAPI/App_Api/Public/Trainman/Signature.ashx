﻿<%@ WebHandler Language="C#" Class="TF.RunSaftyAPI.App_Api.Public.Trainman.Signature" %>
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Configuration;
using System.Reflection;
using System.Web.Configuration;
using System.IO;

namespace TF.RunSaftyAPI.App_Api.Public.Trainman
{
    /// <summary>
    /// 1.1	获取人员库特征码
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Signature : IHttpHandler
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
                if (!string.IsNullOrEmpty(context.Request["cid"]))
                {
                    strcid = context.Request["cid"];
                    sRequest = gotoSignature();
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

        //返回错误信息
        string sRetErrJSON(string strErr)
        {
            return "{\"result\":1,\"resultStr\":\"" + strErr + "\"}";
        }

        /// <summary>
        /// 获取人员库特征码
        /// </summary>
        private string gotoSignature()
        {
            string strSql = "SELECT strValue FROM TAB_System_Config WHERE (strSection = 'SysConfig') AND (strIdent = 'ServerFingerLibGUID')";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
                return "{\"result\":1,\"resultStr\":\"未找到编号为" + strcid + "的cid客户端信息\"}";

            string strValue = dt.Rows[0]["strValue"].ToString();
            string strJson = "{\"result\":0,\"resultStr\":\"返回成功\",\"signature\":\"" + strValue + "\"}";
            return strJson;
        }

        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <returns></returns>
        private string gotoHelp()
        {
            string strHelp = @"1.1	获取人员库特征码
 调用参数
     参数格式

        cid=xxx

     参数说明

        cid：为客户端编号

 返回参数
     参数格式

        {'result':0,'resultStr':'返回成功','signature':'XXXXXXX'}

        {'result':1,'resultStr':'返回错误原因'}

 参数说明
    Signature：人员库特征码";

            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}
