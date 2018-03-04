/**  版本信息模板在安装目录下，可自行修改。
* VIEW_Base_TrainNo.cs
*
* 功 能： N/A
* 类 名： VIEW_Base_TrainNo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/17 14:08:53   N/A    初版
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
	/// 数据访问类:VIEW_Base_TrainNo
	/// </summary>
	public partial class VIEW_Base_TrainNo
	{
		public VIEW_Base_TrainNo()
		{}
		#region  BasicMethod

         

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TF.RunSafty.Model.VIEW_Base_TrainNo DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.VIEW_Base_TrainNo model=new TF.RunSafty.Model.VIEW_Base_TrainNo(); 
            if (row != null)
            {
                if (row["strTrainJiaoluName"] != null)
                {
                    model.strTrainJiaoluName = row["strTrainJiaoluName"].ToString();
                }
                if (row["strTrainmanTypeName"] != null)
                {
                    model.strTrainmanTypeName = row["strTrainmanTypeName"].ToString();
                }
                if (row["strGUID"] != null)
                {
                    model.strGUID = row["strGUID"].ToString();
                }
                if (row["strTrainJiaoluGUID"] != null)
                {
                    model.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
                }
                if (row["strTrainNo"] != null)
                {
                    model.strTrainNo = row["strTrainNo"].ToString();
                }
                if (row["strTrainNumber"] != null)
                {
                    model.strTrainNumber = row["strTrainNumber"].ToString();
                }
                if (row["dtStartTime"] != null && row["dtStartTime"].ToString() != "")
                {
                    DateTime dtStartTime;
                    if (DateTime.TryParse(row["dtStartTime"].ToString(), out dtStartTime))
                    {
                        model.dtStartTime = dtStartTime;
                    }
                }
                if (row["strStartStation"] != null)
                {
                    model.strStartStation = row["strStartStation"].ToString();
                }
                if (row["strEndStation"] != null)
                {
                    model.strEndStation = row["strEndStation"].ToString();
                }
                if (row["dtCreateTime"] != null && row["dtCreateTime"].ToString() != "")
                {
                    DateTime dtCreateTime;
                    if (DateTime.TryParse(row["dtCreateTime"].ToString(), out dtCreateTime))
                    {
                        model.dtCreateTime = dtCreateTime;
                    }
                }
                if (row["strTrainTypeName"] != null)
                {
                    model.strTrainTypeName = row["strTrainTypeName"].ToString();
                }
                if (row["nTrainmanTypeID"] != null && row["nTrainmanTypeID"].ToString() != "")
                {
                    model.nTrainmanTypeID = int.Parse(row["nTrainmanTypeID"].ToString());
                }
                if (row["strStartStationName"] != null)
                {
                    model.strStartStationName = row["strStartStationName"].ToString();
                }
                if (row["nKehuoID"] != null && row["nKehuoID"].ToString() != "")
                {
                    model.nKehuoID = int.Parse(row["nKehuoID"].ToString());
                }
                if (row["dtRealStartTime"] != null && row["dtRealStartTime"].ToString() != "")
                {
                   // model.dtRealStartTime = DateTime.Parse(row["dtRealStartTime"].ToString());
                    DateTime dtRealStartTime;
                    if (DateTime.TryParse(row["dtRealStartTime"].ToString(), out dtRealStartTime))
                    {
                        model.dtRealStartTime = dtRealStartTime;
                    }
                }
                if (row["nPlanType"] != null && row["nPlanType"].ToString() != "")
                {
                    model.nPlanType = int.Parse(row["nPlanType"].ToString());
                }
                if (row["nDragType"] != null && row["nDragType"].ToString() != "")
                {
                    model.nDragType = int.Parse(row["nDragType"].ToString());
                }
                if (row["nRemarkType"] != null && row["nRemarkType"].ToString() != "")
                {
                    model.nRemarkType = int.Parse(row["nRemarkType"].ToString());
                }
                if (row["strRemark"] != null)
                {
                    model.strRemark = row["strRemark"].ToString();
                }
                if (row["strCreateSiteGUID"] != null)
                {
                    model.strCreateSiteGUID = row["strCreateSiteGUID"].ToString();
                }
                if (row["strCreateUserGUID"] != null)
                {
                    model.strCreateUserGUID = row["strCreateUserGUID"].ToString();
                }
                if (row["strEndStationName"] != null)
                {
                    model.strEndStationName = row["strEndStationName"].ToString();
                }
                if (row["nDragTypeName"] != null)
                {
                    model.nDragTypeName = row["nDragTypeName"].ToString();
                }
                if (row["strPlaceID"] != null)
                {
                    model.strPlaceID = row["strPlaceID"].ToString();
                }
                if (row["strPlaceName"] != null)
                {
                    model.strPlaceName = row["strPlaceName"].ToString();
                }
                if (row["strKehuoName"] != null)
                {
                    model.strKehuoName = row["strKehuoName"].ToString();
                }
                if (row["strPlanTypeName"] != null)
                {
                    model.strPlanTypeName = row["strPlanTypeName"].ToString();
                }
                if (row["strRemarkTypeName"] != null)
                {
                    model.strRemarkTypeName = row["strRemarkTypeName"].ToString();
                }
                if (row["dtPlanStartTime"] != null && row["dtPlanStartTime"].ToString() != "")
                {
                    model.dtPlanStartTime = DateTime.Parse(row["dtPlanStartTime"].ToString());
                }
                else
                {
                    model.dtPlanStartTime = model.dtStartTime;
                }
                if (row["dtCallTime"] != null && row["dtCallTime"].ToString() != "")
                {
                    model.dtCallTime = DateTime.Parse(row["dtCallTime"].ToString());
                }
                if (row["dtArriveTime"] != null && row["dtArriveTime"].ToString() != "")
                {
                    model.dtArriveTime = DateTime.Parse(row["dtArriveTime"].ToString());
                }
                if (row["nNeedRest"] != null && row["nNeedRest"].ToString() != "")
                {
                    model.nNeedRest = int.Parse(row["nNeedRest"].ToString());
                }
                if (row["strWorkDay"] != null)
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
            strSql.Append("select strTrainJiaoluName,strTrainmanTypeName,strGUID,strTrainJiaoluGUID,strTrainNo,strTrainNumber,dtStartTime,strStartStation,strEndStation,dtCreateTime,strTrainTypeName,nTrainmanTypeID,strStartStationName,nKehuoID,dtRealStartTime,nPlanType,nDragType,nRemarkType,strRemark,strCreateSiteGUID,strCreateUserGUID,strEndStationName,nDragTypeName,strPlaceID,strPlaceName,strKehuoName,strPlanTypeName,strRemarkTypeName,dtPlanStartTime,dtArriveTime,dtCallTime,strWorkDay,nNeedRest ");
            strSql.Append(" FROM VIEW_Base_TrainNo ");
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
            strSql.Append(" strTrainJiaoluName,strTrainmanTypeName,strGUID,strTrainJiaoluGUID,strTrainNo,strTrainNumber,dtStartTime,strStartStation,strEndStation,dtCreateTime,strTrainTypeName,nTrainmanTypeID,strStartStationName,nKehuoID,dtRealStartTime,nPlanType,nDragType,nRemarkType,strRemark,strCreateSiteGUID,strCreateUserGUID,strEndStationName,nDragTypeName,strPlaceID,strPlaceName,strKehuoName,strPlanTypeName,strRemarkTypeName,dtPlanStartTime,dtArriveTime,dtCallTime,strWorkDay,nNeedRest ");
            strSql.Append(" FROM VIEW_Base_TrainNo ");
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
			strSql.Append("select count(1) FROM VIEW_Base_TrainNo ");
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
			strSql.Append(")AS Row, T.*  from VIEW_Base_TrainNo T ");
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
			parameters[0].Value = "VIEW_Base_TrainNo";
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

        public DataSet GetTrainnosOfTrainJiaolu(string strTrainJiaolu)
        {
            string strWhere = string.Format(" strTrainJiaoluGUID='{0}' ", strTrainJiaolu);
            return GetList(strWhere);
        }

		#endregion  ExtensionMethod
	}
}

