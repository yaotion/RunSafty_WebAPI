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

namespace ThinkFreely.RunSafty.Org
{
    /// <summary>
    ///Account 的摘要说明
    /// </summary>
    public class DutyUser
    {
        #region 私有变量、属性
        public string strDutyGUID = "";
        public string strPassword = "";
        public string strDutyName = "";
        public string strDutyNumber = "";
        public int nDeleteState = 0;
        public string strTokenID = "";
        public DateTime dtLoginTime = DateTime.Parse("1900-01-01 00:00:00");
        public DateTime dtTokenTime = DateTime.Parse("1900-01-01 00:00:00");
        public int nRoleID = 0;
        public DateTime dtCreateTime = DateTime.Parse("1900-01-01 00:00:00");
        public string strAreaGUID = "";
        #endregion 私有变量、属性

        #region 构造函数
        public DutyUser()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public DutyUser(string DutyGUID)
        {
            string strSql = "select top 1 * from TAB_Org_DutyUser where strDutyGUID=@DutyGUID";
            SqlParameter[] sqlParams = {
                                              new SqlParameter("DutyGUID",DutyGUID)                                              
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strDutyGUID = dt.Rows[0]["strDutyGUID"].ToString();
                strPassword = dt.Rows[0]["strPassword"].ToString();
                strDutyName = dt.Rows[0]["strDutyName"].ToString();
                strDutyNumber = dt.Rows[0]["strDutyNumber"].ToString();
                nDeleteState = int.Parse(dt.Rows[0]["nDeleteState"].ToString());
                strTokenID = dt.Rows[0]["strTokenID"].ToString();
                if (!DateTime.TryParse(dt.Rows[0]["dtLoginTime"].ToString(), out dtLoginTime))
                {
                    dtLoginTime = DateTime.Parse("1900-01-01 00:00:00");
                }
                if (!DateTime.TryParse(dt.Rows[0]["dtTokenTime"].ToString(), out dtTokenTime))
                {
                    dtTokenTime = DateTime.Parse("1900-01-01 00:00:00");
                }
                nRoleID = int.Parse(dt.Rows[0]["nRoleID"].ToString());
                if (!DateTime.TryParse(dt.Rows[0]["dtCreateTime"].ToString(), out dtCreateTime))
                {
                    dtCreateTime = DateTime.Parse("1900-01-01 00:00:00");
                }
                strAreaGUID = dt.Rows[0]["strAreaGUID"].ToString();
            }
        }
       
        public DutyUser(string TokenID, long TokenSecond, long LoginSecond)
        {
            string strCondition = " where strTokenID = @TokenID and nDeleteState = 0 ";
            if (TokenSecond > -1)
            {
                strCondition += " and dateadd(ss,@TokenSecond,dtTokenTime) >= getdate()";
            }
            if (LoginSecond > -1)
            {
                strCondition += " and dateadd(ss,@LoginSecond,dtLoginTime) >= getdate()";
            }
            SqlParameter[] sqlParams = {
                                              new SqlParameter("TokenID",TokenID),
                                              new SqlParameter("TokenSecond",TokenSecond),
                                              new SqlParameter("LoginSecond",LoginSecond)
                                          };
            string strSql = "select top 1 * from TAB_Org_DutyUser " + strCondition;
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strDutyGUID = dt.Rows[0]["strDutyGUID"].ToString();
                strPassword = dt.Rows[0]["strPassword"].ToString();
                strDutyName = dt.Rows[0]["strDutyName"].ToString();
                strDutyNumber = dt.Rows[0]["strDutyNumber"].ToString();
                nDeleteState = int.Parse(dt.Rows[0]["nDeleteState"].ToString());
                strTokenID = dt.Rows[0]["strTokenID"].ToString();
                if (!DateTime.TryParse(dt.Rows[0]["dtLoginTime"].ToString(), out dtLoginTime))
                {
                    dtLoginTime = DateTime.Parse("1900-01-01 00:00:00");
                }
                if (!DateTime.TryParse(dt.Rows[0]["dtTokenTime"].ToString(), out dtTokenTime))
                {
                    dtTokenTime = DateTime.Parse("1900-01-01 00:00:00");
                }
                nRoleID = int.Parse(dt.Rows[0]["nRoleID"].ToString());
                if (!DateTime.TryParse(dt.Rows[0]["dtCreateTime"].ToString(), out dtCreateTime))
                {
                    dtCreateTime = DateTime.Parse("1900-01-01 00:00:00");
                }
                strAreaGUID = dt.Rows[0]["strAreaGUID"].ToString();
            }

        }

