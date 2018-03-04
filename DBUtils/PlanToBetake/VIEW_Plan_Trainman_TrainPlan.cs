using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.DAL
{
    public partial class VIEW_Plan_Trainman_TrainPlan
    {



        public List<TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan> GetTrainPlan(string StrGuid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from .VIEW_Plan_Trainman ");
            strSql.Append(" where strTrainPlanGUID=@strTrainPlanGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50)
			};
            parameters[0].Value = StrGuid;

            DataSet set = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            return DataTableToList(set.Tables[0]);


        }




        public List<TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan> modelList = new List<TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan model;
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

        public TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan model = new TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan();
            if (row != null)
            {


                if (row["dtCallTime"] != null && row["dtCallTime"].ToString() != "")
                {
                    if (Convert.ToDateTime(row["dtCallTime"].ToString()) > Convert.ToDateTime("2000-01-01 01:00:00"))
                    {
                        model.dtCallTime = row["dtCallTime"].ToString();
                    }
                    else
                    {
                        model.dtCallTime = "0";
                    }
                }
                if (row["dtChuqinTime"] != null && row["dtChuqinTime"].ToString() != "")
                {
                    if (Convert.ToDateTime(row["dtChuqinTime"].ToString()) > Convert.ToDateTime("2000-01-01 01:00:00"))
                    {
                        model.dtChuqinTime = row["dtChuqinTime"].ToString();
                    }
                    else
                    {
                        model.dtChuqinTime = "0";
                    }
                }
                if (row["dtStartTime"] != null && row["dtStartTime"].ToString() != "")
                {
                    if (Convert.ToDateTime(row["dtStartTime"].ToString()) > Convert.ToDateTime("2000-01-01 01:00:00"))
                    {
                        model.dtStartTime = row["dtStartTime"].ToString();
                    }
                    else
                    {
                        model.dtStartTime = "0";
                    }
                }
                if (row["strStartStation"] != null)
                {
                    model.strStartStation = row["strStartStation"].ToString();
                }
                if (row["strTrainJiaoluGUID"] != null)
                {
                    model.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
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



                if (row["strTrainNo"] != null)
                {
                    model.strTrainNo = row["strTrainNo"].ToString();
                }

                if (row["strWorkShopGUID"] != null)
                {
                    model.strWorkShopGUID = row["strWorkShopGUID"].ToString();
                }


            }
            return model;
        }

    }
}