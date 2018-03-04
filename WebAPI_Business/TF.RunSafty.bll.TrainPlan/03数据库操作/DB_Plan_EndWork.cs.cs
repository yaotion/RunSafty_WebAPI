using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Data;
using TF.RunSafty.Plan;

namespace TF.RunSafty.Plan
{
    public class DB_Plan_EndWork
    {
        #region 执行退勤
        /// <summary>
        /// 判断是否有出勤计划
        /// </summary>
        /// <param name="strSiteGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <returns></returns>
        private bool HasEndWorkPlan(string strSiteGUID, string strTrainmanGUID)
        {
            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = '{0}') and 
nPlanState >= {1} AND (strTrainmanGUID1 = '{2}' or strTrainmanGUID2 = '{2}'
or strTrainmanGUID3 = '{2}' or strTrainmanGUID4 = '{2}') order by dtStartTime desc", strSiteGUID, (int)TRsPlanState.psBeginWork, strTrainmanGUID);
            TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork bllEndWork = new TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork();
            return bllEndWork.GetModelList(strWhere).Count > 0;
        }
        public bool Endwork(DrinkData drinkdata, DutyInfo dutyUser, string planID, string siteID, int VerifyID, string remark, DateTime dtArrvieTime)
        {
            //判断是否有出勤计划
            if (!HasEndWorkPlan(siteID, drinkdata.trainmanID))
                return false;
            try
            {
                string strGroupGUID = "";
                object oPlaceId = null;
                DataTable dtJiaolu = this.checkTuiQin(planID, drinkdata.trainmanID, siteID, ref strGroupGUID, ref oPlaceId);


                string trainmanJiaoluGUID = dtJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                string trainmanJiaoluName = dtJiaolu.Rows[0]["strTrainmanJiaoluName"].ToString();
                int nTrainmanJiaoluType, nRunType;
                int.TryParse(dtJiaolu.Rows[0]["nJiaoluType"].ToString(), out nTrainmanJiaoluType);
                int.TryParse(dtJiaolu.Rows[0]["nTrainmanRunType"].ToString(), out nRunType);
                //int nRunType = int.Parse(dtJiaolu.Rows[0]["nTrainmanRunType"].ToString());

                SqlParameter[] sqlParamsFind = new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID", planID),
                    new SqlParameter("strTrainmanGUID", drinkdata.trainmanID)
                };
                string sqlText =
                    "select top 1 strEndWorkGUID from TAB_Plan_EndWork where strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID";

                object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind);

                string endworkID = Guid.NewGuid().ToString();
                if (obj != null)
                {
                    endworkID = Convert.ToString(obj);
                }

                #region '添加或修改退勤记录'
                int bLocalAreaTrainman;
                if (drinkdata.bLocalAreaTrainman)
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
                mddl = dbdl.GetDrinkCadreEntity(drinkdata.strTrainmanNumber);
                if (mddl != null)
                {
                    drinkdata.strDepartmentID = mddl.strDepartmentID;
                    drinkdata.strDepartmentName = mddl.strDepartmentName;
                    drinkdata.nCadreTypeID = mddl.nCadreTypeID;
                    drinkdata.strCadreTypeName = mddl.strCadreTypeName;
                }
                else
                {
                    drinkdata.strDepartmentID = "";
                    drinkdata.strDepartmentName = "";
                    drinkdata.nCadreTypeID = "";
                    drinkdata.strCadreTypeName = "";
                }
                //职位信息----- 结束----------

                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("strEndWorkGUID", endworkID),
                    new SqlParameter("strTrainPlanGUID", planID),
                    new SqlParameter("strTrainmanGUID", drinkdata.trainmanID),
                    new SqlParameter("dtCreateTime", drinkdata.createTime),
                    new SqlParameter("nVerifyID", VerifyID),
                    new SqlParameter("strStationGUID", ""),
                    new SqlParameter("strRemark", remark),
                    new SqlParameter("strGroupGUID", strGroupGUID),
                    new SqlParameter("strPlaceID", oPlaceId.ToString()),
                    new SqlParameter("nPlanState", 8),
                    new SqlParameter("NullTrainPlanGUID", ""),

                    new SqlParameter("strGUID", Guid.NewGuid().ToString()),
                    new SqlParameter("strTrainmanJiaoluGUID", trainmanJiaoluGUID),
                    new SqlParameter("nDrinkResult", drinkdata.drinkResult),
                    new SqlParameter("strAreaGUID", drinkdata.strAreaGUID),
                    new SqlParameter("strDutyGUID", dutyUser.dutyUserID),
                    new SqlParameter("bLocalAreaTrainman", bLocalAreaTrainman),



                    new SqlParameter("strTrainmanName", drinkdata.strTrainmanName),
                    new SqlParameter("strTrainmanNumber", drinkdata.strTrainmanNumber),

                    new SqlParameter("strTrainNo", drinkdata.strTrainNo),
                    new SqlParameter("strTrainNumber", drinkdata.strTrainNumber),
                    new SqlParameter("strTrainTypeName", drinkdata.strTrainTypeName),

                    new SqlParameter("strWorkShopGUID", drinkdata.strWorkShopGUID),
                    new SqlParameter("strWorkShopName", drinkdata.strWorkShopName),
                    new SqlParameter("strPlaceIDs", drinkdata.strPlaceID),
                    new SqlParameter("strPlaceName", drinkdata.strPlaceName),
                    new SqlParameter("strSiteGUID", drinkdata.strSiteGUID),
                    new SqlParameter("strSiteName", drinkdata.strSiteName),

                    new SqlParameter("dwAlcoholicity", drinkdata.dwAlcoholicity),
                    new SqlParameter("nWorkTypeIDs", drinkdata.nWorkTypeID),


                    new SqlParameter("strWorkID", endworkID),
                    new SqlParameter("nWorkTypeID", 3),
                    new SqlParameter("strImagePath", drinkdata.imagePath),
                    new SqlParameter("dtLastArriveTime", dtArrvieTime),
                    new SqlParameter("dtLastEndWorkTime", DateTime.Now),
                    new SqlParameter("nTrainmanState", 2),


