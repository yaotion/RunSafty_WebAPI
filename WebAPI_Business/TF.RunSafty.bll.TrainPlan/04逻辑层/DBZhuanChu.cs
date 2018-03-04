using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.Plan
{

     


    public class DBZhuanChu
    {

        public static int ZhuanChuHours
        {
            get
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuHours"] == null)
                {
                    return 0;
                }
                return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuHours"].ToString());
            }
        }


        public List<MDZhuanChu> GetTuiqinPlan4ZhuanChuByLastTime(string strWorkShopGUID, string beginTime, string endTime)
        {

            DataSet set = this.GetEndWorkPlans4ZhuanChuByLastTime(strWorkShopGUID, beginTime, endTime);
            List<MDZhuanChu> vPlans = DataTableToList(set.Tables[0]);
            return vPlans;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MDZhuanChu> DataTableToList(DataTable dt)
        {
            List<MDZhuanChu> modelList = new List<MDZhuanChu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MDZhuanChu model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = this.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MDZhuanChu DataRowToModel(DataRow row)
        {
            MDZhuanChu model = new MDZhuanChu();
            if (row != null)
            {
             
                if (row["strTrainNo"] != null)
                {
                    model.TrainNo = row["strTrainNo"].ToString();
                }
                if (row["strTrainNumber"] != null)
                {
                    model.TrainNumber = row["strTrainNumber"].ToString();
                }
                if (row["dtStartTime"] != null && row["dtStartTime"].ToString() != "")
                {
                    DateTime dtStartTime;
                    if (DateTime.TryParse(row["dtStartTime"].ToString(), out dtStartTime))
                    {
                        model.PlanTime = dtStartTime;
                    }
                }

               
                if (row["dtLastUpDateTime"] != null && row["dtLastUpDateTime"].ToString() != "")
                {
                    DateTime dtLastUpDateTime;
                    if (DateTime.TryParse(row["dtLastUpDateTime"].ToString(), out dtLastUpDateTime))
                    {
                        model.FileEndTime = dtLastUpDateTime;
                    }
                }

                if (row["dtUploadTime"] != null && row["dtUploadTime"].ToString() != "")
                {
                    DateTime dtUploadTime;
                    if (DateTime.TryParse(row["dtUploadTime"].ToString(), out dtUploadTime))
                    {
                        model.ZCTime = dtUploadTime;
                    }
                }


                if (row["dtLastArriveTime"] != null && row["dtLastArriveTime"].ToString() != "")
                {
                    DateTime dtLastArriveTime;
                    if (DateTime.TryParse(row["dtLastArriveTime"].ToString(), out dtLastArriveTime))
                    {
                        model.TQTime = dtLastArriveTime;
                    }
                }
                if (row["strTrainTypeName"] != null)
                {
                    model.TrainTypeName = row["strTrainTypeName"].ToString();
                }
               
                if (row["strTrainmanNumber1"] != null)
                {
                    model.TmNumber1 = row["strTrainmanNumber1"].ToString();
                }
                if (row["strTrainmanName1"] != null)
                {
                    model.TmName1 = row["strTrainmanName1"].ToString();
                }
             
                if (row["strTrainmanGUID1"] != null)
                {
                    model.TmGUID1 = row["strTrainmanGUID1"].ToString();
                }
                if (row["strTrainmanGUID2"] != null)
                {
                    model.TmGUID2 = row["strTrainmanGUID2"].ToString();
                }
                if (row["strTrainmanNumber2"] != null)
                {
                    model.TmNumber2 = row["strTrainmanNumber2"].ToString();
                }
                if (row["strTrainmanName2"] != null)
                {
                    model.TmName2 = row["strTrainmanName2"].ToString();
                }
              
                if (row["strTrainmanNumber3"] != null)
                {
                    model.TmNumber3 = row["strTrainmanNumber3"].ToString();
                }
                if (row["strTrainmanGUID3"] != null)
                {
                    model.TmGUID3 = row["strTrainmanGUID3"].ToString();
                }
              
                if (row["strTrainmanName3"] != null)
                {
                    model.TmName3 = row["strTrainmanName3"].ToString();
                }
            
                if (row["strTrainmanName4"] != null)
                {
                    model.TmName4 = row["strTrainmanName4"].ToString();
                }
                if (row["strTrainmanNumber4"] != null)
                {
                    model.TmNumber4 = row["strTrainmanNumber4"].ToString();
                }
             
                if (row["strTrainmanGUID4"] != null)
                {
                    model.TmGUID4 = row["strTrainmanGUID4"].ToString();
                }
            }
            return model;
        }

        public DataSet GetEndWorkPlans4ZhuanChuByLastTime(string strWorkShopGUID, string strBeginTime, string strEndTime)
        {
            string strWhere = " ";
            System.Text.StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" strWorkShopGUID1 = '{0}' ", strWorkShopGUID);
            builder.AppendFormat(" and dtLastArriveTime >='{0}' and nPlanState > 7 ", strBeginTime);
            builder.Append(" order by nPlanState,dtLastArriveTime desc,dtStartTime asc ");
            strWhere = builder.ToString();
            DataSet set = GetList(strWhere);
            return set;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select c.strTrainmanGUID1,c.strTrainmanGUID2,c.strTrainmanGUID3,c.strTrainmanGUID4,c.strTrainmanNumber1,c.strTrainmanNumber2,
  c.strTrainmanNumber3,c.strTrainmanNumber4,
    c.strTrainmanName1,c.strTrainmanName2,c.strTrainmanName3,c.strTrainmanName4,r.dtUploadTime,
    c.strTrainNo,c.strTrainNumber,c.strTrainTypeName,c.dtStartTime,r.dtLastUpDateTime,c.dtLastArriveTime from Tab_EndWork_RunRecrod_Main r left join VIEW_Plan_EndWork c
on r.strPlanGUID=c.strTrainPlanGUID where r.dtLastUpDateTime<DATEADD( HOUR,-" + ZhuanChuHours + ",c.dtLastArriveTime)");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        public List<MDZhuanChu> GetTuiqinPlan4ZhuanChuByCreatTime(string strWorkShopGUID, string beginTime, string endTime)
        {

            DataSet set = this.GetEndWorkPlans4ZhuanChuByCreat(strWorkShopGUID, beginTime, endTime);
            List<MDZhuanChu> vPlans = DataTableToList(set.Tables[0]);
            return vPlans;
        }

        public DataSet GetEndWorkPlans4ZhuanChuByCreat(string strWorkShopGUID, string strBeginTime, string strEndTime)
        {
            string strWhere = " ";
            System.Text.StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" strWorkShopGUID1 = '{0}' ", strWorkShopGUID);
            builder.AppendFormat(" and dtLastArriveTime >='{0}' and nPlanState > 7 ", strBeginTime);
            builder.Append(" order by nPlanState,dtLastArriveTime desc,dtStartTime asc ");
            strWhere = builder.ToString();
            DataSet set = GetListByCreat(strWhere);
            return set;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByCreat(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select c.strTrainmanGUID1,c.strTrainmanGUID2,c.strTrainmanGUID3,c.strTrainmanGUID4,c.strTrainmanNumber1,c.strTrainmanNumber2,
  c.strTrainmanNumber3,c.strTrainmanNumber4,
    c.strTrainmanName1,c.strTrainmanName2,c.strTrainmanName3,c.strTrainmanName4,r.dtUploadTime,
    c.strTrainNo,c.strTrainNumber,c.strTrainTypeName,c.dtStartTime,r.dtLastUpDateTime,c.dtLastArriveTime from Tab_EndWork_RunRecrod_Main r left join VIEW_Plan_EndWork c
on r.strPlanGUID=c.strTrainPlanGUID where r.dtUploadTime>c.dtLastArriveTime");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }



    }





    public class MDZhuanChu
    {
        public string TrainNo;
        public string TrainNumber;
        public string TrainTypeName;
        public DateTime PlanTime;
        public DateTime ZCTime;
        public DateTime TQTime;
        public DateTime FileEndTime;

        public string TmNumber1;
        public string TmName1;
        public string TmGUID1;

        public string TmNumber2;
        public string TmName2;
        public string TmGUID2;

        public string TmNumber3;
        public string TmName3;
        public string TmGUID3;

        public string TmNumber4;
        public string TmName4;
        public string TmGUID4;
    }

}
