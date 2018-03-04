/**  版本信息模板在安装目录下，可自行修改。
* TAB_ReadDocPlan.cs
*
* 功 能： N/A
* 类 名： TAB_ReadDocPlan
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/15 10:42:19   N/A    初版
*
* Copyright (c) 2014 thinkfreely Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：郑州畅想高科股份有限公司　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient; 
using ThinkFreely.DBUtility;
using System.Collections;
using System.Collections.Generic;
namespace TF.RunSafty.DAL
{
	/// <summary>
	/// 数据访问类:TAB_ReadDocPlan
	/// </summary>
	public partial class TAB_ReadDocPlan
	{
		public TAB_ReadDocPlan()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
        //public int GetMaxId()
        //{
        //return DbHelperSQL.GetMaxID("nId", "TAB_ReadDocPlan"); 
        //}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int nId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TAB_ReadDocPlan");
			strSql.Append(" where nId=@nId");
			SqlParameter[] parameters = {
					new SqlParameter("@nId", SqlDbType.Int,4)
			};
			parameters[0].Value = nId;

            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}

        public void Add(DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnString))
            {
                SqlBulkCopy bulk = new SqlBulkCopy(conn);  
                bulk.DestinationTableName = "TAB_ReadDocPlan";
                bulk.ColumnMappings.Add("StrTrainmanGUID", "StrTrainmanGUID");
                bulk.ColumnMappings.Add("StrFileGUID", "StrFileGUID");
                bulk.ColumnMappings.Add("NReadCount", "NReadCount");
                bulk.ColumnMappings.Add("DtFirstReadTime", "DtFirstReadTime");
                bulk.ColumnMappings.Add("DtLastReadTime", "DtLastReadTime");
                bulk.BatchSize = dt.Rows.Count;
                if (dt != null && dt.Rows.Count != 0)
                {
                    conn.Open();
                    bulk.WriteToServer(dt);
                }
                bulk.Close();
            }
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TF.RunSafty.Model.TAB_ReadDocPlan model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_ReadDocPlan(");
			strSql.Append("StrTrainmanGUID,StrFileGUID,NReadCount,DtFirstReadTime,DtLastReadTime)");
			strSql.Append(" values (");
			strSql.Append("@StrTrainmanGUID,@StrFileGUID,@NReadCount,@DtFirstReadTime,@DtLastReadTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@StrTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrFileGUID", SqlDbType.VarChar,50),
					new SqlParameter("@NReadCount", SqlDbType.Int,4),
					new SqlParameter("@DtFirstReadTime", SqlDbType.DateTime),
					new SqlParameter("@DtLastReadTime", SqlDbType.DateTime)};
			parameters[0].Value = model.StrTrainmanGUID;
			parameters[1].Value = model.StrFileGUID;
			parameters[2].Value = model.NReadCount;
			parameters[3].Value = model.DtFirstReadTime;
			parameters[4].Value = model.DtLastReadTime;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);	
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_ReadDocPlan model)
		{
			StringBuilder strSql=new StringBuilder();
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
        /// 删除旧的阅读计划
        /// </summary>
        /// <param name="strFileGUID"></param>
        /// <returns></returns>
        public bool DeleteOldPlan(string strFileGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_ReadDocPlan ");
            strSql.Append(" where StrFileGUID=@strFileGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@strFileGUID",strFileGUID)
			}; 

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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int nId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_ReadDocPlan ");
			strSql.Append(" where nId=@nId");
			SqlParameter[] parameters = {
					new SqlParameter("@nId", SqlDbType.Int,4)
			};
			parameters[0].Value = nId;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string nIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_ReadDocPlan ");
			strSql.Append(" where nId in ("+nIdlist + ")  ");
            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
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
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.TAB_ReadDocPlan GetModel(int nId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 nId,StrTrainmanGUID,StrFileGUID,NReadCount,DtFirstReadTime,DtLastReadTime from TAB_ReadDocPlan ");
			strSql.Append(" where nId=@nId");
			SqlParameter[] parameters = {
					new SqlParameter("@nId", SqlDbType.Int,4)
			};
			parameters[0].Value = nId;

			TF.RunSafty.Model.TAB_ReadDocPlan model=new TF.RunSafty.Model.TAB_ReadDocPlan();
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.TAB_ReadDocPlan DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.TAB_ReadDocPlan model=new TF.RunSafty.Model.TAB_ReadDocPlan();
			if (row != null)
			{
				if(row["nId"]!=null && row["nId"].ToString()!="")
				{
					model.nId=int.Parse(row["nId"].ToString());
				}
				if(row["StrTrainmanGUID"]!=null)
				{
					model.StrTrainmanGUID=row["StrTrainmanGUID"].ToString();
				}
				if(row["StrFileGUID"]!=null)
				{
					model.StrFileGUID=row["StrFileGUID"].ToString();
				}
				if(row["NReadCount"]!=null && row["NReadCount"].ToString()!="")
				{
					model.NReadCount=int.Parse(row["NReadCount"].ToString());
				}
				if(row["DtFirstReadTime"]!=null && row["DtFirstReadTime"].ToString()!="")
				{
					model.DtFirstReadTime=DateTime.Parse(row["DtFirstReadTime"].ToString());
				}
				if(row["DtLastReadTime"]!=null && row["DtLastReadTime"].ToString()!="")
				{
					model.DtLastReadTime=DateTime.Parse(row["DtLastReadTime"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select nId,StrTrainmanGUID,StrFileGUID,NReadCount,DtFirstReadTime,DtLastReadTime ");
			strSql.Append(" FROM TAB_ReadDocPlan ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" nId,StrTrainmanGUID,StrFileGUID,NReadCount,DtFirstReadTime,DtLastReadTime ");
			strSql.Append(" FROM TAB_ReadDocPlan ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM TAB_ReadDocPlan ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.nId desc");
			}
			strSql.Append(")AS Row, T.*  from TAB_ReadDocPlan T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "TAB_ReadDocPlan";
			parameters[1].Value = "nId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 获取阅读记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetReadingHistory(string strWhere, List<SqlParameter> parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from View_ReadRecord");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters.ToArray()).Tables[0];
        }
        /// <summary>
        /// 获取阅读记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetReadingHistoryLate(string strWhere,List<SqlParameter> parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from View_ReadRecord_Late");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(),parameters.ToArray()).Tables[0];
        }
        public DataTable GetReadingHistoryOfTrainman(string strTrainmanGUID, string strFileType)
        {
            string dtDateTime = DateTime.Now.ToString();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strFileGUID,strFileName,dtReadTime from VIEW_ReadingRecordByTrainman where strTrainmanGUID=@strTrainmanGUID and StrTypeGUID=@StrTypeGUID and dtEndTime>@dtEndTime");
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("strTrainmanGUID",strTrainmanGUID)
               ,new SqlParameter("StrTypeGUID",strFileType)
               ,new SqlParameter("dtEndTime",dtDateTime)
            };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters).Tables[0];


            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select strFileGUID,strFileName,dtReadTime from VIEW_ReadingRecordByTrainman where strTrainmanGUID=@strTrainmanGUID and StrTypeGUID=@StrTypeGUID");
            //SqlParameter[] parameters = new SqlParameter[]{
            //    new SqlParameter("strTrainmanGUID",strTrainmanGUID)
            //   ,new SqlParameter("StrTypeGUID",strFileType)
            //};
            //return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters).Tables[0];
        
        }
		#endregion  ExtensionMethod
	}
}

