using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.Api.Utilities;
using TF.RunSafty.Model.InterfaceModel;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///IGetWaitWorkSignPlan 的摘要说明
/// </summary>
public class IGetWaitWorkSignPlan : IQueryResult
{
    public IGetWaitWorkSignPlan()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    private class JiaoLuData
    {
        public string strPlanGUID;
    }
    private class ParamModel
    {
        public string cid;
        public JiaoLuData data;
    }

    private class JsonModel
    {
        public int result;
        public string resultStr;
        public object data;
    }

    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();
        try
        {
            ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            TF.RunSafty.BLL.getAllSign bllPlace = new TF.RunSafty.BLL.getAllSign();
            List<TF.RunSafty.Model.Model_Plan_ToBeTake> plans = bllPlace.GetPlanTrain(paramModel.data.strPlanGUID);
            jsonModel.result = 0;
            jsonModel.resultStr = "提交成功";
            //jsonModel.data = bllPlace.GetPlaceList(placeList);

            if (plans.Count > 0)
            {
                PlanBeginWork work = new PlanBeginWork();
                work.strCheCi = plans[0].StrTrainNo;
                work.dtCallWorkTime = plans[0].dtCallWorkTime.ToString();
                work.dtWaitWorkTime = plans[0].dtWaitWorkTime.ToString();

                work.strTrainmanGUID1 = plans[0].strTrainmanGUID1;
                work.strTrainmanGUID2 = plans[0].strTrainmanGUID2;
                work.strTrainmanGUID3 = plans[0].strTrainmanGUID3;
                work.strTrainmanGUID4 = plans[0].strTrainmanGUID4;

                work.strTrainmanName1 = plans[0].strTrainmanName1;
                work.strTrainmanName2 = plans[0].strTrainmanName2;
                work.strTrainmanName3 = plans[0].strTrainmanName3;
                work.strTrainmanName4 = plans[0].strTrainmanName4;

                work.strTrainmanNumber1 = plans[0].strTrainmanNumber1;
                work.strTrainmanNumber2 = plans[0].strTrainmanNumber2;
                work.strTrainmanNumber3 = plans[0].strTrainmanNumber3;
                work.strTrainmanNumber4 = plans[0].strTrainmanNumber4;


                work.NNeedRest = plans[0].NNeedRest;
                work.NPlanState = plans[0].NPlanState;

                jsonModel.data = work;
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
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
        timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        return Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
    }
}
