/**  版本信息模板在安装目录下，可自行修改。
* TAB_ReadDocPlan.cs
*
* 功 能： N/A
* 类 名： TAB_ReadDocPlan
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/15 10:42:19   N/A    初版
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
using System.Data.SqlClient;
namespace TF.RunSafty.BLL
{
	/// <summary>
	/// TAB_ReadDocPlan
	/// </summary>
	public partial class TAB_ReadDocPlan
	{
		private readonly TF.RunSafty.DAL.TAB_ReadDocPlan dal=new TF.RunSafty.DAL.TAB_ReadDocPlan();
		public TAB_ReadDocPlan()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
        //public int GetMaxId()
        //{
        //    return dal.GetMaxId();
        //}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int nId)
		{
			return dal.Exists(nId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(TF.RunSafty.Model.TAB_ReadDocPlan model)
		{
			return dal.Add(model);
		}
        public void Add(DataTable table)
        {
             dal.Add(table);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_ReadDocPlan model)
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
        public bool DeleteOldPlan(string strFileGUID)
        {
            return dal.DeleteOldPlan(strFileGUID);

        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.TAB_ReadDocPlan GetModel(int nId)
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
		public List<TF.RunSafty.Model.TAB_ReadDocPlan> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.TAB_ReadDocPlan> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.TAB_ReadDocPlan> modelList = new List<TF.RunSafty.Model.TAB_ReadDocPlan>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.TAB_ReadDocPlan model;
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
        /// 获取传达记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetReadingHistoryLate(string strWhere, List<SqlParameter> parameters)
        {
            return dal.GetReadingHistoryLate(strWhere,parameters);
        }
        /// <summary>
        /// 获取补传记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetReadingHistory(string strWhere, List<SqlParameter> parameters)
        {
            return dal.GetReadingHistory(strWhere,parameters);
        }
        public DataTable GetReadingHistoryOfTrainman(string strTrainmanGUID,string strFileType)
        {
            return dal.GetReadingHistoryOfTrainman(strTrainmanGUID,strFileType);
        }

        /// <summary>
        /// 判断该通告是否已经阅读
        /// </summary>
        /// <param name="strFileGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <returns></returns>
        public bool IsNoticeReaded(string strFileGUID,string strTrainmanGUID)
        {
            return true;
        }

        public void UpdateReadTime(string strFileGUID, string strTrainmanGUID, string strReadTime)
        { 
            string strWhere = string.Format(" StrFileGUID='{0}' AND StrTrainmanGUID='{1}' ", strFileGUID, strTrainmanGUID);
            TF.CommonUtility.LogClass.log(strWhere);
            List<TF.RunSafty.Model.TAB_ReadDocPlan> plans = this.GetModelList(strWhere);
            if (plans != null && plans.Count > 0)
            {
                TF.RunSafty.Model.TAB_ReadDocPlan plan = plans[0];
                DateTime readTime=DateTime.Parse(strReadTime);
                if (plan.NReadCount.HasValue && plan.NReadCount.Value>0) //已经阅读
                {
                    plan.NReadCount++;
                    plan.DtLastReadTime =readTime;
                }
                else
                {
                    plan.DtFirstReadTime = readTime;
                    plan.DtLastReadTime = readTime;
                    plan.NReadCount = 1;
                    
                }
                if (this.Update(plan))
                {
                    TF.CommonUtility.LogClass.log("阅读记录更新成功");
                }
                else
                {
                    TF.CommonUtility.LogClass.log("阅读记录更新失败");
                }
            }
        }
		#endregion  ExtensionMethod

        #region 接口
        #endregion
    }
}

