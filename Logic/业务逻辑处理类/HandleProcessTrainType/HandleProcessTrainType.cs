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
    public class HandleProcessTrainType
    {
        #region 属性
        public string strID;
        public string strTrainType;
        public string strProcessID;
        public string dtPostTime;
        #endregion 属性

        #region 构造函数
        public HandleProcessTrainType()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public HandleProcessTrainType(string strid)
        {
            string strSql = "select * from TAB_HandleProcessTrainType where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["strID"].ToString();
                strTrainType = dt.Rows[0]["strTrainType"].ToString();
                strProcessID = dt.Rows[0]["strProcessID"].ToString();
                dtPostTime = dt.Rows[0]["dtPostTime"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_HandleProcessTrainType (strID,strTrainType,strProcessID) values (@strID,@strTrainType,@strProcessID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",guid),
                                           new SqlParameter("strTrainType",strTrainType),
                                           new SqlParameter("strProcessID",strProcessID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_HandleProcessTrainType set strTrainType = @strTrainType where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("strTrainType",strTrainType)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_HandleProcessTrainType where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string Name)
        {
            string strSql = "select count(*) from TAB_HandleProcessTrainType where strTrainType=@strTrainType ";
            if (strid != "")
            {
                strSql += " and strID <> @strID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid),
                                           new SqlParameter("strTrainType",Name)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllHandleProcessTrainType(string uid)
        {
            string strSql = "select * from VIEW_HandleProcessTrainType where 1=1";
            if (uid != "")
            {
                strSql += " and strProcessID = @uid ";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("uid",uid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        #endregion
    }
}
