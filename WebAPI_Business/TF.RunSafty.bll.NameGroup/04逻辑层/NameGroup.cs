using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;
using TF.Api.Utilities;
using TF.CommonUtility;
using ThinkFreely.DBUtility;

using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.DB;


namespace TF.RunSafty.NamePlate
{

    public class NameGroup
    {
        #region 普通机组

        public class Group_In
        {
            public string placeID { get; set; }
            [NotNull]
            public string trainmanjiaoluID { get; set; }
            [NotNull]
            public string trainmanID { get; set; }

        }

        public class Group_Out
        {
            public int result;
            public string resultStr;
            public List<Group> data;
        }
        public Group_Out GetNormalGroup(string data)
        {
            Group_Out jsonModel = new Group_Out();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                Group_In paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Group_In>(data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel))
                {
                    string jiaoluID = paramModel.trainmanjiaoluID;
                    List<string> placeIDs = new List<string>();
                    string trainmanID = paramModel.trainmanID;
                    if (!string.IsNullOrEmpty(paramModel.placeID))
                    {
                        placeIDs.AddRange(paramModel.placeID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                    List<Group> groups = DBOrderGP.GetGroups(jiaoluID, placeIDs, trainmanID);
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
            return jsonModel;
        }
        #endregion

        #region 获取车间下所有的名牌信息

        public class InGetWorkshopBoards
        {
            //车间GUID
            public string WorkShopGUID;
            //车间名
            public string WorkShopName;
        }
        public InterfaceOutPut GetWorkshopBoardsEx(String Data)
        {
            //InterfaceOutPut output = new InterfaceOutPut();
            //InGetWorkshopBoards InParams = Newtonsoft.Json.JsonConvert.DeserializeObject<InGetWorkshopBoards>(Data);

            //LEDNameplateReader reader = new LEDNameplateReader();

            //output.data = reader.ReadNameplates(InParams.WorkShopName);
            //return output;
            return null;
        }

        //获取干部处于计划的状态
        private int GetGanbuPlanState(string TrainanNumber)
        {
            string strSql = @"select nPlanState from VIEW_Plan_Trainman 
            WHERE nPlanState < 8 AND nPlanState >= 4
            AND (strTrainmanNumber1 = @TrainmanNumber or 
            strTrainmanNumber2 = @TrainmanNumber or 
            strTrainmanNumber3 = @TrainmanNumber or 
            strTrainmanNumber4 = @TrainmanNumber)";
            SqlParameter[] sqlParams = {
                                               new SqlParameter("TrainmanNumber",TrainanNumber)
                                           };
            sqlParams[0].SqlDbType = SqlDbType.VarChar;

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["nPlanState"]);
            }
            else
            {
                return 0;
            }

        }
        public InterfaceOutPut GetWorkshopBoards(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 0;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                InGetWorkshopBoards InParams = Newtonsoft.Json.JsonConvert.DeserializeObject<InGetWorkshopBoards>(Data);
                //获取所有的记名式交路名牌
                string strSql = string.Format(@"select * from VIEW_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID in 
(select strTrainmanJiaoluGUID from tab_base_jiaolurelation where strTrainJiaoluGUID in
(select strTrainJiaoluGUID from TAB_Base_TrainJiaolu where strWorkShopGUID=@strWorkShopGUID)) and (nTXState is null or nTXState = 0)
order by strTrainmanJiaoluGUID,nCheciOrder");
                SqlParameter[] sqlParamsNamed = {
                                               new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                                           };
                DataTable tabNamed = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsNamed).Tables[0];

                //获取所有的轮乘交路名牌
                strSql = string.Format(@"select * from VIEW_Nameplate_TrainmanJiaolu_Order where strTrainmanJiaoluGUID in 
(select strTrainmanJiaoluGUID from tab_base_jiaolurelation where strTrainJiaoluGUID in

(select strTrainJiaoluGUID from TAB_Base_TrainJiaolu where strWorkShopGUID=@strWorkShopGUID)) and (nTXState is null or nTXState = 0)
order by strTrainmanJiaoluGUID,groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2");
                SqlParameter[] sqlParamsOrder = {
                                               new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                                           };
                DataTable tabOrder = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsOrder).Tables[0];

                //获取所有的包乘交路名牌
                strSql = string.Format(@"select * from VIEW_Nameplate_TrainmanJiaolu_TogetherTrain where strTrainmanJiaoluGUID in 
               (select strTrainmanJiaoluGUID from tab_base_jiaolurelation where strTrainJiaoluGUID in 
               (select strTrainJiaoluGUID from TAB_Base_TrainJiaolu where strWorkShopGUID=@strWorkShopGUID)) and (nTXState is null or nTXState = 0)
               order by strTrainmanJiaoluGUID,strTrainGUID,GroupState,nOrder");
                SqlParameter[] sqlParamsTrain = {
                                                              new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                                                          };
                DataTable tabTrain = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrain).Tables[0];
                //DataTable tabTrain = new DataTable();

                //获取所有请销假人员
                strSql = @"select strTrainmanGUID,strTrainmanNumber,strTrainmanName,nPostID,strTrainmanJiaoluGUID,bIsKey,
     (select top 1 strLeaveTypeGUID from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeGUID, 
     (select top 1 strTypeName from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeName
     from VIEW_Org_Trainman where strWorkShopGUID=@strWorkShopGUID  
     and nTrainmanState = 0  order by strTrainmanJiaoluGUID,strLeaveTypeGUID,strTrainmanNumber ";
                SqlParameter[] sqlParamsUnrun = {
                                               new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                                           };
                DataTable tabUnun = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUnrun).Tables[0];

                //获取所有预备人员
                strSql = @"select strTrainmanGUID,strTrainmanNumber,strTrainmanName,nPostID,strTrainmanJiaoluGUID,'--------------' as strLeaveTypeGUID,'预备' as strLeaveTypeName
     ,(case 
        when DATEADD(hour,24,ISNULL(dtBecomeReady,getdate())) < GETDATE()   then 1
        else 0
        end) as nReadyOverTime,bIsKey from VIEW_Org_Trainman where strWorkShopGUID=@strWorkShopGUID  
     and nTrainmanState = 1  order by strTrainmanJiaoluGUID,strLeaveTypeGUID,strTrainmanNumber ";
                SqlParameter[] sqlParamsPrepare = {
                                               new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                                           };
                DataTable tabPrepare = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsPrepare).Tables[0];


                DataTable dt = getNoDispatching(InParams.WorkShopGUID);
                //填充数据
                JiaoluArray JiaoluListResult = new JiaoluArray();
                InitJiaoluArray(tabUnun, tabPrepare, tabNamed, tabOrder, tabTrain, JiaoluListResult, dt);

                //获取交路的排序信息
                strSql = @"select * from TAB_Nameplate_TrainmanJiaolu_ShowOrder where strWorkShopGUID =@strWorkShopGUID order by nX,nY";
                SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                                           };
                DataTable tabShowOrder = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];






                //排序
                JiaoluArray OutParams = new JiaoluArray();
                for (int i = 0; i < tabShowOrder.Rows.Count; i++)
                {
                    for (int k = 0; k < JiaoluListResult.Count; k++)
                    {
                        if (JiaoluListResult[k].JiaoluGUID == tabShowOrder.Rows[i]["strTrainmanJiaoluGUID"].ToString())
                        {
                            Jiaolu tmpJl = JiaoluListResult[k];
                            JiaoluListResult.RemoveAt(k);
                            OutParams.Add(tmpJl);
                            tmpJl.Position.X = int.Parse(tabShowOrder.Rows[i]["nX"].ToString());
                            tmpJl.Position.Y = int.Parse(tabShowOrder.Rows[i]["nY"].ToString());
                            break;
                        }
                    }
                }
                for (int k = 0; k < JiaoluListResult.Count; k++)
                {
                    Jiaolu tmpJl = JiaoluListResult[k];
                    JiaoluListResult.RemoveAt(k);
                    OutParams.Add(tmpJl);
                    break;
                }


                FillJlRoomState(OutParams);

                Jiaolu ganbujl = FillGanBuList(InParams.WorkShopGUID);
                OutParams.Insert(0, ganbujl);

