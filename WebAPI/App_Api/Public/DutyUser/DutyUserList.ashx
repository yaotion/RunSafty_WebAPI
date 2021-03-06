﻿<%@ WebHandler Language="C#"  Class="TF.RunSaftyAPI.App_Api.Public.DutyUser.DutyUserList" %>
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

namespace TF.RunSaftyAPI.App_Api.Public.DutyUser
{
    /// <summary>
    /// 1.13	获取人员列表
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DutyUserList : IHttpHandler
    {
        string sRequest = "";
        //参数
        string strcid = "";    //cid：为客户端编号 传入并传出
        string stroption = "0";   //option: 0 不要指纹和照片、1要照片不要指纹、2要指纹不要照片、3照片和指纹都要
        string strnid = "0";      //nid:为上次提交获取的编号，如 第一次获取此参数应该为0
        string strcount = "10";   //count:指定取几条，缺省10条
        string strindex = "1";   //当前开始数量

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

                if (!string.IsNullOrEmpty(context.Request["option"]))
                    stroption = context.Request["option"];
                else
                {
                    sRequest += sRetErrJSON("option不能为空");
                    return;
                }

                if (!string.IsNullOrEmpty(context.Request["nid"]))
                    strnid = context.Request["nid"];
                else
                {
                    sRequest += sRetErrJSON("nid不能为空");
                    return;
                }

                if (!string.IsNullOrEmpty(context.Request["count"]))
                    strcount = context.Request["count"];

                sRequest += gotoTrainman();
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
        /// 1.13	获取人员列表
        /// </summary>
        private string gotoTrainman()
        {
            //查询人员列表
            string strSql = "SELECT TOP " + strcount + " nid, strDutyGUID, strDutyNumber, strDutyName, FingerPrint1, FingerPrint2, Picture, nID FROM TAB_Org_DutyUser"
                    + " WHERE (nid > " + strnid + ") ORDER BY nid";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
                return sRetErrJSON("参数 count 或 nid 不正确");

            string strJson = "";
            string strnID = "0";
            int y = dt.Rows.Count - 1;

            ///组织数据为JSON
            for (int x = 0; x <= y; x++)
            {
                string strDutynid = dt.Rows[x]["nid"].ToString();
                string strDutyGUID = dt.Rows[x]["strDutyGUID"].ToString();
                string strDutyNumber = dt.Rows[x]["strDutyNumber"].ToString();
                string strDutyName = dt.Rows[x]["strDutyName"].ToString();

                string strfinger1 = "";
                string strfinger2 = "";
                string strPicture = "";

                if (stroption == "2" || stroption == "3")   //2要指纹不要照片、3照片和指纹都要
                {
                    if (!DBNull.Value.Equals(dt.Rows[x]["FingerPrint1"]))
                        strfinger1 = Convert.ToBase64String((byte[])dt.Rows[x]["FingerPrint1"]);

                    if (!DBNull.Value.Equals(dt.Rows[x]["FingerPrint2"]))
                        strfinger2 = Convert.ToBase64String((byte[])dt.Rows[x]["FingerPrint2"]);
                }

                if (stroption == "1" || stroption == "3")   //1要照片不要指纹、3照片和指纹都要
                {
                    if (!DBNull.Value.Equals(dt.Rows[x]["Picture"]))
                        strPicture = Convert.ToBase64String((byte[])dt.Rows[x]["Picture"]);
                }

                //组织子对象数据
                strJson += "{\"nid\":\"" + strDutynid
                        + "\",\"dutyUserid\":\"" + strDutyGUID
                        + "\",\"dutyUserNumber\":\"" + strDutyNumber
                        + "\",\"dutyUserName\":\"" + strDutyName
                        + "\",\"finger1\":\"" + strfinger1
                        + "\",\"finger2\":\"" + strfinger2
                        + "\",\"pic\":\"" + strPicture
                        + "\"}";

                if (x < y)  //加分号
                    strJson += ",";
                if (x == y) //下次的nid
                    strnID = dt.Rows[x]["nID"].ToString();
            }

            strSql = "select count(*)  as totalCount,(select count(*) + 1 from TAB_Org_DutyUser where nid <= " + strnid + ") as nIndex from TAB_Org_DutyUser";
            DataTable dtCount = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dtCount.Rows.Count < 1)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append("{\"result\":0,\"resultStr\":\"返回成功\",\"totalCount\":" +
                        dtCount.Rows[0]["totalCount"].ToString() + ",\"index\":" +
                        dtCount.Rows[0]["nIndex"].ToString() + ",\"nid\":" +
                        strnID + ",\"dutyUserList\":[");
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
            string strHelp = @"1.13	获取人员列表
 调用参数

     参数格式

        cid=xxx& option=&nid=&count=

     参数说明

        cid：为客户端编号
        option: 0 不要指纹和照片、1要照片不要指纹、2要指纹不要照片、3照片和指纹都要
        nid:为上次提交获取的编号，如第一次获取此参数应该为0
        count:指定取几条，缺省10条

 返回参数

     参数格式

        {'result':0,'resultStr':'返回成功','totalCount':100,'index':1,'nid':12345,'dutyUserList':[{'nid':'','dutyUserid':'','dutyUserNumber':'','dutyUserName':'','finger1':'','finger2':'','pic':''},…]}
        
        {'result':1,'resultStr':'返回错误'}

     参数说明

        totalCount:人员数量
        index:当前开始数量
        nid：下次请求的编号
        dutyUserList：人员信息列表
	        dutyUserid：乘务员GUID
	        dutyUserNumber：乘务员工号
        dutyUserName：乘务员姓名
        finger1：指纹1
        finger2：指纹2
        pic：照片

";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}