        public DutyUser(string DutyName, string Password)
        {
            string strSql = "select top  1 * from TAB_Org_DutyUser where strDutyNumber=@DutyName and strPassword = @Password and nDeleteState = 0";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("DutyName",DutyName),
                                           new SqlParameter("Password",Password)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strDutyGUID = dt.Rows[0]["strDutyGUID"].ToString();
                strPassword = dt.Rows[0]["strPassword"].ToString();
                strDutyName = dt.Rows[0]["strDutyName"].ToString();
                strDutyNumber = dt.Rows[0]["strDutyNumber"].ToString();
                nDeleteState = int.Parse(dt.Rows[0]["nDeleteState"].ToString());
                strTokenID = dt.Rows[0]["strTokenID"].ToString();
                if (!DateTime.TryParse(dt.Rows[0]["dtLoginTime"].ToString(), out dtLoginTime))
                {
                    dtLoginTime = DateTime.Parse("1900-01-01 00:00:00");
                }
                if (!DateTime.TryParse(dt.Rows[0]["dtTokenTime"].ToString(), out dtTokenTime))
                {
                    dtTokenTime = DateTime.Parse("1900-01-01 00:00:00");
                }
                nRoleID = int.Parse(dt.Rows[0]["nRoleID"].ToString());
                if (!DateTime.TryParse(dt.Rows[0]["dtCreateTime"].ToString(), out dtCreateTime))
                {
                    dtCreateTime = DateTime.Parse("1900-01-01 00:00:00");
                }
                strAreaGUID = dt.Rows[0]["strAreaGUID"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Org_DutyUser (strDutyGUID,strDutyName,strDutyNumber,strPassword,nDeleteState,strTokenID,dtTokenTime,dtLoginTime,nRoleID,dtCreateTime,strAreaGUID) values " +
                "(@DutyGUID,@DutyName,@DutyNumber,@Password,@DeleteState,@TokenID,@TokenTime,@LoginTime,@RoleID,@CreateTime,@AreaGUID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("DutyGUID",guid),
                                           new SqlParameter("DutyName",strDutyName),
                                           new SqlParameter("DutyNumber",strDutyNumber),
                                           new SqlParameter("Password",strPassword),
                                           new SqlParameter("DeleteState",nDeleteState),
                                           new SqlParameter("TokenID",strTokenID),
                                           new SqlParameter("TokenTime",dtTokenTime),
                                           new SqlParameter("LoginTime",dtLoginTime),
                                           new SqlParameter("RoleID",nRoleID),
                                           new SqlParameter("CreateTime",dtCreateTime),
                                           new SqlParameter("AreaGUID",strAreaGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = @"update TAB_Org_DutyUser set strDutyName=@DutyName,nDeleteState=@nDeleteState,strDutyNumber=@DutyNumber,strPassword=@Password,
                strTokenID=@TokenID,dtTokenTime=@TokenTime,dtLoginTime=@LoginTime,nRoleID=@RoleID,strAreaGUID=@AreaGUID where strDutyGUID = @DutyGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("DutyGUID",strDutyGUID),
                                           new SqlParameter("DutyName",strDutyName),
                                           new SqlParameter("DutyNumber",strDutyNumber),
                                           new SqlParameter("Password",strPassword),
                                           new SqlParameter("TokenID",strTokenID),
                                           new SqlParameter("TokenTime",dtTokenTime),
                                           new SqlParameter("LoginTime",dtLoginTime),
                                           new SqlParameter("RoleID",nRoleID),
                                           new SqlParameter("nDeleteState",nDeleteState),
                                           new SqlParameter("AreaGUID",strAreaGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
           
        }
        public static bool Deletemm(string DutyGUID)
        {
            string strSql = "update TAB_Org_DutyUser set strPassword='' where strDutyGUID = @DutyGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("DutyGUID",DutyGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public static bool Delete(string DutyGUID)
        {
            string strSql = "delete TAB_Org_DutyUser where strDutyGUID = @DutyGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("DutyGUID",DutyGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public static bool Exist(string DutyGUID,string DutyNumber)
        {
            string strSql = "select count(*) from TAB_Org_DutyUser where strDutyNumber = @DutyNumber";
            if (DutyGUID.Trim() != "")
            {
                strSql += " and strDutyGUID <> @DutyGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("DutyGUID",DutyGUID),
                                           new SqlParameter("DutyNumber",DutyNumber)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;

        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetDutyUsers(int PageIndex, int PageCount, string DutyName, string DutyNumber,string AreaGUID)
        {
            string strCondition = " where nDeleteState = 0";
            if (DutyName != "")
            {
                strCondition += " and strDutyName like @DutyName";
            }
            if (DutyNumber != "")
            {
                strCondition += " and strDutyNumber like @DutyNumber";
            }
            if (AreaGUID != "")
            {
                strCondition += " and strAreaGUID = @AreaGUID";
            }
            string strSql = "select top " + (PageCount.ToString()) + " * from TAB_Org_DutyUser " + strCondition +
                " and strDutyGUID not in (select top " + ((PageIndex - 1) * PageCount).ToString() + " strDutyGUID from TAB_Org_DutyUser order by strDutyNumber) order by strDutyNumber";
            SqlParameter[] sqlParams = {
                               new SqlParameter("DutyName","%" +DutyName+ "%"),
                               new SqlParameter("DutyNumber","%" +DutyNumber+ "%"),
                               new SqlParameter("AreaGUID",AreaGUID)
                           };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];


        }

        public static Int32 GetDutyUserCount(string DutyName, string DutyNumber, string AreaGUID)
        {
            string strCondition = " where nDeleteState = 0";
            if (DutyName != "")
            {
                strCondition += " and strDutyName like @DutyName";
            }
            if (DutyNumber != "")
            {
                strCondition += " and strDutyNumber like @DutyNumber";
            }
            if (AreaGUID != "")
            {
                strCondition += " and strAreaGUID = @AreaGUID";
            }
            string strSql = "select count(*) from TAB_Org_DutyUser " + strCondition;
            SqlParameter[] sqlParams = {
                               new SqlParameter("DutyName","%" +DutyName+ "%"),
                               new SqlParameter("DutyNumber","%" +DutyNumber+ "%"),
                               new SqlParameter("AreaGUID",AreaGUID)
                           };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams));
        }

        public static DataTable GetAllDutyUsers()
        {
            return GetDutyUsers(1, 10000, "", "", "");
        }
        #endregion
    }
}

