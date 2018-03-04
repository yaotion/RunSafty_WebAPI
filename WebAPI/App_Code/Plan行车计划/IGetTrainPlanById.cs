using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;
using TF.RunSafty.Model.InterfaceModel;
using TF.Api.Utilities;

/// <summary>
///IGetTrainPlanById 的摘要说明
/// </summary>
public class IGetTrainPlanById: IQueryResult
{
	public IGetTrainPlanById()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}


    private class pTrainPlan : ParamBase
    {
        [NotNull]
        public string strPlanGUID { get; set; }
    }


    private class JsonModel
    {
        public int result;
        public string resultStr;
        public GetTrainPlanById data;
    }

    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();
        TF.RunSafty.BLL.VIEW_Plan_Trainman_TrainPlan pb = new TF.RunSafty.BLL.VIEW_Plan_Trainman_TrainPlan();
        TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
        try
        {
            pTrainPlan paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<pTrainPlan>(this.Data);
            //验证数据正确性,非空字段不能为空
            if (validater.IsNotNullPropertiesValidated(paramModel))
            {
                string clientID = paramModel.strPlanGUID;
                List<TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan> plans = pb.GetPlan_Trainman(clientID);
                if (plans.Count > 0)
                {
                    GetTrainPlanById work = new GetTrainPlanById();
                    work.dtCallTime = plans[0].dtCallTime;
                    work.dtChuqinTime = plans[0].dtChuqinTime.ToString();
                    work.dtStartTime = plans[0].dtStartTime.ToString();
                    work.strTrainNo = plans[0].strTrainNo.ToString();

                    work.strStartStation = plans[0].strStartStation.ToString();
                    work.strTrainJiaoluGUID = plans[0].strTrainJiaoluGUID.ToString();
                    work.strWorkShopGUID = plans[0].strWorkShopGUID.ToString();

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

                    jsonModel.data = work;
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