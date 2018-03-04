/**  版本信息模板在安装目录下，可自行修改。
* Tab_DeliverJSPrint.cs
*
* 功 能： N/A
* 类 名： Tab_DeliverJSPrint
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/16 15:30:35   N/A    初版
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
	/// Tab_DeliverJSPrint
	/// </summary>
    public partial class Tab_DeliverJSPrint
    {
        private readonly TF.RunSafty.DAL.Tab_DeliverJSPrint dal = new TF.RunSafty.DAL.Tab_DeliverJSPrint();
        public Tab_DeliverJSPrint()
        { }
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
        public bool Exists(int nID)
        {
            return dal.Exists(nID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TF.RunSafty.Model.Tab_DeliverJSPrint model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TF.RunSafty.Model.Tab_DeliverJSPrint model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int nID)
        {

            return dal.Delete(nID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.Tab_DeliverJSPrint GetModel(int nID)
        {

            return dal.GetModel(nID);
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.Tab_DeliverJSPrint> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.Tab_DeliverJSPrint> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.Tab_DeliverJSPrint> modelList = new List<TF.RunSafty.Model.Tab_DeliverJSPrint>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.Tab_DeliverJSPrint model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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
        /// 判断是否是司机
        /// </summary>
        /// <param name="strPlanGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <returns></returns>
        private bool IsDriver(string strPlanGUID,string strTrainmanGUID)
        {
            DataTable tblPlan = ThinkFreely.RunSafty.Trainman.GetTrainmanTypeByPlanGUID(strPlanGUID);
            if (tblPlan != null && tblPlan.Rows.Count > 0)
            {
                string strTrainmanTypeName = tblPlan.Rows[0]["strTrainmanTypeName"].ToString();
                string strTrainmanGUID1 = tblPlan.Rows[0]["strTrainmanGUID1"].ToString();
                string strTrainmanGUID2 = tblPlan.Rows[0]["strTrainmanGUID2"].ToString();
                if (strTrainmanTypeName == "双司机")
                {
                    if (strTrainmanGUID1 == strTrainmanGUID || strTrainmanGUID2 == strTrainmanGUID)
                    {
                        return true;
                    }
                }
                else if (strTrainmanTypeName == "标准班")
                {
                    if (strTrainmanGUID1 == strTrainmanGUID)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断交付揭示是否可以打印
        /// </summary>
        /// <param name="strPlanGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="strPrintTime"></param>
        /// <returns></returns>
        public int IsJsPrintable(string strPlanGUID, string strTrainmanGUID, string strPrintTime)
        {
            if (string.IsNullOrEmpty(strPlanGUID))
            {
                return 1;
            }
            DateTime dtPrintTime = DateTime.Parse(strPrintTime);
            DateTime dtNow = DateTime.Now;
            if (DateTime.Compare(dtNow.AddMinutes(10), dtPrintTime) < 0)
                return 2;
            if (IsDriver(strPlanGUID, strTrainmanGUID))
            {
                if (IsJsPrinted(strPlanGUID, strTrainmanGUID))
                    return 1;
            }
            else
            {
                return 3;
            }
            return 0;
        }

        /// <summary>
        /// 判断交付揭示是否已经打印
        /// </summary>
        /// <param name="strPlanGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <returns></returns>
        private bool IsJsPrinted(string strPlanGUID, string strTrainmanGUID)
        {
            string strWhere =string.Format(" StrTrainmanGUID='{0}' and StrPlanGUID='{1}' ",strTrainmanGUID,strPlanGUID);
            List<TF.RunSafty.Model.Tab_DeliverJSPrint> printList = GetModelList(strWhere);
            return printList != null && printList.Count > 0;
        }
        #endregion  


        #region 揭示打印接口
        private class ParamModel
        {
            public string cid;
            public string strPlanGUID;
            public string strTrainmanGUID;
            public string dtBeginWorkTime;
        }

        private class JsonModel
        {
            public string result;
            public string returnStr;
            public string bPrint;
        } 
        public string IsJsPrintable(string data)
        {
            JsonModel model = new JsonModel();
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(data);
            TF.RunSafty.BLL.Tab_DeliverJSPrint bllPrint = new TF.RunSafty.BLL.Tab_DeliverJSPrint();
            TF.RunSafty.Model.Tab_DeliverJSPrint modelPlan = new TF.RunSafty.Model.Tab_DeliverJSPrint();
            try
            {
                int bPrint = bllPrint.IsJsPrintable(param.strPlanGUID, param.strTrainmanGUID, param.dtBeginWorkTime);
                modelPlan.StrPlanGUID = param.strPlanGUID;
                modelPlan.StrSiteGUID = param.cid;
                modelPlan.StrTrainmanGUID = param.strTrainmanGUID;
                model.result = "0";
                model.returnStr = "返回成功";
                model.bPrint = bPrint.ToString();

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = "提交失败" + ex.Message;
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return result;
        }

        #endregion 添加打印记录

        private class ParamModel1
        {
            public string PlanGUID;
            public string TrainmanGUID;
            public string PrintTime;
        }

        private class JsonModel1
        {
            public string result;
            public string returnStr;
        }
        public string Add(string cid,string data)
        {
            JsonModel1 model = new JsonModel1();
            ParamModel1 param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel1>(data);
            TF.RunSafty.BLL.Tab_DeliverJSPrint bllPrint = new TF.RunSafty.BLL.Tab_DeliverJSPrint();
            TF.RunSafty.Model.Tab_DeliverJSPrint modelPlan = new TF.RunSafty.Model.Tab_DeliverJSPrint();
            try
            {
                modelPlan.StrPlanGUID = param.PlanGUID;
                modelPlan.StrSiteGUID = cid;
                modelPlan.StrTrainmanGUID = param.TrainmanGUID;
                DateTime dtPrint;
                if (DateTime.TryParse(param.PrintTime, out dtPrint))
                {
                    modelPlan.dtPrintTime = dtPrint;
                }
                if (bllPrint.Add(modelPlan) > 0)
                {
                    model.result = "0";
                    model.returnStr = "提交成功";
                }
                else
                {
                    model.result = "1";
                    model.returnStr = "提交失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = "提交失败";
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return result;
        }

        #region
        #endregion
    }
}

