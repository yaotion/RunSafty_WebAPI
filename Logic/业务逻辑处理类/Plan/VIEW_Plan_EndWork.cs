/**  版本信息模板在安装目录下，可自行修改。
* VIEW_Plan_EndWork.cs
*
* 功 能： N/A
* 类 名： VIEW_Plan_EndWork
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/25 10:13:42   N/A    初版
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
namespace TF.RunSafty.BLL.Plan
{
	/// <summary>
	/// VIEW_Plan_EndWork
	/// </summary>
	public partial class VIEW_Plan_EndWork
	{
		private readonly TF.RunSafty.DAL.VIEW_Plan_EndWork dal=new TF.RunSafty.DAL.VIEW_Plan_EndWork();
		public VIEW_Plan_EndWork()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.VIEW_Plan_EndWork GetModel(int nid)
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
		public List<TF.RunSafty.Model.VIEW_Plan_EndWork> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.VIEW_Plan_EndWork> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.VIEW_Plan_EndWork> modelList = new List<TF.RunSafty.Model.VIEW_Plan_EndWork>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.VIEW_Plan_EndWork model;
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
     nPlanState=8  and (strTrainmanGUID1 ='{1}' or strTrainmanGUID2 = '{1}'  or strTrainmanGUID3 = '{1}'  or strTrainmanGUID4 = '{1}' )", clientGUID, strTrainmanid);
            DataSet set = dal.GetList(strWhere);
            List<TF.RunSafty.Model.VIEW_Plan_EndWork> vPlans = DataTableToList(set.Tables[0]);
            return GetPlanList(vPlans);
        }
        /// <summary>
        /// 获取退勤计划
        /// </summary>
        /// <param name="clientGUID"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> GetTuiqinPlansOfSite(string clientGUID, string beginTime, string endTime)
        {
            TF.RunSafty.BLL.TAB_Base_TrainJiaoluInSite bllInsite = new TAB_Base_TrainJiaoluInSite();
            string strTrainjiaolus = "";
            string strWhere = "";

            strWhere = string.Format(" strSiteGUID ='{0}' ",clientGUID);
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
            DataSet set = dal.GetEndWorkPlans(beginTime,endTime,strTrainjiaolus);
            List<TF.RunSafty.Model.VIEW_Plan_EndWork> vPlans = DataTableToList(set.Tables[0]);
            return GetPlanList(vPlans);
        }

        private List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> GetPlanList(List<TF.RunSafty.Model.VIEW_Plan_EndWork> vPlans)
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
                    trainPlan.planStateName =plan.strPlanStateName;
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
		#endregion  ExtensionMethod
	}
}

