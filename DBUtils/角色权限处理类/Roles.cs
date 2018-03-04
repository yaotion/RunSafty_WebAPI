using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.RunSafty.DBUtils;
using System.Data.SqlClient;

namespace TF.RunSafty.DBUtils
{
    public class Roles : DBOperator
    {
        public Roles(string ConnectionString)
            : base(ConnectionString)
        { }



        #region ========================================查询出所有数据========================================
        /// <summary>
        ///查询出所有数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable QueryCheckRoles(int pageIndex, int pageCount, string RolesName)
        {
            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" and 1=1");
            if (!string.IsNullOrEmpty(RolesName))
                strSqlWhere.Append(" and RolesName like '%" + RolesName + "%'");
            string strSql = @"select top " + pageCount.ToString() + " * from tab_Roles where Id not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" Id from tab_Roles where 1=1" + strSqlWhere.ToString() + " order by id desc)" + strSqlWhere.ToString() + " order by id desc";
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql).Tables[0];
        }
        #endregion

        #region ========================================查询个数========================================
        /// <summary>
        /// 查询个数
        /// </summary>
        /// <returns></returns>
        public int QueryCheckRolesCount(string RolesName)
        {
            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" 1=1");
            if (string.IsNullOrEmpty(RolesName))
                strSqlWhere.Append(" and RolesName like '%" + RolesName + "%'");

            string strSql = "select count(*) from tab_Roles where" + strSqlWhere.ToString() + "";
            return Convert.ToInt32(SqlHelps.ExecuteScalar(ConnectionString, CommandType.Text, strSql));
        }
        #endregion

        #region ========================================删除角色========================================
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string strSql = "delete tab_Roles where Id= @id";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("id",id)
                                          };
            return SqlHelps.ExecuteNonQuery(SqlHelps.SQLConnString, CommandType.Text, strSql, sqlParameter);
        }
        #endregion

        #region ========================================添加角色========================================
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int Add(TF.RunSafty.Entry.Roles model)
        {
            string strSql = @"insert into tab_Roles (RolesName) values 
                            (@RolesName)";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("RolesName",model.RolesName)
                                          };
            object obj = SqlHelps.ExecuteNonQuery(SqlHelps.SQLConnString, CommandType.Text, strSql, sqlParameter);
            if (DBNull.Value.Equals(obj))
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }


        #endregion

        #region ========================================修改角色名称========================================
        /// <summary>
        /// 修改角色名称
        /// </summary>
        /// <returns></returns>
        public bool Update(TF.RunSafty.Entry.Roles model)
        {
            string strSql = @"update tab_Roles set RolesName=@RolesName where Id=@id";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("id",model.Id),
                                               new SqlParameter("RolesName",model.RolesName)
                                          };
            return SqlHelps.ExecuteNonQuery(SqlHelps.SQLConnString, CommandType.Text, strSql, sqlParameter) > 0;

        }
        #endregion

        #region ========================================修改权限========================================
        /// <summary>
        /// 修改角色名称
        /// </summary>
        /// <returns></returns>
        public bool UpdatePowers(TF.RunSafty.Entry.Roles model)
        {
            string strSql = @"update tab_Roles set RolesPowers=@RolesPowers where Id=@id";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("id",model.Id),
                                               new SqlParameter("RolesPowers",model.RolesPowers)
                                          };
            return SqlHelps.ExecuteNonQuery(SqlHelps.SQLConnString, CommandType.Text, strSql, sqlParameter) > 0;

        }
        #endregion

        #region ========================================判断是否存在========================================
        /// <summary>
        /// 查询个数
        /// </summary>
        /// <returns></returns>
        public int IsExistCheckRoles(string RolesName)
        {
            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" 1=1");
            if (!string.IsNullOrEmpty(RolesName))
                strSqlWhere.Append(" and RolesName = '" + RolesName + "'");

            string strSql = "select count(*) from tab_Roles where" + strSqlWhere.ToString() + "";
            return Convert.ToInt32(SqlHelps.ExecuteScalar(SqlHelps.SQLConnString, CommandType.Text, strSql));
        }
        #endregion

        #region ========================================获取对象实体========================================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Entry.Roles GetModel(int Id)
        {
            string strSql = "select * from tab_Roles where Id='" + Id + "'";
            DataSet ds = SqlHelps.ReturnDataSet(SqlHelps.SQLConnString, CommandType.Text, strSql);

            TF.RunSafty.Entry.Roles rl = new TF.RunSafty.Entry.Roles();

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
        public TF.RunSafty.Entry.Roles DataRowToModel(DataRow row)
        {
            TF.RunSafty.Entry.Roles model = new TF.RunSafty.Entry.Roles();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                    model.RolesName = row["RolesName"].ToString();
                    model.RolesPowers = row["RolesPowers"].ToString();
              
            }
            return model;
        }

        #endregion

        #region ========================================查询出所有角色名称========================================
        /// <summary>
        ///查询出所有数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable getAllRolesName()
        {
            string strSql = @"select id,RolesName from tab_Roles order by id desc";
            return SqlHelps.ReturnDataSet(SqlHelps.SQLConnString, CommandType.Text, strSql).Tables[0];
        }
        #endregion


        #region ========================================查看该角色下是否有成员========================================
        /// <summary>
        /// 查询个数
        /// </summary>
        /// <returns></returns>
        public int QueryCheckUsersCount(int id)
        {
            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" 1=1");
            if (id != null && id != 0)
                strSqlWhere.Append(" and roseId = " + id);

            string strSql = "select count(*) from Tab_User where" + strSqlWhere.ToString();
            return Convert.ToInt32(SqlHelps.ExecuteScalar(ConnectionString, CommandType.Text, strSql));
        }
        #endregion

    }
}
