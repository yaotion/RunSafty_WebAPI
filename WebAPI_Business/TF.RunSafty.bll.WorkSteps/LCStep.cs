using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.CommonUtility;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using ThinkFreely.RunSafty;

namespace TF.RunSafty.WorkSteps
{
    public class Get_Out
    {
        public string result = "";
        public string resultStr = "";
        public object data;
    }

    public class Steps
    {
        #region 提交所有步骤信息
        public Get_Out SubmitSteps(string data)
        {


            Get_Out OutResult = new Get_Out();
            Datas input = Newtonsoft.Json.JsonConvert.DeserializeObject<Datas>(data);
            Operate oper = OperationFactoty.createOperate(input.stepName,input.nWorkTypeID);
            try
            {
                if (oper != null)
                {
                    oper.GetResult(input.stepEntity, input.nWorkTypeID);
                    //检测是否是特殊步骤
                    oper.CheckIsSpecialStep();
                    OutResult.result = "0";
                    OutResult.resultStr = "执行成功！";
                }
                else
                {
                    OutResult.resultStr = "提交失败：无法找到该步骤，该步骤名称：" + input.stepName;
                }
            }
            catch (Exception ex)
            {
                OutResult.result = "1";
                OutResult.resultStr = "提交失败：" + ex.Message;
            }
            return OutResult;
        }
        #endregion

        #region 获取该步骤是否能够被执行
        public class Get_InIsExecute
        {
            public string strWorkShopGUID;
            public string strTrainPlanGUID;
            public string strTrainmanGUID;
            public string strStepName;
            public string cid;
            public int nWorkTypeID;

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
                DBStep db = new DBStep();
                IsExecute isExecute = new IsExecute();
                json.result = "0";
                json.resultStr = "返回成功";
                json.data = db.getIsExecute(input.strWorkShopGUID, input.strTrainPlanGUID, input.strTrainmanGUID, input.strStepName, input.nWorkTypeID);
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 将出勤流程强制设置成已出勤

        public class Get_In_setToWorkOut
        {
            public string strTrainPlanGUID;
            public string strUserName;
            public string strUserNumber;
            public DateTime dtConfirmTime;
            public string strConfirmBrief;
            public int nWorkTypeID;
            public string strSiteGUID;
        
        }
        public Get_Out End(string data)
        {
            Get_Out OutResult = new Get_Out();
            Get_In_setToWorkOut input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In_setToWorkOut>(data);
            try
            {
                DBStep db = new DBStep();

                if (input.nWorkTypeID == 1)
                {
                    db.UpdateToYiChuQin(input.strTrainPlanGUID, input.dtConfirmTime, input.strConfirmBrief, input.strUserName, input.strUserNumber, input.nWorkTypeID);
                }
                else if (input.nWorkTypeID == 2)
                {
                    db.UpdateToYiTuiQin(input.strTrainPlanGUID, input.strSiteGUID, input.dtConfirmTime, input.strConfirmBrief, input.strUserName, input.strUserNumber, 2);
                }

                OutResult.result = "0";
                OutResult.resultStr = "执行成功！";
            }
            catch (Exception ex)
            {
                OutResult.result = "1";
                OutResult.resultStr = "提交失败：" + ex.Message;
            }
            return OutResult;
        }

        #endregion

        #region  判断步骤是否执行完毕

        public class Get_In_CheckIsFinished
        {
            public string strTrainPlanGUID;
            public string strWorkShopGUID;
            public int nWorkTypeID;
        }

        public class Get_Out_CheckIsFinished
        {
            public bool IsFinished;
        }
        public Get_Out CheckIsFinished(string data)
        {
            Get_Out OutResult = new Get_Out();
            Get_In_CheckIsFinished input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In_CheckIsFinished>(data);
            try
            {
                DBStep db = new DBStep();
                Get_Out_CheckIsFinished IsFinished = new Get_Out_CheckIsFinished();
                IsFinished.IsFinished = db.CheckIsFinished(input.strTrainPlanGUID, input.strWorkShopGUID,input.nWorkTypeID);
                OutResult.data = IsFinished;
                OutResult.result = "0";
                OutResult.resultStr = "执行成功！";
            }
            catch (Exception ex)
            {
                OutResult.result = "1";
                OutResult.resultStr = "提交失败：" + ex.Message;
            }
            return OutResult;
        }


        #endregion

