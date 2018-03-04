/**  版本信息模板在安装目录下，可自行修改。
* TAB_FileGroup.cs
*
* 功 能： N/A
* 类 名： TAB_FileGroup
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/15 13:13:34   N/A    初版
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
	/// TAB_FileGroup
	/// </summary>
	public partial class TAB_FileGroup
	{
		private readonly TF.RunSafty.DAL.TAB_FileGroup dal=new TF.RunSafty.DAL.TAB_FileGroup();
		public TAB_FileGroup()
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
        /// 判断类型是否存在
        /// </summary>
        /// <param name="noticeType"></param>
        /// <returns></returns>
        public bool Exists(string noticeType)
        {
            string where = string.Format(" strTypeName='{0}'", noticeType);
            IList<TF.RunSafty.Model.TAB_FileGroup> models = GetModelList(where);
            return models.Count > 0;
        }
        /// <summary>
        ///  判断类型是否存在
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="noticeType"></param>
        /// <returns></returns>
        public bool Exists(int nid, string noticeType)
        {
            string where = string.Format(" strTypeName='{0}' and nid<>{1}", noticeType, nid.ToString());
            IList<TF.RunSafty.Model.TAB_FileGroup> models = GetModelList(where);
            return models.Count > 0;
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(TF.RunSafty.Model.TAB_FileGroup model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_FileGroup model)
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
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.TAB_FileGroup GetModel(int nid)
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
		public List<TF.RunSafty.Model.TAB_FileGroup> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.TAB_FileGroup> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.TAB_FileGroup> modelList = new List<TF.RunSafty.Model.TAB_FileGroup>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.TAB_FileGroup model;
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

        #region 获取阅读记录

        private class ParamModel
        {
            public string strTrainmanGUID;
        }
        private class TypeList
        {
            public string strTypeGUID;
            public string strTypeName;
            public DataTable FileList;
        }
        private class InnerData
        {
            public List<TypeList> TypeList;
        }
        private class JsonData
        {
            public InnerData data;
        }
        private class JsonModel
        {
            public string result;
            public string returnStr;
            public JsonData Data;
        }
        public string GetReadingRecords(string _data)
        {
            JsonModel model = new JsonModel();
            DataTable table = null;
            JsonData data = new JsonData();
            List<TypeList> typeList = new List<TypeList>();
            data.data = new InnerData();
            data.data.TypeList = typeList;
            DataTable tblReading = null;
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(_data); 
            TF.RunSafty.BLL.TAB_ReadDocPlan bllPlan = new TF.RunSafty.BLL.TAB_ReadDocPlan();
            try
            {
                table = this.GetAllList().Tables[0];
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        TypeList list = new TypeList();
                        list.strTypeGUID = row["strTypeGUID"].ToString();
                        list.strTypeName = row["strTypeName"].ToString();
                        tblReading = bllPlan.GetReadingHistoryOfTrainman(param.strTrainmanGUID, list.strTypeGUID);
                        list.FileList = tblReading;
                        typeList.Add(list);
                    }
                }
                model.result = "0";
                model.returnStr = "返回成功";
                model.Data = data;
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(model, timeConverter).Replace(":null", ":\"\"");
            return result;
        }
        #endregion


        #region 阅读记录同步
        //private class JsonModel1
        //{
        //    public string result;
        //    public string returnStr;
        //    public DataTable TypeList;
        //    public DataTable FileList;
        //}
        //public string ReadingSync(string data)
        //{
        //    JsonModel1 model = new JsonModel1();
        //    DataTable table = null;
        //    DataTable tblFile = null;  
        //    TF.RunSafty.BLL.TAB_ReadDoc bllFile = new TF.RunSafty.BLL.TAB_ReadDoc();
        //    try
        //    {
        //        table = this.GetAllList().Tables[0];
        //        tblFile = bllFile.GetAllListWithFileType().Tables[0];
        //        model.result = "0";
        //        model.returnStr = "返回成功";
        //        model.TypeList = table;
        //        model.FileList = tblFile;
        //    }
        //    catch (Exception ex)
        //    {
        //        TF.CommonUtility.LogClass.logex(ex, "");
        //    }
        //    Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        //    //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
        //    timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        //    string result = Newtonsoft.Json.JsonConvert.SerializeObject(model, timeConverter).Replace(":null", ":\"\"");
        //    return result;
        //}
        #endregion
        #endregion  ExtensionMethod
    }
}

