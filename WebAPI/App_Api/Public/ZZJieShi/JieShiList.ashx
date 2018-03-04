<%@ WebHandler Language="C#"  Class="TF.RunSaftyAPI.App_Api.Public.ZZJieShi.JieShiList" %>
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
using System.IO;

namespace TF.RunSaftyAPI.App_Api.Public.ZZJieShi
{
    /// <summary>
    /// 1.9	获取写卡揭示
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class JieShiList : IHttpHandler
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

                if (string.IsNullOrEmpty(context.Request["cid"]))
                    sRequest += sRetErrJSON("cid不能为空");
                else
                {
                    strcid = context.Request["cid"];
                    sRequest += gotoJieShiList();
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
        /// 1.9	获取写卡揭示
        /// </summary>
        /// <returns></returns>
        private string gotoJieShiList()
        {
            string strSql = "SELECT * FROM TAB_ZZJieShi_File WHERE (strSectionGUID IN "
                    +" (SELECT strSectionID FROM VIEW_Base_TrainJiaolu_Section WHERE (strTrainJiaoluGUID IN "
                    +"(SELECT strTrainJiaoluGUID FROM TAB_Base_TrainJiaoluInSite WHERE (strSiteGUID = '"+strcid+"')))))";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
            {
                StringBuilder sbEmpty = new StringBuilder();
                sbEmpty.Append("{\"result\":0,\"resultStr\":\"返回成功\",\"zzJieShiList\":[]}");
                return sbEmpty.ToString();
            }

            string strJson = "";
            int y = dt.Rows.Count - 1;

            ///组织数据为JSON
            for (int x = 0; x <= y; x++)
            {
                string strJieShiGUID = dt.Rows[x]["strJieShiGUID"].ToString();
                string strSectionGUID = dt.Rows[x]["strSectionGUID"].ToString();
                string strJieShiFile = dt.Rows[x]["strJieShiFile"].ToString();
                string strOrginName = dt.Rows[x]["strOrginName"].ToString();
                //转换时间格式
                string strdtCreateTime = dt.Rows[x]["dtCreateTime"].ToString();
                DateTime dtTime;
                if (!string.IsNullOrEmpty(strdtCreateTime))
                {
                    dtTime = Convert.ToDateTime(strdtCreateTime);
                    strdtCreateTime = dtTime.ToString("yyyy-MM-dd HH:mm:ss");
                }

                //组织子对象数据
                strJson += "{\"strJieShiGUID\":\"" + strJieShiGUID + "\",\"strSectionID\":\"" + strSectionGUID
                    + "\",\"strJieShiFile\":\"" + strJieShiFile + "\",\"strOrginName\":\"" + strOrginName + "\",\"dtUpTime\":\"" + strdtCreateTime + "\"}";

                if (x < y)  //加分号
                    strJson += ",";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("{\"result\":0,\"resultStr\":\"返回成功\",\"zzJieShiList\":[");
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
            string strHelp = @"1.9	获取写卡揭示
    调用参数

        参数格式
            cid=xxx

        参数说明
            cid:为客户端编号

        返回参数

        参数格式

            {'result':0,'resultStr':'返回成功','zzJieShiList':[{'strJieShiGUID':'','strSectionID':'','strJieShiFile':'','dtUpTime':''},…]}

            {'result':1,'resultStr':'返回错误'}

    参数说明
        zzieShiList：写卡揭示列表
        strJieShiGUID：揭示GUID【strJieShiGUID】
        strSectionID：写卡区段GUID【strSectionID】
        strJieShiFile：揭示文件相对网站跟路径的相对路径【strJieShiFile】
        dtUpTime：创建时间【dtUpTime】
";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}
