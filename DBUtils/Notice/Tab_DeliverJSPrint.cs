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
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
namespace TF.RunSafty.DAL
{
	/// <summary>
	/// 数据访问类:Tab_DeliverJSPrint
	/// </summary>
	public partial class Tab_DeliverJSPrint
	{
		public Tab_DeliverJSPrint()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(nID) FROM Tab_DeliverJSPrint ");

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
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int nID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Tab_DeliverJSPrint");
			strSql.Append(" where nID=@nID");
			SqlParameter[] parameters = {
					new SqlParameter("@nID", SqlDbType.Int,4)
			};
			parameters[0].Value = nID;

            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TF.RunSafty.Model.Tab_DeliverJSPrint model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Tab_DeliverJSPrint(");
			strSql.Append("StrTrainmanGUID,StrPlanGUID,StrSiteGUID,dtPrintTime)");
			strSql.Append(" values (");
			strSql.Append("@StrTrainmanGUID,@StrPlanGUID,@StrSiteGUID,@dtPrintTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@StrTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtPrintTime", SqlDbType.DateTime)};
			parameters[0].Value = model.StrTrainmanGUID;
			parameters[1].Value = model.StrPlanGUID;
			parameters[2].Value = model.StrSiteGUID;
			parameters[3].Value = model.dtPrintTime;

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
		public bool Update(TF.RunSafty.Model.Tab_DeliverJSPrint model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Tab_DeliverJSPrint set ");
			strSql.Append("StrTrainmanGUID=@StrTrainmanGUID,");
			strSql.Append("StrPlanGUID=@StrPlanGUID,");
			strSql.Append("StrSiteGUID=@StrSiteGUID,");
			strSql.Append("dtPrintTime=@dtPrintTime");
			strSql.Append(" where nID=@nID");
			SqlParameter[] parameters = {
					new SqlParameter("@StrTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtPrintTime", SqlDbType.DateTime),
					new SqlParameter("@nID", SqlDbType.Int,4)};
			parameters[0].Value = model.StrTrainmanGUID;
			parameters[1].Value = model.StrPlanGUID;
			parameters[2].Value = model.StrSiteGUID;
			parameters[3].Value = model.dtPrintTime;
			parameters[4].Value = model.nID;

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
		public bool Delete(int nID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Tab_DeliverJSPrint ");
			strSql.Append(" where nID=@nID");
			SqlParameter[] parameters = {
					new SqlParameter("@nID", SqlDbType.Int,4)
			};
			parameters[0].Value = nID;

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
		public bool DeleteList(string nIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Tab_DeliverJSPrint ");
			strSql.Append(" where nID in ("+nIDlist + ")  ");
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
		public TF.RunSafty.Model.Tab_DeliverJSPrint GetModel(int nID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 nID,StrTrainmanGUID,StrPlanGUID,StrSiteGUID,dtPrintTime from Tab_DeliverJSPrint ");
			strSql.Append(" where nID=@nID");
			SqlParameter[] parameters = {
					new SqlParameter("@nID", SqlDbType.Int,4)
			};
			parameters[0].Value = nID;

			TF.RunSafty.Model.Tab_DeliverJSPrint model=new TF.RunSafty.Model.Tab_DeliverJSPrint();
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
		public TF.RunSafty.Model.Tab_DeliverJSPrint DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.Tab_DeliverJSPrint model=new TF.RunSafty.Model.Tab_DeliverJSPrint();
			if (row != null)
			{
				if(row["nID"]!=null && row["nID"].ToString()!="")
				{
					model.nID=int.Parse(row["nID"].ToString());
				}
				if(row["StrTrainmanGUID"]!=null)
				{
					model.StrTrainmanGUID=row["StrTrainmanGUID"].ToString();
				}
				if(row["StrPlanGUID"]!=null)
				{
					model.StrPlanGUID=row["StrPlanGUID"].ToString();
				}
				if(row["StrSiteGUID"]!=null)
				{
					model.StrSiteGUID=row["StrSiteGUID"].ToString();
				}
				if(row["dtPrintTime"]!=null && row["dtPrintTime"].ToString()!="")
				{
					model.dtPrintTime=DateTime.Parse(row["dtPrintTime"].ToString());
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
			strSql.Append("select nID,StrTrainmanGUID,StrPlanGUID,StrSiteGUID,dtPrintTime ");
			strSql.Append(" FROM Tab_DeliverJSPrint ");
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
			strSql.Append(" nID,StrTrainmanGUID,StrPlanGUID,StrSiteGUID,dtPrintTime ");
			strSql.Append(" FROM Tab_DeliverJSPrint ");
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
			strSql.Append("select count(1) FROM Tab_DeliverJSPrint ");
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
				strSql.Append("order by T.nID desc");
			}
			strSql.Append(")AS Row, T.*  from Tab_DeliverJSPrint T ");
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
			parameters[0].Value = "Tab_DeliverJSPrint";
			parameters[1].Value = "nID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

