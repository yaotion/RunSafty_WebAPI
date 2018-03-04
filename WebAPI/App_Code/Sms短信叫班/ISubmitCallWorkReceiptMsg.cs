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
    ///ISubmitCallWorkReceiptMsg 的摘要说明
    /// </summary>
    public class ISubmitCallWorkReceiptMsg : IQueryResult
    {
        public ISubmitCallWorkReceiptMsg()
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
            public string strReceiverPhone { get; set; }
            [NotNull]
            public string strSenderPhone { get; set; }
            [NotNull]
            public string dtRecvTime { get; set; }
            [NotNull]
            public string strContent { get; set; }
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
        }
        public override string QueryResult()
        {
            //根据电话号码找到乘务员的guid,然后根据trainmanguid和receivetime匹配TAB_MsgCallWork 最近的一次记录

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
                        TF.RunSafty.Model.TAB_MsgCallWork work = bllCallWork.GetModelByTelephone(msg.strSenderPhone, msg.dtRecvTime);
                        if (work != null)
                        {
                            TF.RunSafty.Model.TAB_MsgCallWork_Record record = new TAB_MsgCallWork_Record();
                            record.strGUID = Guid.NewGuid().ToString();
                            record.strMsgContent = msg.strContent;
                            record.strSenderPhone = msg.strSenderPhone;
                            record.strReceiverPhone = msg.strReceiverPhone;
                            record.strCallWorkGUID = work.strMsgGUID;
                            record.dtTime = DateTime.Parse(msg.dtRecvTime);
                            record.nType = 0;
                            record.nResult = 1;
                            bllRecord.Add(record);
                            //更新叫班表的叫班状态、接收时间、接收次数
                            work.dtRecvTime = msg.dtRecvTime;
                            work.nRecvCount = work.nRecvCount + 1;
                            work.eCallState = (int)TRsCallWorkState.cwsRecv;
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