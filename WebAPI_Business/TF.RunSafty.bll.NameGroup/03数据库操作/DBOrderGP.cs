using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.PS;
using ThinkFreely.DBUtility;
using TF.CommonUtility;

namespace TF.RunSafty.NamePlate.DB
{
   public class DBOrderGP
   {
       #region 获取轮乘机组列表
       /// <summary>
        /// 获取指定交路下指定出勤点的轮乘机组信息
        /// </summary>
        /// <param name="TrainmanJiaolu">人员交路ID</param>
        /// <param name="PlaceIDs">出勤点列表</param>
        ///<param name="TrainmanGUID">乘务员GUID列表</param>
        /// <returns></returns>
        public static List<TF.RunSafty.NamePlate.MD.OrderGroup> GetOrderGroups(string TrainmanJiaolu, List<string> PlaceIDs, string TrainmanGUID)
        {
            string sqlText = @"select *,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4 
              from VIEW_Nameplate_TrainmanJiaolu_Order  where (nTxState = 0 ) or (nTxState is null) ";
            if (TrainmanGUID != "")
            {
                sqlText += @"  and (strTrainmanGUID1 = @TrainmanID or strTrainmanGUID2 = @TrainmanID or
                    strTrainmanGUID3 =  @TrainmanID or strTrainmanGUID4 = @TrainmanID) ";
            }
            if (TrainmanJiaolu != "")
            {
                sqlText += " and strTrainmanJiaoluGUID = @Trainmanjiaolu";
            }
            string placewhere = "";
            if (PlaceIDs.Count > 0)
            {
                for (int i = 0; i < PlaceIDs.Count; i++)
                {
                    if (placewhere == "")
                    {
                        placewhere = PlaceIDs[i];
                    }
                    else
                    {
                        placewhere += "," + PlaceIDs[i];
                    }
                }
                sqlText += " and strPlaceID in (@Place)";
            }
            sqlText += " order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2";
            System.Data.SqlClient.SqlParameter[] sqlParams = {
                                                                new System.Data.SqlClient.SqlParameter("TrainmanID",TrainmanGUID),
                                                                new System.Data.SqlClient.SqlParameter("Trainmanjiaolu",TrainmanJiaolu),
                                                                new System.Data.SqlClient.SqlParameter("Place",placewhere)
                                                             };
            DataTable dtGroups = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams).Tables[0];
            List<TF.RunSafty.NamePlate.MD.OrderGroup> result = new List<TF.RunSafty.NamePlate.MD.OrderGroup>();
            for (int k = 0; k < dtGroups.Rows.Count; k++)
            {
                TF.RunSafty.NamePlate.MD.OrderGroup group = new TF.RunSafty.NamePlate.MD.OrderGroup();
                result.Add(group);
                group.orderID = dtGroups.Rows[k]["strOrderGUID"].ToString();
                group.order = int.Parse(dtGroups.Rows[k]["nOrder"].ToString());
                group.trainmanjiaoluID = dtGroups.Rows[k]["strTrainmanJiaoluGUID"].ToString();
                if (dtGroups.Rows[k]["dtLastArriveTime"] != DBNull.Value)
                {
                    group.lastArriveTime = Convert.ToDateTime(dtGroups.Rows[k]["dtLastArriveTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    group.lastArriveTime = "";
                }

                DataRowToGroup(dtGroups.Rows[k], group.group);
            }
            return result;
        }

        public static void DataRowToGroup(DataRow Row, Group Group)
        {
            Group.groupID = ObjectConvertClass.static_ext_string(Row["strGroupGUID"]);

            DateTime arriveTime;
            if (DateTime.TryParse(ObjectConvertClass.static_ext_string(Row["dtLastArriveTime"]), out arriveTime))
            {
                Group.arriveTime = arriveTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            Group.dtTXBeginTime = ObjectConvertClass.static_ext_Date(Row["dtTXBeginTime"]);
            Group.nTxState = ObjectConvertClass.static_ext_int(Row["nTXState"]);



            //正常
            Group.groupState = (int)TRsTrainmanState.tsNormal;
            if (ObjectConvertClass.static_ext_string(Row["GroupState"]) != "")
            {
                //计划中
                Group.groupState = (int)TRsTrainmanState.tsPlaning;
                if (ObjectConvertClass.static_ext_string(Row["GroupState"]) == "7")
                {
                    //已出勤
                    Group.groupState = (int)TRsTrainmanState.tsRuning;
                }
            }
            Group.trainPlanID = ObjectConvertClass.static_ext_string(Row["strTrainPlanGUID"]);
            Group.trainNo = ObjectConvertClass.static_ext_string(Row["strTrainNo"]);
            Group.trainTypeName = ObjectConvertClass.static_ext_string(Row["strTrainTypeName"]);
            Group.trainNumber = ObjectConvertClass.static_ext_string(Row["strTrainNumber"]);
            Group.startTime = ObjectConvertClass.static_ext_string(Row["dtStartTime"]);

            DateTime lastInRoomTime1, lastInRoomTime2, lastInRoomTime3, lastInRoomTime4;

            if (DateTime.TryParse(ObjectConvertClass.static_ext_string(Row["InRoomTime1"]), out lastInRoomTime1))
            {
                Group.lastInRoomTime1 = lastInRoomTime1.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (DateTime.TryParse(ObjectConvertClass.static_ext_string(Row["InRoomTime2"]), out lastInRoomTime2))
            {
                Group.lastInRoomTime2 = lastInRoomTime2.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (DateTime.TryParse(ObjectConvertClass.static_ext_string(Row["InRoomTime3"]), out lastInRoomTime3))
            {
                Group.lastInRoomTime3 = lastInRoomTime3.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (DateTime.TryParse(ObjectConvertClass.static_ext_string(Row["InRoomTime4"]), out lastInRoomTime4))
            {
                Group.lastInRoomTime4 = lastInRoomTime4.ToString("yyyy-MM-dd HH:mm:ss");
            }

            DateTime LastOutRoomTime1, LastOutRoomTime2, LastOutRoomTime3, LastOutRoomTime4;

            if (Row.Table.Columns.Contains("OutRoomTime1"))
            {
                if (DateTime.TryParse(ObjectConvertClass.static_ext_string(Row["OutRoomTime1"]), out LastOutRoomTime1))
                {
                    Group.LastOutRoomTime1 = LastOutRoomTime1.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (DateTime.TryParse(ObjectConvertClass.static_ext_string(Row["OutRoomTime2"]), out LastOutRoomTime2))
                {
                    Group.LastOutRoomTime2 = LastOutRoomTime2.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (DateTime.TryParse(ObjectConvertClass.static_ext_string(Row["OutRoomTime3"]), out LastOutRoomTime3))
                {
                    Group.LastOutRoomTime3 = LastOutRoomTime3.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (DateTime.TryParse(ObjectConvertClass.static_ext_string(Row["OutRoomTime4"]), out LastOutRoomTime4))
                {
                    Group.LastOutRoomTime4 = LastOutRoomTime4.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }


            Group.station.stationID = ObjectConvertClass.static_ext_string(Row["strStationGUID"]); ;
            Group.station.stationName = ObjectConvertClass.static_ext_string(Row["strStationName"]); ;
            Group.station.stationNumber = ObjectConvertClass.static_ext_string(Row["strStationNumber"]); ;
            Group.place.placeID = ObjectConvertClass.static_ext_string(Row["strPlaceID"]);
            Group.place.placeName = ObjectConvertClass.static_ext_string(Row["strPlaceName"]);

            Group.trainman1.trainmanID = ObjectConvertClass.static_ext_string(Row["strTrainmanGUID1"]);
            if (Group.trainman1.trainmanID != "")
            {
                Group.trainman1.trainmanName = ObjectConvertClass.static_ext_string(Row["strTrainmanName1"]);
                Group.trainman1.trainmanNumber = ObjectConvertClass.static_ext_string(Row["strTrainmanNumber1"]);

                if (Row["nPost1"] != null && !string.IsNullOrEmpty(ObjectConvertClass.static_ext_string(Row["nPost1"])))
                {
                    Group.trainman1.postID = Int32.Parse(Row["nPost1"].ToString());
                }
                Group.trainman1.telNumber = ObjectConvertClass.static_ext_string(Row["strTelNumber1"]);
                Group.trainman1.trainmanState = 0;

                if (Row["nTrainmanState1"].ToString() != "")
                {
                    Group.trainman1.trainmanState = ObjectConvertClass.static_ext_int(Row["nTrainmanState1"]);
                }
                Int32.TryParse(ObjectConvertClass.static_ext_string(Row["bIsKey1"]), out Group.trainman1.isKey);
                Group.trainman1.strFixedGroupID = ObjectConvertClass.static_ext_string(Row["strFixedGroupGUID1"]);
                Group.trainman1.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(Row["dtLastEndworkTime1"], DateTime.MinValue);
            }

            Group.trainman2.trainmanID = ObjectConvertClass.static_ext_string(Row["strTrainmanGUID2"]);
            if (Group.trainman2.trainmanID != "")
            {
                Group.trainman2.trainmanName = ObjectConvertClass.static_ext_string(Row["strTrainmanName2"]);
                Group.trainman2.trainmanNumber = ObjectConvertClass.static_ext_string(Row["strTrainmanNumber2"]);

                if (Row["nPost2"] != null && !string.IsNullOrEmpty(ObjectConvertClass.static_ext_string(Row["nPost2"])))
                {
                    Group.trainman2.postID = ObjectConvertClass.static_ext_int(Row["nPost2"]);
                }
                Group.trainman2.telNumber = ObjectConvertClass.static_ext_string(Row["strTelNumber2"]);
                Group.trainman2.trainmanState = 0;
                if (ObjectConvertClass.static_ext_string(Row["nTrainmanState2"]) != "")
                {
                    Group.trainman2.trainmanState = ObjectConvertClass.static_ext_int(Row["nTrainmanState2"]);
                }
                Int32.TryParse(ObjectConvertClass.static_ext_string(Row["bIsKey2"]), out Group.trainman2.isKey);
                Group.trainman2.strFixedGroupID = ObjectConvertClass.static_ext_string(Row["strFixedGroupGUID2"]);
                Group.trainman2.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(Row["dtLastEndworkTime2"], DateTime.MinValue);
            }
            Group.trainman3.trainmanID = ObjectConvertClass.static_ext_string(Row["strTrainmanGUID3"]);
            if (Group.trainman3.trainmanID != "")
            {
                Group.trainman3.trainmanName = ObjectConvertClass.static_ext_string(Row["strTrainmanName3"]);
                Group.trainman3.trainmanNumber = ObjectConvertClass.static_ext_string(Row["strTrainmanNumber3"]);
                if (Row["nPost3"] != null && !string.IsNullOrEmpty(ObjectConvertClass.static_ext_string(Row["nPost3"])))
                {
                    Group.trainman3.postID = ObjectConvertClass.static_ext_int(Row["nPost3"]);
                }
                Group.trainman3.telNumber = ObjectConvertClass.static_ext_string(Row["strTelNumber3"]);
                Group.trainman3.trainmanState = 0;
                if (ObjectConvertClass.static_ext_string(Row["nTrainmanState3"]) != "")
                {
                    Group.trainman3.trainmanState = ObjectConvertClass.static_ext_int(Row["nTrainmanState3"]);
                }
                Int32.TryParse(ObjectConvertClass.static_ext_string(Row["bIsKey3"]), out Group.trainman3.isKey);
                Group.trainman3.strFixedGroupID = ObjectConvertClass.static_ext_string(Row["strFixedGroupGUID3"]);
                Group.trainman3.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(Row["dtLastEndworkTime3"], DateTime.MinValue);
            }

            Group.trainman4.trainmanID = ObjectConvertClass.static_ext_string(Row["strTrainmanGUID4"]);
            if (Group.trainman4.trainmanID != "")
            {
                Group.trainman4.trainmanName = ObjectConvertClass.static_ext_string(Row["strTrainmanName4"]);
                Group.trainman4.trainmanNumber = ObjectConvertClass.static_ext_string(Row["strTrainmanNumber4"]);
                if (Row["nPost4"] != null && !string.IsNullOrEmpty(ObjectConvertClass.static_ext_string(Row["nPost4"])))
                {
                    Group.trainman4.postID = ObjectConvertClass.static_ext_int(Row["nPost4"]);
                }
                Group.trainman4.telNumber = ObjectConvertClass.static_ext_string(Row["strTelNumber4"]);
                Group.trainman4.trainmanState = 0;
                if (ObjectConvertClass.static_ext_string(Row["nTrainmanState4"]) != "")
                {
                    Group.trainman4.trainmanState = ObjectConvertClass.static_ext_int(Row["nTrainmanState4"]);
                }
                Int32.TryParse(ObjectConvertClass.static_ext_string(Row["bIsKey4"]), out Group.trainman4.isKey);
                Group.trainman4.strFixedGroupID = ObjectConvertClass.static_ext_string(Row["strFixedGroupGUID4"]);
                Group.trainman4.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(Row["dtLastEndworkTime4"], DateTime.MinValue);
            }
        }

        /// <summary>
        /// 获取指定交路下指定出勤点的轮乘机组列表
        /// </summary>
        /// <param name="TrainmanJiaolu">人员交路ID</param>
        /// <param name="PlaceID">出勤点ID</param>
        ///<param name="TrainmanGUID">乘务员GUID列表</param>
        /// <returns></returns>
        public static List<Group> GetGroups(string TrainmanJiaolu, List<string> PlaceIDs, string TrainmanGUID)
        {
            System.Data.SqlClient.SqlParameter[] sqlParamsJiaolu = {
                                                                new System.Data.SqlClient.SqlParameter("Trainmanjiaolu",TrainmanJiaolu)                                                      
                                                             };

            string sqlText = @"select nJiaoluType from tab_base_trainmanJiaolu where strTrainmanJiaoluGUID = @TrainmanJiaolu";
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsJiaolu);
            int jiaoluType = Convert.ToInt32(obj);

            sqlText = @"select *,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1,
                (select max(dtOutRoomTime) from TAB_Plan_OutRoom where strTrainmanNumber = strTrainmanNumber1) as OutRoomTime1,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                (select max(dtOutRoomTime) from TAB_Plan_OutRoom where strTrainmanNumber = strTrainmanNumber2) as OutRoomTime2,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                (select max(dtOutRoomTime) from TAB_Plan_OutRoom where strTrainmanNumber = strTrainmanNumber3) as OutRoomTime3,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4, 
                (select max(dtOutRoomTime) from TAB_Plan_OutRoom where strTrainmanNumber = strTrainmanNumber4) as OutRoomTime4
              from VIEW_Nameplate_Group  where 1=1 and (nTxState = 0 ) or (nTxState is null)";
            //            sqlText = @"select *,
            //                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1,                
            //                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2,                 
            //                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3,                 
            //                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4                 
            //              from VIEW_Nameplate_Group  where 1=1 ";

            if (TrainmanGUID != "")
            {
                sqlText += @"  and (strTrainmanGUID1 = @TrainmanID or strTrainmanGUID2 = @TrainmanID or
                    strTrainmanGUID3 =  @TrainmanID or strTrainmanGUID4 = @TrainmanID) ";
            }

            if (TrainmanJiaolu != "")
            {
                sqlText += " and strTrainmanJiaoluGUID = @Trainmanjiaolu";
            }
            string placewhere = "";
            switch (jiaoluType)
            {
                //named
                case 2:
                    {
                        sqlText += " order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2";
                    }
                    break;
                //order
                case 3:
                    {
                        if (PlaceIDs.Count > 0)
                        {
                            for (int i = 0; i < PlaceIDs.Count; i++)
                            {
                                if (placewhere == "")
                                {
                                    placewhere = PlaceIDs[i];
                                }
                                else
                                {
                                    placewhere += "," + PlaceIDs[i];
                                }
                            }
                            sqlText += " and strPlaceID in (@Place)";
                        }
                        sqlText += " order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2";
                    }
                    break;
                //together
                case 4:
                    {
                        sqlText += " order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2";
                    }
                    break;
            }
            System.Data.SqlClient.SqlParameter[] sqlParams = {
                                                                new System.Data.SqlClient.SqlParameter("TrainmanID",TrainmanGUID),
                                                                new System.Data.SqlClient.SqlParameter("Trainmanjiaolu",TrainmanJiaolu),
                                                                new System.Data.SqlClient.SqlParameter("Place",placewhere)
                                                             };

            DataTable dtGroups = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams).Tables[0];
            List<Group> result = new List<Group>();
            for (int k = 0; k < dtGroups.Rows.Count; k++)
            {
                Group group = new Group();
                result.Add(group);
                DataRowToGroup(dtGroups.Rows[k], group);
            }
            return result;
        }
       #endregion
   }
}
