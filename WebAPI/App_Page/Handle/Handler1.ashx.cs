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
using System.Web.Configuration;

namespace TF.RunSaftyAPI.App_Page.Handle
{
    /// <summary>
    /// 信息获取方式 [自用]
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler1 : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            string sRequest = "";
            sRequest = ShowRequest(context.Request);
            try
            {
                if (string.IsNullOrEmpty(context.Request["nid"]))    //nid参数不存在
                    sRequest += gotoSignature(context);
                else
                    sRequest += gotoTrainman(context);
            }
            finally
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(sRequest);
            }
        }

        /// <summary>
        /// 返回请求的路径和参数
        /// </summary>
        /// <returns></returns>
        string ShowRequest(HttpRequest hrt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--------------------------------\r");
            sb.Append("请求url路径:\r" + hrt.RawUrl + "\r");
            sb.Append("上次请求url:\r" + hrt.UrlReferrer + "\r");
            sb.Append("物理路径:\r" + hrt.Url + "\r");
            sb.Append("绝对url地址:\r" + hrt.Url.AbsoluteUri + "\r\r");

            foreach (string s in hrt.QueryString)
                sb.Append(string.Format("收到的参数:{0}={1}\r", s, hrt.QueryString[s]));

            sb.Append("--------------------------------\r");
            return sb.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取人员库特征码
        /// </summary>
        /// <param name="hct"></param>
        /// <returns></returns>
        private string gotoSignature(HttpContext hct)
        {
            string strSql = "SELECT strValue FROM TAB_System_Config WHERE (strSection = 'SysConfig') AND (strIdent = 'ServerFingerLibGUID')";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
                return null;

            string strValue = dt.Rows[0]["strValue"].ToString();
            string strJson = "{\"result\":0,\"resultStr\":\"返回成功\",\"signature\":\"" + strValue + "\"}";
            return strJson;
        }

        /// <summary>
        /// 获取人员及指纹库信息
        /// </summary>
        /// <param name="hct"></param>
        /// <returns></returns>
        private string gotoTrainman(HttpContext hct)
        {
            string strSql = "SELECT strTrainmanNumber AS trainmanNumber, strTrainmanName AS trainmanName, FingerPrint1 AS finger1, FingerPrint2 AS finger2, nID FROM TAB_Org_Trainman WHERE (nID = '" + hct.Request["nid"] + "')";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if(dt.Rows.Count<1)
                return null;

            string strtrainmanNumber = dt.Rows[0]["trainmanNumber"].ToString();
            string strtrainmanName = dt.Rows[0]["trainmanName"].ToString();
            byte[] bytefinger1 = null;
            byte[] bytefinger2 = null;
            string strfinger1 = "";
            string strfinger2 = "";

            if (!DBNull.Value.Equals(dt.Rows[0]["finger1"]))
                strfinger1 = Convert.ToBase64String((byte[])dt.Rows[0]["finger1"]);

            if (!DBNull.Value.Equals(dt.Rows[0]["finger2"]))
                strfinger2 = Convert.ToBase64String((byte[])dt.Rows[0]["finger2"]);

            string strnID = dt.Rows[0]["nID"].ToString();

            string strJson = "{\"result\":0,\"resultStr\":\"返回成功\",\"trainmanNumber\":\"" +
                strtrainmanNumber + "\" ,\"trainmanName\":\"" +
                strtrainmanName + "\" ,\"finger1\":\"" +
                strfinger1 + "\" ,\"finger2\":\"" +
                strfinger2 + "\",\"totalCount\":100,\"index\":1,nid:" + strnID + "}";
            return strJson; 
        }

        private static void ChangeConfig(string sUrl, string toUrl)
        {
            UrlMapping urlMap = null;
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            UrlMappingsSection urlMapSection = (UrlMappingsSection)config.GetSection("system.web/urlMappings");

            if (urlMapSection.UrlMappings.Count>0)
            {
                if (urlMapSection.UrlMappings[sUrl].Url == sUrl)
                    urlMapSection.UrlMappings.Remove(sUrl);

                urlMap = new UrlMapping(sUrl, toUrl);
                urlMapSection.UrlMappings.Add(urlMap);
                config.Save();
            }    
         }
    }
}