                output.data = OutParams;
                output.result = 1;
            }

            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetWorkshopBoards:" + ex.Message);
                throw ex;
            }
            return output;
        }


     


        private void FillGrpRoomState(Dictionary<string, DateTime> dictInRoom, Dictionary<string, DateTime> dictOutRoom, Groupmp Group)
        {
            if (!string.IsNullOrEmpty(Group.Trainman1.strTrainmanGUID))
            {
                FillTmRoolState(dictInRoom, dictOutRoom, Group.Trainman1);
            }

            if (!string.IsNullOrEmpty(Group.Trainman2.strTrainmanGUID))
            {
                FillTmRoolState(dictInRoom, dictOutRoom, Group.Trainman2);
            }


            if (!string.IsNullOrEmpty(Group.Trainman3.strTrainmanGUID))
            {
                FillTmRoolState(dictInRoom, dictOutRoom, Group.Trainman3);
            }


            if (!string.IsNullOrEmpty(Group.Trainman3.strTrainmanGUID))
            {
                FillTmRoolState(dictInRoom, dictOutRoom, Group.Trainman4);
            }



        }

        private void FillTmRoolState(Dictionary<string, DateTime> dictInRoom, Dictionary<string, DateTime> dictOutRoom, Trainmanss tm)
        {
            DateTime dtInRoom = DateTime.MinValue;
            DateTime dtOutRoom = DateTime.MinValue;

            if (!dictInRoom.ContainsKey(tm.strTrainmanGUID))
                return;

            dtInRoom = dictInRoom[tm.strTrainmanGUID];

            if (dictOutRoom.ContainsKey(tm.strTrainmanGUID))
            {
                dtOutRoom = dictOutRoom[tm.strTrainmanGUID];
                if (DateTime.Compare(dtInRoom, dtOutRoom) == 1)
                {
                    tm.nWorkState = 2;
                }

            }
            else
            {
                tm.nWorkState = 2;
            }


        }


        private void FillJlRoomState(JiaoluArray OutParams)
        {
            Dictionary<string, DateTime> dictInRoom = new Dictionary<string, DateTime>();
            Dictionary<string, DateTime> dictOutRoom = new Dictionary<string, DateTime>();

            string strSql = @"SELECT [strTrainmanGUID],MAX([dtInRoomTime]) as dtTime
            FROM [TAB_Plan_InRoom] WHERE [dtInRoomTime] > DATEADD(DAY,-3,GETDATE())
            GROUP BY strTrainmanGUID ";



            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                dictInRoom.Add(dr["strTrainmanGUID"].ToString(), Convert.ToDateTime(dr["dtTime"]));

            }




            strSql = @"SELECT [strTrainmanGUID],MAX([dtOutRoomTime]) as dtTime
            FROM [TAB_Plan_OutRoom] WHERE [dtOutRoomTime] > DATEADD(DAY,-3,GETDATE())
            GROUP BY strTrainmanGUID ";

            dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                dictOutRoom.Add(dr["strTrainmanGUID"].ToString(), Convert.ToDateTime(dr["dtTime"]));

            }



            foreach (Jiaolu jl in OutParams)
            {
                if (jl is OrderJiaolu)
                {
                    foreach (OrderGroupAdd orderGrp in (jl as OrderJiaolu).OrderGroupArray)
                    {
                        FillGrpRoomState(dictInRoom, dictOutRoom, orderGrp.Group);

                    }

                }


                if (jl is NamedJiaolu)
                {
                    foreach (NamedGroup NamedGrp in (jl as NamedJiaolu).NamedGroupArray)
                    {
                        FillGrpRoomState(dictInRoom, dictOutRoom, NamedGrp.Group);

                    }

                }

                if (jl is TogetherTrainJiaoLu)
                {
                    foreach (TogetherTrain train in (jl as TogetherTrainJiaoLu).TogetherTrainArray)
                    {
                        foreach (OrderGroupInTrain grp in train.Groups)
                        {
                            FillGrpRoomState(dictInRoom, dictOutRoom, grp.Group);
                        }

                    }

                }




            }



        }
        private Jiaolu FillGanBuList(string WorkShopGUID)
        {
            Jiaolu ganbujl = new Jiaolu();
            ganbujl.JiaoluGUID = "干部";
            ganbujl.JiaoluName = "干部";



            string strSql = @"select TAB_Org_GanBu.*,TAB_Org_Trainman.nPostID from TAB_Org_GanBu inner join 
                TAB_Base_GanBuType on TAB_Org_GanBu.nTypeID = TAB_Base_GanBuType.nTypeID
                left join TAB_Org_Trainman ON TAB_Org_GanBu.strTrainmanID = TAB_Org_Trainman.strTrainmanGUID              
                 where TAB_Org_GanBu.strWorkShopGUID = @strWorkShopGUID ORDER BY TAB_Base_GanBuType.nOrder";

            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",WorkShopGUID)
                                      };


            DataTable tabGanbu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];



            TrainmanLeaveInfo leaveInfo;

            foreach (DataRow dr in tabGanbu.Rows)
            {
                leaveInfo = new TrainmanLeaveInfo();
                leaveInfo.strLeaveTypeName = dr["strTypeName"].ToString();
                leaveInfo.strLeaveTypeGUID = dr["strTypeName"].ToString();
                leaveInfo.Trainman.strTrainmanName = dr["strTrainmanName"].ToString();
                leaveInfo.Trainman.strTrainmanNumber = dr["strTrainmanNumber"].ToString();
                if (dr["nPostID"] != DBNull.Value)
                {
                    leaveInfo.Trainman.nPostID = Convert.ToInt32(dr["nPostID"]);
                }

                leaveInfo.Trainman.nPlanState = GetGanbuPlanState(leaveInfo.Trainman.strTrainmanNumber);

                ganbujl.TrainmanLeaveArray.Add(leaveInfo);
            }
            return ganbujl;
        }

        public void InitGroup(Groupmp GP, DataRow DR, DataTable DTNoDispatching)
        {
            GP.strGroupGUID = DR["strGroupGUID"].ToString();
            GP.strTrainPlanGUID = DR["strTrainPlanGUID"].ToString();
            GP.GroupState = 2;
            if (DR["GroupState"].ToString() != "")
            {
                GP.GroupState = 3;
                if (int.Parse(DR["GroupState"].ToString()) == 7)
                {
                    GP.GroupState = 6;
                }
            }
            if (DR["strTrainmanGUID1"].ToString() != "")
            {
                GP.Trainman1.strTrainmanGUID = DR["strTrainmanGUID1"].ToString();
                GP.Trainman1.strTrainmanName = DR["strTrainmanName1"].ToString();
                GP.Trainman1.strTrainmanNumber = DR["strTrainmanNumber1"].ToString();
                GP.Trainman1.nPostID = 1;
                GP.Trainman1.isKey = ObjectConvertClass.static_ext_int(DR["bIsKey1"]);

                if (DR["nPost1"].ToString() != "")
                    GP.Trainman1.nPostID = int.Parse(DR["nPost1"].ToString());

                GP.Trainman1.nDispatching = checkIsDispatching(GP.Trainman1.strTrainmanNumber, DTNoDispatching);


            }
            if (DR["strTrainmanGUID2"].ToString() != "")
            {
                GP.Trainman2.strTrainmanGUID = DR["strTrainmanGUID2"].ToString();
                GP.Trainman2.strTrainmanName = DR["strTrainmanName2"].ToString();
                GP.Trainman2.strTrainmanNumber = DR["strTrainmanNumber2"].ToString();
                GP.Trainman2.nPostID = 1;
                GP.Trainman2.isKey = ObjectConvertClass.static_ext_int(DR["bIsKey2"]);
                if (DR["nPost2"].ToString() != "")
                    GP.Trainman2.nPostID = int.Parse(DR["nPost2"].ToString());
                GP.Trainman2.nDispatching = checkIsDispatching(GP.Trainman2.strTrainmanNumber, DTNoDispatching);

            }
            if (DR["strTrainmanGUID3"].ToString() != "")
            {
                GP.Trainman3.strTrainmanGUID = DR["strTrainmanGUID3"].ToString();
                GP.Trainman3.strTrainmanName = DR["strTrainmanName3"].ToString();
                GP.Trainman3.strTrainmanNumber = DR["strTrainmanNumber3"].ToString();
                GP.Trainman3.nPostID = 1;
                GP.Trainman3.isKey = ObjectConvertClass.static_ext_int(DR["bIsKey3"]);
                if (DR["nPost3"].ToString() != "")
                {
                    GP.Trainman3.nPostID = int.Parse(DR["nPost3"].ToString());
                }

                GP.Trainman3.nDispatching = checkIsDispatching(GP.Trainman3.strTrainmanNumber, DTNoDispatching);
            }
            if (DR["strTrainmanGUID4"].ToString() != "")
            {
                GP.Trainman4.strTrainmanGUID = DR["strTrainmanGUID4"].ToString();
                GP.Trainman4.strTrainmanName = DR["strTrainmanName4"].ToString();
                GP.Trainman4.strTrainmanNumber = DR["strTrainmanNumber4"].ToString();
                GP.Trainman4.nPostID = 1;
                GP.Trainman4.isKey = ObjectConvertClass.static_ext_int(DR["bIsKey4"]);
                if (DR["nPost4"].ToString() != "")
                {
                    GP.Trainman4.nPostID = int.Parse(DR["nPost4"].ToString());
                }

                GP.Trainman4.nDispatching = checkIsDispatching(GP.Trainman4.strTrainmanNumber, DTNoDispatching);
            }

        }
        public void InitNamedJiaolu(DataTable DTUnrun, DataTable DTPrepare, DataTable DTNamed, DataTable DTOrder, DataTable DTTrain, JiaoluArray Jiaolus, DataTable DTNoDispatching)
        {
            string strTrainmanJiaoluGUID = "";
            NamedJiaolu namedJiaolu = null;
            for (int i = 0; i < DTNamed.Rows.Count; i++)
            {
                if (strTrainmanJiaoluGUID != DTNamed.Rows[i]["strTrainmanJiaoluGUID"].ToString())
                {
                    strTrainmanJiaoluGUID = DTNamed.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                    //添加记名式交路
                    namedJiaolu = new NamedJiaolu();
                    namedJiaolu.JiaoluGUID = DTNamed.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                    namedJiaolu.JiaoluName = DTNamed.Rows[i]["strTrainmanJiaoluName"].ToString();
                    namedJiaolu.JiaoluType = int.Parse(DTNamed.Rows[i]["nJiaoluType"].ToString());
                    //添加非运转人员
                    for (int k = 0; k < DTUnrun.Rows.Count; k++)
                    {
                        if (DTUnrun.Rows[k]["strTrainmanJiaoluGUID"].ToString() == strTrainmanJiaoluGUID)
                        {
                            TrainmanLeaveInfo trainmanLeave = new TrainmanLeaveInfo();
                            trainmanLeave.strLeaveTypeGUID = DTUnrun.Rows[k]["strLeaveTypeGUID"].ToString();
                            trainmanLeave.strLeaveTypeName = DTUnrun.Rows[k]["strLeaveTypeName"].ToString();
                            trainmanLeave.Trainman.strTrainmanGUID = DTUnrun.Rows[k]["strTrainmanGUID"].ToString();
                            trainmanLeave.Trainman.strTrainmanName = DTUnrun.Rows[k]["strTrainmanName"].ToString();
                            trainmanLeave.Trainman.strTrainmanNumber = DTUnrun.Rows[k]["strTrainmanNumber"].ToString();

                            trainmanLeave.Trainman.nDispatching = checkIsDispatching(trainmanLeave.Trainman.strTrainmanNumber, DTNoDispatching);
                            trainmanLeave.Trainman.nPostID = 1;
                            trainmanLeave.Trainman.isKey = ObjectConvertClass.static_ext_int(DTUnrun.Rows[k]["bIsKey"]);

                            if (DTUnrun.Rows[k]["nPostID"].ToString() != "")
                                trainmanLeave.Trainman.nPostID = int.Parse(DTUnrun.Rows[k]["nPostID"].ToString());
                            namedJiaolu.TrainmanLeaveArray.Add(trainmanLeave);
                        }


                    }
                    //添加预备人员
                    for (int k = 0; k < DTPrepare.Rows.Count; k++)
                    {
                        if (DTPrepare.Rows[k]["strTrainmanJiaoluGUID"].ToString() == strTrainmanJiaoluGUID)
                        {

                            TrainmanLeaveInfo trainmanPrepare = new TrainmanLeaveInfo();
                            trainmanPrepare.strLeaveTypeGUID = DTPrepare.Rows[k]["strLeaveTypeGUID"].ToString();
                            trainmanPrepare.strLeaveTypeName = DTPrepare.Rows[k]["strLeaveTypeName"].ToString();
                            trainmanPrepare.Trainman.strTrainmanGUID = DTPrepare.Rows[k]["strTrainmanGUID"].ToString();
                            trainmanPrepare.Trainman.strTrainmanName = DTPrepare.Rows[k]["strTrainmanName"].ToString();
                            trainmanPrepare.Trainman.strTrainmanNumber = DTPrepare.Rows[k]["strTrainmanNumber"].ToString();

                            trainmanPrepare.Trainman.nDispatching = checkIsDispatching(trainmanPrepare.Trainman.strTrainmanNumber, DTNoDispatching);

                            trainmanPrepare.Trainman.nPostID = 1;
                            trainmanPrepare.Trainman.isKey = ObjectConvertClass.static_ext_int(DTPrepare.Rows[k]["bIsKey"]);
                            trainmanPrepare.Trainman.nReadyOverTime = Convert.ToInt32(DTPrepare.Rows[k]["nReadyOverTime"]);
                            if (DTPrepare.Rows[k]["nPostID"].ToString() != "")
                                trainmanPrepare.Trainman.nPostID = int.Parse(DTPrepare.Rows[k]["nPostID"].ToString());
                            namedJiaolu.TrainmanLeaveArray.Add(trainmanPrepare);
                        }
                    }
                    Jiaolus.Add(namedJiaolu);
                }
                //添加机组
                NamedGroup nameGroup = new NamedGroup();
                nameGroup.dtLastArriveTime = DateTime.Parse("2000-01-01");
                if (DTNamed.Rows[i]["dtLastArriveTime"].ToString() != "")
                    nameGroup.dtLastArriveTime = DateTime.Parse(DTNamed.Rows[i]["dtLastArriveTime"].ToString());
                nameGroup.nCheciOrder = int.Parse(DTNamed.Rows[i]["nCheciOrder"].ToString());
                nameGroup.nCheciType = int.Parse(DTNamed.Rows[i]["nCheciType"].ToString());
                nameGroup.strCheci1 = DTNamed.Rows[i]["strCheci1"].ToString();
                nameGroup.strCheci2 = DTNamed.Rows[i]["strCheci2"].ToString();
                nameGroup.strCheciGUID = DTNamed.Rows[i]["strCheciGUID"].ToString();
                nameGroup.strTrainmanJiaoluGUID = DTNamed.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                namedJiaolu.NamedGroupArray.Add(nameGroup);
                InitGroup(nameGroup.Group, DTNamed.Rows[i], DTNoDispatching);
            }
        }

        public void InitOrderJiaolu(DataTable DTUnrun, DataTable DTPrepare, DataTable DTNamed, DataTable DTOrder, DataTable DTTrain, JiaoluArray Jiaolus, DataTable DTNoDispatching)
        {
            string strTrainmanJiaoluGUID = "";
            OrderJiaolu namedJiaolu = null;
            for (int i = 0; i < DTOrder.Rows.Count; i++)
            {

                if (strTrainmanJiaoluGUID != DTOrder.Rows[i]["strTrainmanJiaoluGUID"].ToString())
                {
                    strTrainmanJiaoluGUID = DTOrder.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                    //添加记名式交路
                    namedJiaolu = new OrderJiaolu();
                    namedJiaolu.JiaoluGUID = DTOrder.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                    namedJiaolu.JiaoluName = DTOrder.Rows[i]["strTrainmanJiaoluName"].ToString();
                    namedJiaolu.JiaoluType = int.Parse(DTOrder.Rows[i]["nJiaoluType"].ToString());
                    //添加非运转人员
                    for (int k = 0; k < DTUnrun.Rows.Count; k++)
                    {
                        if (DTUnrun.Rows[k]["strTrainmanJiaoluGUID"].ToString() == strTrainmanJiaoluGUID)
                        {
                            TrainmanLeaveInfo trainmanLeave = new TrainmanLeaveInfo();
                            trainmanLeave.strLeaveTypeGUID = DTUnrun.Rows[k]["strLeaveTypeGUID"].ToString();
                            trainmanLeave.strLeaveTypeName = DTUnrun.Rows[k]["strLeaveTypeName"].ToString();
                            trainmanLeave.Trainman.strTrainmanGUID = DTUnrun.Rows[k]["strTrainmanGUID"].ToString();
                            trainmanLeave.Trainman.strTrainmanName = DTUnrun.Rows[k]["strTrainmanName"].ToString();
                            trainmanLeave.Trainman.strTrainmanNumber = DTUnrun.Rows[k]["strTrainmanNumber"].ToString();
                            trainmanLeave.Trainman.isKey = ObjectConvertClass.static_ext_int(DTUnrun.Rows[k]["bIsKey"]);

                            trainmanLeave.Trainman.nDispatching = checkIsDispatching(trainmanLeave.Trainman.strTrainmanNumber, DTNoDispatching);

                            trainmanLeave.Trainman.nPostID = 1;
                            if (DTUnrun.Rows[k]["nPostID"].ToString() != "")
                                trainmanLeave.Trainman.nPostID = int.Parse(DTUnrun.Rows[k]["nPostID"].ToString()); ;
                            namedJiaolu.TrainmanLeaveArray.Add(trainmanLeave);
                        }


                    }
                    //添加预备人员
                    for (int k = 0; k < DTPrepare.Rows.Count; k++)
                    {
                        if (DTPrepare.Rows[k]["strTrainmanJiaoluGUID"].ToString() == strTrainmanJiaoluGUID)
                        {
                            TrainmanLeaveInfo trainmanPrepare = new TrainmanLeaveInfo();
                            trainmanPrepare.strLeaveTypeGUID = DTPrepare.Rows[k]["strLeaveTypeGUID"].ToString();
                            trainmanPrepare.strLeaveTypeName = DTPrepare.Rows[k]["strLeaveTypeName"].ToString();
                            trainmanPrepare.Trainman.strTrainmanGUID = DTPrepare.Rows[k]["strTrainmanGUID"].ToString();
                            trainmanPrepare.Trainman.strTrainmanName = DTPrepare.Rows[k]["strTrainmanName"].ToString();
                            trainmanPrepare.Trainman.strTrainmanNumber = DTPrepare.Rows[k]["strTrainmanNumber"].ToString();


                            trainmanPrepare.Trainman.nDispatching = checkIsDispatching(trainmanPrepare.Trainman.strTrainmanNumber, DTNoDispatching);

                            trainmanPrepare.Trainman.nPostID = 1;
                            trainmanPrepare.Trainman.isKey = ObjectConvertClass.static_ext_int(DTPrepare.Rows[k]["bIsKey"]);
                            trainmanPrepare.Trainman.nReadyOverTime = Convert.ToInt32(DTPrepare.Rows[k]["nReadyOverTime"]);
                            if (DTPrepare.Rows[k]["nPostID"].ToString() != "")
                                trainmanPrepare.Trainman.nPostID = int.Parse(DTPrepare.Rows[k]["nPostID"].ToString());
                            namedJiaolu.TrainmanLeaveArray.Add(trainmanPrepare);
                        }
                    }
                    Jiaolus.Add(namedJiaolu);
                }
                //添加机组
                OrderGroupAdd nameGroup = new OrderGroupAdd();
                nameGroup.dtLastArriveTime = DateTime.Parse("2000-01-01");
                if (DTOrder.Rows[i]["dtLastArriveTime"].ToString() != "")
                    nameGroup.dtLastArriveTime = DateTime.Parse(DTOrder.Rows[i]["dtLastArriveTime"].ToString());
                nameGroup.nOrder = int.Parse(DTOrder.Rows[i]["nOrder"].ToString());
                nameGroup.strOrderGUID = DTOrder.Rows[i]["strOrderGUID"].ToString();

                nameGroup.strTrainmanJiaoluGUID = DTOrder.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                namedJiaolu.OrderGroupArray.Add(nameGroup);
                InitGroup(nameGroup.Group, DTOrder.Rows[i], DTNoDispatching);

            }
        }

        public void InitTogetherJiaolu(DataTable DTUnrun, DataTable DTPrepare, DataTable DTNamed, DataTable DTOrder, DataTable DTTrain, JiaoluArray Jiaolus, DataTable DTNoDispatching)
        {
            string strTrainmanJiaoluGUID = "";
            string strTogetherTrainGUID = "";
            TogetherTrain train = null;
            TogetherTrainJiaoLu namedJiaolu = null;
            for (int i = 0; i < DTTrain.Rows.Count; i++)
            {

                if (strTrainmanJiaoluGUID != DTTrain.Rows[i]["strTrainmanJiaoluGUID"].ToString())
                {
                    strTrainmanJiaoluGUID = DTTrain.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                    //添加记名式交路
                    namedJiaolu = new TogetherTrainJiaoLu();
                    namedJiaolu.JiaoluGUID = DTTrain.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                    namedJiaolu.JiaoluName = DTTrain.Rows[i]["strTrainmanJiaoluName"].ToString();
                    namedJiaolu.JiaoluType = 0;
                    if (DTTrain.Rows[i]["nJiaoluType"].ToString() != "")
                        namedJiaolu.JiaoluType = int.Parse(DTTrain.Rows[i]["nJiaoluType"].ToString());
                    //添加非运转人员
                    for (int k = 0; k < DTUnrun.Rows.Count; k++)
                    {
                        if (DTUnrun.Rows[k]["strTrainmanJiaoluGUID"].ToString() == strTrainmanJiaoluGUID)
                        {
                            TrainmanLeaveInfo trainmanLeave = new TrainmanLeaveInfo();
                            trainmanLeave.strLeaveTypeGUID = DTUnrun.Rows[k]["strLeaveTypeGUID"].ToString();
                            trainmanLeave.strLeaveTypeName = DTUnrun.Rows[k]["strLeaveTypeName"].ToString();
                            trainmanLeave.Trainman.strTrainmanGUID = DTUnrun.Rows[k]["strTrainmanGUID"].ToString();
                            trainmanLeave.Trainman.strTrainmanName = DTUnrun.Rows[k]["strTrainmanName"].ToString();
                            trainmanLeave.Trainman.strTrainmanNumber = DTUnrun.Rows[k]["strTrainmanNumber"].ToString();


                            trainmanLeave.Trainman.nDispatching = checkIsDispatching(trainmanLeave.Trainman.strTrainmanNumber, DTNoDispatching);
                            trainmanLeave.Trainman.isKey = ObjectConvertClass.static_ext_int(DTUnrun.Rows[k]["bIsKey"]);
                            trainmanLeave.Trainman.nPostID = 1;
                            if (DTUnrun.Rows[k]["nPostID"].ToString() != "")
                                trainmanLeave.Trainman.nPostID = int.Parse(DTUnrun.Rows[k]["nPostID"].ToString());
                            namedJiaolu.TrainmanLeaveArray.Add(trainmanLeave);
                        }


                    }
                    //添加预备人员
                    for (int k = 0; k < DTPrepare.Rows.Count; k++)
                    {
                        if (DTPrepare.Rows[k]["strTrainmanJiaoluGUID"].ToString() == strTrainmanJiaoluGUID)
                        {
                            TrainmanLeaveInfo trainmanPrepare = new TrainmanLeaveInfo();
                            trainmanPrepare.strLeaveTypeGUID = DTPrepare.Rows[k]["strLeaveTypeGUID"].ToString();
                            trainmanPrepare.strLeaveTypeName = DTPrepare.Rows[k]["strLeaveTypeName"].ToString();
                            trainmanPrepare.Trainman.strTrainmanGUID = DTPrepare.Rows[k]["strTrainmanGUID"].ToString();
                            trainmanPrepare.Trainman.strTrainmanName = DTPrepare.Rows[k]["strTrainmanName"].ToString();
                            trainmanPrepare.Trainman.strTrainmanNumber = DTPrepare.Rows[k]["strTrainmanNumber"].ToString();

                            trainmanPrepare.Trainman.nDispatching = checkIsDispatching(trainmanPrepare.Trainman.strTrainmanNumber, DTNoDispatching);

                            trainmanPrepare.Trainman.nPostID = 1;
                            trainmanPrepare.Trainman.isKey = ObjectConvertClass.static_ext_int(DTPrepare.Rows[k]["bIsKey"]);
                            trainmanPrepare.Trainman.nReadyOverTime = Convert.ToInt32(DTPrepare.Rows[k]["nReadyOverTime"]);
                            if (DTPrepare.Rows[k]["nPostID"].ToString() != "")
                                trainmanPrepare.Trainman.nPostID = int.Parse(DTPrepare.Rows[k]["nPostID"].ToString());
                            namedJiaolu.TrainmanLeaveArray.Add(trainmanPrepare);
                        }
                    }
                    Jiaolus.Add(namedJiaolu);
                }
                if (strTogetherTrainGUID != DTTrain.Rows[i]["strTrainGUID"].ToString())
                {
                    train = new TogetherTrain();
                    train.strTrainGUID = DTTrain.Rows[i]["strTrainGUID"].ToString();
                    train.strTrainNumber = DTTrain.Rows[i]["strTrainNumber"].ToString();


                    train.strTrainTypeName = DTTrain.Rows[i]["strTrainTypeName"].ToString();
                    namedJiaolu.TogetherTrainArray.Add(train);
                    strTogetherTrainGUID = train.strTrainGUID;
                }

                //添加机组
                OrderGroupInTrain nameGroup = new OrderGroupInTrain();
                nameGroup.dtLastArriveTime = DateTime.Parse("2000-01-01");
                if (DTTrain.Rows[i]["dtLastArriveTime"].ToString() != "")
                    nameGroup.dtLastArriveTime = DateTime.Parse(DTTrain.Rows[i]["dtLastArriveTime"].ToString());
                nameGroup.nOrder = 0;
                if (DTTrain.Rows[i]["nOrder"].ToString() != "")
                    nameGroup.nOrder = int.Parse(DTTrain.Rows[i]["nOrder"].ToString());
                nameGroup.strOrderGUID = DTTrain.Rows[i]["strOrderGUID"].ToString();

                nameGroup.strTrainGUID = DTTrain.Rows[i]["strTrainGUID"].ToString();
                train.Groups.Add(nameGroup);
                InitGroup(nameGroup.Group, DTTrain.Rows[i], DTNoDispatching);

            }
        }

        /// <summary>
        /// 填充交路名牌数据
        /// </summary>
        /// <param name="DTUnrun">非运转人员</param>
        /// <param name="DTPrepare">预备人员</param>
        /// <param name="DTNamed">记名式交路名牌</param>
        /// <param name="DTOrder">轮乘交路名牌</param>
        /// <param name="DTTrain">包乘交路名牌</param>
        public void InitJiaoluArray(DataTable DTUnrun, DataTable DTPrepare, DataTable DTNamed, DataTable DTOrder, DataTable DTTrain, JiaoluArray Jiaolus,DataTable dt)
        {
            
            InitNamedJiaolu(DTUnrun, DTPrepare, DTNamed, DTOrder, DTTrain, Jiaolus, dt);
            InitOrderJiaolu(DTUnrun, DTPrepare, DTNamed, DTOrder, DTTrain, Jiaolus, dt);
            InitTogetherJiaolu(DTUnrun, DTPrepare, DTNamed, DTOrder, DTTrain, Jiaolus, dt);
        }


        //获取所有未派班的人员
        public DataTable getNoDispatching(string strWorkShopGUID)
        {
            //获取交路的排序信息
            string strSql = @"select distinct(strTrainmanNumber) from  TAB_Drink_Information where dtCreateTime>dateadd(HH,-24,getdate()) and strWorkShopGUID=@strWorkShopGUID";
            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                           };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            return dt;
        }

        //判断24小时之内人员是否派过班
        public int checkIsDispatching(string TNumber,DataTable dt)
        {

            DataView view = dt.DefaultView;
            view.RowFilter = string.Format("strTrainmanNumber={0} ", "'" + TNumber.ToString() + "'");
            if (view.ToTable().Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }




        #endregion

        #region 变换机组的出勤点
        /// <summary>
        /// 变换机组的出勤点
        /// </summary>
        /// <param name="groupID">机组ID</param>
        /// <param name="sourcePlaceID">源出勤点id</param>
        /// <param name="destPlaceID">目标出勤点id</param>
        public void ChangeDutyPlace(string groupID, string sourcePlaceID, string destPlaceID)
        {
            if (groupID == null || sourcePlaceID == null || destPlaceID == null)
                throw new Exception("所传参数不能是null");

            if (!DBNameBoard.ChangeGroupPlace(groupID, sourcePlaceID, destPlaceID))
                throw new Exception("数据提交失败：未找到指定的机组信息");
        }
        #endregion

        #region 获取预备人员队列
        /// <summary>
        /// 通过车间 获取预备人员队列
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <returns></returns>
        public RRsTrainmanList GetPreparedTrainmans(string strWorkShopGUID)
        {
            RRsTrainmanList l = new RRsTrainmanList();
            DBNameBoard db = new DBNameBoard();
            return db.GetPreparedTrainmans(strWorkShopGUID);
        }
        #endregion

        #region 获取非运转人员列表
        public RRsTrainmanList GetUnRunTrainmans(string strWorkShopGUID)
        {
            RRsTrainmanList l = new RRsTrainmanList();
            DBNameBoard db = new DBNameBoard();
            return db.GetUnRunTrainmans(strWorkShopGUID);
        }
        #endregion

        #region 获取客户端管辖的机车交路
        public TrainJiaoluList GetAllTrainJiaoluArrayOfSite(string strSiteGUID)
        {
            DBNameBoard db = new DBNameBoard();
            return db.GetAllTrainJiaoluArrayOfSite(strSiteGUID);
        }
        #endregion

        #region 获取人员交路

        public class TrainmanJiaolu_In
        {
            public string strTrainjiaoluGUID = "";
        }

        public class TrainmanJiaolu_Out : JsonOutBase
        {
            public TrainmanJiaoluList data = new TrainmanJiaoluList();
        }

        public TrainmanJiaolu_Out GetTrainmanJiaolusOfTrainJiaolu(string input)
        {
            TrainmanJiaolu_Out json = new TrainmanJiaolu_Out();
            TrainmanJiaolu_In data = Newtonsoft.Json.JsonConvert.DeserializeObject<TrainmanJiaolu_In>(input);
            TF.RunSafty.NamePlate.MD.TrainmanJiaolu trainmanJiaolu = null;
            try
            {
                string strSql = string.Format("select * from VIEW_Base_JiaoluRelation where 1=1 ");
                if (!string.IsNullOrEmpty(data.strTrainjiaoluGUID))
                {
                    strSql += string.Format("and strTrainJiaoluGUID ='{0}' ", data.strTrainjiaoluGUID);
                }
                strSql += " order by strTrainmanJiaoluName ";
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                foreach (DataRow dataRow in table.Rows)
                {
                    trainmanJiaolu = new TF.RunSafty.NamePlate.MD.TrainmanJiaolu();
                    ADOQueryToTrainmanJiaolu(dataRow, trainmanJiaolu);
                    json.data.Add(trainmanJiaolu);
                }
                json.result = "0";
                json.resultStr = "获取人员交路成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }

        public void ADOQueryToTrainmanJiaolu(DataRow row, TF.RunSafty.NamePlate.MD.TrainmanJiaolu trainmanJiaolu)
        {
            int nDragTypeID, nJiaoluType, nKeHuoID, nTrainmanRunType, nTrainmanTypeID;
            if (int.TryParse(row["nDragTypeID"].ToString(), out nDragTypeID))
            {
                trainmanJiaolu.nDragTypeID = nDragTypeID;
            }
            if (int.TryParse(row["nJiaoluType"].ToString(), out nJiaoluType))
            {
                trainmanJiaolu.nJiaoluType = nJiaoluType;
            }
            if (int.TryParse(row["nKeHuoID"].ToString(), out nKeHuoID))
            {
                trainmanJiaolu.nKeHuoID = nKeHuoID;
            }
            if (int.TryParse(row["nTrainmanRunType"].ToString(), out nTrainmanRunType))
            {
                trainmanJiaolu.nTrainmanRunType = nTrainmanRunType;
            }
            if (int.TryParse(row["nTrainmanTypeID"].ToString(), out nTrainmanTypeID))
            {
                trainmanJiaolu.nTrainmanTypeID = nTrainmanTypeID;
            }
            //trainmanJiaolu.strAreaGUID = row["strAreaGUID"].ToString();
            //trainmanJiaolu.strEndStationGUID = row["strEndStationGUID"].ToString();
            trainmanJiaolu.strEndStationName = row["strEndStationName"].ToString();
            //trainmanJiaolu.strLineGUID = row["strLineGUID"].ToString();
            //trainmanJiaolu.strStartStationGUID = row["strStartStationGUID"].ToString();
            trainmanJiaolu.strStartStationName = row["strStartStationName"].ToString();
            trainmanJiaolu.strTrainmanJiaoluGUID = row["strTrainmanJiaoluGUID"].ToString();
            trainmanJiaolu.strTrainmanJiaoluName = row["strTrainmanJiaoluName"].ToString();
        }
        #endregion

        #region 获取记名式内机班信息

        public class NameGroup_In
        {
            public string strTrainmanJiaoluGUID = "";
        }

        public class NameGroup_Out : JsonOutBase
        {
            public NameGroupList data = new NameGroupList();
        }

        public NameGroup_Out GetNamedJiaoluGroups(string input)
        {
            NameGroup_Out json = new NameGroup_Out();
            NameGroup_In data = Newtonsoft.Json.JsonConvert.DeserializeObject<NameGroup_In>(input);
            string strSql = string.Format(@" select * from VIEW_Nameplate_TrainmanJiaolu_Named  
     where (nTxState = 0 ) or (nTxState is null) and strTrainmanJiaoluGUID= '{0}' order by nCheciOrder", data.strTrainmanJiaoluGUID);
            RRsNamedGroup group = null;

            try
            {
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                foreach (DataRow dataRow in table.Rows)
                {
                    group = new RRsNamedGroup();
                    SetGroupValue(group, dataRow);
                    json.data.Add(group);
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }

        public void GroupFromADOQuery(Group group, DataRow row)
        {
            int tsNil = (int)TF.RunSafty.NamePlate.MD.TRsTrainmanState.tsNil;
            group.groupID = row["strGroupGUID"].ToString();
            group.trainPlanID = ObjectConvertClass.static_ext_string(row["strTrainPlanGUID"]);
            int postID;

            group.trainman1.trainmanID = row["strTrainmanGUID1"].ToString();
            group.trainman1.trainmanName = row["strTrainmanName1"].ToString();
            group.trainman1.trainmanNumber = row["strTrainmanNumber1"].ToString();
            group.trainman1.telNumber = row["strTelNumber1"].ToString();
            group.trainman1.strFixedGroupID = ObjectConvertClass.static_ext_string(row["strFixedGroupGUID1"]);
            group.trainman1.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(row["dtLastEndworkTime1"], DateTime.MinValue);
            Int32.TryParse(row["bIsKey1"].ToString(), out group.trainman1.isKey);
            if (row["nTrainmanState1"] == DBNull.Value)
                group.trainman1.trainmanState = tsNil;
            else
                group.trainman1.trainmanState = Convert.ToInt32(row["nTrainmanState1"]);
            Int32.TryParse(row["nPost1"].ToString(), out group.trainman1.postID);


            group.trainman2.trainmanID = row["strTrainmanGUID2"].ToString();
            group.trainman2.trainmanName = row["strTrainmanName2"].ToString();
            group.trainman2.trainmanNumber = row["strTrainmanNumber2"].ToString();
            group.trainman2.telNumber = row["strTelNumber2"].ToString();
            group.trainman2.strFixedGroupID = ObjectConvertClass.static_ext_string(row["strFixedGroupGUID2"]);
            group.trainman2.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(row["dtLastEndworkTime2"], DateTime.MinValue);
            Int32.TryParse(row["bIsKey2"].ToString(), out group.trainman2.isKey);
            if (row["nTrainmanState2"] == DBNull.Value)
                group.trainman2.trainmanState = tsNil;
            else
                group.trainman2.trainmanState = Convert.ToInt32(row["nTrainmanState2"]);
            Int32.TryParse(row["nPost2"].ToString(), out group.trainman2.postID);

            group.trainman3.trainmanID = row["strTrainmanGUID3"].ToString();
            group.trainman3.trainmanName = row["strTrainmanName3"].ToString();
            group.trainman3.trainmanNumber = row["strTrainmanNumber3"].ToString();
            group.trainman3.telNumber = row["strTelNumber3"].ToString();
            group.trainman3.strFixedGroupID = ObjectConvertClass.static_ext_string(row["strFixedGroupGUID3"]);
            group.trainman3.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(row["dtLastEndworkTime3"], DateTime.MinValue);
            Int32.TryParse(row["bIsKey3"].ToString(), out group.trainman3.isKey);
            if (row["nTrainmanState3"] == DBNull.Value)
                group.trainman3.trainmanState = tsNil;
            else
                group.trainman3.trainmanState = Convert.ToInt32(row["nTrainmanState3"]);
            Int32.TryParse(row["nPost3"].ToString(), out group.trainman3.postID);

            group.trainman4.trainmanID = row["strTrainmanGUID4"].ToString();
            group.trainman4.trainmanName = row["strTrainmanName4"].ToString();
            group.trainman4.trainmanNumber = row["strTrainmanNumber4"].ToString();
            group.trainman4.telNumber = row["strTelNumber4"].ToString();
            group.trainman4.strFixedGroupID = ObjectConvertClass.static_ext_string(row["strFixedGroupGUID4"]);
            group.trainman4.lastEndworkTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(row["dtLastEndworkTime4"], DateTime.MinValue);
            Int32.TryParse(row["bIsKey4"].ToString(), out group.trainman4.isKey);

            if (row["nTrainmanState4"] == DBNull.Value)
                group.trainman4.trainmanState = tsNil;
            else
                group.trainman4.trainmanState = Convert.ToInt32(row["nTrainmanState4"]);
            Int32.TryParse(row["nPost4"].ToString(), out group.trainman4.postID);
            DateTime dtArriveTime;
            if (DateTime.TryParse(row["dtLastArriveTime"].ToString(), out dtArriveTime))
            {
                group.arriveTime = dtArriveTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            int groupState = (int)TF.RunSafty.NamePlate.MD.TRsTrainmanState.tsNormal;
            group.groupState = groupState;
            if (row["GroupState"] != DBNull.Value)
            {
                group.groupState = (int)TF.RunSafty.NamePlate.MD.TRsTrainmanState.tsPlaning;
                if (row["GroupState"].ToString() == ((int)TRsPlanState.psBeginWork).ToString())
                {
                    group.groupState = (int)TF.RunSafty.NamePlate.MD.TRsTrainmanState.tsRuning;
                }
            }

        }

        private void SetGroupValue(RRsNamedGroup group, DataRow dataRow)
        {
            int nCheciOrder, nCheciType;
            DateTime dtLastArriveTime;
            string strGroupGUID = "";
            group.strCheciGUID = dataRow["strCheciGUID"].ToString();
            group.strTrainmanJiaoluGUID = dataRow["strTrainmanJiaoluGUID"].ToString();
            if (int.TryParse(dataRow["nCheciOrder"].ToString(), out nCheciOrder))
            {
                group.nCheciOrder = nCheciOrder;
            }
            if (int.TryParse(dataRow["nCheciType"].ToString(), out nCheciType))
            {
                group.nCheciType = nCheciType;
            }
            group.strCheci1 = dataRow["strCheci1"].ToString();
            group.strCheci2 = dataRow["strCheci2"].ToString();
            if (DateTime.TryParse(dataRow["dtLastArriveTime"].ToString(), out dtLastArriveTime))
            {
                group.dtLastArriveTime = dtLastArriveTime;
            }
            strGroupGUID = dataRow["strGroupGUID"].ToString();
            GroupFromADOQuery(group.Group, dataRow);
        }
        #endregion

        #region 获取包乘机组

        public class Together_In
        {
            public string strTrainmanJiaoluGUID = "";
            public string strTrainmanNumber = "";
            public string strTrainmanName = "";
        }

        public class Together_Out : JsonOutBase
        {
            public TRsTogetherTrainArray data = new TRsTogetherTrainArray();
        }

        public Together_Out GetTogetherTrains(string input)
        {
            Together_Out json = new Together_Out();
            bool bFind = false;
            Together_In data = Newtonsoft.Json.JsonConvert.DeserializeObject<Together_In>(input);
            string strTrainmanJiaoluGUID = data.strTrainmanJiaoluGUID;
            string strTrainmanName = data.strTrainmanName;
            string strTrainmanNumber = data.strTrainmanNumber;
            string strSql = "select * from VIEW_Nameplate_TrainmanJiaolu_TogetherTrain where 1=1  and (nTxState = 0 ) or (nTxState is null)  ";
            if (!string.IsNullOrEmpty(strTrainmanJiaoluGUID))
            {
                strSql += string.Format(" and strTrainmanJiaoluGUID ='{0}' ", strTrainmanJiaoluGUID);
            }
            if (!string.IsNullOrEmpty(strTrainmanNumber))
            {
                strSql +=
                    string.Format(
                        "and (strTrainmanNumber1 = '{0}' or strTrainmanNumber2 = '{0}' or strTrainmanNumber3 = '{0}' or strTrainmanNumber4='{0}') ",
                        strTrainmanNumber);
            }
            if (!string.IsNullOrEmpty(strTrainmanName))
            {
                strSql +=
                    string.Format(
                        "and (strTrainmanName1 ='{0}'  or strTrainmanName2 = '{0}' or strTrainmanName3 = '{0}'  or strTrainmanName4 ='{0}' )",
                        strTrainmanName);
            }
            strSql += " order by strTrainNumber,nOrder";
            try
            {
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                int length = 0;
                RRsTogetherTrain groupInTrain = null;
                foreach (DataRow dataRow in table.Rows)
                {
                    string strTrainGUID = dataRow["strTrainGUID"].ToString();
                    groupInTrain = HasTrainAdded(json.data, strTrainGUID);
                    if (groupInTrain == null)
                    {
                        groupInTrain = new RRsTogetherTrain();
                        //不存在机车信息。添加机车信息
                        groupInTrain.strTrainGUID = dataRow["strTrainGUID"].ToString();
                        groupInTrain.strTrainmanJiaoluGUID = dataRow["strTrainmanJiaoluGUID"].ToString();
                        groupInTrain.strTrainTypeName = dataRow["strTrainTypeName"].ToString();
                        groupInTrain.strTrainNumber = dataRow["strTrainNumber"].ToString();
                        json.data.Add(groupInTrain);
                    }
                    //添加机车内的机组信息RRsOrderGroupInTrain

                    string strOrderGUID = dataRow["strOrderGUID"].ToString();
                    if (!string.IsNullOrEmpty(strOrderGUID))
                    {
                        RRsOrderGroupInTrain group = new RRsOrderGroupInTrain();
                        groupInTrain.Groups.Add(group);
                        group.strOrderGUID = dataRow["strOrderGUID"].ToString();
                        group.strTrainGUID = dataRow["strTrainGUID"].ToString();
                        int.TryParse(dataRow["nOrder"].ToString(), out group.nOrder);
                        DateTime.TryParse(dataRow["dtLastArriveTime"].ToString(), out group.dtLastArriveTime);
                        int.TryParse(dataRow["nOrder"].ToString(), out group.nOrder);
                        Group namedGroup = new Group();
                        GroupFromADOQuery(namedGroup, dataRow);
                        group.Group = namedGroup;
                    }
                }

                json.result = "0";
                json.resultStr = "获取成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw;
            }
            return json;
        }

        private RRsTogetherTrain HasTrainAdded(List<RRsTogetherTrain> list, string strTrainGUID)
        {
            return list.FirstOrDefault(train => train.strTrainGUID.Equals(strTrainGUID));
        }

        #endregion

        #region 添加记名式机组信息

        public class Add_In
        {
            public string TrainmanJiaoluGUID;
            public RRsNamedGroup nameGroup;
        }

        public void AddTrainmanJiaoLuToTrainman(SqlTransaction transaction, SqlCommand command, string TrainmanGUID, string TrainmanJiaoLuGUID)
        {
            if (string.IsNullOrEmpty(TrainmanGUID))
                return;
            string strSql = "update TAB_Org_Trainman set strTrainmanJiaoluGUID  =@strTrainmanJiaoluGUID where strTrainmanGUID=@strTrainmanGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
           {
               new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoLuGUID), 
               new SqlParameter("strTrainmanGUID",TrainmanGUID), 
           };
            command.CommandText = strSql;
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// 添加记名式机组信息
        /// <summary>
        public InterfaceOutPut AddNamedGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            SqlTransaction transaction = null;
            SqlConnection connection = new SqlConnection();
            try
            {
                Add_In model = Newtonsoft.Json.JsonConvert.DeserializeObject<Add_In>(Data);
                RRsNamedGroup group = model.nameGroup;
                string strTrainmanJiaoluGUID = model.TrainmanJiaoluGUID;
                string groupGUID = string.Empty,
                    checiGUID = string.Empty,
                    strTrainJiaoluGUID = string.Empty,
                    strWorkShopGUID = string.Empty;
                groupGUID = group.Group.groupID;
                checiGUID = group.strCheciGUID;
                string strSql = @"insert into TAB_Nameplate_Group  
         (strGroupGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4)  
          values (@strGroupGUID,@strTrainmanGUID1,@strTrainmanGUID2,@strTrainmanGUID3,@strTrainmanGUID4)";
                SqlParameter[] sqlParameters = new SqlParameter[]
               {
                   new SqlParameter("strGroupGUID", groupGUID),
                   new SqlParameter("strTrainmanGUID1", group.Group.trainman1.trainmanID),
                   new SqlParameter("strTrainmanGUID2", group.Group.trainman2.trainmanID),
                   new SqlParameter("strTrainmanGUID3", group.Group.trainman3.trainmanID),
                   new SqlParameter("strTrainmanGUID4", group.Group.trainman4.trainmanID),
               };


                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                connection.ConnectionString = SqlHelper.ConnString;
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = strSql;
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParameters);
                int count = command.ExecuteNonQuery();
                if (count == 0)
                {
                    transaction.Rollback();
                    output.resultStr = "创建机组错误";
                    return output;
                }
                strSql =
                    @"select strTrainJiaoluGUID,strWorkShopGUID from  VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID";
                sqlParameters = new SqlParameter[] { new SqlParameter("strTrainmanJiaoluGUID", strTrainmanJiaoluGUID), };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, command.CommandText, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    strTrainJiaoluGUID = table.Rows[0]["strTrainJiaoluGUID"].ToString();
                    strWorkShopGUID = table.Rows[0]["strWorkShopGUID"].ToString();
                }
                UpdateTrainmanState(transaction, command, strTrainJiaoluGUID, strWorkShopGUID,
                    group.Group.trainman1.trainmanID);
                UpdateTrainmanState(transaction, command, strTrainJiaoluGUID, strWorkShopGUID,
                    group.Group.trainman2.trainmanID);
                UpdateTrainmanState(transaction, command, strTrainJiaoluGUID, strWorkShopGUID,
                    group.Group.trainman3.trainmanID);
                UpdateTrainmanState(transaction, command, strTrainJiaoluGUID, strWorkShopGUID,
                    group.Group.trainman4.trainmanID);

                strSql = @"insert into TAB_Nameplate_TrainmanJiaolu_Named 
        (strCheciGUID,strTrainmanJiaoluGUID,nCheciOrder,nCheciType,strCheci1,strCheci2,strGroupGUID,dtLastArriveTime) '
        (select @checiGUID,@strTrainmanJiaoluGUID, (case  when max(nCheCiOrder) is null then 1 else max(nCheCiOrder) + 1 end) ,
        @nCheciType,@strCheci1,@strCheci2,@groupGUID,getdate() from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID=strTrainmanJiaoluGUID)";
                sqlParameters = new SqlParameter[]
               {
                   new SqlParameter("checiGUID", checiGUID),
                   new SqlParameter("strTrainmanJiaoluGUID", strTrainmanJiaoluGUID),
                   new SqlParameter("nCheciType", group.nCheciType),
                   new SqlParameter("strCheci1", group.strCheci1),
                   new SqlParameter("strCheci2", group.strCheci2),
                   new SqlParameter("groupGUID", groupGUID),
               };
                command.CommandText = strSql;
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParameters);
                if (command.ExecuteNonQuery() == 0)
                {
                    transaction.Rollback();
                    throw new Exception("创建记名式机组错误");
                }
                AddTrainmanJiaoLuToTrainman(transaction, command, group.Group.trainman1.trainmanID, strTrainmanJiaoluGUID);
                AddTrainmanJiaoLuToTrainman(transaction, command, group.Group.trainman2.trainmanID, strTrainmanJiaoluGUID);
                AddTrainmanJiaoLuToTrainman(transaction, command, group.Group.trainman3.trainmanID, strTrainmanJiaoluGUID);
                AddTrainmanJiaoLuToTrainman(transaction, command, group.Group.trainman4.trainmanID, strTrainmanJiaoluGUID);
                transaction.Commit();
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            finally
            {
                connection.Dispose();
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
            return output;
        }

        /// <summary>
        ///  //修改人员状态为正常状态
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="command"></param>
        /// <param name="strTrainJiaoluGUID"></param>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        private void UpdateTrainmanState(SqlTransaction transaction, SqlCommand command, string strTrainJiaoluGUID, string strWorkShopGUID, string strTrainmanGUID)
        {
            if (string.IsNullOrEmpty(strTrainmanGUID))
                return;
            string strSql = @"update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,
        nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
           {
               new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID), 
               new SqlParameter("strWorkShopGUID",strWorkShopGUID),
               new SqlParameter("nTrainmanState",TF.RunSafty.NamePlate.MD.TRsTrainmanState.tsNormal),
               new SqlParameter("strTrainmanGUID",strTrainmanGUID), 
           };
            command.CommandText = strSql;
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
            int count = command.ExecuteNonQuery();
            if (count == 0)
            {
                transaction.Rollback();
                throw new Exception("修改人员状态错误");
            }
        }
        #endregion

        #region 删除记名式机组

        private void UpdateTrainmanState(SqlTransaction transaction, SqlCommand command, string strTrainmanGUID)
        {
            if (string.IsNullOrEmpty(strTrainmanGUID))
                return;
            string strSql = @"update Tab_Org_Trainman set  nTrainmanState = @nTrainmanState,
                dtBecomeReady = getdate() where strTrainmanGUID = @strTrainmanGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
           { 
               new SqlParameter("nTrainmanState",TF.RunSafty.NamePlate.MD.TRsTrainmanState.tsReady),
               new SqlParameter("strTrainmanGUID",strTrainmanGUID), 
           };
            command.CommandText = strSql;
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
            int count = command.ExecuteNonQuery();
            if (count == 0)
            {
                transaction.Rollback();
                throw new Exception("修改人员状态错误");
            }
        }

        public bool Delete(string strGroupGUID)
        {
            SqlTransaction transaction = null;
            SqlConnection connection = new SqlConnection();
            string strTrainmanGUID1, strTrainmanGUID2, strTrainmanGUID3, strTrainmanGUID4;
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                connection.ConnectionString = SqlHelper.ConnString;
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                string strSql = "select strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4 from TAB_Nameplate_Group where strGroupGUID=@strGroupGUID";
                SqlParameter[] sqlParameters = new SqlParameter[]
               {
                   new SqlParameter("strGroupGUID",strGroupGUID), 
               };
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    strTrainmanGUID1 = row["strTrainmanGUID1"].ToString();
                    strTrainmanGUID2 = row["strTrainmanGUID2"].ToString();
                    strTrainmanGUID3 = row["strTrainmanGUID3"].ToString();
                    strTrainmanGUID4 = row["strTrainmanGUID4"].ToString();
                    UpdateTrainmanState(transaction, command, strTrainmanGUID1);
                    UpdateTrainmanState(transaction, command, strTrainmanGUID2);
                    UpdateTrainmanState(transaction, command, strTrainmanGUID3);
                    UpdateTrainmanState(transaction, command, strTrainmanGUID4);
                    strSql = "delete from TAB_Nameplate_Group where strGroupGUID=@strGroupGUID";
                    sqlParameters = new SqlParameter[]
                   {
                       new SqlParameter("strGroupGUID",strGroupGUID), 
                   };
                    command.CommandText = strSql;
                    command.Parameters.Clear();
                    command.Parameters.AddRange(sqlParameters);
                    command.ExecuteNonQuery();
                    strSql = "delete from TAB_Nameplate_TrainmanJiaolu_Named where strGroupGUID=@strGroupGUID";
                    command.CommandText = strSql;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                transaction.Rollback();
                throw ex;
            }
            return false;
        }
        /// <summary>
        /// 删除记名式机组
        /// <summary>
        public InterfaceOutPut DeleteNamedGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                string GroupGUID = ObjectConvertClass.static_ext_string(cjm.GetValue("GroupGUID"));
                if (this.Delete(GroupGUID))
                {
                    output.result = 0;
                    output.resultStr = "删除记名式机组成功";
                }
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return output;
        }

        #endregion

        #region 删除一个交路的机组记名式
        private class Delete_InData 
        {
            public string TrainmanjiaoluID;
        }

        public class Delete_OutData
        {
            public string result = "";
            public string resultStr = "";
            //public object data;
        }

        public Delete_OutData DeleteNamedGroupByJiaoLu(string data)
        {
            Delete_OutData json = new Delete_OutData();
            Delete_InData input = Newtonsoft.Json.JsonConvert.DeserializeObject<Delete_InData>(data);
            try
            {
                if (DBNameBoard.DeleteNamedGroupByJiaoLu(input.TrainmanjiaoluID))
                {
                    json.result = "0";
                    json.resultStr = "返回成功";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 修改车次信息

        public class UpdateInfo_In
        {
            public RRsNamedGroup namedGroup;
        }
        /// <summary>
        /// 修改车次信息
        /// <summary>
        public InterfaceOutPut UpdateCheciInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                UpdateInfo_In model = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateInfo_In>(Data);
                string strSql = @"update TAB_Nameplate_TrainmanJiaolu_Named set strCheci1=@strCheci1,
                strCheci2=@strCheci2,nCheciType =@nCheciType where strCheciGUID =@strCheciGUID";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strCheci1",model.namedGroup.strCheci1), 
                    new SqlParameter("strCheci1",model.namedGroup.strCheci2), 
                    new SqlParameter("nCheciType",model.namedGroup.nCheciType), 
                    new SqlParameter("strCheciGUID",model.namedGroup.strCheciGUID), 
                };
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return output;
        }
        #endregion

        #region 记名式机组添加乘务员

        public class AddTrainman_In
        {
            public int nTrainmanIndex;
            public string strNewGroupGUID;
            public string strOldGroupGUID;
            public string strTrainmanGUID;
            public string strTrainmanJiaoluGUID;
        }

        /// <summary>
        /// 记名式机组添加乘务员
        /// <summary>
        public InterfaceOutPut AddTrainmanToNamedGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            AddTrainman_In model = null;
            TF.RunSafty.NamePlate.MD.RRsGroup GroupOld = null;
            string strGroupGUID = string.Empty;
            output.result = 1;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<AddTrainman_In>(Data);

                //删除旧的机组里面的人员
                GroupOld = DBNameBoard.GetTrainmanGroup(model.strTrainmanGUID);
                DBNameBoard.DeleteTrainman(model.strTrainmanGUID);
                strGroupGUID = GroupOld.strGroupGUID;
                if (DBNameBoard.GetGroupInfo(strGroupGUID, GroupOld))
                {
                    //检查老机组是否为空，如果为空咋删除老机组
                    if (GroupOld.Trainman1.strTrainmanGUID == "" && GroupOld.Trainman2.strTrainmanGUID == ""
                        &&
                        GroupOld.Trainman3.strTrainmanGUID == ""
                        &&
                        GroupOld.Trainman4.strTrainmanGUID == "")
                    {
                        DeleteNamedGroup(GroupOld.strGroupGUID);
                    }

                }

                //添加人员到机组
                DBNameBoard.AddTrainman(model.strNewGroupGUID, model.nTrainmanIndex, model.strTrainmanGUID);
                //添加人员的交路信息
                DBNameBoard.AddTrainmanJiaoLuToTrainman(model.strTrainmanGUID, model.strTrainmanJiaoluGUID);

                DBNameBoard.AddTrainman(model.strOldGroupGUID, model.nTrainmanIndex, model.strTrainmanGUID);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddTrainmanToNamedGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion
    }

}
      