using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TF.RunSafty.DBUtils
{
    public class Nav : DBOperator
    {
        public Nav(string ConnectionString)
            : base(ConnectionString)
        { }


        #region =========================查看该类别下是否存在子类别==================================
        /// <summary>
        /// 看是否已经存在该typeid的类别
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public bool IfExists(string typeID)
        {
            string strSql = "select TypeName from Tab_Nav where TypeParentID=@typeID";
            SqlParameter param = new SqlParameter("@typeID", typeID);
            object obj = SqlHelps.ExecuteScalar(ConnectionString, CommandType.Text, strSql, param);
            if (obj != null && obj.ToString() != "")
                return true;
            return false;
        } 
        #endregion

        #region ==================================获取那个最大的typeid==================================
        /// <summary>
        /// 获取那个最大的typeid
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public DataSet GetMaxId(string typeID)
        {
            string strSql = "select max(typeID) as maxId from Tab_Nav where TypeParentID='" + typeID + "'";
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql.ToString());
        } 
        #endregion

        #region ==========================================增加一条数据==========================================
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TF.RunSafty.Entry.nav model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_Nav(");
            strSql.Append("TypeID,TypeParentID,typeName,NavUrl,StrSort,TrNavUrl)");
            strSql.Append(" values (");
            strSql.Append("@TypeID,@TypeParentID,@typeName,@NavUrl,@StrSort,@TrNavUrl)");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.VarChar,50),
					new SqlParameter("@TypeParentID", SqlDbType.VarChar,50),
					new SqlParameter("@typeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NavUrl", SqlDbType.NVarChar,100),
                    new SqlParameter("@StrSort", SqlDbType.Int,4),
                    new SqlParameter("@TrNavUrl", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.TypeID;
            parameters[1].Value = model.TypeParentID;
            parameters[2].Value = model.typeName;
            parameters[3].Value = model.NavUrl;
            parameters[4].Value = model.StrSort;
            parameters[5].Value = model.TrNavUrl;


            int rows = SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion

        #region ==========================================更新一条数据==========================================
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TF.RunSafty.Entry.nav model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tab_Nav set ");
            strSql.Append("typeName=@typeName,");
            strSql.Append("NavUrl=@NavUrl,");
            strSql.Append("StrSort=@StrSort,");
            strSql.Append("TrNavUrl=@TrNavUrl");
            strSql.Append(" where TypeID=@TypeID");
            SqlParameter[] parameters = {
					new SqlParameter("@typeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@NavUrl", SqlDbType.NVarChar,100),
                    new SqlParameter("@TypeID", SqlDbType.VarChar,50),
                    new SqlParameter("@StrSort", SqlDbType.Int,4),
                    new SqlParameter("@TrNavUrl", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.typeName;
            parameters[1].Value = model.NavUrl;
            parameters[2].Value = model.TypeID;
            parameters[3].Value = model.StrSort;
            parameters[4].Value = model.TrNavUrl;
            int rows = SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion

        #region ========================================== 删除一条数据==========================================
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string typeID)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tab_Nav ");
            strSql.Append(" where TypeID=@typeID");
            SqlParameter param = new SqlParameter("@typeID", typeID);

            int rows = SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), param);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion

        #region ==========================================得到一个对象实体==========================================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Entry.nav GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 TypeID,TypeParentID,typeName from Tab_Nav ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
			};

            TF.RunSafty.Entry.nav model = new TF.RunSafty.Entry.nav();
            DataSet ds = SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql.ToString(), parameters);
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
        public TF.RunSafty.Entry.nav DataRowToModel(DataRow row)
        {
            TF.RunSafty.Entry.nav model = new TF.RunSafty.Entry.nav();
            if (row != null)
            {
                if (row["TypeID"] != null)
                {
                    model.TypeID = row["TypeID"].ToString();
                }
                if (row["TypeParentID"] != null)
                {
                    model.TypeParentID = row["TypeParentID"].ToString();
                }
                if (row["typeName"] != null)
                {
                    model.typeName = row["typeName"].ToString();
                }
            }
            return model;
        } 
        #endregion

        #region ==========================================获得数据列表==========================================
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM Tab_Nav ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by StrSort asc");
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql.ToString()).Tables[0];
        } 
        #endregion

        #region ==========================================获取导航内容==========================================
        /// <summary>
        /// 获取导航内容
        /// </summary>
        public DataTable GetListForNav(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM Tab_Nav ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by StrSort asc");
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql.ToString()).Tables[0];
        } 
        #endregion


	}
}
