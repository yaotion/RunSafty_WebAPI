using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.CommonUtility;
using System.Data;

namespace TF.RunSafty.WorkSteps
{
    //定义一个运算基类，包含GetResult虚方法  
    public class Operate //定义一个运算类  
    {
        public int _nWorkTypeID { get; set; }
        public virtual void CheckIsSpecialStep()
        {

        }

        public virtual void GetResult(object data, int nWorkTypeID)//返回运算后结果的方法  
        {


        }



    }
    #region 记名式传达
    public class WorkReading : Operate    //记名式传达
    {
        public override void GetResult(object data, int nWorkTypeID)
        {
            InFlows_Notice input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_Notice>(data.ToString());

            DBStep db = new DBStep();
            //如果传过的类型不是记名式传达
            if (input.strFileType == "4c628583-10b9-4962-ad09-71b96b44970f")
            {
                foreach (trainmanList t in input.trainmanList)
                {
                    if (db.getNeedReadCount(t.strTrainmanGUID) <= input.fileList.Count)
                    {
                        #region 出勤步骤结果的实体信息
                        StepResult StepResult = new StepResult();
                        StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                        StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                        StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                        int nStepIndex = db.getIndexOfStep(input.strWorkShopGUID, "RS.STEP.WORKREADING", nWorkTypeID);
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
                        StepResult.nWorkTypeID = nWorkTypeID;
                        #endregion
                        db.AddStep(null, null, StepResult);

                        //向旧的接口提交数据
                        MDPlan_Beginwork_Step md = new MDPlan_Beginwork_Step();
                        md.dtCreateTime = input.dtCreateTime;
                        md.dtEventEndTime = input.dtEndTime;
                        md.dtEventTime = input.dtCreateTime;
                        md.nStepID = 1004;
                        md.nStepResultID = 1;
                        md.strStepResultText = "";
                        md.strTrainmanGUID = t.strTrainmanGUID;
                        md.strTrainmanName = t.strTrainmanName;
                        md.strTrainmanNumber = t.strTrainmanName;
                        md.strTrainPlanGUID = input.strTrainPlanGUID;
                        db.AddPlan_Beginwork_Step(md);
                        //向旧的接口提交数据结束
                        foreach (fileList fi in input.fileList)
                        {
                            db.UpdateReadTime(fi.fileGUID, t.strTrainmanGUID, input.dtBeginTime.ToString());
                        }
                        //检测是否是特殊步骤
                        if (db.CheckIsSpecialStep("RS.STEP.WORKREADING", input.strWorkShopGUID, nWorkTypeID))
                        {
                            db.UpdateToYiChuQin(input.strTrainPlanGUID, DateTime.Now, "", "", "", nWorkTypeID);
                        }
                    }
                }
            }

            db.creatMsg("RS.STEP.WORKREADING", input.strTrainPlanGUID, input.cid, nWorkTypeID, input.strWorkShopGUID);
        }
    }
    #endregion

    #region 个性化出勤
    public class Gxhcq : Operate    //个性化出勤
    {
        public override void GetResult(object data, int nWorkTypeID)
        {

            InFlows_SpecificOnDutyPrint input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_SpecificOnDutyPrint>(data.ToString());
            DBStep db = new DBStep();
            foreach (trainmanList t in input.trainmanList)
            {
                #region 出勤步骤结果的实体信息
                StepResult StepResult = new StepResult();
                StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                int nStepIndex = db.getIndexOfStep(input.strWorkShopGUID, "RS.STEP.PRINT.GXHCQ", nWorkTypeID);
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
                StepResult.nWorkTypeID = nWorkTypeID;
                #endregion
                db.AddStep(null, null, StepResult);

                //向旧的接口提交数据
                MDPlan_Beginwork_Step md = new MDPlan_Beginwork_Step();
                md.dtCreateTime = input.dtCreateTime;
                md.dtEventEndTime = input.dtEndTime;
                md.dtEventTime = input.dtCreateTime;
                md.nStepID = 1013;
                md.nStepResultID = 1;
                md.strStepResultText = "";
                md.strTrainmanGUID = t.strTrainmanGUID;
                md.strTrainmanName = t.strTrainmanName;
                md.strTrainmanNumber = t.strTrainmanName;
                md.strTrainPlanGUID = input.strTrainPlanGUID;
                db.AddPlan_Beginwork_Step(md);
                //向旧的接口提交数据结束


                //检测是否是特殊步骤
                if (db.CheckIsSpecialStep("RS.STEP.PRINT.GXHCQ", input.strWorkShopGUID, nWorkTypeID))
                {
                    db.UpdateToYiChuQin(input.strTrainPlanGUID, DateTime.Now, "", "", "", nWorkTypeID);
                }
            }
            db.creatMsg("RS.STEP.PRINT.GXHCQ", input.strTrainPlanGUID, input.cid, nWorkTypeID, input.strWorkShopGUID);
        }
    }
    #endregion

