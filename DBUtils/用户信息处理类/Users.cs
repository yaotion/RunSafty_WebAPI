using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.DBUtils;
using System.Data;
using System.Data.SqlClient;

namespace TF.RunSafty.DBUtils
{
    public class Users : DBOperator
    {
        public Users(string ConnectionString)
            : base(ConnectionString)
        { }

        #region =============================登录==============================
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public DataTable login(string userNumber, string Password)
        {
            string sql = " where";
            sql += " userNumber=@userNumber and passWord=@passWord";
            string strSql = "select * from Tab_User " + sql;
            SqlParameter[] sqlParams = {
                                           new SqlParameter("userNumber",userNumber),
                                           new SqlParameter("passWord",Password)
                                       };
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql, sqlParams).Tables[0];
        }

        #endregion

        #region ========================================查询出所有数据========================================
        /// <summary>
        ///查询出所有数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable QueryCheckRoles(int pageIndex, int pageCount, string userName)
        {
            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" and 1=1");
            if (!string.IsNullOrEmpty(userName))
                strSqlWhere.Append(" and userName like '%" + userName + "%'");
            string strSql = @"select top " + pageCount.ToString() + " * from VIEW_Users where Id not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" Id from VIEW_Users where 1=1" + strSqlWhere.ToString() + " order by id desc)" + strSqlWhere.ToString() + " order by id desc";
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql).Tables[0];
        }
        #endregion

        #region ========================================查询个数========================================
        /// <summary>
        /// 查询个数
        /// </summary>
        /// <returns></returns>
        public int QueryCheckRolesCount(string userName)
        {
            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" 1=1");
            if (string.IsNullOrEmpty(userName))
                strSqlWhere.Append(" and userName like '%" + userName + "%'");

            string strSql = "select count(*) from VIEW_Users where" + strSqlWhere.ToString() + "";
            return Convert.ToInt32(SqlHelps.ExecuteScalar(ConnectionString, CommandType.Text, strSql));
        }
        #endregion

        #region ========================================删除用户========================================
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string strSql = "delete Tab_User where Id= @id";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("id",id)
                                          };
            return SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql, sqlParameter);
        }
        #endregion

        #region ========================================添加用户========================================
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int Add(TF.RunSafty.Entry.Users model)
        {
            string strSql = @"insert into Tab_User (userNumber,userName,passWord,roseId) values 
                            (@userNumber,@userName,@passWord,@roseId)";
            SqlParameter[] sqlParameter = {
                                                new SqlParameter("@userNumber", model.userNumber),
					                            new SqlParameter("@userName", model.userName),
					                            new SqlParameter("@passWord", model.passWord),
					                            new SqlParameter("@roseId", model.roseId)
                                         };
            object obj = SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql, sqlParameter);
            if (DBNull.Value.Equals(obj))
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }


        #endregion

        #region ========================================修改用户信息========================================
        /// <summary>
        /// 修改用户名称
        /// </summary>
        /// <returns></returns>
        public bool Update(TF.RunSafty.Entry.Users model)
        {
            string strSql = @"update Tab_User set userNumber=@userNumber,userName=@userName,roseId=@roseId where Id=@id";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("id",model.id),
                                                new SqlParameter("@userNumber", model.userNumber),
					                            new SqlParameter("@userName", model.userName),
					                            new SqlParameter("@roseId", model.roseId)
                                          };
            return SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql, sqlParameter) > 0;

        }
        #endregion

        #region ========================================判断是否存在========================================
        /// <summary>
        /// 查询个数
        /// </summary>
        /// <returns></returns>
        public int IsExistCheckRoles(string userNumber)
        {
            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" 1=1");
            if (!string.IsNullOrEmpty(userNumber))
                strSqlWhere.Append(" and userNumber = '" + userNumber + "'");

            string strSql = "select count(*) from Tab_User where" + strSqlWhere.ToString() + "";
            return Convert.ToInt32(SqlHelps.ExecuteScalar(ConnectionString, CommandType.Text, strSql));
        }
        #endregion

        #region ========================================获取对象实体========================================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Entry.Users GetModel(int Id)
        {
            string strSql = "select * from Tab_User where Id='" + Id + "'";
            DataSet ds = SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql);

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
        public TF.RunSafty.Entry.Users DataRowToModel(DataRow row)
        {
            TF.RunSafty.Entry.Users model = new TF.RunSafty.Entry.Users();
            if (row != null)
            {

                model.id = int.Parse(row["id"].ToString());

                model.userNumber = row["userNumber"].ToString();

                model.userName = row["userName"].ToString();

                model.passWord = row["passWord"].ToString();

                model.roseId = int.Parse(row["roseId"].ToString());
            }
            return model;
        }

        #endregion

        #region ========================================查询出所有用户名称（通过用户真实姓名虚拟查询）========================================
        /// <summary>
        ///查询出所有数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable getAllName(string name)
        {

            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" 1=1");

            if (!string.IsNullOrEmpty(name))
                strSqlWhere.Append(" and userName like '%" + name + "%'");

            string strSql = "select top 10 * from Tab_User where " + strSqlWhere.ToString();
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql).Tables[0];
        }
        #endregion

        #region ========================================查询出所有用户名称（通过用户名虚拟查询）========================================
        /// <summary>
        ///查询出所有数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable getAllNameByuserNumber(string Number)
        {

            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" 1=1");

            if (!string.IsNullOrEmpty(Number))
                strSqlWhere.Append(" and userNumber = '" + Number + "'");

            string strSql = "select top 1 * from Tab_User where " + strSqlWhere.ToString();
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql).Tables[0];
        }
        #endregion

        #region ========================================修改用户信息========================================
        /// <summary>
        /// 修改用户名称
        /// </summary>
        /// <returns></returns>
        public bool UpdatePassWord(TF.RunSafty.Entry.Users model)
        {
            string strSql = @"update Tab_User set passWord=@passWord where Id=@id";
            SqlParameter[] sqlParameter = {
                                                new SqlParameter("id",model.id),
					                            new SqlParameter("@passWord", model.passWord)
                                          };
            return SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql, sqlParameter) > 0;

        }
        #endregion

    }
}
