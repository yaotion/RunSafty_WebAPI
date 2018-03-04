<%@ WebHandler Language="C#" Class="TF.RunSaftyAPI.App_Api.Public.Plan.SectionList" %>
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
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace TF.RunSaftyAPI.App_Api.Public.Plan
{
    /// <summary>
    /// 单个区段信息类
    /// </summary>
    public class SectinInfo
    {
       public string sectionName { get; set; }
       public string sectionID { get; set; }
       public string strJWDNumber { get; set; }
       public string strQDNumber { get; set; }
    }
    /// <summary>
    /// 区段信息列表返回信息类
    /// </summary>
    public class RltSeciontList
    {
        public RltSeciontList()
        {
            sectionList = new List<SectinInfo>(); 
        }
        public int result { get; set; }
        public string resultStr { get; set; }
        public List<SectinInfo> sectionList { get; set; }
    }
    /// <summary>
    /// 1.17	获取写卡区段
    ///
    public class SectionList : IHttpHandler
    {

        #region Request参数
        public string SiteGUID
        {
            get {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["cid"]))
                {
                    return HttpContext.Current.Request["cid"].ToString();
                }
                return "";
            } 
        }
        public string PlanGUID
        {
            get
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["pid"]))
                {
                    return HttpContext.Current.Request["pid"].ToString();
                }
                return "";
            }
        }
        public string HelpCmd
        {
            get
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["help"]))
                {
                    return HttpContext.Current.Request["help"].ToString();
                }
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["h"]))
                {
                    return HttpContext.Current.Request["h"].ToString();
                }
                return "";
            }
        }
        #endregion Request参数
        
        public void ProcessRequest(HttpContext context)
        {
            string sRequest = "";
            try
            {
                if (HelpCmd != "")
                {

                    sRequest = gotoHelp(); 
                    return;
                }

                if (SiteGUID == "")
                {
                    sRequest = ReturnErrorString(1002,"参数cid不能为空");
                    return;
                }
                if (PlanGUID == "")
                {
                    sRequest = ReturnErrorString(1003, "参数pid不能为空");
                    return;
                }
                sRequest = gotoSectionList();                
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
        /// 1.17	获取写卡区段
        /// </summary>
        /// <param name="hct"></param>
        /// <returns></returns>
        private string gotoSectionList()
        {
            ///依据cid从TAB_Base_Site获取客户端所属机务段的编号:strAreaGUID
            ///从表VIEW_Base_Writecard_Section中依据字段strAreaGUID字段获取sectionName，sectionID信息并返回
            RltSeciontList sectionList = new RltSeciontList();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {

                string strSql = @"SELECT strSectionName,strSectionID,strJWDNumber,strQDNumber FROM VIEW_Base_TrainJiaolu_Section 
	WHERE strTrainJiaoluGUID in(SELECT strTrainJiaoluGUID FROM TAB_Plan_Train  WHERE strTrainPlanGUID =@TrainPlanGUID)";
                SqlParameter[] sqlParams = { 
                                         new SqlParameter("TrainPlanGUID",PlanGUID)
                                     };
            
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count < 1)
                {
                    sectionList.result = 0;
                    sectionList.resultStr = "没有找到计划相关的区段";
                    return jsonSerializer.Serialize(sectionList);
                }

                sectionList.result = 0;
                sectionList.resultStr = "返回成功";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SectinInfo item = new SectinInfo();
                    item.sectionID = dt.Rows[i]["strSectionID"].ToString();
                    item.sectionName = dt.Rows[i]["strSectionName"].ToString();
                    item.strJWDNumber = dt.Rows[i]["strJWDNumber"].ToString();
                    item.strQDNumber = dt.Rows[i]["strQDNumber"].ToString();
                    sectionList.sectionList.Add(item);
                }
            }
            catch(Exception ex)
            {
                sectionList.result = 0;
                sectionList.resultStr = "系统异常：" + ex.Message;
            }
            
            return jsonSerializer.Serialize(sectionList);           
        }

        /// <summary>
        /// 返回错误的信息
        /// </summary>
        /// <param name="ErrorID">错误代码</param>
        /// <param name="ErrorText">错误描述</param>
        /// <returns>错误信息的JSON格式</returns>
        private string ReturnErrorString(int ErrorID,string ErrorText)
        {
            RltSeciontList sectionList = new RltSeciontList();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            sectionList.result = ErrorID;
            sectionList.resultStr = ErrorText;
            return jsonSerializer.Serialize(sectionList);  
        }
        
        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <returns></returns>
        private string gotoHelp()
        {
            string strHelp = @"1.17	获取计划所对应的写卡区段列表
 调用参数
     参数格式
        cid=xxx

     参数说明
        cid:为客户端编号

 返回参数
    参数格式
        {'result':0,'resultStr':'返回成功',sectionList':[{'sectionName':'','sectionID':''},…]}

        {'result':1,'resultStr':'返回错误'}

     参数说明

        sectionList：区段列表
        sectionName：区段名称
        sectionID：区段编号
";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}

