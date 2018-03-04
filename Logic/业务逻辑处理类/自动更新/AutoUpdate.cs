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

namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///AutoUpdate 的摘要说明
    /// </summary>
    public class AutoUpdate            
    {
        public int nid = 0;
        public string strProjectID = "";
        public DateTime dtCreateTime = DateTime.MinValue;
        public string strProjectVersion = "";
        public string strUpdateBrief = "";
        public string strPackageUrl = "";

        public AutoUpdate()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static string GetUploadFile(int nid)
        {
            string strSql = "select top 1 strPackageUrl from TAB_AutoUpdate_Version where nid = @nid";

            SqlParameter[] sqlParams = {
                                          new SqlParameter("nid",nid) 
                                       };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            if ((obj != null) && (!DBNull.Value.Equals(obj)))
            {
                return obj.ToString();
            }
            return "";
        }
        public bool Add()
        {
            string strSql = "insert into TAB_AutoUpdate_Version (strProjectID,dtCreateTime,strProjectVersion,strUpdateBrief,strPackageUrl) values (@strProjectID,@dtCreateTime,@strProjectVersion,@strUpdateBrief,@strPackageUrl)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strProjectID",strProjectID),
                                           new SqlParameter("dtCreateTime",dtCreateTime),
                                           new SqlParameter("strProjectVersion",strProjectVersion),
                                           new SqlParameter("strUpdateBrief",strUpdateBrief),
                                           new SqlParameter("strPackageUrl",strPackageUrl)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(int nid)
        {
            string strSql = "delete from TAB_AutoUpdate_Version where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nid",nid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static DataTable GetAllProject()
        {
            string strSql = "select * from TAB_AutoUpdate_Project order by nid";

            SqlParameter[] sqlParams = {
                                          
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
    }
}
