/**  版本信息模板在安装目录下，可自行修改。
* VIEW_Plan_Trainman.cs
*
* 功 能： N/A
* 类 名： VIEW_Plan_Trainman
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/24 9:39:26   N/A    初版
*
* Copyright (c) 2014 thinkfreely Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：郑州畅想高科股份有限公司　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.DAL
{
	/// <summary>
	/// 数据访问类:VIEW_Plan_Trainman
	/// </summary>
	public partial class VIEW_Plan_Trainman
	{
		public VIEW_Plan_Trainman()
		{}
		#region  BasicMethod



		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.VIEW_Plan_Trainman GetModel(int nid)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 strTrainJiaoluName,strTrainmanTypeName,strTrainPlanGUID,strTrainJiaoluGUID,strTrainNo,strTrainNumber,dtStartTime,dtChuQinTime,strStartStation,strEndStation,dtCreateTime,nPlanState,strTrainTypeName,strStartStationName,strEndStationName,nTrainmanTypeID,SendPlan,nNeedRest,dtArriveTime,dtCallTime,nKehuoID,dtRealStartTime,nPlanType,nDragType,nRemarkType,strRemark,strCreateSiteGUID,strCreateUserGUID,strCreateUserID,strCreateUserName,strCreateSiteName,strPlaceName,strTrainmanNumber1,strTrainmanName1,nPostID1,strWorkShopGUID1,strTelNumber1,dtLastEndWorkTime1,strTrainmanGUID1,strTrainmanGUID2,strTrainmanNumber2,strTrainmanName2,strWorkShopGUID2,strTelNumber2,strTrainmanNumber3,strTrainmanGUID3,dtLastEndWorkTime2,nPostID2,strTrainmanName3,nPostID3,strWorkShopGUID3,strTelNumber3,dtLastEndWorkTime3,strDutyGUID,strGroupGUID,strDutySiteGUID,dtTrainmanCreateTime,dtFirstStartTime,nid,nDragTypeName,strKehuoName,nTrainmanState1,nTrainmanState2,nTrainmanState3,strBak1,dtLastArriveTime,strMainPlanGUID,strTrainmanName4,strTrainmanNumber4,nPostID4,strTelNumber4,dtLastEndWorkTime4,nTrainmanState,strTrainmanGUID4,strRemarkTypeName,strPlanTypeName,strPlaceID,strStateName,nDriverType1,nDriverType2,nDriverType3,nDriverType4,strABCD1,strABCD2,strABCD3,strABCD4,isKey1,isKey2,isKey3,isKey4,strPlanStateName,strWaiQinClientGUID,strWaiQinClientNumber,strWaiQinClientName, from VIEW_Plan_Trainman ");
            strSql.Append(" where nid=@nid");
			SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;

			TF.RunSafty.Model.VIEW_Plan_Trainman model=new TF.RunSafty.Model.VIEW_Plan_Trainman();
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.VIEW_Plan_Trainman DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.VIEW_Plan_Trainman model = new TF.RunSafty.Model.VIEW_Plan_Trainman();
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
                if (row["strWaiQinClientGUID"] != null)
                {
                    model.strWaiQinClientGUID = row["strWaiQinClientGUID"].ToString();
                }
                if (row["strWaiQinClientNumber"] != null)
                {
                    model.strWaiQinClientNumber = row["strWaiQinClientNumber"].ToString();
                }
                if (row["strWaiQinClientName"] != null)
                {
                    model.strWaiQinClientName = row["strWaiQinClientName"].ToString();
                }
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
                if (row["strPlaceName"] != null)
                {
                    model.strPlaceName = row["strPlaceName"].ToString();
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
                   // model.dtLastEndWorkTime1 = DateTime.Parse(row["dtLastEndWorkTime1"].ToString());
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
                   // model.dtLastEndWorkTime2 = DateTime.Parse(row["dtLastEndWorkTime2"].ToString());
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
                    if(DateTime.TryParse(row["dtBeginWorkTime"].ToString(),out _dtBeginWorkTime))
                    {
                        model.dtBeginWorkTime = _dtBeginWorkTime;
                    }
                }
            }
            return model;
        }


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strTrainJiaoluName,strTrainmanTypeName,strTrainPlanGUID,strTrainJiaoluGUID,strTrainNo,strTrainNumber,dtStartTime,dtChuQinTime,strStartStation,strEndStation,dtCreateTime,nPlanState,strTrainTypeName,strStartStationName,strEndStationName,nTrainmanTypeID,SendPlan,nNeedRest,dtArriveTime,dtCallTime,nKehuoID,dtRealStartTime,nPlanType,nDragType,nRemarkType,strRemark,strCreateSiteGUID,strCreateUserGUID,strCreateUserID,strCreateUserName,strCreateSiteName,strPlaceName,strTrainmanNumber1,strTrainmanName1,nPostID1,strWorkShopGUID1,strTelNumber1,dtLastEndWorkTime1,strTrainmanGUID1,strTrainmanGUID2,strTrainmanNumber2,strTrainmanName2,strWorkShopGUID2,strTelNumber2,strTrainmanNumber3,strTrainmanGUID3,dtLastEndWorkTime2,nPostID2,strTrainmanName3,nPostID3,strWorkShopGUID3,strTelNumber3,dtLastEndWorkTime3,strDutyGUID,strGroupGUID,strDutySiteGUID,dtTrainmanCreateTime,dtFirstStartTime,nid,nDragTypeName,strKehuoName,nTrainmanState1,nTrainmanState2,nTrainmanState3,strBak1,dtLastArriveTime,strMainPlanGUID,strTrainmanName4,strTrainmanNumber4,nPostID4,strTelNumber4,dtLastEndWorkTime4,nTrainmanState,strTrainmanGUID4,strRemarkTypeName,strPlanTypeName,strPlaceID,strStateName,nDriverType1,nDriverType2,nDriverType3,nDriverType4,strABCD1,strABCD2,strABCD3,strABCD4,isKey1,isKey2,isKey3,isKey4,strPlanStateName,nMsgState1,nMsgState2,nMsgState3,nMsgState4,strMsgID1,strMsgID2,strMsgID3,strMsgID4,strMobileNumber1,strMobileNumber2,strMobileNumber3,strMobileNumber4,dtBeginWorkTime,strWaiQinClientGUID,strWaiQinClientNumber,strWaiQinClientName,dtSendTime,dtRecvTime ");
            strSql.Append(" FROM VIEW_Plan_Trainman ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(@" strTrainJiaoluName,strTrainmanTypeName,strTrainPlanGUID,strTrainJiaoluGUID,strTrainNo,strTrainNumber,dtStartTime,
                dtChuQinTime,strStartStation,strEndStation,dtCreateTime,nPlanState,strTrainTypeName,strStartStationName,strEndStationName,
                nTrainmanTypeID,SendPlan,nNeedRest,dtArriveTime,dtCallTime,nKehuoID,dtRealStartTime,nPlanType,nDragType,nRemarkType,strRemark,
                strCreateSiteGUID,strCreateUserGUID,strCreateUserID,strCreateUserName,strCreateSiteName,strPlaceName,strTrainmanNumber1,
                strTrainmanName1,nPostID1,strWorkShopGUID1,strTelNumber1,dtLastEndWorkTime1,strTrainmanGUID1,strTrainmanGUID2,strTrainmanNumber2,
                strTrainmanName2,strWorkShopGUID2,strTelNumber2,strTrainmanNumber3,strTrainmanGUID3,dtLastEndWorkTime2,nPostID2,strTrainmanName3,
                nPostID3,strWorkShopGUID3,strTelNumber3,dtLastEndWorkTime3,strDutyGUID,strGroupGUID,strDutySiteGUID,dtTrainmanCreateTime,
                dtFirstStartTime,nid,nDragTypeName,strKehuoName,nTrainmanState1,nTrainmanState2,nTrainmanState3,strBak1,dtLastArriveTime,
                strMainPlanGUID,strTrainmanName4,strTrainmanNumber4,nPostID4,strTelNumber4,dtLastEndWorkTime4,nTrainmanState,strTrainmanGUID4,
                strRemarkTypeName,strPlanTypeName,strPlaceID,strStateName,nDriverType1,nDriverType2,nDriverType3,nDriverType4,strABCD1,strABCD2,
                strABCD3,strABCD4,isKey1,isKey2,isKey3,isKey4,strPlanStateName,strMobileNumber1,strMobileNumber2,strMobileNumber3,strMobileNumber4 ");
            strSql.Append(" FROM VIEW_Plan_Trainman ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM VIEW_Plan_Trainman ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.nid desc");
			}
			strSql.Append(")AS Row, T.*  from VIEW_Plan_Trainman T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "VIEW_Plan_Trainman";
			parameters[1].Value = "nid";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

