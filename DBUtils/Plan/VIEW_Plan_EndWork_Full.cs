/**  版本信息模板在安装目录下，可自行修改。
* VIEW_Plan_EndWork_Full.cs
*
* 功 能： N/A
* 类 名： VIEW_Plan_EndWork_Full
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/26 10:15:36   N/A    初版
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
	/// 数据访问类:VIEW_Plan_EndWork_Full
	/// </summary>
	public partial class VIEW_Plan_EndWork_Full
	{
		public VIEW_Plan_EndWork_Full()
		{}
		#region  BasicMethod

 
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.VIEW_Plan_EndWork_Full GetModel(int nid)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 strTrainJiaoluName,strTrainmanTypeName,strTrainPlanGUID,strTrainJiaoluGUID,strTrainNo,strTrainNumber,dtStartTime,dtChuQinTime,strStartStation,strEndStation,dtCreateTime,nPlanState,strTrainTypeName,strStartStationName,strEndStationName,nTrainmanTypeID,SendPlan,nNeedRest,dtArriveTime,dtCallTime,nKehuoID,dtRealStartTime,nPlanType,nDragType,nRemarkType,strRemark,strCreateSiteGUID,strCreateUserGUID,strCreateUserID,strCreateUserName,strCreateSiteName,strTrainmanNumber1,strTrainmanName1,nPostID1,strWorkShopGUID1,strTelNumber1,dtLastEndWorkTime1,strTrainmanGUID1,strTrainmanGUID2,strTrainmanNumber2,strTrainmanName2,strWorkShopGUID2,strTelNumber2,strTrainmanNumber3,strTrainmanGUID3,dtLastEndWorkTime2,nPostID2,strTrainmanName3,nPostID3,strWorkShopGUID3,strTelNumber3,dtLastEndWorkTime3,strDutyGUID,strGroupGUID,strDutySiteGUID,dtTrainmanCreateTime,dtFirstStartTime,nid,nDragTypeName,strKehuoName,nTrainmanState1,nTrainmanState2,nTrainmanState3,strBak1,dtLastArriveTime,strMainPlanGUID,strTrainmanName4,strTrainmanNumber4,nPostID4,strTelNumber4,dtLastEndWorkTime4,nTrainmanState,strTrainmanGUID4,strRemarkTypeName,strPlanTypeName,strPlaceID,strStateName,nDriverType1,nDriverType2,nDriverType3,nDriverType4,strABCD1,strABCD2,strABCD3,strABCD4,isKey1,isKey2,isKey3,isKey4,strPlanStateName,strStationName,strStationNumber,strPlaceName,strStationGUID,dtTestTime1,nVerifyID1,nDrinkResult1,dtTestTime2,nVerifyID2,nDrinkResult2,dtTestTime3,nVerifyID3,nDrinkResult3,DrinkImage3,DrinkImage1,DrinkImage2,strEndWorkGUID1,strEndWorkGUID2,strEndWorkGUID3,strEndWorkGUID4,dtTestTime4,nVerifyID4,DrinkImage4,nDrinkResult4,dtTurnStartTime,bSigned,bIsOver,nTurnMinutes,nTurnAlarmMinutes from VIEW_Plan_EndWork_Full ");
            strSql.Append(" where nid=@nid");
			SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;

			TF.RunSafty.Model.VIEW_Plan_EndWork_Full model=new TF.RunSafty.Model.VIEW_Plan_EndWork_Full();
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
        public TF.RunSafty.Model.VIEW_Plan_EndWork_Full DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.VIEW_Plan_EndWork_Full model = new TF.RunSafty.Model.VIEW_Plan_EndWork_Full();
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
                        model.dtTestTime3= dtTestTime3;
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
                if (row["dtTurnStartTime"] != null && row["dtTurnStartTime"].ToString() != "")
                {
                    model.dtTurnStartTime = DateTime.Parse(row["dtTurnStartTime"].ToString());
                }
                if (row["bSigned"] != null && row["bSigned"].ToString() != "")
                {
                    model.bSigned = int.Parse(row["bSigned"].ToString());
                }
                if (row["bIsOver"] != null && row["bIsOver"].ToString() != "")
                {
                    model.bIsOver = int.Parse(row["bIsOver"].ToString());
                }
                if (row["nTurnMinutes"] != null && row["nTurnMinutes"].ToString() != "")
                {
                    model.nTurnMinutes = int.Parse(row["nTurnMinutes"].ToString());
                }
                if (row["nTurnAlarmMinutes"] != null && row["nTurnAlarmMinutes"].ToString() != "")
                {
                    model.nTurnAlarmMinutes = int.Parse(row["nTurnAlarmMinutes"].ToString());
                }                

            }
            return model;
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select strTrainJiaoluName,strTrainmanTypeName,strTrainPlanGUID,strTrainJiaoluGUID,strTrainNo,strTrainNumber,dtStartTime,dtChuQinTime,strStartStation,strEndStation,dtCreateTime,nPlanState,strTrainTypeName,strStartStationName,strEndStationName,nTrainmanTypeID,SendPlan,nNeedRest,dtArriveTime,dtCallTime,nKehuoID,dtRealStartTime,nPlanType,nDragType,nRemarkType,strRemark,strCreateSiteGUID,strCreateUserGUID,strCreateUserID,strCreateUserName,strCreateSiteName,strTrainmanNumber1,strTrainmanName1,nPostID1,strWorkShopGUID1,strTelNumber1,dtLastEndWorkTime1,strTrainmanGUID1,strTrainmanGUID2,strTrainmanNumber2,strTrainmanName2,strWorkShopGUID2,strTelNumber2,strTrainmanNumber3,strTrainmanGUID3,dtLastEndWorkTime2,nPostID2,strTrainmanName3,nPostID3,strWorkShopGUID3,strTelNumber3,dtLastEndWorkTime3,strDutyGUID,strGroupGUID,strDutySiteGUID,dtTrainmanCreateTime,dtFirstStartTime,nid,nDragTypeName,strKehuoName,nTrainmanState1,nTrainmanState2,nTrainmanState3,strBak1,dtLastArriveTime,strMainPlanGUID,strTrainmanName4,strTrainmanNumber4,nPostID4,strTelNumber4,dtLastEndWorkTime4,nTrainmanState,strTrainmanGUID4,strRemarkTypeName,strPlanTypeName,strPlaceID,strStateName,nDriverType1,nDriverType2,nDriverType3,nDriverType4,strABCD1,strABCD2,strABCD3,strABCD4,isKey1,isKey2,isKey3,isKey4,strPlanStateName,'' as strStationName,'' as strStationNumber,strPlaceName,'' as strStationGUID,dtTestTime1,nVerifyID1,nDrinkResult1,dtTestTime2,nVerifyID2,nDrinkResult2,dtTestTime3,nVerifyID3,nDrinkResult3,DrinkImage3,DrinkImage1,DrinkImage2,strEndWorkGUID1,strEndWorkGUID2,strEndWorkGUID3,strEndWorkGUID4,dtTestTime4,nVerifyID4,DrinkImage4,nDrinkResult4,dtTurnStartTime,bSigned,bIsOver,nTurnMinutes,nTurnAlarmMinutes ");
            strSql.Append(" FROM VIEW_Plan_EndWork_Full ");
			if(strWhere.Trim()!="")
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
            strSql.Append(" strTrainJiaoluName,strTrainmanTypeName,strTrainPlanGUID,strTrainJiaoluGUID,strTrainNo,strTrainNumber,dtStartTime,dtChuQinTime,strStartStation,strEndStation,dtCreateTime,nPlanState,strTrainTypeName,strStartStationName,strEndStationName,nTrainmanTypeID,SendPlan,nNeedRest,dtArriveTime,dtCallTime,nKehuoID,dtRealStartTime,nPlanType,nDragType,nRemarkType,strRemark,strCreateSiteGUID,strCreateUserGUID,strCreateUserID,strCreateUserName,strCreateSiteName,strTrainmanNumber1,strTrainmanName1,nPostID1,strWorkShopGUID1,strTelNumber1,dtLastEndWorkTime1,strTrainmanGUID1,strTrainmanGUID2,strTrainmanNumber2,strTrainmanName2,strWorkShopGUID2,strTelNumber2,strTrainmanNumber3,strTrainmanGUID3,dtLastEndWorkTime2,nPostID2,strTrainmanName3,nPostID3,strWorkShopGUID3,strTelNumber3,dtLastEndWorkTime3,strDutyGUID,strGroupGUID,strDutySiteGUID,dtTrainmanCreateTime,dtFirstStartTime,nid,nDragTypeName,strKehuoName,nTrainmanState1,nTrainmanState2,nTrainmanState3,strBak1,dtLastArriveTime,strMainPlanGUID,strTrainmanName4,strTrainmanNumber4,nPostID4,strTelNumber4,dtLastEndWorkTime4,nTrainmanState,strTrainmanGUID4,strRemarkTypeName,strPlanTypeName,strPlaceID,strStateName,nDriverType1,nDriverType2,nDriverType3,nDriverType4,strABCD1,strABCD2,strABCD3,strABCD4,isKey1,isKey2,isKey3,isKey4,strPlanStateName,'' as strStationName,'' as strStationNumber,strPlaceName,'' as strStationGUID,dtTestTime1,nVerifyID1,nDrinkResult1,dtTestTime2,nVerifyID2,nDrinkResult2,dtTestTime3,nVerifyID3,nDrinkResult3,DrinkImage3,DrinkImage1,DrinkImage2,strEndWorkGUID1,strEndWorkGUID2,strEndWorkGUID3,strEndWorkGUID4,dtTestTime4,nVerifyID4,DrinkImage4,nDrinkResult4,dtTurnStartTime,bSigned,bIsOver,nTurnMinutes,nTurnAlarmMinutes ");
            strSql.Append(" FROM VIEW_Plan_EndWork_Full ");
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
			strSql.Append("select count(1) FROM VIEW_Plan_EndWork_Full ");
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
			strSql.Append(")AS Row, T.*  from VIEW_Plan_EndWork_Full T ");
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
			parameters[0].Value = "VIEW_Plan_EndWork_Full";
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
        /// <summary>
        /// 获取退勤计划
        /// </summary>
        /// <param name="strBeginTime">开始时间</param>
        /// <param name="strEndTime">结束时间</param>
        /// <param name="strTrainjiaolus">交路</param>
        /// <param name="isAll">是否查询所有的退勤计划，1：退勤的和已经出勤的，0：只显示已经退勤的</param>
        /// <returns></returns>
        public DataSet GetEndWorkPlans(string strBeginTime, string strEndTime, string strTrainjiaolus,int isAll)
        {
            int nPlanState = (int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psBeginWork;
            string strWhere = " ";
            System.Text.StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" strTrainJiaoluGUID in ({0}) ", strTrainjiaolus);
            if (isAll == 0)
            {
                builder.AppendFormat(" and dtLastArriveTime >='{0}' and nPlanState > 7 ",strBeginTime);
            }
            else
            {
                builder.AppendFormat(" and ( (dtLastArriveTime >='{0}'  and nPlanState >=7) or nPlanState = {1} ) ", strBeginTime, nPlanState);
//                strWhere = string.Format(@" (dtLastArriveTime >='{0}') 
//    or
//  (nPlanState = {3} and  bIsOver = 0 and '{1}' > DATEADD (mi,nTurnAlarmMinutes,dtTurnStartTime) ) 
//", strBeginTime, strEndTime, strTrainjiaolus, nPlanState);
                
            }
            builder.Append(" order by nPlanState,dtLastArriveTime desc,dtStartTime asc ");
            strWhere = builder.ToString();
            DataSet set = GetList(strWhere);
            return set;
        }
		#endregion  ExtensionMethod
	}
}

