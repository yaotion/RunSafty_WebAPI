/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_TrainNo.cs
*
* 功 能： N/A
* 类 名： TAB_Base_TrainNo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/18 9:17:47   N/A    初版
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
	/// 数据访问类:TAB_Base_TrainNo
	/// </summary>
	public partial class TAB_Base_TrainNo
	{
		public TAB_Base_TrainNo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string strGUID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TAB_Base_TrainNo");
			strSql.Append(" where strGUID=@strGUID ");
			SqlParameter[] parameters = {
					new SqlParameter("@strGUID", SqlDbType.VarChar,50)			};
			parameters[0].Value = strGUID;

			return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(TF.RunSafty.Model.TAB_Base_TrainNo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_Base_TrainNo(");
            strSql.Append("strGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,strPlaceID,dtPlanStartTime,nNeedRest,dtArriveTime,dtCallTime,strWorkDay)");
			strSql.Append(" values (");
            strSql.Append("@strGUID,@strTrainTypeName,@strTrainNumber,@strTrainNo,@dtStartTime,@dtRealStartTime,@strTrainJiaoluGUID,@strStartStation,@strEndStation,@nTrainmanTypeID,@nPlanType,@nDragType,@nKehuoID,@nRemarkType,@strRemark,@dtCreateTime,@strCreateSiteGUID,@strCreateUserGUID,@strPlaceID,@dtPlanStartTime,@nNeedRest,@dtArriveTime,@dtCallTime,@strWorkDay)");
			SqlParameter[] parameters = {
					new SqlParameter("@strGUID", SqlDbType.VarChar,50),
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
					new SqlParameter("@dtCreateTime", SqlDbType.DateTime),
					new SqlParameter("@strCreateSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strCreateUserGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strPlaceID", SqlDbType.VarChar,50),
                    new SqlParameter("@dtPlanStartTime",SqlDbType.DateTime)      ,
                    new SqlParameter("@nNeedRest",SqlDbType.Int,4), 
                    new SqlParameter("@dtArriveTime",SqlDbType.DateTime), 
                    new SqlParameter("@dtCallTime",SqlDbType.DateTime), 
                    new SqlParameter("@strWorkDay",SqlDbType.VarChar,50), 
                                        };
			parameters[0].Value = model.strGUID;
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
			parameters[15].Value = model.dtCreateTime;
			parameters[16].Value = model.strCreateSiteGUID;
			parameters[17].Value = model.strCreateUserGUID;
			parameters[18].Value = model.strPlaceID;
            parameters[19].Value = model.dtPlanStartTime;
		    parameters[20].Value = model.nNeedRest;
		    parameters[21].Value = model.dtArriveTime;
		    parameters[22].Value = model.dtCallTime;
		    parameters[23].Value = model.strWorkDay;
			int rows=(int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
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
		public bool Update(TF.RunSafty.Model.TAB_Base_TrainNo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TAB_Base_TrainNo set ");
			strSql.Append("strTrainTypeName=@strTrainTypeName,");
			strSql.Append("strTrainNumber=@strTrainNumber,");
			strSql.Append("strTrainNo=@strTrainNo,");
			strSql.Append("dtStartTime=@dtStartTime,");
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
			strSql.Append("strPlaceID=@strPlaceID,");
            strSql.Append("dtPlanStartTime=@dtPlanStartTime,");
            strSql.Append("nNeedRest=@nNeedRest,");
            strSql.Append("dtArriveTime=@dtArriveTime,");
            strSql.Append("dtCallTime=@dtCallTime,");
            strSql.Append("strWorkDay=@strWorkDay");
			strSql.Append(" where strGUID=@strGUID ");
			SqlParameter[] parameters = {
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
					new SqlParameter("@dtCreateTime", SqlDbType.DateTime),
					new SqlParameter("@strCreateSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strCreateUserGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strPlaceID", SqlDbType.VarChar,50),
					new SqlParameter("@strGUID", SqlDbType.VarChar,50),
                    new SqlParameter("@dtPlanStartTime",SqlDbType.DateTime),
                    new SqlParameter("@nNeedRest",SqlDbType.Int,4), 
                    new SqlParameter("@dtArriveTime",SqlDbType.DateTime), 
                    new SqlParameter("@dtCallTime",SqlDbType.DateTime), 
                    new SqlParameter("@strWorkDay",SqlDbType.VarChar,50), };
			parameters[0].Value = model.strTrainTypeName;
			parameters[1].Value = model.strTrainNumber;
			parameters[2].Value = model.strTrainNo;
			parameters[3].Value = model.dtStartTime;
			parameters[4].Value = model.dtRealStartTime;
			parameters[5].Value = model.strTrainJiaoluGUID;
			parameters[6].Value = model.strStartStation;
			parameters[7].Value = model.strEndStation;
			parameters[8].Value = model.nTrainmanTypeID;
			parameters[9].Value = model.nPlanType;
			parameters[10].Value = model.nDragType;
			parameters[11].Value = model.nKehuoID;
			parameters[12].Value = model.nRemarkType;
			parameters[13].Value = model.strRemark;
			parameters[14].Value = model.dtCreateTime;
			parameters[15].Value = model.strCreateSiteGUID;
			parameters[16].Value = model.strCreateUserGUID;
			parameters[17].Value = model.strPlaceID;
			parameters[18].Value = model.strGUID;
            parameters[19].Value=model.dtPlanStartTime;
            parameters[20].Value = model.nNeedRest;
            parameters[21].Value = model.dtArriveTime;
            parameters[22].Value = model.dtCallTime;
            parameters[23].Value = model.strWorkDay;
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
		public bool Delete(string strGUID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_Base_TrainNo ");
			strSql.Append(" where strGUID=@strGUID ");
			SqlParameter[] parameters = {
					new SqlParameter("@strGUID", SqlDbType.VarChar,50)			};
			parameters[0].Value = strGUID;

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
        /// 删除一个数据
        /// </summary>
        public bool DeleteByJiaoLu(string strJiaoLuGUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Base_TrainNo ");
            strSql.Append(" where strTrainJiaoluGUID=@strTrainJiaoluGUID ");
            SqlParameter[] parameters = {
					new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50)			};
            parameters[0].Value = strJiaoLuGUID;

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
		public bool DeleteList(string strGUIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_Base_TrainNo ");
			strSql.Append(" where strGUID in ("+strGUIDlist + ")  ");
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
		public TF.RunSafty.Model.TAB_Base_TrainNo GetModel(string strGUID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 strGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,strPlaceID,dtPlanStartTime,nNeedRest,dtArriveTime,dtCallTime,strWorkDay from TAB_Base_TrainNo ");
			strSql.Append(" where strGUID=@strGUID ");
			SqlParameter[] parameters = {
					new SqlParameter("@strGUID", SqlDbType.VarChar,50)			};
			parameters[0].Value = strGUID;

			TF.RunSafty.Model.TAB_Base_TrainNo model=new TF.RunSafty.Model.TAB_Base_TrainNo();
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(),parameters);
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
		public TF.RunSafty.Model.TAB_Base_TrainNo DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.TAB_Base_TrainNo model=new TF.RunSafty.Model.TAB_Base_TrainNo();
			if (row != null)
			{
				if(row["strGUID"]!=null)
				{
					model.strGUID=row["strGUID"].ToString();
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
                if (row["dtPlanStartTime"] != null && row["dtPlanStartTime"].ToString() != "")
                {
                    model.dtPlanStartTime = DateTime.Parse(row["dtPlanStartTime"].ToString());
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
				if(row["strPlaceID"]!=null)
				{
					model.strPlaceID=row["strPlaceID"].ToString();
				}

                model.nNeedRest = 0;                
			    if (row["nNeedRest"] != null)
			    {
                    int nTemp;
                    if (Int32.TryParse(row["nNeedRest"].ToString(), out nTemp))
                    {
                        model.nNeedRest = nTemp;
                    }
			    }
			    if (row["dtArriveTime"] != null && !string.IsNullOrEmpty(row["dtArriveTime"].ToString()))
			    {
			        model.dtArriveTime = DateTime.Parse(row["dtArriveTime"].ToString());
			    }
                if (row["dtCallTime"] != null && !string.IsNullOrEmpty(row["dtCallTime"].ToString()))
			    {
                    model.dtCallTime = DateTime.Parse(row["dtCallTime"].ToString());
			    }
                if (row["strWorkDay"] != null && !string.IsNullOrEmpty(row["strWorkDay"].ToString()))
			    {
			        model.strWorkDay = row["strWorkDay"].ToString();
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
            strSql.Append("select strGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,strPlaceID,dtPlanStartTime,nNeedRest,dtArriveTime,dtCallTime,strWorkDay ");
			strSql.Append(" FROM TAB_Base_TrainNo ");
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
            strSql.Append(" strGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,strPlaceID,dtPlanStartTime,nNeedRest,dtArriveTime,dtCallTime,strWorkDay ");
			strSql.Append(" FROM TAB_Base_TrainNo ");
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
			strSql.Append("select count(1) FROM TAB_Base_TrainNo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj =SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
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
				strSql.Append("order by T.strGUID desc");
			}
			strSql.Append(")AS Row, T.*  from TAB_Base_TrainNo T ");
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
			parameters[0].Value = "TAB_Base_TrainNo";
			parameters[1].Value = "strGUID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        public void GetTrainnoByTime(string strTrainjiaolu, DateTime dtBegin, DateTime dtEnd,int PlanState)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@dtBeginTIme", dtBegin),
					new SqlParameter("@dtEndTime", dtEnd),
					new SqlParameter("@strTrainjiaoluGUID", strTrainjiaolu), 
                    new SqlParameter("@strDutyUserGUID",DBNull.Value),
                    new SqlParameter("@strSiteGUID",DBNull.Value),
                    new SqlParameter("@PlanState",PlanState)
					};
            SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.StoredProcedure, "PROC_LoadTrainNo", parameters);
            
        }
		#endregion  ExtensionMethod
	}
}

