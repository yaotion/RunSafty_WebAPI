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
using System.Collections.Generic;
using System.Linq;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using TF.CommonUtility;
using TF.Api.Utilities;
namespace TF.RunSafty.BLL.Plan
{
	/// <summary>
	/// VIEW_Plan_Trainman
	/// </summary>
	public partial class VIEW_Plan_Trainman
	{
		private readonly TF.RunSafty.DAL.VIEW_Plan_Trainman dal=new TF.RunSafty.DAL.VIEW_Plan_Trainman();
		public VIEW_Plan_Trainman()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.VIEW_Plan_Trainman GetModel(int nid)
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
		public List<TF.RunSafty.Model.VIEW_Plan_Trainman> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.VIEW_Plan_Trainman> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.VIEW_Plan_Trainman> modelList = new List<TF.RunSafty.Model.VIEW_Plan_Trainman>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.VIEW_Plan_Trainman model;
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
        //GetPlans函数实际上不应该出现在这个类中，有空的话建议移出去
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vPlans"></param>
        /// <returns></returns>
        private List<PlansOfClient> GetPlans(List<TF.RunSafty.Model.VIEW_Plan_Trainman> vPlans)
        {
            List<PlansOfClient> plans = new List<PlansOfClient>();
            TF.RunSafty.Model.InterfaceModel.TrainPlan tPlan = null;
            TF.RunSafty.Model.InterfaceModel.PlansOfClient pClient = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_Trainman plan in vPlans)
                {
                    tPlan = new TF.RunSafty.Model.InterfaceModel.TrainPlan();
                    if (plan.dtCreateTime.HasValue)
                    {
                        tPlan.createTime = plan.dtCreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    tPlan.createSiteGUID = plan.strCreateSiteGUID;
                    tPlan.createSiteName = plan.strCreateSiteName;
                    tPlan.createUserGUID = plan.strCreateUserGUID;
                    tPlan.createUserName = plan.strCreateUserName;
                    if (plan.nDragType.HasValue)
                    {
                        tPlan.dragTypeID = plan.nDragType.ToString();
                    }
                    tPlan.endStationID = plan.strEndStation;
                    tPlan.endStationName = plan.strEndStationName;
                    tPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    tPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    tPlan.kehuoID = plan.nKehuoID.ToString();
                    tPlan.kehuoName = plan.strKehuoName;
                    tPlan.mainPlanGUID = plan.strMainPlanGUID;
                    tPlan.placeID = plan.strPlaceID;
                    tPlan.placeName = plan.strPlaceName;
                    tPlan.planID = plan.strTrainPlanGUID;
                    tPlan.planStateID = plan.nPlanState.ToString();
                    tPlan.planStateName = plan.strPlanStateName;
                    tPlan.planTypeID = plan.nPlanType.ToString();
                    tPlan.planTypeName = plan.strPlanTypeName;
                    tPlan.remarkTypeID = plan.nRemarkType.ToString();
                    tPlan.remarkTypeName = plan.strRemarkTypeName;
                    tPlan.strRemark = plan.strRemark;
                    tPlan.startStationID = plan.strStartStation;
                    tPlan.startStationName = plan.strStartStationName;
                    tPlan.dragTypeName = plan.nDragTypeName;
                    tPlan.planID = plan.strTrainPlanGUID;
                    tPlan.strWaiQinClientGUID = plan.strWaiQinClientGUID;
                    tPlan.strWaiQinClientNumber = plan.strWaiQinClientNumber;
                    tPlan.strWaiQinClientName = plan.strWaiQinClientName;
                    

                    if (plan.dtStartTime.HasValue)
                    {
                        tPlan.startTime = plan.dtStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtSendTime.HasValue)
                    {
                        tPlan.sendPlanTime = plan.dtSendTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtRecvTime.HasValue)
                    {
                        tPlan.recvPlanTime = plan.dtRecvTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtCreateTime.HasValue)
                    {
                        tPlan.createTime = plan.dtCreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");   
                    }
                    if (plan.dtChuQinTime.HasValue)
                    { 
                        tPlan.kaiCheTime = plan.dtChuQinTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        //tPlan.kaiCheTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        tPlan.firstStartTime = plan.dtFirstStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtRealStartTime.HasValue)
                    {
                        tPlan.realStartTime = plan.dtRealStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtBeginWorkTime.HasValue)
                    {
                        tPlan.beginWorkTime = plan.dtBeginWorkTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    rests rest = new rests();
                    
                    
                    
                    if (plan.dtArriveTime.HasValue)
                    {
                        tPlan.dtArriveTime = plan.dtArriveTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        rest.dtArriveTime = plan.dtArriveTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.dtCallTime.HasValue)
                    {
                        tPlan.dtCallTime = plan.dtCallTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        rest.dtCallTime = plan.dtCallTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nNeedRest.HasValue)
                    {
                        tPlan.nNeedRest = plan.nNeedRest.Value;
                        rest.nNeedRest = plan.nNeedRest.ToString();
                    }
                    tPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    tPlan.trainNo = plan.strTrainNo;
                    tPlan.trainNumber = plan.strTrainNumber;
                    tPlan.trainTypeName = plan.strTrainTypeName;
                    tPlan.trainmanTypeName = plan.strTrainmanTypeName;
                    NameGroup group = new NameGroup();
                    group.groupID = plan.strGroupGUID;
                    group.trainman1 = new Trainman();
                    group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    group.trainman1.trainmanName = plan.strTrainmanName1;
                    group.trainman1.trainmanNumber = plan.strTrainmanNumber1;
                    group.trainman1.callWorkState = plan.nMsgState1.Value;
                    group.trainman1.callWorkID = plan.strMsgID1;
                    group.trainman1.telNumber = plan.strMobileNumber1;

                    group.trainman2 = new Trainman();
                    group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    group.trainman2.trainmanName = plan.strTrainmanName2;
                    group.trainman2.trainmanNumber = plan.strTrainmanNumber2;
                    group.trainman2.callWorkState = plan.nMsgState2.Value;
                    group.trainman2.callWorkID = plan.strMsgID2;
                    group.trainman2.telNumber = plan.strMobileNumber2;

                    group.trainman3 = new Trainman();
                    group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    group.trainman3.trainmanName = plan.strTrainmanName3;
                    group.trainman3.trainmanNumber = plan.strTrainmanNumber3;
                    group.trainman3.callWorkState = plan.nMsgState3.Value;
                    group.trainman3.callWorkID = plan.strMsgID3;
                    group.trainman3.telNumber = plan.strMobileNumber3;

                    group.trainman4 = new Trainman();
                    group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    group.trainman4.trainmanName = plan.strTrainmanName4;
                    group.trainman4.trainmanNumber = plan.strTrainmanNumber4;
                    group.trainman4.callWorkState = plan.nMsgState4.Value;
                    group.trainman4.callWorkID = plan.strMsgID4;
                    group.trainman4.telNumber = plan.strMobileNumber4;

                    if (plan.dtArriveTime.HasValue)
                    {
                        group.arriveTime = plan.dtArriveTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    group.groupID = plan.strGroupGUID;
                    group.place = new ChuqinPlace();
                    group.place.placeID = plan.strPlaceID;
                    group.place.placeName = plan.strPlaceName;
                    group.station = new Station();
                    group.trainPlanID = plan.strTrainPlanGUID;
                    pClient = new PlansOfClient();
                    pClient.trainPlan = tPlan;
                    pClient.group = group;
                    pClient.rest = rest;

                    plans.Add(pClient);
                }
            }
            return plans;
        }


	    public TF.RunSafty.Model.InterfaceModel.PlansOfClient GetPlanByGUID(string strTrainplanGUID)
	    {
	        List<TF.RunSafty.Model.VIEW_Plan_Trainman> vPlans = this.GetTrainmanPlanByGUID(strTrainplanGUID);
	        if (vPlans != null && vPlans.Count > 0)
	        {
	            return GetPlans(vPlans)[0];
	        }
            return null;
	    }

	    public List<TF.RunSafty.Model.InterfaceModel.PlansOfClient> GetPlansOfNeedRest(string strTrainmanPlanGUIDList)
	    {
	        string[] list = strTrainmanPlanGUIDList.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
	        string guidList = "'"+string.Join("','", list)+"'";
            string strWhere = string.Format(" strTrainplanGUID in ({0}) and nNeedRest>=1 and nPlanState >= 4 ",guidList);
            DataSet set = dal.GetList(strWhere);
            List<TF.RunSafty.Model.VIEW_Plan_Trainman> vPlans=DataTableToList(set.Tables[0]);
            return GetPlans(vPlans);
	    }
        public List<TF.RunSafty.Model.InterfaceModel.PlansOfClient> GetPlans(string startTime, string endTime, string strTrainJiaoluGUID)
        {
            List<TF.RunSafty.Model.VIEW_Plan_Trainman> vPlans = this.GetTrainjiaoluPlansOfClient(startTime, endTime, strTrainJiaoluGUID);
            return GetPlans(vPlans);
        }
        public List<TF.RunSafty.Model.VIEW_Plan_Trainman> GetTrainjiaoluPlansOfClient( string startTime, string endTime, string strTrainJiaoluGUID)
        {
            string strWhere = string.Format(" (dtStartTime >='{0}' or dtStartTime <= 36524)and dtStartTime <= '{1}' and nPlanState <> 0 and strTrainJiaoluGUID = '{2}'   order by dtStartTime ", startTime, endTime, strTrainJiaoluGUID);
            DataSet set = dal.GetList(strWhere);
            return DataTableToList(set.Tables[0]);
        }
        public List<TF.RunSafty.Model.VIEW_Plan_Trainman> GetTrainmanPlanByGUID(string strTrainmanPlanGUID)
        {
            string strWhere = string.Format(" strTrainplanGUID='{0}' ",strTrainmanPlanGUID);
            DataSet set = dal.GetList(strWhere);
            return DataTableToList(set.Tables[0]);
        }
        /// <summary>
        /// 获取指定时间段内已下发的行车计划
        /// </summary>
        /// <param name="clientGUID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="strTrainJiaoluGUID"></param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.InterfaceModel.PlansOfClient> GetTrainjiaoluPlansSentOfClient(string clientGUID, string startTime, string endTime, string strTrainJiaoluGUID)
        {



            //先根据clientID,trainjiaoluGUID,查找出勤点的编号
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            TF.RunSafty.BLL.Place.VIEW_Base_Site_DutyPlace bllPlace=new TF.RunSafty.BLL.Place.VIEW_Base_Site_DutyPlace();
            string strWhere=string.Format(" strSiteGUID='{0}' and strTrainJiaoluGUID='{1}' ",clientGUID,strTrainJiaoluGUID);
            DataTable table= bllPlace.GetList(strWhere).Tables[0];

            if (table != null && table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    builder.AppendFormat("'{0}'",table.Rows[i]["strPlaceID"].ToString());
                    if (i < table.Rows.Count - 1)
                    {
                        builder.Append(",");
                    }
                }
            }
            else
            {
                TF.CommonUtility.LogClass.log("wwwwwwwwwwwwwddddddddddddddddw" + clientGUID + startTime + endTime + strTrainJiaoluGUID);
                throw new Exception("该客户端下的行车区段没有出勤点");
            }
            string placeIDs = builder.ToString();
            string strJlCondition = string.Empty;

            if (strTrainJiaoluGUID != string.Empty)
            {
                strJlCondition = " and (strTrainJiaoluGUID = '{0}' or strTrainJiaoluGUID in (select strSubTrainJiaoluGUID from TAB_Base_TrainJiaolu_SubDetail where strTrainJiaoluGUID = '{0}'))";
                strJlCondition = string.Format(strJlCondition, strTrainJiaoluGUID);
            }
            else
            {
                strJlCondition = " and (strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID='{0}') ) ";
                strJlCondition = string.Format(strJlCondition, clientGUID);
            }

            strWhere = string.Format(" (dtStartTime >='{0}' or dtStartTime <= 36524)and dtStartTime <= '{1}' and nPlanState <> 0 {2} and nPlanState >=2 and strPlaceID in ({3}) order by dtStartTime asc,nid asc", startTime, endTime, strJlCondition, placeIDs);
            DataSet set = dal.GetList(strWhere);
            return GetPlans(DataTableToList(set.Tables[0]));
        }
		#endregion  ExtensionMethod


        #region 接口
        #region 获取指定客户端下指定交路的已发布行车计划
        private class pTrainjiaoluPlan : ParamBase
        {
            public Plan data;
        }
        public class Plan
        {
            private string _siteID;
            private string _trainjiaoluID;
            private string _begintime;
            private string _endtime;
            [NotNull]
            public string siteID { get { return _siteID; } set { _siteID = value; } }
            [NotNull]
            public string trainjiaoluID { get { return _trainjiaoluID; } set { _trainjiaoluID = value; } }
            [NotNull]
            public string begintime { get { return _begintime; } set { _begintime = value; } }
            [NotNull]
            public string endtime { get { return _endtime; } set { _endtime = value; } }
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<PlansOfClient> data;
        }
        public string GetTrainjiaoluPlansSentOfClient(string data)
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
                    string strTrainjiaoluGUID = paramModel.data.trainjiaoluID;
                    string clientID = paramModel.data.siteID;
                    List<TF.RunSafty.Model.InterfaceModel.PlansOfClient> plans = this.GetTrainjiaoluPlansSentOfClient(clientID, starttime, endtime, strTrainjiaoluGUID);
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
        #endregion 获取指定客户端下指定乘务员的行车计划
         

        #region
        #endregion
        #endregion
    }
}

