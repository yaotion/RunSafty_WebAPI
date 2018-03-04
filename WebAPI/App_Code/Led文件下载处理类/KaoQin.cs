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
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;


namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///KaoQin 的摘要说明
    /// </summary>
    public class IKaoQin : IQueryResult
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
        public IKaoQin()
        {
        }

        public override string QueryResult()
        {
            DataModel dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(this.Data);
            string strWorkShopGUID = dataModel.WorkShopGUID;
            string cid = dataModel.cid;
            JsonModel jsonModel = new JsonModel();
            jsonModel.result = "0";
            jsonModel.resultStr = "返回成功";
            TF.Api.BLL.KaoQinBLL bllKaoQin = new TF.Api.BLL.KaoQinBLL();
            DataTable table = bllKaoQin.GetKaoQinByWorkShopGUID(strWorkShopGUID);
            jsonModel.Content = table;
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
        }
    }
}