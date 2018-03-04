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
    ///ITrainmanPlansOfSite 的摘要说明
    /// </summary>
    public class ITrainmanPlansOfSite : IQueryResult
    {
        public ITrainmanPlansOfSite()
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
            public string trainjiaoluID { get; set; }
            [NotNull]
            public string begintime { get; set; }
            [NotNull]
            public string endtime { get; set; }
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<PlansOfClient> data;
        }

        private List<PlansOfClient> GetPlans(List<TF.RunSafty.Model.VIEW_Plan_Trainman> vPlans)
        {
            List<PlansOfClient> plans = new List<PlansOfClient>();
            TF.RunSafty.Model.InterfaceModel.TrainPlan tPlan = null;
            TF.RunSafty.Model.InterfaceModel.PlansOfClient pClient = null;
            if (vPlans != null)
            {
                foreach (VIEW_Plan_Trainman plan in vPlans)
                {
                    tPlan = new TF.RunSafty.Model.InterfaceModel.TrainPlan();
                    tPlan.dragTypeID = plan.nDragType.ToString();
                    tPlan.dragTypeName = plan.nDragTypeName;
                    tPlan.endStationID = plan.strEndStation;
                    tPlan.endStationName = plan.strEndStationName;
                    tPlan.kehuoID = plan.nKehuoID.ToString();
                    tPlan.kehuoName = plan.strKehuoName;
                    tPlan.placeName = plan.strPlaceName;
                    tPlan.planStateID = plan.nPlanState.ToString();
                    tPlan.planStateName = plan.strStateName;

                    tPlan.planTypeID = plan.nPlanType.ToString();
                    tPlan.planTypeName = plan.strPlanTypeName;

                    tPlan.remarkTypeID = plan.nRemarkType.ToString();
                    tPlan.remarkTypeName = plan.strRemarkTypeName;

                    tPlan.startStationID = plan.strStartStation;
                    tPlan.startStationName = plan.strStartStationName;
                    if (plan.dtStartTime.HasValue)
                    {
                        tPlan.startTime = plan.dtStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    tPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    tPlan.trainmanTypeName = plan.strTrainmanTypeName;
                    tPlan.trainNo = plan.strTrainNo;
                    tPlan.trainNumber = plan.strTrainNumber;
                    tPlan.trainTypeName = plan.strTrainTypeName;
                    tPlan.planID = plan.strTrainPlanGUID;
                    tPlan.placeID = plan.strPlaceID;

                    tPlan.strWaiQinClientGUID = plan.strWaiQinClientGUID;
                    tPlan.strWaiQinClientNumber = plan.strWaiQinClientNumber;
                    tPlan.strWaiQinClientName = plan.strWaiQinClientName;

                    NameGroup group = new NameGroup();
                    group.groupID = plan.strGroupGUID;
                    group.trainman1 = new Trainman();
                    group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    group.trainman1.trainmanName = plan.strTrainmanName1;
                    group.trainman1.trainmanNumber = plan.strTrainmanNumber1;

                    group.trainman2 = new Trainman();
                    group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    group.trainman2.trainmanName = plan.strTrainmanName2;
                    group.trainman2.trainmanNumber = plan.strTrainmanNumber2;

                    group.trainman3 = new Trainman();
                    group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    group.trainman3.trainmanName = plan.strTrainmanName3;
                    group.trainman3.trainmanNumber = plan.strTrainmanNumber3;

                    group.trainman4 = new Trainman();
                    group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    group.trainman4.trainmanName = plan.strTrainmanName4;
                    group.trainman4.trainmanNumber = plan.strTrainmanNumber4;


                    if (plan.dtArriveTime.HasValue)
                    {
                        group.arriveTime = plan.dtArriveTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    group.groupID = plan.strGroupGUID;
                    group.place = new ChuqinPlace();
                    group.place.placeID = plan.strPlaceID;
                    group.place.placeName = plan.strPlaceName;
                    group.station = new Station();
                    group.trainPlanID = plan.strTrainPlanGUID;
                    pClient = new PlansOfClient();
                    pClient.trainPlan = tPlan;
                    pClient.group = group;

                    plans.Add(pClient);
                }
            }
            return plans;
        }
        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();

            TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                pTrainjiaoluPlan paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<pTrainjiaoluPlan>(this.Data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    string starttime = paramModel.data.begintime;
                    string endtime = paramModel.data.endtime;
                    string strTrainjiaoluGUID = paramModel.data.trainjiaoluID;
                    string clientID = paramModel.data.siteID;
                    jsonModel.data = bllPlan.GetPlans(starttime, endtime, strTrainjiaoluGUID);
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
