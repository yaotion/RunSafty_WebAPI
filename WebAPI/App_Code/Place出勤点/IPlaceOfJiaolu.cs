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
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///IPlaceOfJiaolu 的摘要说明
    /// </summary>
    public class IPlaceOfJiaolu : IQueryResult
    {
        private class JiaoLuData
        {
            public string trainjiaoluID;
        }
        private class ParamModel
        {
            public string cid;
            public JiaoLuData data;
        }
        private class listModel
        {
            public string placeID;
            public string placeName;
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<listModel> data;
        }
        public IPlaceOfJiaolu()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private List<listModel> GetPlaceList(List<TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace> placeList)
        {
            if (placeList != null)
            {
                List<listModel> resultList = new List<listModel>();
                foreach (TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace place in placeList)
                {
                    listModel model = new listModel();
                    model.placeID = place.strPlaceID;
                    model.placeName = place.strPlaceName;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }
        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
                TF.RunSafty.BLL.Place.VIEW_Base_TrainJiaolu_DutyPlace bllPlace = new TF.RunSafty.BLL.Place.VIEW_Base_TrainJiaolu_DutyPlace();
                List<TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace> placeList = bllPlace.GetPlaceOfTrainJiaolu(paramModel.data.trainjiaoluID);
                jsonModel.result = 0;
                jsonModel.resultStr = "提交成功";
                jsonModel.data = GetPlaceList(placeList);
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
}
