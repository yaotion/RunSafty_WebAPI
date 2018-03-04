using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSafty.Model.InterfaceModel;
using TF.Api.Utilities;

namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///IGetPlanRestByWorkShopAndTime 的摘要说明
    /// </summary>
    public class IGetPlanRestByWorkShopAndTime : IQueryResult
    {
        public IGetPlanRestByWorkShopAndTime()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        public class PlanSign
        {
            public TrainmanData data;
            public string cid;

        }
        public class TrainmanData
        {
            public List<JiaoLu> JiaoLuAry;
            public string dtStartTime;
            public string dtEndTime;
        }



        public class JiaoLu
        {
            public string strTrainJiaoLuGUID;
        }


        private class JsonModel
        {
            public int result;
            public string resultStr;
            public object data;
        }

        private class ArrayName
        {
            public object signplanary;
        }


        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                PlanSign paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanSign>(this.Data);

                TF.RunSafty.BLL.Tab_Plan_Rest bllPlace = new TF.RunSafty.BLL.Tab_Plan_Rest();
                string strJiaoLu = "";
                foreach (TF.RunSaftyAPI.App_Api.Public.IGetPlanRestByWorkShopAndTime.JiaoLu JiaoLuId in paramModel.data.JiaoLuAry)
                {
                    strJiaoLu += "'" + JiaoLuId.strTrainJiaoLuGUID + "',";
                }
                strJiaoLu = strJiaoLu.Substring(0, strJiaoLu.Length - 1);
                List<TF.RunSafty.Model.TAB_Plan_Rest> placeList = bllPlace.GetPlanListByTimeAndWorkShop(strJiaoLu, paramModel.data.dtStartTime, paramModel.data.dtEndTime);
                jsonModel.result = 0;
                jsonModel.resultStr = "提交成功";
                ArrayName an = new ArrayName();
                jsonModel.data = an;
                an.signplanary = bllPlace.GetPlaceList(placeList);
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