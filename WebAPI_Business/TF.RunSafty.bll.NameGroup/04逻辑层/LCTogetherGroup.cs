using System;
using System.Text;
using System.Linq;
using TF.CommonUtility;
using ThinkFreely.DBUtility;

using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.PS;

namespace TF.RunSafty.NamePlate
{
    public class LCTogetherGroup
    {
        #region 判断指定的包乘机车是否存在
        public class InExistTrain
        {
            //车型
            public string TrainTypeName;
            //车号
            public string TrainNumber;
        }
        public class OutExistTrain
        {
            //是否存在(1存在,0不存在)
            public int Exist;
        }
        /// <summary>
        /// 判断指定的包乘机车组是否已经存在
        /// </summary>
        public InterfaceOutPut ExistTrain(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
               JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
               InExistTrain InParams = javaScriptSerializer.Deserialize<InExistTrain>(Data);
               string strSql = "select top 1 * from TAB_Nameplate_TrainmanJiaolu_Train where strTrainTypeName = @strTrainTypeName and strTrainNumber = @strTrainNumber ";
               SqlParameter[] sqlParams = new SqlParameter[]{
                   new SqlParameter("strTrainTypeName",InParams.TrainTypeName),
                   new SqlParameter("strTrainNumber",InParams.TrainNumber)
               };
               
               OutExistTrain OutParams = new OutExistTrain();
               OutParams.Exist = 0;
               if (SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0].Rows.Count > 0)
               {
                   OutParams.Exist = 1;
               }
               output.data = OutParams;
               output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ExistTrain:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion


        #region 添加包乘机车信息
        public class InAddTrain
        {
            //所属人员交路GUID
            public string TrainmanJiaoluGUID;
            //包乘机车GUID
            public string TrainGUID;
            //车型
            public string TrainTypeName;
            //车号
            public string TrainNumber;
        }

