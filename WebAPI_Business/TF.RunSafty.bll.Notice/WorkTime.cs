using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace TF.RunSafty.Notice
{
    public class WorkTime
    {
        #region 获取劳时
        public class WorkTime_In
        {
            public string strTrainmanGUID;
            public string dtBeginTime;
            public string dtEndTime;
        }
        public class WorkTime_Out
        {
            public string result;
            public string resultStr;
            public string nTotalWorkCount;
            public string ntotalMinutes;
            public DataTable Detail;
        }  
 
        public WorkTime_Out GetWorkTime(string data)
        {
            WorkTime_Out jsonModel = new WorkTime_Out();
            WorkTime_In param = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkTime_In>(data);
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
                    object minutes=table.Compute("sum(fTotalTime)", "");
                    ntotalMinutes = Convert.ToDecimal((minutes==null || minutes==DBNull.Value)?"0":minutes.ToString());
                }
                jsonModel.Detail = table;
                jsonModel.ntotalMinutes = ntotalMinutes.ToString();
                jsonModel.nTotalWorkCount = nTotalWorkCount.ToString();
                jsonModel.result = "0";
                jsonModel.resultStr = "返回成功";

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = "1";
                jsonModel.resultStr = "提交失败:" + ex.Message;
            }
            return jsonModel;

        }

        #endregion
    }
}
