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
    ///BeginWork 的摘要说明
    /// </summary>
    public class BeginWork
    {
        public BeginWork()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //g 
        }
        /// <summary>
        /// 查询出勤记录
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="TrainmanName"></param>
        /// <param name="TrainmanNumber"></param>
        /// <param name="StationGUID"></param>
        /// <param name="DrinkResult"></param>
        /// <returns></returns>
        public static DataTable QueryBeginWork(int PageIndex, int PageCount, string BeginTime, string EndTime, string TrainmanName, string TrainmanNumber, string StationGUID, int DrinkResult,string TrainPlanGUID)
        {
            string strSql = "";
            string strSqlCondition = " where 1=1 and nPlanState >=9 and ";
             if (TrainPlanGUID != "")
            {
                strSqlCondition += " strTrainPlanGUID=@TrainPlanGUID and ";
            }
            string strSqlCondition1 = " 1=1";
            string strSqlCondition2 = " 1=1";
            string strSqlCondition3 = " 1=1";
            if (BeginTime != "")
            {
                strSqlCondition1 += " and dtBeginWorkTime1 >= @BeginTime";
                strSqlCondition2 += " and dtBeginWorkTime2 >= @BeginTime";
                strSqlCondition3 += " and dtBeginWorkTime3 <= @BeginTime";
            }
            if (EndTime != "")
            {
                strSqlCondition1 += " and dtBeginWorkTime1 <= @EndTime";
                strSqlCondition2 += " and dtBeginWorkTime2 <= @EndTime";
                strSqlCondition3 += " and dtBeginWorkTime3 <= @EndTime";
            }
            if (TrainmanName != "")
            {
                strSqlCondition1 += " and strTrainmanName1 like @TrainmanName";
                strSqlCondition2 += " and strTrainmanName2 like @TrainmanName";
                strSqlCondition3 += " and strTrainmanName3 like @TrainmanName";
            }
            if (TrainmanNumber != "")
            {
                strSqlCondition1 += " and strTrainmanNumber1 = @TrainmanNumber";
                strSqlCondition2 += " and strTrainmanNumber2 = @TrainmanNumber";
                strSqlCondition3 += " and strTrainmanNumber3 = @TrainmanNumber";
            }
            if (StationGUID != "")
            {
                strSqlCondition1 += " and strBeginWorkStationGUID1 = @StationGUID";
                strSqlCondition2 += " and strBeginWorkStationGUID2 = @StationGUID";
                strSqlCondition3 += " and strBeginWorkStationGUID3 = @StationGUID";
            }
            if (DrinkResult > -1)
            {
                strSqlCondition1 += " and nDrinkResult1 = @DrinkResult";
                strSqlCondition2 += " and nDrinkResult2 = @DrinkResult";
                strSqlCondition3 += " and nDrinkResult3 = @DrinkResult";
            }
           
            strSqlCondition += string.Format("(({0}) or ({1}) or ({2}))", strSqlCondition1, strSqlCondition2, strSqlCondition3);

            strSql = "select top " + PageCount.ToString()+" * from VIEW_Plan_ChuQin " + strSqlCondition +
                "  and strTrainPlanGUID not in (select top " +((PageIndex-1)*PageCount).ToString()+" strTrainPlanGUID from VIEW_Plan_ChuQin " + strSqlCondition + " order by dtChuQinTime desc)  order by dtChuQinTime desc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("BeginTime",BeginTime),
                                           new SqlParameter("EndTime",EndTime),
                                           new SqlParameter("TrainmanName",TrainmanName + "%"),
                                           new SqlParameter("TrainmanNumber",TrainmanNumber),
                                           new SqlParameter("DrinkResult",DrinkResult),
                                           new SqlParameter("StationGUID",StationGUID),
                                           new SqlParameter("TrainPlanGUID",TrainPlanGUID)                                       
                                       };           
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }

        /// <summary>
        /// 获取乘务员的出勤卡控记录
        /// </summary>
        /// <param name="TrainmanGUID"></param>
        /// <param name="CheckTime"></param>
        /// <returns></returns>
        public static DataTable GetCheckPointRecord(string TrainmanGUID, DateTime CheckTime)
        {
            DateTime dtMaxTime = CheckTime.AddMinutes(30);
            DateTime dtMinTime = CheckTime.AddMinutes(-30);
            string strSql = "PROC_CheckPoint_GetRecords";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TrainmanGUID",TrainmanGUID),
                                           new SqlParameter("MinCheckTime",dtMinTime),
                                           new SqlParameter("MaxCheckTime",dtMaxTime)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.StoredProcedure, strSql, sqlParams).Tables[0];
        }
    }
}