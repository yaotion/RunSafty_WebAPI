using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.VoiceCard
{
    public class Group
    {
        #region 获取班序
        public class JsonModel
        {
            public int result;
            public string resultStr;
            public GroupIndex data = new GroupIndex();
        }
        public class GroupIndex
        {
            public string strWorkOrder = "";
            public string strStartTime = "";
        }
        private class OrderGroupQuery
        {
            public string strTrainmanNumber { get; set; }
            public string strPhoneNum { get; set; }
            public int nSave { get; set; }
        }
        public class OrderGroupQueryParam
        {
            public string trainmanID { get; set; }
        }

        public class VoiceQueryRecord
        {
            public string strGUID = "";
            public string strTrainmanJiaoluGUID = "";
            public string strTrainmanJiaoluName = "";
            public string strGroupGUID = "";
            public string strTrainmanGUID = "";
            public string strTrainmanNumber = "";
            public string strTrainmanName = "";
            public string strCallerNum = "";
            public DateTime dtQueryTime;
            public DateTime dtLastBeginWorkTime;
            public int nOrder = 0;
        }

        public int GetTrainmanIndex(List<OrderGroup> groups, string trainmanID)
        {
            StringBuilder builder = new StringBuilder();
            int nIndex = 0;
            if (groups != null)
            {
                foreach (OrderGroup group in groups)
                {
                    nIndex++;
                    builder.Append(group.group.trainman1.trainmanName + "_" + group.group.trainman1.trainmanNumber + " ");
                    builder.Append(group.group.trainman2.trainmanName + "_" + group.group.trainman2.trainmanNumber + "\r\n");
                    if (group.group.trainman1.trainmanNumber == trainmanID || group.group.trainman2.trainmanNumber == trainmanID || group.group.trainman3.trainmanNumber == trainmanID)
                    {
                        return nIndex;
                    }
                }
            }
            return 0;
        }

        //保存语音查询记录
        public bool SaveVoiceQuery(VoiceQueryRecord record)
        {
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            SqlCommand command = new SqlCommand();
            connection.ConnectionString = SqlHelper.ConnString;
            try
            {
                connection.Open();
                command.Connection = connection;
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                UpdateVoiceQueryTime(command, record);
                AddVoiceQueryRecrod(command, record);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                command.Dispose();
                connection.Dispose();
            }
            return false;
        }

        //插入语音查询记录
        public void AddVoiceQueryRecrod(SqlCommand command, VoiceQueryRecord record)
        {
            string sqlText = "Insert into TAB_Voice_QueryOrderRecord values(@strGUID,@strTrainmanJiaoluGUID,@strTrainmanJiaoluName,@strGroupGUID,@strTrainmanGUID";
            sqlText = sqlText + ",@strTrainmanNumber,@strTrainmanName,@strCallerNum,@dtQueryTime,@dtLastBeginWorkTime,@nOrder)";
            SqlParameter[] sqlParams = new SqlParameter[]{ 
                    new SqlParameter("strGUID",Guid.NewGuid().ToString()),
                    new SqlParameter("strTrainmanJiaoluGUID",record.strTrainmanJiaoluGUID),
                    new SqlParameter("strTrainmanJiaoluName",record.strTrainmanJiaoluName),
                    new SqlParameter("strGroupGUID",record.strGroupGUID),
                    new SqlParameter("strTrainmanGUID",record.strTrainmanGUID),
                    new SqlParameter("strTrainmanNumber",record.strTrainmanNumber),
                    new SqlParameter("strTrainmanName",record.strTrainmanName),
                    new SqlParameter("strCallerNum",record.strCallerNum),
                    new SqlParameter("dtQueryTime",record.dtQueryTime),
                    new SqlParameter("dtLastBeginWorkTime",record.dtLastBeginWorkTime),
                    new SqlParameter("nOrder",record.nOrder)
                };
            command.CommandText = sqlText;
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParams);
            command.ExecuteNonQuery();
        }

        //更新最后一次语音查询班序时间
        public void UpdateVoiceQueryTime(SqlCommand command, VoiceQueryRecord record)
        {
            string sqlText = "update TAB_Nameplate_TrainmanJiaolu_Order set dtLastVoiceQueryTime = @dtLastVoiceQueryTime where strGroupGUID = @strGoupGUID";
            SqlParameter[] sqlParams = new SqlParameter[]{ 
                    new SqlParameter("dtLastVoiceQueryTime",record.dtQueryTime),
                    new SqlParameter("strGoupGUID",record.strGroupGUID)
                };

            command.CommandText = sqlText;
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParams);
            command.ExecuteNonQuery();
        }
        public JsonModel GetBanXuByTrainmanGUID(string data)
        {
            JsonModel jsonModel = new JsonModel();
            TF.RunSafty.BLL.LCNameGroup bllNameGroup = new RunSafty.BLL.LCNameGroup();
            try
            {
                OrderGroupQuery paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderGroupQuery>(data);
                //string jiaoluID = "";
                VoiceQueryRecord record = new VoiceQueryRecord();
                //根据工号获取人员交路GUID
                //jiaoluID = GetTrainmanJiaoluGUIDByNumber(paramModel.strTrainmanNumber);
                GetTrainmanBaseInfoByNumber(paramModel.strTrainmanNumber, ref record);

                record.strCallerNum = paramModel.strPhoneNum;

                
                List<string> placeIDs = new List<string>();
                string trainmanID = paramModel.strTrainmanNumber;
                string strLatestStartTime = GetLatestStartTimeOfTrainjiaolu(record.strTrainmanJiaoluGUID);
                List<OrderGroup> groups = GetOrderGroups(record.strTrainmanJiaoluGUID, placeIDs, "");
                int nIndex = GetTrainmanIndex(groups, record.strTrainmanNumber);
                if (nIndex > 0)
                {
                    jsonModel.data.strWorkOrder = nIndex.ToString();
                    jsonModel.data.strStartTime = strLatestStartTime;

                    if (paramModel.nSave == 1)
                    {
                        record.dtQueryTime = DateTime.Now;
                        record.nOrder = nIndex;
                        record.strGroupGUID = groups[nIndex - 1].group.groupID;
                        if (strLatestStartTime != "")
                        {
                            record.dtLastBeginWorkTime = Convert.ToDateTime(strLatestStartTime);
                        }
                        SaveVoiceQuery(record);
                    }
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
                }
                else
                {
                    jsonModel.result = 1;
                    jsonModel.resultStr = "提交失败,找不到乘务员所属机组信息";
                }

            }
            catch (Exception ex)
            {
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            return jsonModel;
        }




        /// 获取指定交路下指定出勤点的机组信息
        /// </summary>
        /// <param name="TrainmanJiaolu">人员交路ID</param>
        /// <param name="PlaceIDs">出勤点列表</param>
        ///<param name="TrainmanGUID">乘务员GUID列表</param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.InterfaceModel.OrderGroup> GetOrderGroups(string TrainmanJiaolu, List<string> PlaceIDs, string TrainmanGUID)
        {
            //根据乘务员工号找到StationGUID
            object stationGUID = null;
            string strSql = @"select strStationGUID from tab_nameplate_group where strTrainmanGUID1 = @TrainmanID
                        or strTrainmanGUID2 = @TrainmanID or
                    strTrainmanGUID3 =  @TrainmanID or strTrainmanGUID4 = @TrainmanID";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("TrainmanID", TrainmanGUID), };
            if (!string.IsNullOrEmpty(TrainmanGUID))
            { stationGUID = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters); }
            string sqlText = @"select *,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4 
              from VIEW_Nameplate_TrainmanJiaolu_Order  where 1=1 and (nTxState = 0 ) or (nTxState is null) ";
            if (TrainmanGUID != "")
            {
                sqlText += @"  and (strTrainmanGUID1 = @TrainmanID or strTrainmanGUID2 = @TrainmanID or
                    strTrainmanGUID3 =  @TrainmanID or strTrainmanGUID4 = @TrainmanID) ";
            }
            if (stationGUID != null)
            {
                sqlText += " and strStationGUID=@strStationGUID";
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
                                                                new System.Data.SqlClient.SqlParameter("Place",placewhere),
                                                                new SqlParameter("strStationGUID",stationGUID), 
                                                                
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
                DateTime dtLastArriveTime;
                DateTime.TryParse(dtGroups.Rows[k]["dtLastArriveTime"] == null ? "" : dtGroups.Rows[k]["dtLastArriveTime"].ToString(), out dtLastArriveTime);
                group.lastArriveTime = dtLastArriveTime.ToString("yyyy-MM-dd HH:mm:ss");
                DataRowToGroup(dtGroups.Rows[k], group.group);
            }
            return result;
        }
        public void DataRowToGroup(DataRow Row, NameGroup Group)
        {
            Group.groupID = Row["strGroupGUID"].ToString();
            DateTime arriveTime;
            if (DateTime.TryParse(Row["dtLastArriveTime"].ToString(), out arriveTime))
            {
                Group.arriveTime = arriveTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            //正常
            Group.groupState = 2;
            if (Row["GroupState"].ToString() != "")
            {
                //计划中
                Group.groupState = 3;
                if (Row["GroupState"].ToString() == "7")
                {
                    //已出勤
                    Group.groupState = 6;
                }
            }

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
            }
        }
        public string GetTrainmanJiaoluGUIDByNumber(string strTrainmanNumber)
        {
            string sqlText = string.Format("select strTrainmanJiaoluGUID from TAB_Org_Trainman where strTrainmanNumber='{0}' ", strTrainmanNumber);
            SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("strTrainmanNumber", strTrainmanNumber) };
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText).Tables[0];
            TF.CommonUtility.LogClass.log(sqlText);
            if (table != null && table.Rows.Count > 0)
            {
                TF.CommonUtility.LogClass.log("strTrainmanNumber:" + strTrainmanNumber);
                TF.CommonUtility.LogClass.log("strTrainmanJiaoluGUID:" + table.Rows[0]["strTrainmanJiaoluGUID"].ToString());

                return table.Rows[0]["strTrainmanJiaoluGUID"].ToString();
            }
            return string.Empty;
        }
        public string GetTrainmanJiaoluNameByGUID(string strTrainmanJiaoluGUID)
        {
            string sqlText = string.Format("select strTrainmanJiaoluName from TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID='{0}' ", strTrainmanJiaoluGUID);
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText).Tables[0];
            TF.CommonUtility.LogClass.log(sqlText);
            if (table != null && table.Rows.Count > 0)
            {
                return table.Rows[0]["strTrainmanJiaoluName"].ToString();
            }
            return "";
        }
        public bool GetTrainmanBaseInfoByNumber(string strTrainmanNumber, ref VoiceQueryRecord record)
        {
            string sqlText = string.Format("select strTrainmanJiaoluGUID,strTrainmanName,strTrainmanGUID from TAB_Org_Trainman where strTrainmanNumber='{0}' ", strTrainmanNumber);
            SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("strTrainmanNumber", strTrainmanNumber) };
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText).Tables[0];
            if (table != null && table.Rows.Count > 0)
            {
              
                table.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                record.strTrainmanNumber = strTrainmanNumber;
                record.strTrainmanName = table.Rows[0]["strTrainmanName"].ToString();
                record.strTrainmanGUID = table.Rows[0]["strTrainmanGUID"].ToString();
            }

            sqlText = string.Format("SELECT TOP 1 strTrainmanJiaoluGUID,strTrainmanJiaoluName FROM VIEW_Nameplate_Group where strTrainmanNumber1 = '{0}' or strTrainmanNumber2 = '{0}' or strTrainmanNumber3 = '{0}' or strTrainmanNumber4 = '{0}'", strTrainmanNumber);
            table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText).Tables[0];
            if (table != null && table.Rows.Count > 0)
            {
                record.strTrainmanJiaoluGUID = table.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                record.strTrainmanJiaoluName = GetTrainmanJiaoluNameByGUID(record.strTrainmanJiaoluGUID);
                return true;
            }
            return false;
        }
        #endregion



        /// <summary>
        /// 根据人员交路GUID找到最晚的计划出勤时间
        /// </summary>
        /// <param name="strTrainmanjiaolu"></param>
        /// <returns></returns>
        public string GetLatestStartTimeOfTrainjiaolu(string strTrainmanjiaolu)
        {
            string sqlText = @"select MAX(dtStartTime) from VIEW_Plan_BeginWork
where nPlanState=@nPlanState
and strTrainJiaoluGUID in
(select strTrainJiaoluGUID from TAB_Base_JiaoluRelation
where [strTrainmanJiaoluGUID]=@strTrainmanJiaoluGUID)";
            SqlParameter[] sqlParams = new SqlParameter[]{ 
                    new SqlParameter("strTrainmanJiaoluGUID",strTrainmanjiaolu), 
                    new SqlParameter("nPlanState",(int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psPublish)
                };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
            if (obj != null)
            {
                DateTime dtStartTime;
                if (DateTime.TryParse(obj.ToString(), out dtStartTime))
                {
                    return dtStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
