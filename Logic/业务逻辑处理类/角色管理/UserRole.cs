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
    ///UserRole 的摘要说明
    /// </summary>
    public class UserRole
    {
        #region 属性
        public int nID = 0;
        public string strName = "";
        #endregion 属性

        #region 构造函数
        public UserRole()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_UserRole (strName) values (@strName)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strName",strName)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_UserRole set strName = @strName where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",nID),
                                           new SqlParameter("strName",strName)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(int nID)
        {
            string strSql = "delete TAB_UserRole where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",nID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int nID, string strName)
        {
            string strSql = "select count(*) from TAB_UserRole where strName=@strName ";
            if (nID != 0)
            {
                strSql += " and nID <> @nID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",nID),
                                           new SqlParameter("strName",strName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改
    }
}
