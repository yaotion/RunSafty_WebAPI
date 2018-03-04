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
using TF.RunSafty.Utils.Parse;
using TF.RunSafty.NamePlate.DB;

namespace TF.RunSafty.NamePlate
{
    public class LCNamedGroup
    {

        InterfaceRet _Ret = new InterfaceRet();
        #region 插入机组
        public class InInsertGroup
        {
            //所属人员交路GUID
            public string TrainmanJiaoluGUID;
            //旧机组
            public RRsNamedGroup NamedGroupOld = new RRsNamedGroup();
            //新机组
            public RRsNamedGroup NamedGroupNew = new RRsNamedGroup();
            //左侧插入还是右侧插入(1左侧,2右侧)
            public int LeftOrRight;
        }

        /// <summary>
        /// 插入记名式机组
        /// </summary>
        public InterfaceOutPut InsertGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InInsertGroup InParams = javaScriptSerializer.Deserialize<InInsertGroup>(Data);


                SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {

                        //后面的机组顺序移位
                        string strSql = "";
                        int nCheciOrder = InParams.NamedGroupOld.nCheciOrder;
                        if (InParams.LeftOrRight == 1)
                        {

                            strSql = "update TAB_Nameplate_TrainmanJiaolu_Named set nCheciOrder = nCheciOrder + 1 where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID and nCheciOrder >=@nCheciOrder";
                        }
                        if (InParams.LeftOrRight == 2)
                        {
                            nCheciOrder = nCheciOrder + 1;
                            strSql = "update TAB_Nameplate_TrainmanJiaolu_Named set nCheciOrder = nCheciOrder + 1 where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID and nCheciOrder > @nCheciOrder";
                        }
                        SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainmanJiaoluGUID",InParams.TrainmanJiaoluGUID),
                    new SqlParameter("nCheciOrder",nCheciOrder)
                };
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams);

                        RRsNamedGroup namedGroup = InParams.NamedGroupNew;
                        string TrainmanJiaoluGUID = InParams.TrainmanJiaoluGUID;

