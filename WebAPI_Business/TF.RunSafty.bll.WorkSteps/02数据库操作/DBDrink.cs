using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using TF.CommonUtility;
using ThinkFreely.DBUtility;


namespace TF.RunSafty.WorkSteps
{
   public class DBDrink
    {
       public void AddDrinkInfo(InSubmitDrink InParams, int nWorkTypeID, string strStepName = "RS.STEP.DRINKTEST")
       {

           //为了和之前的程序进行兼容，继续向老的数据库中增加数据，按照原有老的方式
           string chuqinID = "";
           int nDrinkWorkTypeID = 0;
           if (nWorkTypeID == 1)
           {
               this.SubBeginWorkForOld(InParams.TrainPlanGUID, InParams.TrainmanGUID, InParams.DrinkInfo.createTime, InParams.VerifyID, InParams.Remark, InParams, ref chuqinID);
               nDrinkWorkTypeID = 2;//将测酒类型变成出勤测酒
           }
           else if (nWorkTypeID == 2)
           {
               this.SubEndWorkForOld(InParams.TrainPlanGUID, InParams.TrainmanGUID, InParams.DrinkInfo.createTime, InParams.VerifyID, InParams.Remark, ref chuqinID);
               nDrinkWorkTypeID = 3;//将测酒类型变成退勤测酒
           }

           //添加测酒记录
         string  strSql = @"insert into TAB_Drink_Information  
                   ( strGUID,strTrainmanGUID,strTrainmanNumber,strTrainmanName ,dwAlcoholicity ,nDrinkResult,dtCreateTime , 
                   strTrainNo , strTrainNumber , strTrainTypeName , strPlaceID , strPlaceName, strSiteGUID , strSiteName , 
                   strWorkShopGUID , strWorkShopName ,strAreaGUID,strDutyNumber,strDutyName,nVerifyID,nWorkTypeID,strWorkID,strImagePath,strSiteNumber,bLocalAreaTrainman,strDepartmentID,strDepartmentName,nCadreTypeID,strCadreTypeName) 
                  values (@strGUID,@strTrainmanGUID,@strTrainmanNumber,@strTrainmanName,@dwAlcoholicity,@nDrinkResult,@dtCreateTime,
                   @strTrainNo , @strTrainNumber , @strTrainTypeName , @strPlaceID , @strPlaceName, @strSiteGUID , @strSiteName , 
                   @strWorkShopGUID , @strWorkShopName ,@strAreaGUID,@strDutyNumber,@strDutyName,@nVerifyID,@nWorkTypeID,@strWorkID,@strImagePath,@strSiteNumber,@bLocalAreaTrainman,@strDepartmentID,@strDepartmentName,@nCadreTypeID,@strCadreTypeName)";

           int bLocalAreaTrainman;
           if (InParams.DrinkInfo.bLocalAreaTrainman)
           {
               bLocalAreaTrainman = 1;
           }
           else
           {
               bLocalAreaTrainman = 0;
           }
           //职位信息----- 开始----------
           DrinkLogic.DBDrinkLogic dbdl = new DrinkLogic.DBDrinkLogic();
           DrinkLogic.MDDrinkLogic mddl = new DrinkLogic.MDDrinkLogic();
           mddl = dbdl.GetDrinkCadreEntity(InParams.DrinkInfo.strTrainmanNumber);
           if (mddl != null)
           {
               InParams.DrinkInfo.strDepartmentID = mddl.strDepartmentID;
               InParams.DrinkInfo.strDepartmentName = mddl.strDepartmentName;
               InParams.DrinkInfo.nCadreTypeID = mddl.nCadreTypeID;
               InParams.DrinkInfo.strCadreTypeName = mddl.strCadreTypeName;
           }
           else
           {
               InParams.DrinkInfo.strDepartmentID = "";
               InParams.DrinkInfo.strDepartmentName = "";
               InParams.DrinkInfo.nCadreTypeID = "";
               InParams.DrinkInfo.strCadreTypeName = "";
           }
           //职位信息----- 结束----------


           SqlParameter[] sqlParamsDrink = new SqlParameter[] { 
                        new SqlParameter("strGUID",Guid.NewGuid().ToString()),
                        new SqlParameter("strTrainmanGUID",InParams.TrainmanGUID),
                        new SqlParameter("strTrainmanNumber",InParams.DrinkInfo.strTrainmanNumber),
                        new SqlParameter("strTrainmanName",InParams.DrinkInfo.strTrainmanName),
                        new SqlParameter("dwAlcoholicity",InParams.DrinkInfo.dwAlcoholicity),
                        new SqlParameter("strTrainNo",InParams.DrinkInfo.strTrainNo),
                        new SqlParameter("strTrainNumber",InParams.DrinkInfo.strTrainNumber),
                        new SqlParameter("strTrainTypeName",InParams.DrinkInfo.strTrainTypeName),
                        new SqlParameter("strPlaceID",InParams.DrinkInfo.strPlaceID),
                        new SqlParameter("strPlaceName",InParams.DrinkInfo.strPlaceName),
                        new SqlParameter("strSiteGUID",InParams.DrinkInfo.strSiteGUID),
                        new SqlParameter("strSiteName",InParams.DrinkInfo.strSiteName),
                        new SqlParameter("strWorkShopGUID",InParams.DrinkInfo.strWorkShopGUID),
                        new SqlParameter("strWorkShopName",InParams.DrinkInfo.strWorkShopName),
                        new SqlParameter("nDrinkResult",InParams.DrinkInfo.drinkResult),
                        new SqlParameter("strImagePath",InParams.DrinkInfo.imagePath),
                        new SqlParameter("dtCreateTime",InParams.DrinkInfo.createTime),
                        new SqlParameter("strAreaGUID",InParams.DrinkInfo.strAreaGUID),
                        new SqlParameter("strDutyNumber",""),
                        new SqlParameter("strDutyName",""),
                        new SqlParameter("nVerifyID",InParams.VerifyID),
                        new SqlParameter("nWorkTypeID",nDrinkWorkTypeID),
                        new SqlParameter("strWorkID",chuqinID),
                        new SqlParameter("strSiteNumber",""),
                        new SqlParameter("bLocalAreaTrainman",bLocalAreaTrainman),
                        new SqlParameter("strDepartmentID",InParams.DrinkInfo.strDepartmentID),
                        new SqlParameter("strDepartmentName",InParams.DrinkInfo.strDepartmentName),
                        new SqlParameter("nCadreTypeID",InParams.DrinkInfo.nCadreTypeID),
                        new SqlParameter("strCadreTypeName",InParams.DrinkInfo.strCadreTypeName)
                    };
           SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDrink);
           string strTrainmanNumber = "";
           string strTrainmanName = "";
           DataTable dt = GetTrainman(InParams.TrainmanGUID);
           if (dt.Rows.Count > 0)
           {
               strTrainmanNumber = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber"]);
               strTrainmanName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanName"]);
           }

