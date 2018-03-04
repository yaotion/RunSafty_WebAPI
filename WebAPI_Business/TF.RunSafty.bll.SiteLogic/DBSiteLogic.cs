using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.RunSafty;

namespace TF.RunSafty.SiteLogic
{
    public class RsRunEvent
    {
        public string strRunEventGUID;
        public string strTrainPlanGUID;
        public int nEventID;
        public DateTime strEventName;
        public DateTime dtEventTime;
        public string strTrainNo;
        public string strTrainTypeName;
        public string strTrainNumber;
        public int nTMIS;
        public string strStationName;
        public int nKehuo;
        public string strGroupGUID;
        public string strTrainmanNumber1;
        public string strTrainmanNumber2;
        public DateTime dtCreateTime;
        public int nResultID;
        public string strResult;


     

    }
    

    public class DBSiteLogic
    {
        private class EventParam
        {
            public string eventGUID;
            public int eventID;
            public DateTime etime;
            public string tmid;
            public int ntmis;
            public int nresult;
            public string strResult;
            public string submitRemark;
            public int submitResult;

            public string strTrainType;// 车型
            public string strTrainNum;// 车号
            public string strCheCi;// 车次
            public int nLoginType;// 登记类型
            public string PhotoID;// 登记照片
            public int IsLackOfRest;// 寓休情况
            public string LackReason;//     不足原因
            public string ShenHeNumber;//  审核工号 
            public string ShenHeName;//     审核名字 
            public string ShenHeLoginType;//审核人登记方式 
            public string ShenHePhotoID;//审核照片ID 
            public DateTime InRoomTime; //离寓时候 的入寓时间 单独为离寓使用


        
        
        }
        #region 数据类型常量
        public const int eteInRoom = 10001;
        public const int eteOutRoom = 10002;
        public const int eteDrinkTest = 30001;
        #endregion

        #region 私有方法
       
