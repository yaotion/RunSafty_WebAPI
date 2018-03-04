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
    ///Zzttj功能：提供班组状态统计
    /// </summary>
    public class Zzttj
    {
        #region 方法
        public static DataTable GetTrainPlanGuidFromTime(string prevtime,string time)
        {
            string strSql = "select strtrainplanguid from view_plan_train where dtRealStartTime>@prevtime and dtRealStartTime<@time and (dtLastArriveTime is null or dtLastArriveTime>@time)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("prevtime",prevtime),
                                           new SqlParameter("time",time)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static DataTable GetTrainPlanGuidFromTimeRunevent(string prevtime, string time)
        {
            string strSql = "select * from tab_plan_runevent where strtrainplanguid in (select strtrainplanguid from view_plan_train where dtRealStartTime<@prevtime and dtRealStartTime>@time and dtLastArriveTime is null)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("prevtime",time),
                                           new SqlParameter("time",prevtime)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }


        public static int GetRunCountInTime(DateTime RunTime)
        {
            string strSql = @"select count(*) from tab_plan_Train where dtStartTime < @dtStartTime and nPlanState >=7 and
                                (
                                   (select count(1) from tab_plan_runevent where strTrainPlanGUID=tab_plan_Train.strTrainPlanGUID and nEventID = 10003) > 0 AND

                                    (select count(1) from tab_plan_runevent where strTrainPlanGUID=tab_plan_Train.strTrainPlanGUID and nEventID = 10005) = 0
                                    or
                                    (select max(dtEventTime) from tab_plan_runevent where strTrainPlanGUID=tab_plan_Train.strTrainPlanGUID and nEventID = 10005) > @dtStartTime
                                )";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("dtStartTime",RunTime)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams));
        }

        public static int GetRestCountInTime(DateTime RestTime)
        {
            string strSql = @"select count(*) from tab_plan_Train where dtStartTime < @dtStartTime  and nPlanState >=7 and 
                            (
                                (select min(dtEventTime) from tab_plan_runevent where strTrainPlanGUID=tab_plan_Train.strTrainPlanGUID and nEventID = 10001) < @dtStartTime
                                and
                                not((select min(dtEventTime) from tab_plan_runevent where strTrainPlanGUID=tab_plan_Train.strTrainPlanGUID and nEventID = 10005) <  @dtStartTime) and
                                    (
                                    (select count (1) from tab_plan_runevent where strTrainPlanGUID=tab_plan_Train.strTrainPlanGUID and nEventID = 10002) = 0
                                    or
                                    (select max(dtEventTime) from tab_plan_runevent where strTrainPlanGUID=tab_plan_Train.strTrainPlanGUID and nEventID = 10002) > @dtStartTime
                                )
                            )";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("dtStartTime",RestTime)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams));

        }
        #endregion
    }
}
