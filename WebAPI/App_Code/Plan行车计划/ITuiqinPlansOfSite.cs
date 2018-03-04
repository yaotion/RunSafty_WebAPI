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
using TF.Api.Utilities;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using System.Collections.Generic;


namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///ITuiqinPlansOfSite 的摘要说明
    /// </summary>
    public class ITuiqinPlansOfSite : IQueryResult
    {
        public ITuiqinPlansOfSite()
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
            public string begintime { get; set; }
            [NotNull]
            public string endtime { get; set; }
            public int showAll { get; set; }
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<mTuiqinPlansOfSite> data;
        }

        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            //TF.RunSafty.BLL.VIEW_Plan_EndWork bllPlan = new TF.RunSafty.BLL.VIEW_Plan_EndWork();
            TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                pTrainjiaoluPlan paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<pTrainjiaoluPlan>(this.Data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    string starttime = paramModel.data.begintime;
                    string endtime = paramModel.data.endtime;
                    string clientID = paramModel.data.siteID;
                    int isAll = paramModel.data.showAll;
                    List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> plans = bllPlan.GetTuiqinPlansOfSite(clientID, starttime, endtime, isAll);
                    jsonModel.data = plans;
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