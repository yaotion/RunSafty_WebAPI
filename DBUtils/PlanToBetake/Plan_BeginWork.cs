using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;


namespace TF.RunSafty.DAL
{
    public partial class Plan_BeginWork
    {
        public List<TF.RunSafty.Model.Model_Plan_ToBeTake> GetBeginWork(string StrGuid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Plan_BeginWork ");
            strSql.Append(" where strTrainPlanGUID=@strTrainPlanGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50)
			};
            parameters[0].Value = StrGuid;

            DataSet set = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            return DataTableToList(set.Tables[0]);
            
        }




         /// <summary>
        /// 得到一个对象实体
        /// </summary>
        private List<TF.RunSafty.Model.Model_Plan_ToBeTake> GetPlanList(List<TF.RunSafty.Model.Model_Plan_ToBeTake> vPlans)
        {
            List<TF.RunSafty.Model.Model_Plan_ToBeTake> model = new List<TF.RunSafty.Model.Model_Plan_ToBeTake>();


             TF.RunSafty.Model.Model_Plan_ToBeTake clientPlan = null;
             if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.Model_Plan_ToBeTake plan in vPlans)
                {
                    clientPlan = new TF.RunSafty.Model.Model_Plan_ToBeTake();

                    clientPlan.StrTrainNo = plan.StrTrainNo;
                    clientPlan.strCheCi = plan.strCheCi;
                    clientPlan.strTrainmanGUID1 = plan.strTrainmanGUID1;
                    clientPlan.strTrainmanGUID2 = plan.strTrainmanGUID2;
                    clientPlan.strTrainmanGUID3 = plan.strTrainmanGUID3;
                    clientPlan.strTrainmanGUID4 = plan.strTrainmanGUID4;
                    clientPlan.dtCallWorkTime = plan.dtCallWorkTime;
                    clientPlan.dtWaitWorkTime = plan.dtWaitWorkTime;
                    clientPlan.NNeedRest = plan.NNeedRest;
                    clientPlan.NPlanState = plan.NPlanState;

                }               
            }
            return model;
        }

        public List<TF.RunSafty.Model.Model_Plan_ToBeTake> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.Model_Plan_ToBeTake> modelList = new List<TF.RunSafty.Model.Model_Plan_ToBeTake>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.Model_Plan_ToBeTake model;
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


        public TF.RunSafty.Model.Model_Plan_ToBeTake DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.Model_Plan_ToBeTake model = new TF.RunSafty.Model.Model_Plan_ToBeTake();
            if (row != null)
            {
                

                if (row["StrTrainNo"] != null)
                {
                    model.StrTrainNo = row["strTrainNo"].ToString();
                }
              

                if (row["strTrainmanGUID1"] != null)
                {
                    model.strTrainmanGUID1 = row["strTrainmanGUID1"].ToString();
                }
                if (row["strTrainmanGUID2"] != null)
                {
                    model.strTrainmanGUID2 = row["strTrainmanGUID2"].ToString();
                }
                if (row["strTrainmanGUID3"] != null)
                {
                    model.strTrainmanGUID3 = row["strTrainmanGUID3"].ToString();
                }
                if (row["strTrainmanGUID4"] != null)
                {
                    model.strTrainmanGUID4 = row["strTrainmanGUID4"].ToString();
                }



                if (row["strTrainmanNumber1"] != null)
                {
                    model.strTrainmanNumber1 = row["strTrainmanNumber1"].ToString();
                }
                if (row["strTrainmanNumber2"] != null)
                {
                    model.strTrainmanNumber2 = row["strTrainmanNumber2"].ToString();
                }
                if (row["strTrainmanNumber3"] != null)
                {
                    model.strTrainmanNumber3 = row["strTrainmanNumber3"].ToString();
                }
                if (row["strTrainmanNumber4"] != null)
                {
                    model.strTrainmanNumber4 = row["strTrainmanNumber4"].ToString();
                }


                if (row["strTrainmanName1"] != null)
                {
                    model.strTrainmanName1 = row["strTrainmanName1"].ToString();
                }
                if (row["strTrainmanName2"] != null)
                {
                    model.strTrainmanName2 = row["strTrainmanName2"].ToString();
                }
                if (row["strTrainmanName3"] != null)
                {
                    model.strTrainmanName3 = row["strTrainmanName3"].ToString();
                }
                if (row["strTrainmanName4"] != null)
                {
                    model.strTrainmanName4 = row["strTrainmanName4"].ToString();
                }








                if (row["dtCallTime"] != null)
                {
                    model.dtCallWorkTime = row["dtCallTime"].ToString();
                }
                if (row["dtArriveTime"] != null)
                {
                    model.dtWaitWorkTime = row["dtArriveTime"].ToString();
                }

                if (row["nNeedRest"] != null)
                {
                    model.NNeedRest = row["nNeedRest"].ToString();
                
                }
                if (row["nPlanState"] != null)
                {
                    model.NPlanState = row["nPlanState"].ToString();
                }

              
            }
            return model;
        }
    }
}
