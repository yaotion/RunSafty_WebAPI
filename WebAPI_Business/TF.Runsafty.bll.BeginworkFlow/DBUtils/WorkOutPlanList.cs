﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using System.Collections.Generic;

namespace TF.RunSafty.BeginworkFlow
{
    public class DBWorkOutPlanList
    {
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM VIEW_Plan_Trainman ");
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }
        public List<TF.RunSafty.BeginworkFlow.WorkOutPlanList> GetPlanWorkOutList(string siteID, DateTime BeginTime, DateTime Endime)
        {
            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite
            where strSiteGUID = '{0}') and   (nPlanState in (4,7)) 
             and dtStartTime>'{1}' and  dtStartTime<'{2}'  order by nPlanState asc ,dtStartTime desc ", siteID, BeginTime, Endime);
            DataSet set = GetList(strWhere);


            string strGUIDs = "";
            for (int k = 0; k < set.Tables[0].Rows.Count; k++)
            {
                strGUIDs += "'" + set.Tables[0].Rows[k]["strTrainPlanGUID"] + "',";
            }
            if (strGUIDs.Length > 0)
                strGUIDs = strGUIDs.Substring(0, strGUIDs.Length - 1);
            else
                strGUIDs = "''";

            DataTable dt2 = getData(strGUIDs);

