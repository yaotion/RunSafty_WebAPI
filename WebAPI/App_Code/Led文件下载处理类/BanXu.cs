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
using System.Collections;
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///BanXu 的摘要说明
    /// </summary>
    public class IBanXu : IQueryResult
    {
        internal class DataModel
        {
            public string cid;
            public string TrainmanJiaoluGUID;
            public string WorkShopGUID;
        }
        internal class TrainMan
        {
            public string trianmanGUID;
            public string TrainmanNo;
            public string TrainmanName;
        }
        internal class Record
        {
            public string Index;
            public IList<TrainMan> TrainmanList;
            public string EndWorkTime;
        }
        internal class JsonModel
        {
            public string result;
            public string resultStr;
            public IList<Record> Content;
        }
        public IBanXu()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public override string QueryResult()
        {
            DataModel dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(this.Data);
            string strTrainmanJiaoLuGUID = dataModel.TrainmanJiaoluGUID;
            string strWorkShopGUID = dataModel.WorkShopGUID;
            TF.Api.BLL.BanXuBLL bllBanXu = new TF.Api.BLL.BanXuBLL();
            JsonModel jsonModel = new JsonModel();
            jsonModel.Content = new List<Record>();
            TrainMan man = null;
            DataTable table = bllBanXu.GetBanXuByJiaoLuGUID(strWorkShopGUID, strTrainmanJiaoLuGUID);
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    Record record = new Record();
                    DateTime endWorkTime;
                    string strEndWorkTime = row["dtLastEndWorkTime1"] == null ? "" : row["dtLastEndWorkTime1"].ToString();
                    if (DateTime.TryParse(strEndWorkTime, out endWorkTime))
                    {
                        record.EndWorkTime = endWorkTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        record.EndWorkTime = "";
                    }
                    record.Index = (i + 1).ToString();
                    record.TrainmanList = new List<TrainMan>();
                    jsonModel.Content.Add(record);
                    for (int j = 1; j < 5; j++)
                    {
                        man = new TrainMan();
                        man.TrainmanName = row["strTrainmanName" + j.ToString()].ToString();
                        man.TrainmanNo = row["strTrainmanNumber" + j.ToString()].ToString();
                        man.trianmanGUID = row["strTrainmanGUID" + j.ToString()].ToString();
                        record.TrainmanList.Add(man);
                    }
                }
            }
            jsonModel.result = "0";
            jsonModel.resultStr = "返回成功";
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
        }
    }
}