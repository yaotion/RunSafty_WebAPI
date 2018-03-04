using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.Api.Utilities;
using TF.RunSafty.Model.InterfaceModel;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///IEditTrainman 的摘要说明
/// </summary>
public class IEditTrainman : IQueryResult
{
	public IEditTrainman()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    private class EditTrainman : ParamBase
    {
        public TrainmanData data;

    }
    public class TrainmanData
    {
        [NotNull]
        public string strPlanGUID { get; set; }
        [NotNull]
        public string strNewTrainmanGUID { get; set; }
        [NotNull]
        public string strOldTrainmanGUID { get; set; }
        [NotNull]
        public string nTrainmanIndex { get; set; }
        [NotNull]
        public string strReason { get; set; }
        [NotNull]
        public string dtModifyTime { get; set; }
        [NotNull]
        public string strWorkGroupGUID { get; set; }
        [NotNull]
        public int ePlanState { get; set; }
      



      

    }
    private class JsonModel
    {
        public int result;
        public string resultStr;
        public object data = new object();
    }
    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();
        TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
        try
        {
            EditTrainman paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<EditTrainman>(this.Data);
            //验证数据正确性,非空字段不能为空
            if (validater.IsNotNullPropertiesValidated(paramModel.data))
            {
                TF.RunSafty.DAL.Tab_Plan_Rest dalTrain = new TF.RunSafty.DAL.Tab_Plan_Rest();
                if (dalTrain.UpdateTrainman(paramModel.data.strPlanGUID, paramModel.data.strNewTrainmanGUID, paramModel.data.strOldTrainmanGUID, paramModel.data.nTrainmanIndex, paramModel.data.strReason, paramModel.data.dtModifyTime, paramModel.data.strWorkGroupGUID, paramModel.data.ePlanState))
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
