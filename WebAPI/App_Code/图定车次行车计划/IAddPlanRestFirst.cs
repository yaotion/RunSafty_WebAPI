using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSafty.Model.InterfaceModel;
using TF.RunSaftyAPI.App_Api.Public;


/// <summary>
///IAddPlanRestFirst 的摘要说明
/// </summary>
public class IAddPlanRestFirst : IQueryResult
{
	public IAddPlanRestFirst()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
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
        TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
        try
        {
            PlanRestModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanRestModel>(this.Data);
            //验证数据正确性,非空字段不能为空
            if (validater.IsNotNullPropertiesValidated(paramModel.data))
            {
                TF.RunSafty.BLL.Tab_Plan_Rest bllTrain = new TF.RunSafty.BLL.Tab_Plan_Rest();
                if (bllTrain.UpdateByParamModel(paramModel, "AddIndexes"))
                {
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
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
