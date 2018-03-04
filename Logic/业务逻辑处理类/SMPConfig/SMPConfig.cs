using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

public class SMPConfig
{
    #region 属性
    public string pageid;
    public string title;
    public string selectsql;
    public string selectCountSql;
    public string selectExportSql;
    public string sqlConString;
    #endregion 属性

    #region 构造函数
    public SMPConfig()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public SMPConfig(string pageid)
    {
        string strSql = "select * from Tab_SMPConfig where pageid=@pageid";
        SqlParameter[] sqlParams = {
                                           new SqlParameter("pageid",pageid)
                                       };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            pageid = dt.Rows[0]["pageid"].ToString();
            title = dt.Rows[0]["title"].ToString();
            selectsql = dt.Rows[0]["selectsql"].ToString();
            selectExportSql = dt.Rows[0]["selectExportSql"].ToString();
            selectCountSql = dt.Rows[0]["selectCountSql"].ToString();
            sqlConString = dt.Rows[0]["sqlConString"].ToString();
        }
    }
    #endregion 构造函数

    #region 增删改
    public bool Add()
    {
        string guid = Guid.NewGuid().ToString();
        string strSql = "insert into Tab_SMPConfig (pageid,title,selectsql,selectExportSql,selectCountSql,sqlConString)values(@pageid,@title,@selectsql,@selectExportSql,@selectCountSql,@sqlConString)";
        SqlParameter[] sqlParams = {
                                           new SqlParameter("pageid",guid),
                                           new SqlParameter("title",title),
                                           new SqlParameter("selectsql",selectsql),
                                           new SqlParameter("selectExportSql",selectExportSql),
                                           new SqlParameter("selectCountSql",selectCountSql),
                                           new SqlParameter("sqlConString",sqlConString)
                                       };
        return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
    }
    public bool Update()
    {
        string strSql = "update Tab_SMPConfig set title = @title,selectsql=@selectsql,selectExportSql=@selectExportSql,selectCountSql=@selectCountSql,sqlConString=@sqlConString where pageid=@pageid";
        SqlParameter[] sqlParams = {
                                          new SqlParameter("pageid",pageid),
                                           new SqlParameter("title",title),
                                           new SqlParameter("selectsql",selectsql),
                                           new SqlParameter("selectExportSql",selectExportSql),
                                           new SqlParameter("selectCountSql",selectCountSql),
                                           new SqlParameter("sqlConString",sqlConString)
                                       };
        return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
    }
    public static bool Delete(string pageid)
    {
        string strSql = "delete Tab_SMPConfig where pageid=@pageid";
        SqlParameter[] sqlParams = {
                                           new SqlParameter("pageid",pageid)
                                       };
        return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
    }
    public static bool Exist(string pageid, string Name)
    {
        string strSql = "select count(*) from Tab_SMPConfig where pageid=@pageid ";
        SqlParameter[] sqlParams = {
                                           new SqlParameter("pageid",pageid)
                                       };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
    }
    public bool UpdatesqlConString()
    {
        string strSql = "update Tab_SMPConfig set sqlConString=@sqlConString where pageid=@pageid";
        SqlParameter[] sqlParams = {
                                           new SqlParameter("sqlConString",sqlConString),
                                           new SqlParameter("pageid",pageid)
                                       };
        return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
    }
    #endregion 增删改

    #region 扩展方法
    public static DataTable GetAllSMPConfig(string pageid)
    {
        string strSql = "select * from Tab_SMPConfig where 1=1";
        if (pageid != "")
        {
            strSql += " and pageid = @pageid ";
        }
        SqlParameter[] sqlParams = {
                                           new SqlParameter("pageid",pageid)
                                       };
        return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
    }
    #endregion
}