    #region 验卡
    public class CheckCard : Operate   //验卡  
    {
        public override void GetResult(object data, int nWorkTypeID)
        {
            InFlows_Card input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_Card>(data.ToString());
            DBStep db = new DBStep();
            IdIcCard c = new IdIcCard();
            int k = 0;//判断循环的次数
            foreach (trainmanList t in input.trainmanList)
            {
                #region 出勤步骤结果的实体信息
                StepResult StepResult = new StepResult();
                StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                int nStepIndex = db.getIndexOfStep(input.strWorkShopGUID, "RS.STEP.CHECKCARD", nWorkTypeID);
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
                StepResult.nWorkTypeID = nWorkTypeID;
                #endregion

                #region 出勤步骤索引信息
                List<StepIndex> LStepIndex = new List<StepIndex>();
                StepIndex StepIndex = new StepIndex();
                StepIndex.dtStartTime = ObjectConvertClass.static_ext_date(DateTime.Now);
                StepIndex.strFieldName = "IsCheckCard";
                StepIndex.nStepData = input.nTestCardResult; ;
                StepIndex.strTrainmanNumber = t.strTrainmanNumber;
                StepIndex.strTrainPlanGUID = input.strTrainPlanGUID;
                StepIndex.nWorkTypeID = nWorkTypeID;
                LStepIndex.Add(StepIndex);
                #endregion

                #region 出勤步骤详细信息

                DBDrink dbpost = new DBDrink();
                string npost = dbpost.getTmPost(input.strTrainPlanGUID, t.strTrainmanNumber);

                List<StepData> LStepData = new List<StepData>();
                StepData StepData1 = new StepData();
                StepData1.strTrainPlanGUID = input.strTrainPlanGUID;
                StepData1.strFieldName = "nTestCardResult" + npost;
                StepData1.nStepData = input.nTestCardResult;
                StepData1.strTrainmanNumber = t.strTrainmanNumber;
                StepData1.strStepName = "RS.STEP.CHECKCARD";
                StepData1.nWorkTypeID = nWorkTypeID;
                LStepData.Add(StepData1);

                StepData StepData2 = new StepData();
                StepData2.strTrainPlanGUID = input.strTrainPlanGUID;
                StepData2.strFieldName = "strTestCardResult" + npost;
                StepData2.strStepData = input.strTestCardResult;
                StepData2.strTrainmanNumber = t.strTrainmanNumber;
                StepData2.strStepName = "RS.STEP.CHECKCARD";
                StepData2.nWorkTypeID = nWorkTypeID;
                LStepData.Add(StepData2);
                #endregion


                c.dtCreateTime = input.dtCreateTime.ToString();
                c.dtEventTime = input.dtBeginTime.ToString();
                c.strEventBrief = input.strTestCardResult;
                c.strTrainmanName = t.strTrainmanName;
                c.strTrainmanNumber = t.strTrainmanNumber;
                c.strTrainPlanGUID = input.strTrainPlanGUID;
                db.AddStep(LStepIndex, LStepData, StepResult);

                //添加验卡实际的业务逻辑
                db.AddIDICCard(c);

                //向旧的接口提交数据
                MDPlan_Beginwork_Step md = new MDPlan_Beginwork_Step();
                md.dtCreateTime = input.dtCreateTime;
                md.dtEventEndTime = input.dtEndTime;
                md.dtEventTime = input.dtCreateTime;
                md.nStepID = 1008;
                md.nStepResultID = 1;
                md.strStepResultText = "";
                md.strTrainmanGUID = t.strTrainmanGUID;
                md.strTrainmanName = t.strTrainmanName;
                md.strTrainmanNumber = t.strTrainmanName;
                md.strTrainPlanGUID = input.strTrainPlanGUID;
                db.AddPlan_Beginwork_Step(md);

                //向运行记录中增加数据
                MDRunEvent runEvent = new MDRunEvent();
                runEvent.dtCreateTime = DateTime.Now;
                runEvent.dtEventTime = input.dtBeginTime;
                runEvent.nEventID = 10008;
                runEvent.nResult = 10008;
                runEvent.nTMIS = input.placeID;
                runEvent.strFlowID = "";
                runEvent.strGroupGUID = input.strTrainPlanGUID;
                runEvent.strResult = input.strTestCardResult;
                runEvent.strTrainPlanGUID = input.strTrainPlanGUID;
                runEvent.strRunEventGUID = Guid.NewGuid().ToString();
                int isAddResult = 0;
                string Ex = "";
                if (!string.IsNullOrEmpty(input.strTrainPlanGUID))
                {
                    DataTable dt = db.getTrainInfo(input.strTrainPlanGUID);
                    if (dt.Rows.Count > 0)
                    {
                        runEvent.strTrainTypeName = dt.Rows[0]["strTrainTypeName"].ToString();
                        runEvent.nKeHuo = Convert.ToInt32(dt.Rows[0]["nKehuoID"].ToString());
                        runEvent.strTrainNo = dt.Rows[0]["strTrainNo"].ToString();
                        runEvent.strTrainNumber = dt.Rows[0]["strTrainNumber"].ToString();
                        runEvent.strTrainmanNumber1 = dt.Rows[0]["strTrainmanNumber1"].ToString();
                        runEvent.strTrainmanNumber2 = dt.Rows[0]["strTrainmanNumber2"].ToString();
                        try
                        {
                            if (db.nCheckIsExist(input.strTrainPlanGUID) >= 1)
                            {
                                if (k == 0)//如果是循环到第一个人，并且是数据库中有记录的话  要进行覆盖
                                    isAddResult = db.UpdatePlan_RunEvent(runEvent);
                            }
                            else
                            {
                                isAddResult = db.AddPlan_RunEvent(runEvent);
                                k++;
                            }
                        }
                        catch (Exception ex)
                        {
                            isAddResult = 2;
                            Ex = ex.Message.ToString();

                        }
                    }
                }
                RunEvent_TrainmanDetail rt = new RunEvent_TrainmanDetail();
                rt.dtCreateTime = DateTime.Now;
                rt.dtEventTime = input.dtBeginTime;
                rt.nEventID = 10008;
                rt.nKeHuo = runEvent.nKeHuo;
                rt.nResultID = input.nTestCardResult;
                rt.strResult = input.strTestCardResult;
                if (isAddResult == 0)
                {
                    rt.nSubmitResult = 0;
                    rt.strSubmitRemark = "添加失败,返回0条数据！";
                }
                else if (isAddResult == 1)
                {
                    rt.nSubmitResult = 1;
                    rt.strSubmitRemark = "添加成功！";
                }
                else if (isAddResult == 2)
                {
                    rt.nSubmitResult = 2;
                    rt.strSubmitRemark = "添加失败！异常信息" + Ex;
                }

                rt.nTMIS = input.placeID;
                rt.strRunEventGUID = runEvent.strRunEventGUID;
                rt.strTrainmanNumber = t.strTrainmanNumber;
                db.AddTrainmanDetail(rt);

                //检测是否是特殊步骤
                if (db.CheckIsSpecialStep("RS.STEP.CHECKCARD", input.strWorkShopGUID, nWorkTypeID))
                {
                    db.UpdateToYiChuQin(input.strTrainPlanGUID, DateTime.Now, "", "", "", nWorkTypeID);
                }
            }
            db.creatMsg("RS.STEP.CHECKCARD", input.strTrainPlanGUID, input.cid, nWorkTypeID, input.strWorkShopGUID);
        }
    }
    #endregion

