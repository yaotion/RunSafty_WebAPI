using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///IGetTrainJiaoLu 的摘要说明
    /// </summary>
    public class IGetTrainJiaoLu : IQueryResult
    {
        public IGetTrainJiaoLu()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private class ParamModel
        {
            public string cid;
        }

        private class JsonModel
        {
            public int result;
            public string resultStr;
            public ArrayName data;
        }

        private class ArrayName
        {
            public object ArrayJiaoLu;

        }


        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
                TF.RunSafty.BLL.TrainJiaoLu bllPlace = new TF.RunSafty.BLL.TrainJiaoLu();
                List<TF.RunSafty.Model.TrainJiaoLu> placeList = bllPlace.GetTrainJiaoLuList("");
                jsonModel.result = 0;
                jsonModel.resultStr = "提交成功";
                ArrayName p = new ArrayName();
                jsonModel.data = p;
                p.ArrayJiaoLu = bllPlace.GetPlaceList(placeList);
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
}