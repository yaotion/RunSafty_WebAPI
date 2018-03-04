/**  版本信息模板在安装目录下，可自行修改。
* TAB_Plan_Train.cs
*
* 功 能： N/A
* 类 名： TAB_Plan_Train
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/29 9:25:00   N/A    初版
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
using TF.RunSafty.Model.InterfaceModel;


namespace TF.RunSafty.BLL.Plan
{
	/// <summary>
	/// TAB_Plan_Train
	/// </summary>
	public partial class TAB_Plan_Train
	{
		private readonly TF.RunSafty.DAL.TAB_Plan_Train dal=new TF.RunSafty.DAL.TAB_Plan_Train();
		public TAB_Plan_Train()
		{}
		#region  BasicMethod

		 
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(TF.RunSafty.Model.TAB_Plan_Train model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_Plan_Train model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int nid)
		{
			
			return dal.Delete(nid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string strTrainPlanGUID,DateTime dtStartTime,int nPlanState)
		{
			
			return dal.Delete(strTrainPlanGUID,dtStartTime,nPlanState);
		}
		 
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.TAB_Plan_Train GetModel(int nid)
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
		public List<TF.RunSafty.Model.TAB_Plan_Train> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.TAB_Plan_Train> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.TAB_Plan_Train> modelList = new List<TF.RunSafty.Model.TAB_Plan_Train>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.TAB_Plan_Train model;
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
        public List<TF.RunSafty.Model.TrainManPlanforLed> DataTableToListForLed(DataTable dt)
        {
            List<TF.RunSafty.Model.TrainManPlanforLed> modelList = new List<TF.RunSafty.Model.TrainManPlanforLed>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.TrainManPlanforLed model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModelFoLed(dt.Rows[n]);
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




        public List<TF.RunSafty.Model.TrainManPlanforLed> GetPlanTrain(string strClientGUID, string strMinute)
        {
            DataSet set = dal.GetAllList(strClientGUID, strMinute);
            DataTable table = set.Tables[0];
            DataView view = table.DefaultView;
            table = view.ToTable();
            return DataTableToListForLed(table);
        }


        public List<TF.RunSafty.Model.TAB_Plan_Train> GetPlaceList(List<TF.RunSafty.Model.TAB_Plan_Train> placeList)
        {
            if (placeList != null)
            {
                List<TF.RunSafty.Model.TAB_Plan_Train> resultList = new List<TF.RunSafty.Model.TAB_Plan_Train>();
                foreach (TF.RunSafty.Model.TAB_Plan_Train place in placeList)
                {
                    TF.RunSafty.Model.TAB_Plan_Train model = new TF.RunSafty.Model.TAB_Plan_Train();
                    model.dtArriveTime = place.dtArriveTime;
                    model.dtCallTime = place.dtCallTime;
                    model.dtChuQinTime = place.dtChuQinTime;

                    model.dtCreateTime = place.dtCreateTime;
                    model.dtFirstStartTime = place.dtFirstStartTime;
                    model.dtLastArriveTime = place.dtLastArriveTime;
                    model.dtRealStartTime = place.dtRealStartTime;

                    model.dtStartTime = place.dtStartTime;
                    model.nDragType = place.nDragType;
                    model.nid = place.nid;
                    model.nKehuoID = place.nKehuoID;

                    model.nNeedRest = place.nNeedRest;
                    model.nPlanState = place.nPlanState;
                    model.nPlanType = place.nPlanType;
                    model.nRemarkType = place.nRemarkType;


                    model.nTrainmanTypeID = place.nTrainmanTypeID;
                    model.strBak1 = place.strBak1;
                    model.strCreateSiteGUID = place.strCreateSiteGUID;
                    model.strCreateUserGUID = place.strCreateUserGUID;

                    model.strEndStation = place.strEndStation;
                    model.strMainPlanGUID = place.strMainPlanGUID;
                    model.strPlaceID = place.strPlaceID;
                    model.strRemark = place.strRemark;


                    model.strStartStation = place.strStartStation;
                    model.strTrainJiaoluGUID = place.strTrainJiaoluGUID;
                    model.strTrainNo = place.strTrainNo;
                    model.strTrainNumber = place.strTrainNumber;


                    model.strTrainPlanGUID = place.strTrainPlanGUID;
                    model.strTrainTypeName = place.strTrainTypeName;

                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }






		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod

        #region 接口
        #region 添加行车计划
        private class ParamModel : ParamBase
        {
            public PlanData data;
        }
        public class PlanData
        {
            public TF.RunSafty.Model.InterfaceModel.CreatedTrainPlan trainPlan;
            public User user;
            public Site site;
        }
        public class User
        {
            public string userID = "";
            public string userName = "";
        }
        public class Site
        {
            public string siteID = "";
            public string siteName = "";
        }
        public class PlanInfo
        {
            public string planID = "";
        }
        public class JsonModel : ResultJsonBase
        {
            public PlanInfo data = new PlanInfo();
        }

        public  string Add(string data)
        {
            JsonModel jsonModel = new JsonModel();
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(data);
            TF.RunSafty.BLL.Plan.TAB_Plan_Train bllPlan = new TF.RunSafty.BLL.Plan.TAB_Plan_Train();
            TF.RunSafty.Model.TAB_Plan_Train plan = new TF.RunSafty.Model.TAB_Plan_Train();
            try
            {
                DateTime dtStartTime;
                if (DateTime.TryParse(param.data.trainPlan.startTime, out dtStartTime))
                {
                    plan.dtStartTime = dtStartTime;
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.dragTypeID))
                {
                    plan.nDragType = int.Parse(param.data.trainPlan.dragTypeID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.kehuoID))
                {
                    plan.nKehuoID = int.Parse(param.data.trainPlan.kehuoID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.planTypeID))
                {
                    plan.nPlanType = int.Parse(param.data.trainPlan.planTypeID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.remarkTypeID))
                {
                    plan.nRemarkType = int.Parse(param.data.trainPlan.remarkTypeID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.trainmanTypeID))
                {
                    plan.nTrainmanTypeID = int.Parse(param.data.trainPlan.trainmanTypeID);
                }
                plan.strCreateSiteGUID = param.data.site.siteID;
                plan.strCreateUserGUID = param.data.user.userID;
                plan.strPlaceID = param.data.trainPlan.placeID;
                plan.strStartStation = param.data.trainPlan.startStationID;
                plan.strEndStation = param.data.trainPlan.endStationID;
                plan.strTrainJiaoluGUID = param.data.trainPlan.trainjiaoluID;
                plan.strTrainNo = param.data.trainPlan.trainNo;
                plan.strTrainNumber = param.data.trainPlan.trainNumber;
                plan.strTrainTypeName = param.data.trainPlan.trainTypeName;
                plan.strCreateUserGUID = param.data.user.userID;
                plan.nPlanState = (int)TRsPlanState.psReceive;
                plan.dtCreateTime = DateTime.Now;
                plan.dtRealStartTime = plan.dtStartTime;
                string planGUID = Guid.NewGuid().ToString();
                plan.strTrainPlanGUID = planGUID;
                if (bllPlan.Add(plan) > 0)
                {
                    jsonModel.data.planID = planGUID;
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");

            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel).Replace(":null", ":\"\"");
            return result;
        }
        #endregion

        #region 添加可编辑行车计划
        private class eParamModel : ParamBase
        {
            public ePlanData data;
        }
        public class ePlanData
        {
            public TF.RunSafty.Model.InterfaceModel.CreatedTrainPlan trainPlan;
            public User user;
            public Site site;
        }
        public class eUser
        {
            public string userID = "";
            public string userName = "";
        }
        public class eSite
        {
            public string siteID = "";
            public string siteName = "";
        }
        public class ePlanInfo
        {
            public string planID = "";
        }
        public class eJsonModel : ResultJsonBase
        {
            public ePlanInfo data = new ePlanInfo();
        }

        public string AddEditabledTrainPlan( string data)
        {
            eJsonModel jsonModel = new eJsonModel();
            eParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<eParamModel>(data); 
            TF.RunSafty.Model.TAB_Plan_Train plan = new TF.RunSafty.Model.TAB_Plan_Train();
            try
            {
                DateTime dtStartTime;
                if (DateTime.TryParse(param.data.trainPlan.startTime, out dtStartTime))
                {
                    plan.dtStartTime = dtStartTime;
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.dragTypeID))
                {
                    plan.nDragType = int.Parse(param.data.trainPlan.dragTypeID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.kehuoID))
                {
                    plan.nKehuoID = int.Parse(param.data.trainPlan.kehuoID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.planTypeID))
                {
                    plan.nPlanType = int.Parse(param.data.trainPlan.planTypeID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.remarkTypeID))
                {
                    plan.nRemarkType = int.Parse(param.data.trainPlan.remarkTypeID);
                }
                if (!string.IsNullOrEmpty(param.data.trainPlan.trainmanTypeID))
                {
                    plan.nTrainmanTypeID = int.Parse(param.data.trainPlan.trainmanTypeID);
                }
                plan.strCreateSiteGUID = param.data.site.siteID;
                plan.strCreateUserGUID = param.data.user.userID;
                plan.strPlaceID = param.data.trainPlan.placeID;
                plan.strStartStation = param.data.trainPlan.startStationID;
                plan.strEndStation = param.data.trainPlan.endStationID;
                plan.strTrainJiaoluGUID = param.data.trainPlan.trainjiaoluID;
                plan.strTrainNo = param.data.trainPlan.trainNo;
                plan.strTrainNumber = param.data.trainPlan.trainNumber;
                plan.strTrainTypeName = param.data.trainPlan.trainTypeName;
                plan.dtCreateTime = DateTime.Now;
                plan.dtRealStartTime = plan.dtStartTime;
                string planGUID = Guid.NewGuid().ToString();
                plan.strTrainPlanGUID = planGUID;
                plan.nPlanState = (int)TRsPlanState.psEdit;
                if (!string.IsNullOrEmpty(param.data.trainPlan.kaiCheTime))
                {
                    DateTime dtChuqinTime;
                    if (DateTime.TryParse(param.data.trainPlan.kaiCheTime, out dtChuqinTime))
                    {
                        plan.dtChuQinTime = dtChuqinTime;
                    }
                }

                if (this.Add(plan) > 0)
                {
                    jsonModel.data.planID = planGUID;
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");

            }

            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter).Replace(":null", ":\"\"");
            return result;
        }
        #endregion
        #endregion
    }
}

