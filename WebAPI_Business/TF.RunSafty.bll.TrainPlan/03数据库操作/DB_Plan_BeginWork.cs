using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using TF.Api.Utilities;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;
namespace TF.RunSafty.BLL.Plan
{
	/// <summary>
	/// VIEW_Plan_BeginWork
	/// </summary>
	public partial class DB_Plan_BeginWork
	{
		private readonly TF.RunSafty.DAL.VIEW_Plan_BeginWork dal=new TF.RunSafty.DAL.VIEW_Plan_BeginWork();
        public DB_Plan_BeginWork()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.VIEW_Plan_BeginWork GetModel(int nid)
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
		public List<TF.RunSafty.Model.VIEW_Plan_BeginWork> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.VIEW_Plan_BeginWork> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.VIEW_Plan_BeginWork> modelList = new List<TF.RunSafty.Model.VIEW_Plan_BeginWork>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.VIEW_Plan_BeginWork model;
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
        /// 3.1.8获取指定人员在指定客户端下的出勤计划
        /// </summary>
        /// <param name="clientGUID"></param>
        /// <param name="strTrainmanid"></param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> GetChuqinPlansOfTrainmanInSite(string clientGUID, string strTrainmanid)
        {
            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite
            where strSiteGUID = '{0}') and  
     (nPlanState in (4,7)) and (strTrainmanGUID1 ='{1}' or strTrainmanGUID2 = '{1}'  or strTrainmanGUID3 = '{1}'  or strTrainmanGUID4 = '{1}'     
) order by nPlanState,dtStartTime", clientGUID, strTrainmanid);
            DataSet set = dal.GetList(strWhere);
            List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans = DataTableToList(set.Tables[0]);
            return GetPlanList(vPlans);
            
        }
        public List<TF.RunSafty.Model.VIEW_Plan_BeginWork> GetBeginWorkOfTrainmanInSite(string clientGUID, string strTrainmanid)
        {
            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite
            where strSiteGUID = '{0}') and  
     (nPlanState in (4,7)) and (strTrainmanGUID1 ='{1}' or strTrainmanGUID2 = '{1}'  or strTrainmanGUID3 = '{1}'  or strTrainmanGUID4 = '{1}'     
) order by nPlanState,dtStartTime", clientGUID, strTrainmanid);
            DataSet set = dal.GetList(strWhere);
            List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans = DataTableToList(set.Tables[0]);
            return vPlans;
        }

        public List<TF.RunSafty.Model.VIEW_Plan_BeginWork> GetBeginWorkOfTrainmanByNumber(string clientGUID, string strTrainmanNumber)
        {
            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite
            where strSiteGUID = '{0}') and  
     (nPlanState in (4,7)) and (strTrainmanNumber1 ='{1}' or strTrainmanNumber2 = '{1}'  or strTrainmanNumber3 = '{1}'  or strTrainmanNumber4 = '{1}'     
) order by nPlanState,dtStartTime", clientGUID, strTrainmanNumber);
            DataSet set = dal.GetList(strWhere);
            List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans = DataTableToList(set.Tables[0]);
            return vPlans;
        }
        /// <summary>
        /// 3.1.6获取指定客户端的出勤计划列表
        /// </summary>
        /// <param name="clientGUID"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> GetChuqinPlansOfSite(string clientGUID, string beginTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select strTrainJiaoluName,strTrainmanTypeName,strTrainPlanGUID,
