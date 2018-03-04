using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;
using TF.RunSafty.Model.InterfaceModel;

/// <summary>
///IEditPlanRest 修改图定车次行车计划
/// </summary>
public class IEditPlanRest : IQueryResult
{
    public IEditPlanRest()
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
                if (bllTrain.UpdateByParamModel(paramModel, ""))
                {
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                }
                else
                {
                    jsonModel.result = 2;
                    jsonModel.resultStr = "返回失败";
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
