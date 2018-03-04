using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.RunSafty.Utils.Parse;
using TF.RunSafty.Plan.MD;
using TF.CommonUtility;

namespace TF.RunSafty.Plan.PS
{
    public class PSPlan
    {
        //从数据行中读取数据到NameGroup中
        public static void GroupFromDB(NameGroup group, DataRow dr)
        {

            group.groupID = dr["strGroupGUID"].ToString();

            group.trainman1.trainmanID = dr["strTrainmanGUID1"].ToString();
            group.trainman1.trainmanName = dr["strTrainmanName1"].ToString();
            group.trainman1.trainmanNumber = dr["strTrainmanNumber1"].ToString();
            group.trainman1.telNumber = dr["strTelNumber1"].ToString();
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nTrainmanState"], ref group.trainman1.trainmanState))
            {
                group.trainman1.trainmanState = 7; //tsNil;
            }
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nPostID1"], ref group.trainman1.trainmanState))
            {
                group.trainman1.postID = 0;
            }





            group.trainman2.trainmanID = dr["strTrainmanGUID2"].ToString();
            group.trainman2.trainmanName = dr["strTrainmanName2"].ToString();
            group.trainman2.trainmanNumber = dr["strTrainmanNumber2"].ToString();
            group.trainman2.telNumber = dr["strTelNumber2"].ToString();

            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nTrainmanState1"], ref group.trainman2.trainmanState))
            {
                group.trainman2.trainmanState = 7; //tsNil;
            }
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nPostID2"], ref  group.trainman2.postID))
            {
                group.trainman2.postID = 0;
            }



            group.trainman3.trainmanID = dr["strTrainmanGUID3"].ToString();
            group.trainman3.trainmanName = dr["strTrainmanName3"].ToString();
            group.trainman3.trainmanNumber = dr["strTrainmanNumber3"].ToString();
            group.trainman3.telNumber = dr["strTelNumber3"].ToString();
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nTrainmanState2"], ref group.trainman3.trainmanState))
            {
                group.trainman3.trainmanState = 7; //tsNil;
            }
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nPostID3"], ref  group.trainman3.postID))
            {
                group.trainman3.postID = 0;
            }


            group.trainman4.trainmanID = dr["strTrainmanGUID4"].ToString();
            group.trainman4.trainmanName = dr["strTrainmanName4"].ToString();
            group.trainman4.trainmanNumber = dr["strTrainmanNumber4"].ToString();
            group.trainman4.telNumber = dr["strTelNumber4"].ToString();
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nTrainmanState3"], ref group.trainman4.trainmanState))
            {
                group.trainman4.trainmanState = 7; //tsNil;
            }
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nPostID4"], ref  group.trainman4.postID))
            {
                group.trainman4.postID = 0;
            }


            group.LastEndworkTime1 = CheckFieldAndConvert_Time(dr, "dtLastEndworkTime1");
            group.LastEndworkTime2 = CheckFieldAndConvert_Time(dr, "dtLastEndworkTime2");
            group.LastEndworkTime3 = CheckFieldAndConvert_Time(dr, "dtLastEndworkTime3");
            group.LastEndworkTime4 = CheckFieldAndConvert_Time(dr, "dtLastEndworkTime4");         
            
            



            //group.arriveTime = dr["dtLastArriveTime"].ToString();
            //group.groupState = 2;//tsNormal;
            //if ((dr["GroupState"] != null) && (DBNull.Value.Equals(dr["GroupState"]) == false))
            //{
            //    group.groupState = 3;//tsPlaning;
            //    //psBeginWork
            //    if (dr["GroupState"].ToString() == "7")

            //        group.groupState = 6;// tsRuning;

            //}
        }

        public static DateTime? CheckFieldAndConvert_Time(DataRow dr,string name)
        {
            if (dr.Table.Columns.Contains(name))
            {

                if (dr[name] == DBNull.Value)
                {
                    return null;
                }
                else
                {
                    return Convert.ToDateTime(dr[name]);
                }                
            }
            else
            {
                return null;
            }
        }

        public static string CheckFieldAndConvert_String(DataRow dr, string name)
        {
            if (dr.Table.Columns.Contains(name))
            {
                if (dr[name] == DBNull.Value)
                {
                    return string.Empty;
                }
                else
                {
                    return dr[name].ToString();
                }         
            }
            else
            {
                return string.Empty;
            }
        }

        public static int CheckFieldAndConvert_Int(DataRow dr, string name)
        {
            if (dr.Table.Columns.Contains(name))
            {
                if (dr[name] == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(dr[name]);
                }
            }
            else
            {
                return 0;
            }
        }

