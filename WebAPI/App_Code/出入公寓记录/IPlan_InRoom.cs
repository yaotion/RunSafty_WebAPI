using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///IPlan_InOutRoom 的摘要说明
/// </summary>
public class IPlan_InRoom : IQueryResult
{
	public IPlan_InRoom()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}



    private class PlanInOutRoom
    {
        public string PlanInRoomID;

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
            TF.RunSafty.Model.InterfaceModel.PlanInRoom paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.InterfaceModel.PlanInRoom>(this.Data);
            //验证数据正确性,非空字段不能为空
            if (validater.IsNotNullPropertiesValidated(paramModel))
            {
                TF.RunSafty.BLL.TAB_Plan_InRoom bllPlanInOutRoom = new TF.RunSafty.BLL.TAB_Plan_InRoom();
                TF.RunSafty.Model.Model_Plan_InRoom PlanInOutRoom = new TF.RunSafty.Model.Model_Plan_InRoom();
                PlanInOutRoom t = new PlanInOutRoom();
                if (bllPlanInOutRoom.AddByParamModel(paramModel, PlanInOutRoom))
                {
                    t.PlanInRoomID = PlanInOutRoom.strInRoomGUID;
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

