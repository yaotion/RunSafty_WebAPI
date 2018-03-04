/**  版本信息模板在安装目录下，可自行修改。
* TAB_ReadDoc.cs
*
* 功 能： N/A
* 类 名： TAB_ReadDoc
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/15 13:13:35   N/A    初版
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
	/// TAB_ReadDoc
	/// </summary>
	public partial class TAB_ReadDoc
	{
		private readonly TF.RunSafty.DAL.TAB_ReadDoc dal=new TF.RunSafty.DAL.TAB_ReadDoc();
		public TAB_ReadDoc()
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
		public bool Exists(int nid)
		{
			return dal.Exists(nid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(TF.RunSafty.Model.TAB_ReadDoc model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_ReadDoc model)
		{
			return dal.Update(model);
		}

        public bool Exists(string strFileName, string strWorkShop,int nid)
        {
            string where = string.Format(" strFileName='{0}' and strWorkShopGUID='{1}' and nid<>{2} ", strFileName, strWorkShop,nid);
            List<TF.RunSafty.Model.TAB_ReadDoc> models = GetModelList(where);
            return models.Count > 0;
        }
        /// <summary>
        /// 查看该车间是否上传过该通告
        /// </summary>
        /// <param name="strFileName">文件名称</param>
        /// <param name="strWorkShop">车间GUID</param>
        /// <returns></returns>
        public bool Exists(string strFileName, string strWorkShop)
        {
            string where = string.Format(" strFileName='{0}' and strWorkShopGUID='{1}' ", strFileName, strWorkShop);
            List<TF.RunSafty.Model.TAB_ReadDoc> models = GetModelList(where);
            return models.Count > 0;
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int nid)
		{
			
			return dal.Delete(nid);
		}
		 

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.TAB_ReadDoc GetModel(int nid)
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
		public List<TF.RunSafty.Model.TAB_ReadDoc> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.TAB_ReadDoc> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.TAB_ReadDoc> modelList = new List<TF.RunSafty.Model.TAB_ReadDoc>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.TAB_ReadDoc model;
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

        public DataSet GetAllListWithFileType(string cid)
        {
            return dal.GetAllListWithFileType(cid);
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

		#endregion  ExtensionMethod
	}
}

