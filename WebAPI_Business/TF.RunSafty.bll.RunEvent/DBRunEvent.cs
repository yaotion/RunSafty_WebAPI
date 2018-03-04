using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using TF.CommonUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.RunEvent
{
    public class DBRunEvent
    {
        #region 1.13.1获取计划的所有事件信息(途中详情)

        public List<RunEvent> GetPlanRunEvents(string TrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Plan_RunEvent where strTrainPlanGUID = '" + TrainPlanGUID + "'  order by dtEventTime ");
            return GetPlanRunEventsDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<RunEvent> GetPlanStationRunEvents(string TrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Plan_RunEvent where strTrainPlanGUID = '" + TrainPlanGUID + "' and nEventID in (10007,10008,10011,10012)  order by dtEventTime ");
            return GetPlanRunEventsDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        
        }
        public List<RunEvent> GetPlanRunEventsDTToList(DataTable dt)
        {
            List<RunEvent> modelList = new List<RunEvent>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                RunEvent model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetPlanRunEventsDRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public RunEvent GetPlanRunEventsDRToModelDTToList(DataRow dr)
        {
            RunEvent model = new RunEvent();
            if (dr != null)
            {
                model.strRunEventGUID = ObjectConvertClass.static_ext_string(dr["strRunEventGUID"]);
                model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
                model.nEventID = ObjectConvertClass.static_ext_int(dr["nEventID"]);
                model.strEventName = GetRunEventTypeName.GetRunEventTypeNamebyID(model.nEventID);
                model.dtEventTime = ObjectConvertClass.static_ext_date(dr["dtEventTime"]);
                model.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
                model.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
                model.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
                model.nTMIS = ObjectConvertClass.static_ext_string(dr["nTMIS"]);
                model.strGroupGUID = ObjectConvertClass.static_ext_string(dr["strGroupGUID"]);
                model.strTrainmanNumber1 = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber1"]);
                model.strTrainmanNumber2 = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber2"]);
                model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.nResultID = ObjectConvertClass.static_ext_int(dr["nResult"]);
                model.strResult = ObjectConvertClass.static_ext_string(dr["strResult"]);
                model.strFlowID = ObjectConvertClass.static_ext_string(dr["strFlowID"]);
                model.strStationName = ObjectConvertClass.static_ext_string(dr["strStationName"]);
                model.strWorkShopGUID1 = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID1"]);
                model.strWorkShopGUID2 = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID2"]);
                model.nKehuoID = ObjectConvertClass.static_ext_int(dr["nKehuoID"]);
                model.nKehuo = ObjectConvertClass.static_ext_int(dr["nKeHuo"]);
            }
            return model;
        }
        #endregion

        #region 1.13.2 DeleteRunEvent（删除事件接口）
        public int DeleteRunEvent(string strRunEventGUID)
        {
            string strSql = "delete from TAB_Plan_RunEvent where strRunEventGUID = '" + strRunEventGUID + "'";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql);
        }
        #endregion

        #region 1.13.3 ReCountRunEvent（重新计算事件信息）
        public int ReCountRunEvent(string TrainPlanGUID)
        {
            string strSql = "select top 1 nid from TAB_Plan_RunEvent where strTrainPlanGUID = '" + TrainPlanGUID + "'";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string strUpSql = "update TAB_Plan_RunEvent set strFlowID ='' where nID = " + Convert.ToInt32(dt.Rows[0]["nID"]);
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strUpSql.ToString());
            }
            else
            {
                return 0;
            }

        }
        #endregion


        #region 1.13.4 AddRunEvent（添加运行事件）
        public int AddRunEvent(RunEvent model,TrainmanPlan plan, ref string  ErrInfo, ref int ErrCode)
        {
            string strSql = "select dbo.CovnertTrainType('" + model.strTrainTypeName + "') as TrainTypeName,dbo.CovnertTrainNumber('" + model.strTrainNumber + "') as TrainNumber";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                model.strTrainTypeName = dt.Rows[0]["TrainTypeName"].ToString();
                model.strTrainNumber = dt.Rows[0]["TrainNumber"].ToString();

                string strUpSql = "insert into TAB_Plan_RunEvent (strRunEventGUID,strTrainPlanGUID, " +
           " nEventID,dtEventTime,strTrainNo,strTrainTypeName,strTrainNumber,nTMIS, " +
           " nKeHuo,strGroupGUID,strTrainmanNumber1,strTrainmanNumber2,dtCreateTime,nResult,strResult) " +
           " values (@strRunEventGUID,@strTrainPlanGUID,@nEventID,@dtEventTime,@strTrainNo, " +
           " @strTrainTypeName,@strTrainNumber,@nTMIS,@nKeHuo,@strGroupGUID,@strTrainmanNumber1," +
           " @strTrainmanNumber2,getdate(),@nResult,@strResult)";

                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                            new SqlParameter("strRunEventGUID",model.strRunEventGUID),
                            new SqlParameter("strTrainPlanGUID",model.strTrainPlanGUID),
                            new SqlParameter("nEventID",model.nEventID),
                            new SqlParameter("dtEventTime",model.dtEventTime),
                            new SqlParameter("strTrainNo",model.strTrainNo),
                            new SqlParameter("strTrainTypeName",model.strTrainTypeName),
                            new SqlParameter("strTrainNumber",model.strTrainNumber),
                            new SqlParameter("nTMIS",model.nTMIS),
                            new SqlParameter("nKeHuo",model.nKehuo),
                            new SqlParameter("strGroupGUID",model.strGroupGUID),
                            new SqlParameter("strTrainmanNumber1",model.strTrainmanNumber1),
                            new SqlParameter("strTrainmanNumber2",model.strTrainmanNumber2),
                            new SqlParameter("nResult",model.nResultID),
                            new SqlParameter("strResult",model.strResult)

                    };

                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strUpSql.ToString(), sqlParameters);
            }
            else
            {
                return 0;
            }

        }
        #endregion


        #region 1.13.5 ComputeWorkTime（计算劳时）
        public int ComputeWorkTime(string TrainPlanGUID)
        {
            string strSql = "Proc_WorkTime_Calc";

            SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                            new SqlParameter("TrainPlanGUID",TrainPlanGUID)
                    };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.StoredProcedure, strSql, sqlParameters);
        }
        #endregion



    }

    public class GetRunEventTypeName
    {
        public static string GetRunEventTypeNamebyID(int nEventID)
        {
            if (nEventID == 0)
                return "空";
            else if (nEventID == 10001)
                return "入寓";
            else if (nEventID == 10002)
                return "离寓";
            else if (nEventID == 10003)
                return "出勤";
            else if (nEventID == 10004)
                return "出库";
            else if (nEventID == 10005)
                return "退勤";
            else if (nEventID == 10006)
                return "入库";
            else if (nEventID == 10007)
                return "停车";
            else if (nEventID == 10008)
                return "开车";
            else if (nEventID == 20001)
                return "验卡";
            else if (nEventID == 30001)
                return "测酒";
            else if (nEventID == 10009)
                return "文件开始";
            else if (nEventID == 10010)
                return "文件结束";
            else if (nEventID == 10011)
                return "进站";
            else if (nEventID == 10012)
                return "出站";
            else if (nEventID == 10013)
                return "站内最后一次停车";
            else
                return "空";
        }
    
    }


}
