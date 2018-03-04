using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using TF.CommonUtility;
using ThinkFreely.DBUtility;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.PS;

namespace TF.RunSafty.NamePlate
{
   
    /// <summary>
    ///类名: Trainman
    ///说明: 获取指定车间的指定人员交路下的非运转人员列表
    /// </summary>
    public class BoardTrainman
    {
        #region "获取指定车间的指定人员交路下的非运转人员列表"
        public class InGetUnRunTrainmans
        {
            //所属车间GUID
            public string WorkShopGUID;
            //所属人员交路GUID
            public string TrainmanJiaoluGUID;
        }
        public class OutGetUnRunTrainmans
        {
            //非运转人员列表
            public TrainmanLeaveList Trainmans = new TrainmanLeaveList();
        }
        /// <summary>
        /// 获取指定车间的指定人员交路下的非运转人员列表
        /// </summary>
        public InterfaceOutPut GetUnRunTrainmans(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetUnRunTrainmans InParams = javaScriptSerializer.Deserialize<InGetUnRunTrainmans>(Data);
                OutGetUnRunTrainmans OutParams = new OutGetUnRunTrainmans();



                string strSql = @"select leave.dBeginTime,leave.dEndTime,leave.strLeaveTypeGUID,leave.strTypeName as strLeaveTypeName,
                        leave.nStatus,* from TAB_Org_Trainman as tm left join VIEW_LeaveMgr_AskLeaveWithTypeName as leave 
                        on  tm.strTrainmanGUID = leave.strTrainManID
                        where nTrainmanState = 0 and (leave.nStatus = 1 or leave.nStatus is null) ";
                if (!string.IsNullOrEmpty(InParams.TrainmanJiaoluGUID))
                    strSql += " and  strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
                if (!string.IsNullOrEmpty(InParams.WorkShopGUID))
                    strSql += " and  strWorkShopGUID=@strWorkShopGUID";
                strSql += " order by strLeaveTypeGUID,dBeginTime,strTrainmanNumber";
                SqlParameter[] sqlParams = new SqlParameter[]{
                        new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID),
                        new SqlParameter("strTrainmanJiaoluGUID",InParams.TrainmanJiaoluGUID)
                    };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TrainmanLeave leave = new TrainmanLeave();
                    leave.strLeaveTypeGUID = dt.Rows[i]["strLeaveTypeGUID"].ToString();
                    leave.strLeaveTypeName = dt.Rows[i]["strLeaveTypeName"].ToString();
                    leave.dBeginTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dBeginTime"],DateTime.Parse("2000-01-01")).ToString("yyyy-MM-dd HH:mm:ss");
                    leave.dEndTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dEndTime"], DateTime.Parse("2000-01-01")).ToString("yyyy-MM-dd HH:mm:ss");

                    PSNameBoard.TrainmanFromDB(leave.Trainman, dt.Rows[i]);
                    OutParams.Trainmans.Add(leave);
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetUnRunTrainmans:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

        #region 获取指定车间的指定人员交路下的预备人员列表

        public class InGetPrepareTrainmans
        {
            //所属车间GUID
            public string WorkShopGUID;
            //所属人员交路GUID
            public string TrainmanJiaoluGUID;
        }
        
        public class OutGetPrepareTrainmans
        {
            //预备人员列表
            public TrainmanNamePlateList Trainmans = new TrainmanNamePlateList();            
        }

        /// <summary>
        /// 获取指定车间的指定人员交路下的预备人员列表
        /// </summary>
        public InterfaceOutPut GetPrepareTrainmans(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetPrepareTrainmans InParams = javaScriptSerializer.Deserialize<InGetPrepareTrainmans>(Data);
                OutGetPrepareTrainmans OutParams = new OutGetPrepareTrainmans();
                string strSql = @"select * from VIEW_Nameplate_TrainmanJiaolu_Prepare where nTrainmanState <> 7 and strWorkShopGUID = @strWorkShopGUID and strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID order by dtLastEndworkTime,strTrainmanNumber";
                SqlParameter[] sqlParams = new SqlParameter[]{
                        new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID),
                        new SqlParameter("strTrainmanJiaoluGUID",InParams.TrainmanJiaoluGUID)
                    };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TrainmanNamePlate trainman = new TrainmanNamePlate();

