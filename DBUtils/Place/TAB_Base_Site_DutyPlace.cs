﻿/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_Site_DutyPlace.cs
*
* 功 能： N/A
* 类 名： TAB_Base_Site_DutyPlace
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/17 9:42:50   N/A    初版
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
	/// 数据访问类:TAB_Base_Site_DutyPlace
	/// </summary>
	public partial class TAB_Base_Site_DutyPlace
	{
		public TAB_Base_Site_DutyPlace()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(nID) FROM TAB_Base_Site_DutyPlace ");

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
		public bool Exists(int nId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TAB_Base_Site_DutyPlace");
			strSql.Append(" where nId=@nId");
			SqlParameter[] parameters = {
					new SqlParameter("@nId", SqlDbType.Int,4)
			};
			parameters[0].Value = nId;

			return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TF.RunSafty.Model.TAB_Base_Site_DutyPlace model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_Base_Site_DutyPlace(");
			strSql.Append("strTrainJiaoluGUID,strPlaceID,strSiteGUID,nPlaceIndex)");
			strSql.Append(" values (");
			strSql.Append("@strTrainJiaoluGUID,@strPlaceID,@strSiteGUID,@nPlaceIndex)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strPlaceID", SqlDbType.VarChar,50),
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@nPlaceIndex", SqlDbType.Int,4)};
			parameters[0].Value = model.strTrainJiaoluGUID;
			parameters[1].Value = model.strPlaceID;
			parameters[2].Value = model.strSiteGUID;
			parameters[3].Value = model.nPlaceIndex;

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
		public bool Update(TF.RunSafty.Model.TAB_Base_Site_DutyPlace model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TAB_Base_Site_DutyPlace set ");
			strSql.Append("strTrainJiaoluGUID=@strTrainJiaoluGUID,");
			strSql.Append("strPlaceID=@strPlaceID,");
			strSql.Append("strSiteGUID=@strSiteGUID,");
			strSql.Append("nPlaceIndex=@nPlaceIndex");
			strSql.Append(" where nId=@nId");
			SqlParameter[] parameters = {
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strPlaceID", SqlDbType.VarChar,50),
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@nPlaceIndex", SqlDbType.Int,4),
					new SqlParameter("@nId", SqlDbType.Int,4)};
			parameters[0].Value = model.strTrainJiaoluGUID;
			parameters[1].Value = model.strPlaceID;
			parameters[2].Value = model.strSiteGUID;
			parameters[3].Value = model.nPlaceIndex;
			parameters[4].Value = model.nId;

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
			strSql.Append("delete from TAB_Base_Site_DutyPlace ");
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
			strSql.Append("delete from TAB_Base_Site_DutyPlace ");
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
        public bool DeleteByTrainJiaoluGUID(string strTrainJiaoluGUID, string strClientGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Base_Site_DutyPlace ");
            strSql.AppendFormat("  where  strTrainJiaoluGUID='{0}'  and strSiteGUID='{1}' ",strTrainJiaoluGUID,strClientGUID);
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
        /// 根据客户端编号删除出勤点信息
        /// </summary>
        /// <param name="strClientGUID"></param>
        /// <returns></returns>
        public bool DeleteByClientGUID(string strClientGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Base_Site_DutyPlace ");
            strSql.AppendFormat("  where  strSiteGUID='{0}'   ", strClientGUID);
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
		public TF.RunSafty.Model.TAB_Base_Site_DutyPlace GetModel(int nId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 nId,strTrainJiaoluGUID,strPlaceID,strSiteGUID,nPlaceIndex from TAB_Base_Site_DutyPlace ");
			strSql.Append(" where nId=@nId");
			SqlParameter[] parameters = {
					new SqlParameter("@nId", SqlDbType.Int,4)
			};
			parameters[0].Value = nId;

			TF.RunSafty.Model.TAB_Base_Site_DutyPlace model=new TF.RunSafty.Model.TAB_Base_Site_DutyPlace();
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
		public TF.RunSafty.Model.TAB_Base_Site_DutyPlace DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.TAB_Base_Site_DutyPlace model=new TF.RunSafty.Model.TAB_Base_Site_DutyPlace();
			if (row != null)
			{
				if(row["nId"]!=null && row["nId"].ToString()!="")
				{
					model.nId=int.Parse(row["nId"].ToString());
				}
				if(row["strTrainJiaoluGUID"]!=null)
				{
					model.strTrainJiaoluGUID=row["strTrainJiaoluGUID"].ToString();
				}
				if(row["strPlaceID"]!=null)
				{
					model.strPlaceID=row["strPlaceID"].ToString();
				}
				if(row["strSiteGUID"]!=null)
				{
					model.strSiteGUID=row["strSiteGUID"].ToString();
				}
				if(row["nPlaceIndex"]!=null && row["nPlaceIndex"].ToString()!="")
				{
					model.nPlaceIndex=int.Parse(row["nPlaceIndex"].ToString());
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
			strSql.Append("select nId,strTrainJiaoluGUID,strPlaceID,strSiteGUID,nPlaceIndex ");
			strSql.Append(" FROM TAB_Base_Site_DutyPlace ");
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
			strSql.Append(" nId,strTrainJiaoluGUID,strPlaceID,strSiteGUID,nPlaceIndex ");
			strSql.Append(" FROM TAB_Base_Site_DutyPlace ");
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
			strSql.Append("select count(1) FROM TAB_Base_Site_DutyPlace ");
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
			strSql.Append(")AS Row, T.*  from TAB_Base_Site_DutyPlace T ");
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
			parameters[0].Value = "TAB_Base_Site_DutyPlace";
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

        public DataSet GetPlaceOfClient(string strTrainJiao, string strSiteID)
        {
            string strWhere = string.Format(" strTrainJiaoluGUID='{0}' and strSiteGUID='{1}' ",strTrainJiao,strSiteID);
            return GetList(strWhere);
        }
		#endregion  ExtensionMethod
	}
}

