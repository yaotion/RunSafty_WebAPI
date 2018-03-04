using System;
using System.Text;
using System.Linq;
using TF.CommonUtility;
using ThinkFreely.DBUtility;

using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.NamePlate.PS;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.DB;


namespace TF.RunSafty.NamePlate
{
    
    ///类名: OrderGroup
    ///说明: 获取指定车间的指定人员交路下的无出勤点的人员列表
    /// </summary>
    public class LCOrderGroup
    {
        #region '获取指定车间的指定人员交路下的无出勤点的人员列表'
        public class InGetNullPlaceGroups
        {
            //所属人员交路GUID
            public string TrainmanJiaoluGUID;
        }

        public class OutGetNullPlaceGroups
        {
            //轮乘机组信息
            public List<TF.RunSafty.NamePlate.MD.OrderGroup> Groups = new List<TF.RunSafty.NamePlate.MD.OrderGroup>();
        }

        /// <summary>
        /// 获取指定车间的指定人员交路下的无出勤点的人员列表
        /// </summary>
        public InterfaceOutPut GetNullPlaceGroups(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetNullPlaceGroups InParams = javaScriptSerializer.Deserialize<InGetNullPlaceGroups>(Data);
                OutGetNullPlaceGroups OutParams = new OutGetNullPlaceGroups();


                string strSql = @"select * from VIEW_Nameplate_TrainmanJiaolu_Order  where strTrainmanJiaoluGUID= @strTrainmanJiaoluGUID and (strPlaceID ='' or strPlaceID is null) order by dtLastArriveTime";

                SqlParameter[] sqlParams = new SqlParameter[]{
                        new SqlParameter("strTrainmanJiaoluGUID",InParams.TrainmanJiaoluGUID)
                    };
               DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   TF.RunSafty.NamePlate.MD.OrderGroup ordergroup = new TF.RunSafty.NamePlate.MD.OrderGroup();
                   ordergroup.orderID = dt.Rows[i]["strOrderGUID"].ToString();
                   ordergroup.trainmanjiaoluID = dt.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                   ordergroup.order = int.Parse(dt.Rows[i]["nOrder"].ToString());
                   ordergroup.lastArriveTime = dt.Rows[i]["dtLastArriveTime"].ToString();
                   PSNameBoard.GroupFromDB(ordergroup.group, dt.Rows[i]);
                   OutParams.Groups.Add(ordergroup);
               }               
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetNullPlaceGroups:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取指定编号的轮乘机组
        public class InGetOrderGroup
        {
            //机组GUID
            public string GroupGUID;
        }

        public class OutGetOrderGroup
        {
            //轮乘机组
            public TF.RunSafty.NamePlate.MD.OrderGroup Group = new TF.RunSafty.NamePlate.MD.OrderGroup();
            //是否存在
            public int Exist = 0;  
        }

        /// <summary>
        /// 获取指定编号的轮乘机组
        /// </summary>
        public InterfaceOutPut GetOrderGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetOrderGroup InParams = javaScriptSerializer.Deserialize<InGetOrderGroup>(Data);
                OutGetOrderGroup OutParams = new OutGetOrderGroup();
                string strSql = @"select top 1 * from VIEW_Nameplate_TrainmanJiaolu_Order where strGroupGUID=@strGroupGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",InParams.GroupGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    OutParams.Exist = 1;
                    PS.PSNameBoard.OrderGroupFromDB(OutParams.Group, dt.Rows[0]);
                }
                
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetOrderGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion 获取指定编号的轮乘机组


        #region 修改机组交路
        public InterfaceOutPut ChangeJiaoLu(string Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            Group group = new Group();
            try
            {
                ChangeGrpJlParam InParams = javaScriptSerializer.Deserialize<ChangeGrpJlParam>(Data);
                if (!LCGroup.GetGroup(InParams.GroupGUID, group))
                {
                    throw new Exception("没有找到对应的机组");
                }
                if (!string.IsNullOrEmpty(group.trainPlanID))
                {
                    throw new Exception("该机组已按排计划，不能修改所属交路");
                }

                SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",InParams.GroupGUID),
                new SqlParameter("DestJiaoLu",InParams.DestJiaolu.jiaoluID),
                new SqlParameter("nOrder",0)
                

                };

                string strSql = "select ISNULL(max(nOrder),1) from TAB_Nameplate_TrainmanJiaolu_Order where strTrainmanJiaoluGUID = @DestJiaoLu";

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
                            case 3: break;
                            //包乘
                            case 4:
                                {
                                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strGroupGUID = @strGroupGUID";
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams);
                                    break;
                                }

                        }


                        //更新名牌关联交路
                        strSql = @"update TAB_Nameplate_TrainmanJiaolu_Order set strTrainmanJiaoluGUID = @DestJiaoLu,nOrder = @nOrder where  strGroupGUID = @strGroupGUID";

                        if (Convert.ToInt32(SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams)) == 0)
                        {
                            strSql = @"insert into TAB_Nameplate_TrainmanJiaolu_Order (strTrainmanJiaoluGUID,nOrder,strGroupGUID) values(@DestJiaoLu,@nOrder,@strGroupGUID)";

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
    }  
}
