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
    public class DBNameBoard
    {
        #region 私有方法
        //判断乘务员是已经被安排在值乘别的计划
        public static bool TrainmanIsNormal(string TrainmanGUID)
        {
            if (TrainmanGUID == "") return false;
            string strSql = @"Select top 1 * from VIEW_Plan_Trainman where  strTrainPlanGUID in (select strTrainPlanGUID from TAB_Nameplate_Group 
                                where strTrainmanGUID1 = @TrainmanGUID or strTrainmanGUID2=@TrainmanGUID or strTrainmanGUID3 = @TrainmanGUID or strTrainmanGUID4 = @TrainmanGUID)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                 new SqlParameter("TrainmanGUID",TrainmanGUID)
            };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0].Rows.Count > 0;
        }

        public static bool GetTrainman(string TrainmanNumber, out  TrainmanMin TM)
        {
            TM = new TrainmanMin();
            if (TrainmanNumber == "") return false;
            string strSql = @"Select top 1 * from TAB_Org_Trainman where  strTrainmanNumber = @strTrainmanNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                 new SqlParameter("strTrainmanNumber",TrainmanNumber)
            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count == 0) return false;

            TM.strTrainmanGUID = dt.Rows[0]["strTrainmanGUID"].ToString();
            TM.strTrainmanNumber = dt.Rows[0]["strTrainmanNumber"].ToString();
            TM.strTrainmanName = dt.Rows[0]["strTrainmanName"].ToString();

            int nState = 7;  //tsnil
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[0]["nTrainmanState"], ref nState))
            {
                nState = 7;
            }
            if (nState == 0)
            {
                throw new Exception("司机：" + TM.strTrainmanName + " 处于请假状态，不能添加机组");
            }

            TM.nTrainmanState = nState;
            return true;

        }
        //检测人员是否处于请假或则和已安排计划状态中(不能派班)
        public static bool CheckTrainman(string TrainmanNumber, ref string checkBrief)
        {
            TrainmanMin tm = null;
            if (TrainmanNumber != "")
            {
                if (!GetTrainman(TrainmanNumber, out tm))
                {
                    checkBrief = string.Format("[{0}]不存在!", TrainmanNumber);
                    return false;
                }
                if (TrainmanIsNormal(tm.strTrainmanGUID))
                {
                    checkBrief = string.Format("[{0}]{1}正在值乘别的计划不能添加人员!", tm.strTrainmanNumber, tm.strTrainmanName);
                    return false;
                }
                //tsUnRuning
                if (tm.nTrainmanState == 0)
                {
                    checkBrief = string.Format("[{0}]{1}处于休假状态,请先完成销假再安排名牌!", tm.strTrainmanNumber, tm.strTrainmanName);
                    return false;
                }
            }
            return true;
        }
        //判断人员是否处于别的机组中
        public static bool TrainmanInGroup(string TrainmanNumber)
        {

            string strSql = @"select top 1 * from VIEW_Nameplate_Group 
                 where  strTrainmanNumber1 = @TrainmanNumber or strTrainmanNumber2=@TrainmanNumber or strTrainmanNumber3=@TrainmanNumber or strTrainmanNumber4=@TrainmanNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                  new SqlParameter("TrainmanNumber",TrainmanNumber)
              };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0].Rows.Count > 0;
        }
        //检测人员是否处于别的机组中
        public static bool CheckTrainmanInGroup(string TrainmanNumber, ref string checkBrief)
        {
            if (TrainmanNumber != "")
            {
                if (TrainmanInGroup(TrainmanNumber))
                {
                    checkBrief = string.Format("[{0}]已经安排在别的机组中!", TrainmanNumber);
                    return false;
                }
            }
            return true;
        }

        //修改人员的人员交路
        public static void UpdateTrainmanTrainmanJiaolu(string TrainmanGUID, string TrainmanJiaoLuGUID)
        {
            string strSql = "update TAB_Org_Trainman set strTrainmanJiaoluGUID  = @strTrainmanJiaoluGUID  where strTrainmanGUID = @strTrainmanGUID ";
            SqlParameter[] sqlParams = new SqlParameter[]{
                new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoLuGUID),
                new SqlParameter("strTrainmanGUID",TrainmanGUID)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }
        //修改机组内人员的所属人员交路为指定的人员交路
        public static void UpdateTrainmanJiaoLuToGroup(Group group, string TrainmanJiaoLuGUID)
        {
            if (group.trainman1.trainmanID != "")
            {
                UpdateTrainmanTrainmanJiaolu(group.trainman1.trainmanID, TrainmanJiaoLuGUID);
            }
            if (group.trainman2.trainmanID != "")
            {
                UpdateTrainmanTrainmanJiaolu(group.trainman2.trainmanID, TrainmanJiaoLuGUID);
            }
            if (group.trainman3.trainmanID != "")
            {
                UpdateTrainmanTrainmanJiaolu(group.trainman3.trainmanID, TrainmanJiaoLuGUID);
            }
            if (group.trainman4.trainmanID != "")
            {
                UpdateTrainmanTrainmanJiaolu(group.trainman4.trainmanID, TrainmanJiaoLuGUID);
            }

        }

        public static bool GetGroup(string GroupGUID, Group G)
        {
            string strSql = "select top 1 * from VIEW_Nameplate_Group   where  strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                PSNameBoard.GroupFromDB(G, dt.Rows[0]);
                return true;
            }
            return false;
        }
        //添加记名式机组
        public static void AddNamedGroup(string TrainmanJiaoluGUID, RRsNamedGroup namedGroup)
        {

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {

                    //创建机组
                    string strSql = @"insert into TAB_Nameplate_Group 
                                (strGroupGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4)
                                values (@strGroupGUID,@strTrainmanGUID1,@strTrainmanGUID2,@strTrainmanGUID3,@strTrainmanGUID4)";
                    SqlParameter[] sqlParamsGroup = new SqlParameter[]{
                new SqlParameter("strGroupGUID",namedGroup.Group.groupID),
                new SqlParameter("strTrainmanGUID1",namedGroup.Group.trainman1.trainmanID),
                new SqlParameter("strTrainmanGUID2",namedGroup.Group.trainman2.trainmanID),
                new SqlParameter("strTrainmanGUID3",namedGroup.Group.trainman3.trainmanID),
                new SqlParameter("strTrainmanGUID4",namedGroup.Group.trainman4.trainmanID)
            };
                    if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsGroup) == 0)
                    {
                        throw new Exception("创建机组错误");
                    }

                    //获取交路车间信息
                    strSql = "select strTrainJiaoluGUID,strWorkShopGUID,strTrainmanJiaoluGUID from  VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
                    SqlParameter[] sqlParamsTMJL = new SqlParameter[]{
                new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID)
            };
                    string trainJiaolu = "";
                    string workShopGUID = "";
                    string trainManJiaolu = "";
                    DataTable dtJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTMJL).Tables[0];

                    if (dtJiaolu.Rows.Count > 0)
                    {
                        trainJiaolu = dtJiaolu.Rows[0]["strTrainJiaoluGUID"].ToString();
                        workShopGUID = dtJiaolu.Rows[0]["strWorkShopGUID"].ToString();
                        trainManJiaolu = dtJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();

                    }

                    //修改人员所属行车区段、车间、状态
                    if ((namedGroup.Group.trainman1.trainmanID != "") && (namedGroup.Group.trainman1.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                      new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",namedGroup.Group.trainman1.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改司机状态错误");

                        }
                    }

                    if ((namedGroup.Group.trainman2.trainmanID != "") && (namedGroup.Group.trainman2.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                      new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",namedGroup.Group.trainman2.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改副司机状态错误");

                        }
                    }

                    if ((namedGroup.Group.trainman3.trainmanID != "") && (namedGroup.Group.trainman3.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                      new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",namedGroup.Group.trainman3.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改学员1状态错误");

                        }
                    }

                    if ((namedGroup.Group.trainman4.trainmanID != "") && (namedGroup.Group.trainman4.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                      new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",namedGroup.Group.trainman4.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改学员2状态错误");

                        }
                    }


                    //添加记名式机组
                    strSql = @"insert into TAB_Nameplate_TrainmanJiaolu_Named 
                (strCheciGUID,strTrainmanJiaoluGUID,nCheciOrder,nCheciType,strCheci1,strCheci2,strGroupGUID,dtLastArriveTime) 
                (select @CheciGUID,@strTrainmanJiaoluGUID, (case  when max(nCheCiOrder) is null then 1 else max(nCheCiOrder) + 1 end) ,@CheciType,@Checi1,@Checi2,@GroupGUID,getdate() from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID)";
                    SqlParameter[] sqlParamsNamed = new SqlParameter[]{
                new SqlParameter("CheciGUID",namedGroup.strCheciGUID),
                new SqlParameter("strTrainmanJiaoluGUID",namedGroup.strTrainmanJiaoluGUID),
                new SqlParameter("CheciType",namedGroup.nCheciType),
                new SqlParameter("Checi1",namedGroup.strCheci1),
                new SqlParameter("Checi2",namedGroup.strCheci2),
                new SqlParameter("GroupGUID",namedGroup.Group.groupID)
                    };
                    if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsNamed) == 0)
                    {
                        throw new Exception("创建记名式机组错误");
                    }
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
        //添加轮乘机组
        public static void AddOrderGroup(string TrainmanJiaoluGUID, TF.RunSafty.NamePlate.MD.OrderGroup orderGroup)
        {

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {

                    //创建机组
                    string strSql = @"insert into TAB_Nameplate_Group 
                (strGroupGUID,strZFQJGUID,strStationGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strPlaceID) 
                values (@strGroupGUID,@strZFQJGUID,@strStationGUID,@strTrainmanGUID1,@strTrainmanGUID2,@strTrainmanGUID3,@strTrainmanGUID4,@strPlaceID)";
                    SqlParameter[] sqlParamsGroup = new SqlParameter[]{
                new SqlParameter("strGroupGUID",orderGroup.group.groupID),
                new SqlParameter("strZFQJGUID",""),
                new SqlParameter("strStationGUID",orderGroup.group.station.stationID),
                new SqlParameter("strTrainmanGUID1",orderGroup.group.trainman1.trainmanID),
                new SqlParameter("strTrainmanGUID2",orderGroup.group.trainman2.trainmanID),
                new SqlParameter("strTrainmanGUID3",orderGroup.group.trainman3.trainmanID),
                new SqlParameter("strTrainmanGUID4",orderGroup.group.trainman4.trainmanID),
                new SqlParameter("strPlaceID",orderGroup.group.place.placeID)
            };
                    if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsGroup) == 0)
                    {
                        throw new Exception("创建机组错误");
                    }

                    //获取交路车间信息
                    strSql = "select strTrainJiaoluGUID,strWorkShopGUID,strTrainmanJiaoluGUID from  VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
                    SqlParameter[] sqlParamsTMJL = new SqlParameter[]{
                new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID)
            };
                    string trainJiaolu = "";
                    string workShopGUID = "";
                    string trainManJiaolu = "";
                    DataTable dtJiaolu = SqlHelper.ExecuteDataset(trans, CommandType.Text, strSql, sqlParamsTMJL).Tables[0];

                    if (dtJiaolu.Rows.Count > 0)
                    {
                        trainJiaolu = dtJiaolu.Rows[0]["strTrainJiaoluGUID"].ToString();
                        workShopGUID = dtJiaolu.Rows[0]["strWorkShopGUID"].ToString();
                        trainManJiaolu = dtJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                    }

                    //修改人员所属行车区段、车间、状态
                    if ((orderGroup.group.trainman1.trainmanID != "") && (orderGroup.group.trainman1.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                        new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                        new SqlParameter("strWorkShopGUID",workShopGUID),
                        new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                        new SqlParameter("strTrainmanGUID",orderGroup.group.trainman1.trainmanID),
                };

                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改司机状态错误");
                        }
                    }

                    if ((orderGroup.group.trainman2.trainmanID != "") && (orderGroup.group.trainman2.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                      new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",orderGroup.group.trainman2.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改副司机状态错误");
                        }
                    }

                    if ((orderGroup.group.trainman3.trainmanID != "") && (orderGroup.group.trainman3.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                    new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",orderGroup.group.trainman3.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改学员1状态错误");

                        }
                    }

                    if ((orderGroup.group.trainman4.trainmanID != "") && (orderGroup.group.trainman4.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                    new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",orderGroup.group.trainman4.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改学员2状态错误");

                        }
                    }
                    //添加记名式机组
                    strSql = @"insert into TAB_Nameplate_TrainmanJiaolu_Order 
                       (strOrderGUID,strTrainmanJiaoluGUID,strGroupGUID,nOrder,dtLastArriveTime)
                       (select @strOrderGUID,@strTrainmanJiaoluGUID,@strGroupGUID,(case  when max(nOrder) is null then 1 else max(nOrder) + 1 end),
                       (case  when max(dtLastArriveTime) is null then getdate() else DATEADD (mi,1,max(dtLastArriveTime)) end)
                       from view_Nameplate_TrainmanJiaolu_Order where strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID and strStationGUID=@strStationGUID)";
                    SqlParameter[] sqlParamsNamed = new SqlParameter[]{
                new SqlParameter("strOrderGUID",orderGroup.orderID),
                new SqlParameter("strTrainmanJiaoluGUID",orderGroup.trainmanjiaoluID),
                new SqlParameter("strGroupGUID",orderGroup.group.groupID),                
                new SqlParameter("strStationGUID",orderGroup.group.station.stationID)
            };
                    if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsNamed) == 0)
                    {
                        throw new Exception("创建轮乘机组错误");
                    }
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
        //添加包乘机组
        public static void AddGroupToTrain(string TrainGUID, RRsOrderGroupInTrain orderGroup)
        {
            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {

                    //创建机组
                    string strSql = @"insert into TAB_Nameplate_Group
                                (strGroupGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4) 
                                values (@strGroupGUID,@strTrainmanGUID1,@strTrainmanGUID2,@strTrainmanGUID3,@strTrainmanGUID4)";
                    SqlParameter[] sqlParamsGroup = new SqlParameter[]{
                new SqlParameter("strGroupGUID",orderGroup.Group.groupID),                
                new SqlParameter("strTrainmanGUID1",orderGroup.Group.trainman1.trainmanID),
                new SqlParameter("strTrainmanGUID2",orderGroup.Group.trainman2.trainmanID),
                new SqlParameter("strTrainmanGUID3",orderGroup.Group.trainman3.trainmanID),
                new SqlParameter("strTrainmanGUID4",orderGroup.Group.trainman4.trainmanID)
            };
                    if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsGroup) == 0)
                    {
                        throw new Exception("创建机组错误");
                    }
                    string strTrainmanJiaoluGUID = "";
                    strSql = @"select strTrainmanJiaoluGUID from TAB_Nameplate_TrainmanJiaolu_Train where strTrainGUID = @strTrainGUID";
                    SqlParameter[] sqlParams = new SqlParameter[]{
              new SqlParameter("@strTrainGUID",TrainGUID)
            };
                    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        strTrainmanJiaoluGUID = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                    }

                    //获取交路车间信息
                    strSql = "select strTrainJiaoluGUID,strWorkShopGUID,strTrainmanJiaoluGUID from  VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
                    SqlParameter[] sqlParamsTMJL = new SqlParameter[]{
                new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID)
            };
                    string trainJiaolu = "";
                    string workShopGUID = "";
                    string trainManJiaolu = "";
                    DataTable dtJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTMJL).Tables[0];

                    if (dtJiaolu.Rows.Count > 0)
                    {
                        trainJiaolu = dtJiaolu.Rows[0]["strTrainJiaoluGUID"].ToString();
                        workShopGUID = dtJiaolu.Rows[0]["strWorkShopGUID"].ToString();
                        trainManJiaolu = dtJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                    }

                    //修改人员所属行车区段、车间、状态
                    if ((orderGroup.Group.trainman1.trainmanID != "") && (orderGroup.Group.trainman1.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                     new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",orderGroup.Group.trainman1.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改司机状态错误");

                        }
                    }

                    if ((orderGroup.Group.trainman2.trainmanID != "") && (orderGroup.Group.trainman2.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                     new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",orderGroup.Group.trainman2.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改副司机状态错误");

                        }
                    }

                    if ((orderGroup.Group.trainman3.trainmanID != "") && (orderGroup.Group.trainman3.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                     new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",orderGroup.Group.trainman3.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改学员1状态错误");

                        }
                    }

                    if ((orderGroup.Group.trainman4.trainmanID != "") && (orderGroup.Group.trainman4.trainmanID != null))
                    {
                        strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                        SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                     new SqlParameter("strTrainmanJiaoluGUID",trainManJiaolu),
                    new SqlParameter("strTrainmanGUID",orderGroup.Group.trainman4.trainmanID),
                };
                        if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                        {
                            throw new Exception("修改学员2状态错误");

                        }
                    }


                    //添加包乘
                    strSql = @"insert into TAB_Nameplate_TrainmanJiaolu_OrderInTrain 
                        (strOrderGUID,strTrainGUID,nOrder,strGroupGUID,dtLastArriveTime) 
                        (select @strOrderGUID,@strTrainGUID,(case  when max(nOrder) is null then 1 else max(nOrder) + 1 end),@strGroupGUID,getdate()
                        from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strTrainGUID=@strTrainGUID)";
                    SqlParameter[] sqlParamsNamed = new SqlParameter[]{
                new SqlParameter("strOrderGUID",orderGroup.strOrderGUID),
                new SqlParameter("strTrainGUID",TrainGUID),
                new SqlParameter("strGroupGUID",orderGroup.Group.groupID)                
            };
                    if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsNamed) == 0)
                    {
                        throw new Exception("创建包乘机组错误");
                    }
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


        //删除记名式机组
        public static void DeleteNamedGroup(string GroupGUID)
        {
            string strSql = "select strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4 from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID";
            SqlParameter[] sqlParamsSelect = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            DataTable dtSelect = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSelect).Tables[0];
            if (dtSelect.Rows.Count == 0) return;
            string strTrainmanGUID1 = dtSelect.Rows[0]["strTrainmanGUID1"].ToString();
            string strTrainmanGUID2 = dtSelect.Rows[0]["strTrainmanGUID2"].ToString();
            string strTrainmanGUID3 = dtSelect.Rows[0]["strTrainmanGUID3"].ToString();
            string strTrainmanGUID4 = dtSelect.Rows[0]["strTrainmanGUID4"].ToString();

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    if (strTrainmanGUID1 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID1)};
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTrainman);
                    }
                    if (strTrainmanGUID2 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID2)};
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTrainman);
                    }

                    if (strTrainmanGUID3 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID3)};
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTrainman);
                    }

                    if (strTrainmanGUID4 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID4)};
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTrainman);
                    }

                    strSql = "delete from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID";
                    SqlParameter[] sqlParamsGroup = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",GroupGUID)};
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsGroup);

                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_Named where strGroupGUID = @strGroupGUID";
                    SqlParameter[] sqlParamsGroup2 = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",GroupGUID)};
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsGroup2);
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
        //删除轮乘机组
        public static void DeleteOrderGroup(string GroupGUID)
        {
            string strSql = "select strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4 from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID";
            SqlParameter[] sqlParamsSelect = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            DataTable dtSelect = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSelect).Tables[0];
            if (dtSelect.Rows.Count == 0) return;
            string strTrainmanGUID1 = dtSelect.Rows[0]["strTrainmanGUID1"].ToString();
            string strTrainmanGUID2 = dtSelect.Rows[0]["strTrainmanGUID2"].ToString();
            string strTrainmanGUID3 = dtSelect.Rows[0]["strTrainmanGUID3"].ToString();
            string strTrainmanGUID4 = dtSelect.Rows[0]["strTrainmanGUID4"].ToString();

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    if (strTrainmanGUID1 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID1)
                };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTrainman);
                    }
                    if (strTrainmanGUID2 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID2)
                };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTrainman);
                    }

                    if (strTrainmanGUID3 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID3)
                };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTrainman);
                    }

                    if (strTrainmanGUID4 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID4)
                };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTrainman);
                    }

                    strSql = "delete from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID";
                    SqlParameter[] sqlParamsGroup = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",GroupGUID)
            };
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsGroup);

                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_Order where strGroupGUID = @strGroupGUID";
                    SqlParameter[] sqlParamsGroup2 = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",GroupGUID)
            };
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsGroup2);
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
        //删除包乘机组
        public static void DeleteTogetherGroup(string GroupGUID)
        {
            string strSql = "select strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4 from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID";
            SqlParameter[] sqlParamsSelect = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            DataTable dtSelect = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSelect).Tables[0];
            if (dtSelect.Rows.Count == 0)
            {
                strSql = "delete from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strGroupGUID = '" + GroupGUID + "'";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql);
                return;
            }
            string strTrainmanGUID1 = dtSelect.Rows[0]["strTrainmanGUID1"].ToString();
            string strTrainmanGUID2 = dtSelect.Rows[0]["strTrainmanGUID2"].ToString();
            string strTrainmanGUID3 = dtSelect.Rows[0]["strTrainmanGUID3"].ToString();
            string strTrainmanGUID4 = dtSelect.Rows[0]["strTrainmanGUID4"].ToString();

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    if (strTrainmanGUID1 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID1)                };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrainman);
                    }
                    if (strTrainmanGUID2 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID2)                };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrainman);
                    }

                    if (strTrainmanGUID3 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID3)                };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrainman);
                    }

                    if (strTrainmanGUID4 != "")
                    {
                        //修改人员状态为空
                        strSql = "update Tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
                        SqlParameter[] sqlParamsTrainman = new SqlParameter[]{
                      new SqlParameter("nTrainmanState",1), //tsReady
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID4)                };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrainman);
                    }

                    strSql = "delete from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID";
                    SqlParameter[] sqlParamsGroup = new SqlParameter[] { new SqlParameter("strGroupGUID", GroupGUID) };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsGroup);

                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strGroupGUID = @strGroupGUID";
                    SqlParameter[] sqlParamsGroup2 = new SqlParameter[] { 
                      new SqlParameter("strGroupGUID",GroupGUID)};
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsGroup2);
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
        //保存名牌变动日志
        public static void SaveChangeLog(TrainmanJiaoluMin jiaolu, LBoardChangeType changeType,
            string LogContent, DutyUser dutyUser, TrainmanList trainmanList)
        {

            string strLogGUID = Guid.NewGuid().ToString();
            string strSql = @"insert into TAB_Nameplate_Log (strLogGUID,strTrainmanJiaoluGUID,
                        strTrainmanJiaoluName,nBoardChangeType,strContent,strDutyUserGUID,
                        strDutyUserNumber,strDutyUserName,dtEventTime) values 
                        (@strLogGUID,@strTrainmanJiaoluGUID,@strTrainmanJiaoluName,@nBoardChangeType,
                        @strContent,@strDutyUserGUID,@strDutyUserNumber,@strDutyUserName,getdate())";
            SqlParameter[] sqlParams = new SqlParameter[] {
                      new SqlParameter("strLogGUID",strLogGUID),
                      new SqlParameter("strTrainmanJiaoluGUID",jiaolu.jiaoluID),
                      new SqlParameter("strTrainmanJiaoluName",jiaolu.jiaoluName),
                      new SqlParameter("nBoardChangeType",changeType),
                      new SqlParameter("strContent",LogContent),
                      new SqlParameter("strDutyUserGUID",dutyUser.strDutyGUID),
                      new SqlParameter("strDutyUserNumber",dutyUser.strDutyNumber),
                      new SqlParameter("strDutyUserName",dutyUser.strDutyName)
                  };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            for (int i = 0; i < trainmanList.Count; i++)
            {
                strSql = @"insert into TAB_Nameplate_Log_Trainman (strLogGUID,strTrainmanGUID,strTrainmanName,strTrainmanNumber)
                values (@strLogGUID,@strTrainmanGUID,@strTrainmanName,@strTrainmanNumber)";

                SqlParameter[] sqlParamsTrainman = new SqlParameter[] {
                  new SqlParameter("strLogGUID",strLogGUID),
                  new SqlParameter("strTrainmanGUID",trainmanList[i].trainmanID),
                  new SqlParameter("strTrainmanName",trainmanList[i].trainmanName),
                  new SqlParameter("strTrainmanNumber",trainmanList[i].trainmanNumber)
          
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrainman);
            }

        }
        //修改机组的最后到达时间
        public static void UpdateGroupArriveTime(string GroupGUID, DateTime ArriveTime)
        {
            //获取机组所属人员交路GUID
            string strSql = "select strGroupGUID,strTrainmanJiaoluGUID from VIEW_Nameplate_Group where strGroupGUID = @strGroupGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                //获取人员交路类型
                strSql = "select nJiaoluType from TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
                SqlParameter[] sqlParamsJL = new SqlParameter[] { 
                    new SqlParameter("strTrainmanJiaoluGUID",dt.Rows[0]["strTrainmanJiaoluGUID"].ToString())
                };
                DataTable dtJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsJL).Tables[0];

                //根据人员交路类型修改不通的机组的退勤时间
                if (dtJiaolu.Rows.Count > 0)
                {
                    strSql = "";
                    switch (dtJiaolu.Rows[0]["nJiaoluType"].ToString())
                    {
                        case "2":
                            {
                                strSql = "update TAB_Nameplate_TrainmanJiaolu_Named set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";
                                break;
                            };
                        case "3":
                            {
                                strSql = "update TAB_Nameplate_TrainmanJiaolu_Order set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";
                                break;
                            };
                        case "4":
                            {
                                strSql = "update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";
                                break;
                            };
                    }
                    if (strSql != "")
                    {
                        SqlParameter[] sqlParamsUpdate = new SqlParameter[] { 
                            new SqlParameter("strGroupGUID",GroupGUID),
                            new SqlParameter("dtLastArriveTime",ArriveTime)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdate);
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Boolean GroupInPlan(string GroupGUID)
        {
            string strSql = @"select count(*) TAB_Nameplate_Group where strGroupGUID = @GroupGUID and ISNULL(strTrainPlanGUID,'') <> '' ";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("GroupGUID",GroupGUID), 
            };
            return
            Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters)) > 0;

        }
        public static RRsGroup GetTrainmanGroup(string TrainmanGUID)
        {

            string strSql = @"select top 1 * from VIEW_Nameplate_Group  where  strTrainmanGUID1 =@strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("strTrainmanGUID",TrainmanGUID), 
            };
            DataTable table =
                SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
            if (table.Rows.Count > 0)
            {
                RRsGroup group = new RRsGroup();
                ADOQueryToGroup(group, table.Rows[0]);
                return group;
            }
            return null;
        }
        public static void ADOQueryToGroup(RRsGroup Group, DataRow row)
        {
            Group.strGroupGUID = row["strGroupGUID"].ToString();
            Group.strTrainPlanGUID = row["strTrainPlanGUID"].ToString();
            DateTime dtArriveTime;
            if (row["dtLastArriveTime"] != null)
            {
                if (DateTime.TryParse(row["dtLastArriveTime"].ToString(), out dtArriveTime))
                    Group.dtArriveTime = dtArriveTime;
            }
            Group.ZFQJ.strZFQJGUID = row["strZFQJGUID"].ToString();
            Group.ZFQJ.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
            int nQuJianIndex;
            if (row["nQuJianIndex"] != null)
            {
                if (int.TryParse(row["nQuJianIndex"].ToString(), out nQuJianIndex))
                {
                    Group.ZFQJ.nQuJianIndex = nQuJianIndex;
                }
            }
            Group.ZFQJ.strBeginStationGUID = row["strBeginStationGUID"].ToString();
            Group.ZFQJ.strBeginStationName = row["strBeginStationName"].ToString();
            Group.ZFQJ.strEndStationGUID = row["strEndStationGUID"].ToString();
            Group.ZFQJ.strEndStationName = row["strEndStationName"].ToString();

            Group.Trainman1.strTrainmanGUID = row["strTrainmanGUID1"].ToString();
            Group.Trainman1.strTrainmanName = row["strTrainmanName1"].ToString();
            Group.Trainman1.strTrainmanNumber = row["strTrainmanNumber1"].ToString();
            Group.Trainman1.strTelNumber = row["strTelNumber1"].ToString();
            if (row["nTrainmanState1"] == null)
                Group.Trainman1.nTrainmanState = TRsTrainmanState.tsNil;
            else
                Group.Trainman1.nTrainmanState = (TRsTrainmanState)(int.Parse(row["nTrainmanState1"].ToString()));

            Group.Trainman1.nPostID = (TRsPost)(int.Parse(row["nPost1"].ToString()));
            Group.Trainman1.strWorkShopGUID = row["strWorkShopGUID1"].ToString();
            DateTime dtLastEndworkTime;
            if (row["dtLastEndworkTime1"] != null)
            {
                if (DateTime.TryParse(row["dtLastEndworkTime1"].ToString(), out dtLastEndworkTime))
                {
                    Group.Trainman1.dtLastEndworkTime = dtLastEndworkTime;
                }
            }

            Group.Trainman2.strTrainmanGUID = row["strTrainmanGUID2"].ToString();
            Group.Trainman2.strTrainmanName = row["strTrainmanName2"].ToString();
            Group.Trainman2.strTrainmanNumber = row["strTrainmanNumber2"].ToString();
            Group.Trainman2.strTelNumber = row["strTelNumber2"].ToString();

            if (row["nTrainmanState2"] == null)
            {
                Group.Trainman2.nTrainmanState = TRsTrainmanState.tsNil;
            }
            else
                Group.Trainman2.nTrainmanState = (TRsTrainmanState)Convert.ToInt32(row["nTrainmanState2"].ToString());
            int nPostID;
            if (row["nPost2"] != null && int.TryParse(row["nPost2"].ToString(), out nPostID))
            {
                Group.Trainman2.nPostID = (TRsPost)nPostID;
            }
            Group.Trainman2.strWorkShopGUID = row["strWorkShopGUID2"].ToString();
            if (row["dtLastEndworkTime2"] != null &&
                DateTime.TryParse(row["dtLastEndworkTime2"].ToString(), out dtLastEndworkTime))
            {
                Group.Trainman2.dtLastEndworkTime = dtLastEndworkTime;
            }

            Group.Trainman3.strTrainmanGUID = row["strTrainmanGUID3"].ToString();
            Group.Trainman3.strTrainmanName = row["strTrainmanName3"].ToString();
            Group.Trainman3.strTrainmanNumber = row["strTrainmanNumber3"].ToString();
            Group.Trainman3.strTelNumber = row["strTelNumber3"].ToString();
            if (row["nTrainmanState3"] == null)
            {
                Group.Trainman3.nTrainmanState = TRsTrainmanState.tsNil;
            }
            else
            {
                Group.Trainman3.nTrainmanState = (TRsTrainmanState)Convert.ToInt32(row["nTrainmanState3"]);
            }
            if (row["nPost3"] != null)
            {
                Group.Trainman3.nPostID = (TRsPost)Convert.ToInt32(row["nPost3"]);
            }
            Group.Trainman3.strWorkShopGUID = row["strWorkShopGUID3"].ToString();
            if (row["dtLastEndworkTime3"] != null &&
                DateTime.TryParse(row["dtLastEndworkTime3"].ToString(), out dtLastEndworkTime))
            {
                Group.Trainman3.dtLastEndworkTime = dtLastEndworkTime;
            }

            Group.Trainman4.strTrainmanGUID = row["strTrainmanGUID4"].ToString();
            Group.Trainman4.strTrainmanName = row["strTrainmanName4"].ToString();
            Group.Trainman4.strTrainmanNumber = row["strTrainmanNumber4"].ToString();
            Group.Trainman4.strTelNumber = row["strTelNumber4"].ToString();
            if (row["nTrainmanState4"] == null)
            {
                Group.Trainman4.nTrainmanState = TRsTrainmanState.tsNil;
            }
            else
            {
                Group.Trainman4.nTrainmanState = (TRsTrainmanState)Convert.ToInt32(row["nTrainmanState4"]);
            }
            if (row["nPost4"] != null)
            {
                Group.Trainman4.nPostID = (TRsPost)Convert.ToInt32(row["nPost4"]);
            }
            Group.Trainman4.strWorkShopGUID = row["strWorkShopGUID4"].ToString();
            if (row["dtLastEndworkTime4"] != null &&
                DateTime.TryParse(row["dtLastEndworkTime4"].ToString(), out dtLastEndworkTime))
            {
                Group.Trainman4.dtLastEndworkTime = dtLastEndworkTime;
            }
        }
        private static void UpdateTrainmanStateToNull(string TrainmanGUID, SqlCommand command)
        {
            command.CommandText = "update Tab_Org_Trainman set nTrainmanState =@nTrainmanState where strTrainmanGUID=@strTrainmanGUID ";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("nTrainmanState",TRsTrainmanState.tsReady),
                new SqlParameter("strTrainmanGUID",TrainmanGUID), 
            };
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
            command.ExecuteNonQuery();
        }
        private static void DeleteTrainmanFromGroup(string TrainmanGUID, SqlCommand command)
        {
            command.CommandText = @"update TAB_Nameplate_Group   set 
strTrainmanGUID1=(case when strTrainmanGUID1=@strTrainmanGUID then '' else strTrainmanGUID1 end),
strTrainmanGUID2=(case when strTrainmanGUID2=@strTrainmanGUID then '' else strTrainmanGUID2 end),
strTrainmanGUID3=(case when strTrainmanGUID3=@strTrainmanGUID then '' else strTrainmanGUID3 end),
strTrainmanGUID4=(case when strTrainmanGUID4=@strTrainmanGUID then '' else strTrainmanGUID4 end)
where strTrainmanGUID1=@strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID
";
            SqlParameter[] sqlParameters = new SqlParameter[]
            { 
                new SqlParameter("strTrainmanGUID",TrainmanGUID), 
            };
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
            int count = command.ExecuteNonQuery();
            if (count > 0)
            {
                command.Transaction.Commit();
            }
        }
        public static void DeleteTrainman(string TrainmanGUID)
        {
            SqlTransaction transaction = null;
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            connection.ConnectionString = SqlHelper.ConnString;
            connection.Open();
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                UpdateTrainmanStateToNull(TrainmanGUID, command);
                DeleteTrainmanFromGroup(TrainmanGUID, command);
                transaction.Rollback();
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            finally
            {
                connection.Dispose();
                transaction.Dispose();
            }
        }
        public static bool GetGroupInfo(string GroupGUID, RRsGroup Group)
        {
            string strSql = "select top 1 * from VIEW_Nameplate_Group   where  strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (table.Rows.Count > 0)
            {
                ADOQueryToGroup(Group, table.Rows[0]);
                return true;
            }
            return false;
        }
        public static void AddTrainman(string GroupGUID, int TrainmanIndex, string TrainmanGUID)
        {
            string strSql = "", strTrainmanJiaoluGUID = "", strTrainJiaoluGUID = "", strWorkShopGUID = "";
            SqlTransaction transaction = null;
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();

            try
            {
                command.Connection = connection;
                connection.ConnectionString = SqlHelper.ConnString;
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                strSql = "select strTrainmanJiaoluGUID from VIEW_Nameplate_Group_TrainmanJiaolu where strGroupGUID=@strGroupGUID";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strGroupGUID",GroupGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    strTrainmanJiaoluGUID = table.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                    strSql =
                        "select strTrainJiaoluGUID,strWorkShopGUID from  VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID";
                    sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID), 
                    };
                    table =
                        SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        strTrainJiaoluGUID = table.Rows[0]["strTrainJiaoluGUID"].ToString();
                        strWorkShopGUID = table.Rows[0]["strWorkShopGUID"].ToString();
                    }
                    //添加人员
                    strSql = string.Format("update TAB_Nameplate_Group set strTrainmanGUID{0}='{1}' where strGroupGUID='{2}' ", TrainmanIndex, TrainmanGUID, GroupGUID);
                    command.CommandText = strSql;
                    if (command.ExecuteNonQuery() == 0)
                    {
                        transaction.Rollback();
                    }
                    if (!string.IsNullOrEmpty(strTrainJiaoluGUID))
                    {
                        //修改人员状态为非运转状态
                        command.CommandText = "update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,nTrainmanState =@nTrainmanState where strTrainmanGUID=@strTrainmanGUID";
                        command.Parameters.Clear();
                        sqlParameters = new SqlParameter[]
                        {
                            new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID), 
                            new SqlParameter("strWorkShopGUID",strWorkShopGUID), 
                            new SqlParameter("nTrainmanState",(int)TRsTrainmanState.tsNormal), 
                            new SqlParameter("strTrainmanGUID",TrainmanGUID), 
                        };
                        command.Parameters.AddRange(sqlParameters);
                        if (command.ExecuteNonQuery() == 0)
                            transaction.Rollback();
                    }
                    else
                    {
                        command.CommandText = "update Tab_Org_Trainman set nTrainmanState =@nTrainmanState where strTrainmanGUID=@strTrainmanGUID ";
                        command.Parameters.Clear();
                        sqlParameters = new SqlParameter[] { 
                            new SqlParameter("nTrainmanState",(int)TRsTrainmanState.tsNormal), 
                            new SqlParameter("strTrainmanGUID",TrainmanGUID), };
                        command.Parameters.AddRange(sqlParameters);
                        if (command.ExecuteNonQuery() == 0)
                            transaction.Rollback();
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
        }
        public static void AddTrainmanJiaoLuToTrainman(string TrainmanGUID, string TrainmanJiaoLuGUID)
        {
            string strSql = "update TAB_Org_Trainman set strTrainmanJiaoluGUID  =@strTrainmanJiaoluGUID where strTrainmanGUID=@strTrainmanGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoLuGUID), 
                new SqlParameter("strTrainmanGUID",TrainmanGUID), 
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
        }
        public static Boolean DeleteNamedGroupByJiaoLu(string TrainmanjiaoluID)
        {
            string strSql = "DELETE FROM dbo.TAB_Nameplate_TrainmanJiaolu_Named WHERE strTrainmanJiaoluGUID =@strTrainmanJiaoluGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("strTrainmanJiaoluGUID",TrainmanjiaoluID), 
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters) > 0;
        }
        #endregion

        /// <summary>
        /// 交换轮乘机组的出勤点
        /// </summary>
        /// <param name="GroupID">轮乘机组ID</param>
        /// <param name="SourcePlaceID">交换前出勤点</param>
        /// <param name="DestPlaceID">交换后出勤点</param>
        public static bool ChangeGroupPlace(string GroupID, string SourcePlaceID, string DestPlaceID)
        {
            string sqlText = "update TAB_Nameplate_Group set strPlaceID = @DestPlaceID where strGroupGUID = @GroupID";
            SqlParameter[] sqlParams = new SqlParameter[]{ 
                    new SqlParameter("DestPlaceID",DestPlaceID),
                    new SqlParameter("GroupID",GroupID)
                };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams) > 0;
        }
        /// <summary>
        /// 通过车间获取所有的预备人员
        /// </summary>
        /// <param name="WsGUID"></param>
        /// <returns></returns>
        public RRsTrainmanList GetPreparedTrainmans(string WsGUID)
        {
            string strSql =
               string.Format(
                   "select * from VIEW_Nameplate_TrainmanJiaolu_Prepare where nTrainmanState <> 7 and strWorkShopGUID = '{0}' order by strTrainmanNumber", WsGUID);
            RRsTrainman trainman = null;
            RRsTrainmanList l = new RRsTrainmanList();
            System.Data.DataTable table =
                SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            foreach (DataRow row in table.Rows)
            {
                trainman = new RRsTrainman();
                ADOQueryToData(row, trainman);
                l.Add(trainman);
            }
            return l;
        }
        public void ADOQueryToData(DataRow row, RRsTrainman trainman)
        {
            trainman.ABCD = row["strABCD"].ToString();
            trainman.telNumber = row["strTelNumber"].ToString();
            trainman.trainmanID = row["strTrainmanGUID"].ToString();
            trainman.trainmanName = row["strTrainmanName"].ToString();
            trainman.trainmanNumber = row["strTrainmanNumber"].ToString();

            int driverTypeID, isKey, nKeHuoID, postID, trainmanState;
            if (int.TryParse(row["nDriverType"].ToString(), out driverTypeID))
            {
                trainman.driverTypeID = driverTypeID;
            }
            if (int.TryParse(row["bIsKey"].ToString(), out isKey))
            {
                trainman.isKey = isKey;
            }
            if (int.TryParse(row["nKeHuoID"].ToString(), out nKeHuoID))
            {
                trainman.nKeHuoID = nKeHuoID;
            }
            if (int.TryParse(row["nPostID"].ToString(), out postID))
            {
                trainman.postID = postID;
            }
            if (int.TryParse(row["nTrainmanState"].ToString(), out trainmanState))
            {
                trainman.trainmanState = trainmanState;
            }
        }
        /// <summary>
        /// 获取非运转人员列表
        /// </summary>
        /// <param name="WsGUID"></param>
        /// <returns></returns>
        public RRsTrainmanList GetUnRunTrainmans(string WsGUID)
        {
            RRsTrainmanList l = new RRsTrainmanList();
            int nTrainmanState = (int)TF.RunSafty.NamePlate.MD.TRsTrainmanState.tsUnRuning;
            string strSql =
                string.Format(@"select *, 
    (select top 1 strLeaveTypeGUID from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeGUID, 
   (select top 1 strTypeName from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeName
   from VIEW_Org_Trainman where strWorkShopGUID='{0}' 
   and nTrainmanState = {1}  order by strLeaveTypeGUID,strTrainmanNumber ", WsGUID, nTrainmanState);
            RRsTrainman trainman = null;

            System.Data.DataTable table =
                SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            foreach (DataRow row in table.Rows)
            {
                trainman = new RRsTrainman();
                ADOQueryToData(row, trainman);
                trainman.strLeaveTypeGUID = row["strLeaveTypeGUID"].ToString();
                trainman.strLeaveTypeName = row["strLeaveTypeName"].ToString();
                l.Add(trainman);
            }
            return l;
        }


        public TrainJiaoluList GetAllTrainJiaoluArrayOfSite(string strSiteGUID)
        {
            TrainJiaoluList l = new TrainJiaoluList();
            string strSql = string.Format(@"select * from VIEW_Base_TrainJiaolu where  
       strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite 
       where strSiteGUID ='{0}' union select strSubTrainJiaoluGUID as strTrainJiaoluGUID 
       from TAB_Base_TrainJiaolu_SubDetail where strTrainJiaoluGUID in 
       (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = '{0}')) and (bIsDir = 0 or bIsDir is null)",
                strSiteGUID);
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            TF.RunSafty.NamePlate.MD.TrainJiaolu jiaolu = null;
            foreach (DataRow row in table.Rows)
            {
                jiaolu = new TF.RunSafty.NamePlate.MD.TrainJiaolu();
                GetTrainJiaoluFromDataRow(row, jiaolu);
                l.Add(jiaolu);
            }
            return l;
        }

        public void GetTrainJiaoluFromDataRow(DataRow row, TF.RunSafty.NamePlate.MD.TrainJiaolu jiaolu)
        {
            jiaolu.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
            jiaolu.strTrainJiaoluName = row["strTrainJiaoluName"].ToString();
            jiaolu.strStartStation = row["strStartStation"].ToString();
            jiaolu.strEndStation = row["strEndStation"].ToString();
            jiaolu.strWorkShopGUID = row["strWorkShopGUID"].ToString();
            int bIsBeginWorkFP;
            if (int.TryParse(row["bIsBeginWorkFP"].ToString(), out bIsBeginWorkFP))
            { jiaolu.bIsBeginWorkFP = bIsBeginWorkFP; }
            jiaolu.strStartStationName = row["strStartStationName"].ToString();
            jiaolu.strEndStationName = row["strEndStationName"].ToString();
        }

       
        public void ConvertTrainmanStateToNull(string TrainmanNumber)
        {
            string strSql = @"update TAB_Org_Trainman set nTrainmanState =7  where strTrainmanNumber = @strTrainmanNumber";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainmanNumber",TrainmanNumber)
                };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }

        public static string GetGroupTMJiaoluGUID(string GroupGUID)
        {
            string strSql = "select strTrainmanJiaoluGUID from VIEW_Nameplate_Group where strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0]["strTrainmanJiaoluGUID"]);
            }
            return "";
        }


        public static void TurnTogetherTrainGroup(string stTrainGUID)
        {           
            ///将所有非自己的机组按照从1开始排序
            string sqlText = "select strGroupGUID from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strTrainGUID=@strTrainGUID  order by nOrder";
            SqlParameter[] sqlParams = new SqlParameter[]{
                            new SqlParameter("strTrainGUID",stTrainGUID)
                        };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams).Tables[0];
            if (dt.Rows.Count == 0) return;
            string strFirstGroupGUID = dt.Rows[0]["strGroupGUID"].ToString();

            sqlText = "select strGroupGUID from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strTrainGUID=@strTrainGUID and strGroupGUID <> @strGroupGUID  order by nOrder";
            SqlParameter[] sqlParamsSubs = new SqlParameter[]{
                            new SqlParameter("strTrainGUID",stTrainGUID),
                            new SqlParameter("strGroupGUID",strFirstGroupGUID)
                        };
            DataTable dtReOrder = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsSubs).Tables[0];
            for (int i = 0; i < dtReOrder.Rows.Count; i++)
            {
                sqlText = "update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set nOrder = @nOrder where strGroupGUID=@strGroupGUID";
                SqlParameter[] sqlReOrder = new SqlParameter[] { 
                            new SqlParameter("nOrder",i+ 1),
                            new SqlParameter("strGroupGUID",dtReOrder.Rows[i]["strGroupGUID"].ToString())
                        };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlReOrder);
            }
            //将自己设置为最大的
            sqlText = "update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set nOrder = @nOrder where strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlReOrder2 = new SqlParameter[] { 
                            new SqlParameter("nOrder",dtReOrder.Rows.Count + 1),
                            new SqlParameter("strGroupGUID",strFirstGroupGUID)
                        };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlReOrder2);        
        }
    }
}
