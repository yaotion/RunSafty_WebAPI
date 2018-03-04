using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;
using System.Data; 

namespace TF.RunSafty.Logic
{
    public class MonthWorkTimeBLL
    {
        private int pageIndex = 0;

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        private int pageSize = 15;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        public int GetMonthWorkTimeStatisticsRecordCount(string where)
        {
            string commandText = string.Format(@"
  select count(*) from  (select strTrainmanNumber,strTrainmanName,
sum(fRunTotalTime) as fMonthRunTotalTime,
sum(fBeginTotalTime) as fMonthBeginTotalTime,
sum(fEndTotalTime) as fMonthEndTotalTime,
sum(fTotalTime) as fMonthTotalTime,
sum(case when nOutTotalTime>0 then nOutTotalTime else 0 end) as nOutTotalTime 
from VIEW_WorkTime_Turn_Trainman where 1=1 
  {0}
group by strTrainmanNumber,strTrainmanName) a  
", where);
            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, commandText);
        }
        public int GetMonthWorkTimeStatisticsRecordCount(string where, List<SqlParameter> parameters)
        {
            string commandText = string.Format(@"
  select count(*) from  (select strTrainmanNumber,strTrainmanName,
sum(fRunTotalTime) as fMonthRunTotalTime,
sum(fBeginTotalTime) as fMonthBeginTotalTime,
sum(fEndTotalTime) as fMonthEndTotalTime,
sum(fTotalTime) as fMonthTotalTime,
sum(case when nOutTotalTime>0 then nOutTotalTime else 0 end) as nOutTotalTime 
from VIEW_WorkTime_Turn_Trainman where 1=1 
  {0}
group by strTrainmanNumber,strTrainmanName) a  
", where);
            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, commandText,parameters.ToArray<SqlParameter>());
        }
        public DataTable GetMonthWorkTimeStatistics(string where, List<SqlParameter> parameters)
        {
            int pageSize = 15;
            DataTable table = null;
            string commandText = string.Format(@"
select top {1} *,(select count(*) 
from VIEW_WorkTime_Turn_Trainman where 1=1 and 
strTrainmanNumber=a.strTrainmanNumber and dtEndWorkTime is not null {0}
) as tlsl,
(SELECT strValue FROM tabSysConfig WHERE strSection='Clyj' AND strIdent = 'YlRed') AS YlRed,
(select strValue from tabsysconfig where strsection='Clyj' and strIdent='YlYellow') AS YlYellow 
from (select strTrainmanNumber,strTrainmanName,
sum(fRunTotalTime) as fMonthRunTotalTime,
sum(fBeginTotalTime) as fMonthBeginTotalTime,
sum(fEndTotalTime) as fMonthEndTotalTime,
sum(fTotalTime) as fMonthTotalTime,
sum(case when nOutTotalTime>0 then nOutTotalTime else 0 end) as nOutTotalTime 
from VIEW_WorkTime_Turn_Trainman where 1=1 
  {0}
group by strTrainmanNumber,strTrainmanName) a where 1=1 and a.strTrainmanNumber 
not in (select top {2} b.strTrainmanNumber from (select strTrainmanNumber,
sum(fTotalTime) as fMonthTotalTime from VIEW_WorkTime_Turn_Trainman where 1=1 {0} 
group by strTrainmanNumber,strTrainmanName ) b order by b.fMonthTotalTime desc) order by a.fMonthTotalTime desc
", where, pageSize,pageIndex*pageSize);
            table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, commandText,parameters.ToArray<SqlParameter>()).Tables[0];
            return table;
        }
        public DataTable GetMonthWorkTimeStatistics(string where)
        {
            int pageSize = 15;
            DataTable table = null;
            string commandText = string.Format(@"
select top {1} *,(select count(*) 
from VIEW_WorkTime_Turn_Trainman where 1=1 and 
strTrainmanNumber=a.strTrainmanNumber and dtEndWorkTime is not null {0}
) as tlsl,
(SELECT strValue FROM tabSysConfig WHERE strSection='Clyj' AND strIdent = 'YlRed') AS YlRed,
(select strValue from tabsysconfig where strsection='Clyj' and strIdent='YlYellow') AS YlYellow 
from (select strTrainmanNumber,strTrainmanName,
sum(fRunTotalTime) as fMonthRunTotalTime,
sum(fBeginTotalTime) as fMonthBeginTotalTime,
sum(fEndTotalTime) as fMonthEndTotalTime,
sum(fTotalTime) as fMonthTotalTime,
sum(case when nOutTotalTime>0 then nOutTotalTime else 0 end) as nOutTotalTime 
from VIEW_WorkTime_Turn_Trainman where 1=1 
  {0}
group by strTrainmanNumber,strTrainmanName) a where 1=1 and a.strTrainmanNumber 
not in (select top {1} b.strTrainmanNumber from (select strTrainmanNumber,
sum(fTotalTime) as fMonthTotalTime from VIEW_WorkTime_Turn_Trainman where 1=1 {0} 
group by strTrainmanNumber,strTrainmanName ) b order by b.fMonthTotalTime desc) order by a.fMonthTotalTime desc
", where, pageSize);
            table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, commandText).Tables[0];
            return table;
        }

