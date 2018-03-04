using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///IDelSignPlan 的摘要说明
/// </summary>
public class IDelSignPlan : IQueryResult
{
	public IDelSignPlan()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
    }

    public class DelSinPlan
    {
        public string nid;
        public dataGUID data;

    
    }

    public class dataGUID
    {
        public string strGUID;
    }



    //输出
    private class strSignPlans
    {
        public string strSignPlan;

    }
    private class JsonModel
    {
        public int result;
        public string resultStr;
        public strSignPlans data;
    }

    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();
        TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
        try
        {
            DelSinPlan paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DelSinPlan>(this.Data);
          
                TF.RunSafty.BLL.AddPlanSign bllSignPlan = new TF.RunSafty.BLL.AddPlanSign();
                TF.RunSafty.Model.TAB_Plan_Rest PlanSign = new TF.RunSafty.Model.TAB_Plan_Rest();
                strSignPlans t = new strSignPlans();
                if (bllSignPlan.DelParamModel(paramModel.data.strGUID) != 0)
                {
                    t.strSignPlan = "删除成功";
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                    jsonModel.data = t;
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