/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_Bfjr_Dictionary.cs
*
* 功 能： N/A
* 类 名： TAB_Base_Bfjr_Dictionary
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-10-18 09:22:02   N/A    初版
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
	/// 数据访问类:TAB_Base_Bfjr_Dictionary
	/// </summary>
	public partial class TAB_Base_Bfjr_Dictionary
	{
		public TAB_Base_Bfjr_Dictionary()
		{}
		#region  BasicMethod

		 

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(TF.RunSafty.Model.TAB_Base_Bfjr_Dictionary model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_Base_Bfjr_Dictionary(");
			strSql.Append("unitid,strJiWuDuan)");
			strSql.Append(" values (");
			strSql.Append("@unitid,@strJiWuDuan)");
			SqlParameter[] parameters = {
					new SqlParameter("@unitid", SqlDbType.VarChar,50),
					new SqlParameter("@strJiWuDuan", SqlDbType.VarChar,50)};
			parameters[0].Value = model.unitid;
			parameters[1].Value = model.strJiWuDuan;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(TF.RunSafty.Model.TAB_Base_Bfjr_Dictionary model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TAB_Base_Bfjr_Dictionary set ");
			strSql.Append("strJiWuDuan=@strJiWuDuan");
			strSql.Append(" where unitid=@unitid ");
			SqlParameter[] parameters = {
					new SqlParameter("@strJiWuDuan", SqlDbType.VarChar,50),
					new SqlParameter("@unitid", SqlDbType.VarChar,50)};
			parameters[0].Value = model.strJiWuDuan;
			parameters[1].Value = model.unitid;

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
		public bool Delete(string unitid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_Base_Bfjr_Dictionary ");
			strSql.Append(" where unitid=@unitid ");
			SqlParameter[] parameters = {
					new SqlParameter("@unitid", SqlDbType.VarChar,50)			};
			parameters[0].Value = unitid;

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
		public bool DeleteList(string unitidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_Base_Bfjr_Dictionary ");
			strSql.Append(" where unitid in ("+unitidlist + ")  ");
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
		public TF.RunSafty.Model.TAB_Base_Bfjr_Dictionary GetModel(string unitid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 unitid,strJiWuDuan from TAB_Base_Bfjr_Dictionary ");
			strSql.Append(" where unitid=@unitid ");
			SqlParameter[] parameters = {
					new SqlParameter("@unitid", SqlDbType.VarChar,50)			};
			parameters[0].Value = unitid;

			TF.RunSafty.Model.TAB_Base_Bfjr_Dictionary model=new TF.RunSafty.Model.TAB_Base_Bfjr_Dictionary();
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
		public TF.RunSafty.Model.TAB_Base_Bfjr_Dictionary DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.TAB_Base_Bfjr_Dictionary model=new TF.RunSafty.Model.TAB_Base_Bfjr_Dictionary();
			if (row != null)
			{
				if(row["unitid"]!=null)
				{
					model.unitid=row["unitid"].ToString();
				}
				if(row["strJiWuDuan"]!=null)
				{
					model.strJiWuDuan=row["strJiWuDuan"].ToString();
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
			strSql.Append("select unitid,strJiWuDuan ");
			strSql.Append(" FROM TAB_Base_Bfjr_Dictionary ");
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
			strSql.Append(" unitid,strJiWuDuan ");
			strSql.Append(" FROM TAB_Base_Bfjr_Dictionary ");
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
			strSql.Append("select count(1) FROM TAB_Base_Bfjr_Dictionary ");
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
				strSql.Append("order by T.unitid desc");
			}
			strSql.Append(")AS Row, T.*  from TAB_Base_Bfjr_Dictionary T ");
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
			parameters[0].Value = "TAB_Base_Bfjr_Dictionary";
			parameters[1].Value = "unitid";
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

