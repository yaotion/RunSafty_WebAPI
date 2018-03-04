using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

    /// <summary>
    ///searchmaster 的摘要说明
    /// </summary>
    public class searchmaster
    {
        protected string SQLCondition = " where 1=1 "; //SQL查询条件

        public searchmaster()
        {
            
        }

        /// <summary>
        /// 取DataGrid配置
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public DataTable GetDataGridConfig(string pageid)
        {
            string strSql = "select * from Tab_SMPDataGridConfig where pageid=@pageid";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("pageid",pageid)
                                          };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameter).Tables[0];
        }

        /// <summary>
        /// 取搜索条件或数据列配置
        /// </summary>
        /// <param name="pageid"></param>
        /// <param name="pagetype"></param>
        /// <returns></returns>
        public DataTable GetSource(string pageid,string pagetype)
        {
            string tablename=pagetype=="0"?"VIEW_SearchPageColumns":"VIEW_SMPControlAndAttribute";
            string where = pagetype == "0" ? " pagetype=0" : " pagetype<>0";
            string strSql = @"select * from "+tablename+" where pageid=@pageid and "+where+" order by sortid asc";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("pageid",pageid)
                                          };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameter).Tables[0];
        }

        /// <summary>
        /// 配置数据json序列化
        /// </summary>
        /// <param name="pageid"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public string GetConfig(string pageid, int page, int rows, string where, string ordby)
        {
            if (pageid != "")
            {
                PageBase p = new PageBase();
                DataTable dt = getConfigData(pageid);
                DataTable dt1 = getDataFromConfig(dt, page, rows, where,ordby);
                if (dt1.Rows.Count > 0)
                {
                    return p.SerializeP(dt1, p.ext_int(getDataFromConfigCount(dt, where)));
                }
            }
            return "{\"total\":0,\"rows\":[]}";
        }

        /// <summary>
        /// 获取页面sql配置
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public DataTable getConfigData(string pageid)
        {
            string strSql = @"select title,selectsql,selectCountSql,selectExportSql,sqlConString from Tab_SMPConfig where pageid=@pageid";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("pageid",pageid)
                                          };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameter).Tables[0];
        }

        /// <summary>
        /// 解析配置sql
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        private DataTable getDataFromConfig(DataTable dt,int page, int rows,string where,string ordby)
        {
            string selectsql=dt.Rows[0]["selectsql"].ToString();
            string[] strsql = selectsql.Split('@');
            string sql = strsql[0] + rows + strsql[1]+ (rows * (page - 1)) + strsql[2];
            sql = sql.Replace("&", where);
            sql = sql.Replace("#", ordby);
            return SqlHelper.ExecuteDataset(ConnString(PageBase.static_ext_string(dt.Rows[0]["sqlConString"].ToString())), CommandType.Text, sql).Tables[0];
        }

        /// <summary>
        /// 解析配置计算数量sql
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        private object getDataFromConfigCount(DataTable dt, string where)
        {
            string selectsql = dt.Rows[0]["selectCountSql"].ToString();
            string[] strsql = selectsql.Split('@');
            string sql = strsql[0] + where;
            if (strsql.Length > 1)
            {
                sql += strsql[1];
            }
            return SqlHelper.ExecuteScalar(ConnString(PageBase.static_ext_string(dt.Rows[0]["sqlConString"].ToString())), CommandType.Text, sql);
        }
        /// <summary>
        /// 解析配置导出excel sql 并执行返回dt
        /// </summary>
        /// <param name="pageid"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable getExportDataFromConfig(string pageid, string where)
        {
            DataTable dt = getConfigData(pageid);
            string selectsql =PageBase.static_ext_string(dt.Rows[0]["selectExportSql"]);
            if (selectsql != "")
            {
                selectsql = selectsql.Replace("@", where);
                return SqlHelper.ExecuteDataset(ConnString(PageBase.static_ext_string(dt.Rows[0]["sqlConString"].ToString())), CommandType.Text, selectsql).Tables[0];
            }
            DataTable nulldt = new DataTable();
            return nulldt;
        }
        /// <summary>
        /// 如有自定义数据库连接字符串适用自定义 否则用默认
        /// </summary>
        /// <param name="ConnString"></param>
        /// <returns></returns>
        private string ConnString(string ConnString)
        {
            return ConnString = ConnString != "" ? ConnString : SqlHelper.ConnString;
        }

        /// <summary>
        /// 取连接字符串
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public static string GetSqlConnConfig(string pageid)
        {
            string strSql = "select sqlConString from Tab_SMPConfig where pageid=@pageid";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("pageid",pageid)
                                          };
            string ConnString = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameter).ToString();
            return ConnString = ConnString != "" ? ConnString : SqlHelper.ConnString;
        }
    }

