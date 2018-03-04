using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.Api.DBUtility;


namespace TF.Api.BLL
{
    public class Bll_Tab_Led_File
    {
        internal class JsonModel
        {
           public  string result;
           public  string returnStr;
           public  DataTable FileList;
        }
        internal class ParamModel
        {
            public string cid;
        }
        public string clientid;

        public string GetLedList(string data)
        {
            JsonModel model = new JsonModel();
            DataTable table = null;
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(data);
            this.clientid = param.cid;
            table = GetLedFileList(clientid);
            model.result = "0";
            model.returnStr = "返回成功";
            model.FileList = table;

            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(model, timeConverter);
        }
        public DataTable GetLedFileList(string clientId)
        {
            TF.Api.DBUtility.ILedFile led=new ILedFile();
            DataTable table = led.GetLedFileList(clientId);
            return table;
        }
    }
}
