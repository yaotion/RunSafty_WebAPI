using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.DutyPlace
{
    public class LCDutyPlace
    {
        #region 获取交路下的出勤地点
        public class TrainJiaolu_In
        {
            public string siteID = "";
            public string trainjiaoluID = "";
        }
        public class TrainJiaolu_Out
        {
            public string result = "";
            public string resultStr = "";
            public DutyPlaceList data;
        }
        public TrainJiaolu_Out GetPlaceOfTrainJiaolu(string  data)
        {
            TrainJiaolu_Out json = new TrainJiaolu_Out();
            try
            {
                TrainJiaolu_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<TrainJiaolu_In>(data);
                TF.RunSafty.BLL.Place.VIEW_Base_TrainJiaolu_DutyPlace bllPlace = new TF.RunSafty.BLL.Place.VIEW_Base_TrainJiaolu_DutyPlace();
                List<TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace> placeList = bllPlace.GetPlaceOfTrainJiaolu(input.trainjiaoluID);
                json.result = "0";
                json.resultStr = "提交成功";
                json.data = GetPlaceList(placeList);
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        private DutyPlaceList GetPlaceList(List<TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace> placeList)
        {
            if (placeList != null)
            {
                DutyPlaceList resultList = new DutyPlaceList();
                foreach (TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace place in placeList)
                {
                    DutyPlace model = new DutyPlace();
                    model.placeID = place.strPlaceID;
                    model.placeName = place.strPlaceName;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }
        public TrainJiaolu_Out GetPlaceOfTrainJiaoluV2(TrainJiaolu_In input)
        {
            TrainJiaolu_Out json = new TrainJiaolu_Out();
            try
            {
                TF.RunSafty.BLL.Place.VIEW_Base_TrainJiaolu_DutyPlace bllPlace = new TF.RunSafty.BLL.Place.VIEW_Base_TrainJiaolu_DutyPlace();
                List<TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace> placeList = bllPlace.GetPlaceOfTrainJiaolu(input.trainjiaoluID);
                json.result = "0";
                json.resultStr = "提交成功";
                json.data = GetPlaceList(placeList);
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.log(ex.Message.ToString());
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取客户端下的出勤地点
        public class Client_In
        {
            public string siteID = "";
            public string trainjiaoluID = "";
        }
        public class Client_Out
        {
            public string result = "";
            public string resultStr = "";
            public DutyPlaceList data;
        }

        private DutyPlaceList GetPlaceListOfClient(List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace> placeList)
        {
            if (placeList != null)
            {
                DutyPlaceList resultList = new DutyPlaceList();
                foreach (TF.RunSafty.Model.VIEW_Base_Site_DutyPlace place in placeList)
                {
                    DutyPlace model = new DutyPlace();
                    model.placeID = place.strPlaceID;
                    model.placeName = place.strPlaceName;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }
        public Client_Out GetPlaceOfClient(string data)
        {
            Client_Out json = new Client_Out();
            try
            {
                Client_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Client_In>(data);
                TF.RunSafty.BLL.Place.VIEW_Base_Site_DutyPlace bllPlace = new TF.RunSafty.BLL.Place.VIEW_Base_Site_DutyPlace();
                List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace> placeList = bllPlace.GetPlaceOfClient(input.trainjiaoluID, input.siteID);
                json.result = "0";
                json.resultStr = "提交成功";
                json.data = GetPlaceListOfClient(placeList);
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
         
        #endregion


        #region  获取客户端所在出勤地点

        public class Get_InGetSitePlace
        {
            public string siteID;
        
        }

        public class Get_OutGetSitePlace
        {
            public string result = "";
            public string resultStr = "";
            public DutyPlace data;
        }
        public Get_OutGetSitePlace GetSitePlace(string data)
        {
            Get_OutGetSitePlace json = new Get_OutGetSitePlace();
            try
            {
                Get_InGetSitePlace input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetSitePlace>(data);
                DBDutyPlace db = new DBDutyPlace();
                json.data = db.GetSitePlace(input.siteID);
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region  获取客户端所在出勤地点

        public class Get_OutGetDutyPlaceList
        {
            public string result = "";
            public string resultStr = "";
            public List<DutyPlace> data;
        }
        public Get_OutGetDutyPlaceList GetDutyPlaceList(string data)
        {
            Get_OutGetDutyPlaceList json = new Get_OutGetDutyPlaceList();
            try
            {
                DBDutyPlace db = new DBDutyPlace();
                json.data = db.GetDutyPlaceList();
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion
    }
}
