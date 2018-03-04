using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace ZbcXxgl.TF.ZbcJxDBUtility.AshxService
{
    public class DBadControl
    {
        public static string QueryData(string sql)
        {
            try
            {
                DataSet ds=SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, sql);
                if (ds.Tables.Count > 0)
                {
                    return SystemPB.returnstrjson(ds.Tables[0]);
                }
                else
                {
                    return "{\"Success\":1,\"ResultText\":\"\"}";
                }
            }
            catch(Exception ex)
            {
                return "{\"Success\":0,\"ResultText\":\"" + ex + "\"}";
            }
        }
       
    }
}
