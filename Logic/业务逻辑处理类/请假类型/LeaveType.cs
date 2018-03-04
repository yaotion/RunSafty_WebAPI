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
    ///Line 的摘要说明
    /// </summary>
    public class LeaveType1
    {
        #region 构造函数
        public LeaveType1()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        //public LeaveType(string TypeID)
        //{
        //    string strSql = "select * from TAB_System_LeaveType where nLeaveTypeID=@nLeaveTypeID";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nLeaveTypeID",TypeID)
        //                               };
        //    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        nLeaveTypeID = dt.Rows[0]["nLeaveTypeID"].ToString();
        //        strLeaveTypeName = dt.Rows[0]["strLeaveTypeName"].ToString();           
        //    }
        //}
        #endregion 构造函数

        #region 增删改
        public int Add(string id , string name)
        {
            string strSql = "insert into TAB_System_LeaveType (nLeaveTypeID,strLeaveTypeName) values (@nLeaveTypeID,@strLeaveTypeName)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nLeaveTypeID",id),
                                           new SqlParameter("strLeaveTypeName",name)                                         
                                       };
            int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            return i;
        }
        public bool Update(string id, string name)
        {
            string strSql = "update TAB_System_LeaveType set strLeaveTypeName = @strLeaveTypeName,nLeaveTypeID=@nLeaveTypeID where nLeaveTypeID=@nLeaveTypeID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nLeaveTypeID",id),
                                           new SqlParameter("strLeaveTypeName",name)   
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string id)
        {
            string strSql = "delete TAB_System_LeaveType where nLeaveTypeID=@nLeaveTypeID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nLeaveTypeID",id)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string id, string name)
        {
            string strSql = "select count(*) from TAB_System_LeaveType where nLeaveTypeID = @nLeaveTypeID or strLeaveTypeName=@strLeaveTypeName ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nLeaveTypeID",id),
                                           new SqlParameter("strLeaveTypeName",name)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        //#region 扩展方法
        public static DataTable GetAlltype(string id, string name)
        {
            string strSql = "select * from TAB_System_LeaveType where 1=1 ";
            strSql += name == "" ? " " : " and strLeaveTypeName like @strLeaveTypeName ";
            strSql += id == "" ? " " : " and nLeaveTypeID = @nLeaveTypeID ";
            strSql += " order by nLeaveTypeID asc ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLeaveTypeName","%" +name+ "%"),
                                           new SqlParameter("nLeaveTypeID",id)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
    }
}
