using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///IPlan_OutRoom 的摘要说明
/// </summary>
public class IPlan_OutRoom : IQueryResult
{
	public IPlan_OutRoom()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}



    private class PlanInOutRoom
    {
        public string PlanOutRoomID;

    }


    private class JsonModel
    {
        public int result;
        public string resultStr;
        public PlanInOutRoom data;
    }

    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();
        TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
        try
        {
            TF.RunSafty.Model.InterfaceModel.PlanOutRoom paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.InterfaceModel.PlanOutRoom>(this.Data);
            //验证数据正确性,非空字段不能为空
            if (validater.IsNotNullPropertiesValidated(paramModel))
            {
                TF.RunSafty.BLL.TAB_Plan_OutRoom bllPlanInOutRoom = new TF.RunSafty.BLL.TAB_Plan_OutRoom();
                TF.RunSafty.Model.Model_Plan_OutRoom PlanOutRoom = new TF.RunSafty.Model.Model_Plan_OutRoom();
                PlanInOutRoom t = new PlanInOutRoom();
                if (bllPlanInOutRoom.AddByParamModel(paramModel, PlanOutRoom))
                {
                    t.PlanOutRoomID = PlanOutRoom.strOutRoomGUID;
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