           SaveDrinkStepData(InParams.TrainPlanGUID, InParams.TrainmanGUID, strTrainmanNumber, strTrainmanName, InParams, nWorkTypeID, strStepName);
       }
       private DataTable GetTrainman(string strTrainmanGUID)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select strTrainmanNumber,strTrainmanName from TAB_Org_Trainman where strTrainmanGUID = '" + strTrainmanGUID + "'");
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           return dt;
       }



       private void SaveDrinkStepData(string strTrainPlanGUID, string strTrainmanGUID, string strTrainmanNumber, string strTainmanName, InSubmitDrink InParams, int nWorkTypeID, string strStepName)
       {
           DBStep db = new DBStep();
           #region 出勤步骤结果的实体信息
           StepResult StepResult = new StepResult();
           StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
           StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
           StepResult.dtEndTime = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
           int nStepIndex = db.getIndexOfStep(InParams.DrinkInfo.strWorkShopGUID, "RS.STEP.DRINKTEST", nWorkTypeID);
           StepResult.nStepIndex = nStepIndex;
           StepResult.strStepBrief = nWorkTypeID==1?"出勤测酒":"退勤测酒";
           StepResult.strStepName = strStepName;
           StepResult.strTrainPlanGUID = strTrainPlanGUID;
           StepResult.strTrainmanGUID = InParams.DrinkInfo.trainmanID;
           StepResult.strTrainmanNumber = strTrainmanNumber;
           StepResult.strTrainmanName = InParams.DrinkInfo.strTrainmanName;
           StepResult.nWorkTypeID = nWorkTypeID;
           StepResult.nStepResult = 1;
           #endregion
           #region 出勤步骤索引信息
           DateTime dtStartTime = GetdtStartTime(strTrainPlanGUID);
           string npost = getTmPost(strTrainPlanGUID, strTrainmanNumber);

           List<StepIndex> LStepIndex = new List<StepIndex>();
           StepIndex StepIndex = new StepIndex();
           StepIndex.dtStartTime = ObjectConvertClass.static_ext_date(dtStartTime);
           StepIndex.strFieldName = "nDrinkResult";
           StepIndex.nStepData = int.Parse(InParams.DrinkInfo.drinkResult);
           StepIndex.strTrainmanNumber = strTrainmanNumber;
           StepIndex.strTrainPlanGUID = strTrainPlanGUID;
           StepIndex.nWorkTypeID = nWorkTypeID;
           LStepIndex.Add(StepIndex);

           StepIndex StepIndex2 = new StepIndex();
           StepIndex2.dtStartTime = ObjectConvertClass.static_ext_date(dtStartTime);
           StepIndex2.strTrainmanNumber = strTrainmanNumber;
           StepIndex2.strTrainPlanGUID = strTrainPlanGUID;
           StepIndex2.strFieldName = "LateTimeDiff";
           DateTime dt1 = Convert.ToDateTime(DateTime.Now);
           DateTime dt2 = Convert.ToDateTime(dtStartTime);
           TimeSpan ts = dt1 - dt2;
           StepIndex2.nStepData = Convert.ToInt32(ts.TotalMinutes);
           StepIndex2.nWorkTypeID = nWorkTypeID;
           LStepIndex.Add(StepIndex2);


           StepIndex StepIndex3 = new StepIndex();
           StepIndex3.dtStartTime = ObjectConvertClass.static_ext_date(dtStartTime);
           StepIndex3.strTrainmanNumber = strTrainmanNumber;
           StepIndex3.strTrainPlanGUID = strTrainPlanGUID;
           StepIndex3.strFieldName = "bbtimediff";
           StepIndex3.nWorkTypeID = nWorkTypeID;
           StepIndex3.nStepData = GetdtStartTimeDiff(strTrainPlanGUID, Convert.ToDateTime(InParams.DrinkInfo.createTime));
           LStepIndex.Add(StepIndex3);


           #endregion
           #region 出勤步骤详细信息
           List<StepData> LStepData = new List<StepData>();
           StepData StepData1 = new StepData();
           StepData1.strTrainPlanGUID = strTrainPlanGUID;
           StepData1.strFieldName = "nDrinkResult" + npost;
           StepData1.strStepName = strStepName;
           StepData1.strTrainmanNumber = strTrainmanNumber;
           StepData1.nWorkTypeID = nWorkTypeID;
           StepData1.nStepData = int.Parse(InParams.DrinkInfo.drinkResult);
           LStepData.Add(StepData1);



           StepData StepData2 = new StepData();
           StepData2.strTrainPlanGUID = strTrainPlanGUID;
           StepData2.strFieldName = "LateTimeDiff" + npost;
           StepData2.strTrainmanNumber = strTrainmanNumber;
           StepData2.strStepName = strStepName;
           StepData2.nWorkTypeID = nWorkTypeID;
           StepData2.nStepData = Convert.ToInt32(ts.TotalMinutes);
           LStepData.Add(StepData2);

           StepData StepData3 = new StepData();
           StepData3.strTrainPlanGUID = strTrainPlanGUID;
           StepData3.strFieldName = "dtEventTime" + npost;
           StepData3.strTrainmanNumber = strTrainmanNumber;
           StepData3.strStepName = strStepName;
           StepData3.nWorkTypeID = nWorkTypeID;
           StepData3.dtStepData = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
           LStepData.Add(StepData3);

           StepData StepData4 = new StepData();
           StepData4.strTrainPlanGUID = strTrainPlanGUID;
           StepData4.strFieldName = "picture" + npost;
           StepData4.strTrainmanNumber = strTrainmanNumber;
           StepData4.strStepName = strStepName;
           StepData4.nWorkTypeID = nWorkTypeID;
           StepData4.strStepData = ObjectConvertClass.static_ext_string(InParams.DrinkInfo.imagePath);
           LStepData.Add(StepData4);

           StepData StepData5 = new StepData();
           StepData5.strTrainPlanGUID = strTrainPlanGUID;
           StepData5.strFieldName = "nVerifyID" + npost;
           StepData5.strTrainmanNumber = strTrainmanNumber;
           StepData5.strStepName = strStepName;
           StepData5.nWorkTypeID = nWorkTypeID;
           StepData5.nStepData = ObjectConvertClass.static_ext_int(InParams.VerifyID);
           LStepData.Add(StepData5);

           #endregion
           db.AddStep(LStepIndex, LStepData, StepResult);

           List<trainmanList> trainmanlist = new List<trainmanList>();
           trainmanList trainman = new trainmanList();
           trainman.strTrainmanGUID = strTrainmanGUID;
           trainmanlist.Add(trainman);

           //检测是否是特殊步骤
           if (db.CheckIsSpecialStep(strStepName, InParams.DrinkInfo.strWorkShopGUID, nWorkTypeID))
           {
               if (CheckIsCejiuAll(strTrainPlanGUID, nWorkTypeID))
               {
                   if (nWorkTypeID == 1)
                   {
                       db.UpdateToYiChuQin(strTrainPlanGUID, DateTime.Now, "", "", "", nWorkTypeID);
                   }
                   else if (nWorkTypeID == 2)
                   {
                       DBStep dal = new DBStep();
                       dal.UpdateToYiTuiQin(InParams.TrainPlanGUID, InParams.DrinkInfo.strSiteGUID, DateTime.Now, "", "", "", 2);
                   }
               }
           }

           //检测是否是断网测酒
           if (getSetTime(InParams.DrinkInfo.strWorkShopGUID, nWorkTypeID) >Convert.ToDateTime(InParams.DrinkInfo.createTime))
           {
               if (CheckIsCejiuAll(strTrainPlanGUID, nWorkTypeID))
               {
                   if (nWorkTypeID == 1)
                   {
                       db.UpdateToYiChuQin(strTrainPlanGUID, DateTime.Now, "", "", "", nWorkTypeID);
                   }
                   else if (nWorkTypeID == 2)
                   {
                       DBStep dal = new DBStep();
                       dal.UpdateToYiTuiQin(InParams.TrainPlanGUID, InParams.DrinkInfo.strSiteGUID, DateTime.Now, "", "", "", 2);
                   }
               }
           }
       }

       public DateTime getSetTime(string strWorkShopGUID, int nWorkTypeID)
       {
           StringBuilder strSql1 = new StringBuilder();
           strSql1.Append(" select top 1 BeginTime  from TAB_Plan_Beginwork_FlowSetRecord_Main where  ");
           strSql1.Append("WorkShopGUID='" + strWorkShopGUID + "'  and  FlowType=" + nWorkTypeID + "  order by nID desc ");
           DataTable dt1 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql1.ToString()).Tables[0];
           if (dt1.Rows.Count > 0)
           {
               return Convert.ToDateTime(dt1.Rows[0]["BeginTime"]);
           }
           return DateTime.Now.AddYears(-1);
       }




       //判断所有的司机是否都已经测过酒
       public bool CheckIsCejiuAll(string tpGUID, int nWorkTypeID)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4 ");
           strSql.Append(" FROM TAB_Plan_Trainman where strTrainPlanGUID= '" + tpGUID + "' ");
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           int i = 0;
           if (!string.IsNullOrEmpty(ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID1"])))
               i++;
           if (!string.IsNullOrEmpty(ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID2"])))
               i++;
           if (!string.IsNullOrEmpty(ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID3"])))
               i++;
           if (!string.IsNullOrEmpty(ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID4"])))
               i++;

           StringBuilder strSql2 = new StringBuilder();
           strSql2.Append("select count(*) ");
           strSql2.Append(" from  [TAB_Plan_Beginwork_StepIndex] where strTrainPlanGUID='" + tpGUID + "' and strFieldName='nDrinkResult' and nWorkTypeID=" + nWorkTypeID);
           int k = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()));
           if (k >= i)
               return true;
           else
               return false;
       }




       //通过计划GUID和当前人员的额工号  判断是司机副司机还是学员
       public string getTmPost(string tpGUID, string tn)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select strTrainmanNumber1,strTrainmanNumber2,strTrainmanNumber3,strTrainmanNumber4 ");
           strSql.Append(" FROM VIEW_Plan_Trainman where strTrainPlanGUID= '" + tpGUID + "' ");
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           if (dt.Rows.Count > 0)
           {
               if (ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber1"].ToString()) == tn)
                   return "1";
               else if (ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber2"].ToString()) == tn)
                   return "2";
               else if (ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber3"].ToString()) == tn)
                   return "3";
               else
                   return "4";
           }
           else
           {
               return "0";
           }
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


        #region 为了和之前的程序进行兼容，继续向出勤记录中增加数据
         public void SubBeginWorkForOld(string TrainPlanGUID, string TrainmanGUID, string createTime, int VerifyID, string Remark, InSubmitDrink InParams, ref string chuqinID)
       {
           //添加或者覆盖出勤计划
           string strSql = "select dtCreateTime,strBeginWorkGUID from TAB_Plan_BeginWork where strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID";
           SqlParameter[] sqlParamsCQ = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",TrainPlanGUID),
                    new SqlParameter("strTrainmanGUID",TrainmanGUID)
                };
           DataTable dtCQ = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsCQ).Tables[0];
           bool bReDrink = false;
           if (dtCQ.Rows.Count == 0)
           {
               chuqinID = Guid.NewGuid().ToString();
               strSql = @"insert into TAB_Plan_BeginWork (strBeginWorkGUID,strTrainPlanGUID,strTrainmanGUID,dtCreateTime,nVerifyID,strRemark)
                        values (@strBeginWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtCreateTime,@nVerifyID,@strRemark)";
               SqlParameter[] sqlParamsTemp = new SqlParameter[] { 
                        new SqlParameter("strBeginWorkGUID",chuqinID),
                        new SqlParameter("strTrainPlanGUID",TrainPlanGUID),
                        new SqlParameter("strTrainmanGUID",TrainmanGUID),
                        new SqlParameter("dtCreateTime",createTime),
                        new SqlParameter("nVerifyID",VerifyID),
                        new SqlParameter("strRemark",Remark)
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
                        new SqlParameter("nVerifyID",VerifyID),
                        new SqlParameter("strRemark",Remark)
                    };
               SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTemp);
           }


           //将之前的测酒记录和计划关联取消
           if (bReDrink)
           {
               strSql = "update TAB_Drink_Information  set strWorkID = '' where strWorkID = @strWorkID and strTrainmanGUID = @strTrainmanGUID and nWorkTypeID = 2";
               SqlParameter[] sqlParamsTemp = new SqlParameter[] { 
                        new SqlParameter("strWorkID",chuqinID),
                        new SqlParameter("strTrainmanGUID",TrainmanGUID)
                    };
               SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTemp);
           }

         

           strSql = "update TAB_Plan_Train set dtBeginWorkTime=getdate() where strTrainPlanGUID = @strTrainPlanGUID and dtBeginWorkTime is null ";
           SqlParameter[] sqlParamsCQTime = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",TrainPlanGUID)
                };
           SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsCQTime);
           //修改实际出勤时间


           string strTrainmanNumber = "";
           string strTrainmanName = "";
           DataTable dt = GetTrainman(TrainmanGUID);
           if (dt.Rows.Count > 0)
           {
               strTrainmanNumber = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber"]);
               strTrainmanName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanName"]);
           }

           //向旧的数据库中增加数据
           SaveDrinkStepData(TrainPlanGUID, TrainmanGUID, strTrainmanNumber, strTrainmanName, InParams);

       }
        #endregion



         public void SubEndWorkForOld(string strTrainPlanGUID, string strTrainmanGUID, string createTime, int VerifyID, string Remark, ref string endworkID)
         {
             string sqlText = "select top 1 strEndWorkGUID from TAB_Plan_EndWork where strTrainPlanGUID = '" + strTrainPlanGUID + "' and strTrainmanGUID = '" + strTrainmanGUID + "'";
             object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText);
             endworkID = Guid.NewGuid().ToString();
             if (obj != null)
             {
                 endworkID = Convert.ToString(obj);
             }
             SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("strEndWorkGUID", endworkID),
                    new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
                    new SqlParameter("strTrainmanGUID", strTrainmanGUID),
                    new SqlParameter("dtCreateTime", createTime),
                    new SqlParameter("nVerifyID", VerifyID),
                    new SqlParameter("strRemark", Remark),
                    new SqlParameter("strStationGUID", ""),
                    new SqlParameter("strWorkID", endworkID),
                    new SqlParameter("nWorkTypeID", 3),
                };
             if (obj == null)
             {
                 sqlText = @"insert into TAB_Plan_EndWork(strEndWorkGUID,strTrainPlanGUID,strTrainmanGUID,dtCreateTime,nVerifyID,strStationGUID,strRemark) values (
                               @strEndWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtCreateTime,@nVerifyID,@strStationGUID,@strRemark)";

                 SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
             }
             else
             {
                 sqlText =
                     "update TAB_Plan_EndWork set nVerifyID=@nVerifyID,strRemark=@strRemark where strEndWorkGUID=@strEndWorkGUID";
                 SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
             }


             sqlText = "select  nid  from  TAB_Drink_Information where strWorkID = @strWorkID and nWorkTypeID=@nWorkTypeID and strTrainmanGUID = @strTrainmanGUID";
             DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams).Tables[0];
             if (dt.Rows.Count > 0)
             {
                 sqlText =
                    "update TAB_Drink_Information set strWorkID='' where nid= " + dt.Rows[0]["nid"].ToString();
                 SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
             }

             DateTime dtLastArriveTime = GetLastArrvieTime(strTrainPlanGUID);
             if (dtLastArriveTime < Convert.ToDateTime("2000-01-01"))
             {
                 dtLastArriveTime = DateTime.Now;
             }
             else
             {
                 if (dtLastArriveTime < DateTime.Now)
                 {
                     dtLastArriveTime = DateTime.Now;
                 }
             
             }

             //修改计划终到时间
             sqlText = @"update TAB_Plan_Train set dtLastArriveTime = '" + dtLastArriveTime + "' where strTrainPlanGUID = '" + strTrainPlanGUID + "'";
             SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
             TF.CommonUtility.LogClass.log("修改计划终到时间");
             //修改人员最后一次退勤时间
             sqlText = "update TAB_Org_Trainman set dtLastEndWorkTime='" + DateTime.Now + "',nTrainmanState=2 where strTrainmanGUID = '" + strTrainmanGUID + "'";
             SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
             TF.CommonUtility.LogClass.log("修改人员最后一次退勤时间");


           
         }

         /// <summary>
         /// 获取计划最后到达时间
         /// </summary>
         public DateTime GetLastArrvieTime(String TrainPlanGUID)
         {
             DBEndWork db = new DBEndWork();
             DateTime ArriveTime = DateTime.Now;
             string strArriveTime = "";
             bool isLastArrvieTime = db.getGetLastArrvieTime(TrainPlanGUID, ref strArriveTime);
             ArriveTime = DBToDateTime(strArriveTime, Convert.ToDateTime("1899-01-01"));
             return ArriveTime;
         }

         public static DateTime DBToDateTime(object DBData, DateTime DefaultValue)
         {
             if (DBData == null)
             {
                 return DefaultValue;
             }
             if (DBNull.Value.Equals(DBData))
             {
                 return DefaultValue;
             }
             DateTime dtTemp;
             if (!DateTime.TryParse(DBData.ToString(), out dtTemp))
             {
                 return DefaultValue;
             }
            return dtTemp;
         }



       public class SubTestAlcoholInfo
       {
           public int TestAlcoholResult = 0;
           public int VerfiyID = 0;
           public DateTime? TestTime;
           public string TestPicturePath = "";
           public int Alcoholicity = 0;

       }

       //向旧的数据库中增加数据
       private void SaveDrinkStepData(string strTrainPlanGUID, string strTrainmanGUID, string strTrainmanNumber, string strTainmanName, InSubmitDrink InParams)
       {
           MDPlan_Beginwork_Step step = new MDPlan_Beginwork_Step();
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
          
           step.strStepResultText = Newtonsoft.Json.JsonConvert.SerializeObject(alcoholInfo);
           DBStep db = new DBStep();
           db.AddPlan_Beginwork_Step(step);
       }



    }
}
