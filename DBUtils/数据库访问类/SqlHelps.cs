using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;

namespace TF.RunSafty.DBUtils
{
   public class SqlHelps
    {
       public static readonly string SQLConnString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

       public static readonly string VersionName = ConfigurationManager.ConnectionStrings["VersionName"].ConnectionString;

       public static readonly string VersionNumber = ConfigurationManager.ConnectionStrings["VersionNumber"].ConnectionString;

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 执行增、删、改等操作，返回受影响的行数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">执行SQL语句的类型</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="commandParameters">传递的参数数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行增、删、改等操作，返回受影响的行数
        /// </summary>
        /// <param name="connection">数据连接对象</param>
        /// <param name="cmdType">执行SQL语句的类型</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="commandParameters">传递的参数数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connection.Close();
            return val;
        }

        /// <summary>
        /// 执行增、删、改等操作，返回受影响的行数
        /// </summary>
        /// <param name="trans">定义的事务对象</param>
        /// <param name="cmdType">执行SQL语句的类型</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="commandParameters">传递的参数数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// 执行单条记录且只进的数据读取，返回类型为 SqlDataReader
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">执行SQL语句的类型</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="commandParameters">传递的参数数组</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
        
        }

        /// <summary>
        /// 执行单条记录且只进的数据读取，返回类型为 SqlDataReader
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">执行SQL语句的类型</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="commandParameters">传递的参数数组</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行第一行第一列数据的查询，返回为object的对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">执行SQL语句的类型</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="commandParameters">传递的参数数组</param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 读取多条数据,返回类型为DataSet
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">执行SQL语句的类型</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="commandParameters">传递的参数数组</param>
        /// <returns></returns>
        public static DataSet ReturnDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                conn.Dispose();
                return ds;
            }
            catch
            {
                conn.Close();
                conn.Dispose();
                throw;
            }
            finally
        {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行第一行第一列数据的查询，返回为object的对象
        /// </summary>
        /// <param name="connection">数据连接对象</param>
        /// <param name="cmdType">执行SQL语句的类型</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="commandParameters">传递的参数数组</param>
        /// <returns></returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            connection.Close();
            return val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="commandParameters"></param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
            {
                return null;
            }

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
            {
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
            }
            return clonedParms;
        }

        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="cmd">执行SQL语句的对象</param>
        /// <param name="conn">数据连接对象</param>
        /// <param name="trans">定义的事务对象</param>
        /// <param name="cmdType">执行SQL语句的类型</param>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="cmdParms">参数</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
    }
}
