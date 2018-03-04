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
    ///Area 的摘要说明
    /// </summary>
    public class Area
    {
        #region 属性
        public string strGUID = "";
        public string strAreaName = "";
        public string strJWDNumber = "";
        #endregion 属性

        #region 构造函数
        public Area()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public Area(string AreaGUID)
        {
            string strSql = "select * from TAB_Org_Area where strGUID=@AreaGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("AreaGUID",AreaGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strGUID = dt.Rows[0]["strGUID"].ToString();
                strAreaName = dt.Rows[0]["strAreaName"].ToString();
                strJWDNumber = dt.Rows[0]["strJWDNumber"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Org_Area (strGUID,strAreaName,strJWDNumber) values (@strGUID,@strAreaName,@strJWDNumber)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",guid),
                                           new SqlParameter("strAreaName",strAreaName),
                                           new SqlParameter("strJWDNumber",strJWDNumber) 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Org_Area set strAreaName = @strAreaName,strJWDNumber=@strJWDNumber where strGUID=@strGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",strGUID),
                                           new SqlParameter("strAreaName",strAreaName),
                                           new SqlParameter("strJWDNumber",strJWDNumber)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string AreaGUID)
        {
            string strSql = "delete TAB_Org_Area where strGUID=@strGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",AreaGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string AreaGUID,string AreaName)
        {
            string strSql = "select count(*) from TAB_Org_Area where strAreaName=@strAreaName ";
            if (AreaGUID != "")
            {
                strSql += " and strGUID <> @strGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",AreaGUID),
                                           new SqlParameter("strAreaName",AreaName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllAreas(string AreaName)
        {
            string strSql = "select * from VIEW_Org_Area ";
            if (AreaName != "")
            {
                strSql += " and strAreaName like @strAreaName ";
            }
            strSql += " order by strAreaName ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strAreaName","%" +AreaName+ "%")
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static DataTable GetAllAreasDic(string DefaultName)
        {
            DataTable dtResult = GetAllAreas("");
            DataRow dr = dtResult.NewRow();
            dr["strGUID"] = "";
            dr["strAreaName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }

        #endregion        
    }
    public class WorkShopParam
    {
        public DateTime dtAutoSaveTime1 = DateTime.Parse("2000-01-01 00:00:00");
        public DateTime dtAutoSaveTime2 = DateTime.Parse("2000-01-01 00:00:00");
        public DateTime dtPlanBeginTime = DateTime.Parse("2000-01-01 00:00:00");
        public DateTime dtPlanEndTime = DateTime.Parse("2000-01-01 00:00:00");
        public DateTime dtKeepSleepTime = DateTime.Parse("2000-01-01 00:00:00");
        public string strWorkShopGUID = "";
        public string strWEBHost = "";
        public bool bEnableSleepCheck = false;

        public static bool GetParams(string WorkShopGUID ,WorkShopParam Param)
        {
            string strSql = "select * from TAB_Base_PlanParam where strWorkShopGUID=@strWorkShopGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",WorkShopGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Param.dtAutoSaveTime1 = DateTime.Parse(dt.Rows[0]["dtAutoSaveTime1"].ToString());
                Param.dtAutoSaveTime2 = DateTime.Parse(dt.Rows[0]["dtAutoSaveTime2"].ToString());
                Param.dtPlanBeginTime = DateTime.Parse(dt.Rows[0]["dtPlanBeginTime"].ToString());
                Param.dtPlanEndTime = DateTime.Parse(dt.Rows[0]["dtPlanEndTime"].ToString());
                Param.dtKeepSleepTime = DateTime.Parse(dt.Rows[0]["dtKeepSleepTime"].ToString());
                Param.strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                Param.strWEBHost = dt.Rows[0]["strWEBHost"].ToString();
                Param.bEnableSleepCheck = bool.Parse(dt.Rows[0]["bEnableSleepCheck"].ToString());
                return true;
            }
            return false;
        }

        public static void UpdateParams(WorkShopParam Param)
        {
            string strSql = "delete from TAB_Base_PlanParam where strWorkShopGUID=@strWorkShopGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",Param.strWorkShopGUID)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            string strSqlAdd = @"insert into TAB_Base_PlanParam (dtAutoSaveTime1,dtAutoSaveTime2,dtPlanBeginTime,dtPlanEndTime,dtKeepSleepTime,
                        strWorkShopGUID,strWEBHost,bEnableSleepCheck) values (@dtAutoSaveTime1,@dtAutoSaveTime2,@dtPlanBeginTime,@dtPlanEndTime,@dtKeepSleepTime,
                        @strWorkShopGUID,@strWEBHost,@bEnableSleepCheck)";
            SqlParameter[] sqlParamsAdd = {
                                           new SqlParameter("dtAutoSaveTime1",Param.dtAutoSaveTime1),
                                           new SqlParameter("dtAutoSaveTime2",Param.dtAutoSaveTime2),
                                           new SqlParameter("dtPlanBeginTime",Param.dtPlanBeginTime),
                                           new SqlParameter("dtPlanEndTime",Param.dtPlanEndTime),
                                           new SqlParameter("dtKeepSleepTime",Param.dtKeepSleepTime),
                                           new SqlParameter("strWorkShopGUID",Param.strWorkShopGUID),
                                           new SqlParameter("strWEBHost",Param.strWEBHost),
                                           new SqlParameter("bEnableSleepCheck",Param.bEnableSleepCheck)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSqlAdd, sqlParamsAdd);
        }
    }
}
