/**  版本信息模板在安装目录下，可自行修改。
* VIEW_Base_TrainNo.cs
*
* 功 能： N/A
* 类 名： VIEW_Base_TrainNo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/17 14:08:53   N/A    初版
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
	/// VIEW_Base_TrainNo
	/// </summary>
	public partial class VIEW_Base_TrainNo
	{
		private readonly TF.RunSafty.DAL.VIEW_Base_TrainNo dal=new TF.RunSafty.DAL.VIEW_Base_TrainNo();
		public VIEW_Base_TrainNo()
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
		public List<TF.RunSafty.Model.VIEW_Base_TrainNo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.VIEW_Base_TrainNo> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.VIEW_Base_TrainNo> modelList = new List<TF.RunSafty.Model.VIEW_Base_TrainNo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.VIEW_Base_TrainNo model;
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
        /// 获取指定交路下的图定车次信息
        /// </summary>
        /// <param name="strTrainJiaolu"></param>
        /// <returns></returns>
        public List<TF.RunSafty.Model.VIEW_Base_TrainNo> GetTrainnosByTrainJiaolu(string strTrainJiaolu)
        {
            DataSet set = dal.GetTrainnosOfTrainJiaolu(strTrainJiaolu);
            DataTable table = set.Tables[0];
            DataView view = table.DefaultView;
            view.Sort = " dtStartTime desc ";
            table = view.ToTable();
            return DataTableToList(table);
        }
        public List<TF.RunSafty.Model.TAB_Base_TrainNo_Json> GetPlaceList(List<TF.RunSafty.Model.VIEW_Base_TrainNo> placeList)
        {
            if (placeList != null)
            {
                List<TF.RunSafty.Model.TAB_Base_TrainNo_Json> resultList = new List<TF.RunSafty.Model.TAB_Base_TrainNo_Json>();
                foreach (TF.RunSafty.Model.VIEW_Base_TrainNo place in placeList)
                {
                    TF.RunSafty.Model.TAB_Base_TrainNo_Json model = new TF.RunSafty.Model.TAB_Base_TrainNo_Json();
                    model.trainjiaoluID = place.strTrainJiaoluGUID;
                    model.trainjiaoluName = place.strTrainJiaoluName;
                    model.placeID = place.strPlaceID;
                    model.placeName = place.strPlaceName;
                    model.trainTypeName = place.strTrainTypeName;
                    model.trainNumber = place.strTrainNumber;
                    model.trainNo = place.strTrainNo;
                    model.remark = place.strRemark;
                    if (place.dtStartTime.HasValue)
                    {
                        model.startTime = place.dtStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    model.startStationID = place.strStartStation;
                    model.startStationName = place.strStartStationName;
                    model.endStationID = place.strEndStation;
                    model.endStationName = place.strEndStationName;
                    model.trainmanTypeID = place.nTrainmanTypeID.ToString();
                    model.trainmanTypeName = place.strTrainmanTypeName;
                    model.planTypeID = place.nPlanType.ToString();
                    model.planTypeName = place.strPlanTypeName;
                    model.dragTypeID = place.nDragType.ToString();
                    model.dragTypeName = place.nDragTypeName;
                    model.kehuoID = place.nKehuoID.ToString();
                    model.kehuoName = place.strKehuoName;
                    model.remarkTypeID = place.nRemarkType.ToString();
                    model.remarkTypeName = place.strRemarkTypeName;
                    model.trainNoID = place.strGUID;
                    if (place.dtPlanStartTime.HasValue)
                    {
                        model.kaiCheTime = place.dtPlanStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        if (place.dtStartTime.HasValue)
                        {
                            model.kaiCheTime = place.dtStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                    model.nNeedRest = place.nNeedRest;
                    if (place.dtArriveTime.HasValue)
                    {
                        model.dtArriveTime = place.dtArriveTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (place.dtCallTime.HasValue)
                    {
                        model.dtCallTime = place.dtCallTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    model.strWorkDay = place.strWorkDay;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }
        public List<TF.RunSafty.Model.VIEW_Base_TrainNo> GetTrainnosByID(string strTrainnoID)
        {
            string strWhere = string.Format(" strGUID='{0}' ",strTrainnoID);
            DataSet set = dal.GetList(strWhere);
            return DataTableToList(set.Tables[0]);
        }
		#endregion  ExtensionMethod


        #region 接口
        #region 获取指定交路下的图定车次信息
        private class JiaoLuData
        {
            public string trainjiaoluID;
        }
        private class ParamModel
        {
            public string cid;
            public JiaoLuData data;
        }

        private class JsonModel
        {
            public int result;
            public string resultStr;
            public object data;
        }
        public List<TF.RunSafty.Model.VIEW_Base_TrainNo> GetTrainnosOfTrainJiaolu(string strTrainJiaolu)
        {
            DataSet set = dal.GetTrainnosOfTrainJiaolu(strTrainJiaolu);
            DataTable table = set.Tables[0];
            DataView view = table.DefaultView;
            table = view.ToTable();
            return DataTableToList(table);
        }
        #endregion

        #region 根据编号获取图定车次信息
        private class JiaoLuData1
        {
            public string trainnoID;
        }
        private class ParamModel1
        {
            public string cid;
            public JiaoLuData1 data;
        }

        private class JsonModel1
        {
            public int result;
            public string returnStr;
            public TF.RunSafty.Model.TAB_Base_TrainNo_Json data;
        }


        public string GetTrainNoByID(string data)
        {
            JsonModel1 jsonModel = new JsonModel1();
            try
            {
                ParamModel1 paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel1>(data);
                List<TF.RunSafty.Model.VIEW_Base_TrainNo> placeList = this.GetTrainnosByID(paramModel.data.trainnoID);
                jsonModel.result = 0;
                jsonModel.returnStr = "提交成功";
                jsonModel.data = this.GetPlaceList(placeList)[0];
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.returnStr = "提交失败" + ex.Message;
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
        }
        #endregion
        #endregion
    }
}