                    PSNameBoard.TrainmanNamePlateFromDB(trainman, dt.Rows[i]);
                    OutParams.Trainmans.Add(trainman);
                }

                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetPrepareTrainmans:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion
        #region '预备人员排序'
        private class InGetPrepareTMOrders
        {
            //所属车间GUID
            public string WorkShopGUID;
            //所属人员交路GUID
            public string TrainmanJiaoluGUID;
        }
        public class OutGetPrepareTMOrders
         {
             //预备人员列表
            public List<PrepareTMOrder> TMOrders = new List<PrepareTMOrder>();  
         }
        public InterfaceOutPut GetPrepareTMOrders(string Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetPrepareTMOrders InParams = javaScriptSerializer.Deserialize<InGetPrepareTMOrders>(Data);
                OutGetPrepareTMOrders TMOrders = new OutGetPrepareTMOrders();
                TMOrders.TMOrders = LCPrepareTMOrder.GetPrepareOrders(InParams.TrainmanJiaoluGUID);
                output.data = TMOrders;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetPrepareTrainmans:" + ex.Message);
                throw ex;
            }
            return output;
        }

        private class InAddPerpareTrainmanOrder
        {
            public PrepareTMOrder TMOrder = new PrepareTMOrder();
        }
        public InterfaceOutPut AddPerpareTrainmanOrder(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddPerpareTrainmanOrder InParams = javaScriptSerializer.Deserialize<InAddPerpareTrainmanOrder>(Data);
                InterfaceOutPut OutParams = new InterfaceOutPut();
                LCPrepareTMOrder.AddPrepareTMOrder(InParams.TMOrder);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddPerpareTrainmanOrder:" + ex.Message);
                throw ex;
            }
            return output;
        }

        
        
        private class InUpdatePerpareTrainman
        { 
            public PrepareTMOrder Source = new PrepareTMOrder();
            public PrepareTMOrder Dest = new PrepareTMOrder();
        }
        public InterfaceOutPut UpdatePerpareTrainmanOrder(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InUpdatePerpareTrainman InParams = javaScriptSerializer.Deserialize<InUpdatePerpareTrainman>(Data);

                InterfaceOutPut OutParams = new InterfaceOutPut();
                
                DB.DBNameBoard dbNameBoard = new DB.DBNameBoard();
                int sourceOrder = InParams.Source.TrainmanOrder;
                int destOrder = InParams.Dest.TrainmanOrder;
                PrepareTMOrder TMOrder = new PrepareTMOrder();
                //目标位置有效
                if (destOrder > 0)
                {
                    bool hasSource = false;
                    if (sourceOrder > 0)
                    {
                        TMOrder.TrainmanOrder = InParams.Source.TrainmanOrder;
                        TMOrder.PostID = InParams.Source.PostID;
                        TMOrder.TrainmanJiaoluGUID = InParams.Source.TrainmanJiaoluGUID;
                        TMOrder.TrainmanJiaoluName = InParams.Source.TrainmanJiaoluName;
                        TMOrder.TrainmanNumber = InParams.Dest.TrainmanNumber;
                        TMOrder.TrainmanName = InParams.Dest.TrainmanName;
                        hasSource = true;
                    }
                    else
                    {
                        PrepareTMOrder tempOrder = new PrepareTMOrder();
                        if (LCPrepareTMOrder.GetTrainmanOrder(InParams.Source.TrainmanNumber, tempOrder))
                        {
                            TMOrder.TrainmanOrder = tempOrder.TrainmanOrder;
                            TMOrder.PostID = tempOrder.PostID;
                            TMOrder.TrainmanJiaoluGUID = tempOrder.TrainmanJiaoluGUID;
                            TMOrder.TrainmanJiaoluName = tempOrder.TrainmanJiaoluName;
                            TMOrder.TrainmanNumber = InParams.Dest.TrainmanNumber;
                            TMOrder.TrainmanName = InParams.Dest.TrainmanName;
                            hasSource = true;
                        }
                    }
                    if (hasSource)
                    {
                        LCPrepareTMOrder.UpdateTrainmanOrder(TMOrder);
                    }

                    TMOrder.TrainmanOrder = InParams.Dest.TrainmanOrder;
                    TMOrder.PostID = InParams.Dest.PostID;
                    TMOrder.TrainmanJiaoluGUID = InParams.Dest.TrainmanJiaoluGUID;
                    TMOrder.TrainmanJiaoluName = InParams.Dest.TrainmanJiaoluName;
                    TMOrder.TrainmanNumber = InParams.Source.TrainmanNumber;
                    TMOrder.TrainmanName = InParams.Source.TrainmanName;
                    LCPrepareTMOrder.UpdateTrainmanOrder(TMOrder);
                }
                                                       
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.UpdatePerpareTrainman:" + ex.Message);
                throw ex;
            }
            return output;
        }

        private class InDeletePerpareTrainman
        {
            public  PrepareTMOrder TMOrder = new PrepareTMOrder();
        }

        public InterfaceOutPut DeletePerpareTrainmanOrder(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InDeletePerpareTrainman InParams = javaScriptSerializer.Deserialize<InDeletePerpareTrainman>(Data);
                InterfaceOutPut OutParams = new InterfaceOutPut();
                LCPrepareTMOrder.DeletePrepareTrainmanOrder(InParams.TMOrder);                
                
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.DeletePerpareTrainmanOrder:" + ex.Message);
                throw ex;
            }
            return output;
        }

        private class InAddPrepareChangeLog
        {
            public PrepareTMOrderLog ChangeLog = new PrepareTMOrderLog();
        }

        public InterfaceOutPut AddPrepareChangeLog(string Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddPrepareChangeLog InParams = javaScriptSerializer.Deserialize<InAddPrepareChangeLog>(Data);
                InterfaceOutPut OutParams = new InterfaceOutPut();
                LCPrepareTMOrder.AddLog(InParams.ChangeLog);

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddPrepareChangeLog:" + ex.Message);
                throw ex;
            }
            return output;
        }
        private class InQueryPrepareChangeLog
        {
            public DateTime BeginTime;
            public DateTime EndTime;
            public string TMJiaoluGUID;
            public string LogText;            
        }
        public class OutQueryPrepareChangeLog
        {
            //预备人员列表
            public List<PrepareTMOrderLog> Logs = new List<PrepareTMOrderLog>();
        }
        public InterfaceOutPut QueryPrepareChangeLog(string Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InQueryPrepareChangeLog InParams = javaScriptSerializer.Deserialize<InQueryPrepareChangeLog>(Data);               
                OutQueryPrepareChangeLog OutLogs = new OutQueryPrepareChangeLog();
                OutLogs.Logs = LCPrepareTMOrder.QueryLog(InParams.BeginTime, InParams.EndTime, InParams.TMJiaoluGUID, InParams.LogText);

                output.data = OutLogs;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.QueryPrepareChangeLog:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion          

        #region 获取指定车间指定人员交路未在牌人员的人员列表


        /// <summary>
        /// 获取指定车间指定人员交路未在牌人员的人员列表
        /// </summary>
        public InterfaceOutPut GetTMNotInNameGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetPrepareTrainmans InParams = javaScriptSerializer.Deserialize<InGetPrepareTrainmans>(Data);
                OutGetPrepareTrainmans OutParams = new OutGetPrepareTrainmans();

                string str = "";
                if (!string.IsNullOrEmpty(InParams.WorkShopGUID))
                    str += " and strWorkShopGUID = @strWorkShopGUID";

                if (!string.IsNullOrEmpty(InParams.TrainmanJiaoluGUID))
                    str += " and strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";


                string strSql = string.Format(@"SELECT *  FROM  TAB_Org_Trainman where  
                     (
                        nTrainmanState=7 or len(nTrainmanState)=0 or
                           (
                              nTrainmanState=1 and
                                (len(strTrainmanJiaoluGUID)=0 or strTrainmanJiaoluGUID not in(select strTrainmanJiaoluGUID from TAB_Base_TrainmanJiaolu))
                            )
                     ) {0}
                     order by strTrainmanNumber ", str);

                SqlParameter[] sqlParams = new SqlParameter[]{
                        new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID),
                        new SqlParameter("strTrainmanJiaoluGUID",InParams.TrainmanJiaoluGUID)
                    };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TrainmanNamePlate trainman = new TrainmanNamePlate();

                    PSNameBoard.TrainmanNamePlateFromDB(trainman, dt.Rows[i]);
                    OutParams.Trainmans.Add(trainman);
                }

                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetPrepareTrainmans:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion
        
        #region 将指定人员的状态从预备转为空(移出名牌)
        public class InConvertTrainmanStateToNull
        {
            //人员工号
            public string TrainmanNumber;
        }

        /// <summary>
        /// 将指定的预备人员的状态转换为空状态
        /// </summary>
        public InterfaceOutPut ConvertTrainmanStateToNull(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InConvertTrainmanStateToNull InParams = javaScriptSerializer.Deserialize<InConvertTrainmanStateToNull>(Data);
                new DB.DBNameBoard().ConvertTrainmanStateToNull(InParams.TrainmanNumber);                
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ConvertTrainmanStateToNull:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 将指定车间的所有人员的状态从预备转为空(移除名牌)
        public class InConvertAllTrainmanStateToNull
        {
            //所属车间GUID   
            public string WorkShopGUID;
        }

        /// <summary>
        /// 将指定交路下的预备人员的状态全部转换为空状态
        /// </summary>
        public InterfaceOutPut ConvertAllTrainmanStateToNull(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InConvertAllTrainmanStateToNull InParams = javaScriptSerializer.Deserialize<InConvertAllTrainmanStateToNull>(Data);
                string strSql = @"update TAB_Org_Trainman set nTrainmanState = 7  where nTrainmanState <> 7 and strWorkShopGUID = @strWorkShopGUID";
                SqlParameter[] sqlParams = new SqlParameter[] {                     
                    new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ConvertAllTrainmanStateToNull:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 查询人员所在名牌位置
        public class InGetTrainmanJL
        {
            //所查找的人员的工号
            public string TrainmanNumber;
        }

        public class OutGetTrainmanJL
        {
            //是否找到(0未找到,1找到)
            public int IsFind;
            //所在人员交路
            public string strTrainmanJiaoluGUID;
            //人员状态
            public int nTrainmanState;
            //所在出勤点
            public string strPlaceID;
            //所在人员交路名称
            public string strTrainmanJiaoluName = "";
            //所在出勤点名称
            public string strPlaceName;
            //人员状态名称
            public string strTrainmanStateName = "";
            //所属机组名称
            public string strGroupGUID;
            //所在机组位置(1,2,3,4)
            public int nTrainmanIndex;
        }

        /// <summary>
        /// 获取人员所在名牌位置
        /// </summary>
        public InterfaceOutPut GetTrainmanJL(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainmanJL InParams = javaScriptSerializer.Deserialize<InGetTrainmanJL>(Data);
                OutGetTrainmanJL OutParams = new OutGetTrainmanJL();


                OutParams.IsFind = 0;

                string strSql = "select top 1 strTrainmanGUID,nTrainmanState,strTrainmanJiaoluGUID from VIEW_Org_Trainman where strTrainmanNumber = @strTrainmanNumber";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainmanNumber",InParams.TrainmanNumber)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string strTrainmanGUID = dt.Rows[0]["strTrainmanGUID"].ToString();
                    OutParams.IsFind = 1;
                    OutParams.nTrainmanState = int.Parse(dt.Rows[0]["nTrainmanState"].ToString());
                    //OutParams.strTrainmanStateName = dt.Rows[0]["strTrainmanStateName"].ToString();
                    OutParams.strTrainmanJiaoluGUID = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                    // OutParams.strTrainmanJiaoluName = dt.Rows[0]["strTrainmanJiaoluName"].ToString();
                    strSql = @"select top 1 * from  TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
                    SqlParameter[] sqlparamsTrainmanJiaolu = new SqlParameter[]{
                        new SqlParameter("strTrainmanJiaoluGUID",dt.Rows[0]["strTrainmanJiaoluGUID"].ToString()),
                        new SqlParameter("strTrainmanGUID",dt.Rows[0]["strTrainmanGUID"].ToString())
                    };
                    DataTable dtTrainmanJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlparamsTrainmanJiaolu).Tables[0];
                    if (dtTrainmanJiaolu.Rows.Count > 0)
                    {
                        OutParams.strTrainmanJiaoluName = dtTrainmanJiaolu.Rows[0]["strTrainmanJiaoluName"].ToString();
                    }
                    else// 如果人员的人员交路 在字典中找不到，并且当前的人员人员状态是“1”则需要将人员的人员状态改成“7”
                    {
                        if (OutParams.nTrainmanState == 1)
                        {
                            OutParams.nTrainmanState = 7;
                        }
                    }

                    strSql = @"SELECT TAB_Nameplate_Group.*, TAB_Base_TrainmanJiaolu.strTrainmanJiaoluName, VIEW_Nameplate_Group_TrainmanJiaolu.strTrainmanJiaoluGUID, 
                      TAB_Base_DutyPlace.strPlaceName FROM TAB_Nameplate_Group LEFT OUTER JOIN  TAB_Base_DutyPlace ON TAB_Nameplate_Group.strPlaceID = TAB_Base_DutyPlace.strPlaceID LEFT OUTER JOIN
 VIEW_Nameplate_Group_TrainmanJiaolu ON TAB_Nameplate_Group.strGroupGUID = VIEW_Nameplate_Group_TrainmanJiaolu.strGroupGUID LEFT OUTER JOIN TAB_Base_TrainmanJiaolu ON VIEW_Nameplate_Group_TrainmanJiaolu.strTrainmanJiaoluGUID = TAB_Base_TrainmanJiaolu.strTrainmanJiaoluGUID 
where strTrainmanGUID1=@strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID  or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
                    DataTable dtGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlparamsTrainmanJiaolu).Tables[0];
                    if (dtGroup.Rows.Count > 0)
                    {
                        OutParams.strTrainmanJiaoluGUID = dtGroup.Rows[0]["strTrainmanJiaoluGUID"] == null ? "" :dtGroup.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                        OutParams.strTrainmanJiaoluName = dtGroup.Rows[0]["strTrainmanJiaoluName"] == null ? "" : dtGroup.Rows[0]["strTrainmanJiaoluName"].ToString();

                        OutParams.strGroupGUID = dtGroup.Rows[0]["strGroupGUID"] == null ? "" : dtGroup.Rows[0]["strGroupGUID"].ToString();
                        OutParams.strPlaceID = dtGroup.Rows[0]["strPlaceID"] == null ? "" : dtGroup.Rows[0]["strPlaceID"].ToString();
                        OutParams.strPlaceName = dtGroup.Rows[0]["strPlaceName"] == null ? "" : dtGroup.Rows[0]["strPlaceName"].ToString();

                   
                    }

                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainmanJL:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取指定车间下指定类型的非运转人员列表

        public class InGetUnRunTrainmansByType
        {
            //所属车间GUID
            public string WorkShopGUID;
            //非运转类型(以,分割)
            public string Types;
        }

        public class OutGetUnRunTrainmansByType
        {
            //查询到的非运转人员列表
            public TrainmanLeaveList Trainmans = new TrainmanLeaveList();
        }

        /// <summary>
        /// 获取车间内指定类型的非运转人员
        /// </summary>
        public InterfaceOutPut GetUnRunTrainmansByType(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetUnRunTrainmansByType InParams = javaScriptSerializer.Deserialize<InGetUnRunTrainmansByType>(Data);
                OutGetUnRunTrainmansByType OutParams = new OutGetUnRunTrainmansByType();
                string[] types = InParams.Types.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string strLeaveTypes = "";
                for (int i = 0; i < types.Length; i++)
                {
                    if (strLeaveTypes == "")
                    {
                        strLeaveTypes = string.Format("'{0}'", types[i]);
                    }
                    else
                    {
                        strLeaveTypes += string.Format(",'{0}'", types[i]);
                    }
                }
                if (strLeaveTypes == "")
                {
                    strLeaveTypes = string.Format("('{0}')", strLeaveTypes);
                }
                else
                {
                    strLeaveTypes = string.Format("({0})", strLeaveTypes);
                }

                string strSql = @"select *, 
                     (select top 1 strLeaveTypeGUID from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeGUID, 
                     (select top 1 strTypeName from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeName
                     from VIEW_Org_Trainman where strWorkShopGUID=@strWorkShopGUID
                     and nTrainmanState = 0 and 
                     (select top 1 strLeaveTypeGUID from VIEW_LeaveMgr_AskLeaveWithTypeName
                     where strTrainmanID=strTrainmanNumber order by dBeginTime desc) in " + strLeaveTypes + @"  
                    order by (select top 1 strLeaveTypeGUID from VIEW_LeaveMgr_AskLeaveWithTypeName 
                     where strTrainmanID=strTrainmanNumber order by dBeginTime desc),strTrainmanNumber ";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TrainmanLeave leave = new TrainmanLeave();
                    leave.strLeaveTypeGUID = dt.Rows[i]["strLeaveTypeGUID"].ToString();
                    leave.strLeaveTypeName = dt.Rows[i]["strLeaveTypeName"].ToString();
                    PSNameBoard.TrainmanFromDB(leave.Trainman, dt.Rows[i]);
                    OutParams.Trainmans.Add(leave);
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetUnRunTrainmansByType:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取人员所处的机组信息
        public class InGetGroupOfTrainman
        {
            //人员工号
            public string TrainmanNumber;
            //是否包含入寓信息(0否,1是)
            public int HasRestInfo;
        }

        public class OutGetGroupOfTrainman
        {
            //机组信息
            public Group Group = new Group();
            //是否存在
            public int Exist;
        }

        /// <summary>
        /// 获取人员所在机组
        /// </summary>
        public InterfaceOutPut GetGroupOfTrainman(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetGroupOfTrainman InParams = javaScriptSerializer.Deserialize<InGetGroupOfTrainman>(Data);
                OutGetGroupOfTrainman OutParams = new OutGetGroupOfTrainman();
                OutParams.Exist = 0;
                if (InParams.HasRestInfo == 0)
                {
                    //                    string strSql = @"select top 1 * from VIEW_Nameplate_Group 
                    //                        where  strTrainmanNumber1 = @strTrainmanNumber or strTrainmanNumber2=@strTrainmanNumber or strTrainmanNumber3=@strTrainmanNumber or strTrainmanNumber4=@strTrainmanNumber";
                    //                        SqlParameter[] sqlParams = new SqlParameter[]{                         
                    //                        new SqlParameter("strTrainmanNumber",InParams.TrainmanNumber)
                    //                    };

                    string strSql = @"select  * from VIEW_Nameplate_Group 
                        where  strTrainmanNumber1 = '{0}' or strTrainmanNumber2='{0}' or strTrainmanNumber3='{0}' or strTrainmanNumber4='{0}'";

                    strSql = string.Format(strSql, InParams.TrainmanNumber);

                    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        OutParams.Exist = 1;
                        PSNameBoard.GroupFromDB(OutParams.Group, dt.Rows[0]);
                    }
                }
                else
                {
                    string strSql = @"select top 1 *,
                         (select max(dtInRoomTime) from 
		                 TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1 and (DATEPART (Hour,dtInRoomTime) < 4 or DATEPART (Hour,dtInRoomTime) > 12) ) as InRoomTime1,
                      (select max(dtInRoomTime) from 
		                  TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2 and (DATEPART (Hour,dtInRoomTime) < 4 or DATEPART (Hour,dtInRoomTime) > 12) ) as InRoomTime2,
                          (select max(dtInRoomTime) from 
		                  TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3 and (DATEPART (Hour,dtInRoomTime) < 4 or DATEPART (Hour,dtInRoomTime) > 12) ) as InRoomTime3, 
                       (select max(dtInRoomTime) from 
		                  TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4 and (DATEPART (Hour,dtInRoomTime) < 4 or DATEPART (Hour,dtInRoomTime) > 12) ) as InRoomTime4 
                         from VIEW_Nameplate_Group
                         where  strTrainmanNumber1 = @strTrainmanNumber or strTrainmanNumber2=@strTrainmanNumber or strTrainmanNumber3=@strTrainmanNumber or strTrainmanNumber4=@strTrainmanNumber";
                    SqlParameter[] sqlParams = new SqlParameter[]{
                        new SqlParameter("strTrainmanNumber",InParams.TrainmanNumber)
                    };
                    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        OutParams.Exist = 1;
                        PSNameBoard.GroupFromDB(OutParams.Group, dt.Rows[0]);
                    }

                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetGroupOfTrainman:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取人员正在值乘的计划信息
        public class InGetPlanOfTrainman
        {
            //人员工号
            public string TrainmanNumber;
        }

        public class OutGetPlanOfTrainman
        {
            //是否找到
            public int Exist;
            //计划简略信息
            public TrainPlanMin Plan = new TrainPlanMin();
        }

        /// <summary>
        /// 获取人员正在值乘的计划信息
        /// </summary>
        public InterfaceOutPut GetPlanOfTrainman(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {


                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetPlanOfTrainman InParams = javaScriptSerializer.Deserialize<InGetPlanOfTrainman>(Data);
                OutGetPlanOfTrainman OutParams = new OutGetPlanOfTrainman();
                output.data = OutParams;
                string strSql = @"Select  dtStartTime,strTrainNo,strTrainJiaoluGUID,strTrainJiaoluName,nPlanState,strTrainPlanGUID  from VIEW_Plan_Trainman where  
                         strTrainPlanGUID in (select strTrainPlanGUID from TAB_Nameplate_Group 
                         where strTrainmanNumber1 = @strTrainmanNumber or strTrainmanNumber2=@strTrainmanNumber 
                            or strTrainmanNumber3 = @strTrainmanNumber or strTrainmanNumber4 = @strTrainmanNumber)";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainmanNumber",InParams.TrainmanNumber)                    
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                OutParams.Exist = 0;
                if (dt.Rows.Count > 0)
                {
                    OutParams.Exist = 1;
                    PSNameBoard.TrainPlanMinFromDB(OutParams.Plan, dt.Rows[0]);
                }

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetPlanOfTrainman:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion
    }





}