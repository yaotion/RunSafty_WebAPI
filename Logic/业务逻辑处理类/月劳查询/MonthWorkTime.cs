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
    ///MonthWorkTime功能：提供月劳详细信息统计
    /// </summary>
    public class MonthWorkTime
    {
        #region 扩展方法
        public static DataTable totalMonthTime(string where)
        {
            string strSql = @"select sum(fMonthRunTotalTime) as totalMonthRunTotalTime,sum(fMonthBeginTotalTime) as totalMonthBeginTotalTime,sum(fMonthEndTotalTime) as totalMonthEndTotalTime,sum(fMonthTotalTime) as totalMonthTotalTime,(select count(*) from (select strTrainmanNumber  from VIEW_WorkTime_Turn_Trainman 
where 1=1 " + where + " group by strTrainmanNumber) b ) as TotalPeople from(select strTrainmanNumber,strTrainmanName,sum(fRunTotalTime) as fMonthRunTotalTime,sum(fBeginTotalTime) as fMonthBeginTotalTime,sum(fEndTotalTime) as fMonthEndTotalTime,sum(fTotalTime) as fMonthTotalTime from VIEW_WorkTime_Turn_Trainman where 1=1 " + where + " group by strTrainmanNumber,strTrainmanName) a ";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取乘务员月趟数和月劳时
        /// </summary>
        /// <param name="TrainmanGUID"></param>
        /// <param name="QueryYear"></param>
        /// <param name="QueryMonth"></param>
        /// <param name="TurnCount"></param>
        /// <param name="TimeCount"></param>
        public static void QueryWorkTime(string TrainmanGUID, int QueryYear, int QueryMonth, out int TurnCount,out int TimeCount)
        {
            TurnCount = 0;
            TimeCount = 0;
            DateTime dtBeginTime = new DateTime(QueryYear, QueryMonth, 1);

            DateTime dtEndTime = dtBeginTime.AddMonths(1).AddSeconds(-1);

            string strSql = @"select count(*) as TurnCount,sum(fTotalTime) as TimeCount from VIEW_WorkTime_Turn 
                Where (strtrainmanguid1=@TrainmanGUID or strtrainmanguid2=@TrainmanGUID or strtrainmanguid3=@TrainmanGUID) 
                        and (dtkcTime between @dtBeginTime and @dtEndTime) and dtEndWorkTime is not null";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TrainmanGUID",TrainmanGUID),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("dtEndTime",dtEndTime)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TurnCount = PageBase.static_ext_int(dt.Rows[0]["TurnCount"]);
                TimeCount = PageBase.static_ext_int(dt.Rows[0]["TimeCount"]);
            }
        }

        /// <summary>
        /// 月劳时统计信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable MonthTotalInfo(string where)
        {
            string strSql = @"select strTrainmanNumber,sum(fTotalTime) as totalMonthTotalTime from VIEW_WorkTime_Turn_Trainman  where 1=1 " + where + " group by strTrainmanNumber";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        #endregion
    }
}
