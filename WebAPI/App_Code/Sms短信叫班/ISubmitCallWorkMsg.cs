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
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using System.Collections.Generic;
using TF.Api.Utilities;

namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///ISubmitCallWorkMsg 的摘要说明
    /// </summary>
    public class ISubmitCallWorkMsg : IQueryResult
    {
        public ISubmitCallWorkMsg()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private class pCallWork : ParamBase
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
        private class JsonModel
        {
            public int result;
            public string resultStr;
        }
        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            TF.RunSafty.BLL.TAB_MsgCallWork bllCallWork = new TF.RunSafty.BLL.TAB_MsgCallWork();
            TF.RunSafty.BLL.TAB_MsgCallWork_Record bllRecord = new TF.RunSafty.BLL.TAB_MsgCallWork_Record();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                pCallWork paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<pCallWork>(this.Data);
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
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
            return result;
        }
    }
}