        //从数据行中读取数据到TrainPlan中
        public static void TrainPlanFromDB(TrainPlan Plan, DataRow dr)
        {
            Plan.createSiteGUID = dr["strCreateSiteGUID"].ToString();
            Plan.createSiteName = dr["strCreateSiteName"].ToString();
            Plan.createTime = TFParse.DBToDateTime(dr["dtCreateTime"], DateTime.Parse("1899-01-01"));
            Plan.createUserGUID = dr["strCreateUserGUID"].ToString();
            Plan.createUserName = dr["strCreateUserName"].ToString();

            Plan.dragTypeID = dr["nDragType"].ToString();
            Plan.dragTypeName = dr["nDragTypeName"].ToString();
            Plan.endStationID = dr["strEndStation"].ToString();
            Plan.endStationName = dr["strEndStationName"].ToString();
            Plan.firstStartTime = TFParse.DBToDateTime(dr["dtFirstStartTime"], DateTime.Parse("1899-01-01"));

            Plan.kaiCheTime = TFParse.DBToDateTime(dr["dtChuQinTime"], DateTime.Parse("1899-01-01"));
            Plan.kehuoID = dr["nKehuoID"].ToString();
            Plan.kehuoName = dr["strKehuoName"].ToString();
            Plan.lastArriveTime = TFParse.DBToDateTime(dr["dtLastArriveTime"], DateTime.Parse("1899-01-01"));
            Plan.mainPlanGUID = dr["strMainPlanGUID"].ToString();

            Plan.placeID = dr["strPlaceID"].ToString();
            Plan.placeName = dr["strPlaceName"].ToString();
            Plan.planID = dr["strTrainPlanGUID"].ToString();
            Plan.planStateID = TFParse.DBToInt(dr["nPlanState"], 0);
            Plan.planStateName = dr["strPlanStateName"].ToString();

            Plan.planTypeID = dr["nPlanType"].ToString();
            Plan.planTypeName = dr["strPlanTypeName"].ToString();
            Plan.realStartTime = TFParse.DBToDateTime(dr["dtRealStartTime"], DateTime.Parse("1899-01-01"));
            Plan.remarkTypeID = dr["nRemarkType"].ToString();
            Plan.remarkTypeName = dr["strRemarkTypeName"].ToString();

            Plan.startStationID = dr["strStartStation"].ToString();
            Plan.startStationName = dr["strStartStationName"].ToString();
            Plan.startTime = TFParse.DBToDateTime(dr["dtStartTime"], DateTime.Parse("1899-01-01"));
            Plan.strRemark = dr["strRemark"].ToString();
            Plan.strTrainPlanGUID = dr["strTrainPlanGUID"].ToString();

            Plan.trainJiaoluGUID = dr["strTrainJiaoluGUID"].ToString();
            Plan.trainJiaoluName = dr["strTrainJiaoluName"].ToString();
            Plan.trainmanTypeID = dr["nTrainmanTypeID"].ToString();
            Plan.trainmanTypeName = dr["strTrainmanTypeName"].ToString();
            Plan.trainNo = dr["strTrainNo"].ToString();

            Plan.trainNumber = dr["strTrainNumber"].ToString();
            Plan.trainTypeName = dr["strTrainTypeName"].ToString();
        }
        //从数据行中读取数据到TrainPlan中
        public static void TrainmanPlanFromDB(TF.RunSafty.Plan.MD.TrainmanPlan Plan, DataRow dr)
        {
        
            TrainPlanFromDB(Plan.trainPlan, dr);
            GroupFromDB(Plan.group, dr);
            RestFromDB(Plan.rest, dr);

        }

