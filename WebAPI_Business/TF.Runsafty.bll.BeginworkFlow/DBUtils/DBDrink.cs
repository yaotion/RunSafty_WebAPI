using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using TF.CommonUtility;
using ThinkFreely.DBUtility;
using TF.RunSafty.BeginworkFlow;

namespace TF.RunSafty.BeginworkFlow
{
   public class DBDrink
    {
       public void AddDrinkInfo(LCBeginworkFlow.InSubmit InParams)
       {
           //添加测酒记录
         string  strSql = @"insert into TAB_Drink_Information  
                   ( strGUID,strTrainmanGUID,strTrainmanNumber,strTrainmanName ,dwAlcoholicity ,nDrinkResult,dtCreateTime , 
                   strTrainNo , strTrainNumber , strTrainTypeName , strPlaceID , strPlaceName, strSiteGUID , strSiteName , 
                   strWorkShopGUID , strWorkShopName ,strAreaGUID,strDutyNumber,strDutyName,nVerifyID,nWorkTypeID,strImagePath,strSiteNumber,bLocalAreaTrainman,strDepartmentID,strDepartmentName,nCadreTypeID,strCadreTypeName) 
                  values (@strGUID,@strTrainmanGUID,@strTrainmanNumber,@strTrainmanName,@dwAlcoholicity,@nDrinkResult,@dtCreateTime,
                   @strTrainNo , @strTrainNumber , @strTrainTypeName , @strPlaceID , @strPlaceName, @strSiteGUID , @strSiteName , 
                   @strWorkShopGUID , @strWorkShopName ,@strAreaGUID,@strDutyNumber,@strDutyName,@nVerifyID,@nWorkTypeID,@strImagePath,@strSiteNumber,@bLocalAreaTrainman,@strDepartmentID,@strDepartmentName,@nCadreTypeID,@strCadreTypeName)";

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
                        new SqlParameter("nWorkTypeID",2),
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

           SaveDrinkStepData(InParams.TrainPlanGUID, InParams.TrainmanGUID, strTrainmanNumber, strTrainmanName, InParams);
       }
       private DataTable GetTrainman(string strTrainmanGUID)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select strTrainmanNumber,strTrainmanName from TAB_Org_Trainman where strTrainmanGUID = '" + strTrainmanGUID + "'");
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           return dt;
       }



