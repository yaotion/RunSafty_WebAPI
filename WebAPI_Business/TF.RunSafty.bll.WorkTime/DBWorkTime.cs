using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;

namespace TF.RunSafty.WorkTime
{
    class DBWorkTime
    {
        public void CalcWorkTime(string PlanID)
        {
            string sql = string.Format("Exec Proc_WorkTime_Calc '%s'", PlanID);
            
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql);            
        }
        private WorkTimeEntity dtRowToWorkTime(DataRow dr)
        {
            
            WorkTimeEntity result = new WorkTimeEntity();
            result.nid = ObjectConvertClass.static_ext_int(dr["nid"]);

           
            result.strFlowID = ObjectConvertClass.static_ext_string(dr["strFlowID"]);
            result.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
            result.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            result.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
            result.dtBeginWorkTime = ObjectConvertClass.static_ext_Date(dr["dtBeginWorkTime"]);
            result.dtStartTime = ObjectConvertClass.static_ext_Date(dr["dtkcTime"]);
            result.dtArriveTime = ObjectConvertClass.static_ext_Date(dr["dtArriveTime"]);
            result.dtInRoomTime = ObjectConvertClass.static_ext_Date(dr["dtInRoomTime"]);
            result.dtOutRoomTime = ObjectConvertClass.static_ext_Date(dr["dtOutRoomTime"]);
            ObjectConvertClass.static_ext_Date(dr["dtOutRoomTime"]);
            result.dtStartTime2 = ObjectConvertClass.static_ext_Date(dr["dtStartTime2"]);
            result.dtArriveTime2 = ObjectConvertClass.static_ext_Date(dr["dtArriveTime2"]);
            result.dtEndWorkTime = ObjectConvertClass.static_ext_Date(dr["dtEndWorkTime"]);
            result.fRunTotalTime = ObjectConvertClass.static_ext_int(dr["fRunTotalTime"]);
            result.fBeginTotalTime = ObjectConvertClass.static_ext_int(dr["fBeginTotalTime"]);
            result.fEndTotalTime = ObjectConvertClass.static_ext_int(dr["fEndTotalTime"]);
            result.fTotalTime = ObjectConvertClass.static_ext_int(dr["fTotalTime"]);

            result.nFlowState = ObjectConvertClass.static_ext_int(dr["nFlowState"]);
            result.nkehuoID = ObjectConvertClass.static_ext_int(dr["nkehuoID"]);
            result.nNoticeState = ObjectConvertClass.static_ext_int(dr["nNoticeState"]);


            result.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
            result.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
            result.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
            result.strWorkShopNumber = ObjectConvertClass.static_ext_string(dr["strWorkShopNumber"]);
            result.strDestStationTMIS = ObjectConvertClass.static_ext_string(dr["strDestStationTMIS"]);
            result.strDestStationName = ObjectConvertClass.static_ext_string(dr["strDestStationName"]);

            result.bLocalOutDepots  = ObjectConvertClass.static_ext_int(dr["bLocalOutDepots"]);
            result.dtLocalOutDepotsTime  = ObjectConvertClass.static_ext_Date(dr["dtLocalOutDepotsTime"]);

            result.bDestInDepots  = ObjectConvertClass.static_ext_int(dr["bDestInDepots"]);
            result.dtDestInDepotsTime  = ObjectConvertClass.static_ext_Date(dr["dtDestInDepotsTime"]);

            result.bDestOutDepots   = ObjectConvertClass.static_ext_int(dr["bDestOutDepots"]);
            result.dtDestOutDepotsTime   = ObjectConvertClass.static_ext_Date(dr["dtDestOutDepotsTime"]);

            result.bLocalInDepots   = ObjectConvertClass.static_ext_int(dr["bLocalInDepots"]);
            result.dtLocalInDepotsTime   = ObjectConvertClass.static_ext_Date(dr["dtLocalInDepotsTime"]);

            result.strBackTrainTypeName   = ObjectConvertClass.static_ext_string(dr["strBackTrainTypeName"]);
            result.strBackTrainNumber   = ObjectConvertClass.static_ext_string(dr["strBackTrainNumber"]);
            result.strBackTrainNo    = ObjectConvertClass.static_ext_string(dr["strBackTrainNo"]);
            result.dtRealArriveTime    = ObjectConvertClass.static_ext_Date(dr["dtRealArriveTime"]);
            result.bConfirm    = ObjectConvertClass.static_ext_int(dr["bConfirm"]);
            result.dtConfirmTime    = ObjectConvertClass.static_ext_Date(dr["dtConfirmTime"]);
            result.strConfirmDutyUser     = ObjectConvertClass.static_ext_string(dr["strConfirmDutyUser"]);
            result.nLocalStopMinutes     = ObjectConvertClass.static_ext_int(dr["nLocalStopMinutes"]);
            result.nRemoteStopMinutes     = ObjectConvertClass.static_ext_int(dr["nRemoteStopMinutes"]);
            result.strArriveStationTMIS     = ObjectConvertClass.static_ext_string(dr["strArriveStationTMIS"]);
            result.strArriveStationName      = ObjectConvertClass.static_ext_string(dr["strArriveStationName"]);

            result.nAlarmMinutes      = ObjectConvertClass.static_ext_int(dr["nAlarmMinutes"]);
            result.nOutMinutes      = ObjectConvertClass.static_ext_int(dr["nOutMinutes"]);

            //如果超劳时小于0则置为0
            if (ObjectConvertClass.static_ext_int(dr["nOutTotalTime"]) < 0)
            {
                result.nOutTotalTime = 0;
            }
            else
            {
                result.nOutTotalTime = ObjectConvertClass.static_ext_int(dr["nOutTotalTime"]);
            }
              

            result.dtFileBeginTime      = ObjectConvertClass.static_ext_Date(dr["dtFileBeginTime"]);
            result.dtFileEndTime      = ObjectConvertClass.static_ext_Date(dr["dtFileEndTime"]);

            result.nChuQinTypeID = ObjectConvertClass.static_ext_int(dr["nChuQinTypeID"]);
            result.nTuiQinTypeID = ObjectConvertClass.static_ext_int(dr["nTuiQinTypeID"]);

            //停留时间
            result.nGoRunTotalMinutes = ObjectConvertClass.static_ext_int(dr["nGoRunTotalMinutes"]);
            result.nBackRunTotalMinutes = ObjectConvertClass.static_ext_int(dr["nBackRunTotalMinutes"]);

            //技术速度
            result.fGoSpeed = ObjectConvertClass.static_ext_double(dr["fGoSpeed"]);
            result.fBackSpeed = ObjectConvertClass.static_ext_double(dr["fBackSpeed"]);
            

            result.TrainmanPlan.TrainPlan.strTrainJiaoluName =  ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
            result.TrainmanPlan.TrainPlan.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
            result.TrainmanPlan.TrainPlan.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
            result.TrainmanPlan.TrainPlan.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
            result.TrainmanPlan.TrainPlan.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
            result.TrainmanPlan.TrainPlan.nKeHuoID = ObjectConvertClass.static_ext_int(dr["nKeHuoID"]);

            result.TrainmanPlan.Group.Trainman1.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID1"]);
            result.TrainmanPlan.Group.Trainman1.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName1"]);
            result.TrainmanPlan.Group.Trainman1.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber1"]);

            result.TrainmanPlan.Group.Trainman2.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID2"]);
            result.TrainmanPlan.Group.Trainman2.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName2"]);
            result.TrainmanPlan.Group.Trainman2.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber2"]);

            result.TrainmanPlan.Group.Trainman3.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID3"]);
            result.TrainmanPlan.Group.Trainman3.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName3"]);
            result.TrainmanPlan.Group.Trainman3.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber3"]);



            return result;
        }

        public WorkTimeEntity GetWorkTime(string PlanID)
        {
            WorkTimeEntity entity = null;
            string sql = "select * from VIEW_WorkTime_Turn where strFlowID = @FlowID";

            SqlParameter[] paramters = { 
                                           new SqlParameter("FlowID",PlanID)
                                       };
            
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql,paramters).Tables[0];

            if (dt.Rows.Count > 0)
            {
                entity = dtRowToWorkTime(dt.Rows[0]);
            }
            return entity;
        }
    }
}
