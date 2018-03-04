using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using TF.RunSafty.Plan.MD;
using TF.CommonUtility;
using ThinkFreely.DBUtility;




namespace TF.Runsafty.Plan
{
    public class LCSendLog
    {
        public class InGetUnRecvSendLog
        {
            //客户端GUID
            public string SiteGUID;
            //起始时间
            public DateTime FromTime;
        }

        public class OutGetUnRecvSendLog
        {
            //日志信息
            public PlanSendLogList Logs = new PlanSendLogList();
        }

        /// <summary>
        /// 获取未下发的计划信息
        /// </summary>
        public InterfaceOutPut GetUnRecvSendLog(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetUnRecvSendLog InParams = javaScriptSerializer.Deserialize<InGetUnRecvSendLog>(Data);
                OutGetUnRecvSendLog OutParams = new OutGetUnRecvSendLog();
                string strSql = @"select * from tab_plan_send  tsend where (dtSendTime <= @FromTime) and 
                     (strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID=@strSiteGUID) 
                     or strTrainJiaoluGUID in (select strSubTrainJiaoluGUID from TAB_Base_TrainJiaolu_SubDetail where strTrainJiaoluGUID in
                     (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID=@strSiteGUID))) 
                     and  (bIsRec = 0) order by dtSendTime";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("FromTime",InParams.FromTime),
                    new SqlParameter("strSiteGUID",InParams.SiteGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PlanSendLog sendlog = new PlanSendLog();
                    sendlog.strTrainNo = dt.Rows[i]["strTrainNo"].ToString();
                     sendlog.strSendGUID = dt.Rows[i]["strSendGUID"].ToString();
                     sendlog.strTrainPlanGUID = dt.Rows[i]["strTrainPlanGUID"].ToString();
                     sendlog.strTrainJiaoluName = dt.Rows[i]["strTrainJiaoluName"].ToString();
                     sendlog.dtStartTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtStartTime"],DateTime.Parse("1899-01-01"));
                     sendlog.dtRealStartTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtRealStartTime"], DateTime.Parse("1899-01-01"));
                     sendlog.strSendSiteName = dt.Rows[i]["strSendSiteName"].ToString();
                     sendlog.dtSendTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtSendTime"], DateTime.Parse("1899-01-01"));
                     OutParams.Logs.Add(sendlog);                
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetUnRecvSendLog:" + ex.Message);
                throw ex;
            }
            return output;
        }
    }
}
