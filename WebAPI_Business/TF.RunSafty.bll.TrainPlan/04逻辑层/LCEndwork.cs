using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using TF.RunSafty.Plan.MD;
using TF.CommonUtility;
using ThinkFreely.DBUtility;
using TF.RunSafty.Utils.Parse;


namespace TF.RunSafty.Plan
{
    public class LCEndwork
    {
        #region '获取计划的最后到达时间'
        public class InGetLastArrvieTime
        {
            //行车计划GUID
            public string TrainPlanGUID;
        }

        public class OutGetLastArrvieTime
        {
            //最后到达时间
            public DateTime ArriveTime;
        }

        /// <summary>
        /// 获取计划最后到达时间
        /// </summary>
        public InterfaceOutPut GetLastArrvieTime(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetLastArrvieTime InParams = javaScriptSerializer.Deserialize<InGetLastArrvieTime>(Data);
                OutGetLastArrvieTime OutParams = new OutGetLastArrvieTime();
                DateTime ArriveTime = DateTime.Now;
                bool isLastArrvieTime = getGetLastArrvieTime(InParams.TrainPlanGUID,ref ArriveTime);
                if (!isLastArrvieTime)
                {
                    output.resultStr = "不存在此计划信息";
                    return output;
                }
                else
                {
                    OutParams.ArriveTime = ArriveTime;
                }

                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetLastArrvieTime:" + ex.Message);
                throw ex;
            }
            return output;
        }

