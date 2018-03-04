using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.RunEvent
{
    public class DBRunEvent_Add
    {
        #region 程序主入口
        public int SubmitRunEvent(RunEvent model)
        {
            int ErrCode = 0;
            string ErrInfo = "";
            string eventGUID = Guid.NewGuid().ToString();

            TrainmanPlan plan = new TrainmanPlan();


            if (DBPlan.GetTrainPlanBriefByRange(model.strTrainmanNumber1, model.dtEventTime, plan))
            {
                AddRunEvent(model, plan, eventGUID, ref ErrInfo, ref ErrCode);
                ErrCode = 0;
            }
            else
            {
                ErrCode = 0;
                ErrInfo = "没有指定乘务员的计划或机组信息";
            }


            Plan_RunEvent_TrainDetail eventParam = new Plan_RunEvent_TrainDetail();
            eventParam.strRunEventGUID = eventGUID;
            eventParam.dtEventTime = model.dtEventTime;
            eventParam.strTrainmanNumber1 = model.strTrainmanNumber1;
            eventParam.strTrainmanNumber2 = model.strTrainmanNumber2;
            eventParam.nTMIS = model.strStationName;
            eventParam.JiaoLuAndnStationNo = model.JiaoLuAndnStationNo;
            eventParam.nKeHuo = model.nKehuo;
            eventParam.strTrainNo = model.strTrainNo;
            eventParam.strTrainTypeName = model.strTrainTypeName;
            eventParam.strTrainNumber = model.strTrainNumber;
            eventParam.dtCreateTime = DateTime.Now;
            eventParam.nResultID = model.nResultID;
            eventParam.strResult = model.strResult;
            eventParam.nEventID = model.nEventID;
            eventParam.strGUID = Guid.NewGuid().ToString();

            ////向TAB_Plan_RunEvent_Site中插入数据
            SubmitEventLog(eventParam);
            return ErrCode;

        }
        #endregion

        #region  判断是插入还是修改运行记录
        private Boolean AddRunEvent(RunEvent Rec, TrainmanPlan plan, string eventGUID,
            ref string ErrInfo, ref int ErrCode)
        {
            ErrCode = 0;
            ErrInfo = "";

            RsRunEvent rsEvent = new RsRunEvent();

            if (Rec.strRunEventGUID == string.Empty)
            {
                ErrCode = 1;
                return false;
            }

            if (Rec.dtEventTime > plan.trainPlan.lastArriveTime)
            {
                ErrCode = 3;
                ErrInfo = "超时被丢弃";
                return false;
            }

            bool bHasEvent = GetRunEventOfPlan(plan.trainPlan.planID, Rec.nEventID, rsEvent);
            if ((bHasEvent) && (rsEvent.dtEventTime == Rec.dtEventTime))
            {
                ErrCode = 4;
                ErrInfo = "记录重复";
                return false;
            }

            rsEvent.JiaoLuAndnStationNo = Rec.JiaoLuAndnStationNo;
            rsEvent.strRunEventGUID = eventGUID;
            rsEvent.nEventID = Rec.nEventID;
            rsEvent.strTrainPlanGUID = plan.trainPlan.planID;
            rsEvent.dtEventTime = Rec.dtEventTime;
            rsEvent.nTMIS = Rec.strStationName;
            rsEvent.strGroupGUID = plan.trainPlan.planID;
            rsEvent.strTrainmanNumber1 = Rec.strTrainmanNumber1;
            rsEvent.strTrainmanNumber2 = Rec.strTrainmanNumber2;
            rsEvent.nResultID = Rec.nResultID;
            rsEvent.strTrainNo = Rec.strTrainNo;
            rsEvent.strTrainNumber = Rec.strTrainNumber;
            rsEvent.strTrainTypeName = Rec.strTrainTypeName;
            rsEvent.strResult = Rec.strResult;
            AddRunEvent(rsEvent);
            return true;


        }
        #endregion

        #region  插入运行记录日志信息
        private int SubmitEventLog(Plan_RunEvent_TrainDetail TrainDetail)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_RunEvent_TrainDetail");
            strSql.Append("(strRunEventGUID,dtEventTime,strTrainmanNumber1,strTrainmanNumber2,nTMIS,nKeHuo,strTrainNo,strTrainTypeName,strTrainNumber,dtCreateTime,nResultID,strResult,nEventID,strGUID,JiaoLuAndnStationNo)");
            strSql.Append("values(@strRunEventGUID,@dtEventTime,@strTrainmanNumber1,@strTrainmanNumber2,@nTMIS,@nKeHuo,@strTrainNo,@strTrainTypeName,@strTrainNumber,@dtCreateTime,@nResultID,@strResult,@nEventID,@strGUID,@JiaoLuAndnStationNo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strRunEventGUID", TrainDetail.strRunEventGUID),
          new SqlParameter("@dtEventTime", TrainDetail.dtEventTime),
          new SqlParameter("@strTrainmanNumber1", TrainDetail.strTrainmanNumber1),
          new SqlParameter("@strTrainmanNumber2", TrainDetail.strTrainmanNumber2),
          new SqlParameter("@nTMIS", TrainDetail.nTMIS),
          new SqlParameter("@nKeHuo", TrainDetail.nKeHuo),
          new SqlParameter("@strTrainNo", TrainDetail.strTrainNo),
          new SqlParameter("@strTrainTypeName", TrainDetail.strTrainTypeName),
          new SqlParameter("@strTrainNumber", TrainDetail.strTrainNumber),
          new SqlParameter("@dtCreateTime", TrainDetail.dtCreateTime),
          new SqlParameter("@nResultID", TrainDetail.nResultID),
          new SqlParameter("@strResult", TrainDetail.strResult),
          new SqlParameter("@nEventID", TrainDetail.nEventID),
          new SqlParameter("@strGUID", TrainDetail.strGUID),
           new SqlParameter("@JiaoLuAndnStationNo", TrainDetail.JiaoLuAndnStationNo)
                                  };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));

        }

        #endregion

        #region  判断运行记录是否存在
        private Boolean GetRunEventOfPlan(string planGUID, int EventID, RsRunEvent rsEvent)
        {
            string sql = "select top 1 * from TAB_Plan_RunEvent where strTrainPlanGUID = @planGUID and nEventID = @eventID order by dtEventTime desc  ";

            SqlParameter[] sqlParams = {
                                        new SqlParameter("@planGUID",planGUID),
                                        new SqlParameter("@eventID",EventID)
                                       };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlParams).Tables[0];
            if (dt.Rows.Count == 0)
                return false;
            else
            {
                DataRow dr = dt.Rows[0];

                rsEvent.strRunEventGUID = dr["strRunEventGUID"].ToString();
                rsEvent.strTrainPlanGUID = dr["strTrainPlanGUID"].ToString();
                rsEvent.nEventID = Convert.ToInt32(dr["nEventID"]);
                rsEvent.dtEventTime = Convert.ToDateTime(dr["dtEventTime"]);
                rsEvent.strTrainNo = dr["strTrainNo"].ToString();
                rsEvent.strTrainTypeName = dr["strTrainTypeName"].ToString();
                rsEvent.strTrainNumber = dr["strTrainNumber"].ToString();
                rsEvent.nTMIS = dr["nTMIS"].ToString(); ;
                rsEvent.strGroupGUID = dr["strGroupGUID"].ToString();
                rsEvent.strTrainmanNumber1 = dr["strTrainmanNumber1"].ToString();
                rsEvent.strTrainmanNumber2 = dr["strTrainmanNumber2"].ToString();
                rsEvent.dtCreateTime = Convert.ToDateTime(dr["dtCreateTime"]);
                rsEvent.nResultID = Convert.ToInt32(dr["nResult"]);
                rsEvent.strResult = dr["strResult"].ToString();
                return true;
            }
        }

        #endregion

        #region 更新和添加运行记录

        /// <summary>
        /// 更新事件人员信息
        /// </summary>
        /// <param name="EventGUID"></param>
        /// <param name="tmid1"></param>
        /// <param name="tmid2"></param>
        /// <param name="eventTime"></param>
        /// <param name="resultContent"></param>
        private void UpdateEventTrainman(string EventGUID, string tmid1, string tmid2, DateTime? eventTime, string resultContent)
        {
            string sql = @"update TAB_Plan_RunEvent set strTrainmanNumber1=@strTrainmanNumber1, 
         strTrainmanNumber2=@strTrainmanNumber2,dtEventTime=@dtEventTime,strResult=@strResult where strRunEventGUID=@strRunEventGUID";

            SqlParameter[] sqlparams = {
                                 new SqlParameter("@strTrainmanNumber1",tmid1),
                                 new SqlParameter("@strTrainmanNumber2",tmid2),
                                 new SqlParameter("@dtEventTime",eventTime),
                                 new SqlParameter("@strResult",resultContent),
                                 new SqlParameter("@strRunEventGUID",EventGUID),
                             };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlparams);
        }
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="rsEvent"></param>
        private void AddRunEvent(RsRunEvent rsEvent)
        {
            string sql = @"insert into TAB_Plan_RunEvent (strRunEventGUID,strTrainPlanGUID, 
         nEventID,dtEventTime,strTrainNo,strTrainTypeName,strTrainNumber,nTMIS, 
         nKeHuo,strGroupGUID,strTrainmanNumber1,strTrainmanNumber2,dtCreateTime,nResult,strResult,JiaoLuAndnStationNo) 
         values (@strRunEventGUID,@strTrainPlanGUID,@nEventID,@dtEventTime,@strTrainNo, 
         @strTrainTypeName,@strTrainNumber,@nTMIS,@nKeHuo,@strGroupGUID,@strTrainmanNumber1,
         @strTrainmanNumber2,getdate(),@nResult,@strResult,@JiaoLuAndnStationNo)";

            SqlParameter[] sqlParams = {
                                        new SqlParameter("@strRunEventGUID",rsEvent.strRunEventGUID),
                                        new SqlParameter("@strTrainPlanGUID",rsEvent.strTrainPlanGUID),
                                        new SqlParameter("@nEventID",rsEvent.nEventID),
                                        new SqlParameter("@dtEventTime",rsEvent.dtEventTime),
                                        new SqlParameter("@strTrainNo",rsEvent.strTrainNo),
                                        new SqlParameter("@strTrainTypeName",rsEvent.strTrainTypeName),
                                        new SqlParameter("@strTrainNumber",rsEvent.strTrainNumber),
                                        new SqlParameter("@nTMIS",rsEvent.nTMIS),
                                        new SqlParameter("@nKeHuo",rsEvent.nKehuo),
                                        new SqlParameter("@strGroupGUID",rsEvent.strGroupGUID),
                                        new SqlParameter("@strTrainmanNumber1",rsEvent.strTrainmanNumber1),
                                        new SqlParameter("@strTrainmanNumber2",rsEvent.strTrainmanNumber2),
                                        new SqlParameter("@nResult",rsEvent.nEventID),
                                        new SqlParameter("@strResult",rsEvent.strResult),
                                        new SqlParameter("@JiaoLuAndnStationNo",rsEvent.JiaoLuAndnStationNo) 
                                       };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlParams);

        }

        #endregion

        #region 通过车站名称获取tmis号码
        private string GetTmisNumber(string strTmisName)
        {
            string sql = "select top 1 * from TAB_Base_Station where strStationName = @strStationName ";

            SqlParameter[] sqlParams = {
                                        new SqlParameter("@strStationName",strTmisName)
                                     
                                       };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlParams).Tables[0];
            if (dt.Rows.Count == 0)
                return "0";
            else
                return dt.Rows[0]["strStationNumber"].ToString();
        }
        #endregion

    }



    class DBPlan
    {
        TrainmanPlan plan = new TrainmanPlan();
        /// <summary>
        /// 根据人员工号及事件时间获取出勤计划，取事件时间向前3天至当前时间的最后一条计划
        /// </summary>
        /// <param name="tmid"></param>
        /// <param name="eventTime"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public static Boolean GetTrainPlanBriefByRange(string tmid, DateTime? eventTime, TrainmanPlan Plan)
        {


            string sql = @"select top 1 * from VIEW_Plan_Trainman 
                    where dtStartTime > DATEADD (Day,-3,@eventTime) and dtStartTime < @eventTime  and nPlanState >= 4 
                    and  ((strTrainmanNumber1 = @tmid) or 
                    (strTrainmanNumber2 = @tmid) or 
                    (strTrainmanNumber3 = @tmid) or 
                    (strTrainmanNumber4 = @tmid)) order by dtStartTime desc";

            SqlParameter[] sqlParams = {
                                        new SqlParameter("@eventTime",eventTime),
                                        new SqlParameter("@tmid",tmid)
                                       };



            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlParams).Tables[0];

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                Plan.trainPlan.planID = dt.Rows[0]["strTrainPlanGUID"].ToString();
                Plan.trainPlan.lastArriveTime = DataTypeConvert.ToDateTime(dt.Rows[0]["dtlastArriveTime"]);
                Plan.trainPlan.startTime = DataTypeConvert.ToDateTime(dt.Rows[0]["dtStartTime"]);
                Plan.trainPlan.createTime = DataTypeConvert.ToDateTime(dt.Rows[0]["dtCreateTime"]);

                Plan.tmGUID1 = dt.Rows[0]["strTrainmanGUID1"].ToString();
                Plan.tmGUID2 = dt.Rows[0]["strTrainmanGUID2"].ToString();
                Plan.tmGUID3 = dt.Rows[0]["strTrainmanGUID3"].ToString();
                Plan.tmGUID4 = dt.Rows[0]["strTrainmanGUID4"].ToString();

                Plan.tmid1 = dt.Rows[0]["strTrainmanNumber1"].ToString();
                Plan.tmid2 = dt.Rows[0]["strTrainmanNumber2"].ToString();
                Plan.tmid3 = dt.Rows[0]["strTrainmanNumber3"].ToString();
                Plan.tmid4 = dt.Rows[0]["strTrainmanNumber4"].ToString();

                Plan.tmname1 = dt.Rows[0]["strTrainmanName1"].ToString();
                Plan.tmname2 = dt.Rows[0]["strTrainmanName2"].ToString();
                Plan.tmname3 = dt.Rows[0]["strTrainmanName3"].ToString();
                Plan.tmname4 = dt.Rows[0]["strTrainmanName4"].ToString();
                return true;
            }
        }


        public class DataTypeConvert
        {
            public static DateTime? ToDateTime(object obj)
            {
                if (DBNull.Value.Equals(obj) || obj == null)
                    return null;
                else
                    return Convert.ToDateTime(obj);

            }

        }
    }


    public class RsRunEvent
    {
        public string strRunEventGUID;
        public string strTrainPlanGUID;
        public int nEventID;
        public DateTime strEventName;
        public DateTime? dtEventTime;
        public string strTrainNo;
        public string strTrainTypeName;
        public string strTrainNumber;
        public string nTMIS;
        public string strStationName;
        public int nKehuo;
        public string strGroupGUID;
        public string strTrainmanNumber1;
        public string strTrainmanNumber2;
        public DateTime dtCreateTime;
        public int nResultID;
        public string strResult;
        public string JiaoLuAndnStationNo;

    }

    public class Plan_RunEvent_TrainDetail
    {

        private string _JiaoLuAndnStationNo;

        public string JiaoLuAndnStationNo
        {
            get { return _JiaoLuAndnStationNo; }
            set { _JiaoLuAndnStationNo = value; }
        }

        private string m_strRunEventGUID;
        /// <summary>
        /// 运行记录id
        /// </summary>
        public string strRunEventGUID
        {
            get { return m_strRunEventGUID; }
            set { m_strRunEventGUID = value; }
        }
        private DateTime? m_dtEventTime;
        /// <summary>
        /// 运行时间
        /// </summary>
        public DateTime? dtEventTime
        {
            get { return m_dtEventTime; }
            set { m_dtEventTime = value; }
        }
        private string m_strTrainmanNumber1;
        /// <summary>
        /// 司机工号
        /// </summary>
        public string strTrainmanNumber1
        {
            get { return m_strTrainmanNumber1; }
            set { m_strTrainmanNumber1 = value; }
        }
        private string m_strTrainmanNumber2;
        /// <summary>
        /// 副司机工号
        /// </summary>
        public string strTrainmanNumber2
        {
            get { return m_strTrainmanNumber2; }
            set { m_strTrainmanNumber2 = value; }
        }
        private string m_nTMIS;
        /// <summary>
        /// tmis
        /// </summary>
        public string nTMIS
        {
            get { return m_nTMIS; }
            set { m_nTMIS = value; }
        }
        private int m_nKeHuo;
        /// <summary>
        /// 客货类型
        /// </summary>
        public int nKeHuo
        {
            get { return m_nKeHuo; }
            set { m_nKeHuo = value; }
        }
        private string m_strTrainNo;
        /// <summary>
        /// 车次
        /// </summary>
        public string strTrainNo
        {
            get { return m_strTrainNo; }
            set { m_strTrainNo = value; }
        }
        private string m_strTrainTypeName;
        /// <summary>
        /// 车型
        /// </summary>
        public string strTrainTypeName
        {
            get { return m_strTrainTypeName; }
            set { m_strTrainTypeName = value; }
        }
        private string m_strTrainNumber;
        /// <summary>
        /// 车号
        /// </summary>
        public string strTrainNumber
        {
            get { return m_strTrainNumber; }
            set { m_strTrainNumber = value; }
        }
        private DateTime? m_dtCreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? dtCreateTime
        {
            get { return m_dtCreateTime; }
            set { m_dtCreateTime = value; }
        }
        private int m_nResultID;
        /// <summary>
        /// 结果
        /// </summary>
        public int nResultID
        {
            get { return m_nResultID; }
            set { m_nResultID = value; }
        }
        private string m_strResult;
        /// <summary>
        /// 结果详细
        /// </summary>
        public string strResult
        {
            get { return m_strResult; }
            set { m_strResult = value; }
        }
        private int m_nEventID;
        /// <summary>
        /// 运行记录类型
        /// </summary>
        public int nEventID
        {
            get { return m_nEventID; }
            set { m_nEventID = value; }
        }
        private string m_strGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strGUID
        {
            get { return m_strGUID; }
            set { m_strGUID = value; }
        }
        private int m_nid;
        /// <summary>
        /// 
        /// </summary>
        public int nid
        {
            get { return m_nid; }
            set { m_nid = value; }
        }
    }


    public class TrainmanPlan
    {
        //机车计划
        public TrainPlan trainPlan = new TrainPlan();

        //乘务员GUID1 -- GUID4
        public string tmGUID1 = string.Empty;
        public string tmGUID2 = string.Empty;
        public string tmGUID3 = string.Empty;
        public string tmGUID4 = string.Empty;

        //乘务员工号1 -- 4
        public string tmid1 = string.Empty;
        public string tmid2 = string.Empty;
        public string tmid3 = string.Empty;
        public string tmid4 = string.Empty;

        //乘务员姓名1 -- 4
        public string tmname1 = string.Empty;
        public string tmname2 = string.Empty;
        public string tmname3 = string.Empty;
        public string tmname4 = string.Empty;
    }


    public class TrainPlan
    {
        //计划出勤时间
        public DateTime? startTime;

        //计划编号
        public string planID;

        //创建时间
        public DateTime? createTime;

        //最后到达时间
        public DateTime? lastArriveTime;
    }

 
}