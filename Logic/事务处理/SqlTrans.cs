using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;

namespace ThinkFreely.RunSafty
{
    /// <summary>
    /// Sql事务
    /// </summary>
    public class SqlTrans
    {
        private SqlConnection conn;

        /// <summary>
        /// 事务
        /// </summary>
        public SqlTransaction trans { get; set; }

        /// <summary>
        /// 事务开始
        /// </summary>
        public void Begin()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    //如果有连接没关闭的话将其关闭
                    conn.Close();
                    conn = null;
                }
                conn = new SqlConnection(SqlHelper.ConnString); //创建连接
                conn.Open();
                trans = conn.BeginTransaction();  //开始事务
            }
            catch
            {
                if (conn != null)
                    conn.Close();
                throw;
            }
        }

        //事务提交
        public void Commit()
        {
            try
            {
                trans.Commit();
                trans.Dispose();
                conn.Close();
            }
            catch
            {
                if (conn != null)
                    conn.Close();
                if (trans != null)
                    trans.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollBack()
        {
            try
            {
                trans.Rollback();
                trans.Dispose();
                conn.Close();
            }
            catch
            {
                if (conn != null)
                    conn.Close();
                if(trans!=null)
                    trans.Dispose();
                throw;
            }
        }
    }
}
