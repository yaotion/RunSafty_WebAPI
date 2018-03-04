/**  版本信息模板在安装目录下，可自行修改。
* TAB_MsgCallWork.cs
*
* 功 能： N/A
* 类 名： TAB_MsgCallWork
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-10-10 13:06:43   N/A    初版
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
namespace TF.RunSafty.BLL
{
	/// <summary>
	/// TAB_MsgCallWork
	/// </summary>
	public partial class TAB_MsgCallWork
	{
		private readonly TF.RunSafty.DAL.TAB_MsgCallWork dal=new TF.RunSafty.DAL.TAB_MsgCallWork();
		public TAB_MsgCallWork()
		{}
		#region  BasicMethod

		 

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(TF.RunSafty.Model.TAB_MsgCallWork model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_MsgCallWork model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int nId)
		{
			
			return dal.Delete(nId);
		}

        public bool Delete(string TrainPlanGUID, string TrainmanGUID)
        {
            return dal.Delete(TrainPlanGUID, TrainmanGUID);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.TAB_MsgCallWork GetModel(int nId)
		{
			
			return dal.GetModel(nId);
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
		public List<TF.RunSafty.Model.TAB_MsgCallWork> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.TAB_MsgCallWork> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.TAB_MsgCallWork> modelList = new List<TF.RunSafty.Model.TAB_MsgCallWork>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.TAB_MsgCallWork model;
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
        public List<TF.RunSafty.Model.TAB_MsgCallWork> GetAllMsg(string nBeginState, string nEndState)
        {
            DataSet set = dal.GetAllMsg(nBeginState, nEndState);
            DataTable table = set.Tables[0];
            DataView view = table.DefaultView;
            table = view.ToTable();
            return DataTableToListGetAllMsg(table);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.TAB_MsgCallWork> DataTableToListGetAllMsg(DataTable dt)
        {
            List<TF.RunSafty.Model.TAB_MsgCallWork> modelList = new List<TF.RunSafty.Model.TAB_MsgCallWork>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.TAB_MsgCallWork model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModelForAllMsg(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public List<TF.RunSafty.Model.TAB_MsgCallWork> GetPlaceList(List<TF.RunSafty.Model.TAB_MsgCallWork> placeList)
        {
            if (placeList != null)
            {
                List<TF.RunSafty.Model.TAB_MsgCallWork> resultList = new List<TF.RunSafty.Model.TAB_MsgCallWork>();
                foreach (TF.RunSafty.Model.TAB_MsgCallWork place in placeList)
                {
                    TF.RunSafty.Model.TAB_MsgCallWork model = new TF.RunSafty.Model.TAB_MsgCallWork();
                    model.dtCallTime = place.dtCallTime;

                    model.dtRecvTime = place.dtRecvTime;

                    model.dtSendTime = place.dtSendTime;
                    model.nCallTimes = place.nCallTimes;
                    model.nRecvCount = place.nRecvCount;
                    model.nSendCount = place.nSendCount;
                    model.eCallState = place.eCallState;
                    model.eCallType = place.eCallType;
                    model.strMsgGUID = place.strMsgGUID;
                    model.strMobileNumber = place.strMobileNumber;
                    model.strPlanGUID = place.strPlanGUID;
                    model.strRecvMsgContent = place.strRecvMsgContent;
                    model.strRecvUser = place.strRecvUser;
                    model.strSendMsgContent = place.strSendMsgContent;
                    model.strSendUser = place.strSendUser;
                    model.strTrainmanGUID = place.strTrainmanGUID;
                    model.strTrainmanName = place.strTrainmanName;
                    model.strTrainmanNumber = place.strTrainmanNumber;
                    model.dtChuQinTime = place.dtChuQinTime;
                    model.dtStartTime = place.dtStartTime;
                    model.strTrainNo = place.strTrainNo;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
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
        /// 根据短信GUID获取记录
        /// </summary>
        /// <param name="strMsgGUID"></param>
        /// <returns></returns>
        public TF.RunSafty.Model.TAB_MsgCallWork GetModelByGUID(string strMsgGUID)
        {
            string strWhere = string.Format(" strGUID='{0}' ", strMsgGUID);
            List<TF.RunSafty.Model.TAB_MsgCallWork> models = GetModelList(strWhere);
            if (models != null && models.Count > 0)
                return models[0];
            return null;
        }


        public TF.RunSafty.Model.TAB_MsgCallWork GetLatestCallWorkByTrainman(string strTrainmanGUID)
        {
            string strWhere = string.Format(" strTrainmanGUID='{0}' and nId=(select max(nId) from TAB_MsgCallWork where strTrainmanGUID='{0}') ", strTrainmanGUID);
            List<TF.RunSafty.Model.TAB_MsgCallWork> models = GetModelList(strWhere);
            if (models != null && models.Count > 0)
                return models[0];
            return null;
        }


        /// <summary>
        /// 根据电话号码和接收时间获取叫班表记录
        /// </summary>
        /// <param name="telePhone"></param>
        /// <param name="receiveTime"></param>
        /// <returns></returns>
        public TF.RunSafty.Model.TAB_MsgCallWork GetModelByTelephone(string telePhone, string receiveTime)
        {
            //根据电话号码找到乘务员信息
           DataTable table= ThinkFreely.RunSafty.Trainman.GetTrainmanByTelephone(telePhone);
           if (table != null && table.Rows.Count > 0)
           {
               string strTrainmanGUID = table.Rows[0]["strTrainmanGUID"].ToString();
               return GetLatestCallWorkByTrainman(strTrainmanGUID);
           }
           else
           {
               throw new Exception("无法根据电话号码不到乘务员信息");
           } 
        }
		#endregion  ExtensionMethod
	}
}

