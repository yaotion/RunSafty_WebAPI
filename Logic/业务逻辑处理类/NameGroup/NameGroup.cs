using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;
using TF.Api.Utilities;
namespace TF.RunSafty.BLL

{
    public partial class LCNameGroup
    {
        public void DataRowToGroup(DataRow Row,NameGroup Group)
        {
            Group.groupID = Row["strGroupGUID"].ToString();
            DateTime arriveTime;
            if (DateTime.TryParse(Row["dtLastArriveTime"].ToString(), out arriveTime))
            {
                Group.arriveTime = arriveTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            //正常
            Group.groupState = (int)TRsTrainmanState.tsNormal;
            if (Row["GroupState"].ToString() != "")
            {
                //计划中
                Group.groupState = (int)TRsTrainmanState.tsPlaning;
                if (Row["GroupState"].ToString() == "7")
                {
                    //已出勤
                    Group.groupState = (int)TRsTrainmanState.tsRuning;
                }
            }
            Group.trainPlanID = Row["strTrainPlanGUID"].ToString();
            DateTime lastInRoomTime1, lastInRoomTime2, lastInRoomTime3, lastInRoomTime4;
            if (DateTime.TryParse(Row["InRoomTime1"].ToString(), out lastInRoomTime1))
            {
                Group.lastInRoomTime1 = lastInRoomTime1.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (DateTime.TryParse(Row["InRoomTime2"].ToString(), out lastInRoomTime2))
            {
                Group.lastInRoomTime2 = lastInRoomTime2.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (DateTime.TryParse(Row["InRoomTime3"].ToString(), out lastInRoomTime3))
            {
                Group.lastInRoomTime3 = lastInRoomTime3.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (DateTime.TryParse(Row["InRoomTime4"].ToString(), out lastInRoomTime4))
            {
                Group.lastInRoomTime4 = lastInRoomTime4.ToString("yyyy-MM-dd HH:mm:ss");
            } 
            Group.station.stationID = Row["strStationGUID"].ToString(); ;
            Group.station.stationName = Row["strStationName"].ToString(); ;
            Group.station.stationNumber = Row["strStationNumber"].ToString(); ;
            Group.place.placeID = Row["strPlaceID"].ToString();
            Group.place.placeName = Row["strPlaceName"].ToString();

            Group.trainman1.trainmanID = Row["strTrainmanGUID1"].ToString();
            
            if (Group.trainman1.trainmanID != "")
            {
                Group.trainman1.trainmanName = Row["strTrainmanName1"].ToString();
                Group.trainman1.trainmanNumber = Row["strTrainmanNumber1"].ToString();
                if (Row["nPost1"] != null && !string.IsNullOrEmpty(Row["nPost1"].ToString()))
                {
                    Group.trainman1.postID = Int32.Parse(Row["nPost1"].ToString());
                }
                Group.trainman1.telNumber = Row["strTelNumber1"].ToString();
                Group.trainman1.trainmanState = 0;
                if (Row["nTrainmanState1"].ToString() != "")
                {
                    Group.trainman1.trainmanState = Int32.Parse(Row["nTrainmanState1"].ToString());
                }
                Int32.TryParse(Row["bIsKey1"].ToString(), out Group.trainman1.isKey);
            }

            Group.trainman2.trainmanID = Row["strTrainmanGUID2"].ToString();
            if (Group.trainman2.trainmanID != "")
            {
                Group.trainman2.trainmanName = Row["strTrainmanName2"].ToString();
                Group.trainman2.trainmanNumber = Row["strTrainmanNumber2"].ToString();
                if (Row["nPost2"] != null && !string.IsNullOrEmpty(Row["nPost2"].ToString()))
                {
                    Group.trainman2.postID = Int32.Parse(Row["nPost2"].ToString());
                }
                Group.trainman2.telNumber = Row["strTelNumber2"].ToString();
                Group.trainman2.trainmanState = 0;
                if (Row["nTrainmanState2"].ToString() != "")
                {
                    Group.trainman2.trainmanState = Int32.Parse(Row["nTrainmanState2"].ToString());
                }
                Int32.TryParse(Row["bIsKey2"].ToString(), out Group.trainman2.isKey);
            }
            Group.trainman3.trainmanID = Row["strTrainmanGUID3"].ToString();
            if (Group.trainman3.trainmanID != "")
            { 
                Group.trainman3.trainmanName = Row["strTrainmanName3"].ToString();
                Group.trainman3.trainmanNumber = Row["strTrainmanNumber3"].ToString();
                if (Row["nPost3"] != null && !string.IsNullOrEmpty(Row["nPost3"].ToString()))
                {
                    Group.trainman3.postID = Int32.Parse(Row["nPost3"].ToString());
                }
                Group.trainman3.telNumber = Row["strTelNumber3"].ToString();
                Group.trainman3.trainmanState = 0;
                if (Row["nTrainmanState3"].ToString() != "")
                {
                    Group.trainman3.trainmanState = Int32.Parse(Row["nTrainmanState3"].ToString());
                }
                Int32.TryParse(Row["bIsKey3"].ToString(), out Group.trainman3.isKey);
            }

            Group.trainman4.trainmanID = Row["strTrainmanGUID4"].ToString();
            if (Group.trainman4.trainmanID != "")
            {
                Group.trainman4.trainmanName = Row["strTrainmanName4"].ToString();
                Group.trainman4.trainmanNumber = Row["strTrainmanNumber4"].ToString();
                if (Row["nPost4"] != null && !string.IsNullOrEmpty(Row["nPost4"].ToString()))
                {
                    Group.trainman4.postID = Int32.Parse(Row["nPost4"].ToString());
                }
                Group.trainman4.telNumber = Row["strTelNumber4"].ToString();
                Group.trainman4.trainmanState = 0;
                if (Row["nTrainmanState4"].ToString() != "")
                {
                    Group.trainman4.trainmanState = Int32.Parse(Row["nTrainmanState4"].ToString());
                }
                Int32.TryParse(Row["bIsKey4"].ToString(), out Group.trainman4.isKey);
            }
        }
        /// <summary>
        /// 获取指定交路下指定出勤点的机组信息
        /// </summary>
        /// <param name="TrainmanJiaolu">人员交路ID</param>
        /// <param name="PlaceIDs">出勤点列表</param>
        ///<param name="TrainmanGUID">乘务员GUID列表</param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.InterfaceModel.OrderGroup> GetOrderGroups(string TrainmanJiaolu, List<string> PlaceIDs, string TrainmanGUID)
        {
            string sqlText = @"select *,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4 
              from VIEW_Nameplate_TrainmanJiaolu_Order  where 1=1 ";
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
            List<TF.RunSafty.Model.InterfaceModel.OrderGroup> result = new List<Model.InterfaceModel.OrderGroup>();
            for (int k = 0; k < dtGroups.Rows.Count; k++)
            {
                OrderGroup group = new OrderGroup();
                result.Add(group);
                group.orderID = dtGroups.Rows[k]["strOrderGUID"].ToString();
                group.order = int.Parse(dtGroups.Rows[k]["nOrder"].ToString());
                group.trainmanjiaoluID = dtGroups.Rows[k]["strTrainmanJiaoluGUID"].ToString();
                group.lastArriveTime = Convert.ToDateTime(dtGroups.Rows[k]["dtLastArriveTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                DataRowToGroup(dtGroups.Rows[k], group.group); 
            }           
            return result;
        }


        public List<TF.RunSafty.Model.InterfaceModel.NameGroup> GetOrderInTrainGroups(string strTrainTypeName, string strTrainNumber)
        {
            string strWhere = " 1=1 ";
            if (!string.IsNullOrEmpty(strTrainTypeName))
            {
                strWhere += string.Format(" and strTrainTypeName='{0}' ", strTrainTypeName);
            }
            if (!string.IsNullOrEmpty(strTrainNumber))
            {
                strWhere += string.Format(" and strTrainNumber='{0}' ", strTrainNumber);
            }
            string sqlText = "";
            sqlText =string.Format( @"select *,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4 
              from VIEW_Nameplate_Group  where 1=1 and strGroupGUID in 
( select strGroupGUID from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strTrainGUID in (select strTrainGUID from TAB_Nameplate_TrainmanJiaolu_Train where {0})
) order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime ", strWhere);
            DataTable dtGroups = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText).Tables[0];
            List<TF.RunSafty.Model.InterfaceModel.NameGroup> result = new List<Model.InterfaceModel.NameGroup>();
            for (int k = 0; k < dtGroups.Rows.Count; k++)
            {
                NameGroup group = new NameGroup();
                result.Add(group);
                DataRowToGroup(dtGroups.Rows[k], group);
            }
            return result;
        }

        /// <summary>
        /// 获取指定交路下指定出勤点的轮乘机组列表
        /// </summary>
        /// <param name="TrainmanJiaolu">人员交路ID</param>
        /// <param name="PlaceID">出勤点ID</param>
        ///<param name="TrainmanGUID">乘务员GUID列表</param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.InterfaceModel.NameGroup> GetGroups(string TrainmanJiaolu, List<string> PlaceIDs, string TrainmanGUID)
        {
            System.Data.SqlClient.SqlParameter[] sqlParamsJiaolu = {
                                                                new System.Data.SqlClient.SqlParameter("Trainmanjiaolu",TrainmanJiaolu)                                                      
                                                             };

            string sqlText = @"select nJiaoluType from tab_base_trainmanJiaolu where strTrainmanJiaoluGUID = @TrainmanJiaolu";
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsJiaolu);
            int jiaoluType = Convert.ToInt32(obj);
            sqlText = @"select *,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4 
              from VIEW_Nameplate_Group  where 1=1 ";
            
            if (TrainmanGUID != "")
            {
                sqlText += @"  and (strTrainmanGUID1 = @TrainmanID or strTrainmanGUID2 = @TrainmanID or
                    strTrainmanGUID3 =  @TrainmanID or strTrainmanGUID4 = @TrainmanID) ";
            }
      
            if (TrainmanJiaolu !="")
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
            List<TF.RunSafty.Model.InterfaceModel.NameGroup> result = new List<Model.InterfaceModel.NameGroup>();
            for (int k = 0; k < dtGroups.Rows.Count; k++)
            {
                NameGroup group = new NameGroup();
                result.Add(group);
                DataRowToGroup(dtGroups.Rows[k],group); 
            } 
            return result;
        }

        /// <summary>
        /// 交换轮乘机组的出勤点
        /// </summary>
        /// <param name="GroupID">轮乘机组ID</param>
        /// <param name="SourcePlaceID">交换前出勤点</param>
        /// <param name="DestPlaceID">交换后出勤点</param>
        public bool ChangeGroupPlace(string GroupID,string SourcePlaceID,string DestPlaceID)
        {

            string sqlText = "update TAB_Nameplate_Group set strPlaceID = @DestPlaceID where strGroupGUID = @GroupID";
            SqlParameter[] sqlParams = new SqlParameter[]{ 
                    new SqlParameter("DestPlaceID",DestPlaceID),
                    new SqlParameter("GroupID",GroupID)
                };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams) > 0;
        }




        #region 接口
        #region 获取轮乘机组
        private class OrderGroupQuery : ParamBase
        {
            public OrderGroupQueryParam data;

        }
        public class OrderGroupQueryParam
        {
            [NotNull]
            public string placeID { get; set; }
            [NotNull]
            public string trainmanjiaoluID { get; set; }
            [NotNull]
            public string trainmanID { get; set; }

        } 
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<OrderGroup> data;
        }
        public string GetOrderGroups(string data)
        {
            JsonModel jsonModel = new JsonModel();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                OrderGroupQuery paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderGroupQuery>(data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    string jiaoluID = paramModel.data.trainmanjiaoluID;
                    List<string> placeIDs = new List<string>();
                    string trainmanID = paramModel.data.trainmanID;
                    placeIDs.AddRange(paramModel.data.placeID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    List<OrderGroup> groups = this.GetOrderGroups(jiaoluID, placeIDs, trainmanID);
                    jsonModel.data = groups;
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
                }

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
            return result;
        }
        #endregion

        #region 获取普通机组
        private class OrderGroupQuery1 : ParamBase
        {
            public OrderGroupQueryParam1 data;

        }
        public class OrderGroupQueryParam1
        {
            public string placeID { get; set; }
            [NotNull]
            public string trainmanjiaoluID { get; set; }
            [NotNull]
            public string trainmanID { get; set; }

        }
        public class GroupParam1
        {
            public string planID { get; set; }
            [NotNull]
            public string trainmanjiaoluID { get; set; }
            [NotNull]
            public string trainmanID { get; set; }
        }
        private class JsonModel1
        {
            public int result;
            public string resultStr;
            public List<NameGroup> data;
        }
        public string GetGroups(string data)
        {
            JsonModel1 jsonModel = new JsonModel1();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                OrderGroupQuery1 paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderGroupQuery1>(data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    string jiaoluID = paramModel.data.trainmanjiaoluID;
                    List<string> placeIDs = new List<string>();
                    string trainmanID = paramModel.data.trainmanID;
                    if (!string.IsNullOrEmpty(paramModel.data.placeID))
                    {
                        placeIDs.AddRange(paramModel.data.placeID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    } 
                    List<NameGroup> groups = this.GetGroups(jiaoluID, placeIDs, trainmanID);
                    //List<NameGroup> groups = null;
                    jsonModel.data = groups;
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
                }

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
            return result;
        }
        #endregion


        #region 变化机组出勤地点
        private class InputParam : ParamBase
        {
            public InputData data;

        }
        public class InputData
        {
            [NotNull]
            public string groupID { get; set; }
            [NotNull]
            public string sourcePlaceID { get; set; }
            [NotNull]
            public string destPlaceID { get; set; }

        }
        private class JsonModel2
        {
            public int result;
            public string resultStr;
            public object data = new object();
        }
        public string ChangeGroupPlace(string data)
        {
            JsonModel2 jsonModel = new JsonModel2();
            jsonModel.result = 1;
            jsonModel.resultStr = "数据提交失败：未找到指定的机组信息";
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                InputParam paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InputParam>(data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                { 
                    if (this.ChangeGroupPlace(paramModel.data.groupID, paramModel.data.sourcePlaceID, paramModel.data.destPlaceID))
                    {
                        jsonModel.result = 0;
                        jsonModel.resultStr = "提交成功";
                    }
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
            return result;
        }
        #endregion
        #endregion
    }


}
