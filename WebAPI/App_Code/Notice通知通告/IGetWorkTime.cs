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
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///IGetWorkTime 的摘要说明
    /// </summary>
    public class IGetWorkTime : IQueryResult
    {
        public IGetWorkTime()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private class ParamModel
        {
            public string strTrainmanGUID;
            public string dtBeginTime;
            public string dtEndTime;
        }
        public class JsonModel
        {
            public string result;
            public string resultStr;
            public string nTotalWorkCount;
            public string ntotalMinutes;
            public DataTable Detail;
        }

        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            TF.RunSafty.Logic.MonthWorkTimeBLL bllWorkTime = new TF.RunSafty.Logic.MonthWorkTimeBLL();
            int nTotalWorkCount = 0;
            decimal ntotalMinutes = 0m;
            DataTable table = null;
            try
            {
                table = bllWorkTime.GetWorkTimeByTrainman(param.strTrainmanGUID, param.dtBeginTime, param.dtEndTime);
                if (table != null)
                {
                    nTotalWorkCount = table.Rows.Count;
                    ntotalMinutes = Convert.ToDecimal(table.Compute("sum(fTotalTime)", ""));
                }
                jsonModel.Detail = table;
                jsonModel.ntotalMinutes = ntotalMinutes.ToString();
                jsonModel.nTotalWorkCount = nTotalWorkCount.ToString();
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");

            }
            jsonModel.result = "0";
            jsonModel.resultStr = "返回成功";
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter).Replace(":null", ":\"\"");
            return result;
        }
    }
}