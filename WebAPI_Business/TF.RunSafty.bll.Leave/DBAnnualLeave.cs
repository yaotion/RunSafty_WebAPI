using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.Leave.MD;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.SqlClient;
using TFApiCommon;
using TFApiCommon.DBParam;

namespace TF.RunSafty.Leave
{
    class DBAnnualLeave
    {

        public void ExecuteLeave(LeaveApplyEntity leaveApply)
        {

            int nYear = leaveApply.dtBeginTime.Year;
            int nMonth = leaveApply.dtBeginTime.Month;

            string sql = "select * From TAB_LeaveMgr_AnnualLeave where strTrainmanNumber = @strTrainmanNumber and nYear = @nYear and nMonth = @nMonth";
            DbParamDict paramDict = new DbParamDict();
            paramDict.Add("nYear", nYear, ParamDataType.dtInt);
            paramDict.Add("nMonth", nMonth, ParamDataType.dtInt);
            paramDict.Add("strTrainmanNumber", leaveApply.strTrainmanNumber,ParamDataType.dtString);


            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, paramDict.GetParams()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                paramDict.Clear();
                
                paramDict.Add("strLeaveGUID", leaveApply.strAskLeaveGUID, ParamDataType.dtString);
                paramDict.Add("nLeaveState", 1, ParamDataType.dtInt);
                paramDict.Add("dtLeaveTime", leaveApply.dtBeginTime, ParamDataType.dtDateTime);
                paramDict.Add("strAnnualGUID", dt.Rows[0]["strAnnualGUID"].ToString(), ParamDataType.dtString);

                sql = "update TAB_LeaveMgr_AnnualLeave set strLeaveGUID = @strLeaveGUID,nLeaveState = @nLeaveState,dtLeaveTime=@dtLeaveTime where strAnnualGUID = @strAnnualGUID";

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, paramDict.GetParams());
            }
        }

        public void ExecuteUnLeave(SqlTransaction trans, CancelLeaveDetailEntity entity)
        {

            string sql = "select * From TAB_LeaveMgr_AnnualLeave where strLeaveGUID = @strLeaveGUID";
            DbParamDict paramDict = new DbParamDict();

            paramDict.Add("strLeaveGUID", entity.strAskLeaveGUID, ParamDataType.dtString);


            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, paramDict.GetParams()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                paramDict.Clear();

                paramDict.Add("strLeaveGUID", entity.strAskLeaveGUID, ParamDataType.dtString);
                paramDict.Add("nLeaveState", 2, ParamDataType.dtInt);
                paramDict.Add("dtUnleaveTime", entity.dtCancelTime, ParamDataType.dtDateTime);


                sql = "update TAB_LeaveMgr_AnnualLeave set dtUnleaveTime= @dtUnleaveTime,nLeaveState = @nLeaveState where strLeaveGUID = @strLeaveGUID";

                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, paramDict.GetParams());
            }
        }

        public void CancelLeave(string AskLeaveGUID)
        {            
            DbParamDict paramDict = new DbParamDict();

            paramDict.Add("strLeaveGUID", AskLeaveGUID, ParamDataType.dtString);

            string sql = "update TAB_LeaveMgr_AnnualLeave set dtUnleaveTime= null,nLeaveState = 0,dtLeaveTime=null where strLeaveGUID = @strLeaveGUID";

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, paramDict.GetParams());
            
        }

        public void Add(AnnualLeaveEntity entity)
        {
            DbParamDict paramDict = new DbParamDict();

            paramDict.Add("strAnnualGUID", entity.ID, ParamDataType.dtString);            
            paramDict.Add("strTrainmanNumber", entity.TrainmanNumber, ParamDataType.dtString);
            paramDict.Add("strTrainmanName", entity.TrainmanName, ParamDataType.dtString);
            paramDict.Add("strWorkShopGUID", entity.WorkShopGUID, ParamDataType.dtString);
            paramDict.Add("nYear", entity.Year, ParamDataType.dtInt);
            paramDict.Add("nMonth", entity.Month, ParamDataType.dtInt);
            paramDict.Add("nNeedDays", entity.NeedDays, ParamDataType.dtInt);
            string sql = "select count(*) from TAB_LeaveMgr_AnnualLeave where nYear=@nYear and nMonth=@nMonth and strTrainmanNumber = @strTrainmanNumber";

            object ret = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, paramDict.GetParams());
            //判断是否已经存在对应月份的记录
            if (Convert.ToInt32(ret) > 0)
            {
                return;
            }
            

            sql = "insert into TAB_LeaveMgr_AnnualLeave " + paramDict.GetInsertSqlString();

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, paramDict.GetParams());
        }

        public void Del(string ID,string log)
        {
            string sql = "update TAB_LeaveMgr_AnnualLeave set nState = 1,strDelReason = @strDelReason  where strAnnualGUID = @strAnnualGUID";

            DbParamDict paramDict = new DbParamDict();

            paramDict.Add("strAnnualGUID", ID, ParamDataType.dtString);
            paramDict.Add("strDelReason", log, ParamDataType.dtString);

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, paramDict.GetParams());
        }

        public List<AnnualLeaveEntity> Get(AnnualQC Condition)
        {
            List<AnnualLeaveEntity> ret = new List<AnnualLeaveEntity>();

            string sql = @"select *, 
                        (case nLeaveState  
                        when 2 then DATEDIFF( day , dtLeaveTime,dtUnleaveTime)  
                        else DATEDIFF( day , dtLeaveTime,getdate())  
                        end) as nLeaveDays 
                        from TAB_LeaveMgr_AnnualLeave where ";



            DbCDBuilder<AnnualQC> ConditionBuilder = new DbCDBuilder<AnnualQC>();
                    
            DbParamDict paramDict = new DbParamDict();


            paramDict.Add("strAnnualGUID", Condition.ID, ParamDataType.dtString);
            paramDict.Add("nMonth", Condition.Month, ParamDataType.dtInt);
            paramDict.Add("nYear", Condition.Year, ParamDataType.dtInt);
            paramDict.Add("strWorkShopGUID", Condition.WorkShopGUID, ParamDataType.dtString);            
            paramDict.Add("strTrainmanNumber", Condition.TrainmanNumber, ParamDataType.dtString);
            paramDict.Add("nState", Condition.State, ParamDataType.dtInt);

            string strCondition = " 1=1 ";
            if (!string.IsNullOrEmpty(Condition.ID))
            {
                strCondition += " and strAnnualGUID = @strAnnualGUID ";
            }

            if (Condition.Month > 0)
            {
                strCondition += " and nMonth = @nMonth ";
            }

            if (Condition.Year > 0)
            {
                strCondition += " and nYear = @nYear ";
            }

            if (!string.IsNullOrEmpty(Condition.WorkShopGUID))
            {
                strCondition += " and strWorkShopGUID = @strWorkShopGUID ";
            }

            if (!string.IsNullOrEmpty(Condition.TrainmanNumber))
            {
                strCondition += " and strTrainmanNumber = @strTrainmanNumber ";
            }

            strCondition += " and nState = @nState ";



            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql + strCondition, paramDict.GetParams()).Tables[0];
            AnnualLeaveEntity LeaveEntity;
            foreach (DataRow dr in dt.Rows)
            {
                LeaveEntity = new AnnualLeaveEntity();
                LeaveEntity.ID = DBConvert.ToString(dr["strAnnualGUID"]);                
                LeaveEntity.TrainmanNumber = DBConvert.ToString(dr["strTrainmanNumber"]);
                LeaveEntity.TrainmanName = DBConvert.ToString(dr["strTrainmanName"]);
                LeaveEntity.WorkShopGUID = DBConvert.ToString(dr["strWorkShopGUID"]);
                LeaveEntity.Year = DBConvert.ToInt(dr["nYear"]);
                LeaveEntity.Month = DBConvert.ToInt(dr["nMonth"]);
                LeaveEntity.LeaveState = DBConvert.ToInt(dr["nLeaveState"]);
                LeaveEntity.LeaveDays = DBConvert.ToInt(dr["nLeaveDays"]);
                if (dr["nNeedDays"] != DBNull.Value)
                {
                    LeaveEntity.NeedDays = DBConvert.ToInt(dr["nNeedDays"]);
                }
                
                //有些非正常记录，请假开始时间大于当前时间或开始时间大于结束时间
                if (LeaveEntity.LeaveDays < 0)
                {
                    LeaveEntity.LeaveDays = 0;
                }
                LeaveEntity.CreateTime = DBConvert.ToDateTime_N(dr["dtCreateTime"]);                

                if (dr["strDelReason"] != DBNull.Value)
                {
                    LeaveEntity.DelReason = dr["strDelReason"].ToString();
                }
                

                ret.Add(LeaveEntity);
            }


            return ret;
        }

        public void BatchAdd(List<AnnualLeaveEntity> entityList)
        {
            foreach (AnnualLeaveEntity entity in entityList)
            {
                Add(entity);
            }

        }
    }
}