    #region 公布揭示勾画
    public class PubJiShiGouHua : Operate    //公布揭示勾画
    {
        public override void GetResult(object data, int nWorkTypeID)
        {
            InFlows_JiaoFuDraw input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_JiaoFuDraw>(data.ToString());

            DBStep db = new DBStep();
            foreach (trainmanList t in input.trainmanList)
            {
                #region 出勤步骤结果的实体信息
                StepResult StepResult = new StepResult();
                StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                int nStepIndex = db.getIndexOfStep(input.strWorkShopGUID, "RS.STEP.PUBJIESHI.GOUHUA", nWorkTypeID);
                if (nStepIndex == 0)
                    break;
                StepResult.nStepIndex = nStepIndex;
                StepResult.strStepBrief = "交付揭示勾画";
                StepResult.strStepName = "RS.STEP.PUBJIESHI.GOUHUA";
                StepResult.strTrainPlanGUID = input.strTrainPlanGUID;
                StepResult.strTrainmanGUID = t.strTrainmanGUID;
                StepResult.nStepResult = input.nStepResult;
                StepResult.strTrainmanName = t.strTrainmanName;
                StepResult.strTrainmanNumber = t.strTrainmanNumber;
                StepResult.nWorkTypeID = nWorkTypeID;
                #endregion
                db.AddStep(null, null, StepResult);

                //向旧的接口提交数据
                MDPlan_Beginwork_Step md = new MDPlan_Beginwork_Step();
                md.dtCreateTime = input.dtCreateTime;
                md.dtEventEndTime = input.dtEndTime;
                md.dtEventTime = input.dtCreateTime;
                md.nStepID = 1009;
                md.nStepResultID = 1;
                md.strStepResultText = "";
                md.strTrainmanGUID = t.strTrainmanGUID;
                md.strTrainmanName = t.strTrainmanName;
                md.strTrainmanNumber = t.strTrainmanName;
                md.strTrainPlanGUID = input.strTrainPlanGUID;
                db.AddPlan_Beginwork_Step(md);
                //向旧的接口提交数据结束

                //检测是否是特殊步骤
                if (db.CheckIsSpecialStep("RS.STEP.PUBJIESHI.GOUHUA", input.strWorkShopGUID, nWorkTypeID))
                {
                    db.UpdateToYiChuQin(input.strTrainPlanGUID, DateTime.Now, "", "", "", nWorkTypeID);
                }
            }
            db.creatMsg("RS.STEP.PUBJIESHI.GOUHUA", input.strTrainPlanGUID, input.cid, nWorkTypeID, input.strWorkShopGUID);
        }
    }
    #endregion

