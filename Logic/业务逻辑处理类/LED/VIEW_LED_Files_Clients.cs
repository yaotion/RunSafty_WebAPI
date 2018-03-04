﻿/**  版本信息模板在安装目录下，可自行修改。
* VIEW_LED_Files_Clients.cs
*
* 功 能： N/A
* 类 名： VIEW_LED_Files_Clients
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-11-20 10:52:51   N/A    初版
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
	/// VIEW_LED_Files_Clients
	/// </summary>
	public partial class VIEW_LED_Files_Clients
	{
		private readonly TF.RunSafty.DAL.VIEW_LED_Files_Clients dal=new TF.RunSafty.DAL.VIEW_LED_Files_Clients();
		public VIEW_LED_Files_Clients()
		{}
		#region  BasicMethod
 
 
 
         
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
		public List<TF.RunSafty.Model.VIEW_LED_Files_Clients> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.VIEW_LED_Files_Clients> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.VIEW_LED_Files_Clients> modelList = new List<TF.RunSafty.Model.VIEW_LED_Files_Clients>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.VIEW_LED_Files_Clients model;
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

        public List<TF.RunSafty.Model.VIEW_LED_Files_Clients> GetLedFilesOfClient(string clientid)
        {
            string strWhere = string.Format(" clientid='{0}'",clientid);
            DataSet set = this.GetList(strWhere);
            return DataTableToList(set.Tables[0]);
        }

		#endregion  ExtensionMethod
	}
}