        public bool getGetLastArrvieTime(string TrainPlanGUID,ref DateTime ArriveTime)
        {

            string strSql = "Select dtLastArriveTime from TAB_Plan_Train where  strTrainPlanGUID = @strTrainPlanGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",TrainPlanGUID)
                };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            ArriveTime = TFParse.DBToDateTime(dt.Rows[0]["dtLastArriveTime"], Convert.ToDateTime("1899-01-01"));

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;

        }

        #endregion

        #region '获取指定编号的退勤计划'
        public class InGetTuiQinPlan
        {
            //退勤计划GUID                          
            public string TuiQinPlanGUID;
        }

        public class OutGetTuiQinPlan
        {
            //退勤计划
            public TuiQinPlan Plan = new TuiQinPlan();
        }

        /// <summary>
        /// 获取退勤计划
        /// </summary>
        public InterfaceOutPut GetTuiQinPlan(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTuiQinPlan InParams = javaScriptSerializer.Deserialize<InGetTuiQinPlan>(Data);
                OutGetTuiQinPlan OutParams = new OutGetTuiQinPlan();

                string strSql = "select top 1  * from VIEW_Plan_EndWork_Full where strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.TuiQinPlanGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    output.resultStr = "没有找到指定编号的退勤计划";
                    return output;
                }
                PS.PSPlan.EndworkFromDB(OutParams.Plan, dt.Rows[0]);
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTuiQinPlan:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion


        #region 获取乘务员指定退勤时间相关的计划信息
        public class InGetEndworkPlanByTime
        {
            //人员工号                                       
            public string TrainmanNumber;
            //指定退勤时间
            public DateTime TuiQinTime;
            //客户端GUID
            public string SiteGUID;
        }

        public class OutGetEndworkPlanByTime
        {
            //退勤计划信息
            public TuiQinPlan Plan = new TuiQinPlan();
            //是否存在
            public int Exist;
        }

        /// <summary>
        /// 获取指定退勤时间相关的人员的退勤计划
        /// </summary>
        public InterfaceOutPut GetEndworkPlanByTime(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetEndworkPlanByTime InParams = javaScriptSerializer.Deserialize<InGetEndworkPlanByTime>(Data);
                OutGetEndworkPlanByTime OutParams = new OutGetEndworkPlanByTime();
                output.data = OutParams;
                string strSql = @"select top 1 * from VIEW_Plan_EndWork 
                         where strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = @strSiteGUID) and
                        nPlanState = 7 AND (strTrainmanNumber1 = @strTrainmanNumber or strTrainmanNumber2 = @strTrainmanNumber or strTrainmanNumber3 =@strTrainmanNumber 
                    or strTrainmanNumber4 = @strTrainmanNumber) order by dtStartTime desc";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strSiteGUID",InParams.SiteGUID),
                    new SqlParameter("strTrainmanNumber",InParams.TrainmanNumber)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DateTime dtStartTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[0]["dtStartTime"], DateTime.Parse("1899-01-01"));
                    //出勤后三天之内的时间有效
                    if ((InParams.TuiQinTime > dtStartTime) && (InParams.TuiQinTime < dtStartTime.AddDays(3)))
                    {
                        OutParams.Exist = 1;
                        PS.PSPlan.EndworkFromDB(OutParams.Plan, dt.Rows[0]);
                    }

                }

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetEndworkPlanByTime:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

        #region 执行退勤

        private class InputParam
        {
            public DrinkData drinkdata { get; set; }
            public DutyInfo dutyUser { get; set; }
            public string planID = "";
            public string siteID = "";
            public int VerifyID = 0;
            public string remark = "";

        }
        public class JsonModel
        {
            public int result;
            public string resultStr;
            public object data;
        }
        public JsonModel ExecuteEndWork(string input)
        {
            JsonModel jsonModel = new JsonModel();
            jsonModel.result = 1;
            jsonModel.resultStr = "数据提交失败：未找到指定的机组信息";
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                InputParam paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InputParam>(input);

                DB_Plan_EndWork rcEndWork = new DB_Plan_EndWork();
                DateTime ArrvieTime = DateTime.Now;
                bool isArrvieTime = getGetLastArrvieTime(paramModel.planID, ref ArrvieTime);

                if (isArrvieTime)
                    ArrvieTime = Convert.ToDateTime("1899-01-01");

                if (rcEndWork.Endwork(paramModel.drinkdata, paramModel.dutyUser, paramModel.planID, paramModel.siteID, paramModel.VerifyID, paramModel.remark, ArrvieTime))
                {
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
                }
                else
                {

                    jsonModel.result = 1;
                }

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            return jsonModel;
        }
        #endregion

        #region 将已有计划和退勤关联（退勤）

        public class InUnionTuiQin
        {
            //人员GUID                                             
            public string TmGUID;

            //人员工号
            public string TmNumber;

            //计划GUID
            public string PlanGUID;
            //测酒信息
            public string DrinkGUID;

            //值班员GUID
            public string DutyGUID;

            //客户端GUID
            public string SiteGUID;
        }

        /// <summary>
        /// 关联已有测酒记录（退勤）
        /// </summary>
        public InterfaceOutPut UnionTuiQin(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InUnionTuiQin InParams = javaScriptSerializer.Deserialize<InUnionTuiQin>(Data);
                DB_Plan_EndWork dbPlanEndWork = new DB_Plan_EndWork();
                dbPlanEndWork.UnionTuiQin(InParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.Union:" + ex.Message);
                throw ex;
            }
            return output;
        }


        #endregion




        #region 获取指定人员在指定客户端下的退勤计划
        public class EndWork_In
        {
            public string siteID { get; set; }
            public string trainmanID { get; set; }
        }
        public class EndWork_Out
        {
            public int result;
            public string resultStr;
            public TuiQinPlan data;
            public object InterfaceRet;
        }
        public EndWork_Out GetPlan(string data)
        {

            EndWork_Out json = new EndWork_Out();
            EndWork_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<EndWork_In>(data);
            TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full();
            try
            {
                string clientID = input.siteID;
                string strTrainmanGUID = input.trainmanID;
                List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> plans = bllPlan.GetEndWorkOfTrainmanInSite(clientID, strTrainmanGUID);
                json.InterfaceRet = (plans != null && plans.Count > 0);
                if ((Boolean)json.InterfaceRet)
                {
                    json.data = GetPlanList(plans)[0];
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = 1;
                json.resultStr = ex.Message;
            }
            return json;
        }
        private List<TuiQinPlan> GetPlanList(List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> vPlans)
        {
            List<TuiQinPlan> lPlans = new List<TuiQinPlan>();
            TuiQinPlan clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_EndWork_Full plan in vPlans)
                {
                    clientPlan = new TuiQinPlan();
                    TuiQinGroup tuiqinGroup = new TuiQinGroup();
                    clientPlan.tuiqinGroup = tuiqinGroup;
                    tuiqinGroup.group = new NameGroup();
                    DutyPlace cPlace = new DutyPlace();
                    tuiqinGroup.group.place = cPlace;
                    cPlace.placeID = plan.strPlaceID;
                    cPlace.placeName = plan.strPlaceName;
                    tuiqinGroup.group.groupID = plan.strGroupGUID;
                    clientPlan.trainPlan = new TrainPlan();
                    clientPlan.trainPlan.createSiteGUID = plan.strCreateSiteGUID;
                    clientPlan.trainPlan.createSiteName = plan.strCreateSiteName;
                    if (plan.dtCreateTime.HasValue)
                    {
                        clientPlan.trainPlan.createTime = plan.dtCreateTime.Value;
                    }
                    clientPlan.trainPlan.createUserGUID = plan.strCreateUserGUID;
                    clientPlan.trainPlan.createUserName = plan.strCreateUserName;
                    clientPlan.trainPlan.dragTypeID = plan.nDragType.ToString();
                    clientPlan.trainPlan.dragTypeName = plan.nDragTypeName;
                    clientPlan.trainPlan.endStationID = plan.strEndStation;
                    clientPlan.trainPlan.endStationName = plan.strEndStationName;
                    clientPlan.trainPlan.kehuoID = plan.nKehuoID.ToString();
                    clientPlan.trainPlan.kehuoName = plan.strKehuoName;
                    clientPlan.trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    clientPlan.trainPlan.placeID = plan.strPlaceID;
                    clientPlan.trainPlan.placeName = plan.strPlaceName;
                    if (plan.nPlanState.HasValue)
                    {
                        clientPlan.trainPlan.planStateID = plan.nPlanState.Value;
                    }
                    clientPlan.trainPlan.planStateName = plan.strPlanStateName;
                    clientPlan.trainPlan.planTypeID = plan.nPlanType.ToString();
                    clientPlan.trainPlan.planTypeName = plan.strPlanTypeName;
                    clientPlan.trainPlan.remarkTypeID = plan.nRemarkType.ToString();
                    clientPlan.trainPlan.remarkTypeName = plan.strRemarkTypeName;
                    clientPlan.trainPlan.startStationID = plan.strStartStation;
                    clientPlan.trainPlan.startStationName = plan.strStartStationName;
                    clientPlan.trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    clientPlan.trainPlan.planID = plan.strTrainPlanGUID;
                    clientPlan.trainPlan.strTrainPlanGUID = plan.strTrainPlanGUID;
                    clientPlan.trainPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    clientPlan.trainPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    if (plan.dtLastArriveTime.HasValue)
                    {
                        clientPlan.trainPlan.lastArriveTime = plan.dtLastArriveTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        clientPlan.trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtRealStartTime.HasValue)
                    {
                        clientPlan.trainPlan.realStartTime = plan.dtRealStartTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        clientPlan.trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        clientPlan.trainPlan.firstStartTime = plan.dtFirstStartTime.Value;
                    }
                    if (plan.dtChuQinTime.HasValue)
                    {
                        clientPlan.beginWorkTime = plan.dtChuQinTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        clientPlan.trainPlan.kaiCheTime = plan.dtChuQinTime.Value;
                    }
                    else
                    {
                        clientPlan.beginWorkTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    clientPlan.trainPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    clientPlan.trainPlan.trainmanTypeName = plan.strTrainmanTypeName;
                    clientPlan.trainPlan.trainNo = plan.strTrainNo;
                    clientPlan.trainPlan.trainNumber = plan.strTrainNumber;
                    clientPlan.trainPlan.trainTypeName = plan.strTrainTypeName;
                    tuiqinGroup.group.station = new Station();
                    tuiqinGroup.group.station.stationID = plan.strStationGUID;
                    tuiqinGroup.group.station.stationName = plan.strStationName;
                    tuiqinGroup.group.station.stationNumber = plan.strStationNumber.ToString();
                    tuiqinGroup.group.trainman1 = new TF.RunSafty.Plan.Trainman();
                    tuiqinGroup.group.trainman1.ABCD = plan.strABCD1;
                    if (plan.nDriverType1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.driverTypeID = plan.nDriverType1.Value;
                    }
                    if (plan.isKey1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.isKey = plan.isKey1.Value;
                    }
                    if (plan.nPostID1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.postID = plan.nPostID1.Value;
                    }
                    tuiqinGroup.group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    tuiqinGroup.group.trainman1.trainmanName = plan.strTrainmanName1;
                    tuiqinGroup.group.trainman1.trainmanNumber = plan.strTrainmanNumber1;
                    tuiqinGroup.group.trainman2 = new TF.RunSafty.Plan.Trainman();
                    tuiqinGroup.group.trainman2.ABCD = plan.strABCD2;
                    if (plan.nDriverType2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.driverTypeID = plan.nDriverType2.Value;
                    }
                    if (plan.isKey2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.isKey = plan.isKey2.Value;
                    }
                    if (plan.nPostID2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.postID = plan.nPostID2.Value;
                    }
                    tuiqinGroup.group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    tuiqinGroup.group.trainman2.trainmanName = plan.strTrainmanName2;
                    tuiqinGroup.group.trainman2.trainmanNumber = plan.strTrainmanNumber2;
                    tuiqinGroup.group.trainman3 = new TF.RunSafty.Plan.Trainman();
                    tuiqinGroup.group.trainman3.ABCD = plan.strABCD3;
                    if (plan.nDriverType3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.driverTypeID = plan.nDriverType3.Value;
                    }
                    if (plan.isKey3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.isKey = plan.isKey3.Value;
                    }
                    if (plan.nPostID3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.postID = plan.nPostID3.Value;
                    }
                    tuiqinGroup.group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    tuiqinGroup.group.trainman3.trainmanName = plan.strTrainmanName3;
                    tuiqinGroup.group.trainman3.trainmanNumber = plan.strTrainmanNumber3;
                    tuiqinGroup.group.trainman4 = new TF.RunSafty.Plan.Trainman(); ;
                    tuiqinGroup.group.trainman4.ABCD = plan.strABCD4;
                    if (plan.nDriverType4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.driverTypeID = plan.nDriverType4.Value;
                    }
                    if (plan.isKey4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.isKey = plan.isKey4.Value;
                    }
                    if (plan.nPostID4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.postID = plan.nPostID4.Value;
                    }
                    tuiqinGroup.group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    tuiqinGroup.group.trainman4.trainmanName = plan.strTrainmanName4;
                    tuiqinGroup.group.trainman4.trainmanNumber = plan.strTrainmanNumber4;

                    tuiqinGroup.turnMinutes = plan.nTurnMinutes.ToString();
                    tuiqinGroup.turnAlarmMinutes = plan.nTurnAlarmMinutes.ToString();
                    tuiqinGroup.isOver = plan.bIsOver.ToString();
                    tuiqinGroup.signed = plan.bSigned.ToString();
                    if (plan.dtTurnStartTime.HasValue)
                    {
                        tuiqinGroup.turnStartTime = plan.dtTurnStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    //clientPlan.icCheckResult = plan.strICCheckResult;


                    if (plan.nVerifyID1.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID1 = plan.nVerifyID1.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo1 = new TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo1.picture = "";
                    if (plan.dtTestTime1.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo1.testTime = plan.dtTestTime1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult1.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo1.testAlcoholResult = plan.nDrinkResult1.Value;
                    }

                    if (plan.nVerifyID2.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID2 = plan.nVerifyID2.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo2 = new TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo2.picture = "";
                    if (plan.dtTestTime2.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo2.testTime = plan.dtTestTime2.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult2.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo2.testAlcoholResult = plan.nDrinkResult2.Value;
                    }


                    if (plan.nVerifyID3.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID3 = plan.nVerifyID3.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo3 = new TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo3.picture = "";
                    if (plan.dtTestTime3.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo3.testTime = plan.dtTestTime3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult3.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo3.testAlcoholResult = plan.nDrinkResult3.Value;
                    }

                    if (plan.nVerifyID4.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID4 = plan.nVerifyID4.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo4 = new TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo4.picture = "";
                    if (plan.dtTestTime4.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo4.testTime = plan.dtTestTime4.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult4.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo4.testAlcoholResult = plan.nDrinkResult4.Value;
                    }

                    lPlans.Add(clientPlan);
                }
            }
            return lPlans;
        }
        #endregion
    }
}
