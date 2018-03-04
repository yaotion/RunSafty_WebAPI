using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkSteps
{
    public class BeginEndWork
    {
        /// <summary>
        /// 获取计划最后到达时间
        /// </summary>
        public DateTime GetLastArrvieTime(String TrainPlanGUID)
        {
            DBEndWork db = new DBEndWork();
            DateTime ArriveTime = DateTime.Now;
            string strArriveTime = "";
            bool isLastArrvieTime = db.getGetLastArrvieTime(TrainPlanGUID, ref strArriveTime);
            ArriveTime = DBToDateTime(strArriveTime, Convert.ToDateTime("1899-01-01"));
            return ArriveTime;
        }

        public static DateTime DBToDateTime(object DBData, DateTime DefaultValue)
        {
            if (DBData == null)
            {
                return DefaultValue;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return DefaultValue;
            }
            DateTime dtTemp;
            if (!DateTime.TryParse(DBData.ToString(), out dtTemp))
            {
                return DefaultValue;
            }
            return dtTemp;
        }

        public class TuiQinPlan
        {
            public TuiQinPlan()
            { }
            public TrainPlan trainPlan = new TrainPlan();
            public string beginWorkTime = "";
            public TuiQinGroup tuiqinGroup = new TuiQinGroup();
        }
        public class TuiQinGroup
        {
            public TuiQinGroup()
            { }

            public NameGroup group = new NameGroup();
            public int verifyID1;
            public TestAlcoholInfo testAlcoholInfo1 = new TestAlcoholInfo();
            public int verifyID2;
            public TestAlcoholInfo testAlcoholInfo2 = new TestAlcoholInfo();
            public int verifyID3;
            public TestAlcoholInfo testAlcoholInfo3 = new TestAlcoholInfo();
            public int verifyID4;
            public TestAlcoholInfo testAlcoholInfo4 = new TestAlcoholInfo();
            public string turnStartTime = "";
            public string signed = "";
            public string isOver = "";
            public string turnMinutes = "";
            public string turnAlarmMinutes = "";

        }

        public List<TuiQinPlan> GetPlanList(List<MDEndWork_Full> vPlans)
        {
            List<TuiQinPlan> lPlans = new List<TuiQinPlan>();
            TuiQinPlan clientPlan = null;
            if (vPlans != null)
            {
                foreach (MDEndWork_Full plan in vPlans)
                {
                    clientPlan = new TuiQinPlan();
                    TuiQinGroup tuiqinGroup = new TuiQinGroup();
                    clientPlan.tuiqinGroup = tuiqinGroup;
                    tuiqinGroup.group = new NameGroup();
                    DutyPlace cPlace = new DutyPlace();
                    tuiqinGroup.group.place = cPlace;
                    cPlace.placeID = plan.strPlaceID;
                    cPlace.placeName = plan.strPlaceName;
                    tuiqinGroup.group.groupID = plan.strGroupGUID;
                    clientPlan.trainPlan = new TrainPlan();
                    clientPlan.trainPlan.createSiteGUID = plan.strCreateSiteGUID;
                    clientPlan.trainPlan.createSiteName = plan.strCreateSiteName;
                    if (plan.dtCreateTime.HasValue)
                    {
                        clientPlan.trainPlan.createTime = plan.dtCreateTime.Value;
                    }
                    clientPlan.trainPlan.createUserGUID = plan.strCreateUserGUID;
                    clientPlan.trainPlan.createUserName = plan.strCreateUserName;
                    clientPlan.trainPlan.dragTypeID = plan.nDragType.ToString();
                    clientPlan.trainPlan.dragTypeName = plan.nDragTypeName;
                    clientPlan.trainPlan.endStationID = plan.strEndStation;
                    clientPlan.trainPlan.endStationName = plan.strEndStationName;
                    clientPlan.trainPlan.kehuoID = plan.nKehuoID.ToString();
                    clientPlan.trainPlan.kehuoName = plan.strKehuoName;
                    clientPlan.trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    clientPlan.trainPlan.placeID = plan.strPlaceID;
                    clientPlan.trainPlan.placeName = plan.strPlaceName;
                    if (plan.nPlanState.HasValue)
                    {
                        clientPlan.trainPlan.planStateID = plan.nPlanState.Value;
                    }
                    clientPlan.trainPlan.planStateName = plan.strPlanStateName;
                    clientPlan.trainPlan.planTypeID = plan.nPlanType.ToString();
                    clientPlan.trainPlan.planTypeName = plan.strPlanTypeName;
                    clientPlan.trainPlan.remarkTypeID = plan.nRemarkType.ToString();
                    clientPlan.trainPlan.remarkTypeName = plan.strRemarkTypeName;
                    clientPlan.trainPlan.startStationID = plan.strStartStation;
                    clientPlan.trainPlan.startStationName = plan.strStartStationName;
                    clientPlan.trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    clientPlan.trainPlan.planID = plan.strTrainPlanGUID;
                    clientPlan.trainPlan.strTrainPlanGUID = plan.strTrainPlanGUID;
                    clientPlan.trainPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    clientPlan.trainPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    if (plan.dtLastArriveTime.HasValue)
                    {
                        clientPlan.trainPlan.lastArriveTime = plan.dtLastArriveTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        clientPlan.trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtRealStartTime.HasValue)
                    {
                        clientPlan.trainPlan.realStartTime = plan.dtRealStartTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        clientPlan.trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        clientPlan.trainPlan.firstStartTime = plan.dtFirstStartTime.Value;
                    }
                    if (plan.dtChuQinTime.HasValue)
                    {
                        clientPlan.beginWorkTime = plan.dtChuQinTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        clientPlan.trainPlan.kaiCheTime = plan.dtChuQinTime.Value;
                    }
                    else
                    {
                        clientPlan.beginWorkTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    clientPlan.trainPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    clientPlan.trainPlan.trainmanTypeName = plan.strTrainmanTypeName;
                    clientPlan.trainPlan.trainNo = plan.strTrainNo;
                    clientPlan.trainPlan.trainNumber = plan.strTrainNumber;
                    clientPlan.trainPlan.trainTypeName = plan.strTrainTypeName;
                    tuiqinGroup.group.station = new Station();
                    tuiqinGroup.group.station.stationID = plan.strStartStation;
                    tuiqinGroup.group.station.stationName = plan.strStartStationName;
                    tuiqinGroup.group.station.stationNumber = plan.ToString();
                    tuiqinGroup.group.trainman1 = new TF.RunSafty.WorkSteps.Trainman();
                    tuiqinGroup.group.trainman1.ABCD = plan.strABCD1;
                    if (plan.nDriverType1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.driverTypeID = plan.nDriverType1.Value;
                    }
                    if (plan.isKey1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.isKey = plan.isKey1.Value;
                    }
                    if (plan.nPostID1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.postID = plan.nPostID1.Value;
                    }
                    tuiqinGroup.group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    tuiqinGroup.group.trainman1.trainmanName = plan.strTrainmanName1;
                    tuiqinGroup.group.trainman1.trainmanNumber = plan.strTrainmanNumber1;
                    tuiqinGroup.group.trainman2 = new TF.RunSafty.WorkSteps.Trainman();
                    tuiqinGroup.group.trainman2.ABCD = plan.strABCD2;
                    if (plan.nDriverType2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.driverTypeID = plan.nDriverType2.Value;
                    }
                    if (plan.isKey2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.isKey = plan.isKey2.Value;
                    }
                    if (plan.nPostID2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.postID = plan.nPostID2.Value;
                    }
                    tuiqinGroup.group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    tuiqinGroup.group.trainman2.trainmanName = plan.strTrainmanName2;
                    tuiqinGroup.group.trainman2.trainmanNumber = plan.strTrainmanNumber2;
                    tuiqinGroup.group.trainman3 = new TF.RunSafty.WorkSteps.Trainman();
                    tuiqinGroup.group.trainman3.ABCD = plan.strABCD3;
                    if (plan.nDriverType3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.driverTypeID = plan.nDriverType3.Value;
                    }
                    if (plan.isKey3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.isKey = plan.isKey3.Value;
                    }
                    if (plan.nPostID3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.postID = plan.nPostID3.Value;
                    }
                    tuiqinGroup.group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    tuiqinGroup.group.trainman3.trainmanName = plan.strTrainmanName3;
                    tuiqinGroup.group.trainman3.trainmanNumber = plan.strTrainmanNumber3;
                    tuiqinGroup.group.trainman4 = new TF.RunSafty.WorkSteps.Trainman(); ;
                    tuiqinGroup.group.trainman4.ABCD = plan.strABCD4;
                    if (plan.nDriverType4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.driverTypeID = plan.nDriverType4.Value;
                    }
                    if (plan.isKey4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.isKey = plan.isKey4.Value;
                    }
                    if (plan.nPostID4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.postID = plan.nPostID4.Value;
                    }
                    tuiqinGroup.group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    tuiqinGroup.group.trainman4.trainmanName = plan.strTrainmanName4;
                    tuiqinGroup.group.trainman4.trainmanNumber = plan.strTrainmanNumber4;

                    tuiqinGroup.turnMinutes = plan.nTurnMinutes.ToString();
                    tuiqinGroup.turnAlarmMinutes = plan.nTurnAlarmMinutes.ToString();
                    tuiqinGroup.isOver = plan.bIsOver.ToString();
                    tuiqinGroup.signed = plan.bSigned.ToString();
                    if (plan.dtTurnStartTime.HasValue)
                    {
                        tuiqinGroup.turnStartTime = plan.dtTurnStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    //clientPlan.icCheckResult = plan.strICCheckResult;


                    if (plan.nVerifyID1.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID1 = plan.nVerifyID1.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo1 = new TestAlcoholInfo();

                    if (plan.picture1 == null)
                        clientPlan.tuiqinGroup.testAlcoholInfo1.picture = "";
                    else
                        clientPlan.tuiqinGroup.testAlcoholInfo1.picture = plan.picture1;



                    if (plan.cqtime1 == null)
                        clientPlan.tuiqinGroup.testAlcoholInfo1.testTime = "";
                    else
                        clientPlan.tuiqinGroup.testAlcoholInfo1.testTime = Convert.ToDateTime(plan.cqtime1).ToString("yyyy-MM-dd HH:mm:ss");
                    
                    clientPlan.tuiqinGroup.testAlcoholInfo1.testAlcoholResult = plan.cqcj1;

                    if (plan.nVerifyID2.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID2 = plan.nVerifyID2.Value;
                    }





                    clientPlan.tuiqinGroup.testAlcoholInfo2 = new TestAlcoholInfo();
                    if (plan.picture2 == null)
                        clientPlan.tuiqinGroup.testAlcoholInfo2.picture = "";
                    else
                        clientPlan.tuiqinGroup.testAlcoholInfo2.picture = plan.picture2;


                    if (plan.cqtime2 == null)
                        clientPlan.tuiqinGroup.testAlcoholInfo2.testTime = "";
                    else
                        clientPlan.tuiqinGroup.testAlcoholInfo2.testTime = Convert.ToDateTime(plan.cqtime2).ToString("yyyy-MM-dd HH:mm:ss");

                    clientPlan.tuiqinGroup.testAlcoholInfo2.testAlcoholResult = plan.cqcj2;

                    if (plan.nVerifyID3.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID3 = plan.nVerifyID3.Value;
                    }

                    clientPlan.tuiqinGroup.testAlcoholInfo3 = new TestAlcoholInfo();
                    if (plan.picture3 == null)
                        clientPlan.tuiqinGroup.testAlcoholInfo3.picture = "";
                    else
                        clientPlan.tuiqinGroup.testAlcoholInfo3.picture = plan.picture3;

                    if (plan.cqtime3 == null)
                        clientPlan.tuiqinGroup.testAlcoholInfo3.testTime = "";
                    else
                        clientPlan.tuiqinGroup.testAlcoholInfo3.testTime = Convert.ToDateTime(plan.cqtime3).ToString("yyyy-MM-dd HH:mm:ss");

                    clientPlan.tuiqinGroup.testAlcoholInfo3.testAlcoholResult = plan.cqcj3;


                    lPlans.Add(clientPlan);
                }
            }
            return lPlans;
        }


    }
}