    #region 公布揭示阅读
    public class PubJiShiRead : Operate    //公布揭示阅读
    {
        public override void GetResult(object data, int nWorkTypeID)
        {
            InFlows_GongBuJieShiRead input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_GongBuJieShiRead>(data.ToString());

            DBStep db = new DBStep();
            foreach (trainmanList t in input.trainmanList)
            {
                #region 出勤步骤结果的实体信息
                StepResult StepResult = new StepResult();
                StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                int nStepIndex = db.getIndexOfStep(input.strWorkShopGUID, "RS.STEP.PUBJIESHI.READ", nWorkTypeID);
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
                StepResult.nWorkTypeID = nWorkTypeID;
                #endregion
                db.AddStep(null, null, StepResult);

                //向旧的接口提交数据
                MDPlan_Beginwork_Step md = new MDPlan_Beginwork_Step();
                md.dtCreateTime = input.dtCreateTime;
                md.dtEventEndTime = input.dtEndTime;
                md.dtEventTime = input.dtCreateTime;
                md.nStepID = 1012;
                md.nStepResultID = 1;
                md.strStepResultText = "";
                md.strTrainmanGUID = t.strTrainmanGUID;
                md.strTrainmanName = t.strTrainmanName;
                md.strTrainmanNumber = t.strTrainmanName;
                md.strTrainPlanGUID = input.strTrainPlanGUID;
                db.AddPlan_Beginwork_Step(md);
                //向旧的接口提交数据结束

                //检测是否是特殊步骤
                if (db.CheckIsSpecialStep("RS.STEP.PUBJIESHI.READ", input.strWorkShopGUID, nWorkTypeID))
                {
                    db.UpdateToYiChuQin(input.strTrainPlanGUID, DateTime.Now, "", "", "", nWorkTypeID);
                }
            }
            db.creatMsg("RS.STEP.PUBJIESHI.READ", input.strTrainPlanGUID, input.cid, nWorkTypeID, input.strWorkShopGUID);
        }
    }
    #endregion

    #region 测酒信息提交
    public class Drink : Operate         //测酒信息提交
    {
        public override void GetResult(object data, int nWorkTypeID)
        {
            DBDrink db = new DBDrink();
            DBStep dbs = new DBStep();
            InSubmitDrink input = Newtonsoft.Json.JsonConvert.DeserializeObject<InSubmitDrink>(data.ToString());
            db.AddDrinkInfo(input, nWorkTypeID);
            dbs.creatMsg("RS.STEP.DRINKTEST", input.TrainPlanGUID, input.cid, nWorkTypeID, input.DrinkInfo.strWorkShopGUID);
            dbs.creatDrinkMsg(input);

        }

    }
    #endregion


