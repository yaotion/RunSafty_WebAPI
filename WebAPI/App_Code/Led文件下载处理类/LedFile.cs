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
    ///LedFile 的摘要说明
    /// </summary>
    public class iLedFile : IQueryResult
    {
        internal class JsonModel
        {
            public string result;
            public string returnStr;
            public DataTable FileList;
        }
        internal class ParamModel
        {
            public string cid;
        }
        public string clientid;
        public iLedFile()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public override string QueryResult()
        {
            JsonModel model = new JsonModel();
            DataTable table = null;
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            this.clientid = param.cid;
            TF.Api.BLL.Bll_Tab_Led_File bllLed = new TF.Api.BLL.Bll_Tab_Led_File();
            try
            {
                table = bllLed.GetLedFileList(clientid);
                model.result = "0";
                model.returnStr = "返回成功";
                model.FileList = table;
            }
            catch (Exception ex)
            {
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(model, timeConverter);
        }
    }
}