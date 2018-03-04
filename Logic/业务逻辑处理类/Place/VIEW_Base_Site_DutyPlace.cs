/**  版本信息模板在安装目录下，可自行修改。
* VIEW_Base_Site_DutyPlace.cs
*
* 功 能： N/A
* 类 名： VIEW_Base_Site_DutyPlace
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/17 11:20:50   N/A    初版
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
namespace TF.RunSafty.BLL.Place
{
	/// <summary>
	/// VIEW_Base_Site_DutyPlace
	/// </summary>
	public partial class VIEW_Base_Site_DutyPlace
	{
		private readonly TF.RunSafty.DAL.VIEW_Base_Site_DutyPlace dal=new TF.RunSafty.DAL.VIEW_Base_Site_DutyPlace();
		public VIEW_Base_Site_DutyPlace()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.VIEW_Base_Site_DutyPlace GetModel(int nId)
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
		public List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace> modelList = new List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.VIEW_Base_Site_DutyPlace model;
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
        /// 获取指定客户端管辖的指定区段所有的出勤点
        /// </summary>
        /// <param name="strTrainJiaolu"></param>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace> GetPlaceOfClient(string strTrainJiaolu, string siteID)
        {
            DataSet set = dal.GetPlaceOfClient(strTrainJiaolu, siteID);
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获取指定区段所有的出勤点
        /// </summary>
        /// <param name="strTrainJiaolu"></param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace> GetPlaceOfTrainJiaolu(string strTrainJiaolu)
        {
            DataSet set = dal.GetPlaceOfTrainJiaolu(strTrainJiaolu);
            return DataTableToList(set.Tables[0]);
        }
		#endregion  ExtensionMethod

        #region 接口
        #region 获取指定客户端下的出勤地点
        private class JiaoLuData
        {
            public string siteID;
            public string trainjiaoluID;
        }
        private class ParamModel
        {
            public string cid;
            public JiaoLuData data;
        }
        private class listModel
        {
            public string placeID;
            public string placeName;
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<listModel> data;
        }

        private List<listModel> GetPlaceList(List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace> placeList)
        {
            if (placeList != null)
            {
                List<listModel> resultList = new List<listModel>();
                foreach (TF.RunSafty.Model.VIEW_Base_Site_DutyPlace place in placeList)
                {
                    listModel model = new listModel();
                    model.placeID = place.strPlaceID;
                    model.placeName = place.strPlaceName;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }
        public  string GetPlacesOfClient(string data)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(data); 
                string strTrainJiaolu = paramModel.data.trainjiaoluID;
                string strSite = paramModel.data.siteID;
                List<TF.RunSafty.Model.VIEW_Base_Site_DutyPlace> placeList = this.GetPlaceOfClient(strTrainJiaolu, strSite);
                jsonModel.result = 0;
                jsonModel.resultStr = "提交成功";
                jsonModel.data = GetPlaceList(placeList);
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败";
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
            return result;
        }
        #endregion
        #endregion
    }
}

