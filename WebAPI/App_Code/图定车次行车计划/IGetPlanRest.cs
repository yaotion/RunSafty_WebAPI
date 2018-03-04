using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///IGetPlanRest 的摘要说明
/// </summary>
public class IGetPlanRest : IQueryResult
{
    public IGetPlanRest()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    private class IGetPlanRestData
    {
        public string dtStartTime;
        public string dtEndTime;
        public string strJiaoluGUID;
    }
    private class ParamModel
    {
        public string cid;
        public IGetPlanRestData data;
    }



    private class JsonModel
    {
        public int result;
        public string resultStr;
        public planArrays data;

    }


    private class planArrays
    {
        public string strTrainJiaoLuGUID; 
        public string strCurPlanGUID;
        public object planArray;
    }


    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();
        try
        {
            ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            TF.RunSafty.BLL.Tab_Plan_Rest bllPlace = new TF.RunSafty.BLL.Tab_Plan_Rest();
            List<TF.RunSafty.Model.TAB_Plan_Rest> placeList = bllPlace.GetPlanTrain(paramModel.data.dtStartTime, paramModel.data.dtEndTime, paramModel.data.strJiaoluGUID);
            jsonModel.result = 0;
            jsonModel.resultStr = "提交成功";
            planArrays p = new planArrays();
            jsonModel.data = p;
            p.strTrainJiaoLuGUID = paramModel.data.strJiaoluGUID;
            p.strCurPlanGUID = "";
            p.planArray = bllPlace.GetPlaceList(placeList);
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