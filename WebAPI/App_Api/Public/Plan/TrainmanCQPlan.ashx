<%@ WebHandler Language="C#" CodeBehind="TrainmanCQPlan.ashx.cs" Class="TF.RunSaftyAPI.App_Api.Public.Plan.TrainmanCQPlan" %>
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using ThinkFreely.DBUtility;
using System.Configuration;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Net;
using System.Runtime.Serialization.Json;

namespace TF.RunSaftyAPI.App_Api.Public.Plan
{
    /// <summary>
    /// 1.5	获取乘务员出勤计划信息
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TrainmanCQPlan : IHttpHandler
    {
        string sRequest = "";

        string strcid = "";
        string strtid = "";

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
                else
                {
                    sRequest += sRetErrJSON("cid不能为空");
                    return;
                }

                if (!string.IsNullOrEmpty(context.Request["tid"]))
                    strtid = context.Request["tid"];
                else
                {
                    sRequest += sRetErrJSON("tid不能为空");
                    return;
                }
                sRequest += gotoTrainmanCQPlan();
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
        /// 1.5	获取乘务员出勤计划信息
        /// </summary>
        /// <returns></returns>
        public string gotoTrainmanCQPlan()
        {
            //获取
            string strSql = "SELECT top 1 * FROM TAB_Org_Trainman WHERE (strTrainmanGUID = '" + strtid + "')";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
            {
                StringBuilder sbTrainmanEmpty = new StringBuilder();
                sbTrainmanEmpty.Append("{\"result\":0,\"resultStr\":\"返回成功\",\"planID\":\"" + string.Empty + "\",\"startTime\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"trainNo\":\"" + string.Empty
        + "\",\"trainTypeName\":\"" + string.Empty + "\",\"trainNumber\":\"" + string.Empty
        + "\",\"trainJiaoluName\":\"" + string.Empty + "\",\"trainJiaoluGUID\":\"" + string.Empty + "\",\"trainmanList\":[");
                sbTrainmanEmpty.Append(string.Empty);
                sbTrainmanEmpty.Append("]}");

                return sbTrainmanEmpty.ToString();
            }
            string strTrainmanNumber = dt.Rows[0]["strTrainmanNumber"].ToString();

            string strDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //获取计划信息
            string strSqlPlan = @"select top 1 * from VIEW_Plan_BeginWork 
    where strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = @SiteGUID) and 
    (nPlanState in (4,7)) and (strTrainmanGUID1 = @TrainmanGUID or strTrainmanGUID2 =@TrainmanGUID or strTrainmanGUID3 = @TrainmanGUID or strTrainmanGUID4 = @TrainmanGUID)";

            SqlParameter[] sqlParamPlan = {
                                        new SqlParameter("EventTime",strDate),
                                        new SqlParameter("TrainmaNumber",strTrainmanNumber),
                                        new SqlParameter("TrainmanGUID",strtid),
                                        new SqlParameter("SiteGUID",strcid)
                                      };

            string strJson = "";
            string TrainmanId = "";
            int TrainmanIndex = 0;
            string TrainmanNumber = "";
            string TrainmanName = "";

            string sTrainPlanGUID = "";
            string sTrainNo = "";
            string sTrainTypeName = "";
            string sTrainNumber = "";

            string strTrainJiaoluName = "";
            string strTrainJiaoluGUID = "";

            DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlPlan, sqlParamPlan).Tables[0];
            if (dtPlan.Rows.Count < 1)
            {
                StringBuilder sbPlanEmpty = new StringBuilder();
                sbPlanEmpty.Append("{\"result\":0,\"resultStr\":\"返回成功\",\"planID\":\"" + string.Empty + "\",\"startTime\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"trainNo\":\"" + string.Empty
        + "\",\"trainTypeName\":\"" + string.Empty + "\",\"trainNumber\":\"" + string.Empty
        + "\",\"trainJiaoluName\":\"" + string.Empty + "\",\"trainJiaoluGUID\":\"" + string.Empty + "\",\"trainmanList\":[");
                sbPlanEmpty.Append(string.Empty);
                sbPlanEmpty.Append("]}");

                return sbPlanEmpty.ToString();
            }
            else
            {
                //组织匹配数据
                sTrainPlanGUID = dtPlan.Rows[0]["strTrainPlanGUID"].ToString();
                sTrainNo = dtPlan.Rows[0]["strTrainNo"].ToString();
                sTrainTypeName = dtPlan.Rows[0]["strTrainTypeName"].ToString();
                sTrainNumber = dtPlan.Rows[0]["strTrainNumber"].ToString();
                strDate = ((DateTime)dtPlan.Rows[0]["dtStartTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                strTrainJiaoluName = dtPlan.Rows[0]["strTrainJiaoluName"].ToString();
                strTrainJiaoluGUID = dtPlan.Rows[0]["strTrainJiaoluGUID"].ToString();

                if (dtPlan.Rows[0]["strTrainmanNumber1"].ToString() != "")
                {
                    TrainmanIndex = 1;
                    TrainmanId = dtPlan.Rows[0]["strTrainmanGuid1"].ToString();

                    TrainmanName = dtPlan.Rows[0]["strTrainmanName1"].ToString();
                    TrainmanNumber = dtPlan.Rows[0]["strTrainmanNumber1"].ToString();
                    strJson += "{\"trainmanID\":\"" + TrainmanId + "\",\"trainmanIndex\":\"" + TrainmanIndex + "\",\"trainmanNumber\":\"" + TrainmanNumber + "\",\"trainmanName\":\"" + TrainmanName + "\"}";
                }

                if (dtPlan.Rows[0]["strTrainmanNumber2"].ToString() != "")
                {
                    TrainmanIndex = 2;
                    TrainmanId = dtPlan.Rows[0]["strTrainmanGuid2"].ToString();

                    if (strJson != "")  //加分号
                        strJson += ",";
                    TrainmanName = dtPlan.Rows[0]["strTrainmanName2"].ToString();
                    TrainmanNumber = dtPlan.Rows[0]["strTrainmanNumber2"].ToString();
                    strJson += "{\"trainmanID\":\"" + TrainmanId + "\",\"trainmanIndex\":\"" + TrainmanIndex + "\",\"trainmanNumber\":\"" + TrainmanNumber + "\",\"trainmanName\":\"" + TrainmanName + "\"}";
                }

                if (dtPlan.Rows[0]["strTrainmanNumber3"].ToString() != "")
                {
                    TrainmanIndex = 3;
                    TrainmanId = dtPlan.Rows[0]["strTrainmanGuid3"].ToString();

                    if (strJson != "")  //加分号
                        strJson += ",";
                    TrainmanName = dtPlan.Rows[0]["strTrainmanName3"].ToString();
                    TrainmanNumber = dtPlan.Rows[0]["strTrainmanNumber3"].ToString();
                    strJson += "{\"trainmanID\":\"" + TrainmanId + "\",\"trainmanIndex\":\"" + TrainmanIndex + "\",\"trainmanNumber\":\"" + TrainmanNumber + "\",\"trainmanName\":\"" + TrainmanName + "\"}";
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("{\"result\":0,\"resultStr\":\"返回成功\",\"planID\":\"" + sTrainPlanGUID + "\",\"startTime\":\"" + strDate + "\",\"trainNo\":\"" + sTrainNo
                + "\",\"trainTypeName\":\"" + sTrainTypeName + "\",\"trainNumber\":\"" + sTrainNumber
                + "\",\"trainJiaoluName\":\"" + strTrainJiaoluName + "\",\"trainJiaoluGUID\":\"" + strTrainJiaoluGUID + "\",\"trainmanList\":[");
            sb.Append(strJson);
            sb.Append("]}");

            return sb.ToString();
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
            string strHelp = @"1.5	获取乘务员出勤计划信息
 调用参数
     参数格式
        cid=xxx&tid=

     参数说明
        cid：为客户端编号
        tid：为乘务员ID

 返回参数
    参数格式

    {'result':0,'resultStr':'返回成功','planID':'','startTime':'','trainNo':'','trainTypeName':'','trainNumber':'','trainJiaoluName':'','trainJiaoluGUID':'','trainmanList':[{'trainmanID':'','trainmanIndex':},…]}

    {'result':1,'resultStr':'返回错误'}

     参数说明

        startTime：计划开车时间
        trainNo：车次
        trainTypeName：车型
        trainNumber：车号
        trainJiaoluName：机车交路名称【strTrainJiaoluName】
        trainJiaoluGUID：机车交路GUID【strTrainJiaoluGUID】
        trainmanList：值乘人员信息
	        trainmanID：值乘人ID
	        trainmanIndex:值乘类型(正司机。。)
";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}