        #region 根据人员分页统计
        public DataTable GetMonthWorkTime(string wherePerson, string whereWork, List<SqlParameter> parameters)
        {
            DataTable table = null;
            string pagerStr = GetPagerString(wherePerson);
            string commandText = string.Format(@"select a.strTrainmanNumber,a.strTrainmanName
,SUM(case when (a.strTrainmanNumber=b.strTrainmanNumber1 or a.strTrainmanNumber=b.strTrainmanNumber2 
or a.strTrainmanNumber=b.strTrainmanNumber3 or a.strTrainmanNumber=b.strTrainmanNumber4 ) then  b.fRunTotalTime else 0 end) as fMonthRunTotalTime
,SUM(case when (a.strTrainmanNumber=b.strTrainmanNumber1 or a.strTrainmanNumber=b.strTrainmanNumber2 
or a.strTrainmanNumber=b.strTrainmanNumber3 or a.strTrainmanNumber=b.strTrainmanNumber4 ) then  1 else 0 end) as tlsl
,SUM(case when (a.strTrainmanNumber=b.strTrainmanNumber1 or a.strTrainmanNumber=b.strTrainmanNumber2 
or a.strTrainmanNumber=b.strTrainmanNumber3 or a.strTrainmanNumber=b.strTrainmanNumber4 ) then  b.fRunTotalTime else 0 end) as fMonthRunTotalTime
,SUM(case when (a.strTrainmanNumber=b.strTrainmanNumber1 or a.strTrainmanNumber=b.strTrainmanNumber2 
or a.strTrainmanNumber=b.strTrainmanNumber3 or a.strTrainmanNumber=b.strTrainmanNumber4 ) then  b.fBeginTotalTime else 0 end) as fMonthBeginTotalTime
,SUM(case when (a.strTrainmanNumber=b.strTrainmanNumber1 or a.strTrainmanNumber=b.strTrainmanNumber2 
or a.strTrainmanNumber=b.strTrainmanNumber3 or a.strTrainmanNumber=b.strTrainmanNumber4 ) then  b.fEndTotalTime else 0 end) as fMonthEndTotalTime
,SUM(case when (a.strTrainmanNumber=b.strTrainmanNumber1 or a.strTrainmanNumber=b.strTrainmanNumber2 
or a.strTrainmanNumber=b.strTrainmanNumber3 or a.strTrainmanNumber=b.strTrainmanNumber4 ) then  b.fTotalTime else 0 end) as fMonthTotalTime
,SUM(case when (a.strTrainmanNumber=b.strTrainmanNumber1 or a.strTrainmanNumber=b.strTrainmanNumber2 
or a.strTrainmanNumber=b.strTrainmanNumber3 or a.strTrainmanNumber=b.strTrainmanNumber4 ) then  b.nOutTotalTime else 0 end) as nOutTotalTime
,
(SELECT strValue FROM tabSysConfig WHERE strSection='Clyj' AND strIdent = 'YlRed') AS YlRed,
(select strValue from tabsysconfig where strsection='Clyj' and strIdent='YlYellow') AS YlYellow 
 from ({0}
)a ,(select * from View_WorkTime_Turn where 1=1 {1}) b
group by a.strTrainmanNumber,a.strTrainmanName", pagerStr,whereWork);
            table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, commandText, parameters.ToArray<SqlParameter>()).Tables[0];
            return table;
        }
        private string GetPagerString(string wherePerson)
        {
            string str = "";
            if (this.pageIndex == 0)
            {
                str =string.Format( @"
select top {1} * from TAB_Org_Trainman where 1=1 {0}
order by nID asc  ",wherePerson,this.pageSize);
            }
            else
            {
                str = string.Format(@"
select top {1} * from TAB_Org_Trainman where nID>(select MAX(nid) from (select top {2} nID from TAB_Org_Trainman 
where 1=1 {0}
order by nID asc )t) ",wherePerson,this.pageSize,pageSize*pageIndex);
            }
            return str;
        }


        public DataTable GetWorkTimeByTrainman(string strTrainmanGUID, string strBegin, string strEnd)
        {
            string strSql = string.Format(@"select  * from View_WorkTime_Turn where dtEndWorkTime is not null and 
dtBeginWorkTime >= '{0}' and dtBeginWorkTime < '{1}' and (strTrainmanGUID1 = '{2}' or strTrainmanGUID2 ='{2}' or strTrainmanGUID3 = '{2}' or strTrainmanGUID4 ='{2}' ) 
order by dtBeginWorkTime
", strBegin,strEnd,strTrainmanGUID);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        #endregion



        #region web 接口
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

        /// <summary>
        /// 获取劳时接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public   string GetWorkTime(string data)
        {
            JsonModel jsonModel = new JsonModel();
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(data);
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
        #endregion
    }
}
