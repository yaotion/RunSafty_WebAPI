using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.Utils.Parse;
using TF.CommonUtility;

namespace TF.RunSafty.NamePlate.PS
{
    /// <summary>
    /// 机组名牌持久化对象
    /// </summary>
    public class PSNameBoard
    {
        //从数据行中读取数据到TrainmanNamePlate对象中
        public static void TrainmanNamePlateFromDB(TrainmanNamePlate tm, DataRow row)
        {
            tm.trainmanID = row["strTrainmanGUID"].ToString();
            tm.trainmanNumber = row["strTrainmanNumber"].ToString();            
            tm.trainmanName = row["strTrainmanName"].ToString();

            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(row["nPostID"], ref  tm.postID))
            {
                tm.postID = 0;
            }

            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(row["nDriverLevel"], ref  tm.driverTypeID))
            {
                tm.driverTypeID = 0;
            }


            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(row["bIsKey"], ref  tm.isKey))
            {
                tm.isKey = 0;
            }


            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(row["nTrainmanState"], ref  tm.trainmanState))
            {
                tm.trainmanState = 0;
            }
            tm.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(row["dtLastEndWorkTime"],DateTime.MinValue);            
            tm.ABCD = row["strABCD"].ToString(); 
            tm.telNumber = row["strTelNumber"].ToString(); 
        }
        //从数据行中读取数据到Trainman对象中
        public static void TrainmanFromDB(Trainman tm, DataRow row)
        {
            tm.trainmanID = row["strTrainmanGUID"].ToString();
            tm.trainmanNumber = row["strTrainmanNumber"].ToString();
            tm.trainmanName = row["strTrainmanName"].ToString();

            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(row["nPostID"], ref  tm.postID))
            {
                tm.postID = 0;
            }

            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(row["nDriverLevel"], ref  tm.driverTypeID))
            {
                tm.driverTypeID = 0;
            }


            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(row["bIsKey"], ref  tm.isKey))
            {
                tm.isKey = 0;
            }


            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(row["nTrainmanState"], ref  tm.trainmanState))
            {
                tm.trainmanState = 0;
            }
                
                      
            tm.ABCD = row["strABCD"].ToString();           
            tm.telNumber = row["strTelNumber"].ToString();           
        }
        //从数据行中读取数据到Group中
        public static void GroupFromDB(Group group,DataRow dr)
        {
            
            group.groupID =dr["strGroupGUID"].ToString();
            group.trainPlanID = dr["strTrainPlanGUID"].ToString();
            group.trainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
            group.trainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
            group.trainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
            group.startTime = ObjectConvertClass.static_ext_string(dr["dtStartTime"]);
            group.trainman1.trainmanID =dr["strTrainmanGUID1"].ToString();
            group.trainman1.trainmanName = dr["strTrainmanName1"].ToString();
            group.trainman1.trainmanNumber = dr["strTrainmanNumber1"].ToString();
            group.trainman1.telNumber = ObjectConvertClass.static_ext_string(dr["strTelNumber1"]);
            group.trainman1.strFixedGroupID =ObjectConvertClass.static_ext_string(dr["strFixedGroupGUID1"]);
            group.trainman1.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dr["dtLastEndworkTime1"],DateTime.MinValue);
            Int32.TryParse(dr["bIsKey1"].ToString(), out group.trainman1.isKey);

            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nTrainmanState1"],ref group.trainman1.trainmanState))
            {
                group.trainman1.trainmanState = 7; //tsNil;
            }
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nPost1"], ref group.trainman1.postID))
            {
                group.trainman1.postID = 0;
            }

            group.trainman2.trainmanID = dr["strTrainmanGUID2"].ToString();
            group.trainman2.trainmanName = dr["strTrainmanName2"].ToString();
            group.trainman2.trainmanNumber = dr["strTrainmanNumber2"].ToString();
            group.trainman2.telNumber = dr["strTelNumber2"].ToString();
            group.trainman2.strFixedGroupID = ObjectConvertClass.static_ext_string(dr["strFixedGroupGUID2"]);
            group.trainman2.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dr["dtLastEndworkTime2"], DateTime.MinValue);
            Int32.TryParse(dr["bIsKey2"].ToString(), out group.trainman2.isKey);

            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nTrainmanState2"], ref group.trainman2.trainmanState))
            {
                group.trainman2.trainmanState = 7; //tsNil;
            }
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nPost2"], ref  group.trainman2.postID))
            {
                group.trainman2.postID = 0;
            }
            


            group.trainman3.trainmanID = dr["strTrainmanGUID3"].ToString();
            group.trainman3.trainmanName = dr["strTrainmanName3"].ToString();
            group.trainman3.trainmanNumber = dr["strTrainmanNumber3"].ToString();
            group.trainman3.telNumber = dr["strTelNumber3"].ToString();
            group.trainman3.strFixedGroupID = ObjectConvertClass.static_ext_string(dr["strFixedGroupGUID3"]);
            group.trainman3.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dr["dtLastEndworkTime3"], DateTime.MinValue);
            Int32.TryParse(dr["bIsKey3"].ToString(), out group.trainman3.isKey);
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nTrainmanState3"], ref group.trainman3.trainmanState))
            {
                group.trainman3.trainmanState = 7; //tsNil;
            }
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nPost3"], ref  group.trainman3.postID))
            {
                group.trainman3.postID = 0;
            }


            group.trainman4.trainmanID = dr["strTrainmanGUID4"].ToString();
            group.trainman4.trainmanName = dr["strTrainmanName4"].ToString();
            group.trainman4.trainmanNumber = dr["strTrainmanNumber4"].ToString();
            group.trainman4.telNumber = dr["strTelNumber4"].ToString();
            group.trainman4.strFixedGroupID = ObjectConvertClass.static_ext_string(dr["strFixedGroupGUID4"]);
            group.trainman4.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dr["dtLastEndworkTime4"], DateTime.MinValue);
            Int32.TryParse(dr["bIsKey4"].ToString(), out group.trainman4.isKey);
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nTrainmanState4"], ref group.trainman4.trainmanState))
            {
                group.trainman4.trainmanState = 7; //tsNil;
            }
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nPost4"], ref  group.trainman4.postID))
            {
                group.trainman4.postID = 0;
            }

            group.arriveTime = TFParse.DBTimeToSerialString(dr["dtLastArriveTime"]);
            group.groupState = 2;//tsNormal;
            if ((dr["GroupState"] != null)   && (DBNull.Value.Equals(dr["GroupState"] ) == false))
            {
                group.groupState = 3;//tsPlaning;
                //psBeginWork
                if (dr["GroupState"].ToString() == "7")

                    group.groupState = 6;// tsRuning;
                
            }
        }
        //从数据行中读取数据到NameGroup中,填充入寓寓休信息
        public static void GroupWithRestFromDB(Group group, DataRow dr)
        {
            GroupFromDB(group,dr);
            group.arriveTime = TFParse.DBTimeToSerialString(dr["dtLastArriveTime"]);
              group.lastInRoomTime1 = TFParse.DBTimeToSerialString(dr["InRoomTime1"]);
              group.lastInRoomTime2 = TFParse.DBTimeToSerialString(dr["InRoomTime2"]);
              group.lastInRoomTime3 =TFParse.DBTimeToSerialString(dr["InRoomTime3"]);
              group.lastInRoomTime4 = TFParse.DBTimeToSerialString(dr["InRoomTime4"]);
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
        //从数据行中读取数据到NamedGroup中
        public static void NamedGroupFromDB(RRsNamedGroup group,DataRow dr)
        {
            GroupFromDB(group.Group, dr);
            group.dtLastArriveTime = TFParse.DBToDateTime(dr["dtLastArriveTime"], DateTime.Parse("1899-01-01"));
            group.strCheciGUID = dr["strCheciGUID"].ToString();
            group.strTrainmanJiaoluGUID = dr["strTrainmanJiaoluGUID"].ToString();
            group.nCheciOrder = TFParse.DBToInt(dr["nCheciOrder"], 0);
            group.nCheciType = TFParse.DBToInt(dr["nCheciType"], 0);
            group.strCheci1 = dr["strCheci1"].ToString();
            group.strCheci2 = dr["strCheci2"].ToString();
        }
        //从数据行中读取数据到NamedGroup中
        public static void OrderGroupFromDB(TF.RunSafty.NamePlate.MD.OrderGroup group, DataRow dr)
        {
            GroupFromDB(group.group, dr);
            group.lastArriveTime = TFParse.DBToDateTime(dr["dtLastArriveTime"], DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:Ss");
            group.orderID = dr["strOrderGUID"].ToString();
            group.trainmanjiaoluID = dr["strTrainmanJiaoluGUID"].ToString();
            group.order = TFParse.DBToInt(dr["nOrder"], 0);
   
        }
        //从数据行中读取数据到TrainPlanMin中
        public static void TrainPlanMinFromDB(TrainPlanMin Plan,DataRow dr)
        {           
            Plan.strTrainNo = dr["strTrainNo"].ToString();
            Plan.strTrainJiaoluGUID = dr["strTrainJiaoluGUID"].ToString();
            Plan.strTrainJiaoluName = dr["strTrainJiaoluName"].ToString();
            Plan.strTrainPlanGUID = dr["strTrainPlanGUID"].ToString();
            Plan.nPlanState = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dr["nPlanState"],0);
            Plan.dtChuQinTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dr["dtStartTime"], DateTime.Parse("1899-01-01"));
        }
        //从数据行中读取数据到TrainPlanMin中
        public static void TuiQinTimeLogFromDB(TuiQinTimeLog Log, DataRow dr)
        {
            Log.strGroupGUID = dr["strGroupGUID"].ToString();
            Log.dtOldArriveTime = TFParse.DBToDateTime(dr["dtOldArriveTime"],DateTime.Parse("1899-01-01"));
            Log.dtNewArriveTime = TFParse.DBToDateTime(dr["dtNewArriveTime"],DateTime.Parse("1899-01-01"));
            Log.strDutyUserNumber = dr["strDutyUserNumber"].ToString();//FieldByName('strDutyUserNumber').AsString;
            Log.strDutyUserName = dr["strDubyUserName"].ToString();//FieldByName('strDubyUserName').AsString;
            Log.dtCreateTime = TFParse.DBToDateTime(dr["dtCreateTime"], DateTime.Parse("1899-01-01"));
        }

    }
}