        #region 获取标准步骤列表
        public class Get_InStepDefList
        {
            public int nWorkTypeID { get; set; }
            public string strWorkShopGUID { get; set; }
          
        }
        public class Get_OutStepDefList
        {
            public int result;
            public string resultStr = "";
            public List<MDstepDef> data;
        }
        public Get_OutStepDefList GetStepDefList(string data)
        {
            Get_OutStepDefList json = new Get_OutStepDefList();
            try
            {
                Get_InStepDefList input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InStepDefList>(data);
                DBStepDef db = new DBStepDef();
                List<MDstepDef> StepDefList = db.GetStepDefList(input.strWorkShopGUID, input.nWorkTypeID);
                if (StepDefList != null && StepDefList.Count > 0)
                {
                    json.data = StepDefList;
                    json.result = 0;
                    json.resultStr = "获取成功";
                }
            }
            catch (Exception ex)
            {
                json.result = 1;
                json.resultStr = "获取成功：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 设置是否为必要步骤
        public class Get_InSetIsNecessary
        {
            public int nID;
            public int nIsNecessary;
        }
        public class Get_OutSetIsNecessary
        {
            public int result;
            public string resultStr = "";
            public object data;
        }
        public Get_OutSetIsNecessary SetToIsNecessary(string data)
        {
            Get_OutSetIsNecessary json = new Get_OutSetIsNecessary();
            try
            {
                Get_InSetIsNecessary input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InSetIsNecessary>(data);
                DBStepDef db = new DBStepDef();
                bool i = db.SetIsNecessary(input.nID, input.nIsNecessary);
                if (i)
                {
                    json.result = 0;
                    json.resultStr = "返回成功";
                }
                else
                {
                    json.result = 0;
                    json.resultStr = "更新0条数据";
                }

            }
            catch (Exception ex)
            {
                json.result = 1;
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取未确认计划列表
        public class Get_InNoConfirm
        {
            public int nWorkTypeID { get; set; }
            public string strWorkShopGUID { get; set; }

        }
        public class Get_OutNoConfirmList
        {
            public int result;
            public string resultStr = "";
            public List<MDNoConfirmPlan> data;
        }
        public Get_OutNoConfirmList GetNoConfirmList(string data)
        {
            Get_OutNoConfirmList json = new Get_OutNoConfirmList();
            try
            {
                Get_InNoConfirm input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InNoConfirm>(data);
                DBNoConfirm db = new DBNoConfirm();
                List<MDNoConfirmPlan> StepDefList = db.GetNoConfirmList(input.strWorkShopGUID, input.nWorkTypeID);
                json.data = StepDefList;
                json.result = 0;
                json.resultStr = "获取成功";
            }
            catch (Exception ex)
            {
                json.result = 1;
                json.resultStr = "获取失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 记录各车间流程模式设置记录
        public class Get_InFlowSetRecord
        {
            public string WorkShopGUID;
            public string ModeTypeID;
            public string FlowType;
            public string BeginTime;
            public string EndTime;
            public string ModeBrief;
            public List<FlowSetRecord_Detail> FlowSetRecord_Detail;
        }
        public class  FlowSetRecord_Detail
        {
            public string strStepID;
        }

        public class Get_OutFlowSetRecord
        {
            public int result;
            public string resultStr = "";
            public object data;
        }
        public Get_OutFlowSetRecord FlowSettingRecord(string data)
        {
            Get_OutFlowSetRecord json = new Get_OutFlowSetRecord();
            #region 情况特殊，如果直接点击测试按钮，响应出测试数据
            if (data == "{}")
            {
                json.result = 1;
                json.resultStr = "提交失败，提交json数据不能为空，测试数据在{}中";
                Get_InFlowSetRecord OTiShi = new Get_InFlowSetRecord();
                OTiShi.BeginTime = "2016-10-01 17:15:21.000";
                OTiShi.EndTime = "2016-11-01 17:15:21.000";
               
                OTiShi.FlowType = "1";
                OTiShi.ModeBrief = "ceshi";
                OTiShi.ModeTypeID = "1";
                OTiShi.WorkShopGUID = "3b50bf66-dabb-48c0-8b6d-05db80591090";
                List<FlowSetRecord_Detail> lod = new List<FlowSetRecord_Detail>();
                FlowSetRecord_Detail od1 = new FlowSetRecord_Detail();
                od1.strStepID = "RS.STEP.PUBJIESHI.READ";
                lod.Add(od1);
                FlowSetRecord_Detail od2 = new FlowSetRecord_Detail();
                od2.strStepID = "RS.STEP.DRINKTEST";
                lod.Add(od2);
                OTiShi.FlowSetRecord_Detail = lod;
                json.data = OTiShi;
                return json;
            }

            #endregion
            Get_InFlowSetRecord input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InFlowSetRecord>(data);
            DBSetRecord db = new DBSetRecord();
            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    string strGUID = Guid.NewGuid().ToString();
                    db.AddMain(input, strGUID, trans);
                    foreach (FlowSetRecord_Detail item in input.FlowSetRecord_Detail)
                        db.AddChild(item, strGUID, trans);
                    trans.Commit();
                    json.result = 0;
                    json.resultStr = "执行成功！";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    json.result = 1;
                    json.resultStr = "提交失败：" + ex.Message;
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                }
            }
            return json;
        }
        #endregion


    }

    
}