        /// <summary>
        ///获取指定计划的指定事件类型的最后一个事件 
        /// </summary>
        /// <param name="planGUID"></param>
        /// <param name="EventID"></param>
        /// <param name="rsEvent"></param>
        /// <returns></returns>
        private Boolean GetRunEventOfPlan(string planGUID,int EventID,RsRunEvent rsEvent)
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
            {   DataRow dr = dt.Rows[0];
                
                rsEvent.strRunEventGUID = dr["strRunEventGUID"].ToString();
                rsEvent.strTrainPlanGUID = dr["strTrainPlanGUID"].ToString();
                rsEvent.nEventID = Convert.ToInt32(dr["nEventID"]);
                rsEvent.dtEventTime = Convert.ToDateTime(dr["dtEventTime"]);
                rsEvent.strTrainNo = dr["strTrainNo"].ToString();
                rsEvent.strTrainTypeName = dr["strTrainTypeName"].ToString();
                rsEvent.strTrainNumber = dr["strTrainNumber"].ToString();
                rsEvent.nTMIS = Convert.ToInt32(dr["nTMIS"]);
                rsEvent.strGroupGUID = dr["strGroupGUID"].ToString();
                rsEvent.strTrainmanNumber1 = dr["strTrainmanNumber1"].ToString();
                rsEvent.strTrainmanNumber2 = dr["strTrainmanNumber2"].ToString();
                rsEvent.dtCreateTime = Convert.ToDateTime(dr["dtCreateTime"]);
                rsEvent.nResultID = Convert.ToInt32(dr["nResult"]);
                rsEvent.strResult = dr["strResult"].ToString();
                return true;
            }        
        }
        /// <summary>
        /// 更新事件人员信息
        /// </summary>
        /// <param name="EventGUID"></param>
        /// <param name="tmid1"></param>
        /// <param name="tmid2"></param>
        /// <param name="eventTime"></param>
        /// <param name="resultContent"></param>
        private void UpdateEventTrainman(string EventGUID,string tmid1,string tmid2,DateTime eventTime,string resultContent)
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
         nKeHuo,strGroupGUID,strTrainmanNumber1,strTrainmanNumber2,dtCreateTime,nResult,strResult) 
         values (@strRunEventGUID,@strTrainPlanGUID,@nEventID,@dtEventTime,@strTrainNo, 
         @strTrainTypeName,@strTrainNumber,@nTMIS,@nKeHuo,@strGroupGUID,@strTrainmanNumber1,
         @strTrainmanNumber2,getdate(),@nResult,@strResult)";

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
                                        new SqlParameter("@strResult",rsEvent.strResult)                                 
                                       };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlParams);
        
        }

        private Boolean SubmitTrainmanEvent(SubmitInOutRoom Rec, int EventID, TrainmanPlan plan,int tmindex,
             ref string ErrInfo, ref int ErrCode, ref string eventGUID)
        {            
            ErrCode = 0;
            ErrInfo = "";
    
            RsRunEvent rsEvent = new RsRunEvent();

            if (Rec.tmid == string.Empty)
            {
                ErrCode = 1;
                return false;
            }

            if (Rec.etime > plan.trainPlan.lastArriveTime)
            {
                ErrCode = 3;
                ErrInfo = "超时被丢弃";
                return false;
            }

            string tmid1;
            string tmid2;

            //如果已经存在对应计划的事件，则更新事件人员
            if (GetRunEventOfPlan(plan.trainPlan.planID, eteOutRoom, rsEvent))
            {
                tmid1 = rsEvent.strTrainmanNumber1;

                tmid2 = rsEvent.strTrainmanNumber2;

                eventGUID = rsEvent.strRunEventGUID;

                DateTime tempTime = rsEvent.dtEventTime;

                if (Rec.etime > tempTime)
                {
                    tempTime = Rec.etime;
                }

                if (tmindex == 1)
                    tmid1 = Rec.tmid;
                if (tmindex == 2)
                    tmid2 = Rec.tmid;


                UpdateEventTrainman(rsEvent.strRunEventGUID, tmid1, tmid2, tempTime, Rec.strResult.ResultToString());
                return true;

            }
            else
            {
                tmid1 = "";
                tmid2 = "";

                if (tmindex == 1)
                    tmid1 = Rec.tmid;
                if (tmindex == 2)
                    tmid2 = Rec.tmid;
                

                rsEvent.strRunEventGUID = eventGUID;
                rsEvent.nEventID = EventID;
                rsEvent.strTrainPlanGUID = plan.trainPlan.planID;
                rsEvent.dtEventTime = Rec.etime;
                rsEvent.nTMIS = Convert.ToInt32(Rec.stmis);
                rsEvent.strGroupGUID = plan.trainPlan.planID;
                rsEvent.strTrainmanNumber1 = tmid1;
                rsEvent.strTrainmanNumber2 = tmid2;
                rsEvent.nResultID = Rec.nresult;
                rsEvent.strResult = Rec.strResult.ResultToString();

                    

                AddRunEvent(rsEvent);

                return true;
            }
                
        }

        private void SubmitEventLog(EventParam eventParam)
        {
            string sql = @"insert into TAB_Plan_RunEvent_Site (strRunEventGUID,nEventID,dtEventTime,strTrainmanNumber,nTMIS,dtCreateTime, 
             nSubmitResult,strSubmitRemark,nResultID,strResult,strTrainType,strTrainNum,strCheCi,nLoginType,PhotoID,IsLackOfRest,LackReason,ShenHeNumber,ShenHeName,ShenHeLoginType,ShenHePhotoID,InRoomTime) values 
             (@strRunEventGUID,@nEventID,@dtEventTime,@strTrainmanNumber,@nTMIS,getdate(), 
             @nSubmitResult,@strSubmitRemark,@ResultID,@strResult,@strTrainType,@strTrainNum,@strCheCi,@nLoginType,@PhotoID,@IsLackOfRest,@LackReason,@ShenHeNumber,@ShenHeName,@ShenHeLoginType,@ShenHePhotoID,@InRoomTime)";
            SqlParameter[] sqlparams = {
                                         new SqlParameter("@strRunEventGUID",eventParam.eventGUID),
                                         new SqlParameter("@nEventID",eventParam.eventID),
                                         new SqlParameter("@dtEventTime",eventParam.etime),
                                         new SqlParameter("@strTrainmanNumber",eventParam.tmid),
                                         new SqlParameter("@nTMIS",eventParam.ntmis),
                                         new SqlParameter("@nSubmitResult",eventParam.submitResult),
                                         new SqlParameter("@strSubmitRemark",eventParam.submitRemark),
                                         new SqlParameter("@ResultID",eventParam.nresult),
                                         new SqlParameter("@strResult",eventParam.strResult),

                                         new SqlParameter("@strTrainType",eventParam.strTrainType),
                                         new SqlParameter("@strTrainNum",eventParam.strTrainNum),
                                         new SqlParameter("@strCheCi",eventParam.strCheCi),
                                         new SqlParameter("@nLoginType",eventParam.nLoginType),
                                         new SqlParameter("@PhotoID",eventParam.PhotoID),
                                         new SqlParameter("@IsLackOfRest",eventParam.IsLackOfRest),
                                         new SqlParameter("@LackReason",eventParam.LackReason),
                                         new SqlParameter("@ShenHeNumber",eventParam.ShenHeNumber),
                                         new SqlParameter("@ShenHeName",eventParam.ShenHeName),
                                         new SqlParameter("@ShenHeLoginType",eventParam.ShenHeLoginType),
                                         new SqlParameter("@ShenHePhotoID",eventParam.ShenHePhotoID),
                                         new SqlParameter("@InRoomTime",eventParam.InRoomTime)

                                         };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlparams);
        }

        private void SubmitInOutRoomRecord(string planGUID, SubmitInOutRoom Rec, int trainmanIndex, int EventID, Trainman trainman)
        {
            if (trainmanIndex < 1 || trainmanIndex > 4)
            {
                return;
            }

            //获取事件地点
            string siteName;
            string sql = @"select top 1 strStationName from TAB_Base_Station where strStationNumber = '" + Rec.stmis + "'";
            object objSiteName = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql);
            if (objSiteName == null)
                siteName = "";
            else
                siteName = objSiteName.ToString();
                

            //如果没有记录则插入新记录
            sql = @"if not Exists(select top 1 * from TAB_Plan_RunEvent_SiteInOut where strTrainPlanGUID = @planid)
	        insert into TAB_Plan_RunEvent_SiteInOut(strTrainPlanGUID,strSiteTMIS,strSiteName)
		    values(@planid,@tmis,@siteName)";

            SqlParameter[] sqlparams = {
                                           new SqlParameter("@planid",planGUID),
                                           new SqlParameter("@tmis",Rec.stmis),
                                           new SqlParameter("@siteName",siteName)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlparams);

            //更新人员信息
            switch (EventID)
            {
                case eteInRoom:
                    sql = @"update TAB_Plan_RunEvent_SiteInOut set strTrainmanGUID{0} = @tmguid,
                    strTrainmanName{0} = @tmname,
                    strTrainmanNumber{0} = @tmid,
                    strInBrief{0} = @brief,
                    dtInRoomTime{0} = @etime,
                    strInRoomSourceID{0} = @worid where strTrainPlanGUID = @planid";
                    break; ;
                case eteOutRoom:
                    sql = @"update TAB_Plan_RunEvent_SiteInOut set strTrainmanGUID{0} = @tmguid,
                    strTrainmanName{0} = @tmname,
                    strTrainmanNumber{0} = @tmid,
                    strOutBrief{0} = @brief,
                    dtOutRoomTime{0} = @etime,
                    strOutRoomSourceID{0} = @worid where strTrainPlanGUID = @planid";
                    break; ;
                default:
                    throw new Exception("更新出入寓记录失败(错误的事件类型)");                    
            }
            

            sql = string.Format(sql, trainmanIndex);

            SqlParameter[] uparams = { 
                                         new SqlParameter("@planid",planGUID),
                                         new SqlParameter("@tmguid",trainman.tmGUID),
                                         new SqlParameter("@tmname",trainman.tmname),
                                         new SqlParameter("@tmid",trainman.tmid),
                                         new SqlParameter("@brief",Rec.strResult.ResultToString()),
                                         new SqlParameter("@etime",Rec.etime),
                                         new SqlParameter("@worid",Rec.workid)
                                     };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, uparams);


        }
        #endregion

        #region 共有方法 插入出入寓记录
        public void SubmitInOutRoomEvent(SubmitInOutRoom Rec, int EventID)
        {
            int tmindex;
            int ErrCode = 0;
            string ErrInfo = "";
            string eventGUID = Guid.NewGuid().ToString();

            TrainmanPlan plan = new TrainmanPlan();
            Trainman trainman = new Trainman();



            if (DBPlan.GetTrainPlanBriefByRange(Rec.tmid, Rec.etime, plan, out tmindex))
            {
                if (DBDictionary.GetTrainman(Rec.tmid, trainman))
                {
                    SubmitTrainmanEvent(Rec, EventID, plan, tmindex, ref ErrInfo, ref ErrCode, ref eventGUID);

                    SubmitInOutRoomRecord(plan.trainPlan.planID, Rec, tmindex, EventID, trainman);
                }
                else
                {
                    ErrCode = 1;
                    ErrInfo = string.Format("没有找到工号为[{0}]的乘务员!", Rec.tmid);
                }



            }
            else
            {
                ErrCode = 1;
                ErrInfo = "没有指定乘务员的计划或机组信息";
            }


            if (ErrCode == 0)
                ErrInfo = "添加成功";




            EventParam eventParam = new EventParam();
            eventParam.eventGUID = eventGUID;
            eventParam.eventID = EventID;
            eventParam.ntmis = Convert.ToInt32(Rec.stmis);
            eventParam.submitRemark = ErrInfo;
            eventParam.submitResult = ErrCode;
            eventParam.strResult = Rec.strResult.ResultToString();
            eventParam.nresult = Rec.nresult;
            eventParam.tmid = Rec.tmid;
            eventParam.etime = Rec.etime;

            eventParam.strTrainType = Rec.strTrainType;
            eventParam.strTrainNum = Rec.strTrainNum;
            eventParam.strCheCi = Rec.strCheCi;
            eventParam.nLoginType = Rec.nLoginType;
            eventParam.PhotoID = Rec.PhotoID;
            eventParam.IsLackOfRest = Rec.IsLackOfRest;
            eventParam.LackReason = Rec.LackReason;
            eventParam.ShenHeNumber = Rec.ShenHeNumber;
            eventParam.ShenHeName = Rec.ShenHeName;
            eventParam.ShenHeLoginType = Rec.ShenHeLoginType;
            eventParam.ShenHePhotoID = Rec.ShenHePhotoID;
            eventParam.InRoomTime = Rec.strResult.InRoomTime;

            //向TAB_Plan_RunEvent_Site中插入数据
            SubmitEventLog(eventParam);

        }
        public void SubmitInRoom(SubmitInOutRoom Rec)
        {
            SubmitInOutRoomEvent(Rec,eteInRoom);
        }

        public void SubmitOutRoom(SubmitInOutRoom Rec)
        {
            SubmitInOutRoomEvent(Rec, eteOutRoom);
        }

        public void SubmitDrinkRecord(SubmitDrinkRec Rec)
        {
            SqlTrans sqlTrans = new SqlTrans();
            sqlTrans.Begin();
            try
            {
                if (Rec.workid != string.Empty)
                {
                    string sql = @"select top 1 * from TAB_Plan_RunEvent_SiteInOut where 
                strInRoomSourceID1 = @workid or 
                strInRoomSourceID2= @workid or 
                strOutRoomSourceID1 = @workid or 
                strOutRoomSourceID2 = @workid";


                    SqlParameter[] sqlparam = {
                                              new SqlParameter("@workid",Rec.workid)
                                          };

                    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlparam).Tables[0];



                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        if (dr["strInRoomSourceID1"].ToString() == Rec.workid)
                            sql = @"Update TAB_Plan_RunEvent_SiteInOut set strInBrief1 = @DrinResult where strTrainPlanGUID = @workid";
                        else
                            if (dr["strInRoomSourceID2"].ToString() == Rec.workid)
                                sql = @"Update TAB_Plan_RunEvent_SiteInOut set strInBrief2 = @DrinResult where strTrainPlanGUID = @workid";
                            else
                                if (dr["strOutRoomSourceID1"].ToString() == Rec.workid)
                                    sql = @"Update TAB_Plan_RunEvent_SiteInOut set strOutBrief1 = @DrinResult where strTrainPlanGUID = @workid";
                                else
                                    if (dr["strOutRoomSourceID2"].ToString() == Rec.workid)
                                        sql = @"Update TAB_Plan_RunEvent_SiteInOut set strOutBrief2 = @DrinResult where strTrainPlanGUID = @workid";



                        SqlParameter[] uparams = {
                                                 new SqlParameter("@workid",Rec.workid),
                                                 new SqlParameter("@DrinResult",Rec.ToResultString())
                                             };

                        SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sql, uparams);

                    }

                }

                DBDrink.InsertDrinkRecord(Rec,sqlTrans.trans);
                sqlTrans.Commit();
            }
            catch (Exception ex)
            {
                sqlTrans.RollBack();
                throw ex;

            }



        }

    
    
        #endregion

        
     
    }
}
