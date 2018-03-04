using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.BeginworkFlow
{
    public class UpdataJiMing
    {
        public void UpdateReadTime(string strFileGUID, string strTrainmanGUID, string strReadTime)
        {
            string strWhere = string.Format(" StrFileGUID='{0}' AND StrTrainmanGUID='{1}' ", strFileGUID, strTrainmanGUID);
            TF.CommonUtility.LogClass.log(strWhere);
            List<TF.RunSafty.Model.TAB_ReadDocPlan> plans = this.GetModelList(strWhere);
            if (plans != null && plans.Count > 0)
            {
                TF.RunSafty.Model.TAB_ReadDocPlan plan = plans[0];
                DateTime readTime = DateTime.Parse(strReadTime);
                if (plan.NReadCount.HasValue && plan.NReadCount.Value > 0) //已经阅读
                {
                    plan.NReadCount++;
                    plan.DtLastReadTime = readTime;
                }
                else
                {
                    plan.DtFirstReadTime = readTime;
                    plan.DtLastReadTime = readTime;
                    plan.NReadCount = 1;

                }
                if (this.Update(plan))
                {
                    TF.CommonUtility.LogClass.log("阅读记录更新成功");
                }
                else
                {
                    TF.CommonUtility.LogClass.log("阅读记录更新失败");
                }
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TF.RunSafty.Model.TAB_ReadDocPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_ReadDocPlan set ");
            strSql.Append("StrTrainmanGUID=@StrTrainmanGUID,");
            strSql.Append("StrFileGUID=@StrFileGUID,");
            strSql.Append("NReadCount=@NReadCount,");
            strSql.Append("DtFirstReadTime=@DtFirstReadTime,");
            strSql.Append("DtLastReadTime=@DtLastReadTime");
            strSql.Append(" where nId=@nId");
            SqlParameter[] parameters = {
					new SqlParameter("@StrTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrFileGUID", SqlDbType.VarChar,50),
					new SqlParameter("@NReadCount", SqlDbType.Int,4),
					new SqlParameter("@DtFirstReadTime", SqlDbType.DateTime),
					new SqlParameter("@DtLastReadTime", SqlDbType.DateTime),
					new SqlParameter("@nId", SqlDbType.Int,4)};
            parameters[0].Value = model.StrTrainmanGUID;
            parameters[1].Value = model.StrFileGUID;
            parameters[2].Value = model.NReadCount;
            parameters[3].Value = model.DtFirstReadTime;
            parameters[4].Value = model.DtLastReadTime;
            parameters[5].Value = model.nId;

            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.TAB_ReadDocPlan> GetModelList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.TAB_ReadDocPlan> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.TAB_ReadDocPlan> modelList = new List<TF.RunSafty.Model.TAB_ReadDocPlan>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.TAB_ReadDocPlan model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
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
        public TF.RunSafty.Model.TAB_ReadDocPlan DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.TAB_ReadDocPlan model = new TF.RunSafty.Model.TAB_ReadDocPlan();
            if (row != null)
            {
                if (row["nId"] != null && row["nId"].ToString() != "")
                {
                    model.nId = int.Parse(row["nId"].ToString());
                }
                if (row["StrTrainmanGUID"] != null)
                {
                    model.StrTrainmanGUID = row["StrTrainmanGUID"].ToString();
                }
                if (row["StrFileGUID"] != null)
                {
                    model.StrFileGUID = row["StrFileGUID"].ToString();
                }
                if (row["NReadCount"] != null && row["NReadCount"].ToString() != "")
                {
                    model.NReadCount = int.Parse(row["NReadCount"].ToString());
                }
                if (row["DtFirstReadTime"] != null && row["DtFirstReadTime"].ToString() != "")
                {
                    model.DtFirstReadTime = DateTime.Parse(row["DtFirstReadTime"].ToString());
                }
                if (row["DtLastReadTime"] != null && row["DtLastReadTime"].ToString() != "")
                {
                    model.DtLastReadTime = DateTime.Parse(row["DtLastReadTime"].ToString());
                }
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nId,StrTrainmanGUID,StrFileGUID,NReadCount,DtFirstReadTime,DtLastReadTime ");
            strSql.Append(" FROM TAB_ReadDocPlan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }


    }
}