strTrainJiaoluGUID,strTrainNo,strTrainNumber,dtStartTime,dtChuQinTime,strStartStation,
strEndStation,dtCreateTime,nPlanState,strTrainTypeName,strStartStationName,strEndStationName,
nTrainmanTypeID,SendPlan,nNeedRest,dtArriveTime,dtCallTime,nKehuoID,dtRealStartTime,nPlanType,
nDragType,nRemarkType,strRemark,strCreateSiteGUID,strCreateUserGUID,strCreateUserID,strCreateUserName,
strCreateSiteName,strTrainmanNumber1,strTrainmanName1,nPostID1,strWorkShopGUID1,strTelNumber1,
dtLastEndWorkTime1,strTrainmanGUID1,strTrainmanGUID2,strTrainmanNumber2,strTrainmanName2,
strWorkShopGUID2,strTelNumber2,strTrainmanNumber3,strTrainmanGUID3,dtLastEndWorkTime2,nPostID2,
strTrainmanName3,nPostID3,strWorkShopGUID3,strTelNumber3,dtLastEndWorkTime3,strDutyGUID,strGroupGUID,
strDutySiteGUID,dtTrainmanCreateTime,dtFirstStartTime,nid,nDragTypeName,strKehuoName,nTrainmanState1,
nTrainmanState2,nTrainmanState3,strBak1,dtLastArriveTime,strMainPlanGUID,strTrainmanName4,strTrainmanNumber4,
nPostID4,strTelNumber4,dtLastEndWorkTime4,nTrainmanState,strTrainmanGUID4,strRemarkTypeName,strPlanTypeName,
strPlaceID,strStateName,nDriverType1,nDriverType2,nDriverType3,nDriverType4,strABCD1,strABCD2,strABCD3,strABCD4,
isKey1,isKey2,isKey3,isKey4,strPlanStateName,'' as strStationName,'' as strStationNumber,strPlaceName,'' as strStationGUID,
dtTestTime1,nVerifyID1,DrinkImage1,nDrinkResult1,dtTestTime2,nVerifyID2,DrinkImage2,nDrinkResult2,
dtTestTime3,nVerifyID3,DrinkImage3,nDrinkResult3,strBeginWorkGUID1,strBeginWorkGUID2,strBeginWorkGUID3,
strBeginWorkGUID4,dtTestTime4,nVerifyID4,DrinkImage4,nDrinkResult4,strICCheckResult,nFlowState ");
            strSql.Append(" FROM VIEW_Plan_BeginWork ");
            string strWhere = string.Format(@" (dtStartTime >='{0}' or nPlanState in (4,5,6)) 
and nPlanState in (4,5,6,7,8) and strTrainJiaoluGUID in  (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite
where strSiteGUID = '{1}') order by nPlanState,dtStartTime",beginTime,clientGUID);
            DataSet set = dal.GetList(strWhere);
            List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans = DataTableToList(set.Tables[0]);
            return GetPlanList(vPlans);
        }
         public List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> GetCQTZ(string clientGUID, string beginTime, string endTime)
        {
            string strSql = @"select *,'' as strStationName,'' as strStationNumber,strPlaceName,'' as strStationGUID from VIEW_Plan_BeginWork  
                where  dtBeginworkTime >=@BeginTime and dtBeginworkTime < @EndTime   and nPlanState >=7 and strTrainJiaoluGUID in  
                (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = @strSiteGUID) order by dtBeginworkTime";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BeginTime",beginTime),
                new SqlParameter("EndTime",endTime),
                new SqlParameter("strSiteGUID",clientGUID)
            };
             DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
            List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans = DataTableToList(dt);
            return GetPlanList(vPlans);
        }
        

        
        public List<TF.RunSafty.Model.VIEW_Plan_BeginWork> GetBeginWorkOfSite(string clientGUID, string beginTime, string endTime)
        {
            string strWhere = string.Format(@" (dtStartTime >='{0}' or nPlanState in (4,5,6)) 
and nPlanState in (4,5,6,7,8) and strTrainJiaoluGUID in  (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite
where strSiteGUID = '{1}') order by nPlanState,dtStartTime", beginTime, clientGUID);
            DataSet set = dal.GetList(strWhere);
            List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans = DataTableToList(set.Tables[0]);
            return vPlans;
        }
        private List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> GetPlanList(List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans)
        {
            List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> lPlans = new List<Model.InterfaceModel.mChuqinPlansOfClient>();
            TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_BeginWork plan in vPlans)
                {
                    clientPlan = new TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient();
                    TF.RunSafty.Model.InterfaceModel.ChuqinGroup chuqinGroup=new TF.RunSafty.Model.InterfaceModel.ChuqinGroup();
                    clientPlan.chuqinGroup = chuqinGroup;
                    TF.RunSafty.Model.InterfaceModel.ChuqinPlace cPlace=new TF.RunSafty.Model.InterfaceModel.ChuqinPlace();
                    chuqinGroup.group = new TF.RunSafty.Model.InterfaceModel.NameGroup();
                    chuqinGroup.group.place = cPlace;
                    cPlace.placeID = plan.strPlaceID;
                    cPlace.placeName = plan.strPlaceName;
                    chuqinGroup.group.groupID = plan.strGroupGUID;
                    chuqinGroup.group.station = new TF.RunSafty.Model.InterfaceModel.Station();
                    chuqinGroup.group.station.stationID = plan.strStationGUID;
                    chuqinGroup.group.station.stationName = plan.strStationName;
                    if (plan.strStationNumber.HasValue)
                    {
                        chuqinGroup.group.station.stationNumber = plan.strStationNumber.ToString();
                    }
                    chuqinGroup.group.trainman1 = new TF.RunSafty.Model.InterfaceModel.Trainman();
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
                    chuqinGroup.group.trainman2 = new TF.RunSafty.Model.InterfaceModel.Trainman();
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
                    chuqinGroup.group.trainman3 = new TF.RunSafty.Model.InterfaceModel.Trainman();
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
                    chuqinGroup.group.trainman4 = new TF.RunSafty.Model.InterfaceModel.Trainman();
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
                    TF.RunSafty.Model.InterfaceModel.TrainPlan trainPlan = new TF.RunSafty.Model.InterfaceModel.TrainPlan();
                    clientPlan.trainPlan = trainPlan;
                    if (plan.dtCreateTime.HasValue)
                    {
                        trainPlan.createTime = plan.dtCreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    clientPlan.nFlowState = plan.nFlowState;
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
                        trainPlan.planStateID = plan.nPlanState.ToString();
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
                    if (plan.dtBeginWorkTime.HasValue)
                    {
                        clientPlan.beginWorkTime = plan.dtBeginWorkTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        clientPlan.trainPlan.kaiCheTime=plan.dtChuQinTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
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
                    chuqinGroup.testAlcoholInfo1 = new TF.RunSafty.Model.InterfaceModel.TestAlcoholInfo();
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
                    chuqinGroup.testAlcoholInfo2 = new TF.RunSafty.Model.InterfaceModel.TestAlcoholInfo();
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
                    chuqinGroup.testAlcoholInfo3 = new TF.RunSafty.Model.InterfaceModel.TestAlcoholInfo();
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
                    chuqinGroup.testAlcoholInfo4 = new TF.RunSafty.Model.InterfaceModel.TestAlcoholInfo();
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
		
        #endregion  ExtensionMethod

        #region 获取指定客户端下的出勤计划
        private class pTrainjiaoluPlan : ParamBase
        {
            public Plan data;
        }
        public class Plan
        {
            private string _siteID;
            private string _begintime;
            private string _endtime;

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
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<mChuqinPlansOfClient> data;
        }

        public string GetChuqinPlansOfSite(string data)
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
                    List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> plans = this.GetChuqinPlansOfSite(clientID, starttime, endtime);

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

        #region 获取指定客户端下指定乘务员的出勤计划
        private class pTrainjiaoluPlan1 : ParamBase
        {
            public Plan1 data;
        }
        public class Plan1
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
                get
                {
                    return _trainmanID;
                }
                set
                {
                    _trainmanID = value;
                }
            }
        }
        private class JsonModel1
        {
            public int result;
            public string resultStr;
            public mChuqinPlansOfClient data;
        }
        public string GetChuqinPlansOfTrainmanInSite(string data)
        {
            JsonModel1 jsonModel = new JsonModel1();
            TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                pTrainjiaoluPlan1 paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<pTrainjiaoluPlan1>(data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(data))
                {
                    string clientID = paramModel.data.siteID;
                    string strTrainmanid = paramModel.data.trainmanID;
                    List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> plans = bllPlan.GetChuqinPlansOfTrainmanInSite(clientID, strTrainmanid);
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

    }
}

