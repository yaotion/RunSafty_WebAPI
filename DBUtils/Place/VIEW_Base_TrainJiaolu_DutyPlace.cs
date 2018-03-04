/**  版本信息模板在安装目录下，可自行修改。
* VIEW_Base_TrainJiaolu_DutyPlace.cs
*
* 功 能： N/A
* 类 名： VIEW_Base_TrainJiaolu_DutyPlace
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/19 14:38:47   N/A    初版
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
	/// 数据访问类:VIEW_Base_TrainJiaolu_DutyPlace
	/// </summary>
	public partial class VIEW_Base_TrainJiaolu_DutyPlace
	{
		public VIEW_Base_TrainJiaolu_DutyPlace()
		{}
		#region  BasicMethod



		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace GetModel(int nId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 nId,strTrainJiaoluGUID,strPlaceID,nPlaceIndex,strPlaceName,strWorkShopGUID,dtCreateTime from VIEW_Base_TrainJiaolu_DutyPlace ");
			strSql.Append(" where nId=@nId");
			SqlParameter[] parameters = {
					new SqlParameter("@nId", SqlDbType.Int,4)
			};
			parameters[0].Value = nId;

			TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace model=new TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace();
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
		public TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace model=new TF.RunSafty.Model.VIEW_Base_TrainJiaolu_DutyPlace();
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
				if(row["nPlaceIndex"]!=null && row["nPlaceIndex"].ToString()!="")
				{
					model.nPlaceIndex=int.Parse(row["nPlaceIndex"].ToString());
				}
				if(row["strPlaceName"]!=null)
				{
					model.strPlaceName=row["strPlaceName"].ToString();
				}
				if(row["strWorkShopGUID"]!=null)
				{
					model.strWorkShopGUID=row["strWorkShopGUID"].ToString();
				}
				if(row["dtCreateTime"]!=null && row["dtCreateTime"].ToString()!="")
				{
					model.dtCreateTime=DateTime.Parse(row["dtCreateTime"].ToString());
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
			strSql.Append("select nId,strTrainJiaoluGUID,strPlaceID,nPlaceIndex,strPlaceName,strWorkShopGUID,dtCreateTime ");
			strSql.Append(" FROM VIEW_Base_TrainJiaolu_DutyPlace ");
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
			strSql.Append(" nId,strTrainJiaoluGUID,strPlaceID,nPlaceIndex,strPlaceName,strWorkShopGUID,dtCreateTime ");
			strSql.Append(" FROM VIEW_Base_TrainJiaolu_DutyPlace ");
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
			strSql.Append("select count(1) FROM VIEW_Base_TrainJiaolu_DutyPlace ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj =  SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
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
			strSql.Append(")AS Row, T.*  from VIEW_Base_TrainJiaolu_DutyPlace T ");
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
			parameters[0].Value = "VIEW_Base_TrainJiaolu_DutyPlace";
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
        public DataSet GetPlaceOfTrainJiaolu(string strTrainJiaolu)
        {
            string strWhere = string.Format(" strTrainJiaoluGUID='{0}' ",strTrainJiaolu);
            return this.GetList(strWhere);
        }
		#endregion  ExtensionMethod
	}
}

