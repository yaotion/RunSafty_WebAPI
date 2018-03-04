using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace TF.Api.BLL 
{
   public  class KaoQinBLL
    {
       internal class JsonModel
        {
            public string result;
            public string resultStr;
            public DataTable Content;
        }
        internal class DataModel
        {
            public string cid;
            public string WorkShopGUID;
        }
        public string GetKaoQinInformation(string data)
        {

            DataModel dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(data);
            string strWorkShopGUID = dataModel.WorkShopGUID;
            string cid = dataModel.cid;
            JsonModel jsonModel = new JsonModel();
            jsonModel.result = "0";
            jsonModel.resultStr = "返回成功"; 
            DataTable table = GetKaoQinByWorkShopGUID(strWorkShopGUID);
            jsonModel.Content = table;
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
        }

       /// <summary>
       /// 根据车间GUID获取考勤信息
       /// </summary>
       /// <param name="strWorkShopGUID"></param>
       /// <returns></returns>
       public DataTable GetKaoQinByWorkShopGUID(string strWorkShopGUID)
       {
           TF.Api.DBUtility.DBKaoQin dbKaoQin = new TF.Api.DBUtility.DBKaoQin();
           DataTable table = dbKaoQin.GetKaoQinByWorkShopGUID(strWorkShopGUID);
           if (table != null)
           {
               table.Columns.Add("Index",typeof(string));
               table.AcceptChanges();
               int j = 1;
               foreach (DataRow row in table.Rows)
               {
                   row["Index"] = j.ToString();
                   j++;
               }
               table.AcceptChanges();
           }
           return table;
       }
    }
}
