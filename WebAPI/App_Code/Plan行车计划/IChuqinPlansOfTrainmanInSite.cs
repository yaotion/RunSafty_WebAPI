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
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using TF.Api.Utilities;
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///IChuqinPlansOfTrainmanInSite 的摘要说明
    /// </summary>
    public class IChuqinPlansOfTrainmanInSite : IQueryResult
    {
        public IChuqinPlansOfTrainmanInSite()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private class pTrainjiaoluPlan : ParamBase
        {
            public Plan data;
        }
        public class Plan
        {
            [NotNull]
            public string siteID { get; set; }
            [NotNull]
            public string trainmanID { get; set; }
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public mChuqinPlansOfClient data;
        }

        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                pTrainjiaoluPlan paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<pTrainjiaoluPlan>(this.Data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    string clientID = paramModel.data.siteID;
                    string strTrainmanid = paramModel.data.trainmanID;
                    List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> plans = bllPlan.GetChuqinPlansOfTrainmanInSite(clientID, strTrainmanid);
                    if (plans.Count > 0)
                    {
                        jsonModel.data = plans[0];
                        jsonModel.result = 0;
                        jsonModel.resultStr = "提交成功";
                    }
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
