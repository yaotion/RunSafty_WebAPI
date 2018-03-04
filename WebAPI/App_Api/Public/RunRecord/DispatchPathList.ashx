<%@ WebHandler Language="C#"  Class="TF.RunSaftyAPI.App_Api.Public.RunRecord.DispatchPathList" %>
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

namespace TF.RunSaftyAPI.App_Api.Public.RunRecord
{
    /// <summary>
    /// 1.11	获取运行记录分发路径
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DispatchPathList : IHttpHandler
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
                    strcid = context.Request["cid"];
                else
                {
                    sRequest += sRetErrJSON("cid不能为空");
                    return;
                }

                sRequest += gotoDispatchPathList();
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
        /// 1.11	获取运行记录分发路径
        /// </summary>
        /// <returns></returns>
        private string gotoDispatchPathList()
        {
            string strSql = "SELECT strAreaGUID,strDispatchPath,strUserName,strPassword FROM TAB_RunRecord_DispatchPath "
                + "WHERE strAreaGUID = (SELECT strAreaGUID FROM TAB_Base_Site WHERE nid = '" + strcid + "')";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
                return sRetErrJSON("该cid未找到对应数据");

            string strJson = "";
            int y = dt.Rows.Count - 1;

            ///组织数据为JSON
            for (int x = 0; x <= y; x++)
            {
                string strAreaGUID = dt.Rows[x]["strAreaGUID"].ToString();
                string strDispatchPath = dt.Rows[x]["strDispatchPath"].ToString();
                string strUserName = dt.Rows[x]["strUserName"].ToString();
                string strPassword = dt.Rows[x]["strPassword"].ToString();

                //组织子对象数据
                strJson = "{\"strAreaGUID\":\"" + strAreaGUID
                    + "\",\"strDispatchPath\":\"" + strDispatchPath
                    + "\",\"strUserName\":\"" + strUserName
                    + "\",\"strPassword\":\"" + strPassword 
                    + "\"}";

                if (x < y)  //加分号
                    strJson += ",";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("{\"result\":0,\"resultStr\":\"返回成功\",dispatchPathList\":[");
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
            string strHelp = @"1.11	获取运行记录分发路径
 调用参数
     参数格式
        cid=xxx

     参数说明
        cid:为客户端编号

 返回参数
    参数格式
        {'result':0,'resultStr':'返回成功',dispatchPathList':[{'strAreaGUID':'','strDispatchPath':'','strUserName':'','strPassword':''},…]}

        {'result':1,'resultStr':'返回错误'}

     参数说明

        strAreaGUID：分发路径列表
        strDispatchPath：路径地址
        strUserName：用户名
        strPassword：密码
";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}