       private void SaveDrinkStepData(string strTrainPlanGUID, string strTrainmanGUID, string strTrainmanNumber, string strTainmanName, LCBeginworkFlow.InSubmit InParams)
       {  
           LCBeginworkFlow addStep = new LCBeginworkFlow();
           #region 出勤步骤结果的实体信息
           LCBeginworkFlow.StepResult StepResult = new LCBeginworkFlow.StepResult();
           StepResult.dtBeginTime = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
           StepResult.dtCreateTime = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
           StepResult.dtEndTime = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
           StepResult.nStepIndex = 1;
           StepResult.strStepBrief = InParams.DrinkInfo.workTypeID == 1 ? "出勤测酒" : "退勤测酒";
           StepResult.strStepName = InParams.DrinkInfo.workTypeID == 1 ? "RS.STEP.DRINKTEST" : "RS.STEP.TQ.DRINKTEST"; ;
           StepResult.strTrainPlanGUID = strTrainPlanGUID;
           StepResult.strTrainmanGUID = InParams.DrinkInfo.trainmanID;
           StepResult.strTrainmanNumber = strTrainmanNumber;
           StepResult.strTrainmanName = InParams.DrinkInfo.strTrainmanName;
           StepResult.nStepResult = 1;

           StepResult.nWorkTypeID = InParams.DrinkInfo.workTypeID;
           #endregion
           #region 出勤步骤索引信息
           DateTime dtStartTime = GetdtStartTime(strTrainPlanGUID);
           string npost = addStep.getTmPost(strTrainPlanGUID, strTrainmanNumber);

           List<LCBeginworkFlow.StepIndex> LStepIndex = new List<LCBeginworkFlow.StepIndex>();
           LCBeginworkFlow.StepIndex StepIndex = new LCBeginworkFlow.StepIndex();
           StepIndex.dtStartTime = ObjectConvertClass.static_ext_date(dtStartTime);
           StepIndex.strFieldName = "nDrinkResult";
           StepIndex.nStepData = int.Parse(InParams.DrinkInfo.drinkResult);
           StepIndex.strTrainmanNumber = strTrainmanNumber;
           StepIndex.strTrainPlanGUID = strTrainPlanGUID;
           LStepIndex.Add(StepIndex);

           LCBeginworkFlow.StepIndex StepIndex2 = new LCBeginworkFlow.StepIndex();
           StepIndex2.dtStartTime = ObjectConvertClass.static_ext_date(dtStartTime);
           StepIndex2.strTrainmanNumber = strTrainmanNumber;
           StepIndex2.strTrainPlanGUID = strTrainPlanGUID;
           StepIndex2.strFieldName = "LateTimeDiff";
           DateTime dt1 = Convert.ToDateTime(DateTime.Now);
           DateTime dt2 = Convert.ToDateTime(dtStartTime);
           TimeSpan ts = dt1 - dt2;
           StepIndex2.nStepData = Convert.ToInt32(ts.TotalMinutes);
           LStepIndex.Add(StepIndex2);


           LCBeginworkFlow.StepIndex StepIndex3 = new LCBeginworkFlow.StepIndex();
           StepIndex3.dtStartTime = ObjectConvertClass.static_ext_date(dtStartTime);
           StepIndex3.strTrainmanNumber = strTrainmanNumber;
           StepIndex3.strTrainPlanGUID = strTrainPlanGUID;
           StepIndex3.strFieldName = "bbtimediff";
           StepIndex3.nStepData = GetdtStartTimeDiff(strTrainPlanGUID, Convert.ToDateTime(InParams.DrinkInfo.createTime));
           LStepIndex.Add(StepIndex3);


           #endregion
           #region 出勤步骤详细信息
           List<LCBeginworkFlow.StepData> LStepData = new List<LCBeginworkFlow.StepData>();
           LCBeginworkFlow.StepData StepData1 = new LCBeginworkFlow.StepData();
           StepData1.strTrainPlanGUID = strTrainPlanGUID;
           StepData1.strFieldName = "nDrinkResult" + npost;
           StepData1.strStepName = "RS.STEP.DRINKTEST";
           StepData1.strTrainmanNumber = strTrainmanNumber;
           StepData1.nStepData = int.Parse(InParams.DrinkInfo.drinkResult);
           LStepData.Add(StepData1);



           LCBeginworkFlow.StepData StepData2 = new LCBeginworkFlow.StepData();
           StepData2.strTrainPlanGUID = strTrainPlanGUID;
           StepData2.strFieldName = "LateTimeDiff" + npost;
           StepData2.strTrainmanNumber = strTrainmanNumber;
           StepData2.strStepName = "RS.STEP.DRINKTEST";
           StepData2.nStepData = Convert.ToInt32(ts.TotalMinutes);
           LStepData.Add(StepData2);

           LCBeginworkFlow.StepData StepData3 = new LCBeginworkFlow.StepData();
           StepData3.strTrainPlanGUID = strTrainPlanGUID;
           StepData3.strFieldName = "dtEventTime" + npost;
           StepData3.strTrainmanNumber = strTrainmanNumber;
           StepData3.strStepName = "RS.STEP.DRINKTEST";
           StepData3.dtStepData = ObjectConvertClass.static_ext_date(InParams.DrinkInfo.createTime);
           LStepData.Add(StepData3);

           LCBeginworkFlow.StepData StepData4 = new LCBeginworkFlow.StepData();
           StepData4.strTrainPlanGUID = strTrainPlanGUID;
           StepData4.strFieldName = "picture" + npost;
           StepData4.strTrainmanNumber = strTrainmanNumber;
           StepData4.strStepName = "RS.STEP.DRINKTEST";
           StepData4.strStepData = ObjectConvertClass.static_ext_string(InParams.DrinkInfo.imagePath);
           LStepData.Add(StepData4);

           LCBeginworkFlow.StepData StepData5 = new LCBeginworkFlow.StepData();
           StepData5.strTrainPlanGUID = strTrainPlanGUID;
           StepData5.strFieldName = "nVerifyID" + npost;
           StepData5.strTrainmanNumber = strTrainmanNumber;
           StepData5.strStepName = "RS.STEP.DRINKTEST";
           StepData5.nStepData = ObjectConvertClass.static_ext_int(InParams.VerifyID);
           LStepData.Add(StepData5);

           #endregion
           addStep.AddStepInfo(LStepIndex, LStepData, StepResult);
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

    }
}
