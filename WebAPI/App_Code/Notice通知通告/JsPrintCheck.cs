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
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///JsPrintCheck 的摘要说明
    /// </summary>
    public class JsPrintCheck : IQueryResult
    {
        public JsPrintCheck()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private class ParamModel
        {
            public string cid;
            public string strPlanGUID;
            public string strTrainmanGUID;
            public string dtBeginWorkTime;
        }

        private class JsonModel
        {
            public string result;
            public string returnStr;
            public string bPrint;
        }


        public override string QueryResult()
        {
            JsonModel model = new JsonModel();
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            TF.RunSafty.BLL.Tab_DeliverJSPrint bllPrint = new TF.RunSafty.BLL.Tab_DeliverJSPrint();
            TF.RunSafty.Model.Tab_DeliverJSPrint modelPlan = new TF.RunSafty.Model.Tab_DeliverJSPrint();
            try
            {
                int bPrint = bllPrint.IsJsPrintable(param.strPlanGUID, param.strTrainmanGUID, param.dtBeginWorkTime);
                modelPlan.StrPlanGUID = param.strPlanGUID;
                modelPlan.StrSiteGUID = this.cid;
                modelPlan.StrTrainmanGUID = param.strTrainmanGUID;
                model.result = "0";
                model.returnStr = "返回成功";
                model.bPrint = bPrint.ToString();

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = "提交失败" + ex.Message;
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return result;
        }
    }
}