            List<TF.RunSafty.BeginworkFlow.WorkOutPlanList> vPlans = DTToList(set.Tables[0], dt2);
            return vPlans;
        }
        public DataTable getData(string strGUIDs)
        {
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select  * FROM [TAB_Plan_Beginwork_StepData] where  ");
            strSql2.Append(" strTrainPlanGUID in (" + strGUIDs + ")");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()).Tables[0];
        }
        public List<TF.RunSafty.BeginworkFlow.WorkOutPlanList> DTToList(DataTable dt, DataTable dtData)
        {
            List<TF.RunSafty.BeginworkFlow.WorkOutPlanList> modelList = new List<TF.RunSafty.BeginworkFlow.WorkOutPlanList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.BeginworkFlow.WorkOutPlanList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n], dtData);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public TF.RunSafty.BeginworkFlow.WorkOutPlanList DataRowToModel(DataRow row, DataTable dtData)
        {
            TF.RunSafty.BeginworkFlow.WorkOutPlanList model = new TF.RunSafty.BeginworkFlow.WorkOutPlanList();
            if (row != null)
            {

                DataView view = new DataView();
                view.Table = dtData;
                view.RowFilter = "strTrainPlanGUID='" + row["strTrainPlanGUID"].ToString() + "'";
                DataTable StepDatas = view.ToTable();
                for (int k = 0; k < StepDatas.Rows.Count; k++)
                {
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "nDrinkResult1")//司机的测酒结果
                        model.cqcj1 = ObjectConvertClass.static_ext_int(StepDatas.Rows[k]["nStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "nDrinkResult2")//学员1的测酒结果
                        model.cqcj2 = ObjectConvertClass.static_ext_int(StepDatas.Rows[k]["nStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "nDrinkResult3")//学员2测酒结果
                        model.cqcj3 = ObjectConvertClass.static_ext_int(StepDatas.Rows[k]["nStepData"]);

                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "dtEventTime1")//司机的测酒时间
                        model.cqtime1 = ObjectConvertClass.static_ext_date(StepDatas.Rows[k]["dtStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "dtEventTime2")//学员1的测酒时间
                        model.cqtime2 = ObjectConvertClass.static_ext_date(StepDatas.Rows[k]["dtStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "dtEventTime3")//学员2测酒时间
                        model.cqtime3 = ObjectConvertClass.static_ext_date(StepDatas.Rows[k]["dtStepData"]);

                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "picture1")//司机照片
                        model.picture1 = ObjectConvertClass.static_ext_string(StepDatas.Rows[k]["strStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "picture2")//学员1的照片
                        model.picture2 = ObjectConvertClass.static_ext_string(StepDatas.Rows[k]["strStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "picture3")//学员2测照片
                        model.picture3 = ObjectConvertClass.static_ext_string(StepDatas.Rows[k]["strStepData"]);


                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "nVerifyID1")//司机的验证方式
                        model.nVerifyID1 = ObjectConvertClass.static_ext_int(StepDatas.Rows[k]["nStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "nVerifyID2")//学员1验证方式
                        model.nVerifyID2 = ObjectConvertClass.static_ext_int(StepDatas.Rows[k]["nStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "nVerifyID3")//学员2验证方式
                        model.nVerifyID3 = ObjectConvertClass.static_ext_int(StepDatas.Rows[k]["nStepData"]);


                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "LateTimeDiff1")//司机晚出勤
                        model.lateTimeDiff1 = ObjectConvertClass.static_ext_int(StepDatas.Rows[k]["nStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "LateTimeDiff2")//学员1晚出勤
                        model.lateTimeDiff2 = ObjectConvertClass.static_ext_int(StepDatas.Rows[k]["nStepData"]);
                    if (StepDatas.Rows[k]["strFieldName"].ToString() == "LateTimeDiff3")//学员2晚出勤
                        model.lateTimeDiff3 = ObjectConvertClass.static_ext_int(StepDatas.Rows[k]["nStepData"]);
                }
                if (row["strTrainJiaoluName"] != null)
                    model.strTrainJiaoluName = row["strTrainJiaoluName"].ToString();
                if (row["strTrainmanTypeName"] != null)
                    model.strTrainmanTypeName = row["strTrainmanTypeName"].ToString();
                if (row["strTrainPlanGUID"] != null)
                    model.strTrainPlanGUID = row["strTrainPlanGUID"].ToString();
                if (row["strTrainJiaoluGUID"] != null)
                    model.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
                if (row["strTrainNo"] != null)
                    model.strTrainNo = row["strTrainNo"].ToString();
                if (row["strTrainNumber"] != null)
                    model.strTrainNumber = row["strTrainNumber"].ToString();
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
                    model.strEndStation = row["strEndStation"].ToString();
                if (row["dtCreateTime"] != null && row["dtCreateTime"].ToString() != "")
                {
                    DateTime dtCreateTime;
                    if (DateTime.TryParse(row["dtCreateTime"].ToString(), out dtCreateTime))
                    {
                        model.dtCreateTime = dtCreateTime;
                    }
                }
                if (row["nPlanState"] != null && row["nPlanState"].ToString() != "")
                    model.nPlanState = int.Parse(row["nPlanState"].ToString());
                if (row["strTrainTypeName"] != null)
                    model.strTrainTypeName = row["strTrainTypeName"].ToString();
                if (row["strStartStationName"] != null)
                    model.strStartStationName = row["strStartStationName"].ToString();
                if (row["strEndStationName"] != null)
                    model.strEndStationName = row["strEndStationName"].ToString();
                if (row["nTrainmanTypeID"] != null && row["nTrainmanTypeID"].ToString() != "")
                    model.nTrainmanTypeID = int.Parse(row["nTrainmanTypeID"].ToString());
                if (row["SendPlan"] != null && row["SendPlan"].ToString() != "")
                    model.SendPlan = int.Parse(row["SendPlan"].ToString());
                if (row["nNeedRest"] != null && row["nNeedRest"].ToString() != "")
                    model.nNeedRest = int.Parse(row["nNeedRest"].ToString());
                if (row["dtArriveTime"] != null && row["dtArriveTime"].ToString() != "")
                    model.dtArriveTime = DateTime.Parse(row["dtArriveTime"].ToString());
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
                    model.nKehuoID = int.Parse(row["nKehuoID"].ToString());
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
                    model.nPlanType = int.Parse(row["nPlanType"].ToString());
                if (row["nDragType"] != null && row["nDragType"].ToString() != "")
                    model.nDragType = int.Parse(row["nDragType"].ToString());
                if (row["nRemarkType"] != null && row["nRemarkType"].ToString() != "")
                    model.nRemarkType = int.Parse(row["nRemarkType"].ToString());
                if (row["strRemark"] != null)
                    model.strRemark = row["strRemark"].ToString();
                if (row["strWaiQinClientGUID"] != null)
                    model.strWaiQinClientGUID = row["strWaiQinClientGUID"].ToString();
                if (row["strWaiQinClientNumber"] != null)
                    model.strWaiQinClientNumber = row["strWaiQinClientNumber"].ToString();
                if (row["strWaiQinClientName"] != null)
                    model.strWaiQinClientName = row["strWaiQinClientName"].ToString();
                if (row["dtSendTime"] != null && row["dtSendTime"].ToString() != "")
                {
                    DateTime dtSendTime;
                    if (DateTime.TryParse(row["dtSendTime"].ToString(), out dtSendTime))
                    {
                        model.dtSendTime = dtSendTime;
                    }
                }
                if (row["dtRecvTime"] != null && row["dtRecvTime"].ToString() != "")
                {
                    DateTime dtRecvTime;
                    if (DateTime.TryParse(row["dtRecvTime"].ToString(), out dtRecvTime))
                    {
                        model.dtRecvTime = dtRecvTime;
                    }
                }
                if (row["strCreateSiteGUID"] != null)
                    model.strCreateSiteGUID = row["strCreateSiteGUID"].ToString();
                if (row["strCreateUserGUID"] != null)
                    model.strCreateUserGUID = row["strCreateUserGUID"].ToString();
                if (row["strCreateUserID"] != null)
                    model.strCreateUserID = row["strCreateUserID"].ToString();
                if (row["strCreateUserName"] != null)
                    model.strCreateUserName = row["strCreateUserName"].ToString();
                if (row["strCreateSiteName"] != null)
                    model.strCreateSiteName = row["strCreateSiteName"].ToString();
                if (row["strPlaceName"] != null)
                    model.strPlaceName = row["strPlaceName"].ToString();
                if (row["strTrainmanNumber1"] != null)
                    model.strTrainmanNumber1 = row["strTrainmanNumber1"].ToString();
                if (row["strTrainmanName1"] != null)
                    model.strTrainmanName1 = row["strTrainmanName1"].ToString();
                if (row["nPostID1"] != null && row["nPostID1"].ToString() != "")
                    model.nPostID1 = int.Parse(row["nPostID1"].ToString());
                if (row["strWorkShopGUID1"] != null)
                    model.strWorkShopGUID1 = row["strWorkShopGUID1"].ToString();
                if (row["strTelNumber1"] != null)
                    model.strTelNumber1 = row["strTelNumber1"].ToString();
                if (row["dtLastEndWorkTime1"] != null && row["dtLastEndWorkTime1"].ToString() != "")
                {
                    // model.dtLastEndWorkTime1 = DateTime.Parse(row["dtLastEndWorkTime1"].ToString());
                    DateTime dtLastEndWorkTime1;
                    if (DateTime.TryParse(row["dtLastEndWorkTime1"].ToString(), out dtLastEndWorkTime1))
                    {
                        model.dtLastEndWorkTime1 = dtLastEndWorkTime1;
                    }
                }
                if (row["strTrainmanGUID1"] != null)
                    model.strTrainmanGUID1 = row["strTrainmanGUID1"].ToString();
                if (row["strTrainmanGUID2"] != null)
                    model.strTrainmanGUID2 = row["strTrainmanGUID2"].ToString();
                if (row["strTrainmanNumber2"] != null)
                    model.strTrainmanNumber2 = row["strTrainmanNumber2"].ToString();
                if (row["strTrainmanName2"] != null)
                    model.strTrainmanName2 = row["strTrainmanName2"].ToString();
                if (row["strWorkShopGUID2"] != null)
                    model.strWorkShopGUID2 = row["strWorkShopGUID2"].ToString();
                if (row["strTelNumber2"] != null)
                    model.strTelNumber2 = row["strTelNumber2"].ToString();
                if (row["strTrainmanNumber3"] != null)
                    model.strTrainmanNumber3 = row["strTrainmanNumber3"].ToString();
                if (row["strTrainmanGUID3"] != null)
                    model.strTrainmanGUID3 = row["strTrainmanGUID3"].ToString();
                if (row["dtLastEndWorkTime2"] != null && row["dtLastEndWorkTime2"].ToString() != "")
                {
                    // model.dtLastEndWorkTime2 = DateTime.Parse(row["dtLastEndWorkTime2"].ToString());
                    DateTime dtLastEndWorkTime2;
                    if (DateTime.TryParse(row["dtLastEndWorkTime2"].ToString(), out dtLastEndWorkTime2))
                    {
                        model.dtLastEndWorkTime2 = dtLastEndWorkTime2;
                    }
                }
                if (row["nPostID2"] != null && row["nPostID2"].ToString() != "")
                    model.nPostID2 = int.Parse(row["nPostID2"].ToString());
                if (row["strTrainmanName3"] != null)
                    model.strTrainmanName3 = row["strTrainmanName3"].ToString();
                if (row["nPostID3"] != null && row["nPostID3"].ToString() != "")
                    model.nPostID3 = int.Parse(row["nPostID3"].ToString());
                if (row["strWorkShopGUID3"] != null)
                    model.strWorkShopGUID3 = row["strWorkShopGUID3"].ToString();
                if (row["strTelNumber3"] != null)
                    model.strTelNumber3 = row["strTelNumber3"].ToString();
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
                    model.strDutyGUID = row["strDutyGUID"].ToString();
                if (row["strGroupGUID"] != null)
                    model.strGroupGUID = row["strGroupGUID"].ToString();
                if (row["strDutySiteGUID"] != null)
                    model.strDutySiteGUID = row["strDutySiteGUID"].ToString();
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
                model.nMsgState1 = 0;
                if (row["nMsgState1"] != null && row["nMsgState1"].ToString() != "")
                {
                    model.nMsgState1 = int.Parse(row["nMsgState1"].ToString());
                }
                if (row["strMsgID1"] != null)
                {
                    model.strMsgID1 = row["strMsgID1"].ToString();
                }
                model.nMsgState2 = 0;
                if (row["nMsgState2"] != null && row["nMsgState2"].ToString() != "")
                {
                    model.nMsgState2 = int.Parse(row["nMsgState2"].ToString());
                }
                if (row["strMsgID2"] != null)
                {
                    model.strPlanStateName = row["strMsgID2"].ToString();
                }
                model.nMsgState3 = 0;
                if (row["nMsgState3"] != null && row["nMsgState3"].ToString() != "")
                {
                    model.nMsgState3 = int.Parse(row["nMsgState3"].ToString());
                }

                if (row["strMsgID3"] != null)
                {
                    model.strMsgID3 = row["strMsgID3"].ToString();
                }
                model.nMsgState4 = 0;
                if (row["nMsgState4"] != null && row["nMsgState4"].ToString() != "")
                {
                    model.nMsgState4 = int.Parse(row["nMsgState4"].ToString());
                }
                if (row["strMsgID4"] != null)
                {
                    model.strMsgID4 = row["strMsgID4"].ToString();
                }

                if (row["strMobileNumber1"] != null)
                {
                    model.strMobileNumber1 = row["strMobileNumber1"].ToString();
                }
                if (row["strMobileNumber2"] != null)
                {
                    model.strMobileNumber2 = row["strMobileNumber2"].ToString();
                }
                if (row["strMobileNumber3"] != null)
                {
                    model.strMobileNumber3 = row["strMobileNumber3"].ToString();
                }
                if (row["strMobileNumber4"] != null)
                {
                    model.strMobileNumber4 = row["strMobileNumber4"].ToString();
                }
                DateTime _dtBeginWorkTime;
                if (row["dtBeginWorkTime"] != DBNull.Value)
                {
                    if (DateTime.TryParse(row["dtBeginWorkTime"].ToString(), out _dtBeginWorkTime))
                    {
                        model.dtBeginWorkTime = _dtBeginWorkTime;
                    }
                }
            }
            return model;
        }
        public List<TrainmanPlan> GetBeginFlowPlanList(List<TF.RunSafty.BeginworkFlow.WorkOutPlanList> vPlans)
        {
            List<TrainmanPlan> lPlans = new List<TrainmanPlan>();
            TrainmanPlan clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.BeginworkFlow.WorkOutPlanList plan in vPlans)
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
                    //chuqinGroup.group.station.stationID = plan.strStationGUID;
                    //chuqinGroup.group.station.stationName = plan.strStationName;
                    //if (plan.strStationNumber.HasValue)
                    //{
                    //    chuqinGroup.group.station.stationNumber = plan.strStationNumber.ToString();
                    //}
                    chuqinGroup.group.trainman1 = new TF.RunSafty.BeginworkFlow.Trainman();
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
                    chuqinGroup.group.trainman2 = new TF.RunSafty.BeginworkFlow.Trainman();
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
                    chuqinGroup.group.trainman3 = new TF.RunSafty.BeginworkFlow.Trainman();
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
                    chuqinGroup.group.trainman4 = new TF.RunSafty.BeginworkFlow.Trainman();
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
                    //clientPlan.icCheckResult = plan.strICCheckResult;
                    TF.RunSafty.BeginworkFlow.TrainPlan trainPlan = new TrainPlan();
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


                    chuqinGroup.verifyID1 = plan.nVerifyID1;
                    chuqinGroup.testAlcoholInfo1 = new TestAlcoholInfo();

                    chuqinGroup.verifyID2 = plan.nVerifyID2;
                    chuqinGroup.testAlcoholInfo2 = new TestAlcoholInfo();

                    chuqinGroup.verifyID3 = plan.nVerifyID3;
                    chuqinGroup.testAlcoholInfo3 = new TestAlcoholInfo();

                    //chuqinGroup.verifyID4 = plan.nVerifyID4;
                    //chuqinGroup.testAlcoholInfo4 = new TestAlcoholInfo();

                    chuqinGroup.testAlcoholInfo1.testAlcoholResult = plan.cqcj1;
                    chuqinGroup.testAlcoholInfo2.testAlcoholResult = plan.cqcj2;
                    chuqinGroup.testAlcoholInfo3.testAlcoholResult = plan.cqcj3;
                    //chuqinGroup.testAlcoholInfo4.testAlcoholResult = plan.cqcj4;

                    chuqinGroup.testAlcoholInfo1.testTime = plan.cqtime1.ToString();
                    chuqinGroup.testAlcoholInfo2.testTime = plan.cqtime1.ToString();
                    chuqinGroup.testAlcoholInfo3.testTime = plan.cqtime1.ToString();
                    // chuqinGroup.testAlcoholInfo4.testTime =""




                    chuqinGroup.testAlcoholInfo1.picture = plan.picture1;
                    chuqinGroup.testAlcoholInfo2.picture = plan.picture2;
                    chuqinGroup.testAlcoholInfo3.picture = plan.picture3;
                    //chuqinGroup.testAlcoholInfo4.picture = plan.picture4;


                    trainPlan.trainNo = plan.strTrainNo;
                    trainPlan.trainNumber = plan.strTrainNumber;
                    trainPlan.trainTypeName = plan.strTrainTypeName;
                    trainPlan.trainmanTypeName = plan.strTrainmanTypeName;



                    lPlans.Add(clientPlan);
                }
            }
            return lPlans;
        }



    }
}
