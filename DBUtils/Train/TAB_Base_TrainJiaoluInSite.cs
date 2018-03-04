/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_TrainJiaoluInSite.cs
*
* 功 能： N/A
* 类 名： TAB_Base_TrainJiaoluInSite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/25 10:33:25   N/A    初版
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
	/// 数据访问类:TAB_Base_TrainJiaoluInSite
	/// </summary>
	public partial class TAB_Base_TrainJiaoluInSite
	{
		public TAB_Base_TrainJiaoluInSite()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string strSiteGUID,string strTrainJiaoluGUID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TAB_Base_TrainJiaoluInSite");
			strSql.Append(" where strSiteGUID=@strSiteGUID and strTrainJiaoluGUID=@strTrainJiaoluGUID ");
			SqlParameter[] parameters = {
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50)			};
			parameters[0].Value = strSiteGUID;
			parameters[1].Value = strTrainJiaoluGUID;

            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_Base_TrainJiaoluInSite(");
			strSql.Append("strSiteGUID,strTrainJiaoluGUID,strJiaoluInSiteGUID)");
			strSql.Append(" values (");
			strSql.Append("@strSiteGUID,@strTrainJiaoluGUID,@strJiaoluInSiteGUID)");
			SqlParameter[] parameters = {
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strJiaoluInSiteGUID", SqlDbType.VarChar,50)};
			parameters[0].Value = model.strSiteGUID;
			parameters[1].Value = model.strTrainJiaoluGUID;
			parameters[2].Value = model.strJiaoluInSiteGUID;

            int rows = (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
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
		public bool Update(TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TAB_Base_TrainJiaoluInSite set ");
			strSql.Append("strJiaoluInSiteGUID=@strJiaoluInSiteGUID");
			strSql.Append(" where strSiteGUID=@strSiteGUID and strTrainJiaoluGUID=@strTrainJiaoluGUID ");
			SqlParameter[] parameters = {
					new SqlParameter("@strJiaoluInSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50)};
			parameters[0].Value = model.strJiaoluInSiteGUID;
			parameters[1].Value = model.strSiteGUID;
			parameters[2].Value = model.strTrainJiaoluGUID;

            int rows = (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
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
		public bool Delete(string strSiteGUID,string strTrainJiaoluGUID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_Base_TrainJiaoluInSite ");
			strSql.Append(" where strSiteGUID=@strSiteGUID and strTrainJiaoluGUID=@strTrainJiaoluGUID ");
			SqlParameter[] parameters = {
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50)			};
			parameters[0].Value = strSiteGUID;
			parameters[1].Value = strTrainJiaoluGUID;

            int rows = (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
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
		public TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite GetModel(string strSiteGUID,string strTrainJiaoluGUID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 strSiteGUID,strTrainJiaoluGUID,strJiaoluInSiteGUID from TAB_Base_TrainJiaoluInSite ");
			strSql.Append(" where strSiteGUID=@strSiteGUID and strTrainJiaoluGUID=@strTrainJiaoluGUID ");
			SqlParameter[] parameters = {
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50)			};
			parameters[0].Value = strSiteGUID;
			parameters[1].Value = strTrainJiaoluGUID;

			TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite model=new TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite();
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
		public TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite model=new TF.RunSafty.Model.TAB_Base_TrainJiaoluInSite();
			if (row != null)
			{
				if(row["strSiteGUID"]!=null)
				{
					model.strSiteGUID=row["strSiteGUID"].ToString();
				}
				if(row["strTrainJiaoluGUID"]!=null)
				{
					model.strTrainJiaoluGUID=row["strTrainJiaoluGUID"].ToString();
				}
				if(row["strJiaoluInSiteGUID"]!=null)
				{
					model.strJiaoluInSiteGUID=row["strJiaoluInSiteGUID"].ToString();
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
			strSql.Append("select strSiteGUID,strTrainJiaoluGUID,strJiaoluInSiteGUID ");
			strSql.Append(" FROM TAB_Base_TrainJiaoluInSite ");
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
			strSql.Append(" strSiteGUID,strTrainJiaoluGUID,strJiaoluInSiteGUID ");
			strSql.Append(" FROM TAB_Base_TrainJiaoluInSite ");
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
			strSql.Append("select count(1) FROM TAB_Base_TrainJiaoluInSite ");
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
				strSql.Append("order by T.strTrainJiaoluGUID desc");
			}
			strSql.Append(")AS Row, T.*  from TAB_Base_TrainJiaoluInSite T ");
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
			parameters[0].Value = "TAB_Base_TrainJiaoluInSite";
			parameters[1].Value = "strTrainJiaoluGUID";
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

