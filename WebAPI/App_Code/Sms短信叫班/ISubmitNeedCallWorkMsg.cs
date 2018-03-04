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
    ///ISubmitNeedCallWorkMsg 的摘要说明
    /// </summary>
    public class ISubmitNeedCallWorkMsg : IQueryResult
    {
        private class pCallWork : ParamBase
        {
            public pMsg data;
        }
        public class pMsg
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
            [NotNull]
            public string eCallType { get; set; }
            [NotNull]
            public string dtSendTime { get; set; }
            [NotNull]
            public string strSendUser { get; set; }
            [NotNull]
            public string strRecvUser { get; set; }
            [NotNull]
            public string dtRecvTime { get; set; }

        }

        private class JsonModel
        {
            public int result;
            public string resultStr;
        }

        public ISubmitNeedCallWorkMsg()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            TF.RunSafty.BLL.TAB_MsgCallWork bllCallWork = new TF.RunSafty.BLL.TAB_MsgCallWork();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                pCallWork paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<pCallWork>(this.Data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {

                    TF.RunSafty.Model.TAB_MsgCallWork work = new TAB_MsgCallWork();

                    work.strTrainmanGUID = paramModel.data.strTrainmanGUID;
                    work.strPlanGUID = paramModel.data.strPlanGUID;
                    work.strSendMsgContent = paramModel.data.strMsgContent;
                    work.dtCallTime = DateTime.Parse(paramModel.data.dtCreateTime);
                    work.strMsgGUID = paramModel.data.strMsgGUID;
                    work.eCallType = paramModel.data.eCallType;
                    work.strRecvUser = paramModel.data.strRecvUser;
                    work.dtRecvTime = paramModel.data.dtRecvTime;
                    work.strSendUser = paramModel.data.strSendUser;
                    work.dtSendTime = paramModel.data.dtSendTime;


                    work.nCallTimes = 0;
                    work.nRecvCount = 0;
                    work.nSendCount = 0;
                    work.eCallState = 1;
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
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
            return result;
        }
    }
}
