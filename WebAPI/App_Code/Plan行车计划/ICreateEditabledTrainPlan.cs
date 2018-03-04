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
using TF.RunSafty.Model.InterfaceModel;
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///ICreateEditabledTrainPlan 的摘要说明
    /// </summary>
    public class ICreateEditabledTrainPlan : IQueryResult
    {
        public ICreateEditabledTrainPlan()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private class ParamModel : ParamBase
        {
            public PlanData data;
        }
        public class PlanData
        {
            public TF.RunSafty.Model.InterfaceModel.CreatedTrainPlan trainPlan;
            public User user;
            public Site site;
        }
        public class User
        {
            public string userID = "";
            public string userName = "";
        }
        public class Site
        {
            public string siteID = "";
            public string siteName = "";
        }
        public class PlanInfo
        {
            public string planID = "";
        }
        public class JsonModel : ResultJsonBase
        {
            public PlanInfo data = new PlanInfo();
        }

        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            TF.RunSafty.BLL.Plan.TAB_Plan_Train bllPlan = new TF.RunSafty.BLL.Plan.TAB_Plan_Train();
            TF.RunSafty.Model.TAB_Plan_Train plan = new TF.RunSafty.Model.TAB_Plan_Train();
            try
            {
                DateTime dtStartTime;
                if (DateTime.TryParse(param.data.trainPlan.startTime, out dtStartTime))
                {
                    plan.dtStartTime = dtStartTime;
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.dragTypeID))
                {
                    plan.nDragType = int.Parse(param.data.trainPlan.dragTypeID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.kehuoID))
                {
                    plan.nKehuoID = int.Parse(param.data.trainPlan.kehuoID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.planTypeID))
                {
                    plan.nPlanType = int.Parse(param.data.trainPlan.planTypeID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.remarkTypeID))
                {
                    plan.nRemarkType = int.Parse(param.data.trainPlan.remarkTypeID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.trainmanTypeID))
                {
                    plan.nTrainmanTypeID = int.Parse(param.data.trainPlan.trainmanTypeID);
                }
                plan.strCreateSiteGUID = param.data.site.siteID;
                plan.strCreateUserGUID = param.data.user.userID;
                plan.strPlaceID = param.data.trainPlan.placeID;
                plan.strStartStation = param.data.trainPlan.startStationID;
                plan.strEndStation = param.data.trainPlan.endStationID;
                plan.strTrainJiaoluGUID = param.data.trainPlan.trainjiaoluID;
                plan.strTrainNo = param.data.trainPlan.trainNo;
                plan.strTrainNumber = param.data.trainPlan.trainNumber;
                plan.strTrainTypeName = param.data.trainPlan.trainTypeName;
                plan.dtCreateTime = DateTime.Now;
                plan.dtRealStartTime = plan.dtStartTime;
                string planGUID = Guid.NewGuid().ToString();
                plan.strTrainPlanGUID = planGUID;
                plan.nPlanState = (int)TRsPlanState.psEdit;
                if (!string.IsNullOrEmpty(param.data.trainPlan.kaiCheTime))
                {
                    DateTime dtChuqinTime;
                    if (DateTime.TryParse(param.data.trainPlan.kaiCheTime, out dtChuqinTime))
                    {
                        plan.dtChuQinTime = dtChuqinTime;
                    }
                }

                if (bllPlan.Add(plan) > 0)
                {
                    jsonModel.data.planID = planGUID;
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");

            }

            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter).Replace(":null", ":\"\"");
            return result;
        }
    }
}