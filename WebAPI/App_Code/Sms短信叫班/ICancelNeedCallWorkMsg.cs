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
    ///ICancelNeedCallWorkMsg 的摘要说明
    /// </summary>
    public class ICancelNeedCallWorkMsg : IQueryResult
    {
        public ICancelNeedCallWorkMsg()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private class InputData : ParamBase
        {
            public InputParam data;
        }
        public class InputParam
        {
            [NotNull]
            public string strPlanGUID { get; set; }
            [NotNull]
            public string strTrainmanGUID { get; set; }
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
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                InputData paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InputData>(this.Data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {

                    bllCallWork.Delete(paramModel.data.strPlanGUID, paramModel.data.strTrainmanGUID);

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