                        //创建机组
                        strSql = @"insert into TAB_Nameplate_Group 
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
                        strSql = "select strTrainJiaoluGUID,strWorkShopGUID from  VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
                        SqlParameter[] sqlParamsTMJL = new SqlParameter[]{
                new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID)
            };
                        string trainJiaolu = "";
                        string workShopGUID = "";
                        DataTable dtJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsGroup).Tables[0];

                        if (dtJiaolu.Rows.Count > 0)
                        {
                            trainJiaolu = dtJiaolu.Rows[0]["strTrainJiaoluGUID"].ToString();
                            workShopGUID = dtJiaolu.Rows[0]["strWorkShopGUID"].ToString();
                        }

                        //修改人员所属行车区段、车间、状态
                        if (namedGroup.Group.trainman1.trainmanID != "")
                        {
                            strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                            SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                    new SqlParameter("strTrainmanGUID",namedGroup.Group.trainman1.trainmanID),
                };
                            if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                            {
                                throw new Exception("修改司机状态错误");

                            }
                        }

                        if (namedGroup.Group.trainman2.trainmanID != "")
                        {
                            strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                            SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                    new SqlParameter("strTrainmanGUID",namedGroup.Group.trainman2.trainmanID),
                };
                            if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                            {
                                throw new Exception("修改副司机状态错误");

                            }
                        }

                        if (namedGroup.Group.trainman3.trainmanID != "")
                        {
                            strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                            SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
                    new SqlParameter("strTrainmanGUID",namedGroup.Group.trainman3.trainmanID),
                };
                            if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsTM) == 0)
                            {
                                throw new Exception("修改学员1状态错误");

                            }
                        }

                        if (namedGroup.Group.trainman4.trainmanID != "")
                        {
                            strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,nTrainmanState = 2 where strTrainmanGUID = @strTrainmanGUID";
                            SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainJiaoluGUID",trainJiaolu),
                    new SqlParameter("strWorkShopGUID",workShopGUID),
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
                new SqlParameter("GroupGUID",namedGroup.Group.groupID),
                new SqlParameter("strTrainmanJiaoluGUID",namedGroup.strTrainmanJiaoluGUID)
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
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.InsertGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 修改记名式机组的车次信息
        public class InUpdateCheciInfo
        {
            //车次1
            public string checi1;
            //车次2
            public string checi2;
            //车次类型(0往,1返)
            public int checiType;
            //记名式机组GUID
            public string checiID;
        }

        /// <summary>
        /// 修改车次信息
        /// </summary>
        public InterfaceOutPut UpdateTrainno(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InUpdateCheciInfo InParams = javaScriptSerializer.Deserialize<InUpdateCheciInfo>(Data);
                string strSql = "update TAB_Nameplate_TrainmanJiaolu_Named set strCheci1=@strCheci1,strCheci2=@strCheci2,nCheciType = @nCheciType where strCheciGUID = @strCheciGUID";
                SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("strCheci1",InParams.checi1),
                    new SqlParameter("strCheci2",InParams.checi2),
                    new SqlParameter("nCheciType",InParams.checiType),
                    new SqlParameter("strCheciGUID",InParams.checiID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.UpdateCheciInfo:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 修改记名式交路的序号
        public class InUpdateNamedGroupOrder
        {
            //记名式机组GUID
            public string CheciGUID;
            //新序号
            public int CheciOrder;
        }

        /// <summary>
        /// 修改记名式机组的序号信息
        /// </summary>
        public InterfaceOutPut UpdateNamedGroupOrder(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InUpdateNamedGroupOrder InParams = javaScriptSerializer.Deserialize<InUpdateNamedGroupOrder>(Data);
                string strSql = "update TAB_Nameplate_TrainmanJiaolu_Named set nCheciOrder = @nCheciOrder where strCheciGUID = @strCheciGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("nCheciOrder",InParams.CheciOrder),
                    new SqlParameter("strCheciGUID",InParams.CheciGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.UpdateNamedGroupOrder:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 记名式交路机组翻牌
        public class InTurnPlateNamed
        {
            //所属人员交路GUID
            public string TrainmanJiaoluGUID;
            //向左/右翻拍(1左,2右)
            public int LeftOrRight;
        }

        /// <summary>
        /// 记名式名牌翻牌
        /// </summary>
        public InterfaceOutPut TurnPlateNamed(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InTurnPlateNamed InParams = javaScriptSerializer.Deserialize<InTurnPlateNamed>(Data);

                SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {


                        //左移
                        if (InParams.LeftOrRight == 1)
                        {
                            string strSql = @"select * from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID order by nCheciOrder";
                            SqlParameter[] sqlParams = new SqlParameter[] { 
                            new SqlParameter("strTrainmanJiaoluGUID",InParams.TrainmanJiaoluGUID)
                        };
                            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                            string strFirstGroupGUID = dt.Rows[0]["strGroupGUID"].ToString();
                            string strFinalCheciGUID = dt.Rows[dt.Rows.Count - 1]["strCheCiGUID"].ToString();
                            string strPreCheciGUID = "";
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                if (i > 0)
                                {
                                    strSql = "update TAB_Nameplate_TrainmanJiaolu_Named set strGroupGUID = @strGroupGUID where strCheciGUID = @strCheciGUID";
                                    SqlParameter[] sqlParamsUpdate = new SqlParameter[] { 
                                   new SqlParameter("strGroupGUID",dt.Rows[i]["strGroupGUID"].ToString()),
                                   new SqlParameter("strCheciGUID",strPreCheciGUID)
                               };
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsUpdate);
                                }
                                strPreCheciGUID = dt.Rows[i]["strCheCiGUID"].ToString();
                            }
                            strSql = "update TAB_Nameplate_TrainmanJiaolu_Named set strGroupGUID = @strGroupGUID where strCheciGUID = @strCheciGUID";
                            SqlParameter[] sqlParamsUpdate2 = new SqlParameter[] { 
                                   new SqlParameter("strGroupGUID",strFirstGroupGUID),
                                   new SqlParameter("strCheciGUID",strFinalCheciGUID)
                               };
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsUpdate2);
                        }
                        else //右移
                        {

                            string strSql = @"select * from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID order by nCheciOrder desc";
                            SqlParameter[] sqlParams = new SqlParameter[] { 
                            new SqlParameter("strTrainmanJiaoluGUID",InParams.TrainmanJiaoluGUID)
                        };
                            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                            string strFirstGroupGUID = dt.Rows[0]["strGroupGUID"].ToString();
                            string strFinalCheciGUID = dt.Rows[dt.Rows.Count - 1]["strCheCiGUID"].ToString();
                            string strPreCheciGUID = "";
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                if (i > 0)
                                {
                                    strSql = "update TAB_Nameplate_TrainmanJiaolu_Named set strGroupGUID = @strGroupGUID where strCheciGUID = @strCheciGUID";
                                    SqlParameter[] sqlParamsUpdate = new SqlParameter[] { 
                                   new SqlParameter("strGroupGUID",dt.Rows[i]["strGroupGUID"].ToString()),
                                   new SqlParameter("strCheciGUID",strPreCheciGUID)
                               };
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsUpdate);
                                }
                                strPreCheciGUID = dt.Rows[i]["strCheCiGUID"].ToString();
                            }
                            strSql = "update TAB_Nameplate_TrainmanJiaolu_Named set strGroupGUID = @strGroupGUID where strCheciGUID = @strCheciGUID";
                            SqlParameter[] sqlParamsUpdate2 = new SqlParameter[] { 
                                   new SqlParameter("strGroupGUID",strFirstGroupGUID),
                                   new SqlParameter("strCheciGUID",strFinalCheciGUID)
                               };
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsUpdate2);
                        }


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
                output.resultStr = ex.Message;
                LogClass.log("Interface.TurnPlateNamed:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取指定的记名式机组
        public class InGetNamedGroup
        {
            //记名式机组GUID
            public string GroupGUID;
        }

        public class OutGetNamedGroup
        {
            //记名式机组                               
            public RRsNamedGroup Group = new RRsNamedGroup();
            //是否存在
            public int Exist;
        }

        /// <summary>
        /// 获取指定编号的记名式机组
        /// </summary>
        public InterfaceOutPut GetNamedGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetNamedGroup InParams = javaScriptSerializer.Deserialize<InGetNamedGroup>(Data);
                OutGetNamedGroup OutParams = new OutGetNamedGroup();
                string strSql = "select * from VIEW_Nameplate_TrainmanJiaolu_Named  where strGroupGUID= @strGroupGUID order by nCheciOrder";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",InParams.GroupGUID)
                };
                OutParams.Exist = 0;
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    OutParams.Exist = 1;
                    PS.PSNameBoard.NamedGroupFromDB(OutParams.Group, dt.Rows[0]);
                }

                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetNamedGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

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
                if (!string.IsNullOrEmpty(group.trainPlanID))
                {
                    throw new Exception("该机组已按排计划，不能修改所属交路");
                }

                SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",InParams.GroupGUID),
                new SqlParameter("DestJiaoLu",InParams.DestJiaolu.jiaoluID),
                new SqlParameter("Checi1",InParams.CheCi1),
                new SqlParameter("Checi2",InParams.CheCi2),
                new SqlParameter("NewCheCiGUID",Guid.NewGuid())


                };

                string strSql = "select ISNULL(max(nCheciOrder),1) from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID = @DestJiaoLu";

                int nOrder = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams));

                nOrder++;
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
                                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strGroupGUID = @strGroupGUID";
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams);
                                    break;
                                }

                        }


                        //更新名牌关联交路
                        strSql = @"update TAB_Nameplate_TrainmanJiaolu_Named set strTrainmanJiaoluGUID = @DestJiaoLu,nCheciOrder = "
                        + nOrder.ToString() +
                        ",strCheCi1 = @Checi1,strCheCi2 = @Checi2 where  strGroupGUID = @strGroupGUID";

                        if (Convert.ToInt32(SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams)) == 0)
                        {
                            //如果没有旧的机组，则重新插入记录
                            strSql = @"insert into TAB_Nameplate_TrainmanJiaolu_Named (strCheCiGUID,strTrainmanJiaoluGUID,nCheciOrder,nCheciType,strCheci1,strCheci2,strGroupGUID) 
                    values(@NewCheCiGUID,@DestJiaoLu,"
                            + nOrder.ToString() +
                            ",0,@Checi1,@Checi2,@strGroupGUID)";
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


        #region  移动记名式机组
        public class InMoveGrp
        {
            public DutyUser DutyUser = new DutyUser();
            public string CCGUID;
            public int CCOrder;
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
        }


        public InterfaceRet MoveGrp(String Data)
        {
            _Ret.Clear();
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InMoveGrp InParams = javaScriptSerializer.Deserialize<InMoveGrp>(Data);
                //移动记名式交路机组

                if (InParams.CCOrder == 0)
                    throw new Exception("所传目标位置不能为空");
                if (InParams.DutyUser == null)
                    throw new Exception("值班员信息不能为空");
                if (InParams.TrainmanJiaolu == null)
                    throw new Exception("交路信息不能为空");
                //执行移动操作
                DBNamedGroup.MoveGrp(InParams.CCGUID, InParams.CCOrder,InParams.TrainmanJiaolu);
                TrainmanList trainmanList = new TrainmanList();
                string strContent = string.Format("成功将目标名牌的位置移动到{0}位置", InParams.CCOrder);
                DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.MovenamedGrp, strContent, InParams.DutyUser, trainmanList);
                _Ret.result = 0;
            }
            catch (Exception ex)
            {
                _Ret.resultStr = ex.Message;
                _Ret.result = 1;
                LogClass.log("Interface.AddNamedGroup:" + ex.Message);
                throw ex;
            }
            return _Ret;
        }

        #endregion

    }
}
