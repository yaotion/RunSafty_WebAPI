using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;
using TF.RunSafty.Model.InterfaceModel;
using TF.Api.Utilities;

/// <summary>
///IPlan_BeginWork 的摘要说明
/// </summary>
public class IPlan_BeginWork : IQueryResult
{
    public IPlan_BeginWork()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    private class pPlan_BeginWork : ParamBase
    {
        [NotNull]
        public string strPlanGUID { get; set; }
    }


    private class JsonModel
    {
        public int result;
        public string resultStr;
        public PlanBeginWork data;
    }

    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();     
        TF.RunSafty.BLL.Plan_BeginWork pb = new TF.RunSafty.BLL.Plan_BeginWork();
        TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
        try
        {
            pPlan_BeginWork paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<pPlan_BeginWork>(this.Data);
            //验证数据正确性,非空字段不能为空
            if (validater.IsNotNullPropertiesValidated(paramModel))
            {
                string clientID = paramModel.strPlanGUID;
                List<TF.RunSafty.Model.Model_Plan_ToBeTake> plans = pb.GetPlan_BeginWork(clientID);
                if (plans.Count > 0)
                {
                    PlanBeginWork work=new PlanBeginWork();
                    work.strCheCi=plans[0].StrTrainNo;
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