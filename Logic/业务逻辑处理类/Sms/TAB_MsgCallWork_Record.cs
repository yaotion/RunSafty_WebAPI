/**  版本信息模板在安装目录下，可自行修改。
* TAB_MsgCallWork_Record.cs
*
* 功 能： N/A
* 类 名： TAB_MsgCallWork_Record
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-10-10 13:06:46   N/A    初版
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
	/// TAB_MsgCallWork_Record
	/// </summary>
	public partial class TAB_MsgCallWork_Record
	{
		private readonly TF.RunSafty.DAL.TAB_MsgCallWork_Record dal=new TF.RunSafty.DAL.TAB_MsgCallWork_Record();
		public TAB_MsgCallWork_Record()
		{}
		#region  BasicMethod

		 
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(TF.RunSafty.Model.TAB_MsgCallWork_Record model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_MsgCallWork_Record model)
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
		public TF.RunSafty.Model.TAB_MsgCallWork_Record GetModel(int nId)
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
		public List<TF.RunSafty.Model.TAB_MsgCallWork_Record> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.TAB_MsgCallWork_Record> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.TAB_MsgCallWork_Record> modelList = new List<TF.RunSafty.Model.TAB_MsgCallWork_Record>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.TAB_MsgCallWork_Record model;
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
        /// 根据短信GUID获取记录
        /// </summary>
        /// <param name="strMsgGUID"></param>
        /// <returns></returns>
        public TF.RunSafty.Model.TAB_MsgCallWork_Record GetModelByGUID(string strMsgGUID)
        {
            string strWhere = string.Format(" strGUID='{0}' ", strMsgGUID);
            List<TF.RunSafty.Model.TAB_MsgCallWork_Record> models = GetModelList(strWhere);
            if (models != null && models.Count > 0)
                return models[0];
            return null;
        }
		#endregion  ExtensionMethod
	}
}

