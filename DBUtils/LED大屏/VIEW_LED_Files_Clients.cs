/**  版本信息模板在安装目录下，可自行修改。
* VIEW_LED_Files_Clients.cs
*
* 功 能： N/A
* 类 名： VIEW_LED_Files_Clients
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-11-20 10:52:51   N/A    初版
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
    /// 数据访问类:VIEW_LED_Files_Clients
    /// </summary>
    public partial class VIEW_LED_Files_Clients
    {
        public VIEW_LED_Files_Clients()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TF.RunSafty.Model.VIEW_LED_Files_Clients model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into VIEW_LED_Files_Clients(");
            strSql.Append("nid,strFileGUID,strWorkShopGUID,DtUpdate,strFilePathName,strFileName,clientid)");
            strSql.Append(" values (");
            strSql.Append("@nid,@strFileGUID,@strWorkShopGUID,@DtUpdate,@strFilePathName,@strFileName,@clientid)");
            SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4),
					new SqlParameter("@strFileGUID", SqlDbType.NVarChar,100),
					new SqlParameter("@strWorkShopGUID", SqlDbType.NVarChar,100),
					new SqlParameter("@DtUpdate", SqlDbType.DateTime),
					new SqlParameter("@strFilePathName", SqlDbType.NVarChar,500),
					new SqlParameter("@strFileName", SqlDbType.NVarChar,200),
					new SqlParameter("@clientid", SqlDbType.VarChar,50)};
            parameters[0].Value = model.nid;
            parameters[1].Value = model.strFileGUID;
            parameters[2].Value = model.strWorkShopGUID;
            parameters[3].Value = model.DtUpdate;
            parameters[4].Value = model.strFilePathName;
            parameters[5].Value = model.strFileName;
            parameters[6].Value = model.clientid;

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
        public bool Update(TF.RunSafty.Model.VIEW_LED_Files_Clients model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VIEW_LED_Files_Clients set ");
            strSql.Append("nid=@nid,");
            strSql.Append("strFileGUID=@strFileGUID,");
            strSql.Append("strWorkShopGUID=@strWorkShopGUID,");
            strSql.Append("DtUpdate=@DtUpdate,");
            strSql.Append("strFilePathName=@strFilePathName,");
            strSql.Append("strFileName=@strFileName,");
            strSql.Append("clientid=@clientid");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4),
					new SqlParameter("@strFileGUID", SqlDbType.NVarChar,100),
					new SqlParameter("@strWorkShopGUID", SqlDbType.NVarChar,100),
					new SqlParameter("@DtUpdate", SqlDbType.DateTime),
					new SqlParameter("@strFilePathName", SqlDbType.NVarChar,500),
					new SqlParameter("@strFileName", SqlDbType.NVarChar,200),
					new SqlParameter("@clientid", SqlDbType.VarChar,50)};
            parameters[0].Value = model.nid;
            parameters[1].Value = model.strFileGUID;
            parameters[2].Value = model.strWorkShopGUID;
            parameters[3].Value = model.DtUpdate;
            parameters[4].Value = model.strFilePathName;
            parameters[5].Value = model.strFileName;
            parameters[6].Value = model.clientid;

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
        public bool Delete()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from VIEW_LED_Files_Clients ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
			};

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
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.VIEW_LED_Files_Clients GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 nid,strFileGUID,strWorkShopGUID,DtUpdate,strFilePathName,strFileName,clientid from VIEW_LED_Files_Clients ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
			};

            TF.RunSafty.Model.VIEW_LED_Files_Clients model = new TF.RunSafty.Model.VIEW_LED_Files_Clients();
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public TF.RunSafty.Model.VIEW_LED_Files_Clients DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.VIEW_LED_Files_Clients model = new TF.RunSafty.Model.VIEW_LED_Files_Clients();
            if (row != null)
            {
                if (row["nid"] != null && row["nid"].ToString() != "")
                {
                    model.nid = int.Parse(row["nid"].ToString());
                }
                if (row["strFileGUID"] != null)
                {
                    model.strFileGUID = row["strFileGUID"].ToString();
                }
                if (row["strWorkShopGUID"] != null)
                {
                    model.strWorkShopGUID = row["strWorkShopGUID"].ToString();
                }
                if (row["DtUpdate"] != null && row["DtUpdate"].ToString() != "")
                {
                    model.DtUpdate = DateTime.Parse(row["DtUpdate"].ToString());
                }
                if (row["strFilePathName"] != null)
                {
                    model.strFilePathName = row["strFilePathName"].ToString();
                }
                if (row["strFileName"] != null)
                {
                    model.strFileName = row["strFileName"].ToString();
                }
                if (row["clientid"] != null)
                {
                    model.clientid = row["clientid"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nid,strFileGUID,strWorkShopGUID,DtUpdate,strFilePathName,strFileName,clientid ");
            strSql.Append(" FROM VIEW_LED_Files_Clients ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" nid,strFileGUID,strWorkShopGUID,DtUpdate,strFilePathName,strFileName,clientid ");
            strSql.Append(" FROM VIEW_LED_Files_Clients ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM VIEW_LED_Files_Clients ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T. desc");
            }
            strSql.Append(")AS Row, T.*  from VIEW_LED_Files_Clients T ");
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
            parameters[0].Value = "VIEW_LED_Files_Clients";
            parameters[1].Value = "";
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

