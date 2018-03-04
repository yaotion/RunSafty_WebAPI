using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.CommonUtility;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.BeginworkFlow
{
    public class LCBeginworkFlow
    {
        #region 强出勤流程状态置为完成(当需手工确认时)

        public class Get_OutFlows
        {
            public string result = "";
            public string resultStr = "";
            public object data;
        }
        public Get_OutFlows SetFlowIsOkByPerson(string data)
        {
            Get_OutFlows json = new Get_OutFlows();
            try
            {
                Plan_Beginwork_Flow input = Newtonsoft.Json.JsonConvert.DeserializeObject<Plan_Beginwork_Flow>(data);
                DBPlan_Beginwork_Flow db = new DBPlan_Beginwork_Flow();
                int i = db.Add(input);
                if (i >= 1)
                {
                    json.result = "0";
                    json.resultStr = "返回成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "更新0条数据";
                }

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 先通过条件获取计划GUID 再通过GUID 获取流程详情
        public class Get_InGetBeginworkStepData
        {
            public string strTrainmanNumber;
            public string dtBeginStartTime;
            public string dtEndStartTime;

        }
        public class Get_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<Plan_Beginwork_StepData> data;
        }
        public Get_Out GetBeginworkStepData(string data)
        {
            Get_Out json = new Get_Out();
            try
            {
                Get_InGetBeginworkStepData input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetBeginworkStepData>(data);
                DBPlan_Beginwork_StepData db = new DBPlan_Beginwork_StepData();
                json.data = db.GetDataList(input.strTrainmanNumber, input.dtBeginStartTime, input.dtEndStartTime);
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取出勤流程步骤信息接口（从流程步骤结果表中获取指定流程的步骤执行结果信息）
        public class Get_InGetBeginworkStepResult
        {
            public string strTrainPlanGUID;
            public string strWorkShopGUID;
            public int nWorkTypeID;
        }
        public class Get_OutGetBeginworkStepResult
        {
            public string result = "";
            public string resultStr = "";
            public List<Plan_Beginwork_StepResult> data;
        }

        public Get_OutGetBeginworkStepResult GetBeginworkStepResult(string data)
        {
            Get_OutGetBeginworkStepResult json = new Get_OutGetBeginworkStepResult();
            try
            {
               
                Plan_Beginwork_StepResultList list = new Plan_Beginwork_StepResultList();

                Get_InGetBeginworkStepResult input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetBeginworkStepResult>(data);
                DBPlan_Beginwork_StepResult db = new DBPlan_Beginwork_StepResult();
                DataTable dtplan = db.getPlanDetail(input.strTrainPlanGUID);
                List<Plan_Beginwork_StepDef> stepList = db.getStep(input.strWorkShopGUID, input.nWorkTypeID);
                List<Plan_Beginwork_StepResult> resultList = db.getStepResult(input.strTrainPlanGUID, input.nWorkTypeID);
 
                List<string> planTrain = new List<string>();
                List<string> planTrainManName = new List<string>();
                //获取计划人员
                if (dtplan != null && dtplan.Rows.Count > 0)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        if (!string.IsNullOrEmpty(dtplan.Rows[0]["strTrainmanNumber" + i].ToString()))
                        {
                            planTrain.Add(dtplan.Rows[0]["strTrainmanNumber" + i].ToString());
                            planTrainManName.Add(dtplan.Rows[0]["strTrainmanName" + i].ToString());
                        }
                    }
                }
                //构建新的步骤列表，一个人员对应一个步骤列表
                List<Plan_Beginwork_StepDef> newstepList = new List<Plan_Beginwork_StepDef>();
                Plan_Beginwork_StepDef newstep = null;
                for (int ii = 0; ii < planTrain.Count;ii++ )
                {
                    for (int j = 0; j < stepList.Count; j++)
                    {
                        newstep = new Plan_Beginwork_StepDef();
                        newstep.strStepID = stepList[j].strStepID;
                        newstep.strTrainmanNumber_Step = planTrain[ii];
                        newstep.strStepName = stepList[j].strStepName;
                        newstep.strTrainmanName = planTrainManName[ii];
                        newstep.nStepIndex = stepList[j].nStepIndex;
                        newstep.nIsNecessary = stepList[j].nIsNecessary;
                        newstepList.Add(newstep);
                    }
                }
               
                List<Plan_Beginwork_StepResult> rtlist = new List<Plan_Beginwork_StepResult>();
                Plan_Beginwork_StepResult rtm = null;
                for (int i = 0; i < newstepList.Count; i++)
                {
                    rtm = new Plan_Beginwork_StepResult();
                    rtm.strTrainmanName = newstepList[i].strTrainmanName;
                    rtm.nStepIndex = newstepList[i].nStepIndex;
                    rtm.nStepResult = 0;
                    var child = resultList.Where(x => (x.strStepName == newstepList[i].strStepID && x.strTrainmanNumber == newstepList[i].strTrainmanNumber_Step)).LastOrDefault();
                    if (child != null)
                    {
                        rtm = child;
                        rtm.nStepResult = 1;
                    }
                    rtm.nIsNecessary = newstepList[i].nIsNecessary;
                    rtm.strStepBrief = newstepList[i].strStepName;
                    rtm.strTrainmanNumber = newstepList[i].strTrainmanNumber_Step;
                    if (rtm.strStepName == "RS.STEP.DRINKTEST")
                    {
                        string url = getURL(rtm.strTrainmanNumber, input.strTrainPlanGUID, input.nWorkTypeID);
                        rtm.ExInfo = new { url = url };
                    }
                    rtlist.Add(rtm);
                }
                json.data = rtlist;
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }

        public string getURL(string strTrainmanNumber, string strTrainPlanGUID, int nWorkTypeID)
        {
            string strSqlData = "select * from  TAB_Plan_Beginwork_StepData where strTrainPlanGUID='" + strTrainPlanGUID + "' and strTrainmanNumber='" + strTrainmanNumber + "' and nWorkTypeID= " + nWorkTypeID;
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlData.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                { 
                    string filename=dt.Rows[i]["strFieldName"].ToString();
                    if (filename == "picture1" || filename == "picture2" || filename == "picture3" || filename == "picture4")
                    {
                        return dt.Rows[i]["strStepData"].ToString();
                    }
                }
            }
            return "";
        }
        #endregion

        #region 获取乘务员当前正在值乘的出勤计划接口((1)从计划表中获取符合条件的计划(2)从流程显示表中获取流程需要显示的信息)
        public class Get_InBeginworkFlow
        {
            public string siteID { get; set; }
            public string trainmanID { get; set; }

        }
        public class Get_OutGetBeginworkFlow
        {
            public string result = "";
            public string resultStr = "";
            public TrainmanPlan data;
        }
        public Get_OutGetBeginworkFlow GetBeginworkFlow(string data)
        {
            Get_OutGetBeginworkFlow json = new Get_OutGetBeginworkFlow();
            try
            {
                Get_InBeginworkFlow input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InBeginworkFlow>(data);
                DBPlan_Beginwork_Flow db = new DBPlan_Beginwork_Flow();
                List<TF.RunSafty.Model.VIEW_Plan_Trainman> vplans = db.GetData(input.siteID, input.trainmanID);
                Plan_Beginwork_Flow model = new Plan_Beginwork_Flow();
                List<TrainmanPlan> plans = GetPlanList(vplans);
                if (plans != null && plans.Count > 0)
                {
                    json.data = plans[0];
                    json.result = "0";
                    json.resultStr = "提交成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "没有该乘务员的出勤计划";
                }
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        private List<TrainmanPlan> GetPlanList(List<TF.RunSafty.Model.VIEW_Plan_Trainman> vPlans)
        {
            List<TrainmanPlan> lPlans = new List<TrainmanPlan>();
            TrainmanPlan clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_Trainman plan in vPlans)
                {
                    clientPlan = new TrainmanPlan();
                    ChuqinGroup chuqinGroup = new ChuqinGroup();
                    clientPlan.chuqinGroup = chuqinGroup;
                    DutyPlace cPlace = new DutyPlace();
                    chuqinGroup.group = new NameGroup();
                    chuqinGroup.group.place = cPlace;
                    cPlace.placeID = plan.strPlaceID;
                    cPlace.placeName = plan.strPlaceName;
                    chuqinGroup.group.groupID = plan.strGroupGUID;
                    chuqinGroup.group.station = new Station();
                    //chuqinGroup.group.station.stationID = plan.strStationGUID;
                    //chuqinGroup.group.station.stationName = plan.strStationName;
                    //if (plan.strStationNumber.HasValue)
                    //{
                    //    chuqinGroup.group.station.stationNumber = plan.strStationNumber.ToString();
                    //}
                    chuqinGroup.group.trainman1 = new TF.RunSafty.BeginworkFlow.Trainman();
                    chuqinGroup.group.trainman1.ABCD = plan.strABCD1;
                    if (plan.nDriverType1.HasValue)
                    {
                        chuqinGroup.group.trainman1.driverTypeID = plan.nDriverType1.Value;
                    }
                    if (plan.isKey1.HasValue)
                    {
                        chuqinGroup.group.trainman1.isKey = plan.isKey1.Value;
                    }
                    if (plan.nPostID1.HasValue)
                    {
                        chuqinGroup.group.trainman1.postID = plan.nPostID1.Value;
                    }
                    chuqinGroup.group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    chuqinGroup.group.trainman1.trainmanName = plan.strTrainmanName1;
                    chuqinGroup.group.trainman1.trainmanNumber = plan.strTrainmanNumber1;
                    chuqinGroup.group.trainman2 = new TF.RunSafty.BeginworkFlow.Trainman();
                    chuqinGroup.group.trainman2.ABCD = plan.strABCD2;
                    if (plan.nDriverType2.HasValue)
                    {
                        chuqinGroup.group.trainman2.driverTypeID = plan.nDriverType2.Value;
                    }
                    if (plan.isKey2.HasValue)
                    {
                        chuqinGroup.group.trainman2.isKey = plan.isKey2.Value;
                    }
                    if (plan.nPostID2.HasValue)
                    {
                        chuqinGroup.group.trainman2.postID = plan.nPostID2.Value;
                    }
                    chuqinGroup.group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    chuqinGroup.group.trainman2.trainmanName = plan.strTrainmanName2;
                    chuqinGroup.group.trainman2.trainmanNumber = plan.strTrainmanNumber2;
                    chuqinGroup.group.trainman3 = new TF.RunSafty.BeginworkFlow.Trainman();
                    chuqinGroup.group.trainman3.ABCD = plan.strABCD3;
                    if (plan.nDriverType3.HasValue)
                    {
                        chuqinGroup.group.trainman3.driverTypeID = plan.nDriverType3.Value;
                    }
                    if (plan.isKey3.HasValue)
                    {
                        chuqinGroup.group.trainman3.isKey = plan.isKey3.Value;
                    }
                    if (plan.nPostID3.HasValue)
                    {
                        chuqinGroup.group.trainman3.postID = plan.nPostID3.Value;
                    }
                    chuqinGroup.group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    chuqinGroup.group.trainman3.trainmanName = plan.strTrainmanName3;
                    chuqinGroup.group.trainman3.trainmanNumber = plan.strTrainmanNumber3;
                    chuqinGroup.group.trainman4 = new TF.RunSafty.BeginworkFlow.Trainman();
                    chuqinGroup.group.trainman4.ABCD = plan.strABCD4;
                    if (plan.nDriverType4.HasValue)
                    {
                        chuqinGroup.group.trainman4.driverTypeID = plan.nDriverType4.Value;
                    }
                    if (plan.isKey4.HasValue)
                    {
                        chuqinGroup.group.trainman4.isKey = plan.isKey4.Value;
                    }
                    if (plan.nPostID4.HasValue)
                    {
                        chuqinGroup.group.trainman4.postID = plan.nPostID4.Value;
                    }
                    chuqinGroup.group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    chuqinGroup.group.trainman4.trainmanName = plan.strTrainmanName4;
                    chuqinGroup.group.trainman4.trainmanNumber = plan.strTrainmanNumber4;
                    //clientPlan.icCheckResult = plan.strICCheckResult;
                    TF.RunSafty.BeginworkFlow.TrainPlan trainPlan = new TrainPlan();
                    clientPlan.trainPlan = trainPlan;
                    if (plan.dtCreateTime.HasValue)
                    {
                        trainPlan.createTime = plan.dtCreateTime.Value;
                    }
                    trainPlan.createSiteGUID = plan.strCreateSiteGUID;
                    trainPlan.createSiteName = plan.strCreateSiteName;
                    trainPlan.createUserGUID = plan.strCreateUserGUID;
                    trainPlan.createUserName = plan.strCreateUserName;
                    if (plan.nDragType.HasValue)
                    {
                        trainPlan.dragTypeID = plan.nDragType.ToString();
                    }
                    trainPlan.endStationID = plan.strEndStation;
                    trainPlan.endStationName = plan.strEndStationName;
                    if (plan.nKehuoID.HasValue)
                    {
                        trainPlan.kehuoID = plan.nKehuoID.ToString();
                    }
                    trainPlan.kehuoName = plan.strKehuoName;
                    trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    trainPlan.placeID = plan.strPlaceID;
                    trainPlan.placeName = plan.strPlaceName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    if (plan.nPlanState.HasValue)
                    {
                        trainPlan.planStateID = plan.nPlanState.Value;
                    }
                    trainPlan.planStateName = plan.strPlanStateName;
                    if (plan.nPlanType.HasValue)
                    {
                        trainPlan.planTypeID = plan.nPlanType.ToString();
                    }
                    trainPlan.planTypeName = plan.strPlanTypeName;
                    if (plan.nRemarkType.HasValue)
                    {
                        trainPlan.remarkTypeID = plan.nRemarkType.ToString();
                    }
                    trainPlan.remarkTypeName = plan.strRemarkTypeName;

                    trainPlan.startStationID = plan.strStartStation;
                    trainPlan.startStationName = plan.strStartStationName;
                    trainPlan.dragTypeName = plan.nDragTypeName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    trainPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    trainPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    trainPlan.strTrainPlanGUID = plan.strTrainPlanGUID;



                    if (plan.dtRealStartTime.HasValue)
                    {
                        trainPlan.realStartTime = plan.dtRealStartTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        trainPlan.firstStartTime = plan.dtFirstStartTime.Value;
                    }
                    if (plan.dtChuQinTime.HasValue)
                    {
                        clientPlan.beginWorkTime = plan.dtChuQinTime.Value;
                        clientPlan.trainPlan.kaiCheTime = plan.dtChuQinTime.Value;
                    }
                    else
                    {
                    }
                    if (plan.nTrainmanTypeID.HasValue)
                    {
                        trainPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    }

                    DBPlan_Beginwork_StepData db = new DBPlan_Beginwork_StepData();
                    DataTable dt = db.GetStepDatas(trainPlan.strTrainPlanGUID);
                    chuqinGroup.verifyID1 = 1;
                    chuqinGroup.testAlcoholInfo1 = new TestAlcoholInfo();

                    chuqinGroup.verifyID2 = 1;
                    chuqinGroup.testAlcoholInfo2 = new TestAlcoholInfo();

                    chuqinGroup.verifyID3 = 1;
                    chuqinGroup.testAlcoholInfo3 = new TestAlcoholInfo();

                    chuqinGroup.verifyID4 = 1;
                    chuqinGroup.testAlcoholInfo4 = new TestAlcoholInfo();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["strFieldName"].ToString() == "nDrinkResult1")
                            chuqinGroup.testAlcoholInfo1.testAlcoholResult = ObjectConvertClass.static_ext_int(dt.Rows[i]["nStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "nDrinkResult2")
                            chuqinGroup.testAlcoholInfo2.testAlcoholResult = ObjectConvertClass.static_ext_int(dt.Rows[i]["nStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "nDrinkResult3")
                            chuqinGroup.testAlcoholInfo3.testAlcoholResult = ObjectConvertClass.static_ext_int(dt.Rows[i]["nStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "nDrinkResult4")
                            chuqinGroup.testAlcoholInfo4.testAlcoholResult = ObjectConvertClass.static_ext_int(dt.Rows[i]["nStepData"].ToString());

                        if (dt.Rows[i]["strFieldName"].ToString() == "dtEventTime1")
                            chuqinGroup.testAlcoholInfo1.testTime = ObjectConvertClass.static_ext_string(dt.Rows[i]["dtStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "dtEventTime2")
                            chuqinGroup.testAlcoholInfo2.testTime = ObjectConvertClass.static_ext_string(dt.Rows[i]["dtStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "dtEventTime3")
                            chuqinGroup.testAlcoholInfo3.testTime = ObjectConvertClass.static_ext_string(dt.Rows[i]["dtStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "dtEventTime4")
                            chuqinGroup.testAlcoholInfo4.testTime = ObjectConvertClass.static_ext_string(dt.Rows[i]["dtStepData"].ToString());



                        if (dt.Rows[i]["strFieldName"].ToString() == "dtEventTime1")
                            chuqinGroup.testAlcoholInfo1.testTime = ObjectConvertClass.static_ext_string(dt.Rows[i]["dtStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "dtEventTime2")
                            chuqinGroup.testAlcoholInfo2.testTime = ObjectConvertClass.static_ext_string(dt.Rows[i]["dtStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "dtEventTime3")
                            chuqinGroup.testAlcoholInfo3.testTime = ObjectConvertClass.static_ext_string(dt.Rows[i]["dtStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "dtEventTime4")
                            chuqinGroup.testAlcoholInfo4.testTime = ObjectConvertClass.static_ext_string(dt.Rows[i]["dtStepData"].ToString());


                        if (dt.Rows[i]["strFieldName"].ToString() == "picture1")
                            chuqinGroup.testAlcoholInfo1.picture = ObjectConvertClass.static_ext_string(dt.Rows[i]["strStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "picture2")
                            chuqinGroup.testAlcoholInfo2.picture = ObjectConvertClass.static_ext_string(dt.Rows[i]["strStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "picture3")
                            chuqinGroup.testAlcoholInfo3.picture = ObjectConvertClass.static_ext_string(dt.Rows[i]["strStepData"].ToString());
                        if (dt.Rows[i]["strFieldName"].ToString() == "picture4")
                            chuqinGroup.testAlcoholInfo4.picture = ObjectConvertClass.static_ext_string(dt.Rows[i]["strStepData"].ToString());

                    }

                    trainPlan.trainNo = plan.strTrainNo;
                    trainPlan.trainNumber = plan.strTrainNumber;
                    trainPlan.trainTypeName = plan.strTrainTypeName;
                    trainPlan.trainmanTypeName = plan.strTrainmanTypeName;



                    lPlans.Add(clientPlan);
                }
            }
            return lPlans;
        }
        #endregion

        #region 获取出勤计划列表
        public class Get_InPlanWorkOutList
        {
            public string siteID { get; set; }
            public DateTime BeginTime { get; set; }
            public DateTime Endime { get; set; }
        }
        public class Get_OutPlanWorkOutList
        {
            public string result = "";
            public string resultStr = "";
            public List<TrainmanPlan> data;
        }
        public Get_OutPlanWorkOutList GetPlanWorkOutList(string data)
        {
            Get_OutPlanWorkOutList json = new Get_OutPlanWorkOutList();
            try
            {
                Get_InPlanWorkOutList input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InPlanWorkOutList>(data);
                DBWorkOutPlanList db = new DBWorkOutPlanList();
                List<TF.RunSafty.BeginworkFlow.WorkOutPlanList> vplans = db.GetPlanWorkOutList(input.siteID, input.BeginTime, input.Endime);
                List<TrainmanPlan> plans = db.GetBeginFlowPlanList(vplans);
                if (vplans != null && vplans.Count > 0)
                {
                    json.data = plans;
                    json.result = "0";
                    json.resultStr = "提交成功";
                }

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }


        #endregion



        #region 设置出勤步骤规则（是否按照顺序执行）
        public class Get_InSetBeginworkRule
        {
            public string strWorkShopGUID;
            public string strWorkShopName;
            public int nExecByStepIndex;

        }
        public class Get_OutBeginworkRule
        {
            public string result = "";
            public string resultStr = "";
            public object data;
        }
        public Get_OutBeginworkRule SetBeginworkRule(string data)
        {
            Get_OutBeginworkRule json = new Get_OutBeginworkRule();
            try
            {
                Get_InSetBeginworkRule input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InSetBeginworkRule>(data);
                DBPlan_Beginwork_Rule db = new DBPlan_Beginwork_Rule();
                bool i = db.SetBeginworkRule(input.strWorkShopGUID, input.strWorkShopName, input.nExecByStepIndex);
                if (i)
                {
                    json.result = "0";
                    json.resultStr = "返回成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "更新0条数据";
                }

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 设置出勤步骤规则（自动触发，手动提交）
        public class Get_InSetConfirmType
        {
            public string strWorkShopGUID;
            public string strWorkShopName;
            public string strKeyStepName;
            public int ConfirmType;

        }
        public class Get_OutSetConfirmType
        {
            public string result = "";
            public string resultStr = "";
            public object data;
        }
        public Get_OutSetConfirmType SetConfirmType(string data)
        {
            Get_OutSetConfirmType json = new Get_OutSetConfirmType();
            try
            {
                Get_InSetConfirmType input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InSetConfirmType>(data);
                DBPlan_Beginwork_Rule db = new DBPlan_Beginwork_Rule();
                bool i = db.SetConfirmType(input.strWorkShopGUID, input.strWorkShopName, input.ConfirmType, input.strKeyStepName);
                if (i)
                {
                    json.result = "0";
                    json.resultStr = "返回成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "更新0条数据";
                }

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取流程的执行方式（是否按照顺序执行）
        public class Get_InGetExecByStep
        {
            public string strWorkShopGUID;

        }
        public class Get_OutGetExecByStep
        {
            public string result = "";
            public string resultStr = "";
            public object data;
        }
        public Get_OutGetExecByStep GetExecByStep(string data)
        {
            Get_OutGetExecByStep json = new Get_OutGetExecByStep();
            try
            {
                Get_InGetExecByStep input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetExecByStep>(data);
                DBPlan_Beginwork_Rule db = new DBPlan_Beginwork_Rule();
                Plan_Beginwork_RuleList list = db.GetExecByStepList(input.strWorkShopGUID);
                json.result = "0";
                json.resultStr = "返回成功";
                json.data = list;

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取流程的确认类型（值班员手动 或者按某步骤自动触发）
        public class Get_InConfirmType
        {
            public string strWorkShopGUID;

        }
        public class Get_GeConfirmType
        {
            public string result = "";
            public string resultStr = "";
            public object data;
        }
        public Get_GeConfirmType GetConfirmType(string data)
        {
            Get_GeConfirmType json = new Get_GeConfirmType();
            try
            {
                Get_InConfirmType input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InConfirmType>(data);
                DBPlan_Beginwork_Rule db = new DBPlan_Beginwork_Rule();
                Plan_Beginwork_RuleList list = db.GetConfirmTypeList(input.strWorkShopGUID);
                json.result = "0";
                json.resultStr = "返回成功";
                json.data = list;

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取该步骤是否能够被执行
        public class Get_InIsExecute
        {
            public string strWorkShopGUID;
            public string strTrainPlanGUID;
            public string strTrainmanGUID;
            public string strStepID;
            public string cid;

        }
        public class Get_OutIsExecute
        {
            public string result = "";
            public string resultStr = "";
            public object data;
        }
        public Get_OutIsExecute CanExecute(string data)
        {
            Get_OutIsExecute json = new Get_OutIsExecute();
            try
            {
                Get_InIsExecute input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InIsExecute>(data);
                DBPlan_Beginwork_Rule db = new DBPlan_Beginwork_Rule();
                IsExecute isExecute = new IsExecute();
                json.result = "0";
                json.resultStr = "返回成功";
                json.data = db.getIsExecute(input.strWorkShopGUID, input.strTrainPlanGUID, input.strTrainmanGUID, input.strStepID);
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 根据所传工号 判断是司机还是副司机学员
        public string getTmPost(string tpGUID, string tn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strTrainmanNumber1,strTrainmanNumber2,strTrainmanNumber3,strTrainmanNumber4 ");
            strSql.Append(" FROM VIEW_Plan_Trainman where strTrainPlanGUID= '" + tpGUID + "' ");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber1"].ToString()) == tn)
                return "1";
            else if (ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber2"].ToString()) == tn)
                return "2";
            else if (ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber3"].ToString()) == tn)
                return "3";
            else
                return "4";
        }
        #endregion

        #region 根绝车间和步骤的编号判断步骤执行的顺序
        public int getIndexOfStep(string strWorkShopGUID, string strStepId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 nStepIndex ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepDef where strWorkShopGUID= '" + strWorkShopGUID + "' and strStepID='" + strStepId + "' ");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["nStepIndex"].ToString());
            else
                return 0;
        }
        #endregion

        #region  判断表中是否已经存在  不存在添加直接执行下一步 否则删除
        public void getIndexOfStep(string strTrainPlanGUID, string strStepName, string strTrainmanGUID, string strTrainmanNumber,int WorkTypeID=1)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID= '" + strTrainPlanGUID + "' and strStepName='" + strStepName + "' and strTrainmanGUID='" + strTrainmanGUID + "'");
            int k = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
            if (k > 0)
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("delete FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID= '" + strTrainPlanGUID + "' and strStepName='" + strStepName + "' and strTrainmanGUID='" + strTrainmanGUID + "' ");
                SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql2.ToString());

                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("delete FROM TAB_Plan_Beginwork_StepData where strTrainPlanGUID= '" + strTrainPlanGUID + "' and strStepName='" + strStepName + "' and strTrainmanNumber='" + strTrainmanNumber + "'");
                SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql3.ToString());

                StringBuilder strSql4 = new StringBuilder();
                strSql4.Append("delete FROM TAB_Plan_Beginwork_StepIndex where strTrainPlanGUID= '" + strTrainPlanGUID + "' and strTrainmanNumber='" + strTrainmanNumber + "' and nWorkTypeID='" + WorkTypeID + "'");
                SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql4.ToString());
            }
        }
        #endregion


        #region 插入步骤信息
        public void AddStepInfo(List<StepIndex> LstepIndex, List<StepData> LstepData, StepResult StepResult)
        {
            DataTable tbStepIndex = CreateTable_StepIndex();
            getIndexOfStep(StepResult.strTrainPlanGUID, StepResult.strStepName, StepResult.strTrainmanGUID, StepResult.strTrainmanNumber);
            if (LstepIndex != null)
            {
                foreach (StepIndex si in LstepIndex)
                {
                    DataRow SiRow = tbStepIndex.NewRow();
                    SiRow["strTrainPlanGUID"] = si.strTrainPlanGUID;
                    SiRow["strTrainmanNumber"] = si.strTrainmanNumber;
                    SiRow["dtStartTime"] = si.dtStartTime;
                    SiRow["strFieldName"] = si.strFieldName;
                    SiRow["nStepData"] = si.nStepData;
                    if (si.dtStepData == null)
                        SiRow["dtStepData"] = DBNull.Value;
                    else
                        SiRow["dtStepData"] = si.dtStepData;

                    if (si.strStepData == null)
                        SiRow["strStepData"] = DBNull.Value;
                    else
                        SiRow["strStepData"] = si.strStepData;

                    tbStepIndex.Rows.Add(SiRow);
                }
            }
            DataTable tbStepData = CreateTable_StepData();

            if (LstepData != null)
            {
                foreach (StepData sd in LstepData)
                {
                    DataRow SiRow = tbStepData.NewRow();
                    SiRow["strTrainPlanGUID"] = sd.strTrainPlanGUID;
                    SiRow["strFieldName"] = sd.strFieldName;
                    SiRow["strStepName"] = sd.strStepName;
                    SiRow["nStepData"] = sd.nStepData;
                    SiRow["strTrainmanNumber"] = sd.strTrainmanNumber;
                    if (sd.dtStepData == null)
                        SiRow["dtStepData"] = DBNull.Value;
                    else
                        SiRow["dtStepData"] = sd.dtStepData;

                    if (sd.strStepData == null)
                        SiRow["strStepData"] = DBNull.Value;
                    else
                        SiRow["strStepData"] = sd.strStepData;
                    tbStepData.Rows.Add(SiRow);
                }
            }

            AddStepIndex(tbStepIndex);
            AddStepInfo(tbStepData);
            AddStepResult(StepResult);
        }


        /// <summary>
        ///  添加索引信息
        /// </summary>
        /// <param name="dt"></param>
        public void AddStepIndex(DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnString))
            {
                SqlBulkCopy bulk = new SqlBulkCopy(conn);
                bulk.DestinationTableName = "TAB_Plan_Beginwork_StepIndex";
                bulk.ColumnMappings.Add("strTrainPlanGUID", "strTrainPlanGUID");
                bulk.ColumnMappings.Add("strTrainmanNumber", "strTrainmanNumber");
                bulk.ColumnMappings.Add("dtStartTime", "dtStartTime");
                bulk.ColumnMappings.Add("strFieldName", "strFieldName");
                bulk.ColumnMappings.Add("nStepData", "nStepData");
                bulk.ColumnMappings.Add("dtStepData", "dtStepData");
                bulk.ColumnMappings.Add("strStepData", "strStepData");
                bulk.BatchSize = dt.Rows.Count;
                if (dt != null && dt.Rows.Count != 0)
                {
                    conn.Open();
                    bulk.WriteToServer(dt);
                }
                bulk.Close();
            }
        }


        /// <summary>
        /// 添加步骤的详细信息
        /// </summary>
        /// <param name="dt"></param>
        public void AddStepInfo(DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnString))
            {
                SqlBulkCopy bulk = new SqlBulkCopy(conn);
                bulk.DestinationTableName = "TAB_Plan_Beginwork_StepData";
                bulk.ColumnMappings.Add("strTrainPlanGUID", "strTrainPlanGUID");
                bulk.ColumnMappings.Add("strFieldName", "strFieldName");
                bulk.ColumnMappings.Add("nStepData", "nStepData");
                bulk.ColumnMappings.Add("dtStepData", "dtStepData");
                bulk.ColumnMappings.Add("strStepData", "strStepData");
                bulk.ColumnMappings.Add("strStepName", "strStepName");
                bulk.ColumnMappings.Add("strTrainmanNumber", "strTrainmanNumber");
                bulk.BatchSize = dt.Rows.Count;
                if (dt != null && dt.Rows.Count != 0)
                {
                    conn.Open();
                    bulk.WriteToServer(dt);
                }
                bulk.Close();
            }
        }



        public void AddStepResult(StepResult StepResult)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Beginwork_StepResult");
            strSql.Append("(strTrainPlanGUID,strStepName,nStepResult,dtBeginTime,dtEndTime,strStepBrief,dtCreateTime,nStepIndex,strTrainmanGUID,strTrainmanNumber,strTrainmanName,nWorkTypeID)");
            strSql.Append("values(@strTrainPlanGUID,@strStepName,@nStepResult,@dtBeginTime,@dtEndTime,@strStepBrief,@dtCreateTime,@nStepIndex,@strTrainmanGUID,@strTrainmanNumber,@strTrainmanName,@nWorkTypeID)");
            SqlParameter[] parameters = {
                  new SqlParameter("@strTrainPlanGUID", StepResult.strTrainPlanGUID),
                  new SqlParameter("@strStepName", StepResult.strStepName),
                  new SqlParameter("@nStepResult", StepResult.nStepResult),
                  new SqlParameter("@dtBeginTime", StepResult.dtBeginTime),
                  new SqlParameter("@dtEndTime", StepResult.dtEndTime),
                  new SqlParameter("@strStepBrief", StepResult.strStepBrief),
                  new SqlParameter("@dtCreateTime", StepResult.dtCreateTime),
                  new SqlParameter("@nStepIndex", StepResult.nStepIndex),
                  new SqlParameter("@strTrainmanGUID", StepResult.strTrainmanGUID),
                  new SqlParameter("@strTrainmanNumber", StepResult.strTrainmanNumber),
                  new SqlParameter("@strTrainmanName", StepResult.strTrainmanName),
                  new SqlParameter("@nWorkTypeID", StepResult.nWorkTypeID)
                                        };
            SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }


        //步骤的索引
        public class StepIndex
        {
            public string strTrainPlanGUID;
            public string strTrainmanNumber;
            public DateTime? dtStartTime;
            public string strFieldName;
            public int nStepData;
            public DateTime? dtStepData;
            public string strStepData;
        }

        //步骤的详细信息
        public class StepData
        {
            public string strTrainPlanGUID;
            public string strFieldName;
            public int nStepData;
            public DateTime? dtStepData;
            public string strStepData;
            public string strStepName;
            public string strTrainmanNumber;
        }

        //步骤的结果信息
        public class StepResult
        {
            public string strTrainPlanGUID;
            public string strStepName;
            public int nStepResult;
            public DateTime? dtBeginTime;
            public DateTime? dtEndTime;
            public string strStepBrief;
            public DateTime? dtCreateTime;
            public int nStepIndex;
            public string strTrainmanGUID;
            public string strTrainmanNumber;
            public string strTrainmanName;

            public int nWorkTypeID = 1;
        }

        #endregion

        #region 为三表分别创建相对应的DateTable
        private DataTable CreateTable_StepIndex()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("strTrainPlanGUID", typeof(string)));
            table.Columns.Add(new DataColumn("strTrainmanNumber", typeof(string)));
            table.Columns.Add(new DataColumn("dtStartTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("strFieldName", typeof(string)));
            table.Columns.Add(new DataColumn("nStepData", typeof(int)));
            table.Columns.Add(new DataColumn("dtStepData", typeof(DateTime)));
            table.Columns.Add(new DataColumn("strStepData", typeof(string)));
            table.Columns.Add(new DataColumn("strStepName", typeof(string)));
            return table;
        }
        private DataTable CreateTable_StepData()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("strTrainPlanGUID", typeof(string)));
            table.Columns.Add(new DataColumn("strFieldName", typeof(string)));
            table.Columns.Add(new DataColumn("strTrainmanNumber", typeof(string)));
            table.Columns.Add(new DataColumn("nStepData", typeof(int)));
            table.Columns.Add(new DataColumn("dtStepData", typeof(DateTime)));
            table.Columns.Add(new DataColumn("strStepData", typeof(string)));
            table.Columns.Add(new DataColumn("strStepName", typeof(string)));
            return table;
        }
        private DataTable CreateTable_StepResult()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("strTrainPlanGUID", typeof(string)));
            table.Columns.Add(new DataColumn("strStepName", typeof(string)));
            table.Columns.Add(new DataColumn("nStepResult", typeof(int)));
            table.Columns.Add(new DataColumn("dtBeginTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("dtEndTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("strStepBrief", typeof(string)));
            table.Columns.Add(new DataColumn("dtCreateTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("nStepIndex", typeof(int)));
            return table;
        }
        #endregion

        #region 测酒步骤上传
        public class InSubmit
        {
            //出勤人员GUID
            public string TrainmanGUID;
            //人员值乘的计划信息
            public string TrainPlanGUID;
            //测酒信息
            public DrinkData DrinkInfo = new DrinkData();
            //
            public int VerifyID;
            //
            public string Remark;
        }
        public class DrinkData
        {
            public string strGuid = "";
            public string trainmanID = "";
            public string drinkResult = "0";
            public int workTypeID = 0;
            public string createTime = "";
            public string imagePath = "";

            //人员信息
            public string strTrainmanName;
            public string strTrainmanNumber;

            //车次信息
            public string strTrainNo;
            public string strTrainNumber;
            public string strTrainTypeName;

            //车间信息
            public string strWorkShopGUID;
            public string strWorkShopName;
            //退勤点信息
            public string strPlaceID;
            public string strPlaceName;
            public string strSiteGUID;
            public string strSiteName;
            //酒精度
            public string dwAlcoholicity;
            public string nWorkTypeID;

            public string strAreaGUID;
            public Boolean bLocalAreaTrainman;

            //干部相关（部门名称（id），职位名称（id）)
            public string strDepartmentID;//部门id（车间DUID）
            public string strDepartmentName;//部门名称
            public string nCadreTypeID;//职位id
            public string strCadreTypeName;//职位名称

        }


        public Get_OutNoticeFlows AddDrinkFlows(string data)
        {
            Get_OutNoticeFlows json = new Get_OutNoticeFlows();
            try
            {
                DBDrink db = new DBDrink();
                InSubmit InParams = Newtonsoft.Json.JsonConvert.DeserializeObject<InSubmit>(data);
                db.AddDrinkInfo(InParams);
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }





        #endregion

        #region 记名式传达  通知通告接口步骤上传
        public class Get_OutNoticeFlows
        {
            public string result = "";
            public string resultStr = "";
            public object data;
        }

        public Get_OutNoticeFlows AddNoticeFlows(string data)
        {
            Get_OutNoticeFlows json = new Get_OutNoticeFlows();
            UpdataJiMing ujm = new UpdataJiMing();
            try
            {
                InFlows_Notice input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_Notice>(data);
                //如果传过的类型不是记名式传达
                if (input.strFileType != "4c628583-10b9-4962-ad09-71b96b44970f")
                { }
                else
                {
                    foreach (trainmanList t in input.trainmanList)
                    {
                        if (getNeedReadCount(t.strTrainmanGUID) <= input.fileList.Count)
                        {
                            #region 出勤步骤结果的实体信息
                            LCBeginworkFlow.StepResult StepResult = new LCBeginworkFlow.StepResult();
                            StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                            StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                            StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                            int nStepIndex = getIndexOfStep(input.strWorkShopGUID, "RS.STEP.WORKREADING");
                            if (nStepIndex == 0)
                                break;
                            StepResult.nStepIndex = nStepIndex;
                            StepResult.strStepBrief = "记名式传达";
                            StepResult.strStepName = "RS.STEP.WORKREADING";
                            StepResult.strTrainPlanGUID = input.strTrainPlanGUID;
                            StepResult.strTrainmanGUID = t.strTrainmanGUID;
                            StepResult.nStepResult = input.nStepResult;
                            StepResult.strTrainmanName = t.strTrainmanName;
                            StepResult.strTrainmanNumber = t.strTrainmanNumber;
                            #endregion
                            AddStepInfo(null, null, StepResult);
                            foreach (fileList fi in input.fileList)
                            {
                                ujm.UpdateReadTime(fi.fileGUID, t.strTrainmanGUID, input.dtBeginTime.ToString());
                            }
                        }
                        else
                        {
                            json.result = "0";
                            json.resultStr = "记名式传达阅读数量少于需要阅读的数量！";
                            return json;
                        }
                    }
                }
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }

        public int getNeedReadCount(string tmGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM TAB_ReadDocPlan where StrTrainmanGUID= '" + tmGUID + "' and dtEndTime>'" + DateTime.Now + "' ");
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }




        #endregion

        #region 验卡记录步骤接口的提交
        public Get_OutNoticeFlows AddWriteCardFlows(string data)
        {
            Get_OutNoticeFlows json = new Get_OutNoticeFlows();
            CheckCard cc = new CheckCard();
            card c = new card();
            try
            {
                InFlows_Card input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_Card>(data);

                foreach (trainmanList t in input.trainmanList)
                {
                    #region 出勤步骤结果的实体信息
                    LCBeginworkFlow.StepResult StepResult = new LCBeginworkFlow.StepResult();
                    StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                    StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                    StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                    int nStepIndex = getIndexOfStep(input.strWorkShopGUID, "RS.STEP.CHECKCARD");
                    if (nStepIndex == 0)
                        break;
                    StepResult.nStepIndex = nStepIndex;
                    StepResult.strStepBrief = "验卡记录";
                    StepResult.strStepName = "RS.STEP.CHECKCARD";
                    StepResult.strTrainPlanGUID = input.strTrainPlanGUID;
                    StepResult.strTrainmanGUID = t.strTrainmanGUID;
                    StepResult.nStepResult = input.nStepResult;
                    StepResult.strTrainmanName = t.strTrainmanName;
                    StepResult.strTrainmanNumber = t.strTrainmanNumber;
                    #endregion

                    #region 出勤步骤索引信息
                    List<LCBeginworkFlow.StepIndex> LStepIndex = new List<LCBeginworkFlow.StepIndex>();
                    LCBeginworkFlow.StepIndex StepIndex = new LCBeginworkFlow.StepIndex();
                    StepIndex.dtStartTime = ObjectConvertClass.static_ext_date(DateTime.Now);
                    StepIndex.strFieldName = "IsCheckCard";
                    StepIndex.nStepData = input.nTestCardResult; ;
                    StepIndex.strTrainmanNumber = t.strTrainmanNumber;
                    StepIndex.strTrainPlanGUID = input.strTrainPlanGUID;
                    LStepIndex.Add(StepIndex);
                    #endregion

                    #region 出勤步骤详细信息
                    List<LCBeginworkFlow.StepData> LStepData = new List<LCBeginworkFlow.StepData>();
                    LCBeginworkFlow.StepData StepData1 = new LCBeginworkFlow.StepData();
                    StepData1.strTrainPlanGUID = input.strTrainPlanGUID;
                    StepData1.strFieldName = "nTestCardResult";
                    StepData1.nStepData = input.nTestCardResult;
                    StepData1.strTrainmanNumber = t.strTrainmanNumber;
                    StepData1.strStepName = "RS.STEP.CHECKCARD";
                    LStepData.Add(StepData1);

                    LCBeginworkFlow.StepData StepData2 = new LCBeginworkFlow.StepData();
                    StepData2.strTrainPlanGUID = input.strTrainPlanGUID;
                    StepData2.strFieldName = "strTestCardResult";
                    StepData2.strStepData = input.strTestCardResult;
                    StepData2.strTrainmanNumber = t.strTrainmanNumber;
                    StepData2.strStepName = "RS.STEP.CHECKCARD";
                    LStepData.Add(StepData2);
                    #endregion
                    c.dtCreateTime = input.dtCreateTime.ToString();
                    c.dtEventTime = input.dtBeginTime.ToString();
                    c.strEventBrief = input.strTestCardResult;
                    c.strTrainmanName = t.strTrainmanName;
                    c.strTrainmanNumber = t.strTrainmanNumber;
                    c.strTrainPlanGUID = input.strTrainPlanGUID;
                    cc.AddStepResult(c);
                    TF.CommonUtility.LogClass.log(input.strTestCardResult);
                    AddStepInfo(LStepIndex, LStepData, StepResult);
                }
                json.result = "0";
                json.resultStr = "返回成功";

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }

        #endregion

        #region 个性化出勤打印接口
        public Get_OutNoticeFlows AddSpecificOnDutyPrintFlows(string data)
        {
            Get_OutNoticeFlows json = new Get_OutNoticeFlows();
            try
            {
                InFlows_SpecificOnDutyPrint input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_SpecificOnDutyPrint>(data);

                foreach (trainmanList t in input.trainmanList)
                {

                    #region 出勤步骤结果的实体信息
                    LCBeginworkFlow.StepResult StepResult = new LCBeginworkFlow.StepResult();
                    StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                    StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                    StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                    int nStepIndex = getIndexOfStep(input.strWorkShopGUID, "RS.STEP.PRINT.GXHCQ");
                    if (nStepIndex == 0)
                        break;
                    StepResult.nStepIndex = nStepIndex;
                    StepResult.strStepBrief = "个性化出勤打印";
                    StepResult.strStepName = "RS.STEP.PRINT.GXHCQ";
                    StepResult.strTrainPlanGUID = input.strTrainPlanGUID;
                    StepResult.strTrainmanGUID = t.strTrainmanGUID;
                    StepResult.nStepResult = input.nStepResult;
                    StepResult.strTrainmanName = t.strTrainmanName;
                    StepResult.strTrainmanNumber = t.strTrainmanNumber;
                    #endregion

                    AddStepInfo(null, null, StepResult);
                }
                json.result = "0";
                json.resultStr = "返回成功";

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }

        #endregion

        #region 交付揭示勾画
        public Get_OutNoticeFlows AddJiaoFuDrawFlows(string data)
        {
            Get_OutNoticeFlows json = new Get_OutNoticeFlows();
            try
            {
                InFlows_JiaoFuDraw input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_JiaoFuDraw>(data);
                foreach (trainmanList t in input.trainmanList)
                {
                    #region 出勤步骤结果的实体信息
                    LCBeginworkFlow.StepResult StepResult = new LCBeginworkFlow.StepResult();
                    StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                    StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                    StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                    int nStepIndex = getIndexOfStep(input.strWorkShopGUID, "RS.STEP.WORKREADING");
                    if (nStepIndex == 0)
                        break;
                    StepResult.nStepIndex = nStepIndex;
                    StepResult.strStepBrief = "交付揭示勾画";
                    StepResult.strStepName = "RS.STEP.WORKREADING";
                    StepResult.strTrainPlanGUID = input.strTrainPlanGUID;
                    StepResult.strTrainmanGUID = t.strTrainmanGUID;
                    StepResult.nStepResult = input.nStepResult;
                    StepResult.strTrainmanName = t.strTrainmanName;
                    StepResult.strTrainmanNumber = t.strTrainmanNumber;
                    #endregion

                    AddStepInfo(null, null, StepResult);
                }
                json.result = "0";
                json.resultStr = "返回成功";

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }

        #endregion

        #region 公布揭示阅读


        public Get_OutNoticeFlows AddGongBuJieShiReadFlows(string data)
        {
            Get_OutNoticeFlows json = new Get_OutNoticeFlows();
            try
            {
                InFlows_GongBuJieShiRead input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_GongBuJieShiRead>(data);
                foreach (trainmanList t in input.trainmanList)
                {
                    #region 出勤步骤结果的实体信息
                    LCBeginworkFlow.StepResult StepResult = new LCBeginworkFlow.StepResult();
                    StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                    StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                    StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                    int nStepIndex = getIndexOfStep(input.strWorkShopGUID, "RS.STEP.PUBJIESHI.READ");
                    if (nStepIndex == 0)
                        break;
                    StepResult.nStepIndex = nStepIndex;
                    StepResult.strStepBrief = "公布揭示阅读";
                    StepResult.strStepName = "RS.STEP.PUBJIESHI.READ";
                    StepResult.strTrainPlanGUID = input.strTrainPlanGUID;
                    StepResult.strTrainmanGUID = t.strTrainmanGUID;
                    StepResult.nStepResult = input.nStepResult;
                    StepResult.strTrainmanName = t.strTrainmanName;
                    StepResult.strTrainmanNumber = t.strTrainmanNumber;
                    #endregion

                    AddStepInfo(null, null, StepResult);
                }
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }

        #endregion

        #region 转储
        public class InFlows_OutZhuanChu : Plan_Beginwork_StepResult
        {
            public RunRecordFileMain runRecordFileMain = new RunRecordFileMain();
            public List<trainmanList> trainmanList;
            public string strWorkShopGUID;
        }

        /// <summary>
        /// 退勤转储
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Get_OutNoticeFlows AddTQZhuanChu(string data)
        {
            Get_OutNoticeFlows json = new Get_OutNoticeFlows();
            try
            {
                InFlows_OutZhuanChu input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_OutZhuanChu>(data);
                //保存转储数据
                DBEndWork dal = new DBEndWork();

             
                if (input.runRecordFileMain!=null)
                  dal.SubMitRunRecordInfo(input.runRecordFileMain);
                else
                    LogClass.log("没有转储数据");

                LCBeginworkFlow.StepResult StepResult = null;
                int nStepIndex = getIndexOfStep(input.strWorkShopGUID, "RS.STEP.ZHUANCHU");
                if (nStepIndex != 0)
                {
                    //保存转储步骤执行数据
                    foreach (trainmanList t in input.trainmanList)
                    {
                        #region 出勤步骤结果的实体信息
                        StepResult = new LCBeginworkFlow.StepResult();
                        StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                        StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                        StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                        StepResult.nStepIndex = nStepIndex;
                        StepResult.strStepBrief = "运行记录转储";
                        StepResult.strStepName = "RS.STEP.ZHUANCHU";
                        StepResult.strTrainPlanGUID = input.strTrainPlanGUID;
                        StepResult.strTrainmanGUID = t.strTrainmanGUID;
                        StepResult.nStepResult = input.nStepResult;
                        StepResult.strTrainmanName = t.strTrainmanName;
                        StepResult.strTrainmanNumber = t.strTrainmanNumber;
                        StepResult.nWorkTypeID = 2;
                        #endregion
                        AddStepInfo(null, null, StepResult);
                    }
                    dal.setPlanState(nStepIndex, input.strWorkShopGUID, input.strTrainPlanGUID);
                }

                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }



        #endregion

        #region 获取执行结果详情 用于出勤客户端显示流程步骤
        public class Get_InStepData
        {
            public string strTrainplanGUID;
        }
        public class Get_OutStepData
        {
            public string result = "";
            public string resultStr = "";
            public Beginwork_Flow data;
        }

        public class Beginwork_Flow
        {
            public int nFlowState;
            public string strTrainPlanGUID = "";
            public string strUserNumber = "";
            public string strUserName = "";
            public DateTime? dtCreateTime;
            public string strConfirmBrief = "";
            public DateTime? dtBeginTime;
            public DateTime? dtEndTime;
            public List<object> Steps = new List<object>();
        }

        public Get_OutStepData GetStepData(string data)
        {
            Get_OutStepData json = new Get_OutStepData();
            try
            {
                Get_InStepData input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InStepData>(data);
                DBPlan_Beginwork_StepData db = new DBPlan_Beginwork_StepData();
                json.data = db.GetStepData(input.strTrainplanGUID);
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion



    }

    #region 记名式传达 通知通告传入的实体
    public class InFlows_Notice
    {
        public string strTrainPlanGUID;
        public int nStepIndex;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public int nStepResult;
        public DateTime? dtCreateTime;
        public int IsRead;
        public List<trainmanList> trainmanList;
        public List<fileList> fileList;
        public string strFileType;
        public string strWorkShopGUID;
        public string cid;

    }
    public class fileList
    {
        public string fileGUID;
    }


    #endregion

    #region 验卡记录实体类库
    public class InFlows_Card
    {
        public string strTrainPlanGUID;
        public int nStepIndex;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public int nStepResult;
        public DateTime? dtCreateTime;
        public int nTestCardResult;
        public string strTestCardResult;
        public List<trainmanList> trainmanList;
        public string strWorkShopGUID;
        public string cid;
    }


    #endregion

    #region 个性化出勤打印实体类库
    public class InFlows_SpecificOnDutyPrint
    {
        public string strTrainPlanGUID;
        public int nStepIndex;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public int nStepResult;
        public DateTime? dtCreateTime;
        public int nIsPrint;
        public List<trainmanList> trainmanList;
        public string strWorkShopGUID;
        public string cid;

    }


    #endregion

    #region 交付揭示勾画实体类库
    public class InFlows_JiaoFuDraw
    {
        public string strTrainPlanGUID;
        public int nStepIndex;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public int nStepResult;
        public DateTime? dtCreateTime;
        public int nIsDraw;
        public List<trainmanList> trainmanList;
        public string strWorkShopGUID;
        public string cid;

    }


    #endregion

    #region 公布揭示阅读
    public class InFlows_GongBuJieShiRead
    {
        public string strTrainPlanGUID;
        public int nStepIndex;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public int nStepResult;
        public DateTime? dtCreateTime;
        public int IsRead;
        public List<trainmanList> trainmanList;
        public string strWorkShopGUID;
        public string cid;

    }
    #endregion

    #region  人员
    public class trainmanList
    {
        public string strTrainmanNumber;
        public string strTrainmanName;
        public string strTrainmanGUID;
    }
    #endregion

}
