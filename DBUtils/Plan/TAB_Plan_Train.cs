/**  版本信息模板在安装目录下，可自行修改。
* TAB_Plan_Train.cs
*
* 功 能： N/A
* 类 名： TAB_Plan_Train
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/29 9:25:00   N/A    初版
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
	/// 数据访问类:TAB_Plan_Train
	/// </summary>
	public partial class TAB_Plan_Train
	{
		public TAB_Plan_Train()
		{}
		#region  BasicMethod
 
		 

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TF.RunSafty.Model.TAB_Plan_Train model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_Plan_Train(");
			strSql.Append("strTrainPlanGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,nPlanState,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,dtFirstStartTime,dtChuQinTime,nNeedRest,dtArriveTime,dtCallTime,strBak1,dtLastArriveTime,strMainPlanGUID,strPlaceID)");
			strSql.Append(" values (");
			strSql.Append("@strTrainPlanGUID,@strTrainTypeName,@strTrainNumber,@strTrainNo,@dtStartTime,@dtRealStartTime,@strTrainJiaoluGUID,@strStartStation,@strEndStation,@nTrainmanTypeID,@nPlanType,@nDragType,@nKehuoID,@nRemarkType,@strRemark,@nPlanState,@dtCreateTime,@strCreateSiteGUID,@strCreateUserGUID,@dtFirstStartTime,@dtChuQinTime,@nNeedRest,@dtArriveTime,@dtCallTime,@strBak1,@dtLastArriveTime,@strMainPlanGUID,@strPlaceID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainTypeName", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainNumber", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainNo", SqlDbType.VarChar,50),
					new SqlParameter("@dtStartTime", SqlDbType.DateTime),
					new SqlParameter("@dtRealStartTime", SqlDbType.DateTime),
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strStartStation", SqlDbType.VarChar,50),
					new SqlParameter("@strEndStation", SqlDbType.VarChar,50),
					new SqlParameter("@nTrainmanTypeID", SqlDbType.Int,4),
					new SqlParameter("@nPlanType", SqlDbType.Int,4),
					new SqlParameter("@nDragType", SqlDbType.Int,4),
					new SqlParameter("@nKehuoID", SqlDbType.Int,4),
					new SqlParameter("@nRemarkType", SqlDbType.Int,4),
					new SqlParameter("@strRemark", SqlDbType.VarChar,50),
					new SqlParameter("@nPlanState", SqlDbType.Int,4),
					new SqlParameter("@dtCreateTime", SqlDbType.DateTime),
					new SqlParameter("@strCreateSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strCreateUserGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtFirstStartTime", SqlDbType.DateTime),
					new SqlParameter("@dtChuQinTime", SqlDbType.DateTime),
					new SqlParameter("@nNeedRest", SqlDbType.Int,4),
					new SqlParameter("@dtArriveTime", SqlDbType.DateTime),
					new SqlParameter("@dtCallTime", SqlDbType.DateTime),
					new SqlParameter("@strBak1", SqlDbType.VarChar,50),
					new SqlParameter("@dtLastArriveTime", SqlDbType.DateTime),
					new SqlParameter("@strMainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strPlaceID", SqlDbType.VarChar,50)};
			parameters[0].Value = model.strTrainPlanGUID;
			parameters[1].Value = model.strTrainTypeName;
			parameters[2].Value = model.strTrainNumber;
			parameters[3].Value = model.strTrainNo;
			parameters[4].Value = model.dtStartTime;
			parameters[5].Value = model.dtRealStartTime;
			parameters[6].Value = model.strTrainJiaoluGUID;
			parameters[7].Value = model.strStartStation;
			parameters[8].Value = model.strEndStation;
			parameters[9].Value = model.nTrainmanTypeID;
			parameters[10].Value = model.nPlanType;
			parameters[11].Value = model.nDragType;
			parameters[12].Value = model.nKehuoID;
			parameters[13].Value = model.nRemarkType;
			parameters[14].Value = model.strRemark;
			parameters[15].Value = model.nPlanState;
			parameters[16].Value = model.dtCreateTime;
			parameters[17].Value = model.strCreateSiteGUID;
			parameters[18].Value = model.strCreateUserGUID;
			parameters[19].Value = model.dtFirstStartTime;
			parameters[20].Value = model.dtChuQinTime;
			parameters[21].Value = model.nNeedRest;
			parameters[22].Value = model.dtArriveTime;
			parameters[23].Value = model.dtCallTime;
			parameters[24].Value = model.strBak1;
			parameters[25].Value = model.dtLastArriveTime;
			parameters[26].Value = model.strMainPlanGUID;
			parameters[27].Value = model.strPlaceID;

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
		public bool Update(TF.RunSafty.Model.TAB_Plan_Train model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TAB_Plan_Train set ");
			strSql.Append("strTrainTypeName=@strTrainTypeName,");
			strSql.Append("strTrainNumber=@strTrainNumber,");
			strSql.Append("strTrainNo=@strTrainNo,");
			strSql.Append("dtRealStartTime=@dtRealStartTime,");
			strSql.Append("strTrainJiaoluGUID=@strTrainJiaoluGUID,");
			strSql.Append("strStartStation=@strStartStation,");
			strSql.Append("strEndStation=@strEndStation,");
			strSql.Append("nTrainmanTypeID=@nTrainmanTypeID,");
			strSql.Append("nPlanType=@nPlanType,");
			strSql.Append("nDragType=@nDragType,");
			strSql.Append("nKehuoID=@nKehuoID,");
			strSql.Append("nRemarkType=@nRemarkType,");
			strSql.Append("strRemark=@strRemark,");
			strSql.Append("dtCreateTime=@dtCreateTime,");
			strSql.Append("strCreateSiteGUID=@strCreateSiteGUID,");
			strSql.Append("strCreateUserGUID=@strCreateUserGUID,");
			strSql.Append("dtFirstStartTime=@dtFirstStartTime,");
			strSql.Append("dtChuQinTime=@dtChuQinTime,");
			strSql.Append("nNeedRest=@nNeedRest,");
			strSql.Append("dtArriveTime=@dtArriveTime,");
			strSql.Append("dtCallTime=@dtCallTime,");
			strSql.Append("strBak1=@strBak1,");
			strSql.Append("dtLastArriveTime=@dtLastArriveTime,");
			strSql.Append("strMainPlanGUID=@strMainPlanGUID,");
			strSql.Append("strPlaceID=@strPlaceID");
			strSql.Append(" where nid=@nid");
			SqlParameter[] parameters = {
					new SqlParameter("@strTrainTypeName", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainNumber", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainNo", SqlDbType.VarChar,50),
					new SqlParameter("@dtRealStartTime", SqlDbType.DateTime),
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strStartStation", SqlDbType.VarChar,50),
					new SqlParameter("@strEndStation", SqlDbType.VarChar,50),
					new SqlParameter("@nTrainmanTypeID", SqlDbType.Int,4),
					new SqlParameter("@nPlanType", SqlDbType.Int,4),
					new SqlParameter("@nDragType", SqlDbType.Int,4),
					new SqlParameter("@nKehuoID", SqlDbType.Int,4),
					new SqlParameter("@nRemarkType", SqlDbType.Int,4),
					new SqlParameter("@strRemark", SqlDbType.VarChar,50),
					new SqlParameter("@dtCreateTime", SqlDbType.DateTime),
					new SqlParameter("@strCreateSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strCreateUserGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtFirstStartTime", SqlDbType.DateTime),
					new SqlParameter("@dtChuQinTime", SqlDbType.DateTime),
					new SqlParameter("@nNeedRest", SqlDbType.Int,4),
					new SqlParameter("@dtArriveTime", SqlDbType.DateTime),
					new SqlParameter("@dtCallTime", SqlDbType.DateTime),
					new SqlParameter("@strBak1", SqlDbType.VarChar,50),
					new SqlParameter("@dtLastArriveTime", SqlDbType.DateTime),
					new SqlParameter("@strMainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strPlaceID", SqlDbType.VarChar,50),
					new SqlParameter("@nid", SqlDbType.Int,4),
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtStartTime", SqlDbType.DateTime),
					new SqlParameter("@nPlanState", SqlDbType.Int,4)};
			parameters[0].Value = model.strTrainTypeName;
			parameters[1].Value = model.strTrainNumber;
			parameters[2].Value = model.strTrainNo;
			parameters[3].Value = model.dtRealStartTime;
			parameters[4].Value = model.strTrainJiaoluGUID;
			parameters[5].Value = model.strStartStation;
			parameters[6].Value = model.strEndStation;
			parameters[7].Value = model.nTrainmanTypeID;
			parameters[8].Value = model.nPlanType;
			parameters[9].Value = model.nDragType;
			parameters[10].Value = model.nKehuoID;
			parameters[11].Value = model.nRemarkType;
			parameters[12].Value = model.strRemark;
			parameters[13].Value = model.dtCreateTime;
			parameters[14].Value = model.strCreateSiteGUID;
			parameters[15].Value = model.strCreateUserGUID;
			parameters[16].Value = model.dtFirstStartTime;
			parameters[17].Value = model.dtChuQinTime;
			parameters[18].Value = model.nNeedRest;
			parameters[19].Value = model.dtArriveTime;
			parameters[20].Value = model.dtCallTime;
			parameters[21].Value = model.strBak1;
			parameters[22].Value = model.dtLastArriveTime;
			parameters[23].Value = model.strMainPlanGUID;
			parameters[24].Value = model.strPlaceID;
			parameters[25].Value = model.nid;
			parameters[26].Value = model.strTrainPlanGUID;
			parameters[27].Value = model.dtStartTime;
			parameters[28].Value = model.nPlanState;

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
		public bool Delete(int nid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_Plan_Train ");
			strSql.Append(" where nid=@nid");
			SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;

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
		public bool Delete(string strTrainPlanGUID,DateTime dtStartTime,int nPlanState)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_Plan_Train ");
			strSql.Append(" where strTrainPlanGUID=@strTrainPlanGUID and dtStartTime=@dtStartTime and nPlanState=@nPlanState ");
			SqlParameter[] parameters = {
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtStartTime", SqlDbType.DateTime),
					new SqlParameter("@nPlanState", SqlDbType.Int,4)			};
			parameters[0].Value = strTrainPlanGUID;
			parameters[1].Value = dtStartTime;
			parameters[2].Value = nPlanState;

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
		public bool DeleteList(string nidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_Plan_Train ");
			strSql.Append(" where nid in ("+nidlist + ")  ");
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
		public TF.RunSafty.Model.TAB_Plan_Train GetModel(int nid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 nid,strTrainPlanGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,nPlanState,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,dtFirstStartTime,dtChuQinTime,nNeedRest,dtArriveTime,dtCallTime,strBak1,dtLastArriveTime,strMainPlanGUID,strPlaceID from TAB_Plan_Train ");
			strSql.Append(" where nid=@nid");
			SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;

			TF.RunSafty.Model.TAB_Plan_Train model=new TF.RunSafty.Model.TAB_Plan_Train();
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
		public TF.RunSafty.Model.TAB_Plan_Train DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.TAB_Plan_Train model=new TF.RunSafty.Model.TAB_Plan_Train();
			if (row != null)
			{
				if(row["nid"]!=null && row["nid"].ToString()!="")
				{
					model.nid=int.Parse(row["nid"].ToString());
				}
				if(row["strTrainPlanGUID"]!=null)
				{
					model.strTrainPlanGUID=row["strTrainPlanGUID"].ToString();
				}
				if(row["strTrainTypeName"]!=null)
				{
					model.strTrainTypeName=row["strTrainTypeName"].ToString();
				}
				if(row["strTrainNumber"]!=null)
				{
					model.strTrainNumber=row["strTrainNumber"].ToString();
				}
				if(row["strTrainNo"]!=null)
				{
					model.strTrainNo=row["strTrainNo"].ToString();
				}
				if(row["dtStartTime"]!=null && row["dtStartTime"].ToString()!="")
				{
					model.dtStartTime=DateTime.Parse(row["dtStartTime"].ToString());
				}
				if(row["dtRealStartTime"]!=null && row["dtRealStartTime"].ToString()!="")
				{
					model.dtRealStartTime=DateTime.Parse(row["dtRealStartTime"].ToString());
				}
				if(row["strTrainJiaoluGUID"]!=null)
				{
					model.strTrainJiaoluGUID=row["strTrainJiaoluGUID"].ToString();
				}
				if(row["strStartStation"]!=null)
				{
					model.strStartStation=row["strStartStation"].ToString();
				}
				if(row["strEndStation"]!=null)
				{
					model.strEndStation=row["strEndStation"].ToString();
				}
				if(row["nTrainmanTypeID"]!=null && row["nTrainmanTypeID"].ToString()!="")
				{
					model.nTrainmanTypeID=int.Parse(row["nTrainmanTypeID"].ToString());
				}
				if(row["nPlanType"]!=null && row["nPlanType"].ToString()!="")
				{
					model.nPlanType=int.Parse(row["nPlanType"].ToString());
				}
				if(row["nDragType"]!=null && row["nDragType"].ToString()!="")
				{
					model.nDragType=int.Parse(row["nDragType"].ToString());
				}
				if(row["nKehuoID"]!=null && row["nKehuoID"].ToString()!="")
				{
					model.nKehuoID=int.Parse(row["nKehuoID"].ToString());
				}
				if(row["nRemarkType"]!=null && row["nRemarkType"].ToString()!="")
				{
					model.nRemarkType=int.Parse(row["nRemarkType"].ToString());
				}
				if(row["strRemark"]!=null)
				{
					model.strRemark=row["strRemark"].ToString();
				}
				if(row["nPlanState"]!=null && row["nPlanState"].ToString()!="")
				{
					model.nPlanState=int.Parse(row["nPlanState"].ToString());
				}
				if(row["dtCreateTime"]!=null && row["dtCreateTime"].ToString()!="")
				{
					model.dtCreateTime=DateTime.Parse(row["dtCreateTime"].ToString());
				}
				if(row["strCreateSiteGUID"]!=null)
				{
					model.strCreateSiteGUID=row["strCreateSiteGUID"].ToString();
				}
				if(row["strCreateUserGUID"]!=null)
				{
					model.strCreateUserGUID=row["strCreateUserGUID"].ToString();
				}
				if(row["dtFirstStartTime"]!=null && row["dtFirstStartTime"].ToString()!="")
				{
					//model.dtFirstStartTime=DateTime.Parse(row["dtFirstStartTime"].ToString());
                    DateTime dtFirstStartTime;
                    if (DateTime.TryParse(row["dtFirstStartTime"].ToString(), out dtFirstStartTime))
                    {
                        model.dtFirstStartTime = dtFirstStartTime;
                    }
				}
				if(row["dtChuQinTime"]!=null && row["dtChuQinTime"].ToString()!="")
				{
					model.dtChuQinTime=DateTime.Parse(row["dtChuQinTime"].ToString());
				}
				if(row["nNeedRest"]!=null && row["nNeedRest"].ToString()!="")
				{
					model.nNeedRest=int.Parse(row["nNeedRest"].ToString());
				}
				if(row["dtArriveTime"]!=null && row["dtArriveTime"].ToString()!="")
				{
					model.dtArriveTime=DateTime.Parse(row["dtArriveTime"].ToString());
				}
				if(row["dtCallTime"]!=null && row["dtCallTime"].ToString()!="")
				{
					model.dtCallTime=DateTime.Parse(row["dtCallTime"].ToString());
				}
				if(row["strBak1"]!=null)
				{
					model.strBak1=row["strBak1"].ToString();
				}
				if(row["dtLastArriveTime"]!=null && row["dtLastArriveTime"].ToString()!="")
				{
					//model.dtLastArriveTime=DateTime.Parse(row["dtLastArriveTime"].ToString());
                    DateTime dtLastArriveTime;
                    if (DateTime.TryParse(row["dtLastArriveTime"].ToString(), out dtLastArriveTime))
                    {
                        model.dtLastArriveTime = dtLastArriveTime;
                    }
				}
				if(row["strMainPlanGUID"]!=null)
				{
					model.strMainPlanGUID=row["strMainPlanGUID"].ToString();
				}
				if(row["strPlaceID"]!=null)
				{
					model.strPlaceID=row["strPlaceID"].ToString();
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
			strSql.Append("select nid,strTrainPlanGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,nPlanState,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,dtFirstStartTime,dtChuQinTime,nNeedRest,dtArriveTime,dtCallTime,strBak1,dtLastArriveTime,strMainPlanGUID,strPlaceID ");
			strSql.Append(" FROM TAB_Plan_Train ");
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
			strSql.Append(" nid,strTrainPlanGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,nPlanState,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,dtFirstStartTime,dtChuQinTime,nNeedRest,dtArriveTime,dtCallTime,strBak1,dtLastArriveTime,strMainPlanGUID,strPlaceID ");
			strSql.Append(" FROM TAB_Plan_Train ");
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
			strSql.Append("select count(1) FROM TAB_Plan_Train ");
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
				strSql.Append("order by T.nid desc");
			}
			strSql.Append(")AS Row, T.*  from TAB_Plan_Train T ");
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
			parameters[0].Value = "TAB_Plan_Train";
			parameters[1].Value = "nid";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        public TF.RunSafty.Model.TrainManPlanforLed DataRowToModelFoLed(DataRow row)
        {
            TF.RunSafty.Model.TrainManPlanforLed model = new TF.RunSafty.Model.TrainManPlanforLed();
            if (row != null)
            {
                if (row["strTrainTypeName"] != null && row["strTrainTypeName"].ToString() != "")
                {
                    model.StrTrainTypeName = row["strTrainTypeName"].ToString();
                }
                else
                {
                    model.StrTrainTypeName = "";
                }


                if (row["strTrainNumber"] != null && row["strTrainNumber"].ToString() != "")
                {
                    model.Strtrainnumber = row["strTrainNumber"].ToString();
                }
                else
                {
                    model.Strtrainnumber = "";
                }


                if (row["strTrainNo"] != null && row["strTrainNo"].ToString() != "")
                {
                    model.Strtrainno = row["strTrainNo"].ToString();
                }
                else
                {
                    model.Strtrainno = "";
                }

                if (row["dtStartTime"] != null && row["dtStartTime"].ToString() != "")
                {
                    model.Dtstarttaintime = row["dtStartTime"].ToString();
                }
                else
                {
                    model.Dtstarttaintime = "";
                }

                if (row["dtChuQinTime"] != null && row["dtChuQinTime"].ToString() != "")
                {
                    model.Dtchuqintime = row["dtChuQinTime"].ToString();
                }
                else
                {
                    model.Dtchuqintime = "";
                }

                if (row["strTrainmanName1"] != null && row["strTrainmanName1"].ToString() != "")
                {
                    model.Strtrainmanname1 = row["strTrainmanName1"].ToString();
                }
                else
                {
                    model.Strtrainmanname1 = "";
                }

                if (row["strTrainmanName2"] != null && row["strTrainmanName2"].ToString() != "")
                {
                    model.Strtrainmanname2 = row["strTrainmanName2"].ToString();
                }
                else
                {
                    model.Strtrainmanname2 = "";
                }

                if (row["strStartStationName"] != null && row["strStartStationName"].ToString() != "")
                {
                    model.Strstartstation = row["strStartStationName"].ToString();
                }
                else
                {
                    model.Strstartstation = "";
                }

                if (row["strWorkShopName"] != null && row["strWorkShopName"].ToString() != "")
                {
                    model.Workshopname = row["strWorkShopName"].ToString();
                }
                else
                {
                    model.Workshopname = "";

                }
            }
            return model;
        }



        public DataSet GetAllList(string strClientGUID, string strMinute)
        {
            DateTime dt = DateTime.Now;
            string strThisTime = "";
            string strAddTime = "";
            if (string.IsNullOrEmpty(strMinute))
            {
                strThisTime = "120";
            }
            else
            {
                strThisTime = strMinute;
            }

            strAddTime = dt.AddMinutes(-Convert.ToInt32(strThisTime)).ToString();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM VIEW_Plan_Train_Led where 1=1 ");
            strSql.Append(" and dtStartTime>'" + strAddTime + "'");
            strSql.Append(" and nPlanState>1 and nPlanState<=7");
            if (!string.IsNullOrEmpty(strClientGUID))
            {
                strSql.Append(" and strKeHuDuanGUID='" + strClientGUID + "'");
            }

            strSql.Append(" order by dtStartTime");

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }


		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