        /// <summary>
        /// 添加包乘机车信息
        /// </summary>
        public InterfaceOutPut AddTrain(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddTrain InParams = javaScriptSerializer.Deserialize<InAddTrain>(Data);
                string strSql = @"insert into TAB_Nameplate_TrainmanJiaolu_Train 
                     (strTrainGUID,strTrainmanJiaoluGUID,strTrainTypeName,strTrainNumber,dtCreateTime) 
                     values (@strTrainGUID,@strTrainmanJiaoluGUID,@strTrainTypeName,@strTrainNumber,getdate())";
                SqlParameter[] sqlParams = new SqlParameter[]{
                   new SqlParameter("strTrainGUID",InParams.TrainGUID),
                   new SqlParameter("strTrainmanJiaoluGUID",InParams.TrainmanJiaoluGUID),
                   new SqlParameter("strTrainTypeName",InParams.TrainTypeName),
                   new SqlParameter("strTrainNumber",InParams.TrainNumber)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddTrain:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 删除包乘机组
        public class InDeleteTogetherTrain
        {
            //待删除的包乘机车信息
            public string TrainGUID;
        }

        /// <summary>
        /// 删除指定的包乘机车信息
        /// </summary>
        public InterfaceOutPut DeleteTogetherTrain(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InDeleteTogetherTrain InParams = javaScriptSerializer.Deserialize<InDeleteTogetherTrain>(Data);

                string strSql = "delete from TAB_Nameplate_TrainmanJiaolu_Train where strTrainGUID = @strTrainGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainGUID",InParams.TrainGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.DeleteTogetherTrain:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion


        #region 获取指定编号的包乘机组全部信息
        public class InGetTogetherTrain
        {
            //包乘机车GUID
            public string strTrainGUID;
        }

        public class OutGetTogetherTrain
        {
            //包乘机车信息
            public RRsTogetherTrain Train = new RRsTogetherTrain();
        }

        /// <summary>
        /// 获取指定的包乘机车信息
        /// </summary>
        public InterfaceOutPut GetTogetherTrain(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTogetherTrain InParams = javaScriptSerializer.Deserialize<InGetTogetherTrain>(Data);
                OutGetTogetherTrain OutParams = new OutGetTogetherTrain();

                string strSql = @"select * from VIEW_Nameplate_TrainmanJiaolu_TogetherTrain 
                         where strTrainGUID= @strTrainGUID order by dtCreateTime,nOrder";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainGUID",InParams.strTrainGUID)
                };
                DataTable dt =SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];

                bool bFind = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (OutParams.Train.strTrainGUID == dt.Rows[i]["strTrainGUID"].ToString())
                    {
                        RRsOrderGroupInTrain group = new RRsOrderGroupInTrain();
                        group.strOrderGUID = dt.Rows[i]["strOrderGUID"].ToString();
                        group.strTrainGUID = dt.Rows[i]["strTrainGUID"].ToString();
                        group.nOrder = int.Parse(dt.Rows[i]["nOrder"].ToString());
                        group.dtLastArriveTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtLastArriveTime"], DateTime.Parse("1899-01-01")); ;
                        PSNameBoard.GroupFromDB(group.Group,dt.Rows[i]);
                        OutParams.Train.Groups.Add(group);
                        bFind = true;
                    }
                    if (bFind) continue;
                    OutParams.Train.strTrainGUID = dt.Rows[i]["strTrainGUID"].ToString();
                    OutParams.Train.strTrainmanJiaoluGUID =dt.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                    OutParams.Train.strTrainTypeName = dt.Rows[i]["strTrainTypeName"].ToString();
                    OutParams.Train.strTrainNumber = dt.Rows[i]["strTrainNumber"].ToString();
                    if (dt.Rows[i]["strOrderGUID"].ToString() != "")
                    {
                        RRsOrderGroupInTrain group = new RRsOrderGroupInTrain();
                        group.strOrderGUID = dt.Rows[i]["strOrderGUID"].ToString();
                        group.strTrainGUID = dt.Rows[i]["strTrainGUID"].ToString();
                        group.nOrder = int.Parse(dt.Rows[i]["nOrder"].ToString());
                        group.dtLastArriveTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtLastArriveTime"], DateTime.Parse("1899-01-01")); ;
                        PSNameBoard.GroupFromDB(group.Group, dt.Rows[i]);
                        OutParams.Train.Groups.Add(group);
                    }
                }
                
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTogetherTrain:" + ex.Message);
                throw ex;
            }
            return output;
        }    


        #endregion


        #region '获取指定包乘机车中机组正在值乘的计划信息'
        public class InGetPlanOfTrain
        {
            //包乘机车GUID
            public string TrainGUID;
        }

        public class OutGetPlanOfTrain
        {
            //计划信息
            public TrainPlanMin Plan = new TrainPlanMin();
            //是否存在(0不存在，1存在)
            public int Exist;
        }

        /// <summary>
        /// 获取包乘机车中机组正在值乘的假话信息
        /// </summary>
        public InterfaceOutPut GetPlanOfTrain(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetPlanOfTrain InParams = javaScriptSerializer.Deserialize<InGetPlanOfTrain>(Data);
                OutGetPlanOfTrain OutParams = new OutGetPlanOfTrain();
                string strSql = @"Select dtStartTime,strTrainNo,strTrainJiaoluName from VIEW_Plan_Trainman where 
                         strTrainPlanGUID in (select strTrainPlanGUID from VIEW_Nameplate_TrainmanJiaolu_TogetherTrain where strTrainGUID = @strTrainGUID)";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainGUID",InParams.TrainGUID)                    
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
                OutParams.Exist = 0;
                if (dt.Rows.Count > 0)
                {
                    OutParams.Exist = 1;
                    PSNameBoard.TrainPlanMinFromDB(OutParams.Plan, dt.Rows[0]);                   
                }               
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetPlanOfTrain:" + ex.Message);
                throw ex;
            }
            return output;
        }     

        #endregion


        #region '修改包乘机车的车型车号信息'
        public class InUpdateTrainInfo
        {
            //包乘机车GUID
            public string TrainGUID;
            //车型
            public string TrainTypeName;
            //车号
            public string TrainNumber;
        }

        /// <summary>
        /// 修改包乘组的机车信息
        /// </summary>
        public InterfaceOutPut UpdateTrainInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InUpdateTrainInfo InParams = javaScriptSerializer.Deserialize<InUpdateTrainInfo>(Data);
                string strSql = "update TAB_Nameplate_TrainmanJiaolu_Train set strTrainTypeName=@strTrainTypeName,strTrainNumber=@strTrainNumber where strTrainGUID = @strTrainGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainTypeName",InParams.TrainTypeName),
                    new SqlParameter("strTrainNumber",InParams.TrainNumber),
                    new SqlParameter("strTrainGUID",InParams.TrainGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.UpdateTrainInfo:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion '修改包乘机车的车型车号信息'

        #region'修改机组交路'
        public InterfaceOutPut ChangeJiaoLu(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            Group group = new Group();
            try
            {


                ChangeGrpJlParam InParams = javaScriptSerializer.Deserialize<ChangeGrpJlParam>(data);
                if (!LCGroup.GetGroup(InParams.GroupGUID, group))
                {
                    throw new Exception("没有找到对应的机组");
                }
                //可能还需要更新PLACEID


                if (!string.IsNullOrEmpty(group.trainPlanID))
                {
                    throw new Exception("该机组已按排计划，不能修改所属交路");
                }
                SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",InParams.GroupGUID),
                new SqlParameter("DestJiaoLu",InParams.DestJiaolu.jiaoluID),
                new SqlParameter("TrainGUID",InParams.TrainGUID),
                new SqlParameter("nOrder",0)

                };

                //获取最大序号
                string strSql = "select ISNUll(max(nOrder),1) from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strTrainGUID = @TrainGUID";

                int nOrder = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams));

                nOrder++;
                sqlParams[sqlParams.Length - 1].SqlValue = nOrder;

                SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {

                        switch (InParams.SrcJiaolu.jiaoluType)
                        {
                            //记名式
                            case 2:
                                {
                                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_Named where strGroupGUID = @strGroupGUID";
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams);
                                    break;
                                }
                            //轮乘
                            case 3:
                                {
                                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_Order where strGroupGUID = @strGroupGUID";
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams);
                                    break;
                                }
                            //包乘
                            case 4:
                                {
                                    //strSql = "delete from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strGroupGUID = @strGroupGUID";
                                    //SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);                                                      
                                    break;
                                }

                        }

                        strSql = "update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set strTrainGUID = @TrainGUID,nOrder = @nOrder where strGroupGUID = @strGroupGUID";
                        if (Convert.ToInt32(SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams)) == 0)
                        {
                            //插入关联信息
                            strSql = @"insert into TAB_Nameplate_TrainmanJiaolu_OrderInTrain (strTrainGUID,nOrder,strGroupGUID) values(@TrainGUID,@nOrder,@strGroupGUID)";
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams);
                        }




                        //更新人员所属交路
                        strSql = @"update TAB_Org_Trainman set strTrainmanJiaoluGUID = @DestJiaoLu where strTrainmanGUID in (
                        select strTrainmanGUID1 from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID
                        union
                        select strTrainmanGUID2 from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID
                        union
                        select strTrainmanGUID3 from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID
                        union
                        select strTrainmanGUID4 from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID)";


                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams);


                        string strContent = string.Format("机组【{0}】 由【{1}】交路 更改为【{2}】交路", LCGroup.GetGroupString(group), InParams.SrcJiaolu.jiaoluName, InParams.DestJiaolu.jiaoluName);

                        LCGroup.SaveNameplateLog(group, InParams.SrcJiaolu, InParams.DutyUser, LBoardChangeType.btcChangeJiaoLu, strContent);

                        output.result = 0;
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message.ToString();
                output.result = 1;

            }


            return output;
        }                       
        #endregion

        
        #region 通过人员交路获取机车列表
        public class Get_TogetherTrainlistInput
        {
            public string TrainmanJiaoluGUID = string.Empty;
        }

        public class Get_TogetherTrainlistOutput
        {
            public int result;
            public string resultStr;
            public List<RRsTogetherTrain> data = new List<RRsTogetherTrain>();
        }
        public Get_TogetherTrainlistOutput GetTrainList(string Data)
        {
            Get_TogetherTrainlistOutput json = new Get_TogetherTrainlistOutput();
            Get_TogetherTrainlistInput input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_TogetherTrainlistInput>(Data);

            string strSql = "select * from TAB_Nameplate_TrainmanJiaolu_Train where 1=1 ";
            if (!string.IsNullOrEmpty(input.TrainmanJiaoluGUID))
            {
                strSql += string.Format(" and strTrainmanJiaoluGUID ='{0}' ", input.TrainmanJiaoluGUID);
            }
           
            strSql += " order by strTrainTypeName,strTrainNumber";
            try
            {
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                
                RRsTogetherTrain TrainInfo = null;
                foreach (DataRow dataRow in table.Rows)
                {
                    TrainInfo = new RRsTogetherTrain();
                    TrainInfo.strTrainGUID = dataRow["strTrainGUID"].ToString();
                    TrainInfo.strTrainmanJiaoluGUID = dataRow["strTrainmanJiaoluGUID"].ToString();
                    TrainInfo.strTrainNumber = dataRow["strTrainNumber"].ToString();
                    TrainInfo.strTrainTypeName = dataRow["strTrainTypeName"].ToString();
                    json.data.Add(TrainInfo);
                }

                json.result = 0;
                json.resultStr = "获取成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = '1';
                json.resultStr = ex.Message.ToString();

            }
            return json;
        }
        #endregion
        
    }


}
