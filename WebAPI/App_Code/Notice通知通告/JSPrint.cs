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
using System.Collections.Generic;
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///JSPrint 的摘要说明
    /// </summary>
    public class IJSPrint : IQueryResult
    {
        private class ParamModel
        {
            public string PlanGUID;
            public string TrainmanGUID;
            public string PrintTime;
        }

        private class JsonModel
        {
            public string result;
            public string returnStr;
        }
        public IJSPrint()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public override string QueryResult()
        {
            JsonModel model = new JsonModel();
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            TF.RunSafty.BLL.Tab_DeliverJSPrint bllPrint = new TF.RunSafty.BLL.Tab_DeliverJSPrint();
            TF.RunSafty.Model.Tab_DeliverJSPrint modelPlan = new TF.RunSafty.Model.Tab_DeliverJSPrint();
            try
            {
                modelPlan.StrPlanGUID = param.PlanGUID;
                modelPlan.StrSiteGUID = this.cid;
                modelPlan.StrTrainmanGUID = param.TrainmanGUID;
                DateTime dtPrint;
                if (DateTime.TryParse(param.PrintTime, out dtPrint))
                {
                    modelPlan.dtPrintTime = dtPrint;
                }
                if (bllPrint.Add(modelPlan) > 0)
                {
                    model.result = "0";
                    model.returnStr = "提交成功";
                }
                else
                {
                    model.result = "1";
                    model.returnStr = "提交失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = "提交失败";
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return result;
        }
    }
}