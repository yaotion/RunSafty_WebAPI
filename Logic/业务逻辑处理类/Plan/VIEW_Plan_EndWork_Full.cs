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
using System.Collections.Generic;
using TF.RunSafty.Model;
using TF.Api.Utilities;
using TF.RunSafty.Model.InterfaceModel;


namespace TF.RunSafty.BLL.Plan
{
	/// <summary>
	/// VIEW_Plan_EndWork_Full
	/// </summary>
	public partial class VIEW_Plan_EndWork_Full
	{
		private readonly TF.RunSafty.DAL.VIEW_Plan_EndWork_Full dal=new TF.RunSafty.DAL.VIEW_Plan_EndWork_Full();
		public VIEW_Plan_EndWork_Full()
		{}
		#region  BasicMethod
         

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.VIEW_Plan_EndWork_Full GetModel(int nid)
		{
			
			return dal.GetModel(nid);
		}
         
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> modelList = new List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.VIEW_Plan_EndWork_Full model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 3.1.9获取指定人员在指定客户端下的退勤计划
        /// </summary>
        /// <param name="clientGUID"></param>
        /// <param name="strTrainmanid"></param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> GetTuiqinPlansOfTrainmanInSite(string clientGUID, string strTrainmanid)
        {

            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite
            where strSiteGUID = '{0}') and  
     nPlanState={2} and (strTrainmanGUID1 ='{1}' or strTrainmanGUID2 = '{1}'  or strTrainmanGUID3 = '{1}'  or strTrainmanGUID4 = '{1}' )", clientGUID, strTrainmanid,(int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psBeginWork);
            DataSet set = dal.GetList(strWhere);
            List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> vPlans = DataTableToList(set.Tables[0]);
            return GetPlanList(vPlans);
        }

