/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_Site_DutyPlace.cs
*
* 功 能： N/A
* 类 名： TAB_Base_Site_DutyPlace
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/17 9:42:50   N/A    初版
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
	/// TAB_Base_Site_DutyPlace
	/// </summary>
	public partial class TAB_Base_Site_DutyPlace
	{
		private readonly TF.RunSafty.DAL.TAB_Base_Site_DutyPlace dal=new TF.RunSafty.DAL.TAB_Base_Site_DutyPlace();
		public TAB_Base_Site_DutyPlace()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

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
		public int  Add(TF.RunSafty.Model.TAB_Base_Site_DutyPlace model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_Base_Site_DutyPlace model)
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
		 
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.TAB_Base_Site_DutyPlace GetModel(int nId)
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
		public List<TF.RunSafty.Model.TAB_Base_Site_DutyPlace> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.TAB_Base_Site_DutyPlace> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.TAB_Base_Site_DutyPlace> modelList = new List<TF.RunSafty.Model.TAB_Base_Site_DutyPlace>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.TAB_Base_Site_DutyPlace model;
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
        /// 更新出勤地点
        /// </summary>
        /// <param name="strTrainjiaoluGUID"></param>
        /// <param name="strClientGUID"></param>
        /// <param name="strDutyPlaces"></param>
        public void UpdateClientDutyPlace(string strTrainjiaoluGUID, string strClientGUID, string strDutyPlaces)
        {
            dal.DeleteByTrainJiaoluGUID(strTrainjiaoluGUID,strClientGUID);
            string[] strPlaceArray = strDutyPlaces.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            foreach (string placeId in strPlaceArray)
            {
                TF.RunSafty.Model.TAB_Base_Site_DutyPlace model = new TF.RunSafty.Model.TAB_Base_Site_DutyPlace();
                model.strSiteGUID = strClientGUID;
                model.strPlaceID = placeId;
                model.strTrainJiaoluGUID = strTrainjiaoluGUID;
                model.nPlaceIndex = i;
                this.Add(model);
                i++;
            }
        }

        public void DeleteByClientGUID(string strTrainjiaoluGUID,string strClientGUID)
        {
            dal.DeleteByTrainJiaoluGUID(strTrainjiaoluGUID, strClientGUID);
        }

        public void DeleteByClientGUID(string strClientGUID)
        {
            dal.DeleteByClientGUID(strClientGUID);
        }

		#endregion  ExtensionMethod
	}
}

