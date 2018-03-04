using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///IGetCallWork 的摘要说明
/// </summary>
public class IGetCallWork : IQueryResult
{
    public IGetCallWork()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public class CallWork
    {
        public nState data;
        public string cid;

    }
    public class nState
    {
        public string nStartState;
        public string nEndState;
    }



    private class JsonModel
    {
        public int result;
        public string resultStr;
        public object data;
    }

    private class ArrayName
    {
        public object MsgAry;
    }


    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();
        try
        {
            CallWork paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<CallWork>(this.Data);
            TF.RunSafty.BLL.TAB_MsgCallWork bllPlace = new TF.RunSafty.BLL.TAB_MsgCallWork();
            List<TF.RunSafty.Model.TAB_MsgCallWork> placeList = bllPlace.GetAllMsg(paramModel.data.nStartState, paramModel.data.nEndState);
            jsonModel.result = 0;
            jsonModel.resultStr = "提交成功";
            ArrayName an = new ArrayName();
            jsonModel.data = an;
            an.MsgAry = bllPlace.GetPlaceList(placeList);
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