        public List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> GetEndWorkOfTrainmanInSite(string clientGUID, string strTrainmanid)
        {
            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite
            where strSiteGUID = '{0}') and  
     nPlanState={2} and (strTrainmanGUID1 ='{1}' or strTrainmanGUID2 = '{1}'  or strTrainmanGUID3 = '{1}'  or strTrainmanGUID4 = '{1}' )", clientGUID, strTrainmanid, (int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psBeginWork);
            DataSet set = dal.GetList(strWhere);
            List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> vPlans = DataTableToList(set.Tables[0]);
            return vPlans;
        }
        /// <summary>
        /// 获取退勤计划
        /// </summary>
        /// <param name="clientGUID"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// 
        /// <returns></returns>
        public List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> GetTuiqinPlansOfSite(string clientGUID, string beginTime, string endTime,int isAll)
        {
            TF.RunSafty.BLL.TAB_Base_TrainJiaoluInSite bllInsite = new TAB_Base_TrainJiaoluInSite();
            string strTrainjiaolus = "";
            string strWhere = "";

            strWhere = string.Format(" strSiteGUID ='{0}' ", clientGUID);
            List<TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite> sites = bllInsite.GetModelList(strWhere);

            if (sites != null && sites.Count > 0)
            {
                for (int i = 0; i < sites.Count; i++)
                {
                    strTrainjiaolus += string.Format("'{0}'", sites[i].strTrainJiaoluGUID);
                    if (i < sites.Count - 1)
                    {
                        strTrainjiaolus += ",";
                    }
                }
            }
            else
            {
                throw new Exception("查找不到该客户端下行车区段");
            }
            DataSet set = dal.GetEndWorkPlans(beginTime, endTime, strTrainjiaolus,isAll);
            List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> vPlans = DataTableToList(set.Tables[0]);
            return GetPlanList(vPlans);
        }
        public List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> GetEndWorkOfSite(string clientGUID, string beginTime, string endTime, int isAll)
        {
            TF.RunSafty.BLL.TAB_Base_TrainJiaoluInSite bllInsite = new TAB_Base_TrainJiaoluInSite();
            string strTrainjiaolus = "";
            string strWhere = "";

            strWhere = string.Format(" strSiteGUID ='{0}' ", clientGUID);
            List<TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite> sites = bllInsite.GetModelList(strWhere);

            if (sites != null && sites.Count > 0)
            {
                for (int i = 0; i < sites.Count; i++)
                {
                    strTrainjiaolus += string.Format("'{0}'", sites[i].strTrainJiaoluGUID);
                    if (i < sites.Count - 1)
                    {
                        strTrainjiaolus += ",";
                    }
                }
            }
            else
            {
                throw new Exception("查找不到该客户端下行车区段");
            }
            DataSet set = dal.GetEndWorkPlans(beginTime, endTime, strTrainjiaolus, isAll);
            List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> vPlans = DataTableToList(set.Tables[0]);
            return vPlans;
        }
        private List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> GetPlanList(List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> vPlans)
        {
            List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> lPlans = new List<Model.InterfaceModel.mTuiqinPlansOfSite>();
            TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_EndWork_Full plan in vPlans)
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
                    clientPlan.trainPlan = new TF.RunSafty.Model.InterfaceModel.TrainPlan();
                    clientPlan.trainPlan.createSiteGUID = plan.strCreateSiteGUID;
                    clientPlan.trainPlan.createSiteName = plan.strCreateSiteName;
                    if (plan.dtCreateTime.HasValue)
                    {
                        clientPlan.trainPlan.createTime = plan.dtCreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    clientPlan.trainPlan.createUserGUID = plan.strCreateUserGUID;
                    clientPlan.trainPlan.createUserName = plan.strCreateUserName;
                    clientPlan.trainPlan.dragTypeID = plan.nDragType.ToString();
                    clientPlan.trainPlan.dragTypeName = plan.nDragTypeName;
                    clientPlan.trainPlan.endStationID = plan.strEndStation;
                    clientPlan.trainPlan.endStationName = plan.strEndStationName;
                    clientPlan.trainPlan.kehuoID = plan.nKehuoID.ToString();
                    clientPlan.trainPlan.kehuoName = plan.strKehuoName;
                    clientPlan.trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    clientPlan.trainPlan.placeID = plan.strPlaceID;
                    clientPlan.trainPlan.placeName = plan.strPlaceName;
                    clientPlan.trainPlan.planStateID = plan.nPlanState.ToString();
                    clientPlan.trainPlan.planStateName = plan.strPlanStateName;
                    clientPlan.trainPlan.planTypeID = plan.nPlanType.ToString();
                    clientPlan.trainPlan.planTypeName = plan.strPlanTypeName;
                    clientPlan.trainPlan.remarkTypeID = plan.nRemarkType.ToString();
                    clientPlan.trainPlan.remarkTypeName = plan.strRemarkTypeName;
                    clientPlan.trainPlan.startStationID = plan.strStartStation;
                    clientPlan.trainPlan.startStationName = plan.strStartStationName;
                    clientPlan.trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    clientPlan.trainPlan.planID = plan.strTrainPlanGUID;
                    clientPlan.trainPlan.strTrainPlanGUID = plan.strTrainPlanGUID;
                    clientPlan.trainPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    clientPlan.trainPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    if (plan.dtArriveTime.HasValue)
                    {
                        clientPlan.trainPlan.dtArriveTime = plan.dtArriveTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtCallTime.HasValue)
                    {
                        clientPlan.trainPlan.dtCallTime = plan.dtCallTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nNeedRest.HasValue)
                    {
                        clientPlan.trainPlan.nNeedRest = plan.nNeedRest.Value;
                    }
                    if (plan.dtLastArriveTime.HasValue)
                    {
                        clientPlan.trainPlan.lastArriveTime = plan.dtLastArriveTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        clientPlan.trainPlan.startTime = plan.dtStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtRealStartTime.HasValue)
                    {
                        clientPlan.trainPlan.realStartTime = plan.dtRealStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        clientPlan.trainPlan.startTime = plan.dtStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        clientPlan.trainPlan.firstStartTime = plan.dtFirstStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
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
                    clientPlan.trainPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    clientPlan.trainPlan.trainmanTypeName = plan.strTrainmanTypeName;
                    clientPlan.trainPlan.trainNo = plan.strTrainNo;
                    clientPlan.trainPlan.trainNumber = plan.strTrainNumber;
                    clientPlan.trainPlan.trainTypeName = plan.strTrainTypeName;
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

                    tuiqinGroup.turnMinutes = plan.nTurnMinutes.ToString();
                    tuiqinGroup.turnAlarmMinutes = plan.nTurnAlarmMinutes.ToString();
                    tuiqinGroup.isOver = plan.bIsOver.ToString();
                    tuiqinGroup.signed = plan.bSigned.ToString();
                    if (plan.dtTurnStartTime.HasValue)
                    {
                        tuiqinGroup.turnStartTime = plan.dtTurnStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    //clientPlan.icCheckResult = plan.strICCheckResult;
                     

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
		#endregion  ExtensionMethod


        #region 接口
        #region 获取指定客户端下的退勤计划

        private class pTrainjiaoluPlan : ParamBase
        {
            public Plan data;
        }
        public class Plan
        {
            private string _siteID;
            private string _begintime;
            private string _endtime;
            private int _showAll;
            [NotNull]
            public string siteID { get { return _siteID; } set { _siteID = value; } }
            [NotNull]
            public string begintime
            {
                get
                {
                    return _begintime;
                }
                set
                {
                    _begintime = value;
                }
            }
            [NotNull]
            public string endtime
            {
                get
                {
                    return _endtime;
                }
                set
                {
                    _endtime = value;
                }
            }
            public int showAll
            {
                get { return _showAll; }
                set
                {
                    _showAll = value;
                }
            }
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<mTuiqinPlansOfSite> data;
        }

        public string GetTuiqinPlansOfSite(string data)
        {
            JsonModel jsonModel = new JsonModel();  
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                pTrainjiaoluPlan paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<pTrainjiaoluPlan>(data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    string starttime = paramModel.data.begintime;
                    string endtime = paramModel.data.endtime;
                    string clientID = paramModel.data.siteID;
                    int isAll = paramModel.data.showAll;
                    List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> plans = this.GetTuiqinPlansOfSite(clientID, starttime, endtime, isAll);
                    jsonModel.data = plans;
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
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
            return result;
        }
        #endregion



        #region


        private class aTrainjiaoluPlan : ParamBase
        {
            public aPlan data;
        }
        public class aPlan
        {
            private string _siteID;
            private string _trainmanID;
            [NotNull]
            public string siteID
            {
                get
                {
                    return _siteID;
                }
                set
                {
                    _siteID = value;
                }
            }
            [NotNull]
            public string trainmanID
            {
                get { return _trainmanID; }
                set { _trainmanID = value; }
            }
        }
        private class aJsonModel
        {
            public int result;
            public string resultStr;
            public mTuiqinPlansOfSite data;
        }

        public string GetTuiqinPlansOfTrainmanInSite(string data)
        {
            aJsonModel jsonModel = new aJsonModel();
            TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                aTrainjiaoluPlan paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<aTrainjiaoluPlan>(data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    string clientID = paramModel.data.siteID;
                    string strTrainmanGUID = paramModel.data.trainmanID;
                    List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> plans = this.GetTuiqinPlansOfTrainmanInSite(clientID, strTrainmanGUID);
                    if (plans.Count > 0)
                    {
                        jsonModel.data = plans[0];
                        jsonModel.result = 0;
                        jsonModel.resultStr = "提交成功";
                    }
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
            return result;
        }
        #endregion
        #endregion
    }
}

