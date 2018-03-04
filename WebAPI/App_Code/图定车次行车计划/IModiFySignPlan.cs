using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;


/// <summary>
///IModiFySignPlan 的摘要说明
/// </summary>
public class IModiFySignPlan : IQueryResult
{
	public IModiFySignPlan()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    private class AddSignPlan
    {
        public string SignPlanGUID;

    }
    private class JsonModel
    {
        public int result;
        public string resultStr;
        public AddSignPlan data;
    }

    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();
        TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
        try
        {
            TF.RunSafty.Model.InterfaceModel.PlanSigns paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.InterfaceModel.PlanSigns>(this.Data);
            //验证数据正确性,非空字段不能为空
            if (validater.IsNotNullPropertiesValidated(paramModel))
            {
                TF.RunSafty.BLL.AddPlanSign bllSignPlan = new TF.RunSafty.BLL.AddPlanSign();
                TF.RunSafty.Model.TAB_Plan_Rest PlanSign = new TF.RunSafty.Model.TAB_Plan_Rest();
                AddSignPlan t = new AddSignPlan();
                if (bllSignPlan.EditParamModel(paramModel.data.SignPlan, PlanSign) != 0)
                {
                    t.SignPlanGUID = PlanSign.strGUID;
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                    jsonModel.data = t;
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