        //从数据行中读取数据到TrainPlanChangeLog中
        public static void TrainPlanChangeLogFromDB(TrainPlanChangeLog Log,DataRow dr)
        {
            Log.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
            Log.strLogGUID = ObjectConvertClass.static_ext_string(dr["strLogGUID"]);
            Log.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
            Log.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
            Log.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
            Log.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
            Log.dtStartTime = ObjectConvertClass.static_ext_Date(dr["dtStartTime"]);
            Log.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
            Log.strTrainJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
            Log.strStartStation = ObjectConvertClass.static_ext_string(dr["strStartStation"]);
            Log.strStartStationName = ObjectConvertClass.static_ext_string(dr["strStartStationName"]);
            Log.strEndStation = ObjectConvertClass.static_ext_string(dr["strEndStation"]);
            Log.strEndStationName = ObjectConvertClass.static_ext_string(dr["strEndStationName"]);
            Log.nTrainmanTypeID = ObjectConvertClass.static_ext_int(dr["nTrainmanTypeID"]);
            Log.nPlanType = ObjectConvertClass.static_ext_int(dr["nPlanType"]);
            Log.nDragType = ObjectConvertClass.static_ext_int(dr["nDragType"]);
            Log.nKehuoID = ObjectConvertClass.static_ext_int(dr["nKehuoID"]);
            Log.nRemarkType = ObjectConvertClass.static_ext_int(dr["nRemarkType"]);
            Log.strRemark = ObjectConvertClass.static_ext_string(dr["strRemark"]);
            Log.nPlanState = ObjectConvertClass.static_ext_int(dr["nPlanState"]);
            Log.dtCreateTime = ObjectConvertClass.static_ext_Date(dr["dtCreateTime"]);
            Log.strOperateSiteGUID = ObjectConvertClass.static_ext_string(dr["strOperateSiteGUID"]);
            Log.strOperateSiteName = ObjectConvertClass.static_ext_string(dr["strOperateSiteName"]);
            Log.strOperateUserGUID = ObjectConvertClass.static_ext_string(dr["strOperateUserGUID"]);
            Log.strOperateUserName = ObjectConvertClass.static_ext_string(dr["strOperateUserName"]);
            Log.strOperateUserID = ObjectConvertClass.static_ext_string(dr["strOperateUserID"]);
            Log.dtChangeTime = ObjectConvertClass.static_ext_Date(dr["dtChangeTime"]);
        }
        //从数据行中读取数据到TuiQinPlan中
        public static void EndworkFromDB(TuiQinPlan Plan,DataRow dr)
        {
            TrainPlanFromDB(Plan.trainPlan, dr);
            GroupFromDB(Plan.tuiqinGroup.group, dr);
            Plan.beginWorkTime = Plan.trainPlan.kaiCheTime.ToString("yyyy-MM-dd HH:mm:ss");
            //Plan.tuiqinGroup.isOver = dr["bIsOver"].ToString();
            //Plan.tuiqinGroup.signed = dr["bSigned"].ToString();
            Plan.tuiqinGroup.testAlcoholInfo1.testAlcoholResult = TFParse.DBToInt(dr["nDrinkResult1"],0);
            Plan.tuiqinGroup.testAlcoholInfo1.testTime = TFParse.DBToDateTime(dr["dtTestTime1"],DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss");
            //Plan.tuiqinGroup.testAlcoholInfo1.picture = dr["Picture1"].ToString();
            Plan.tuiqinGroup.testAlcoholInfo2.testAlcoholResult = TFParse.DBToInt(dr["nDrinkResult2"], 0);
            Plan.tuiqinGroup.testAlcoholInfo2.testTime = TFParse.DBToDateTime(dr["dtTestTime2"], DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss");
            //Plan.tuiqinGroup.testAlcoholInfo2.picture = dr["Picture2"].ToString();
            Plan.tuiqinGroup.testAlcoholInfo3.testAlcoholResult = TFParse.DBToInt(dr["nDrinkResult3"], 0);
            Plan.tuiqinGroup.testAlcoholInfo3.testTime = TFParse.DBToDateTime(dr["dtTestTime3"], DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss");
            //Plan.tuiqinGroup.testAlcoholInfo3.picture = dr["Picture3"].ToString();
            Plan.tuiqinGroup.testAlcoholInfo4.testAlcoholResult = TFParse.DBToInt(dr["nDrinkResult4"], 0);
            Plan.tuiqinGroup.testAlcoholInfo4.testTime = TFParse.DBToDateTime(dr["dtTestTime4"], DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss");
            //Plan.tuiqinGroup.testAlcoholInfo4.picture = dr["Picture4"].ToString();

            //Plan.tuiqinGroup.turnAlarmMinutes = dr["nTurnAlarmMinutes"].ToString();
            //Plan.tuiqinGroup.turnMinutes = dr["nTurnMinutes"].ToString() ;           
            //Plan.tuiqinGroup.turnStartTime = TFParse.DBToDateTime(dr["dtTurnStartTime"], DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss"); 
            Plan.tuiqinGroup.verifyID1 =  TFParse.DBToInt(dr["nVerifyID1"].ToString(),0);
            Plan.tuiqinGroup.verifyID2 = TFParse.DBToInt(dr["nVerifyID2"].ToString(), 0);
            Plan.tuiqinGroup.verifyID3 = TFParse.DBToInt(dr["nVerifyID3"].ToString(), 0);
            Plan.tuiqinGroup.verifyID4 = TFParse.DBToInt(dr["nVerifyID4"].ToString(), 0);  
        }
        //
        public static void BeginworkFromDB(ChuQinPlan Plan, DataRow dr)
        {
            TrainPlanFromDB(Plan.trainPlan, dr);
            GroupFromDB(Plan.chuqinGroup.group, dr);
            Plan.chuqinGroup.verifyID1 = TFParse.DBToInt(dr["nVerifyID1"], 0);
            Plan.chuqinGroup.testAlcoholInfo1.testAlcoholResult = TFParse.DBToInt(dr["nDrinkResult1"], 0);
            Plan.chuqinGroup.testAlcoholInfo1.testTime = TFParse.DBToDateTime(dr["dtTestTime1"], DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss");

            Plan.chuqinGroup.verifyID2 = TFParse.DBToInt(dr["nVerifyID2"], 0);
            Plan.chuqinGroup.testAlcoholInfo2.testAlcoholResult = TFParse.DBToInt(dr["nDrinkResult2"], 0);
            Plan.chuqinGroup.testAlcoholInfo2.testTime = TFParse.DBToDateTime(dr["dtTestTime2"], DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss");

            Plan.chuqinGroup.verifyID3 = TFParse.DBToInt(dr["nVerifyID3"], 0);
            Plan.chuqinGroup.testAlcoholInfo3.testAlcoholResult = TFParse.DBToInt(dr["nDrinkResult3"], 0);
            Plan.chuqinGroup.testAlcoholInfo3.testTime = TFParse.DBToDateTime(dr["dtTestTime3"], DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss");

            Plan.chuqinGroup.verifyID4 = TFParse.DBToInt(dr["nVerifyID4"], 0);
            Plan.chuqinGroup.testAlcoholInfo4.testAlcoholResult = TFParse.DBToInt(dr["nDrinkResult4"], 0);
            Plan.chuqinGroup.testAlcoholInfo4.testTime = TFParse.DBToDateTime(dr["dtTestTime4"], DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss");
        }
        //从数据行中读取数据到RoomSign
        public static void RoomSignFromDB(RoomSign Sign,DataRow dr,int SignType )
        {
            Sign.strInRoomGUID = dr["strInRoomGUID"].ToString();
            Sign.strTrainPlanGUID = dr["strTrainPlanGUID"].ToString();
            Sign.strTrainmanGUID = dr["strTrainmanGUID"].ToString();
            Sign.strDutyUserGUID = dr["strDutyUserGUID"].ToString();
            Sign.strTrainmanNumber = dr["strTrainmanNumber"].ToString();
            Sign.strTrainmanName = dr["strTrainmanName"].ToString();
            Sign.dtInRoomTime = TFParse.DBToDateTime(dr["dtInRoomTime"], DateTime.Parse("1899-01-01"));
            Sign.SignType = SignType;
        }
        //从数据行中读取数据到Rest中
        public static void RestFromDB(Rest rest, DataRow dr)
        {
            rest.dtArriveTime = TFParse.DBToDateTime(dr["dtArriveTime"], DateTime.Parse("1899-01-01"));
            rest.dtCallTime = TFParse.DBToDateTime(dr["dtCallTime"], DateTime.Parse("1899-01-01"));
            rest.nNeedRest = TFParse.DBToInt(dr["nNeedRest"], 0);            
        }
    }

    public class PSMealTicket
    {
        public static void MealTicketRuleFromDB(MealTicketRule Rule,DataRow DR)
        {
            Rule.strName =  DR["strName"].ToString();
            Rule.strGUID = DR["strGUID"].ToString();
            Rule.strWorkShopGUID = DR["strWorkShopGUID"].ToString(); 
            Rule.iA = TFParse.DBToInt(DR["iA"],0); 
            Rule.iB = TFParse.DBToInt(DR["iB"], 0); 
            Rule.iType = TFParse.DBToInt(DR["iType"], 0); 
        }


        public static void MealTicketCheciFromDB(MealTicketCheCi Rule, DataRow DR)
        {
            Rule.strWorkShopGUID = DR["strWorkShopGUID"].ToString();
            Rule.iType = TFParse.DBToInt(DR["iType"], 0);
            Rule.strQuDuan = DR["strQuDuan"].ToString();
            Rule.strRuleGUID = DR["strRuleGUID"].ToString();
            Rule.strGUID = DR["strGUID"].ToString();
            Rule.strCheCi = DR["strCheCi"].ToString();
            Rule.dtStartTime = TFParse.DBToDateTime(DR["dtStartTime"], DateTime.Parse("1899-01-01"));
            Rule.dtEndTime = TFParse.DBToDateTime(DR["dtEndTime"], DateTime.Parse("1899-01-01"));
            
        }
    }
}
