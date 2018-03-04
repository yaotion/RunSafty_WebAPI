using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using TF.RunSafty.Plan.MD;
using TF.CommonUtility;
using ThinkFreely.DBUtility;


namespace TF.RunSafty.Plan
{
    public class LCPaiBan
    {
        #region 交换计划的机组
        public class InExchangeGroup
        {
            //待交换计划GUID
            public string SourcePlanGUID;
            //目标计划GUID
            public string DestPlanGUID;
            //值班员信息
            public DutyUser DutyUser = new DutyUser();     
        }
        /// <summary>
        /// 交换计划的机组
        /// </summary>
        public InterfaceOutPut ExchangeGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InExchangeGroup InParams = javaScriptSerializer.Deserialize<InExchangeGroup>(Data);
                //判断源计划的状态是否小于已出勤,且已安排人员
                string strSql = "select nPlanState,strGroupGUID from VIEW_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParamsSourcePlan = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.SourcePlanGUID)
                };
                DataTable dtSourPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSourcePlan).Tables[0];
                if (dtSourPlan.Rows.Count == 0)
                {
                    output.resultStr = "源计划不存在,不能交换!";
                    return output;
                }
                if (int.Parse(dtSourPlan.Rows[0]["nPlanState"].ToString()) >= 7)
                {
                    output.resultStr = "源计划已出勤,不能交换!";
                    return output;
                }
                if (dtSourPlan.Rows[0]["strGroupGUID"].ToString()== "")
                {
                    output.resultStr = "源计划未安排机组,不能交换!";
                    return output;
                }

                //判断源计划的状态是否小于已出勤,且已安排人员
                strSql = "select nPlanState,strGroupGUID from VIEW_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParamsDestPlan = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.DestPlanGUID)
                };
                DataTable dtDestPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDestPlan).Tables[0];
                if (dtDestPlan.Rows.Count == 0)
                {
                    output.resultStr = "目标计划不存在,不能交换!";
                    return output;
                }
                if (int.Parse(dtDestPlan.Rows[0]["nPlanState"].ToString()) >= 7)
                {
                    output.resultStr = "目标计划已出勤,不能交换!";
                    return output;
                }
                if (dtDestPlan.Rows[0]["strGroupGUID"].ToString() == "")
                {
                    output.resultStr = "目标计划未安排机组,不能交换!";
                    return output;
                }

                //删除人员计划
                strSql = "delete from tab_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParamsSourcePlan);

                strSql = "delete from tab_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDestPlan);


                //添加人员计划
                strSql = @"insert into TAB_Plan_Trainman (strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,strDutyGUID,strDutySiteGUID)
                    ( select top 1 @strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,@strDutyGUID,@strDutySiteGUID from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID)";
                SqlParameter[] sqlParamsSourceGroup = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.SourcePlanGUID),
                    new SqlParameter("strGroupGUID",dtDestPlan.Rows[0]["strGroupGUID"].ToString()),
                    new SqlParameter("strDutyGUID",InParams.DutyUser.userID),
                    new SqlParameter("strDutySiteGUID",""),
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSourceGroup);

                //添加人员计划
                strSql = @"insert into TAB_Plan_Trainman (strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,strDutyGUID,strDutySiteGUID)
                    ( select top 1 @strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,@strDutyGUID,@strDutySiteGUID from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID)";
                SqlParameter[] sqlParamsDestGroup = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.DestPlanGUID),
                    new SqlParameter("strGroupGUID",dtSourPlan.Rows[0]["strGroupGUID"].ToString()),
                    new SqlParameter("strDutyGUID",InParams.DutyUser.userID),
                    new SqlParameter("strDutySiteGUID",""),
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDestGroup);
                //修改计划的值乘方式

               strSql = @"update TAB_Plan_Train set 
                     nTrainmanTypeID = (select top 1 nTrainmanTypeID from VIEW_Nameplate_Group where strGroupGUID = @strGroupGUID) where strTrainPlanGUID = @strTrainPlanGUID";

               SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSourceGroup);

               strSql = @"update TAB_Plan_Train set 
                     nTrainmanTypeID = (select top 1 nTrainmanTypeID from VIEW_Nameplate_Group where strGroupGUID = @strGroupGUID) where strTrainPlanGUID = @strTrainPlanGUID";

               SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDestGroup);


                //设置机组值乘的计划
               strSql = "update TAB_Nameplate_Group set strTrainPlanGUID = @strTrainPlanGUID where strGroupGUID = @strGroupGUID";
               SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSourceGroup);

               strSql = "update TAB_Nameplate_Group set strTrainPlanGUID = @strTrainPlanGUID where strGroupGUID = @strGroupGUID";
               SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDestGroup);

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ExchangeGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 向计划安排机组
        public class InSetGroup
        {
            //计划GUID
            public string PlanGUID;
            //机组GUID
            public string GroupGUID;
            public DutyUser DutyUser = new DutyUser();
        }

        /// <summary>
        /// 为计划设置机组
        /// </summary>
        public InterfaceOutPut SetGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InSetGroup InParams = javaScriptSerializer.Deserialize<InSetGroup>(Data);

                //目标计划已出勤或者已经安排机组则不能安排计划
                string strSql = "select nPlanState,strGroupGUID from VIEW_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParamsPlan = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.PlanGUID)
                };
                DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsPlan).Tables[0];
                if (dtPlan.Rows.Count == 0)
                {
                    output.resultStr = "源计划不存在,不能安排人员!";
                    return output;
                }
                //只有已下发或者已发布的计划才能安排机组
                if ((int.Parse(dtPlan.Rows[0]["nPlanState"].ToString()) != 3) && (int.Parse(dtPlan.Rows[0]["nPlanState"].ToString()) != 4))
                {
                    output.resultStr = "只有已下发或者已发布的计划才能安排机组!";
                    return output;
                }
                if (dtPlan.Rows[0]["strGroupGUID"].ToString() != "")
                {
                    output.resultStr = "源计划已安排机组,不能重复安排!";
                    return output;
                }
                //源机组如果已安排计划或者已出勤则不能安排
                strSql = "select strTrainPlanGUID from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID";
                SqlParameter[] sqlParamsGroup = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",InParams.GroupGUID)
                };
                DataTable dtGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParamsGroup).Tables[0];
                if (dtGroup.Rows.Count == 0)
                {
                    output.resultStr = "指定的机组不存在!";
                    return output;
                }
                if (dtGroup.Rows[0]["strTrainPlanGUID"].ToString() != "")
                {
                    output.resultStr = "指定的机组已经在值乘别的计划,不能重复安排!";
                    return output;
                }
                //根据设置判断是否卡控机组内所有人员的寓休不足,后续需加上
                               

                //设置计划的值乘方式为机组所在人员交路的值乘方式
                strSql  = @"update TAB_Plan_Train set 
                    nTrainmanTypeID = (select top 1 nTrainmanTypeID from VIEW_Nameplate_Group where strGroupGUID =@strGroupGUID),
            nKehuoID = (select top 1 nKeHuoID from VIEW_Nameplate_Group where strGroupGUID =@strGroupGUID) where strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strGroupGUID",InParams.GroupGUID),
                    new SqlParameter("strTrainPlanGUID",InParams.PlanGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
               

                //添加人员计划
                strSql = @"insert into TAB_Plan_Trainman (strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,strDutyGUID,strDutySiteGUID)
                    ( select top 1 @strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,@strDutyGUID,@strDutySiteGUID from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID)";
                SqlParameter[] sqlParamsFull = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.PlanGUID),
                    new SqlParameter("strGroupGUID",InParams.GroupGUID),
                    new SqlParameter("strDutyGUID",InParams.DutyUser.userID),
                    new SqlParameter("strDutySiteGUID",""),
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsFull);
              
                //设置机组值乘的计划
                strSql = "update TAB_Nameplate_Group set strTrainPlanGUID = @strTrainPlanGUID where strGroupGUID = @strGroupGUID";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.SetGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        
        #region 向计划的指定位置安排人员\如计划未安排人员则安排整个机组
        public class InSetTrainmanToPlan
        {
            //计划GUID
            public string TrainPlanGUID;
            //人员工号
            public string TrainmanNumber;
            //位置(1,2,3,4)
            public int TrainmanIndex;
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
        }

        /// <summary>
        /// 将人员安排到计划的指定位置中
        /// </summary>
        public InterfaceOutPut SetTrainmanToPlan(String Data)
        {

            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InSetTrainmanToPlan InParams = javaScriptSerializer.Deserialize<InSetTrainmanToPlan>(Data);

                TF.RunSafty.NamePlate.MD.DutyUser namePlateDutyuser = new NamePlate.MD.DutyUser();
                        namePlateDutyuser.strDutyGUID = InParams.DutyUser.userGUID;
                        namePlateDutyuser.strDutyNumber = InParams.DutyUser.userID;
                        namePlateDutyuser.strDutyName = InParams.DutyUser.userName;

                #region 获取计划信息
                //获取计划信息
                string strSql = "select nPlanState,strGroupGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strTrainJiaoluName from VIEW_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParamsPlan = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID)
                };
                DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsPlan).Tables[0];

                //源计划不存在,不能安排人员
                if (dtPlan.Rows.Count == 0)
                {
                    output.resultStr = "源计划不存在,不能安排人员!";
                    return output;
                }
                //只有已下发或者已发布的计划才能安排机组
                if ((int.Parse(dtPlan.Rows[0]["nPlanState"].ToString()) != 3) && (int.Parse(dtPlan.Rows[0]["nPlanState"].ToString()) != 4))
                {
                    output.resultStr = "只有已下发或者已发布的计划才能安排机组!";
                    return output;
                }
                //if (dtPlan.Rows[0]["strGroupGUID"].ToString() != "")
                //{
                //    output.resultStr = "计划中已经安排机组，请将源机组移除后再添加!";
                //    return output;
                //}


                #endregion         

                #region 获取源人员
                strSql = "select strTrainmanGUID,nTrainmanState from tab_org_trainman where strTrainmanNumber = @strTrainmanNumber";
                SqlParameter[] sqlParamsTrainman = new SqlParameter[] { 
                    new SqlParameter("strTrainmanNumber",InParams.TrainmanNumber)
                };
                DataTable dtTrainman = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrainman).Tables[0];
                if (dtTrainman.Rows.Count == 0)
                {
                    output.resultStr = "指定的人员不存在!";
                    return output;
                }
                string strSourceTrainmanGUID = dtTrainman.Rows[0]["strTrainmanGUID"].ToString();
                if (dtTrainman.Rows[0]["nTrainmanState"] != DBNull.Value)
                {
                    if (Convert.ToInt32(dtTrainman.Rows[0]["nTrainmanState"]) == 0)
                    {
                        output.resultStr = "指定的人员已请假!";
                        return output;
                    }
                }
                
                #endregion

                #region 获取源机组
                //获取目标人员所在机组信息
                strSql = @"select strGroupGUID,strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,nTXState from TAB_Nameplate_Group where strTrainmanGUID1= @strTrainmanGUID 
                    or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID  or strTrainmanGUID4= @strTrainmanGUID";
                SqlParameter[] sqlParamsGroup = new SqlParameter[] { 
                    new SqlParameter("strTrainmanGUID",strSourceTrainmanGUID)
                };
                DataTable dtGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsGroup).Tables[0];

                //判断目标人员所在机组是否已经安排计划或出勤，如是则不能安排
                if (dtGroup.Rows.Count > 0)
                {
                    if (dtGroup.Rows[0]["strTrainPlanGUID"].ToString() != "")
                    {
                        output.resultStr = "人员所在机组已经在值乘别的计划!";
                        return output;
                    }
                    string nTXState= ObjectConvertClass.static_ext_string(dtGroup.Rows[0]["nTXState"]);
                    if (nTXState == "1")
                    {
                        output.resultStr = "人员所在机组已经处于调休状态，不能派班!";
                        return output;
                    }
                }
                #endregion

                #region 获取目标人员
                string strDestTrainman = "";
                //将计划的指定位置的人员的状态置为预备
                if (InParams.TrainmanIndex == 1)
                {
                    strDestTrainman = dtPlan.Rows[0]["strTrainmanGUID1"].ToString();
                }
                if (InParams.TrainmanIndex == 2)
                {
                    strDestTrainman = dtPlan.Rows[0]["strTrainmanGUID2"].ToString();
                }
                if (InParams.TrainmanIndex == 3)
                {
                    strDestTrainman = dtPlan.Rows[0]["strTrainmanGUID3"].ToString();
                }
                if (InParams.TrainmanIndex == 4)
                {
                    strDestTrainman = dtPlan.Rows[0]["strTrainmanGUID4"].ToString();
                }                
                #endregion

                #region 获取机组
                string strSourceGroupGUID;

                if (dtGroup.Rows.Count > 0)
                {
                    strSourceGroupGUID = dtGroup.Rows[0]["strGroupGUID"].ToString();
                }
                else
                {
                    strSourceGroupGUID = string.Empty;
                }
                string strDestGroupGUID = dtPlan.Rows[0]["strGroupGUID"].ToString();
                #endregion
                
                #region 状态判断
                //如果计划没有安排机组且指定的人员已经安排机组则不能安排，必须先把人安排到名牌中
                if ((string.IsNullOrEmpty(strDestGroupGUID)) && (dtGroup.Rows.Count == 0))
                {
                    output.resultStr = "必须先把人员安排到名牌中才能继续派班!";
                    return output;
                }

                //计划安排机组且人员也已经在机组中则必须先把计划中的机组清除后才能继续执行操作
                //if ((dtPlan.Rows[0]["strGroupGUID"].ToString() != "") && (dtGroup.Rows.Count > 0))
                //{
                //    output.resultStr = "计划和指定的人员都有各自所在的机组，必须先清除掉计划中的机组信息!";
                //    return output;
                //}
                #endregion                

                #region 计划中没有机组,且源人员为机组
                //计划未安排机组且人员已有机组则将人员机组安排到计划中
                if ((dtPlan.Rows[0]["strGroupGUID"].ToString() == "") && (dtGroup.Rows.Count > 0))
                {
                    #region 设置计划的值乘方式为机组所在人员交路的值乘方式
                    //设置计划的值乘方式为机组所在人员交路的值乘方式
                    strSql = @"update TAB_Plan_Train set 
                    nTrainmanTypeID = (select top 1 nTrainmanTypeID from VIEW_Nameplate_Group where strGroupGUID =@strGroupGUID) where strTrainPlanGUID = @strTrainPlanGUID";
                    SqlParameter[] sqlParams = new SqlParameter[] { 
                        new SqlParameter("strGroupGUID",dtGroup.Rows[0]["strGroupGUID"].ToString()),
                        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID)
                    };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                    #endregion

                    #region 添加人员计划
                    //添加人员计划
                    strSql = @"insert into TAB_Plan_Trainman (strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,strDutyGUID,strDutySiteGUID)
                    ( select top 1 @strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,@strDutyGUID,@strDutySiteGUID from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID)";
                    SqlParameter[] sqlParamsFull = new SqlParameter[] { 
                       new SqlParameter("strGroupGUID",dtGroup.Rows[0]["strGroupGUID"].ToString()),
                        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                        new SqlParameter("strDutyGUID",InParams.DutyUser.userID),
                        new SqlParameter("strDutySiteGUID",""),
                    };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsFull);
                    #endregion

                    #region 设置机组值乘的计划
                    //设置机组值乘的计划
                    strSql = "update TAB_Nameplate_Group set strTrainPlanGUID = @strTrainPlanGUID where strGroupGUID = @strGroupGUID";
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);                    
                    #endregion

                    output.result = 0;
                    return output;
                }
                #endregion

                #region 计划中已有机组
                //string strNameplateLog = "";
                //计划安排机组且人员未在机组中则将人员安排到机组中且将人员与现有的人员替换
                if ((!string.IsNullOrEmpty(strDestGroupGUID)))
                {
                    #region 源人员有机组，则从机组中移除人员
                    if (!string.IsNullOrEmpty(strSourceTrainmanGUID) && !string.IsNullOrEmpty(strSourceGroupGUID))
                    {
                        int srcTrainmanIndex = 0;
                        if (dtGroup.Rows[0]["strTrainmanGUID1"].ToString() == strSourceTrainmanGUID)
                        {
                            srcTrainmanIndex = 1;
                        }
                        else
                        if (dtGroup.Rows[0]["strTrainmanGUID2"].ToString() == strSourceTrainmanGUID)
                        {
                            srcTrainmanIndex = 2;
                        }
                        else
                        if (dtGroup.Rows[0]["strTrainmanGUID3"].ToString() == strSourceTrainmanGUID)
                        {
                            srcTrainmanIndex = 3;
                        }
                        else
                        if (dtGroup.Rows[0]["strTrainmanGUID4"].ToString() == strSourceTrainmanGUID)
                        {
                            srcTrainmanIndex = 4;
                        }
                        if (srcTrainmanIndex == 0)
                        {
                            throw new Exception("源机组中没有找到乘务员序号");
                        }

                        SqlParameter[] sqlParamsDestTM = new SqlParameter[] {                             
                            new SqlParameter("GroupGUID",strSourceGroupGUID)
                        };
                        

                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID{0} = '' where strGroupGUID = @GroupGUID";
                        strSql = string.Format(strSql, srcTrainmanIndex);
                        
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDestTM);

                        if (!string.IsNullOrEmpty(strSourceGroupGUID))
                        {
                            string strContent = "";
                            strContent = TF.RunSafty.NamePlate.LCGroup.GetTrainmanStringByID(strSourceTrainmanGUID);

                            strContent = strContent + "(工号派班)从机组中移除";


                            TF.RunSafty.NamePlate.LCGroup.SaveNameplateLogWithGUID(strSourceGroupGUID,
                                namePlateDutyuser,
                                TF.RunSafty.NamePlate.MD.LBoardChangeType.btcDeleteTrainman,
                                strContent);
                        }

                        
                        
                        
                    }
                    
                    #endregion

                    #region 目标机组中移除人员
                    if (!string.IsNullOrEmpty(strDestTrainman))
                    {
                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID{0} = '' where strGroupGUID = @GroupGUID";
                        strSql = string.Format(strSql, InParams.TrainmanIndex);
                        SqlParameter[] sqlParamsDestTM = new SqlParameter[] {                             
                            new SqlParameter("GroupGUID",strDestGroupGUID)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDestTM);

                        string strContent = "";
                        strContent = TF.RunSafty.NamePlate.LCGroup.GetTrainmanStringByID(strDestTrainman);

                        strContent = strContent + "(工号派班)从机组中移除";


                        TF.RunSafty.NamePlate.LCGroup.SaveNameplateLogWithGUID(strDestGroupGUID,
                            namePlateDutyuser,
                            TF.RunSafty.NamePlate.MD.LBoardChangeType.btcDeleteTrainman,
                            strContent);
                    }
                    #endregion

                   
                    #region 目标人员状态置为预备
                    if (strDestTrainman != "")
                    {
                        strSql = "update tab_org_trainman set nTrainmanState = 1,dtBecomeReady = getdate() where strTrainmanGUID = @DestTrainmanGUID";
                        SqlParameter[] sqlParamsDestTM = new SqlParameter[] { 
                            new SqlParameter("DestTrainmanGUID",strDestTrainman)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDestTM);
                    }
                    #endregion

                    #region 替换机组及计划中指定位置的人员信息

                    strSql = "update tab_plan_trainman set strTrainmanGUID{0} = @SourceTrainmanGUID where strTrainPlanGUID = @strTrainPlanGUID";
                    strSql = string.Format(strSql, InParams.TrainmanIndex);
                    SqlParameter[] sqlParamsUpdateTM = new SqlParameter[] { 
                            new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                            new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                        };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM);


                    strSql = "update TAB_Nameplate_Group set strTrainmanGUID{0} = @SourceTrainmanGUID where strGroupGUID = @strGroupGUID";
                    strSql = string.Format(strSql, InParams.TrainmanIndex);
                    SqlParameter[] sqlParamsUpdateTM2 = new SqlParameter[] { 
                            new SqlParameter("strGroupGUID",dtPlan.Rows[0]["strGroupGUID"].ToString()),
                            new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                        };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM2);

                    string strlogContent = "";
                    strlogContent = TF.RunSafty.NamePlate.LCGroup.GetTrainmanStringByID(strSourceTrainmanGUID);

                    strlogContent = strlogContent + "(工号派班)添加到机组" + TF.RunSafty.NamePlate.LCGroup.GetGroupStringByID((dtPlan.Rows[0]["strGroupGUID"].ToString()));


                    TF.RunSafty.NamePlate.LCGroup.SaveNameplateLogWithGUID(dtPlan.Rows[0]["strGroupGUID"].ToString(),
                        namePlateDutyuser,
                        TF.RunSafty.NamePlate.MD.LBoardChangeType.btcAddTrainman,
                        strlogContent);
                    #region 旧代码
                    ////替换机组及计划中指定位置的人员信息
                    //if (InParams.TrainmanIndex == 1)
                    //{
                    //    strSql = "update tab_plan_trainman set strTrainmanGUID1 = @SourceTrainmanGUID where strTrainPlanGUID = @strTrainPlanGUID";
                    //    SqlParameter[] sqlParamsUpdateTM = new SqlParameter[] { 
                    //        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                    //        new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                    //    };
                    //    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM);


                    //    strSql = "update TAB_Nameplate_Group set strTrainmanGUID1 = @SourceTrainmanGUID where strGroupGUID = @strGroupGUID";
                    //    SqlParameter[] sqlParamsUpdateTM2 = new SqlParameter[] { 
                    //        new SqlParameter("strGroupGUID",dtPlan.Rows[0]["strGroupGUID"].ToString()),
                    //        new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                    //    };
                    //    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM2);

                    //}
                    //if (InParams.TrainmanIndex == 2)
                    //{
                    //    strSql = "update tab_plan_trainman set strTrainmanGUID2 = @SourceTrainmanGUID where strTrainPlanGUID = @strTrainPlanGUID";
                    //    SqlParameter[] sqlParamsUpdateTM = new SqlParameter[] { 
                    //        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                    //        new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                    //    };
                    //    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM);


                    //    strSql = "update TAB_Nameplate_Group set strTrainmanGUID2 = @SourceTrainmanGUID where strGroupGUID = @strGroupGUID";
                    //    SqlParameter[] sqlParamsUpdateTM2 = new SqlParameter[] { 
                    //        new SqlParameter("strGroupGUID",dtPlan.Rows[0]["strGroupGUID"].ToString()),
                    //        new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                    //    };
                    //    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM2);
                    //}
                    //if (InParams.TrainmanIndex == 3)
                    //{
                    //    strSql = "update tab_plan_trainman set strTrainmanGUID3 = @SourceTrainmanGUID where strTrainPlanGUID = @strTrainPlanGUID";
                    //    SqlParameter[] sqlParamsUpdateTM = new SqlParameter[] { 
                    //        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                    //        new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                    //    };
                    //    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM);


                    //    strSql = "update TAB_Nameplate_Group set strTrainmanGUID3 = @SourceTrainmanGUID where strGroupGUID = @strGroupGUID";
                    //    SqlParameter[] sqlParamsUpdateTM2 = new SqlParameter[] { 
                    //        new SqlParameter("strGroupGUID",dtPlan.Rows[0]["strGroupGUID"].ToString()),
                    //        new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                    //    };
                    //    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM2);
                    //}
                    //if (InParams.TrainmanIndex == 4)
                    //{
                    //    strSql = "update tab_plan_trainman set strTrainmanGUID4 = @SourceTrainmanGUID where strTrainPlanGUID = @strTrainPlanGUID";
                    //    SqlParameter[] sqlParamsUpdateTM = new SqlParameter[] { 
                    //        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                    //        new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                    //    };
                    //    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM);


                    //    strSql = "update TAB_Nameplate_Group set strTrainmanGUID4 = @SourceTrainmanGUID where strGroupGUID = @strGroupGUID";
                    //    SqlParameter[] sqlParamsUpdateTM2 = new SqlParameter[] { 
                    //        new SqlParameter("strGroupGUID",dtPlan.Rows[0]["strGroupGUID"].ToString()),
                    //        new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID)
                    //    };
                    //    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateTM2);
                    //}
                    #endregion
                    
                    #endregion

                    #region 将新人员的状态修改为正常

                    SqlParameter[] sqlParamsUpdateState = new SqlParameter[] { 
                            new SqlParameter("SourceTrainmanGUID",strSourceTrainmanGUID),
                            new SqlParameter("GroupGUID",strDestGroupGUID)
                        };
                    //获取目标机组所在交路
                    strSql = "select * from VIEW_Nameplate_Group_TrainmanJiaolu where strGroupGUID = @GroupGUID";
                    DataTable dtDestGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateState).Tables[0];
                    string strTrainmanJiaoluGUID = "";

                    if (dtDestGroup.Rows.Count > 0)
                    {
                        strTrainmanJiaoluGUID = dtDestGroup.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                    }

                    //将新人员的状态修改为正常
                    strSql = "update tab_org_trainman set nTrainmanState = 2,strTrainmanJiaoluGUID = @JiaoluGUID where strTrainmanGUID = @SourceTrainmanGUID";

                    sqlParamsUpdateState[1] = new SqlParameter("JiaoluGUID", strTrainmanJiaoluGUID);

                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdateState);
                    #endregion
                    

                    output.result = 0;
                    return output;
                }

                #endregion
                //如没有安排机组则执行派班操作
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.SetTrainmanToPlan:" + ex.Message);
                throw ex;
            }
            return output;
        }       
        #endregion

        #region 自动派班,目前仅支持记名式交路
        public class InAutoDispatch
        {
            //所属行车区段GUID
            public string TrainJiaoluGUID;
            //需要自动派班的行车计划GUID列表，以逗号隔开
            public string PlanGUIDs;
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
        }

        /// <summary>
        /// 自动派班
        /// </summary>
        public InterfaceOutPut AutoDispatch(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAutoDispatch InParams = javaScriptSerializer.Deserialize<InAutoDispatch>(Data);
               
                //循环计划进行派班
                string[] plans = InParams.PlanGUIDs.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < plans.Length; i++)
                {
                    string planGUID = plans[i];
                    //查询指定的计划是否为已接收或者已发布且未派班的状态
                    string strSql = "select top 1 strTrainNo from VIEW_plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID  and nPlanState in (3,4) and (strGroupGUID = '' or  strGroupGUID is null)";
                    SqlParameter[] sqlParamsPlan = new SqlParameter[] { 
                        new SqlParameter("strTrainPlanGUID",planGUID)
                    };
                    DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsPlan).Tables[0];
                    if (dtPlan.Rows.Count == 0)
                        continue;
                    //获取可已派班的机组信息（车次1和指定车次相同且未安排计划且在指定的有关系的交路里的记名式机组）
                    strSql = @"  select top 1 * from VIEW_Nameplate_TrainmanJiaolu_Named where 
                         (strTrainmanJiaoluGUID in (select strTrainmanjiaoluGUID from VIEW_Base_JiaoluRelation where strTrainJiaoluGUID = @strTrainJiaoluGUID)) and                          
                         (strCheCi1 = @strTrainNo) and
                         (select count(*) from dbo.VIEW_Nameplate_Group 
                                   where strGroupGUID = VIEW_Nameplate_TrainmanJiaolu_Named.strGroupGUID and  ( strTrainPlanGUID = '' or strTrainPlanGUID is null ) )> 0 ";
                    SqlParameter[] sqlParamsGroup = new SqlParameter[]{
                        new SqlParameter("strTrainJiaoluGUID",InParams.TrainJiaoluGUID),
                        new SqlParameter("strTrainNo",dtPlan.Rows[0]["strTrainNo"].ToString())
                    };
                    DataTable dtGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsGroup).Tables[0];
                    //没找到匹配的机组信息
                    if (dtGroup.Rows.Count == 0)
                        continue;
                    //派班
                    string strGroupGUID = dtGroup.Rows[0]["strGroupGUID"].ToString();
                    //设置计划的值乘方式为机组所在人员交路的值乘方式
                    strSql = @"update TAB_Plan_Train set 
                    nTrainmanTypeID = (select top 1 nTrainmanTypeID from VIEW_Nameplate_Group where strGroupGUID =@strGroupGUID) where strTrainPlanGUID = @strTrainPlanGUID";
                    SqlParameter[] sqlParams = new SqlParameter[] { 
                        new SqlParameter("strGroupGUID",strGroupGUID),
                        new SqlParameter("strTrainPlanGUID",planGUID)
                    };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);


                    //添加人员计划
                    strSql = @"insert into TAB_Plan_Trainman (strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,strDutyGUID,strDutySiteGUID)
                    ( select top 1 @strTrainPlanGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4,strGroupGUID,@strDutyGUID,@strDutySiteGUID from TAB_Nameplate_Group where strGroupGUID = @strGroupGUID)";
                    SqlParameter[] sqlParamsFull = new SqlParameter[] { 
                       new SqlParameter("strGroupGUID",strGroupGUID),
                        new SqlParameter("strTrainPlanGUID",planGUID),
                        new SqlParameter("strDutyGUID",InParams.DutyUser.userID),
                        new SqlParameter("strDutySiteGUID",""),
                    };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsFull);

                    //设置机组值乘的计划
                    strSql = "update TAB_Nameplate_Group set strTrainPlanGUID = @strTrainPlanGUID where strGroupGUID = @strGroupGUID";
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AutoDispatch:" + ex.Message);
                throw ex;
            }
            return output;
        }          


        #endregion

        #region 发布计划

        public class InPublishPlan
        {
            //待发布的计划的GUID列表,以逗号隔开
            public string PlanGUIDs;
            //值班员
            public string DutyGUID;
            //所属客户端信息
            public string SiteGUID;
        }

        /// <summary>
        /// 发布计划
        /// </summary>
        public InterfaceOutPut PublishPlan(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InPublishPlan InParams = javaScriptSerializer.Deserialize<InPublishPlan>(Data);
                string[] plans = InParams.PlanGUIDs.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < plans.Length; i++)
                {
                    string strSql = @"update TAB_Plan_Train set nPlanState = 4 where strTrainPlanGUID = @strTrainPlanGUID and nPlanState = 3
                    and (select count(*) from tab_plan_trainman where strTrainPlanGUID =TAB_Plan_Train.strTrainPlanGUID and strGroupGUID <> '') > 0 ";
                    SqlParameter[] sqlParamsUpdate = new SqlParameter[]{
                        new SqlParameter("strTrainPlanGUID",plans[i])
                    };
                    int updateCount = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUpdate);

                    if (updateCount > 0)
                    {
                        strSql = "PROC_Plan_AddPublishRecord";
                        SqlParameter[] sqlParamsPB = new SqlParameter[]{
                        new SqlParameter("strTrainPlanGUID",plans[i]),
                        new SqlParameter("strDutyUserGUID",InParams.DutyGUID),
                        new SqlParameter("strSiteGUID",InParams.SiteGUID)                        
                    };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.StoredProcedure, strSql, sqlParamsPB);
                    }
                }

                     
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.PublishPlan:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 设置寓休信息
        public class InSetPlanRest
        {
            //计划GUID
            public string PlanGUID;
            //寓休信息
            public Rest RestInfo = new Rest();
        }

        /// <summary>
        /// 设置计划的寓休信息
        /// </summary>
        public InterfaceOutPut SetPlanRest(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InSetPlanRest InParams = javaScriptSerializer.Deserialize<InSetPlanRest>(Data);
                string strSql = "update TAB_Plan_Train set nNeedRest=@nNeedRest,dtArriveTime=@dtArriveTime,dtCallTime=@dtCallTime where strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("nNeedRest",InParams.RestInfo.nNeedRest),
                    new SqlParameter("dtArriveTime",InParams.RestInfo.dtArriveTime),
                    new SqlParameter("dtCallTime",InParams.RestInfo.dtCallTime),
                    new SqlParameter("strTrainPlanGUID",InParams.PlanGUID),
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.SetPlanRest:" + ex.Message);
                throw ex;
            }
            return output;
        }     

        #endregion

        #region 获取派班写实列表
        public class InGetXSTrainmanPlans
        {
            //开始时间
            public DateTime dtBeginTime;
            //结束时间
            public DateTime dtEndTime;
            //交路GUID列表以逗号隔开
            public string jiaolus;
        }

        public class OutGetXSTrainmanPlans
        {
            //计划信息列表
            public MD.TrainmanPlanList Plans = new MD.TrainmanPlanList();
        }

        /// <summary>
        /// 获取派班写实记录
        /// </summary>
        public InterfaceOutPut GetXSTrainmanPlans(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetXSTrainmanPlans InParams = javaScriptSerializer.Deserialize<InGetXSTrainmanPlans>(Data);
                OutGetXSTrainmanPlans OutParams = new OutGetXSTrainmanPlans();
                string[] jiaolus = InParams.jiaolus.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string strJiaolus = "";
                for (int i = 0; i < jiaolus.Length; i++)
                {
                    if (strJiaolus == "")
                    {
                        strJiaolus = "'" + jiaolus[i] + "'";
                    }
                    else
                    {
                        strJiaolus += "," + "'" + jiaolus[i] + "'";
                    }
                }
                if (strJiaolus != "")
                {
                    strJiaolus = string.Format("and strTrainJiaoluGUID  in ({0}) " , strJiaolus);
                }
                string strSql = @"select * 
                         from VIEW_Plan_Trainman as p where dtStartTime >=@BeginTime and dtStartTime <= @EndTime and nPlanState >= 4 " + strJiaolus + "  order by dtStartTime ";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("BeginTime",InParams.dtBeginTime),
                    new SqlParameter("EndTime",InParams.dtEndTime)
                };
                     
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MD.TrainmanPlan tmPlan = new MD.TrainmanPlan();
                    PS.PSPlan.TrainmanPlanFromDB(tmPlan,dt.Rows[i]);
                    OutParams.Plans.Add(tmPlan);
                    
                }

                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetXSTrainmanPlans:" + ex.Message);
                throw ex;
            }
            return output;
        }      

        #endregion

        #region  获取计划变动日志
        public class InGetChangeTrainPlanLog
        {
            //行车计划GUID
            public string TrainPlanGUID;
        }

        public class OutGetChangeTrainPlanLog
        {
            //变动记录
            public TrainPlanChangeLogList Logs = new TrainPlanChangeLogList();
        }

        /// <summary>
        /// 获取计划变动日志
        /// </summary>
        public InterfaceOutPut GetChangeTrainPlanLog(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetChangeTrainPlanLog InParams = javaScriptSerializer.Deserialize<InGetChangeTrainPlanLog>(Data);
                OutGetChangeTrainPlanLog OutParams = new OutGetChangeTrainPlanLog();
                string strSql = "select * from TAB_Plan_ChangeLog where strTrainPlanGUID =  @strTrainPlanGUID ORDER BY dtChangeTime DESC";
                SqlParameter[] sqlParams = new SqlParameter[]{
                     new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TrainPlanChangeLog log = new TrainPlanChangeLog();
                    PS.PSPlan.TrainPlanChangeLogFromDB(log,dt.Rows[i]);
                    OutParams.Logs.Add(log);
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetChangeTrainPlanLog:" + ex.Message);
                throw ex;
            }
            return output;
        }    
        #endregion
    }
}

