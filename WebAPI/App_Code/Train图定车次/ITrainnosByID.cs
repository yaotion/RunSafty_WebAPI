using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using TF.RunSafty.Model;
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///ITrainnosByID 的摘要说明
    /// </summary>
    public class ITrainnosByID : IQueryResult
    {
        public ITrainnosByID()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private class JiaoLuData
        {
            public string trainnoID;
        }
        private class ParamModel
        {
            public string cid;
            public JiaoLuData data;
        }

        private class JsonModel
        {
            public int result;
            public string returnStr;
            public TF.RunSafty.Model.TAB_Base_TrainNo_Json data;
        }


        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
                TF.RunSafty.BLL.VIEW_Base_TrainNo bllPlace = new TF.RunSafty.BLL.VIEW_Base_TrainNo();
                List<TF.RunSafty.Model.VIEW_Base_TrainNo> placeList = bllPlace.GetTrainnosByID(paramModel.data.trainnoID);
                jsonModel.result = 0;
                jsonModel.returnStr = "提交成功";
                jsonModel.data = bllPlace.GetPlaceList(placeList)[0];
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.returnStr = "提交失败" + ex.Message;
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
        }
    }
}
