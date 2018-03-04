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

namespace TF.RunSafty.Logic
{
    public class Sysconfig
    {
        #region 属性
        public string strID;
        public string strSection;
        public string strIdent;
        public string strValue;
        #endregion 属性

        #region 构造函数
        public Sysconfig()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public Sysconfig(string strid)
        {
            string strSql = "select * from tabSysConfig where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["strID"].ToString();
                strSection = dt.Rows[0]["strSection"].ToString();
                strValue = dt.Rows[0]["strValue"].ToString();
                strIdent = dt.Rows[0]["strIdent"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into tabSysConfig (strID,strSection,strIdent,strValue) values (@strID,@strSection,@strIdent,@strValue)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",guid),
                                           new SqlParameter("strSection",strSection),
                                           new SqlParameter("strIdent",strIdent),
                                           new SqlParameter("strValue",strValue)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update tabSysConfig set strValue=@strValue where strIdent=@strIdent";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strValue",strValue),
                                           new SqlParameter("strIdent",strIdent)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete tabSysConfig where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strSection, string strIdent)
        {
            string strSql = "select count(*) from tabSysConfig where strSection=@strSection ";
            if (strSection != "")
            {
                strSql += " and strIdent = @strIdent ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSection",strSection),
                                           new SqlParameter("strIdent",strIdent)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllSysconfig(string strSection)
        {
            string strSql = "select * from tabSysConfig where 1=1";
            if (strSection != "")
            {
                strSql += " and strSection  = @strSection  ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSection ",strSection )
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 获取指定value
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strIdent"></param>
        /// <returns></returns>
        public static string GetSingleSysconfig(string strSection, string strIdent)
        {
            string strSql = "select strValue from tabSysConfig where 1=1";
            if (strSection != "")
            {
                strSql += " and strSection  = @strSection  ";
            }
            strSql += strIdent == "" ? "" : " and strIdent  = @strIdent ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSection ",strSection ),
                                           new SqlParameter("strIdent ",strIdent )
                                       };
            return SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).ToString();
        }
        #endregion
    }
}
