/**  版本信息模板在安装目录下，可自行修改。
* TAB_MsgCallWork_Record.cs
*
* 功 能： N/A
* 类 名： TAB_MsgCallWork_Record
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-10-10 13:06:46   N/A    初版
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
	/// 数据访问类:TAB_MsgCallWork_Record
	/// </summary>
	public partial class TAB_MsgCallWork_Record
	{
		public TAB_MsgCallWork_Record()
		{}
		#region  BasicMethod
 
 

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TF.RunSafty.Model.TAB_MsgCallWork_Record model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_MsgCallWork_Record(");
			strSql.Append("strGUID,strMsgContent,strCallWorkGUID,dtTime,nType,nResult,strSenderPhone,strReceiverPhone)");
			strSql.Append(" values (");
			strSql.Append("@strGUID,@strMsgContent,@strCallWorkGUID,@dtTime,@nType,@nResult,@strSenderPhone,@strReceiverPhone)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@strGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strMsgContent", SqlDbType.VarChar,200),
					new SqlParameter("@strCallWorkGUID", SqlDbType.VarChar,200),
					new SqlParameter("@dtTime", SqlDbType.DateTime),
					new SqlParameter("@nType", SqlDbType.Int,4),
					new SqlParameter("@nResult", SqlDbType.Int,4),
					new SqlParameter("@strSenderPhone", SqlDbType.VarChar,20),
					new SqlParameter("@strReceiverPhone", SqlDbType.VarChar,20)};
			parameters[0].Value = model.strGUID;
			parameters[1].Value = model.strMsgContent;
			parameters[2].Value = model.strCallWorkGUID;
			parameters[3].Value = model.dtTime;
			parameters[4].Value = model.nType;
			parameters[5].Value = model.nResult;
			parameters[6].Value = model.strSenderPhone;
			parameters[7].Value = model.strReceiverPhone;

			object obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
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
		public bool Update(TF.RunSafty.Model.TAB_MsgCallWork_Record model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TAB_MsgCallWork_Record set ");
			strSql.Append("strGUID=@strGUID,");
			strSql.Append("strMsgContent=@strMsgContent,");
			strSql.Append("strCallWorkGUID=@strCallWorkGUID,");
			strSql.Append("dtTime=@dtTime,");
			strSql.Append("nType=@nType,");
			strSql.Append("nResult=@nResult,");
			strSql.Append("strSenderPhone=@strSenderPhone,");
			strSql.Append("strReceiverPhone=@strReceiverPhone");
			strSql.Append(" where nId=@nId");
			SqlParameter[] parameters = {
					new SqlParameter("@strGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strMsgContent", SqlDbType.VarChar,200),
					new SqlParameter("@strCallWorkGUID", SqlDbType.VarChar,200),
					new SqlParameter("@dtTime", SqlDbType.DateTime),
					new SqlParameter("@nType", SqlDbType.Int,4),
					new SqlParameter("@nResult", SqlDbType.Int,4),
					new SqlParameter("@strSenderPhone", SqlDbType.VarChar,20),
					new SqlParameter("@strReceiverPhone", SqlDbType.VarChar,20),
					new SqlParameter("@nId", SqlDbType.Int,4)};
			parameters[0].Value = model.strGUID;
			parameters[1].Value = model.strMsgContent;
			parameters[2].Value = model.strCallWorkGUID;
			parameters[3].Value = model.dtTime;
			parameters[4].Value = model.nType;
			parameters[5].Value = model.nResult;
			parameters[6].Value = model.strSenderPhone;
			parameters[7].Value = model.strReceiverPhone;
			parameters[8].Value = model.nId;

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
			strSql.Append("delete from TAB_MsgCallWork_Record ");
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
			strSql.Append("delete from TAB_MsgCallWork_Record ");
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
		public TF.RunSafty.Model.TAB_MsgCallWork_Record GetModel(int nId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 nId,strGUID,strMsgContent,strCallWorkGUID,dtTime,nType,nResult,strSenderPhone,strReceiverPhone from TAB_MsgCallWork_Record ");
			strSql.Append(" where nId=@nId");
			SqlParameter[] parameters = {
					new SqlParameter("@nId", SqlDbType.Int,4)
			};
			parameters[0].Value = nId;

			TF.RunSafty.Model.TAB_MsgCallWork_Record model=new TF.RunSafty.Model.TAB_MsgCallWork_Record();
			DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
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
		public TF.RunSafty.Model.TAB_MsgCallWork_Record DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.TAB_MsgCallWork_Record model=new TF.RunSafty.Model.TAB_MsgCallWork_Record();
			if (row != null)
			{
				if(row["nId"]!=null && row["nId"].ToString()!="")
				{
					model.nId=int.Parse(row["nId"].ToString());
				}
				if(row["strGUID"]!=null)
				{
					model.strGUID=row["strGUID"].ToString();
				}
				if(row["strMsgContent"]!=null)
				{
					model.strMsgContent=row["strMsgContent"].ToString();
				}
				if(row["strCallWorkGUID"]!=null)
				{
					model.strCallWorkGUID=row["strCallWorkGUID"].ToString();
				}
				if(row["dtTime"]!=null && row["dtTime"].ToString()!="")
				{
					model.dtTime=DateTime.Parse(row["dtTime"].ToString());
				}
				if(row["nType"]!=null && row["nType"].ToString()!="")
				{
					model.nType=int.Parse(row["nType"].ToString());
				}
				if(row["nResult"]!=null && row["nResult"].ToString()!="")
				{
					model.nResult=int.Parse(row["nResult"].ToString());
				}
				if(row["strSenderPhone"]!=null)
				{
					model.strSenderPhone=row["strSenderPhone"].ToString();
				}
				if(row["strReceiverPhone"]!=null)
				{
					model.strReceiverPhone=row["strReceiverPhone"].ToString();
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
			strSql.Append("select nId,strGUID,strMsgContent,strCallWorkGUID,dtTime,nType,nResult,strSenderPhone,strReceiverPhone ");
			strSql.Append(" FROM TAB_MsgCallWork_Record ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql.ToString());
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
			strSql.Append(" nId,strGUID,strMsgContent,strCallWorkGUID,dtTime,nType,nResult,strSenderPhone,strReceiverPhone ");
			strSql.Append(" FROM TAB_MsgCallWork_Record ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM TAB_MsgCallWork_Record ");
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
			strSql.Append(")AS Row, T.*  from TAB_MsgCallWork_Record T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql.ToString());
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
			parameters[0].Value = "TAB_MsgCallWork_Record";
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

		#endregion  ExtensionMethod
	}
}

