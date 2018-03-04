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
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///CallRecord 的摘要说明
    /// </summary>
    public class CallRecord
    {
        public CallRecord()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static DataTable QueryCallRecords(int PageIndex, int PageCount, string BeginTime, string EndTime, string AreaGUID, string RoomNumber, string TrainNo, int PlanType,string TrainPlanGUID)
        {
            string strSql = @"  select (select count(*) from TAB_RoomWaiting_CallRecord where TAB_RoomWaiting_CallRecord.strGUID=VIEW_RestInWaiting_CallRecord.strPlanGUID) as QXCount ,
                                    * from VIEW_RestInWaiting_CallRecord ";

            string strSqlCondition = " where 1=1 ";
            if (AreaGUID != "")
            {
                strSqlCondition += " and strAreaGUID = @AreaGUID ";
            }
            if (BeginTime != "")
            {
                strSqlCondition += " and dtCreateTime >= @BeginTime";
            }
            if (EndTime != "")
            {
                strSqlCondition += " and dtCreateTime <= @EndTime";
            }



            if (PlanType >= 0)
            {
                strSqlCondition += " and (select count(*) from TAB_RoomWaiting_CallRecord where TAB_RoomWaiting_CallRecord.strGUID=VIEW_RestInWaiting_CallRecord.strPlanGUID) = @PlanType";
            }
            if (RoomNumber != "")
            {
                strSqlCondition += " and strRoomNumber = @RoomNumber";
            }

            if (TrainNo != "")
            {
                strSqlCondition += " and strTrainNo = @TrainNo";
            }

            if (TrainPlanGUID != "")
            {
                strSqlCondition += " and strPlanGUID = @TrainPlanGUID";
            }

            strSqlCondition += "  order by dtCreateTime desc ";

            strSql += strSqlCondition;
            SqlParameter[] sqlParams = {
                                           new SqlParameter("AreaGUID",AreaGUID),
                                           new SqlParameter("TrainPlanGUID",TrainPlanGUID),
                                           new SqlParameter("BeginTime",BeginTime),
                                           new SqlParameter("EndTime",EndTime),
                                           new SqlParameter("PlanType",PlanType),
                                           new SqlParameter("RoomNumber",RoomNumber),
                                           new SqlParameter("TrainNo",TrainNo)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
        }
    }
}