    #region 退勤转储
    /// <summary>
    /// 转储
    /// </summary>
    public class TQZhuanChu : Operate
    {
        InFlows_TQZhuanChu input = new InFlows_TQZhuanChu();

        public override void GetResult(object data, int nWorkTypeID)
        {
            base._nWorkTypeID = nWorkTypeID;
            input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_TQZhuanChu>(data.ToString());
            //保存转储数据
            DBEndWork dal = new DBEndWork();
            if (input.runRecordFileMain != null)
                dal.SubMitRunRecordInfo(input.runRecordFileMain);
            else
                LogClass.log("没有转储数据");
            DBStep db = new DBStep();
            StepResult StepResult = null;
            int nStepIndex = db.getIndexOfStep(input.strWorkShopGUID, WorkType.TQZhuanChu, nWorkTypeID);
            if (nStepIndex != 0)
            {
                //保存转储步骤执行数据
                foreach (trainmanList t in input.trainmanList)
                {
                    #region 退勤步骤结果的实体信息
                    StepResult = new StepResult();
                    StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                    StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                    StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
                    StepResult.nStepIndex = nStepIndex;
                    StepResult.strStepBrief = "运行记录转储";
                    StepResult.strStepName = WorkType.TQZhuanChu;
                    StepResult.strTrainPlanGUID = input.strTrainPlanGUID;
                    StepResult.strTrainmanGUID = t.strTrainmanGUID;
                    StepResult.nStepResult = input.nStepResult;
                    StepResult.strTrainmanName = t.strTrainmanName;
                    StepResult.strTrainmanNumber = t.strTrainmanNumber;
                    StepResult.nWorkTypeID = 2;
                    StepResult.strStepName = "RS.STEP.ICCARDRUNRECORD";

                    #endregion
                    db.AddStep(null, null, StepResult);
                }

                db.creatMsg("RS.STEP.ICCARDRUNRECORD", input.strTrainPlanGUID, input.cid, nWorkTypeID, input.strWorkShopGUID);
            }
        }

        public override void CheckIsSpecialStep()
        {
            DBStep db = new DBStep();
            //检测是否是特殊步骤
            if (db.CheckIsSpecialStep(WorkType.TQZhuanChu, input.strWorkShopGUID, base._nWorkTypeID))
            {

                DBStep dbStep = new DBStep();
                dbStep.UpdateToYiTuiQin(input.strTrainPlanGUID, input.runRecordFileMain.strSiteNumber, DateTime.Now, "", "", "", 2);


            }
        }
    }
    #endregion

    public class QiTa : Operate  //其它
    {
        public override void GetResult(object data, int nWorkTypeID)
        {
            InFlows_Base input = Newtonsoft.Json.JsonConvert.DeserializeObject<InFlows_Base>(data.ToString());
            DBStep db = new DBStep();
            foreach (trainmanList t in input.trainmanList)
            {
                StepResult StepResult = new StepResult();
                StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(input.dtBeginTime);
                StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(input.dtCreateTime);
                StepResult.dtEndTime = ObjectConvertClass.static_ext_date(input.dtEndTime);
              
                //int nStepIndex = db.getIndexOfStep(input.strWorkShopGUID, input.stepName, nWorkTypeID);
                StepResult.nStepIndex = 1;

                StepResult.strStepBrief = input.stepChineseName;
                StepResult.strStepName = input.stepName;
                StepResult.strTrainPlanGUID = input.strTrainPlanGUID;

                //人员没有guid的情况下，从新从人员库中获取
                if (t.strTrainmanGUID == null || t.strTrainmanGUID == "")
                    StepResult.strTrainmanGUID = db.getTrainManGUID(t.strTrainmanNumber);
                else
                    StepResult.strTrainmanGUID = t.strTrainmanGUID;


                StepResult.nStepResult = input.nStepResult;
                StepResult.strTrainmanName = t.strTrainmanName;
                StepResult.strTrainmanNumber = t.strTrainmanNumber;
                StepResult.nWorkTypeID = nWorkTypeID;
                db.AddStep(null, null, StepResult);
            }
        }


    }
}
