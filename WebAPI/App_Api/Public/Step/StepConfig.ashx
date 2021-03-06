﻿<%@ WebHandler Language="C#" Class="TF.RunSaftyAPI.App_Api.Public.Step.StepConfig" %>
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

namespace TF.RunSaftyAPI.App_Api.Public.Step
{
    /// <summary>
    /// 1.4	获取客户端流程信息
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class StepConfig : IHttpHandler
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
                if (!string.IsNullOrEmpty(context.Request["cid"]))    //cid参数不存在
                    strcid = context.Request["cid"];
                else
                {
                    sRequest += sRetErrJSON("cid不能为空");
                    return;
                }
                sRequest += gotoStepConfig();
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
        /// 1.4	获取客户端流程信息
        /// </summary>
        private string gotoStepConfig()
        {
            string strSql = "SELECT StepConfig, ConfigName FROM TAB_Base_Site_StepConfig WHERE (strSiteGUID = '" + strcid + "')";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
                return "{\"result\":1,\"resultStr\":\"未找到编号为" + strcid + "的cid信息\"}";

            string strStepConfig = "";
            string strConfigName = "";

            if (!DBNull.Value.Equals(dt.Rows[0]["StepConfig"]))
                strStepConfig = Convert.ToBase64String((byte[])dt.Rows[0]["StepConfig"]);
            strConfigName = dt.Rows[0]["ConfigName"].ToString();

            string strJson = "{\"result\":0,\"resultStr\":\"返回成功\",\"StepConfig\":\"" + strStepConfig + "\",\"ConfigName\":\"" + strConfigName + "\"}";
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
            string strHelp = @"1.4	获取客户端流程信息
 调用参数
     参数格式
         cid=xxx

     参数说明
        cid：为客户端编号

 返回参数
     参数格式
        {'result':0,'resultStr':'返回成功','StepConfig':'XXXXXXX','ConfigName':''}

        {'result':1,'resultStr':'返回错误'}

 参数说明
    StepConfig：步骤配置文件内容
    ConfigName:配置文件名称
";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}
