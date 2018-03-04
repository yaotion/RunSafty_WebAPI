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
    ///LeaveMgrLeaveType功能：提供请假类型增删改查
    /// </summary>
    public class LeaveMgrLeaveType
    {
        #region 属性
        public string nClassID;
        public string strTypeName;
        public string strTypeGUID;
        public string strClassName;
        #endregion 属性

        #region 构造函数
        public LeaveMgrLeaveType()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public LeaveMgrLeaveType(string strid)
        {
            string strSql = "select * from VIEW_LeaveMgr_AllLeaveTypes where strTypeGUID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                nClassID = dt.Rows[0]["nClassID"].ToString();
                strTypeName = dt.Rows[0]["strTypeName"].ToString();
                strTypeGUID = dt.Rows[0]["strTypeGUID"].ToString();
                strClassName = dt.Rows[0]["strClassName"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_LeaveMgr_LeaveType (strTypeGUID,strTypeName,nClassID) values (@strTypeGUID,@strTypeName,@nClassID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTypeGUID",guid),
                                           new SqlParameter("strTypeName",strTypeName),
                                           new SqlParameter("nClassID",nClassID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_LeaveMgr_LeaveType set strTypeName=@strTypeName,nClassID=@nClassID where strTypeGUID=@strTypeGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTypeGUID",strTypeGUID),
                                           new SqlParameter("strTypeName",strTypeName),
                                           new SqlParameter("nClassID",nClassID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_LeaveMgr_LeaveType where strTypeGUID=@strTypeGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTypeGUID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strTypeGUID, string strTypeName,string classid)
        {
            string strSql = "select count(*) from TAB_LeaveMgr_LeaveType where strTypeName=@strTypeName and nClassID=@nClassID";
            if (strTypeGUID != "")
            {
                strSql += " and strTypeGUID <> @strTypeGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nClassID",classid),
                                           new SqlParameter("strTypeGUID",strTypeGUID),
                                           new SqlParameter("strTypeName",strTypeName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        //public static DataTable GetAllLeaveMgrClass(string name)
        //{
        //    string strSql = "select * from TAB_LeaveMgr_LeaveType";
        //    if (name != "")
        //    {
        //        strSql += " and strClassName like @name ";
        //    }
        //    strSql += " order by strTypeGUID ";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("name","%" +name+ "%")
        //                               };
        //    return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        //}

        #endregion
    }
}