                    new SqlParameter("strDepartmentID",drinkdata.strDepartmentID),
                    new SqlParameter("strDepartmentName",drinkdata.strDepartmentName),
                    new SqlParameter("nCadreTypeID",drinkdata.nCadreTypeID),
                    new SqlParameter("strCadreTypeName",drinkdata.strCadreTypeName)
                };
                if (obj == null)
                {
                    sqlText =
                        @"insert into TAB_Plan_EndWork(strEndWorkGUID,strTrainPlanGUID,strTrainmanGUID,dtCreateTime,nVerifyID,strStationGUID,strRemark) values (
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

                sqlText = @"insert into TAB_Drink_Information (strGUID,strTrainmanGUID,nDrinkResult,dtCreateTime,strAreaGUID,strDutyGUID,nVerifyID,strWorkID,nWorkTypeID,strImagePath,strTrainmanName,strTrainmanNumber,strTrainNo,strTrainNumber,strTrainTypeName,strWorkShopGUID,strWorkShopName,strPlaceID,strPlaceName,strSiteGUID,strSiteName,dwAlcoholicity,bLocalAreaTrainman,strDepartmentID,strDepartmentName,nCadreTypeID,strCadreTypeName) 
                 values (@strGUID,@strTrainmanGUID,@nDrinkResult,@dtCreateTime,
                 @strAreaGUID,@strDutyGUID,@nVerifyID,@strWorkID,@nWorkTypeIDs,@strImagePath,@strTrainmanName,@strTrainmanNumber,@strTrainNo,@strTrainNumber,@strTrainTypeName,@strWorkShopGUID,@strWorkShopName,@strPlaceIDs,@strPlaceName,@strSiteGUID,@strSiteName,@dwAlcoholicity,@bLocalAreaTrainman,@strDepartmentID,@strDepartmentName,@nCadreTypeID,@strCadreTypeName)";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
                if (string.IsNullOrEmpty(drinkdata.imagePath))
                {
                    TF.CommonUtility.LogClass.log("imagepath为空");
                }
                #endregion

                //将计划设置成已经退勤
                UpdateToYiTuiQing(planID, nTrainmanJiaoluType, nRunType, dtArrvieTime,Convert.ToDateTime(drinkdata.createTime), drinkdata.trainmanID, strGroupGUID, oPlaceId);

                
                bool bFinishedPlan = false;
                string strSql = "select nplanState from tab_plan_train where strTrainPlanGUID = @strTrainPlanGUID";
                object objFinished = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsFind);
                if ((objFinished != null) && !DBNull.Value.Equals(objFinished))
                {
                    objFinished = Convert.ToInt32(objFinished) > 7;
                }
                #region 根据配置将轮乘机组打散
                if (bFinishedPlan)
                {
                    //轮乘机组退勤机组转预备
                    TF.RunSafty.NamePlate.LCGroup.DeleteOrderGroup(trainmanJiaoluGUID, trainmanJiaoluName, strGroupGUID);
                #endregion 根据配置将轮乘机组打散
                }
                #region 发送退勤消息
                ////发送退勤消息
                //TF.RunSafty.Plan.MD.EndworkMsgData msgData = new TF.RunSafty.Plan.MD.EndworkMsgData();
                //msgData.cjjg = Convert.ToInt32(drinkdata.drinkResult);
                //msgData.dtStartTime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(dtStartTime);
                //msgData.dttime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(endworkTime);

                //msgData.jiaoLuGUID = strTrainJiaoluGUID;
                //msgData.jiaoLuName = strTrainJiaoluName;
                //msgData.planGuid = InParams.PlanGUID;
                //msgData.strTrainNo = strTrainNo;
                //msgData.tmGuid = InParams.TmGUID;
                //msgData.tmid = InParams.TmNumber;
                //msgData.tmname = strTrainmanName;
                //msgData.Tmis = 0;
                //ThinkFreely.RunSafty.AttentionMsg msg = msgData.ToMsg();
                //msg.CreatMsg();
                #endregion
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return true;
        }

        #endregion

        public List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> GetTQTZ(string clientGUID, string beginTime, string endTime)
        {
            string strSql = @"select *,'' as strStationName,'' as strStationNumber,strPlaceName,'' as strStationGUID from VIEW_Plan_EndWork  
                where  dtLastArriveTime >=@BeginTime and dtLastArriveTime < @EndTime  and nPlanState > 7 and strTrainJiaoluGUID in  
                (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = @strSiteGUID) order by dtLastArriveTime";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BeginTime",beginTime),
                new SqlParameter("EndTime",endTime),
                new SqlParameter("strSiteGUID",clientGUID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            List<TF.RunSafty.Model.VIEW_Plan_EndWork> vPlans = TQDataTableToList(dt);
            return GetTQPlanList(vPlans);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.VIEW_Plan_EndWork> TQDataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.VIEW_Plan_EndWork> modelList = new List<TF.RunSafty.Model.VIEW_Plan_EndWork>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.VIEW_Plan_EndWork model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = TQDataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public TF.RunSafty.Model.VIEW_Plan_EndWork TQDataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.VIEW_Plan_EndWork model = new TF.RunSafty.Model.VIEW_Plan_EndWork();
            if (row != null)
            {
                if (row["strTrainJiaoluName"] != null)
                {
                    model.strTrainJiaoluName = row["strTrainJiaoluName"].ToString();
                }
                if (row["strTrainmanTypeName"] != null)
                {
                    model.strTrainmanTypeName = row["strTrainmanTypeName"].ToString();
                }
                if (row["strTrainPlanGUID"] != null)
                {
                    model.strTrainPlanGUID = row["strTrainPlanGUID"].ToString();
                }
                if (row["strTrainJiaoluGUID"] != null)
                {
                    model.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
                }
                if (row["strTrainNo"] != null)
                {
                    model.strTrainNo = row["strTrainNo"].ToString();
                }
                if (row["strTrainNumber"] != null)
                {
                    model.strTrainNumber = row["strTrainNumber"].ToString();
                }
                if (row["dtStartTime"] != null && row["dtStartTime"].ToString() != "")
                {
                    DateTime dtStartTime;
                    if (DateTime.TryParse(row["dtStartTime"].ToString(), out dtStartTime))
                    {
                        model.dtStartTime = dtStartTime;
                    }
                }
                if (row["dtChuQinTime"] != null && row["dtChuQinTime"].ToString() != "")
                {
                    DateTime dtChuQinTime;
                    if (DateTime.TryParse(row["dtChuQinTime"].ToString(), out dtChuQinTime))
                    {
                        model.dtChuQinTime = dtChuQinTime;
                    }
                }
                if (row["strStartStation"] != null)
                {
                    model.strStartStation = row["strStartStation"].ToString();
                }
                if (row["strEndStation"] != null)
                {
                    model.strEndStation = row["strEndStation"].ToString();
                }
                if (row["dtCreateTime"] != null && row["dtCreateTime"].ToString() != "")
                {
                    DateTime dtCreateTime;
                    if (DateTime.TryParse(row["dtCreateTime"].ToString(), out dtCreateTime))
                    {
                        model.dtCreateTime = dtCreateTime;
                    }
                }
                if (row["nPlanState"] != null && row["nPlanState"].ToString() != "")
                {
                    model.nPlanState = int.Parse(row["nPlanState"].ToString());
                }
                if (row["strTrainTypeName"] != null)
                {
                    model.strTrainTypeName = row["strTrainTypeName"].ToString();
                }
                if (row["strStartStationName"] != null)
                {
                    model.strStartStationName = row["strStartStationName"].ToString();
                }
                if (row["strEndStationName"] != null)
                {
                    model.strEndStationName = row["strEndStationName"].ToString();
                }
                if (row["nTrainmanTypeID"] != null && row["nTrainmanTypeID"].ToString() != "")
                {
                    model.nTrainmanTypeID = int.Parse(row["nTrainmanTypeID"].ToString());
                }
                if (row["SendPlan"] != null && row["SendPlan"].ToString() != "")
                {
                    model.SendPlan = int.Parse(row["SendPlan"].ToString());
                }
                if (row["nNeedRest"] != null && row["nNeedRest"].ToString() != "")
                {
                    model.nNeedRest = int.Parse(row["nNeedRest"].ToString());
                }
                if (row["dtArriveTime"] != null && row["dtArriveTime"].ToString() != "")
                {
                    model.dtArriveTime = DateTime.Parse(row["dtArriveTime"].ToString());
                }
                if (row["dtCallTime"] != null && row["dtCallTime"].ToString() != "")
                {
                    //model.dtCallTime = DateTime.Parse(row["dtCallTime"].ToString());
                    DateTime dtCallTime;
                    if (DateTime.TryParse(row["dtCallTime"].ToString(), out dtCallTime))
                    {
                        model.dtCallTime = dtCallTime;
                    }
                }
                if (row["nKehuoID"] != null && row["nKehuoID"].ToString() != "")
                {
                    model.nKehuoID = int.Parse(row["nKehuoID"].ToString());
                }
                if (row["dtRealStartTime"] != null && row["dtRealStartTime"].ToString() != "")
                {
                    //model.dtRealStartTime = DateTime.Parse(row["dtRealStartTime"].ToString());
                    DateTime dtRealStartTime;
                    if (DateTime.TryParse(row["dtRealStartTime"].ToString(), out dtRealStartTime))
                    {
                        model.dtRealStartTime = dtRealStartTime;
                    }
                }
                if (row["nPlanType"] != null && row["nPlanType"].ToString() != "")
                {
                    model.nPlanType = int.Parse(row["nPlanType"].ToString());
                }
                if (row["nDragType"] != null && row["nDragType"].ToString() != "")
                {
                    model.nDragType = int.Parse(row["nDragType"].ToString());
                }
                if (row["nRemarkType"] != null && row["nRemarkType"].ToString() != "")
                {
                    model.nRemarkType = int.Parse(row["nRemarkType"].ToString());
                }
                if (row["strRemark"] != null)
                {
                    model.strRemark = row["strRemark"].ToString();
                }
                if (row["strCreateSiteGUID"] != null)
                {
                    model.strCreateSiteGUID = row["strCreateSiteGUID"].ToString();
                }
                if (row["strCreateUserGUID"] != null)
                {
                    model.strCreateUserGUID = row["strCreateUserGUID"].ToString();
                }
                if (row["strCreateUserID"] != null)
                {
                    model.strCreateUserID = row["strCreateUserID"].ToString();
                }
                if (row["strCreateUserName"] != null)
                {
                    model.strCreateUserName = row["strCreateUserName"].ToString();
                }
                if (row["strCreateSiteName"] != null)
                {
                    model.strCreateSiteName = row["strCreateSiteName"].ToString();
                }
                if (row["strTrainmanNumber1"] != null)
                {
                    model.strTrainmanNumber1 = row["strTrainmanNumber1"].ToString();
                }
                if (row["strTrainmanName1"] != null)
                {
                    model.strTrainmanName1 = row["strTrainmanName1"].ToString();
                }
                if (row["nPostID1"] != null && row["nPostID1"].ToString() != "")
                {
                    model.nPostID1 = int.Parse(row["nPostID1"].ToString());
                }
                if (row["strWorkShopGUID1"] != null)
                {
                    model.strWorkShopGUID1 = row["strWorkShopGUID1"].ToString();
                }
                if (row["strTelNumber1"] != null)
                {
                    model.strTelNumber1 = row["strTelNumber1"].ToString();
                }
                if (row["dtLastEndWorkTime1"] != null && row["dtLastEndWorkTime1"].ToString() != "")
                {
                    //model.dtLastEndWorkTime1 = DateTime.Parse(row["dtLastEndWorkTime1"].ToString());
                    DateTime dtLastEndWorkTime1;
                    if (DateTime.TryParse(row["dtLastEndWorkTime1"].ToString(), out dtLastEndWorkTime1))
                    {
                        model.dtLastEndWorkTime1 = dtLastEndWorkTime1;
                    }
                }
                if (row["strTrainmanGUID1"] != null)
                {
                    model.strTrainmanGUID1 = row["strTrainmanGUID1"].ToString();
                }
                if (row["strTrainmanGUID2"] != null)
                {
                    model.strTrainmanGUID2 = row["strTrainmanGUID2"].ToString();
                }
                if (row["strTrainmanNumber2"] != null)
                {
                    model.strTrainmanNumber2 = row["strTrainmanNumber2"].ToString();
                }
                if (row["strTrainmanName2"] != null)
                {
                    model.strTrainmanName2 = row["strTrainmanName2"].ToString();
                }
                if (row["strWorkShopGUID2"] != null)
                {
                    model.strWorkShopGUID2 = row["strWorkShopGUID2"].ToString();
                }
                if (row["strTelNumber2"] != null)
                {
                    model.strTelNumber2 = row["strTelNumber2"].ToString();
                }
                if (row["strTrainmanNumber3"] != null)
                {
                    model.strTrainmanNumber3 = row["strTrainmanNumber3"].ToString();
                }
                if (row["strTrainmanGUID3"] != null)
                {
                    model.strTrainmanGUID3 = row["strTrainmanGUID3"].ToString();
                }
                if (row["dtLastEndWorkTime2"] != null && row["dtLastEndWorkTime2"].ToString() != "")
                {
                    //model.dtLastEndWorkTime2 = DateTime.Parse(row["dtLastEndWorkTime2"].ToString());
                    DateTime dtLastEndWorkTime2;
                    if (DateTime.TryParse(row["dtLastEndWorkTime2"].ToString(), out dtLastEndWorkTime2))
                    {
                        model.dtLastEndWorkTime2 = dtLastEndWorkTime2;
                    }
                }
                if (row["nPostID2"] != null && row["nPostID2"].ToString() != "")
                {
                    model.nPostID2 = int.Parse(row["nPostID2"].ToString());
                }
                if (row["strTrainmanName3"] != null)
                {
                    model.strTrainmanName3 = row["strTrainmanName3"].ToString();
                }
                if (row["nPostID3"] != null && row["nPostID3"].ToString() != "")
                {
                    model.nPostID3 = int.Parse(row["nPostID3"].ToString());
                }
                if (row["strWorkShopGUID3"] != null)
                {
                    model.strWorkShopGUID3 = row["strWorkShopGUID3"].ToString();
                }
                if (row["strTelNumber3"] != null)
                {
                    model.strTelNumber3 = row["strTelNumber3"].ToString();
                }
                if (row["dtLastEndWorkTime3"] != null && row["dtLastEndWorkTime3"].ToString() != "")
                {
                    //model.dtLastEndWorkTime3 = DateTime.Parse(row["dtLastEndWorkTime3"].ToString());
                    DateTime dtLastEndWorkTime3;
                    if (DateTime.TryParse(row["dtLastEndWorkTime3"].ToString(), out dtLastEndWorkTime3))
                    {
                        model.dtLastEndWorkTime3 = dtLastEndWorkTime3;
                    }
                }
                if (row["strDutyGUID"] != null)
                {
                    model.strDutyGUID = row["strDutyGUID"].ToString();
                }
                if (row["strGroupGUID"] != null)
                {
                    model.strGroupGUID = row["strGroupGUID"].ToString();
                }
                if (row["strDutySiteGUID"] != null)
                {
                    model.strDutySiteGUID = row["strDutySiteGUID"].ToString();
                }
                if (row["dtTrainmanCreateTime"] != null && row["dtTrainmanCreateTime"].ToString() != "")
                {
                    DateTime dtTrainmanCreateTime;
                    if (DateTime.TryParse(row["dtTrainmanCreateTime"].ToString(), out dtTrainmanCreateTime))
                    {
                        model.dtTrainmanCreateTime = dtTrainmanCreateTime;
                    }
                }
                if (row["dtFirstStartTime"] != null && row["dtFirstStartTime"].ToString() != "")
                {
                    //model.dtFirstStartTime = DateTime.Parse(row["dtFirstStartTime"].ToString());
                    DateTime dtFirstStartTime;
                    if (DateTime.TryParse(row["dtFirstStartTime"].ToString(), out dtFirstStartTime))
                    {
                        model.dtFirstStartTime = dtFirstStartTime;
                    }
                }
                if (row["nid"] != null && row["nid"].ToString() != "")
                {
                    model.nid = int.Parse(row["nid"].ToString());
                }
                if (row["nDragTypeName"] != null)
                {
                    model.nDragTypeName = row["nDragTypeName"].ToString();
                }
                if (row["strKehuoName"] != null)
                {
                    model.strKehuoName = row["strKehuoName"].ToString();
                }
                if (row["nTrainmanState1"] != null && row["nTrainmanState1"].ToString() != "")
                {
                    model.nTrainmanState1 = int.Parse(row["nTrainmanState1"].ToString());
                }
                if (row["nTrainmanState2"] != null && row["nTrainmanState2"].ToString() != "")
                {
                    model.nTrainmanState2 = int.Parse(row["nTrainmanState2"].ToString());
                }
                if (row["nTrainmanState3"] != null && row["nTrainmanState3"].ToString() != "")
                {
                    model.nTrainmanState3 = int.Parse(row["nTrainmanState3"].ToString());
                }
                if (row["strBak1"] != null)
                {
                    model.strBak1 = row["strBak1"].ToString();
                }
                if (row["dtLastArriveTime"] != null && row["dtLastArriveTime"].ToString() != "")
                {
                    //model.dtLastArriveTime = DateTime.Parse(row["dtLastArriveTime"].ToString());
                    DateTime dtLastArriveTime;
                    if (DateTime.TryParse(row["dtLastArriveTime"].ToString(), out dtLastArriveTime))
                    {
                        model.dtLastArriveTime = dtLastArriveTime;
                    }
                }
                if (row["strMainPlanGUID"] != null)
                {
                    model.strMainPlanGUID = row["strMainPlanGUID"].ToString();
                }
                if (row["strTrainmanName4"] != null)
                {
                    model.strTrainmanName4 = row["strTrainmanName4"].ToString();
                }
                if (row["strTrainmanNumber4"] != null)
                {
                    model.strTrainmanNumber4 = row["strTrainmanNumber4"].ToString();
                }
                if (row["nPostID4"] != null && row["nPostID4"].ToString() != "")
                {
                    model.nPostID4 = int.Parse(row["nPostID4"].ToString());
                }
                if (row["strTelNumber4"] != null)
                {
                    model.strTelNumber4 = row["strTelNumber4"].ToString();
                }
                if (row["dtLastEndWorkTime4"] != null && row["dtLastEndWorkTime4"].ToString() != "")
                {
                    //model.dtLastEndWorkTime4 = DateTime.Parse(row["dtLastEndWorkTime4"].ToString());
                    DateTime dtLastEndWorkTime4;
                    if (DateTime.TryParse(row["dtLastEndWorkTime4"].ToString(), out dtLastEndWorkTime4))
                    {
                        model.dtLastEndWorkTime4 = dtLastEndWorkTime4;
                    }
                }
                if (row["nTrainmanState"] != null && row["nTrainmanState"].ToString() != "")
                {
                    model.nTrainmanState = int.Parse(row["nTrainmanState"].ToString());
                }
                if (row["strTrainmanGUID4"] != null)
                {
                    model.strTrainmanGUID4 = row["strTrainmanGUID4"].ToString();
                }
                if (row["strRemarkTypeName"] != null)
                {
                    model.strRemarkTypeName = row["strRemarkTypeName"].ToString();
                }
                if (row["strPlanTypeName"] != null)
                {
                    model.strPlanTypeName = row["strPlanTypeName"].ToString();
                }
                if (row["strPlaceID"] != null)
                {
                    model.strPlaceID = row["strPlaceID"].ToString();
                }
                if (row["strStateName"] != null)
                {
                    model.strStateName = row["strStateName"].ToString();
                }
                if (row["nDriverType1"] != null && row["nDriverType1"].ToString() != "")
                {
                    model.nDriverType1 = int.Parse(row["nDriverType1"].ToString());
                }
                if (row["nDriverType2"] != null && row["nDriverType2"].ToString() != "")
                {
                    model.nDriverType2 = int.Parse(row["nDriverType2"].ToString());
                }
                if (row["nDriverType3"] != null && row["nDriverType3"].ToString() != "")
                {
                    model.nDriverType3 = int.Parse(row["nDriverType3"].ToString());
                }
                if (row["nDriverType4"] != null && row["nDriverType4"].ToString() != "")
                {
                    model.nDriverType4 = int.Parse(row["nDriverType4"].ToString());
                }
                if (row["strABCD1"] != null)
                {
                    model.strABCD1 = row["strABCD1"].ToString();
                }
                if (row["strABCD2"] != null)
                {
                    model.strABCD2 = row["strABCD2"].ToString();
                }
                if (row["strABCD3"] != null)
                {
                    model.strABCD3 = row["strABCD3"].ToString();
                }
                if (row["strABCD4"] != null)
                {
                    model.strABCD4 = row["strABCD4"].ToString();
                }
                if (row["isKey1"] != null && row["isKey1"].ToString() != "")
                {
                    model.isKey1 = int.Parse(row["isKey1"].ToString());
                }
                if (row["isKey2"] != null && row["isKey2"].ToString() != "")
                {
                    model.isKey2 = int.Parse(row["isKey2"].ToString());
                }
                if (row["isKey3"] != null && row["isKey3"].ToString() != "")
                {
                    model.isKey3 = int.Parse(row["isKey3"].ToString());
                }
                if (row["isKey4"] != null && row["isKey4"].ToString() != "")
                {
                    model.isKey4 = int.Parse(row["isKey4"].ToString());
                }
                if (row["strPlanStateName"] != null)
                {
                    model.strPlanStateName = row["strPlanStateName"].ToString();
                }
                if (row["strStationName"] != null)
                {
                    model.strStationName = row["strStationName"].ToString();
                }
                if (row["strStationNumber"] != null && row["strStationNumber"].ToString() != "")
                {
                    model.strStationNumber = int.Parse(row["strStationNumber"].ToString());
                }
                if (row["strPlaceName"] != null)
                {
                    model.strPlaceName = row["strPlaceName"].ToString();
                }
                if (row["strStationGUID"] != null)
                {
                    model.strStationGUID = row["strStationGUID"].ToString();
                }
                if (row["dtTestTime1"] != null && row["dtTestTime1"].ToString() != "")
                {
                    DateTime dtTestTime1;
                    if (DateTime.TryParse(row["dtTestTime1"].ToString(), out dtTestTime1))
                    {
                        model.dtTestTime1 = dtTestTime1;
                    }
                }
                if (row["nVerifyID1"] != null && row["nVerifyID1"].ToString() != "")
                {
                    model.nVerifyID1 = int.Parse(row["nVerifyID1"].ToString());
                }
                if (row["nDrinkResult1"] != null && row["nDrinkResult1"].ToString() != "")
                {
                    model.nDrinkResult1 = int.Parse(row["nDrinkResult1"].ToString());
                }
                if (row["dtTestTime2"] != null && row["dtTestTime2"].ToString() != "")
                {
                    DateTime dtTestTime2;
                    if (DateTime.TryParse(row["dtTestTime2"].ToString(), out dtTestTime2))
                    {
                        model.dtTestTime2 = dtTestTime2;
                    }
                }
                if (row["nVerifyID2"] != null && row["nVerifyID2"].ToString() != "")
                {
                    model.nVerifyID2 = int.Parse(row["nVerifyID2"].ToString());
                }
                if (row["nDrinkResult2"] != null && row["nDrinkResult2"].ToString() != "")
                {
                    model.nDrinkResult2 = int.Parse(row["nDrinkResult2"].ToString());
                }
                if (row["dtTestTime3"] != null && row["dtTestTime3"].ToString() != "")
                {
                    DateTime dtTestTime3;
                    if (DateTime.TryParse(row["dtTestTime3"].ToString(), out dtTestTime3))
                    {
                        model.dtTestTime3 = dtTestTime3;
                    }
                }
                if (row["nVerifyID3"] != null && row["nVerifyID3"].ToString() != "")
                {
                    model.nVerifyID3 = int.Parse(row["nVerifyID3"].ToString());
                }
                if (row["nDrinkResult3"] != null && row["nDrinkResult3"].ToString() != "")
                {
                    model.nDrinkResult3 = int.Parse(row["nDrinkResult3"].ToString());
                }
                if (row["DrinkImage3"] != null && row["DrinkImage3"].ToString() != "")
                {
                    model.DrinkImage3 = (byte[])row["DrinkImage3"];
                }
                if (row["DrinkImage1"] != null && row["DrinkImage1"].ToString() != "")
                {
                    model.DrinkImage1 = (byte[])row["DrinkImage1"];
                }
                if (row["DrinkImage2"] != null && row["DrinkImage2"].ToString() != "")
                {
                    model.DrinkImage2 = (byte[])row["DrinkImage2"];
                }
                if (row["strEndWorkGUID1"] != null)
                {
                    model.strEndWorkGUID1 = row["strEndWorkGUID1"].ToString();
                }
                if (row["strEndWorkGUID2"] != null)
                {
                    model.strEndWorkGUID2 = row["strEndWorkGUID2"].ToString();
                }
                if (row["strEndWorkGUID3"] != null)
                {
                    model.strEndWorkGUID3 = row["strEndWorkGUID3"].ToString();
                }
                if (row["strEndWorkGUID4"] != null)
                {
                    model.strEndWorkGUID4 = row["strEndWorkGUID4"].ToString();
                }
                if (row["dtTestTime4"] != null && row["dtTestTime4"].ToString() != "")
                {
                    DateTime dtTestTime4;
                    if (DateTime.TryParse(row["dtTestTime4"].ToString(), out dtTestTime4))
                    {
                        model.dtTestTime4 = dtTestTime4;
                    }
                }
                if (row["nVerifyID4"] != null && row["nVerifyID4"].ToString() != "")
                {
                    model.nVerifyID4 = int.Parse(row["nVerifyID4"].ToString());
                }
                if (row["DrinkImage4"] != null && row["DrinkImage4"].ToString() != "")
                {
                    model.DrinkImage4 = (byte[])row["DrinkImage4"];
                }
                if (row["nDrinkResult4"] != null && row["nDrinkResult4"].ToString() != "")
                {
                    model.nDrinkResult4 = int.Parse(row["nDrinkResult4"].ToString());
                }
            }
            return model;
        }
        private List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> GetTQPlanList(List<TF.RunSafty.Model.VIEW_Plan_EndWork> vPlans)
        {
            List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> lPlans = new List<Model.InterfaceModel.mTuiqinPlansOfSite>();
            TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_EndWork plan in vPlans)
                {
                    clientPlan = new TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite();

                    TF.RunSafty.Model.InterfaceModel.TuiqinGroup tuiqinGroup = new TF.RunSafty.Model.InterfaceModel.TuiqinGroup();
                    clientPlan.tuiqinGroup = tuiqinGroup;
                    tuiqinGroup.group = new TF.RunSafty.Model.InterfaceModel.NameGroup();
                    TF.RunSafty.Model.InterfaceModel.ChuqinPlace cPlace = new TF.RunSafty.Model.InterfaceModel.ChuqinPlace();
                    tuiqinGroup.group.place = cPlace;
                    cPlace.placeID = plan.strPlaceID;
                    cPlace.placeName = plan.strPlaceName;
                    tuiqinGroup.group.groupID = plan.strGroupGUID;
                    tuiqinGroup.group.station = new TF.RunSafty.Model.InterfaceModel.Station();
                    tuiqinGroup.group.station.stationID = plan.strStationGUID;
                    tuiqinGroup.group.station.stationName = plan.strStationName;
                    tuiqinGroup.group.station.stationNumber = plan.strStationNumber.ToString();
                    tuiqinGroup.group.trainman1 = new TF.RunSafty.Model.InterfaceModel.Trainman();
                    tuiqinGroup.group.trainman1.ABCD = plan.strABCD1;
                    if (plan.nDriverType1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.driverTypeID = plan.nDriverType1.Value;
                    }
                    if (plan.isKey1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.isKey = plan.isKey1.Value;
                    }
                    if (plan.nPostID1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.postID = plan.nPostID1.Value;
                    }
                    tuiqinGroup.group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    tuiqinGroup.group.trainman1.trainmanName = plan.strTrainmanName1;
                    tuiqinGroup.group.trainman1.trainmanNumber = plan.strTrainmanNumber1;
                    tuiqinGroup.group.trainman2 = new TF.RunSafty.Model.InterfaceModel.Trainman();
                    tuiqinGroup.group.trainman2.ABCD = plan.strABCD2;
                    if (plan.nDriverType2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.driverTypeID = plan.nDriverType2.Value;
                    }
                    if (plan.isKey2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.isKey = plan.isKey2.Value;
                    }
                    if (plan.nPostID2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.postID = plan.nPostID2.Value;
                    }
                    tuiqinGroup.group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    tuiqinGroup.group.trainman2.trainmanName = plan.strTrainmanName2;
                    tuiqinGroup.group.trainman2.trainmanNumber = plan.strTrainmanNumber2;
                    tuiqinGroup.group.trainman3 = new TF.RunSafty.Model.InterfaceModel.Trainman();
                    tuiqinGroup.group.trainman3.ABCD = plan.strABCD3;
                    if (plan.nDriverType3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.driverTypeID = plan.nDriverType3.Value;
                    }
                    if (plan.isKey3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.isKey = plan.isKey3.Value;
                    }
                    if (plan.nPostID3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.postID = plan.nPostID3.Value;
                    }
                    tuiqinGroup.group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    tuiqinGroup.group.trainman3.trainmanName = plan.strTrainmanName3;
                    tuiqinGroup.group.trainman3.trainmanNumber = plan.strTrainmanNumber3;
                    tuiqinGroup.group.trainman4 = new TF.RunSafty.Model.InterfaceModel.Trainman();
                    tuiqinGroup.group.trainman4.ABCD = plan.strABCD4;
                    if (plan.nDriverType4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.driverTypeID = plan.nDriverType4.Value;
                    }
                    if (plan.isKey4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.isKey = plan.isKey4.Value;
                    }
                    if (plan.nPostID4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.postID = plan.nPostID4.Value;
                    }
                    tuiqinGroup.group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    tuiqinGroup.group.trainman4.trainmanName = plan.strTrainmanName4;
                    tuiqinGroup.group.trainman4.trainmanNumber = plan.strTrainmanNumber4;
                    //

                    //clientPlan.icCheckResult = plan.strICCheckResult;
                    TF.RunSafty.Model.InterfaceModel.TrainPlan trainPlan = new TF.RunSafty.Model.InterfaceModel.TrainPlan();
                    clientPlan.trainPlan = trainPlan;
                    if (plan.dtCreateTime.HasValue)
                    {
                        trainPlan.createTime = plan.dtCreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
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

                    trainPlan.kehuoID = plan.nKehuoID.ToString();
                    trainPlan.kehuoName = plan.strKehuoName;
                    trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    trainPlan.placeID = plan.strPlaceID;
                    trainPlan.placeName = plan.strPlaceName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    trainPlan.planStateID = plan.nPlanState.ToString();
                    trainPlan.planStateName = plan.strPlanStateName;
                    trainPlan.planTypeID = plan.nPlanType.ToString();
                    trainPlan.planTypeName = plan.strPlanTypeName;
                    trainPlan.remarkTypeID = plan.nRemarkType.ToString();
                    trainPlan.remarkTypeName = plan.strRemarkTypeName;
                    trainPlan.startStationID = plan.strStartStation;
                    trainPlan.startStationName = plan.strStartStationName;
                    trainPlan.dragTypeName = plan.nDragTypeName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    trainPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    trainPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    trainPlan.strTrainPlanGUID = plan.strTrainPlanGUID;
                    trainPlan.planStateName = plan.strPlanStateName;
                    if (plan.dtLastArriveTime.HasValue)
                    {
                        trainPlan.lastArriveTime = plan.dtLastArriveTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtArriveTime.HasValue)
                    {
                        trainPlan.dtArriveTime = plan.dtArriveTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        trainPlan.startTime = plan.dtStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtRealStartTime.HasValue)
                    {
                        trainPlan.realStartTime = plan.dtRealStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        trainPlan.startTime = plan.dtStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        trainPlan.firstStartTime = plan.dtFirstStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtChuQinTime.HasValue)
                    {
                        clientPlan.beginWorkTime = plan.dtChuQinTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        clientPlan.trainPlan.kaiCheTime = plan.dtChuQinTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        clientPlan.beginWorkTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    trainPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    trainPlan.trainNo = plan.strTrainNo;
                    trainPlan.trainNumber = plan.strTrainNumber;
                    trainPlan.trainTypeName = plan.strTrainTypeName;
                    trainPlan.trainmanTypeName = plan.strTrainmanTypeName;
                    if (plan.nVerifyID1.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID1 = plan.nVerifyID1.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo1 = new TF.RunSafty.Model.InterfaceModel.TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo1.picture = "";
                    if (plan.dtTestTime1.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo1.testTime = plan.dtTestTime1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult1.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo1.testAlcoholResult = plan.nDrinkResult1.Value;
                    }

                    if (plan.nVerifyID2.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID2 = plan.nVerifyID2.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo2 = new TF.RunSafty.Model.InterfaceModel.TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo2.picture = "";
                    if (plan.dtTestTime2.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo2.testTime = plan.dtTestTime2.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult2.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo2.testAlcoholResult = plan.nDrinkResult2.Value;
                    }


                    if (plan.nVerifyID3.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID3 = plan.nVerifyID3.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo3 = new TF.RunSafty.Model.InterfaceModel.TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo3.picture = "";
                    if (plan.dtTestTime3.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo3.testTime = plan.dtTestTime3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult3.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo3.testAlcoholResult = plan.nDrinkResult3.Value;
                    }

                    if (plan.nVerifyID4.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID4 = plan.nVerifyID4.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo4 = new TF.RunSafty.Model.InterfaceModel.TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo4.picture = "";
                    if (plan.dtTestTime4.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo4.testTime = plan.dtTestTime4.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult4.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo4.testAlcoholResult = plan.nDrinkResult4.Value;
                    }
                    lPlans.Add(clientPlan);
                }
            }
            return lPlans;
        }
        #region 关联退勤记录
        public void UnionTuiQin(LCEndwork.InUnionTuiQin InParams)
        {
            if (InParams.DrinkGUID == "")
            {
                throw new Exception("不能关联不存在的测酒记录");
            }
            #region 获取计划信息
            string strSqlPlan = "select strTrainJiaoluGUID,strTrainJiaoluName,strTrainNo,dtStartTime,dtLastArriveTime from view_Plan_Train where strTrainPlanGUID=@strTrainPlanGUID";
            SqlParameter[] sqlParamsPlan = new SqlParameter[]{
                new SqlParameter("strTrainPlanGUID",InParams.PlanGUID)
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
            #endregion

            #region 获取人员信息
            string strSqlTM = "select strTrainmanNumber,strTrainmanName  from TAB_Org_Trainman where strTrainmanGUID=@strTrainmanGUID";
            SqlParameter[] sqlParamsTM = new SqlParameter[]{
                new SqlParameter("strTrainmanGUID",InParams.TmGUID)
            };
            DataTable dtTM = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlTM, sqlParamsTM).Tables[0];
            if (dtTM.Rows.Count == 0)
            {
                throw new Exception("指定的人员信息不存在,关联退勤记录失败");

            }
            string strTrainmanName = dtTM.Rows[0]["strTrainmanName"].ToString();
            #endregion


            //验证能否被执行退勤
            string strGroupGUID = "";
            object oPlaceId = null;
            DataTable dtJiaolu = this.checkTuiQin(InParams.PlanGUID, InParams.TmGUID, InParams.SiteGUID, ref strGroupGUID, ref oPlaceId);

            int nTrainmanJiaoluType, nRunType;
            int.TryParse(dtJiaolu.Rows[0]["nJiaoluType"].ToString(), out nTrainmanJiaoluType);
            int.TryParse(dtJiaolu.Rows[0]["nTrainmanRunType"].ToString(), out nRunType);
            string trainmanJiaoluGUID = dtJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
            string trainmanJiaoluName = dtJiaolu.Rows[0]["strTrainmanJiaoluName"].ToString();
            int drinkResult = 0;
            //获取验证方式
            string strSqlDrink = "select * from TAB_Drink_Information where strGUID = @strGUID";
            SqlParameter[] sqlParamsCQDr = new SqlParameter[]{
                    new SqlParameter("strGUID",InParams.DrinkGUID)
                };
            DataTable dtTQDrink = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlDrink, sqlParamsCQDr).Tables[0];
            if (dtTQDrink.Rows.Count == 0)
            {
                 throw new Exception("指定的测酒信息不存在,关联退勤记录失败");
            }
            int nVerifyID = 1;
             object oVerifyID = dtTQDrink.Rows[0]["nVerifyID"];
                if (oVerifyID != null)
                {
                    nVerifyID = Convert.ToInt32(oVerifyID);
                }
            drinkResult = Convert.ToInt32(dtTQDrink.Rows[0]["nDrinkResult"]);
            DateTime drinkTime = Convert.ToDateTime(dtTQDrink.Rows[0]["dtCreateTime"]);
            //如果以前没退勤时间则以本次的测酒时间为退勤时间，如果以前有退勤时间且比这次的大则以本次否则以之前的为准
            DateTime  endworkTime = Convert.ToDateTime(dtTQDrink.Rows[0]["dtCreateTime"]);
        
            if ((dtPlan.Rows[0]["dtLastArriveTime"] != null) && (!DBNull.Value.Equals(dtPlan.Rows[0]["dtLastArriveTime"])))
            {
                DateTime lastArriveTime = Convert.ToDateTime(dtPlan.Rows[0]["dtLastArriveTime"]);
                if (endworkTime > lastArriveTime)
                {
                    endworkTime = lastArriveTime;
                }
            }
            //添加或者覆盖退勤计划
            string strSql = "select dtCreateTime,strEndWorkGUID from TAB_Plan_EndWork where strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID";
            SqlParameter[] sqlParamsCQ = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",InParams.PlanGUID),
                    new SqlParameter("strTrainmanGUID",InParams.TmGUID)
                };
            DataTable dtTQ = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsCQ).Tables[0];
            string TuQinID = "";
            if (dtTQ.Rows.Count == 0)
            {
                
                TuQinID = Guid.NewGuid().ToString();
                strSql = @"insert into TAB_Plan_EndWork (strEndWorkGUID,strTrainPlanGUID,strTrainmanGUID,dtCreateTime,nVerifyID,strStationGUID,strRemark)
                        values (@strEndWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtCreateTime,@nVerifyID,@strStationGUID,@strRemark)";
                SqlParameter[] sqlParamsTemp = new SqlParameter[] { 
                        new SqlParameter("strEndWorkGUID",TuQinID),
                        new SqlParameter("strTrainPlanGUID",InParams.PlanGUID),
                        new SqlParameter("strTrainmanGUID",InParams.TmGUID),
                        new SqlParameter("dtCreateTime",DateTime.Now),
                        new SqlParameter("nVerifyID",nVerifyID),
                        new SqlParameter("strStationGUID",""),
                        new SqlParameter("strRemark","")
                    };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTemp);
            }
            else
            {
                TuQinID = dtTQ.Rows[0]["strEndWorkGUID"].ToString();
                strSql = @"update TAB_Plan_EndWork set nVerifyID=@nVerifyID,strRemark=@strRemark where strEndWorkGUID=@strEndWorkGUID";
                SqlParameter[] sqlParamsTemp = new SqlParameter[] { 
                        new SqlParameter("strEndWorkGUID",TuQinID),                       
                        new SqlParameter("nVerifyID",nVerifyID),
                        new SqlParameter("strRemark","")
                    };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTemp);

                strSql = @"update TAB_Drink_Information set strWorkID=''  where strGUID = @strGUID";
                SqlParameter[] sqlParamsD = new SqlParameter[] { 
                        new SqlParameter("strGUID",InParams.DrinkGUID)             
                    };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsD);
            }

            //修改测酒记录
            strSql = @"update TAB_Drink_Information set strWorkID=@strWorkID , nWorkTypeID=3  where strGUID = @strGUID";
            SqlParameter[] sqlParamsDrink = new SqlParameter[] { 
                        new SqlParameter("strGUID",InParams.DrinkGUID),               
                        new SqlParameter("strWorkID",TuQinID),
                    };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsDrink);

            //修改计划为已经退勤
            UpdateToYiTuiQing(InParams.PlanGUID, nTrainmanJiaoluType, nRunType, endworkTime, drinkTime, InParams.TmGUID, strGroupGUID, oPlaceId);
           
            bool bFinishedPlan = false;
            strSql = "select nplanState from tab_plan_train where strTrainPlanGUID = @strTrainPlanGUID";
            TF.CommonUtility.LogClass.log("检查计划是否完成退勤");
            object objFinished = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsCQ);
            if ((objFinished != null) && !DBNull.Value.Equals(objFinished))
            {
                bFinishedPlan = Convert.ToInt32(objFinished) > 7;
            }
            TF.CommonUtility.LogClass.log(string.Format("退勤计划状态为:{0}",objFinished));
            #region 根据配置将轮乘机组打散
            if (bFinishedPlan)
            {                
                //轮乘机组退勤机组转预备
                if ((nTrainmanJiaoluType == 3))
                {
                    TF.RunSafty.NamePlate.LCGroup.DeleteOrderGroup(trainmanJiaoluGUID, trainmanJiaoluName, strGroupGUID);                                  
                }
            #endregion 根据配置将轮乘机组打散
            }
            #region 发送退勤消息
            //发送退勤消息
            TF.RunSafty.Plan.MD.EndworkMsgData msgData = new TF.RunSafty.Plan.MD.EndworkMsgData();
            msgData.cjjg = drinkResult;
            msgData.dtStartTime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(dtStartTime);
            msgData.dttime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(endworkTime);

            msgData.jiaoLuGUID = strTrainJiaoluGUID;
            msgData.jiaoLuName = strTrainJiaoluName;
            msgData.planGuid = InParams.PlanGUID;
            msgData.strTrainNo = strTrainNo;
            msgData.tmGuid = InParams.TmGUID;
            msgData.tmid = InParams.TmNumber;
            msgData.tmname = strTrainmanName;      
            msgData.Tmis = 0;
            ThinkFreely.RunSafty.AttentionMsg msg = msgData.ToMsg();
            msg.CreatMsg();
            #endregion
        }
        #endregion

        //判断是否能够执行退勤
        public DataTable checkTuiQin(string planID, string trainmanID, string siteID, ref string strGroupGUID, ref object oPlaceId)
        {

            //根据planid获取交路，然后根据客户端编号和交路获取退勤端的第一个出勤点
            string strSql =
                "select strTrainJiaoluGUID from tab_plan_train where strTrainPlanGUID=@strTrainPlanGUID    ";
            SqlParameter[] sqlParamsFind = new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID", planID),
                    new SqlParameter("strTrainmanGUID", trainmanID)
                };
            SqlParameter[] sqlParameters;
            object oTrainjiaolu = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql,
                sqlParamsFind);
            if (oTrainjiaolu != null)
            {
                strSql =
                    "select top 1 strPlaceID from TAB_Base_Site_DutyPlace where strSiteGUID=@strSiteGUID and strTrainJiaoluGUID=strTrainJiaoluGUID";
                sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("strSiteGUID", siteID),
                        new SqlParameter("strTrainJiaoluGUID", oTrainjiaolu.ToString()),
                    };
                oPlaceId = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
            }
            if (oPlaceId == null)
            {
                throw new Exception("找不到该退勤端所管理的出勤点，请在网站中配置。");
            }
            string sqlText = "select strGroupGUID from TAB_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
            object obj =
                Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText,
                    sqlParamsFind));
            if (obj != null)
            {
                strGroupGUID = Convert.ToString(obj);
            }

            sqlText =
                @"select nJiaoluType,strTrainmanJiaoluGUID,nTrainmanRunType,strTrainmanJiaoluName from VIEW_Nameplate_TrainmanInJiaolu_All 
             where strTrainmanGUID = @strTrainmanGUID";
            DataTable dtJiaolu =
                SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind).Tables[0];
            if (dtJiaolu.Rows.Count == 0)
            {
                throw new Exception("该机组没有处于任何人员交路中");
            }
            return dtJiaolu;
        }

        //修改计划为已经退勤
        public bool UpdateToYiTuiQing(string planID, int nTrainmanJiaoluType, int nRunType, DateTime dtArrvieTime,DateTime DrinkTime, string trainmanID, string strGroupGUID, object oPlaceId)
        {
            

            #region '修改计划状态为已退勤'
            string sqlText =
                 @"update TAB_Plan_Train set nPlanState=@nPlanState where strTrainPlanGUID=@strTrainPlanGUID and  
                   (select count(*) from VIEW_Plan_EndWork where strTrainPlanGUID=TAB_Plan_Train.strTrainPlanGUID  and  
                   ((strTrainmanGUID1 is null) or (strTrainmanGUID1 = '') or not(dtTestTime1 is null)) and 
                   ((strTrainmanGUID2 is null) or (strTrainmanGUID2 = '') or not(dtTestTime2 is null))  and 
                   ((strTrainmanGUID3 is null) or (strTrainmanGUID3 = '') or not(dtTestTime3 is null))  and 
                   ((strTrainmanGUID4 is null) or (strTrainmanGUID4 = '') or not(dtTestTime4 is null))) > 0";

            SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID", planID),
                    new SqlParameter("nPlanState", 8),
                    new SqlParameter("NullTrainPlanGUID", ""),
                    new SqlParameter("dtLastArriveTime", dtArrvieTime),
                    new SqlParameter("nTrainmanState", 2),
                    new SqlParameter("dtLastEndWorkTime", DrinkTime),
                    new SqlParameter("strTrainmanGUID",trainmanID),
                    new SqlParameter("strGroupGUID", strGroupGUID),
                    new SqlParameter("strStationGUID",""),
                    new SqlParameter("strPlaceID",oPlaceId.ToString())

                };
            bool bFinishedPlan = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams) > 0;

            if (bFinishedPlan)
            {
                string strSql =
                      "update TAB_Nameplate_Group set strTrainPlanGUID = @NullTrainPlanGUID where strGroupGUID = @strGroupGUID";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                //翻牌
                //jltTogether
                if (nTrainmanJiaoluType == 4)
                {
                    TurnTogetherTrainGroup(strGroupGUID, dtArrvieTime);
                }

                //jltOrder,轮乘交路需要修改出勤点
                if (nTrainmanJiaoluType == 3)
                {
                    sqlText =
                        "update TAB_Nameplate_Group set strStationGUID = @strStationGUID,strPlaceID=@strPlaceID where strGroupGUID = @strGroupGUID";
                    int count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);

                    sqlText =
                        "update TAB_Nameplate_TrainmanJiaolu_Order set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
                }

                TF.CommonUtility.LogClass.log("修改计划状态为已勤");
            }

            #endregion


            //修改计划终到时间
            sqlText =
                @"update TAB_Plan_Train set dtLastArriveTime = @dtLastArriveTime where strTrainPlanGUID = @strTrainPlanGUID";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
            TF.CommonUtility.LogClass.log("修改计划终到时间");
            //修改人员最后一次退勤时间
            sqlText =
                "update TAB_Org_Trainman set dtLastEndWorkTime=@dtLastEndWorkTime,nTrainmanState=@nTrainmanState where strTrainmanGUID = @strTrainmanGUID";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
            TF.CommonUtility.LogClass.log("修改人员最后一次退勤时间");
            return bFinishedPlan;
        }
        public static void TurnTogetherTrainGroup(string strGroupGUID, DateTime LastEndworkTime)
        {
            string stTrainGUID = "";
            string sqlText = "select strTrainGUID from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strGroupGUID",strGroupGUID),
                new SqlParameter("dtLastArriveTime",LastEndworkTime)
            };
            object objTrainGUID = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
            if ((objTrainGUID != null) && (!DBNull.Value.Equals(objTrainGUID)))
            {
                stTrainGUID = objTrainGUID.ToString();
            }

            ///将所有非自己的机组按照从1开始排序
            sqlText = "select strGroupGUID from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strTrainGUID=@strTrainGUID and strGroupGUID <> @strGroupGUID order by nOrder";
            SqlParameter[] sqlParamsSubs = new SqlParameter[]{
                            new SqlParameter("strTrainGUID",stTrainGUID),
                            new SqlParameter("strGroupGUID",strGroupGUID)
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
                            new SqlParameter("strGroupGUID",strGroupGUID)
                        };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlReOrder2);

            sqlText =
                @"update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
        }
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

       
    }

}
