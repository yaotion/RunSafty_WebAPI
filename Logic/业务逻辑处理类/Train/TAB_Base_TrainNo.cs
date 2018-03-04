/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_TrainNo.cs
*
* 功 能： N/A
* 类 名： TAB_Base_TrainNo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/18 9:17:47   N/A    初版
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
using TF.RunSafty.Model.InterfaceModel;
using TF.RunSafty.Model;
namespace TF.RunSafty.BLL
{
	/// <summary>
	/// TAB_Base_TrainNo
	/// </summary>
	public partial class TAB_Base_TrainNo
	{
		private readonly TF.RunSafty.DAL.TAB_Base_TrainNo dal=new TF.RunSafty.DAL.TAB_Base_TrainNo();
		public TAB_Base_TrainNo()
		{}
		#region  BasicMethod
		

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(TF.RunSafty.Model.TAB_Base_TrainNo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_Base_TrainNo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string strGUID)
		{
			
			return dal.Delete(strGUID);
		}

        /// <summary>
        /// 删除一组数据
        /// </summary>
        public bool DeleteByJiaoLu(string strJiaoLuGUID)
        {

            return dal.DeleteByJiaoLu(strJiaoLuGUID);
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.TAB_Base_TrainNo GetModel(string strGUID)
		{
			
			return dal.GetModel(strGUID);
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
		public List<TF.RunSafty.Model.TAB_Base_TrainNo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TF.RunSafty.Model.TAB_Base_TrainNo> DataTableToList(DataTable dt)
		{
			List<TF.RunSafty.Model.TAB_Base_TrainNo> modelList = new List<TF.RunSafty.Model.TAB_Base_TrainNo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TF.RunSafty.Model.TAB_Base_TrainNo model;
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
        public bool UpdateByParamModel(TF.RunSafty.Model.InterfaceModel.ParamModel paramModel )
        {
            TF.RunSafty.Model.TAB_Base_TrainNo train = this.GetModel(paramModel.data.trainNoID);
            SetModelValue(paramModel, train);
            return this.Update(train);
        }

        public void GetTrainnoByTime(string strTrainjiaolu, DateTime dtBegin, DateTime dtEnd,int PlanState)
        {
            dal.GetTrainnoByTime(strTrainjiaolu, dtBegin, dtEnd, PlanState);
        }
        public bool AddByParamModel(TF.RunSafty.Model.InterfaceModel.ParamModel paramModel, TF.RunSafty.Model.TAB_Base_TrainNo train)
        { 
            string strGUID = Guid.NewGuid().ToString();
            train.strGUID = strGUID;
            SetModelValue(paramModel, train);
            return this.Add(train);
        }
        public void SetModelValue(TF.RunSafty.Model.InterfaceModel.ParamModel paramModel, TF.RunSafty.Model.TAB_Base_TrainNo train)
        {
            train.dtCreateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(paramModel.data.dragTypeID))
                train.nDragType = int.Parse(paramModel.data.dragTypeID);
            if (!string.IsNullOrEmpty(paramModel.data.kehuoID))
                train.nKehuoID = int.Parse(paramModel.data.kehuoID);
            if (!string.IsNullOrEmpty(paramModel.data.planTypeID))
                train.nPlanType = int.Parse(paramModel.data.planTypeID);
            train.nRemarkType = int.Parse(paramModel.data.remarkTypeID);
            train.nTrainmanTypeID = int.Parse(paramModel.data.trainmanTypeID);
            train.strEndStation = paramModel.data.endStationID;
            train.strPlaceID = paramModel.data.placeID;
            train.strRemark = paramModel.data.remark;
            train.strStartStation = paramModel.data.startStationID;
            train.strTrainJiaoluGUID = paramModel.data.trainjiaoluID;
            train.strTrainNo = paramModel.data.trainNo;
            train.strTrainNumber = paramModel.data.trainNumber;
            train.strTrainTypeName = paramModel.data.trainTypeName;
            DateTime dt = DateTime.Parse(paramModel.data.startTime);
            train.dtStartTime =dt;
            train.dtRealStartTime =dt;
            DateTime dtChuQinTime;
            if (DateTime.TryParse(paramModel.data.kaiCheTime, out dtChuQinTime))
            {
                train.dtPlanStartTime = dtChuQinTime;
            }
        }
		#endregion  ExtensionMethod



        #region 接口
        #region 添加图定车次信息
        private class Train
        {
            public string trainnoID;

        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public Train data;
        }
        public  string AddTrainNo(string data)
        {
            JsonModel jsonModel = new JsonModel();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                TF.RunSafty.Model.InterfaceModel.ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.InterfaceModel.ParamModel>(data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                { 
                    TF.RunSafty.Model.TAB_Base_TrainNo train = new TF.RunSafty.Model.TAB_Base_TrainNo();
                    Train t = new Train();
                    if (this.AddByParamModel(paramModel, train))
                    {
                        t.trainnoID = train.strGUID;
                        jsonModel.result = 0;
                        jsonModel.resultStr = "返回成功";
                        jsonModel.data = t;
                    }
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
        #endregion


        #region 编辑图定车次信息

        private class Train1
        {
            public string trainnoID = "";

        }
        private class JsonModel1
        {
            public int result;
            public string resultStr; 
        }
        public  string EditTrainNo(string data)
        {
            JsonModel1 jsonModel = new JsonModel1();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
                    if (bllTrain.UpdateByParamModel(paramModel))
                    {
                        jsonModel.result = 0;
                        jsonModel.resultStr = "返回成功";
                    }
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
        #endregion

        #region 删除图定车次

        private class ParamModel2
        {
            public string cid;
            public Train2 data;
        }
        private class Train2
        {
            public string trainnoID;
        }
        private class JsonModel2
        {
            public int result;
            public string resultStr;
            public object data;
        }

        public string DeleteTrainNo(string data)
        {
            JsonModel2 jsonModel = new JsonModel2();
            try
            {
                ParamModel2 paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel2>(data);
                TF.RunSafty.Model.TAB_Base_TrainNo train = new TF.RunSafty.Model.TAB_Base_TrainNo();
                if (this.Delete(paramModel.data.trainnoID))
                {
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
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
        #endregion


        #region 加载图定车次
        private class ParamModel3
        {
            public string cid;
            public Train3 data;
        }
        private class Train3
        {
            public string trainjiaoluID;
            public string beginTime;
            public string endTime;
            public int planState;
        }
        private class JsonModel3
        {
            public int result;
            public string resultStr = "";
            public string data = "";
        }

        public string LoadTrainNo(string data)
        {
            JsonModel3 jsonModel = new JsonModel3();
            try
            {
                ParamModel3 paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel3>(data);
                TF.RunSafty.Model.TAB_Base_TrainNo train = new TF.RunSafty.Model.TAB_Base_TrainNo();
                TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
                DateTime dtBegin, dtEnd;
                string strTrainjiao = paramModel.data.trainjiaoluID;
                dtBegin = DateTime.Parse(paramModel.data.beginTime);
                dtEnd = DateTime.Parse(paramModel.data.endTime);
                //缺省计划状态为1编辑。在指定了状态后可以为指定的状态
                int PlanState = paramModel.data.planState <= 0 ? 1 : paramModel.data.planState;
               
                bllTrain.GetTrainnoByTime(strTrainjiao, dtBegin, dtEnd, PlanState);
                jsonModel.result = 0;
                jsonModel.resultStr = "返回成功";
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
        #endregion
        #endregion
    }
}

