using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.SiteLogic
{
    //数据库字典
    class DBDictionary
    {
        /// <summary>
        /// 通过出勤地点ID获取出勤点名称
        /// </summary>
        /// <param name="PlaceID"></param>
        /// <returns></returns>
        public static string GetDutyPlaceName(string PlaceID)
        {
            string sql = "select top 1 * from TAB_Base_DutyPlace where strPlaceID = '" + PlaceID + "'";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["strPlaceName"].ToString();
            else
                return "";
        }
        /// <summary>
        /// 通过出勤点姓名获取出勤点ID
        /// </summary>
        /// <param name="PlaceName"></param>
        /// <returns></returns>
        public static string GetDutyPlaceID(string PlaceName)
        {
            string sql = "select top 1 * from TAB_Base_DutyPlace where strPlaceName = '" + PlaceName + "'";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["strPlaceID"].ToString();
            else
                return "";
        }
        /// <summary>
        /// 通过工号获取人员信息
        /// </summary>
        /// <param name="tmid"></param>
        /// <param name="trainman"></param>
        /// <returns></returns>
        public static Boolean GetTrainman(string tmid,Trainman trainman)
        {
            string sql = @"select top 1 TAB_Org_Trainman.strTrainmanGUID,TAB_Org_Trainman.strTrainmanNumber,
            TAB_Org_Trainman.strTrainmanName,TAB_Org_Trainman.strWorkShopGUID,TAB_Org_WorkShop.strWorkShopName 
            from TAB_Org_Trainman left join TAB_Org_WorkShop on TAB_Org_Trainman.strWorkShopGUID = TAB_Org_WorkShop.strWorkShopGUID  
            where TAB_Org_Trainman.strTrainmanNumber = '" + tmid + "'";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];

            if (dt.Rows.Count > 0)
            {
                trainman.tmGUID = dt.Rows[0]["strTrainmanGUID"].ToString();
                trainman.tmid = dt.Rows[0]["strTrainmanNumber"].ToString();
                trainman.tmname = dt.Rows[0]["strTrainmanName"].ToString();
                trainman.workShopID = dt.Rows[0]["strWorkShopGUID"].ToString();
                trainman.workShopName = dt.Rows[0]["strWorkShopName"].ToString(); 
                
                return true;
            }            
            else
                return false;
        }
    }

    class DBPlan
    {
        /// <summary>
        /// 根据人员工号及事件时间获取出勤计划，取事件时间向前3天至当前时间的最后一条计划
        /// </summary>
        /// <param name="tmid"></param>
        /// <param name="eventTime"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public static Boolean GetTrainPlanBriefByRange(string tmid, DateTime eventTime, TrainmanPlan Plan,out int tmindex)
        {
            tmindex = 1;
        
            string sql = @"select top 1 * from VIEW_Plan_Trainman 
                    where dtStartTime > DATEADD (Day,-3,@eventTime) and dtStartTime < @eventTime  and nPlanState >= 4 
                    and  ((strTrainmanNumber1 = @tmid) or 
                    (strTrainmanNumber2 = @tmid) or 
                    (strTrainmanNumber3 = @tmid) or 
                    (strTrainmanNumber4 = @tmid)) order by dtStartTime desc";

            SqlParameter[] sqlParams = {
                                        new SqlParameter("@eventTime",eventTime),
                                        new SqlParameter("@tmid",tmid)
                                       };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlParams).Tables[0];

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                Plan.trainPlan.planID = dt.Rows[0]["strTrainPlanGUID"].ToString();
                Plan.trainPlan.lastArriveTime = DataTypeConvert.ToDateTime(dt.Rows[0]["dtlastArriveTime"]);
                Plan.trainPlan.startTime = DataTypeConvert.ToDateTime(dt.Rows[0]["dtStartTime"]);
                Plan.trainPlan.createTime = DataTypeConvert.ToDateTime(dt.Rows[0]["dtCreateTime"]);

                Plan.tmGUID1 = dt.Rows[0]["strTrainmanGUID1"].ToString();
                Plan.tmGUID2 = dt.Rows[0]["strTrainmanGUID2"].ToString();
                Plan.tmGUID3 = dt.Rows[0]["strTrainmanGUID3"].ToString();
                Plan.tmGUID4 = dt.Rows[0]["strTrainmanGUID4"].ToString();

                Plan.tmid1 = dt.Rows[0]["strTrainmanNumber1"].ToString();
                Plan.tmid2 = dt.Rows[0]["strTrainmanNumber2"].ToString();
                Plan.tmid3 = dt.Rows[0]["strTrainmanNumber3"].ToString();
                Plan.tmid4 = dt.Rows[0]["strTrainmanNumber4"].ToString();

                Plan.tmname1 = dt.Rows[0]["strTrainmanName1"].ToString();
                Plan.tmname2 = dt.Rows[0]["strTrainmanName2"].ToString();
                Plan.tmname3 = dt.Rows[0]["strTrainmanName3"].ToString();
                Plan.tmname4 = dt.Rows[0]["strTrainmanName4"].ToString();

                if (Plan.tmid1 == tmid)
                    tmindex = 1;
                else
                    if (Plan.tmid2 == tmid)
                        tmindex = 2;
                    else
                        if (Plan.tmid3 == tmid)
                            tmindex = 3;

                    return true;
            }
        }

        /// <summary>
        /// 获取人员计划，只获取计划简略信息及人员信息
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public static Boolean GetTrainmanBrief(string planid,TrainmanPlan Plan)
        {
            string sql = @"select top 1 * from VIEW_Plan_Trainman where strTrainPlanGUID = @planid";

            SqlParameter[] sqlparam= {new SqlParameter("@planid", planid)};
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlparam).Tables[0];

            if (dt.Rows.Count == 0)
                return false;
            else
            {
                Plan.trainPlan.planID = dt.Rows[0]["strTrainPlanGUID"].ToString();
                Plan.trainPlan.lastArriveTime = Convert.ToDateTime(dt.Rows[0]["dtlastArriveTime"]);
                Plan.trainPlan.startTime = Convert.ToDateTime(dt.Rows[0]["dtStartTime"]);
                Plan.trainPlan.createTime = Convert.ToDateTime(dt.Rows[0]["dtCreateTime"]);

                Plan.tmGUID1 = dt.Rows[0]["strTrainmanGUID1"].ToString();
                Plan.tmGUID2 = dt.Rows[0]["strTrainmanGUID2"].ToString();
                Plan.tmGUID3 = dt.Rows[0]["strTrainmanGUID3"].ToString();
                Plan.tmGUID4 = dt.Rows[0]["strTrainmanGUID4"].ToString();

                Plan.tmid1 = dt.Rows[0]["strTrainmanNumber1"].ToString();
                Plan.tmid2 = dt.Rows[0]["strTrainmanNumber2"].ToString();
                Plan.tmid3 = dt.Rows[0]["strTrainmanNumber3"].ToString();
                Plan.tmid4 = dt.Rows[0]["strTrainmanNumber4"].ToString();

                Plan.tmname1 = dt.Rows[0]["strTrainmanName1"].ToString();
                Plan.tmname2 = dt.Rows[0]["strTrainmanName2"].ToString();
                Plan.tmname3 = dt.Rows[0]["strTrainmanName3"].ToString();
                Plan.tmname4 = dt.Rows[0]["strTrainmanName4"].ToString();

                return true;
            }


        }


    }
}
