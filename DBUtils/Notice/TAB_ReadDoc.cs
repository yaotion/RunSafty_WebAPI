/**  版本信息模板在安装目录下，可自行修改。
* TAB_ReadDoc.cs
*
* 功 能： N/A
* 类 名： TAB_ReadDoc
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/15 13:13:35   N/A    初版
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
	/// 数据访问类:TAB_ReadDoc
	/// </summary>
	public partial class TAB_ReadDoc
	{
		public TAB_ReadDoc()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(nid) FROM TAB_ReadDoc ");

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
		public bool Exists(int nid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TAB_ReadDoc");
			strSql.Append(" where nid=@nid");
			SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;

			return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TF.RunSafty.Model.TAB_ReadDoc model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_ReadDoc(");
			strSql.Append("strFileGUID,strWorkShopGUID,StrTypeGUID,strFileName,strFilePath,dtBeginTime,dtEndTime,dtUpTime,nReadMode,nReadInterval)");
			strSql.Append(" values (");
			strSql.Append("@strFileGUID,@strWorkShopGUID,@StrTypeGUID,@strFileName,@strFilePath,@dtBeginTime,@dtEndTime,@dtUpTime,@nReadMode,@nReadInterval)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@strFileGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strWorkShopGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrTypeGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strFileName", SqlDbType.VarChar,50),
					new SqlParameter("@strFilePath", SqlDbType.VarChar,100),
					new SqlParameter("@dtBeginTime", SqlDbType.VarChar,50),
					new SqlParameter("@dtEndTime", SqlDbType.VarChar,50),
					new SqlParameter("@dtUpTime", SqlDbType.DateTime),
					new SqlParameter("@nReadMode", SqlDbType.Int,4),
					new SqlParameter("@nReadInterval", SqlDbType.Int,4)};
			parameters[0].Value = model.strFileGUID;
			parameters[1].Value = model.strWorkShopGUID;
			parameters[2].Value = model.StrTypeGUID;
			parameters[3].Value = model.strFileName;
			parameters[4].Value = model.strFilePath;
			parameters[5].Value = model.dtBeginTime;
			parameters[6].Value = model.dtEndTime;
			parameters[7].Value = model.dtUpTime;
			parameters[8].Value = model.nReadMode;
			parameters[9].Value = model.nReadInterval;

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
		public bool Update(TF.RunSafty.Model.TAB_ReadDoc model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TAB_ReadDoc set ");
			strSql.Append("strFileGUID=@strFileGUID,");
			strSql.Append("strWorkShopGUID=@strWorkShopGUID,");
			strSql.Append("StrTypeGUID=@StrTypeGUID,");
			strSql.Append("strFileName=@strFileName,");
			strSql.Append("strFilePath=@strFilePath,");
			strSql.Append("dtBeginTime=@dtBeginTime,");
			strSql.Append("dtEndTime=@dtEndTime,");
			strSql.Append("dtUpTime=@dtUpTime,");
			strSql.Append("nReadMode=@nReadMode,");
			strSql.Append("nReadInterval=@nReadInterval");
			strSql.Append(" where nid=@nid");
			SqlParameter[] parameters = {
					new SqlParameter("@strFileGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strWorkShopGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrTypeGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strFileName", SqlDbType.VarChar,50),
					new SqlParameter("@strFilePath", SqlDbType.VarChar,100),
					new SqlParameter("@dtBeginTime", SqlDbType.VarChar,50),
					new SqlParameter("@dtEndTime", SqlDbType.VarChar,50),
					new SqlParameter("@dtUpTime", SqlDbType.DateTime),
					new SqlParameter("@nReadMode", SqlDbType.Int,4),
					new SqlParameter("@nReadInterval", SqlDbType.Int,4),
					new SqlParameter("@nid", SqlDbType.Int,4)};
			parameters[0].Value = model.strFileGUID;
			parameters[1].Value = model.strWorkShopGUID;
			parameters[2].Value = model.StrTypeGUID;
			parameters[3].Value = model.strFileName;
			parameters[4].Value = model.strFilePath;
			parameters[5].Value = model.dtBeginTime;
			parameters[6].Value = model.dtEndTime;
			parameters[7].Value = model.dtUpTime;
			parameters[8].Value = model.nReadMode;
			parameters[9].Value = model.nReadInterval;
			parameters[10].Value = model.nid;

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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int nid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_ReadDoc ");
			strSql.Append(" where nid=@nid");
			SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string nidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_ReadDoc ");
            strSql.Append(" where nid in (" + nidlist + ")  ");
			int rows=(int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
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
		public TF.RunSafty.Model.TAB_ReadDoc GetModel(int nid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 nid,strFileGUID,strWorkShopGUID,StrTypeGUID,strFileName,strFilePath,dtBeginTime,dtEndTime,dtUpTime,nReadMode,nReadInterval from TAB_ReadDoc ");
			strSql.Append(" where nid=@nid");
			SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;

			TF.RunSafty.Model.TAB_ReadDoc model=new TF.RunSafty.Model.TAB_ReadDoc();
			DataSet ds=SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
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
		public TF.RunSafty.Model.TAB_ReadDoc DataRowToModel(DataRow row)
		{
			TF.RunSafty.Model.TAB_ReadDoc model=new TF.RunSafty.Model.TAB_ReadDoc();
			if (row != null)
			{
				if(row["nid"]!=null && row["nid"].ToString()!="")
				{
					model.nid=int.Parse(row["nid"].ToString());
				}
				if(row["strFileGUID"]!=null)
				{
					model.strFileGUID=row["strFileGUID"].ToString();
				}
				if(row["strWorkShopGUID"]!=null)
				{
					model.strWorkShopGUID=row["strWorkShopGUID"].ToString();
				}
				if(row["StrTypeGUID"]!=null)
				{
					model.StrTypeGUID=row["StrTypeGUID"].ToString();
				}
				if(row["strFileName"]!=null)
				{
					model.strFileName=row["strFileName"].ToString();
				}
				if(row["strFilePath"]!=null)
				{
					model.strFilePath=row["strFilePath"].ToString();
				}
				if(row["dtBeginTime"]!=null)
				{
					model.dtBeginTime=row["dtBeginTime"].ToString();
				}
				if(row["dtEndTime"]!=null)
				{
					model.dtEndTime=row["dtEndTime"].ToString();
				}
				if(row["dtUpTime"]!=null && row["dtUpTime"].ToString()!="")
				{
					model.dtUpTime=DateTime.Parse(row["dtUpTime"].ToString());
				}
				if(row["nReadMode"]!=null && row["nReadMode"].ToString()!="")
				{
					model.nReadMode=int.Parse(row["nReadMode"].ToString());
				}
				if(row["nReadInterval"]!=null && row["nReadInterval"].ToString()!="")
				{
					model.nReadInterval=int.Parse(row["nReadInterval"].ToString());
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
			strSql.Append("select nid,strFileGUID,strWorkShopGUID,StrTypeGUID,strFileName,strFilePath,dtBeginTime,dtEndTime,dtUpTime,nReadMode,nReadInterval ");
			strSql.Append(" FROM TAB_ReadDoc ");
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
			strSql.Append(" nid,strFileGUID,strWorkShopGUID,StrTypeGUID,strFileName,strFilePath,dtBeginTime,dtEndTime,dtUpTime,nReadMode,nReadInterval ");
			strSql.Append(" FROM TAB_ReadDoc ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
		}

        /// <summary>
        /// 获取文件列表，
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllListWithFileType()
        {
            

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.strTypeGUID,strFileGUID,strFilePath strfilePathName ,strType,
strFileName,a.nReadInterval as nReadTimeCount
from TAB_ReadDoc a inner join TAB_FileGroup b on a.StrTypeGUID=b.strTypeGUID");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        public DataSet GetAllListWithFileType(string cid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.strTypeGUID,strFileGUID,strFilePath strfilePathName ,strType,strWorkShopGUID,
strFileName,a.nReadInterval as nReadTimeCount
from TAB_ReadDoc a inner join TAB_FileGroup b on a.StrTypeGUID=b.strTypeGUID where dtEndTime > GetDate()");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());

//            StringBuilder strSql = new StringBuilder();
//            strSql.AppendFormat(@"select a.strTypeGUID,strFileGUID,strFilePath strfilePathName ,strType,
//strFileName,a.nReadInterval as nReadTimeCount
//from TAB_ReadDoc a inner join TAB_FileGroup b on a.StrTypeGUID=b.strTypeGUID where a.strWorkShopGUID in (
//select strWorkShopGUID from TAB_Base_Site where strSiteGUID='{0}'
//)  and dtEndTime > GetDate()", cid);
//            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }
		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM TAB_ReadDoc ");
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
			strSql.Append(")AS Row, T.*  from TAB_ReadDoc T ");
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
			parameters[0].Value = "TAB_ReadDoc";
			parameters[1].Value = "nid";
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

