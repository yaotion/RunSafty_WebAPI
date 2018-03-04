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
using ThinkFreely.RunSafty;
using TF.Runsafty.Plan.DB;
using TF.RunSafty.DrinkLogic;
using TF.Runsafty.Plan.MD;


namespace TF.RunSafty.Plan
{
    class LCBeginwork
    {
        #region '提交出勤记录'
        public class InSubmit
        {
            //出勤人员GUID
            public string TrainmanGUID;
            //人员值乘的计划信息
            public string TrainPlanGUID;
            //测酒信息
            public DrinkData DrinkInfo = new DrinkData();
            //
            public int VerifyID;
            //
            public string Remark;
        }

        /// <summary>
        /// 上传出勤记录
        /// </summary>
        public InterfaceOutPut Submit(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InSubmit InParams = javaScriptSerializer.Deserialize<InSubmit>(Data);

                string strSqlPlan = "select strTrainJiaoluGUID,strTrainJiaoluName,strTrainNo,dtStartTime,strTrainTypeName,strTrainNumber from view_Plan_Train where strTrainPlanGUID=@strTrainPlanGUID";
                SqlParameter[] sqlParamsPlan = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID)
                };
                DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlPlan, sqlParamsPlan).Tables[0];
                if (dtPlan.Rows.Count == 0)
                {
                    throw new Exception("指定的计划信息不存在,关联退勤记录失败");
                }
                string strTrainJiaoluGUID = dtPlan.Rows[0]["strTrainJiaoluGUID"].ToString();
                string strTrainJiaoluName = dtPlan.Rows[0]["strTrainJiaoluName"].ToString();
                DateTime dtStartTime = Convert.ToDateTime(dtPlan.Rows[0]["dtStartTime"]);
                string strTrainNo = dtPlan.Rows[0]["strTrainNo"].ToString();
                string strTrainTypeName = dtPlan.Rows[0]["strTrainTypeName"].ToString();
                string strTrainNumber = dtPlan.Rows[0]["strTrainNumber"].ToString();
                //添加或者覆盖出勤计划
                string strSql = "select dtCreateTime,strBeginWorkGUID from TAB_Plan_BeginWork where strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID";
                SqlParameter[] sqlParamsCQ = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                    new SqlParameter("strTrainmanGUID",InParams.TrainmanGUID)
                };
                DataTable dtCQ = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsCQ).Tables[0];
                string chuqinID = "";
                SqlTrans sqlTrans = new SqlTrans();
                try
                {
                    sqlTrans.Begin();
                    if (dtCQ.Rows.Count == 0)
                    {
                        chuqinID = Guid.NewGuid().ToString();
                        strSql = @"insert into TAB_Plan_BeginWork (strBeginWorkGUID,strTrainPlanGUID,strTrainmanGUID,dtCreateTime,nVerifyID,strRemark)
                        values (@strBeginWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtCreateTime,@nVerifyID,@strRemark)";
                        SqlParameter[] sqlParamsTemp = new SqlParameter[] { 
                        new SqlParameter("strBeginWorkGUID",chuqinID),
                        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                        new SqlParameter("strTrainmanGUID",InParams.TrainmanGUID),
                        new SqlParameter("dtCreateTime",InParams.DrinkInfo.createTime),
                        new SqlParameter("nVerifyID",InParams.VerifyID),
                        new SqlParameter("strRemark",InParams.Remark)
                    };
                        SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, strSql, sqlParamsTemp);
                    }
                    else
                    {
                        chuqinID = dtCQ.Rows[0]["strBeginWorkGUID"].ToString();
                        strSql = @"update TAB_Plan_BeginWork set nVerifyID=@nVerifyID,strRemark=@strRemark where strBeginWorkGUID=@strBeginWorkGUID";
                        SqlParameter[] sqlParamsTemp = new SqlParameter[] { 
                        new SqlParameter("strBeginWorkGUID",chuqinID),                       
                        new SqlParameter("nVerifyID",InParams.VerifyID),
                        new SqlParameter("strRemark",InParams.Remark)
                    };
                        SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, strSql, sqlParamsTemp);
                    }


                    //将之前的测酒记录和计划关联取消
                    SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("nWorkTypeID", WORKTYPE.WORKTYPE_BEGIN),
                     new SqlParameter("strWorkID", chuqinID),
                    new SqlParameter("strTrainmanGUID", InParams.TrainmanGUID)
                };
                    string sqlText = "update TAB_Drink_Information set strWorkID='' where strWorkID = @strWorkID and nWorkTypeID=@nWorkTypeID and strTrainmanGUID = @strTrainmanGUID";
                    SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sqlText, sqlParams);


                    #region 添加测酒记录
                    MDDrink MDDr = new MDDrink();
                    DBDrink DBDr = new DBDrink();


                    //职位信息----- 开始----------
                    DBDrinkLogic dbdl = new DBDrinkLogic();
                    MDDrinkLogic mddl = new MDDrinkLogic();
                    mddl = dbdl.GetDrinkCadreEntity(InParams.DrinkInfo.strTrainmanNumber);
                    if (mddl != null)
                    {
                        MDDr.strDepartmentID = mddl.strDepartmentID;
                        MDDr.strDepartmentName = mddl.strDepartmentName;
                        MDDr.nCadreTypeID = mddl.nCadreTypeID;
                        MDDr.strCadreTypeName = mddl.strCadreTypeName;
                    }
                    //职位信息----- 结束----------
                    sqlText = "select strGUID from tab_drink_information where strTrainmanGUID=@strTrainmanGUID and dtCreateTime=@dtCreateTime";
                    SqlParameter[] sqlParamsDrink = new SqlParameter[] { 
                    new SqlParameter("strTrainmanGUID",InParams.TrainmanGUID),
                    new SqlParameter("dtCreateTime",InParams.DrinkInfo.createTime)
                     };
                    string drinkGUID = "";
                    object objDrink = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsDrink);
                    if ((objDrink != null) && (!DBNull.Value.Equals(objDrink)))
                    {
                        drinkGUID = objDrink.ToString();
                    }
                    if (drinkGUID == "")
                    {
                        //是否是本段
                        MDDr.nLocalAreaTrainman = InParams.DrinkInfo.bLocalAreaTrainman == true ? 1 : 0;
                        MDDr.trainmanID = InParams.TrainmanGUID;
                        MDDr.createTime = InParams.DrinkInfo.createTime;
                        MDDr.verifyID = InParams.VerifyID;
                        MDDr.oPlaceId = chuqinID;
                        MDDr.strGuid = Guid.NewGuid().ToString();
                        MDDr.drinkResult = InParams.DrinkInfo.drinkResult;
                        MDDr.strAreaGUID = InParams.DrinkInfo.strAreaGUID;
                        MDDr.dutyUserID = "";
                        MDDr.strTrainmanName = InParams.DrinkInfo.strTrainmanName;
                        MDDr.strTrainmanNumber = InParams.DrinkInfo.strTrainmanNumber;
                        MDDr.strTrainNo = InParams.DrinkInfo.strTrainNo;
                        MDDr.strTrainNumber = InParams.DrinkInfo.strTrainNumber;
                        MDDr.strTrainTypeName = InParams.DrinkInfo.strTrainTypeName;
                        MDDr.createTime = InParams.DrinkInfo.createTime;
                        MDDr.strWorkShopGUID = InParams.DrinkInfo.strWorkShopGUID;
                        MDDr.strWorkShopName = InParams.DrinkInfo.strWorkShopName;
                        MDDr.strPlaceID = InParams.DrinkInfo.strPlaceID;
                        MDDr.strPlaceName = InParams.DrinkInfo.strPlaceName;
                        MDDr.strSiteGUID = InParams.DrinkInfo.strSiteGUID;
                        MDDr.strSiteName = InParams.DrinkInfo.strSiteName;
                        MDDr.dwAlcoholicity = InParams.DrinkInfo.dwAlcoholicity;
                        MDDr.strWorkID = chuqinID;
                        MDDr.nWorkTypeID = WORKTYPE.WORKTYPE_BEGIN;//工作类型为出勤
                        MDDr.imagePath = InParams.DrinkInfo.imagePath;
                        DBDr.SubmitDrink(MDDr, sqlTrans.trans);
                    }
                    else
                    {
                        strSql = @"Update Tab_Drink_Information set strWorkID = @strWorkID,nWorkTypeID =nWorkTypeID,
                            strTrainNo=@strTrainNo,strTrainTypeName=@strTrainTypeName,strTrainmanName=@strTrainmanName where strGUID = @strGUID";
                        SqlParameter[] sqlParamsDrinkU = new SqlParameter[] { 
                            new SqlParameter("strWorkID",chuqinID),
                            new SqlParameter("nWorkTypeID",WORKTYPE.WORKTYPE_END),
                            new SqlParameter("strGUID",drinkGUID),
                            new SqlParameter("strTrainNo",InParams.DrinkInfo.strTrainNo),
                            new SqlParameter("strTrainTypeName",InParams.DrinkInfo.strTrainTypeName),
                            new SqlParameter("strTrainmanName",InParams.DrinkInfo.strTrainmanName)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDrinkU);
                    }
                    #endregion

                    strSql = @"update TAB_Plan_Train set nPlanState=7 where strTrainPlanGUID=@strTrainPlanGUID and 
                             (select count(*) from VIEW_Plan_BeginWork where strTrainPlanGUID=TAB_Plan_Train.strTrainPlanGUID  and 
                             ((strTrainmanGUID1 is null) or (strTrainmanGUID1 = @TrainmanGUID) or not(dtTestTime1 is null)) and 
                               ((strTrainmanGUID2 is null) or (strTrainmanGUID2 = @TrainmanGUID) or not(dtTestTime2 is null))  and 
                               ((strTrainmanGUID3 is null) or (strTrainmanGUID3 = @TrainmanGUID) or not(dtTestTime3 is null))   and 
                               ((strTrainmanGUID4 is null) or (strTrainmanGUID4 = @TrainmanGUID) or not(dtTestTime4 is null))) > 0";
                    SqlParameter[] sqlParamsState = new SqlParameter[] { 
                        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                        new SqlParameter("TrainmanGUID","")
                    };
                    SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, strSql, sqlParamsState);

                    strSql = "update TAB_Plan_Train set dtBeginWorkTime=getdate() where strTrainPlanGUID = @strTrainPlanGUID and dtBeginWorkTime is null ";
                    SqlParameter[] sqlParamsCQTime = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID)
                    };
                    SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, strSql, sqlParamsCQTime);
                    sqlTrans.Commit();
                    //修改实际出勤时间
                    output.result = 0;

                    //发送出勤消息
                    TF.RunSafty.Plan.MD.BeginworkMsgData msgData = new TF.RunSafty.Plan.MD.BeginworkMsgData();
                    msgData.cjjg = Convert.ToInt32(InParams.DrinkInfo.drinkResult);
                    msgData.dtStartTime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(dtStartTime);
                    msgData.dttime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(Convert.ToDateTime(InParams.DrinkInfo.createTime));

                    msgData.jiaoLuGUID = strTrainJiaoluGUID;
                    msgData.jiaoLuName = strTrainJiaoluName;
                    msgData.strTrainNo = strTrainNo;
                    msgData.strTrainTypeName = strTrainTypeName;
                    msgData.strTrainNumber = strTrainNumber;

                    msgData.planGuid = InParams.TrainPlanGUID;

                    msgData.tmGuid = InParams.TrainmanGUID;
                    msgData.tmid = InParams.DrinkInfo.strTrainmanNumber;
                    msgData.tmname = InParams.DrinkInfo.strTrainmanName;
                    msgData.Tmis = 0;
                    ThinkFreely.RunSafty.AttentionMsg msg = msgData.ToMsg();
                    msg.CreatMsg();

                    //string strTrainmanNumber = "";
                    //string strTrainmanName = "";
                    //DataTable dt = GetTrainman(InParams.TrainmanGUID);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    strTrainmanNumber = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber"]);
                    //    strTrainmanName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanName"]);
                    //}
                    //SaveDrinkStepData(InParams.TrainPlanGUID, InParams.TrainmanGUID, strTrainmanNumber, strTrainmanName, InParams, sqlTrans.trans);
                }
                catch (Exception ex)
                {
                    sqlTrans.RollBack();
                    throw ex;
                }
               ;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.Submit:" + ex.Message);
                throw ex;
            }
            return output;
        }

        public class SubTestAlcoholInfo
        {
            public int TestAlcoholResult = 0;
            public int VerfiyID = 0;
            public DateTime? TestTime;
            public string TestPicturePath = "";
            public int Alcoholicity = 0;

        }

        private DataTable GetTrainman(string strTrainmanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strTrainmanNumber,strTrainmanName from TAB_Org_Trainman where strTrainmanGUID = '" + strTrainmanGUID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            return dt;
        }

        private void SaveDrinkStepData(string strTrainPlanGUID, string strTrainmanGUID, string strTrainmanNumber, string strTainmanName, InSubmit InParams,SqlTransaction trans)
        {
            DBPlan_Beginwork_Step db = new DBPlan_Beginwork_Step();
            Plan_Beginwork_Step step = new Plan_Beginwork_Step();
            SubTestAlcoholInfo alcoholInfo = new SubTestAlcoholInfo();
            alcoholInfo.TestAlcoholResult = int.Parse(InParams.DrinkInfo.drinkResult);
            alcoholInfo.VerfiyID = InParams.VerifyID;
            alcoholInfo.TestTime = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
            alcoholInfo.TestPicturePath = InParams.DrinkInfo.imagePath;
            alcoholInfo.Alcoholicity = ObjectConvertClass.static_ext_int(InParams.DrinkInfo.dwAlcoholicity);
            step.nStepID = 1003;
            step.nStepResultID = 1;
            step.strTrainmanGUID = strTrainmanGUID;
            step.strTrainmanNumber = strTrainmanNumber;
            step.strTrainmanName = strTainmanName;
            step.strTrainPlanGUID = strTrainPlanGUID;
            step.dtEventTime = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
            step.dtEventEndTime = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
            //step.strStepResultText = string.Format("{\"TestAlcoholResult\":\"{0}\",\"VerfiyID\":\"{1}\",\"TestTime\":\"{2}\",\"TestPicturePath\":\"{3}\",\"Alcoholicity\":\"{4}\"}",
            //           InParams.DrinkInfo.drinkResult, InParams.VerifyID, InParams.DrinkInfo.createTime, InParams.DrinkInfo.imagePath, InParams.DrinkInfo.dwAlcoholicity);
            step.strStepResultText = Newtonsoft.Json.JsonConvert.SerializeObject(alcoholInfo);
            db.Add(step,trans);
        }



        //通过计划GUID获取开车时间
        public DateTime GetdtStartTime(string strTrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 dtStartTime from TAB_Plan_Train where strTrainPlanGUID ='" + strTrainPlanGUID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return Convert.ToDateTime(dt.Rows[0]["dtStartTime"].ToString());
            }
            return DateTime.Now;
         
        }


        //通过计划GUID获取实际出勤时间差
        public int GetdtStartTimeDiff(string strTrainPlanGUID, DateTime dtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_Plan_Beginwork_StepData where strTrainPlanGUID ='" + strTrainPlanGUID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            DateTime dtEventTime1 = new DateTime();
            DateTime dtEventTime2 = new DateTime();
            DateTime dtEventTime3 = new DateTime();
            int nMinnutes1 = 0, nMinnutes2 = 0, nMinnutes3 = 0;
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (dt.Rows[k]["strFieldName"].ToString() == "dtEventTime1")
                {
                    dtEventTime1 = Convert.ToDateTime(dt.Rows[k]["dtStepData"]);
                }
                else if (dt.Rows[k]["strFieldName"].ToString() == "dtEventTime2")
                {
                    dtEventTime2 = Convert.ToDateTime(dt.Rows[k]["dtStepData"]);
                }
                else if (dt.Rows[k]["strFieldName"].ToString() == "dtEventTime3")
                {
                    dtEventTime3 = Convert.ToDateTime(dt.Rows[k]["dtStepData"]);
                }

            }

            if (dtEventTime1 > Convert.ToDateTime("1999-01-01"))
            {
                TimeSpan ts = dtime - dtEventTime1;
                nMinnutes1 = Convert.ToInt32(ts.TotalMinutes);
            }
            if (dtEventTime2 > Convert.ToDateTime("1999-01-01"))
            {
                TimeSpan ts = dtime - dtEventTime2;
                nMinnutes2 = Convert.ToInt32(ts.TotalMinutes);

            }
            if (dtEventTime3 > Convert.ToDateTime("1999-01-01"))
            {
                TimeSpan ts = dtime - dtEventTime2;
                nMinnutes3 = Convert.ToInt32(ts.TotalMinutes);
            }
            if (nMinnutes1 > nMinnutes2 && nMinnutes1 > nMinnutes3)
                return nMinnutes1;
            else if (nMinnutes2 > nMinnutes1 && nMinnutes2 > nMinnutes3)
                return nMinnutes2;
            else
                return nMinnutes3;
        }

        #endregion

        #region '清除出勤记录'
        public class InClear
        {
            //指定计划GUID                                    
            public string PlanGUID;
            //指定人员GUID
            public string TrainmanNumber;
        }

        /// <summary>
        /// 清除指定人员的出勤记录
        /// </summary>
        public InterfaceOutPut Clear(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InClear InParams = javaScriptSerializer.Deserialize<InClear>(Data);
                //查找计划
                string strSql = "select  dtStartTime,nPlanState from TAB_Plan_Train where strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.PlanGUID)
                };
                DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dtPlan.Rows.Count == 0)
                {
                    output.resultStr = "没有找到指定的计划";
                    return output;
                }
                //不能撤销已退勤的出勤计划
                if (dtPlan.Rows[0]["nPlanState"].ToString() != "7" && dtPlan.Rows[0]["nPlanState"].ToString() != "4")
                {
                    output.resultStr = "只能撤销处于已出勤或已发布状态的计划";
                    return output;
                }
                //计划出勤时间已过6个小时，不能撤销!
                if (DateTime.Parse(dtPlan.Rows[0]["dtStartTime"].ToString()) < DateTime.Now.AddHours(-6))
                {
                    output.resultStr = "计划出勤时间已过6个小时，不能撤销";
                    return output;
                }

                //获取人员GUID
                strSql = "select strTrainmanGUID from TAB_Org_Trainman where strTrainmanNumber = @strTrainmanNumber";
                SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainmanNumber",InParams.TrainmanNumber)
                };
                DataTable dtTM = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTM).Tables[0];
                if (dtTM.Rows.Count == 0)
                {
                    output.resultStr = "没有找到指定的人员信息";
                    return output;
                }
                string strTrainmanGUID  = dtTM.Rows[0]["strTrainmanGUID"].ToString();

                //获取人员在指定计划的出勤记录
                strSql = "Select strBeginWorkGUID from TAB_Plan_BeginWork where strTrainPlanGUID = @strTrainPlanGUID  and strTrainmanGUID = @strTrainmanGUID";
                SqlParameter[] sqlParamsCQ = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.PlanGUID),
                    new SqlParameter("strTrainmanGUID",strTrainmanGUID)
                };
                DataTable dtCQ = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParamsCQ).Tables[0];
                if (dtCQ.Rows.Count == 0)
                {
                    output.resultStr = "没有找到指定的人员的出勤记录";
                    return output;
                }
                string strBeginWorkGUID = dtCQ.Rows[0]["strBeginWorkGUID"].ToString();

                //删除出勤记录
                strSql = "delete from TAB_Plan_BeginWork where strBeginWorkGUID = @strBeginWorkGUID";
                SqlParameter[] sqlParamsBeginwork = new SqlParameter[] { 
                    new SqlParameter("strBeginWorkGUID",strBeginWorkGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsBeginwork);
                //删除测酒记录
                strSql = "delete from TAB_Drink_Information where strWorkID = @strWorkID";
                SqlParameter[] sqlParamsDrink = new SqlParameter[] { 
                    new SqlParameter("strWorkID",strBeginWorkGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDrink);
                //修改计划状态
                strSql = "update TAB_Plan_Train Set nPlanState = 4 where strTrainPlanGUID = @strTrainPlanGUID ";
                SqlParameter[] sqlParamsPlanUpdate = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.PlanGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsPlanUpdate);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.Clear:" + ex.Message);
                throw ex;
            }
            return output;
        }    
        #endregion

        #region 判断人员在指定计划中是否已经出勤
        public class InTrainmanIsBeginwork
        {
            //人员工号
            public string TrainmanGUID;
            //计划GUID
            public string TrainPlanGUID;
        }

        public class OutTrainmanIsBeginwork
        {
            //是否存在
            public int Exist;
        }

        /// <summary>
        /// 乘务员是否已经出勤
        /// </summary>
        public InterfaceOutPut TrainmanIsBeginwork(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InTrainmanIsBeginwork InParams = javaScriptSerializer.Deserialize<InTrainmanIsBeginwork>(Data);
                OutTrainmanIsBeginwork OutParams = new OutTrainmanIsBeginwork();
                string strSql = "select top 1 * from tab_plan_BeginWork where strTrainmanGUID = @strTrainmanGUID and strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainmanGUID",InParams.TrainmanGUID),
                    new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID)
                };
                OutParams.Exist = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0].Rows.Count;
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.TrainmanIsBeginwork:" + ex.Message);
                throw ex;
            }
            return output;
        }        

        #endregion

        #region 将已有测酒记录和计划匹配
        public class InUnion
        {
            //人员GUID                                             
            public string TrainmanGUID;
            //计划GUID
            public string TrainPlanGUID;
            //测酒信息
            public DrinkData DrinkInfo = new DrinkData();
            //
            public int VerifyID;
            //
            public string Remark;
        }

        /// <summary>
        /// 关联已有测酒记录
        /// </summary>
        public InterfaceOutPut Union(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InUnion InParams = javaScriptSerializer.Deserialize<InUnion>(Data);
                if (InParams.DrinkInfo.strGuid == "")
                {
                    output.resultStr = "不能关联不存在的测酒记录";
                    return output;
                }
                //添加或者覆盖出勤计划
                string strSql = "select dtCreateTime,strBeginWorkGUID from TAB_Plan_BeginWork where strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID";
                SqlParameter[] sqlParamsCQ = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                    new SqlParameter("strTrainmanGUID",InParams.TrainmanGUID)
                };
                DataTable dtCQ = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsCQ).Tables[0];
                string chuqinID = "";
                bool bReDrink = false;
                if (dtCQ.Rows.Count == 0)
                {
                    chuqinID = Guid.NewGuid().ToString();
                    strSql = @"insert into TAB_Plan_BeginWork (strBeginWorkGUID,strTrainPlanGUID,strTrainmanGUID,dtCreateTime,nVerifyID,strRemark)
                        values (@strBeginWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtCreateTime,@nVerifyID,@strRemark)";
                    SqlParameter[] sqlParamsTemp = new SqlParameter[] { 
                        new SqlParameter("strBeginWorkGUID",chuqinID),
                        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                        new SqlParameter("strTrainmanGUID",InParams.TrainmanGUID),
                        new SqlParameter("dtCreateTime",InParams.DrinkInfo.createTime),
                        new SqlParameter("nVerifyID",InParams.VerifyID),
                        new SqlParameter("strRemark",InParams.Remark)
                    };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTemp);
                }
                else
                {
                    chuqinID = dtCQ.Rows[0]["strBeginWorkGUID"].ToString();
                    bReDrink = true;
                    strSql = @"update TAB_Plan_BeginWork set nVerifyID=@nVerifyID,strRemark=@strRemark where strBeginWorkGUID=@strBeginWorkGUID";
                    SqlParameter[] sqlParamsTemp = new SqlParameter[] { 
                        new SqlParameter("strBeginWorkGUID",chuqinID),                       
                        new SqlParameter("nVerifyID",InParams.VerifyID),
                        new SqlParameter("strRemark",InParams.Remark)
                    };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTemp);
                }            
                //修改测酒记录
                strSql = @"update TAB_Drink_Information set strWorkID=@strWorkID , nWorkTypeID=2  where strGUID = @strGUID";
                SqlParameter[] sqlParamsDrink = new SqlParameter[] { 
                        new SqlParameter("strGUID",InParams.DrinkInfo.strGuid),               
                        new SqlParameter("strWorkID",chuqinID),
                    };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDrink);

                //修改计划状态,设置为已出勤

                strSql = @"update TAB_Plan_Train set nPlanState=7 where strTrainPlanGUID=@strTrainPlanGUID and 
                             (select count(*) from VIEW_Plan_BeginWork where strTrainPlanGUID=TAB_Plan_Train.strTrainPlanGUID  and 
                             ((strTrainmanGUID1 is null) or (strTrainmanGUID1 = @TrainmanGUID) or (strTrainmanGUID1 = '') or not(dtTestTime1 is null)) and 
                               ((strTrainmanGUID2 is null) or (strTrainmanGUID2 = @TrainmanGUID) or (strTrainmanGUID2 = '') or not(dtTestTime2 is null))  and 
                               ((strTrainmanGUID3 is null) or (strTrainmanGUID3 = @TrainmanGUID) or (strTrainmanGUID3 = '') or not(dtTestTime3 is null))   and 
                               ((strTrainmanGUID4 is null) or (strTrainmanGUID4 = @TrainmanGUID) or (strTrainmanGUID4 = '') or not(dtTestTime4 is null))) > 0";
                SqlParameter[] sqlParamsState = new SqlParameter[] { 
                        new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                        new SqlParameter("TrainmanGUID",InParams.TrainmanGUID)
                    };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsState);

                strSql = "update TAB_Plan_Train set dtBeginWorkTime=@dtBeginWorkTime where strTrainPlanGUID = @strTrainPlanGUID and dtBeginWorkTime is null ";
                SqlParameter[] sqlParamsCQTime = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID),
                    new SqlParameter("dtBeginWorkTime",InParams.DrinkInfo.createTime)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsCQTime);

                #region 获取计划信息
                string strSqlPlan = "select strTrainJiaoluGUID,strTrainJiaoluName,strTrainNo,dtStartTime,strTrainTypeName,strTrainNumber from view_Plan_Train where strTrainPlanGUID=@strTrainPlanGUID";
                SqlParameter[] sqlParamsPlan = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.TrainPlanGUID)
                };
                DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlPlan, sqlParamsPlan).Tables[0];
                if (dtPlan.Rows.Count == 0)
                {
                    throw new Exception("指定的计划信息不存在,关联退勤记录失败");
                }
                string strTrainJiaoluGUID = dtPlan.Rows[0]["strTrainJiaoluGUID"].ToString();
                string strTrainJiaoluName = dtPlan.Rows[0]["strTrainJiaoluName"].ToString();
                DateTime dtStartTime = Convert.ToDateTime(dtPlan.Rows[0]["dtStartTime"]);
                string strTrainNo = dtPlan.Rows[0]["strTrainNo"].ToString();
                string strTrainTypeName = dtPlan.Rows[0]["strTrainTypeName"].ToString();
                string strTrainNumber = dtPlan.Rows[0]["strTrainNumber"].ToString();
                
                    
                #endregion

                #region 获取人员信息
                string strSqlTM = "select strTrainmanNumber,strTrainmanName  from TAB_Org_Trainman where strTrainmanGUID=@strTrainmanGUID";
                SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainmanGUID",InParams.TrainmanGUID)
                };
                DataTable dtTM = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlTM, sqlParamsTM).Tables[0];
                if (dtTM.Rows.Count == 0)
                {
                    throw new Exception("指定的人员信息不存在,关联退勤记录失败");

                }
                string strTrainmanName = dtTM.Rows[0]["strTrainmanName"].ToString();
                string strTrainmanNumber = dtTM.Rows[0]["strTrainmanNumber"].ToString();
                #endregion


                //发送出勤消息
                TF.RunSafty.Plan.MD.BeginworkMsgData msgData = new TF.RunSafty.Plan.MD.BeginworkMsgData();
                msgData.cjjg = Convert.ToInt32(InParams.DrinkInfo.drinkResult);
                msgData.dtStartTime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(dtStartTime);
                msgData.dttime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(Convert.ToDateTime(InParams.DrinkInfo.createTime));

                msgData.jiaoLuGUID = strTrainJiaoluGUID;
                msgData.jiaoLuName = strTrainJiaoluName;
                msgData.strTrainNo = strTrainNo;
                msgData.strTrainTypeName = strTrainTypeName;
                msgData.strTrainNumber = strTrainNumber;

                msgData.planGuid = InParams.TrainPlanGUID;

                msgData.tmGuid = InParams.TrainmanGUID;
                msgData.tmid = strTrainmanNumber;
                msgData.tmname = strTrainmanName;
                msgData.Tmis = 0;
                ThinkFreely.RunSafty.AttentionMsg msg = msgData.ToMsg();
                msg.CreatMsg();
                //修改实际出勤时间
                output.result = 0;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.Union:" + ex.Message);
                throw ex;
            }
            return output;
        } 


        #endregion

        #region 
        public class InGetBeginworkPlanByTime
        {
            //乘务员工号
            public string TrainmanNumber;
            //出勤时间                                             
            public DateTime ChuQinTime;
            //客户端GUID
            public string SiteGUID;
        }

        public class OutGetBeginworkPlanByTime
        {
       
            //计划信息
            public ChuQinPlan Plan = new ChuQinPlan();    
            //是否存在
            public int Exist;
        }

        /// <summary>
        /// 获取指定退勤时间相关的人员的出勤计划
        /// </summary>
        public InterfaceOutPut GetBeginworkPlanByTime(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetBeginworkPlanByTime InParams = javaScriptSerializer.Deserialize<InGetBeginworkPlanByTime>(Data);
                OutGetBeginworkPlanByTime OutParams = new OutGetBeginworkPlanByTime();
                output.data = OutParams;
                string strSql = @"select top 1 * from VIEW_Plan_BeginWork 
                             where strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = @strSiteGUID) and 
                            (nPlanState in (4,7)) and (strTrainmanNumber1 = @strTrainmanNumber or strTrainmanNumber2 = @strTrainmanNumber or strTrainmanNumber3 = @strTrainmanNumber or strTrainmanNumber4 = @strTrainmanNumber)";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strSiteGUID",InParams.SiteGUID),
                    new SqlParameter("strTrainmanNumber",InParams.TrainmanNumber)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                                
                    DateTime dtStartTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[0]["dtStartTime"],DateTime.Parse("1899-01-01"));
                    if (InParams.ChuQinTime < dtStartTime.AddMinutes(30))
                    {
                         OutParams.Exist = 1;
                         PS.PSPlan.BeginworkFromDB(OutParams.Plan, dt.Rows[0]);
                    }
                }

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetBeginworkPlanByTime:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

        #region  执行允许出勤操作
        public class InAllowBeginwork
        {
            //允许出勤数据
            public AllowBeginworkData AllowData = new AllowBeginworkData();
        }

        /// <summary>
        /// 执行允许出勤操作
        /// </summary>
        public InterfaceOutPut AllowBeginwork(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAllowBeginwork InParams = javaScriptSerializer.Deserialize<InAllowBeginwork>(Data);
                string strSql = "select top 1 * from TAB_Plan_Beginwork_Flow where strTrainPlanGUID = @strTrainPlanGUID";
                SqlParameter[] sqlParamsPlan = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.AllowData.strTrainPlanGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsPlan).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    strSql = @"insert into TAB_Plan_Beginwork_Flow (strTrainPlanGUID,strWorkShopGUID,nFlowState,strDutyUserName,strDutyUserGUID,
                        strDutyUserNumber,dtCreateTime,dtConfirmTime,strBrief) values (@strTrainPlanGUID,@strWorkShopGUID,@nFlowState,@strDutyUserName,
                        @strDutyUserGUID,@strDutyUserNumber,@dtCreateTime,@dtConfirmTime,@strBrief)";
                    SqlParameter[] sqlparamsUpdate = new SqlParameter[] { 
                        new SqlParameter("strTrainPlanGUID",InParams.AllowData.strTrainPlanGUID),
                        new SqlParameter("strWorkShopGUID",InParams.AllowData.strWorkShopGUID),
                        new SqlParameter("nFlowState",InParams.AllowData.nFlowState),
                        new SqlParameter("strDutyUserName",InParams.AllowData.strDutyUserName),
                        new SqlParameter("strDutyUserGUID",InParams.AllowData.strDutyUserGUID),
                        new SqlParameter("strDutyUserNumber",InParams.AllowData.strDutyUserNumber),
                        new SqlParameter("dtCreateTime",InParams.AllowData.dtCreateTime),
                        new SqlParameter("dtConfirmTime",InParams.AllowData.dtConfirmTime),
                        new SqlParameter("strBrief",InParams.AllowData.strBrief)
                    };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlparamsUpdate);
                }
                else
                {
                    strSql = @"update TAB_Plan_Beginwork_Flow set strWorkShopGUID=@strWorkShopGUID,nFlowState=@nFlowState,strDutyUserName=@strDutyUserName,
                        strDutyUserGUID=@strDutyUserGUID,
                        strDutyUserNumber=@strDutyUserNumber,dtCreateTime=@dtCreateTime,dtConfirmTime=@dtConfirmTime,strBrief=@strBrief 
                        where strTrainPlanGUID = @strTrainPlanGUID";
                    SqlParameter[] sqlparamsUpdate = new SqlParameter[] { 
                        new SqlParameter("strTrainPlanGUID",InParams.AllowData.strTrainPlanGUID),
                        new SqlParameter("strWorkShopGUID",InParams.AllowData.strWorkShopGUID),
                        new SqlParameter("nFlowState",InParams.AllowData.nFlowState),
                        new SqlParameter("strDutyUserName",InParams.AllowData.strDutyUserName),
                        new SqlParameter("strDutyUserGUID",InParams.AllowData.strDutyUserGUID),
                        new SqlParameter("strDutyUserNumber",InParams.AllowData.strDutyUserNumber),
                        new SqlParameter("dtCreateTime",InParams.AllowData.dtCreateTime),
                        new SqlParameter("dtConfirmTime",InParams.AllowData.dtConfirmTime),
                        new SqlParameter("strBrief",InParams.AllowData.strBrief)
                    };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlparamsUpdate);
                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AllowBeginwork:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取乘务员出勤卡空结果信息

        public class InGetTrainmanCheckRecords
        {
            //工号
            public string TrainmanNumber;
            //时间
            public DateTime CheckTime;
        }

        public class OutGetTrainmanCheckRecords
        {
            //卡控项列表
            public CheckRecordList CheckRecords = new CheckRecordList();
        }

        /// <summary>
        /// 获取乘务员出勤卡空结果信息
        /// </summary>
        public InterfaceOutPut GetTrainmanCheckRecords(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainmanCheckRecords InParams = javaScriptSerializer.Deserialize<InGetTrainmanCheckRecords>(Data);
                OutGetTrainmanCheckRecords OutParams = new OutGetTrainmanCheckRecords();
                string strSql = @"select C.*,B.strPointName,B.nIsHold,B.nPointID  from TAB_CheckPoint_Standard AS B 
                    Left Join (SELECT top 1 * FROM TAB_Plan_RunEvent_TrainmanDetail AS A 
                    where A.dtEventTime > @MinTime and A.dtEventTime < @MaxTime AND A.strTrainmanNumber = @strTrainmanNumber order by a.dtEventTime desc) 
                    AS C ON  C.nEventID = B.nPointID ";
                DateTime dtMaxTime = InParams.CheckTime.AddMinutes(30);
                DateTime dtMinTime = InParams.CheckTime.AddMinutes(-30);
                SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("MinTime",dtMinTime),
                    new SqlParameter("MaxTime",dtMaxTime),
                    new SqlParameter("strTrainmanNumber",InParams.TrainmanNumber)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckRecord record = new CheckRecord();
                    record.nPointID = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nPointID"], 0);
                    record.strPointName = dt.Rows[i]["strPointName"].ToString();
                    record.nIsHold = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nIsHold"], 0);
                    if ((dt.Rows[i]["dtEventTime"] == null) || (DBNull.Value.Equals(dt.Rows[i]["dtEventTime"])))
                    {
                        record.strItemContent = "无记录";
                        record.nCheckResult = 1;
                    }
                    else
                    {
                        record.strTrainmanNumber = dt.Rows[i]["strTrainmanNumber"].ToString();
                        record.strItemContent = dt.Rows[i]["strResult"].ToString();
                        record.nCheckResult = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nResultID"], 0);
                        record.dtCheckTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtEventTime"], DateTime.Parse("1899-01-01"));
                        record.dtCreateTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtCreateTime"], DateTime.Parse("1899-01-01"));
                    }



                    OutParams.CheckRecords.Add(record);
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainmanCheckRecords:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

        #region 获取计划出勤流程信息
        public class InGetPlanFlow
        {
            //行车计划GUID
            public string TrainplanGUID;
            //所在车间GUID                           
            public string WorkShopGUID;
        }

        public class OutGetPlanFlow
        {            
            //FlowArray
            public BeginworkFlowList FlowArray = new BeginworkFlowList();
            //StepArray
            public TrainmanBeginworkStepList StepArray = new TrainmanBeginworkStepList();
            //TrainFlow
            public TrainplanBeginworkFlow TrainFlow = new TrainplanBeginworkFlow();
        }

        /// <summary>
        /// 获取计划的出勤步骤信息
        /// </summary>
        public InterfaceOutPut GetPlanFlow(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetPlanFlow InParams = javaScriptSerializer.Deserialize<InGetPlanFlow>(Data);
                OutGetPlanFlow OutParams = new OutGetPlanFlow();
                string strSql = "select * from TAB_Base_Beginwork_Flow where strWorkShopGUID = @strWorkShopGUID order by nStepIndex";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //BeginworkFlow f = new BeginworkFlow();
                    //f.nid = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nid"], 0);
                    //f.nStepID = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nStepID"], 0);
                    //f.nStepIndex = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nStepIndex"], 0);
                    //f.nStepType = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nStepType"], 0);
                    //f.strStepName = dt.Rows[i]["strStepName"].ToString();
                    //f.strWorkShopGUID = dt.Rows[i]["strWorkShopGUID"].ToString();
                    //OutParams.FlowArray.Add(f);
                }
                strSql = "select * from TAB_Plan_Beginwork_Step where strTrainPlanGUID = @strTrainPlanGUID order by strTrainmanGUID,nStepID";
                SqlParameter[] sqlParams2 = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",InParams.TrainplanGUID)
                };
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams2).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TrainmanBeginworkStep step = new TrainmanBeginworkStep();
                    step.dtCreateTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtCreateTime"], DateTime.Parse("1899-01-01"));
                    step.dtEventTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[i]["dtEventTime"], DateTime.Parse("1899-01-01"));
                    step.nStepID = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nStepID"], 0);
                    step.nStepResultID = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[i]["nStepResultID"], 0);
                    step.strStepResultText = dt.Rows[i]["strStepResultText"].ToString(); ;
                    step.strTrainmanGUID = dt.Rows[i]["strTrainmanGUID"].ToString();
                    step.strTrainmanName = dt.Rows[i]["strTrainmanName"].ToString();
                    step.strTrainmanNumber = dt.Rows[i]["strTrainmanNumber"].ToString();
                    step.strTrainPlanGUID = dt.Rows[i]["strTrainPlanGUID"].ToString();
                    OutParams.StepArray.Add(step);
                }

                strSql = "select * from TAB_Plan_Beginwork_Flow where strTrainPlanGUID = @strTrainPlanGUID";
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams2).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    OutParams.TrainFlow.strTrainPlanGUID = dt.Rows[0]["strTrainPlanGUID"].ToString();
                    OutParams.TrainFlow.strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                    OutParams.TrainFlow.dtCreateTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[0]["dtCreateTime"], DateTime.Parse("1899-01-01"));
                    OutParams.TrainFlow.nFlowState = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[0]["nFlowState"], 0);
                    OutParams.TrainFlow.strDutyUserName = dt.Rows[0]["strDutyUserName"].ToString();
                    OutParams.TrainFlow.strDutyUserGUID = dt.Rows[0]["strDutyUserGUID"].ToString();
                    OutParams.TrainFlow.strDutyUserNumber = dt.Rows[0]["strDutyUserNumber"].ToString();
                    OutParams.TrainFlow.strBrief = dt.Rows[0]["strBrief"].ToString();
                    OutParams.TrainFlow.dtConfirmTime = TF.RunSafty.Utils.Parse.TFParse.DBToDateTime(dt.Rows[0]["dtConfirmTime"], DateTime.Parse("1899-01-01"));
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetPlanFlow:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion 获取计划出勤流程信息

        #region 获取指定人员在指定客户端下的出勤计划
        public class BeginWork_In
        {
            public string siteID { get; set; }
            public string trainmanID { get; set; }
        }
        public class BeginWork_Out
        {
            public int result;
            public string resultStr;
            public TrainmanPlan data;
            public object InterfaceRet;
        }
        public BeginWork_Out GetPlan(string data)
        {
            BeginWork_Out json = new BeginWork_Out();
            BeginWork_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<BeginWork_In>(data);
            TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork();
            try
            {
                string clientID = input.siteID;
                string strTrainmanid = input.trainmanID;
                // List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> plans = bllPlan.GetChuqinPlansOfTrainmanInSite(clientID, strTrainmanid);
                List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vplans = bllPlan.GetBeginWorkOfTrainmanInSite(clientID, strTrainmanid);
                List<TrainmanPlan> plans = GetPlanList(vplans);


                json.InterfaceRet = (plans != null && plans.Count > 0);
                if ((Boolean)json.InterfaceRet)
                {
                    json.data = plans[0];
                }
             
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = 1;
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }

        private List<TrainmanPlan> GetPlanList(List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans)
        {
            List<TrainmanPlan> lPlans = new List<TrainmanPlan>();
            TrainmanPlan clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_BeginWork plan in vPlans)
                {
                    clientPlan = new TrainmanPlan();
                    ChuqinGroup chuqinGroup = new ChuqinGroup();
                    clientPlan.chuqinGroup = chuqinGroup;
                    DutyPlace cPlace = new DutyPlace();
                    chuqinGroup.group = new NameGroup();
                    chuqinGroup.group.place = cPlace;
                    cPlace.placeID = plan.strPlaceID;
                    cPlace.placeName = plan.strPlaceName;
                    chuqinGroup.group.groupID = plan.strGroupGUID;
                    chuqinGroup.group.station = new Station();
                    chuqinGroup.group.station.stationID = plan.strStationGUID;
                    chuqinGroup.group.station.stationName = plan.strStationName;
                    if (plan.strStationNumber.HasValue)
                    {
                        chuqinGroup.group.station.stationNumber = plan.strStationNumber.ToString();
                    }
                    chuqinGroup.group.trainman1 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman1.ABCD = plan.strABCD1;
                    if (plan.nDriverType1.HasValue)
                    {
                        chuqinGroup.group.trainman1.driverTypeID = plan.nDriverType1.Value;
                    }
                    if (plan.isKey1.HasValue)
                    {
                        chuqinGroup.group.trainman1.isKey = plan.isKey1.Value;
                    }
                    if (plan.nPostID1.HasValue)
                    {
                        chuqinGroup.group.trainman1.postID = plan.nPostID1.Value;
                    }
                    chuqinGroup.group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    chuqinGroup.group.trainman1.trainmanName = plan.strTrainmanName1;
                    chuqinGroup.group.trainman1.trainmanNumber = plan.strTrainmanNumber1;
                    chuqinGroup.group.trainman2 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman2.ABCD = plan.strABCD2;
                    if (plan.nDriverType2.HasValue)
                    {
                        chuqinGroup.group.trainman2.driverTypeID = plan.nDriverType2.Value;
                    }
                    if (plan.isKey2.HasValue)
                    {
                        chuqinGroup.group.trainman2.isKey = plan.isKey2.Value;
                    }
                    if (plan.nPostID2.HasValue)
                    {
                        chuqinGroup.group.trainman2.postID = plan.nPostID2.Value;
                    }
                    chuqinGroup.group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    chuqinGroup.group.trainman2.trainmanName = plan.strTrainmanName2;
                    chuqinGroup.group.trainman2.trainmanNumber = plan.strTrainmanNumber2;
                    chuqinGroup.group.trainman3 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman3.ABCD = plan.strABCD3;
                    if (plan.nDriverType3.HasValue)
                    {
                        chuqinGroup.group.trainman3.driverTypeID = plan.nDriverType3.Value;
                    }
                    if (plan.isKey3.HasValue)
                    {
                        chuqinGroup.group.trainman3.isKey = plan.isKey3.Value;
                    }
                    if (plan.nPostID3.HasValue)
                    {
                        chuqinGroup.group.trainman3.postID = plan.nPostID3.Value;
                    }
                    chuqinGroup.group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    chuqinGroup.group.trainman3.trainmanName = plan.strTrainmanName3;
                    chuqinGroup.group.trainman3.trainmanNumber = plan.strTrainmanNumber3;
                    chuqinGroup.group.trainman4 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman4.ABCD = plan.strABCD4;
                    if (plan.nDriverType4.HasValue)
                    {
                        chuqinGroup.group.trainman4.driverTypeID = plan.nDriverType4.Value;
                    }
                    if (plan.isKey4.HasValue)
                    {
                        chuqinGroup.group.trainman4.isKey = plan.isKey4.Value;
                    }
                    if (plan.nPostID4.HasValue)
                    {
                        chuqinGroup.group.trainman4.postID = plan.nPostID4.Value;
                    }
                    chuqinGroup.group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    chuqinGroup.group.trainman4.trainmanName = plan.strTrainmanName4;
                    chuqinGroup.group.trainman4.trainmanNumber = plan.strTrainmanNumber4;
                    clientPlan.icCheckResult = plan.strICCheckResult;
                    TF.RunSafty.Plan.TrainPlan trainPlan = new TrainPlan();
                    clientPlan.trainPlan = trainPlan;
                    if (plan.dtCreateTime.HasValue)
                    {
                        trainPlan.createTime = plan.dtCreateTime.Value;
                    }
                    trainPlan.createSiteGUID = plan.strCreateSiteGUID;
                    trainPlan.createSiteName = plan.strCreateSiteName;
                    trainPlan.createUserGUID = plan.strCreateUserGUID;
                    trainPlan.createUserName = plan.strCreateUserName;
                    if (plan.nDragType.HasValue)
                    {
                        trainPlan.dragTypeID = plan.nDragType.ToString();
                    }
                    trainPlan.endStationID = plan.strEndStation;
                    trainPlan.endStationName = plan.strEndStationName;
                    if (plan.nKehuoID.HasValue)
                    {
                        trainPlan.kehuoID = plan.nKehuoID.ToString();
                    }
                    trainPlan.kehuoName = plan.strKehuoName;
                    trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    trainPlan.placeID = plan.strPlaceID;
                    trainPlan.placeName = plan.strPlaceName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    if (plan.nPlanState.HasValue)
                    {
                        trainPlan.planStateID = plan.nPlanState.Value;
                    }
                    trainPlan.planStateName = plan.strPlanStateName;
                    if (plan.nPlanType.HasValue)
                    {
                        trainPlan.planTypeID = plan.nPlanType.ToString();
                    }
                    trainPlan.planTypeName = plan.strPlanTypeName;
                    if (plan.nRemarkType.HasValue)
                    {
                        trainPlan.remarkTypeID = plan.nRemarkType.ToString();
                    }
                    trainPlan.remarkTypeName = plan.strRemarkTypeName;

                    trainPlan.startStationID = plan.strStartStation;
                    trainPlan.startStationName = plan.strStartStationName;
                    trainPlan.dragTypeName = plan.nDragTypeName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    trainPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    trainPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    trainPlan.strTrainPlanGUID = plan.strTrainPlanGUID;
                    if (plan.dtRealStartTime.HasValue)
                    {
                        trainPlan.realStartTime = plan.dtRealStartTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        trainPlan.firstStartTime = plan.dtFirstStartTime.Value;
                    }
                    if (plan.dtChuQinTime.HasValue)
                    {
                        clientPlan.beginWorkTime = plan.dtChuQinTime.Value;
                        clientPlan.trainPlan.kaiCheTime = plan.dtChuQinTime.Value;
                    }
                    else
                    {
                    }
                    if (plan.nTrainmanTypeID.HasValue)
                    {
                        trainPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    }
                    trainPlan.trainNo = plan.strTrainNo;
                    trainPlan.trainNumber = plan.strTrainNumber;
                    trainPlan.trainTypeName = plan.strTrainTypeName;
                    trainPlan.trainmanTypeName = plan.strTrainmanTypeName;

                    if (plan.nVerifyID1.HasValue)
                    {
                        chuqinGroup.verifyID1 = plan.nVerifyID1.Value;
                    }
                    chuqinGroup.testAlcoholInfo1 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo1.picture = "";
                    if (plan.dtTestTime1.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo1.testTime = plan.dtTestTime1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult1.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo1.testAlcoholResult = plan.nDrinkResult1.Value;
                    }

                    if (plan.nVerifyID2.HasValue)
                    {
                        chuqinGroup.verifyID2 = plan.nVerifyID2.Value;
                    }
                    chuqinGroup.testAlcoholInfo2 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo2.picture = "";
                    if (plan.dtTestTime2.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo2.testTime = plan.dtTestTime2.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult2.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo2.testAlcoholResult = plan.nDrinkResult2.Value;
                    }

                    if (plan.nVerifyID3.HasValue)
                    {
                        chuqinGroup.verifyID3 = plan.nVerifyID3.Value;
                    }
                    chuqinGroup.testAlcoholInfo3 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo3.picture = "";
                    if (plan.dtTestTime3.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo3.testTime = plan.dtTestTime3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult3.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo3.testAlcoholResult = plan.nDrinkResult3.Value;
                    }

                    if (plan.nVerifyID4.HasValue)
                    {
                        chuqinGroup.verifyID4 = plan.nVerifyID4.Value;
                    }
                    chuqinGroup.testAlcoholInfo4 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo4.picture = "";
                    if (plan.dtTestTime4.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo4.testTime = plan.dtTestTime4.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult4.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo4.testAlcoholResult = plan.nDrinkResult4.Value;
                    }
                    lPlans.Add(clientPlan);
                }
            }
            return lPlans;
        }

        #endregion

    }
}
