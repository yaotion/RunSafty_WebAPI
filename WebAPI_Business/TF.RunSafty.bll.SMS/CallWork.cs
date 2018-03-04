using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Api.Utilities;
using TF.RunSafty.Model.InterfaceModel;
namespace TF.RunSafty.SMS
{
    public class CallWork
    {
        #region 取消叫班
         
        public class Cancel_In
        {
            [NotNull]
            public string strPlanGUID { get; set; }
            [NotNull]
            public string strTrainmanGUID { get; set; }
        }

        private class Cancel_Out
        {
            public int result;
            public string resultStr;
        }

        private Cancel_Out CancelCallWork(string input)
        {
            Cancel_Out jsonModel = new Cancel_Out();
            TF.RunSafty.BLL.TAB_MsgCallWork bllCallWork = new TF.RunSafty.BLL.TAB_MsgCallWork();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                Cancel_In paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Cancel_In>(input);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel))
                {

                    bllCallWork.Delete(paramModel.strPlanGUID, paramModel.strTrainmanGUID);

                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
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

        #region 提交短信叫班
        private class Submit_In 
        {
            public List<pMsg> data;
        }
        public class pMsg
        {
            [NotNull]
            public string strGUID { get; set; }
            [NotNull]
            public string strReceiverPhone { get; set; }
            [NotNull]
            public string strSenderPhone { get; set; }
            [NotNull]
            public string dtSendTime { get; set; }
            [NotNull]
            public string nResult { get; set; }
        }
        private class Submit_Out
        {
            public int result;
            public string resultStr;
        }
        private Submit_Out SubmitCallWork(string input)
        {
            Submit_Out jsonModel = new Submit_Out();
            TF.RunSafty.BLL.TAB_MsgCallWork bllCallWork = new TF.RunSafty.BLL.TAB_MsgCallWork();
            TF.RunSafty.BLL.TAB_MsgCallWork_Record bllRecord = new TF.RunSafty.BLL.TAB_MsgCallWork_Record();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                Submit_In paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Submit_In>(input);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    foreach (pMsg msg in paramModel.data)
                    {
                        TF.RunSafty.Model.TAB_MsgCallWork_Record record = bllRecord.GetModelByGUID(msg.strGUID);
                        if (record != null)
                        {
                            record.strReceiverPhone = msg.strReceiverPhone;
                            record.strSenderPhone = msg.strSenderPhone;
                            record.dtTime = DateTime.Parse(msg.dtSendTime);
                            record.nResult = int.Parse(msg.nResult);
                            bllRecord.Update(record);
                        }
                        else
                        {
                            record.strReceiverPhone = msg.strReceiverPhone;
                            record.strSenderPhone = msg.strSenderPhone;
                            record.dtTime = DateTime.Parse(msg.dtSendTime);
                            record.nResult = int.Parse(msg.nResult);
                            bllRecord.Add(record);
                        }
                        //更新叫班次数
                        TF.RunSafty.Model.TAB_MsgCallWork work = bllCallWork.GetModelByGUID(msg.strGUID);
                        if (work != null)
                        {
                            work.dtCallTime = DateTime.Parse(msg.dtSendTime);
                            work.nCallTimes = work.nCallTimes + 1;
                            bllCallWork.Update(work);
                        }
                    }
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
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

        #region 提交短信回执

        private class Receipt_In 
        {
            public List<pMsgContent> data;
        }
        public class pMsgContent
        {
            [NotNull]
            public string strReceiverPhone { get; set; }
            [NotNull]
            public string strSenderPhone { get; set; }
            [NotNull]
            public string dtRecvTime { get; set; }
            [NotNull]
            public string strContent { get; set; }
        }
        public class Receipt_Out
        {
            public int result;
            public string resultStr;
        }
        public Receipt_Out SubmitReceipt(string input)
        {
            //根据电话号码找到乘务员的guid,然后根据trainmanguid和receivetime匹配TAB_MsgCallWork 最近的一次记录

            Receipt_Out jsonModel = new Receipt_Out();
            TF.RunSafty.BLL.TAB_MsgCallWork bllCallWork = new TF.RunSafty.BLL.TAB_MsgCallWork();
            TF.RunSafty.BLL.TAB_MsgCallWork_Record bllRecord = new TF.RunSafty.BLL.TAB_MsgCallWork_Record();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                Receipt_In paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Receipt_In>(input);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    foreach (pMsgContent msg in paramModel.data)
                    {
                        TF.RunSafty.Model.TAB_MsgCallWork work = bllCallWork.GetModelByTelephone(msg.strSenderPhone, msg.dtRecvTime);
                        if (work != null)
                        {
                            TF.RunSafty.Model.TAB_MsgCallWork_Record record = new TF.RunSafty.Model.TAB_MsgCallWork_Record();
                            record.strGUID = Guid.NewGuid().ToString();
                            record.strMsgContent = msg.strContent;
                            record.strSenderPhone = msg.strSenderPhone;
                            record.strReceiverPhone = msg.strReceiverPhone;
                            //record.strCallWorkGUID = work.strGUID;
                            record.dtTime = DateTime.Parse(msg.dtRecvTime);
                            record.nType = 0;
                            record.nResult = 1;
                            bllRecord.Add(record);
                            //更新叫班表的叫班状态、接收时间、接收次数
                            //work.DtRecvMsgTime = DateTime.Parse(msg.dtRecvTime);
                            work.nRecvCount = work.nRecvCount + 1;
                            //work.nState = (int)TRsCallWorkState.cwsRecv;
                            bllCallWork.Update(work);
                        }
                    }
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
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

        #region 提交需要叫班的短信

        private class NeedCallWork_In 
        {
            public CallWorkMsg data;
        }
        public class CallWorkMsg
        {
            [NotNull]
            public string strMsgGUID { get; set; }
            [NotNull]
            public string strPlanGUID { get; set; }
            [NotNull]
            public string strMsgContent { get; set; }
            [NotNull]
            public string dtCreateTime { get; set; }            
            [NotNull]
            public string strTrainmanGUID { get; set; }
            [NotNull]
            public string strTrainmanNumber { get; set; }
            [NotNull]
            public string strTrainmanName { get; set; }
            [NotNull]
            public string strMobileNumber { get; set; }
        }

        public class NeedCallWork_Out
        {
            public int result;
            public string resultStr; 
        }

        public NeedCallWork_Out SubmitNeededCallWork(string input)
        {
            NeedCallWork_Out jsonModel = new NeedCallWork_Out();
            TF.RunSafty.BLL.TAB_MsgCallWork bllCallWork = new TF.RunSafty.BLL.TAB_MsgCallWork();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                NeedCallWork_In paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<NeedCallWork_In>(input);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {

                    TF.RunSafty.Model.TAB_MsgCallWork work = new TF.RunSafty.Model.TAB_MsgCallWork();

                    work.strTrainmanGUID = paramModel.data.strTrainmanGUID;
                    work.strPlanGUID = paramModel.data.strPlanGUID;
                    work.strSendMsgContent = paramModel.data.strMsgContent;
                    work.dtCallTime = DateTime.Parse(paramModel.data.dtCreateTime);
                    //work.strGUID = "";//paramModel.data.strMsgGUID;
                    work.nCallTimes = 0;
                    work.nRecvCount = 0;
                    work.nSendCount = 0;
                    //work.nState = 1;                   
                    bllCallWork.Add(work);
                    
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
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
    }
}
