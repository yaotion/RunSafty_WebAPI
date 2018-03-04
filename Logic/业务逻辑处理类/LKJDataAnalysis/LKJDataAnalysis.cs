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

namespace TF.RunSafty.Logic
{
    public class LKJDataAnalysis
    {
        #region 属性
        public DateTime? dtPostTime;
        public string strID;
        public int JiCheID;
        public DateTime? dtAnalysisDate;
        public string strFileName;
        #endregion 属性

         #region 构造函数
        public LKJDataAnalysis()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public LKJDataAnalysis(string jcid)
        {
            string strSql = "select * from TAB_LKJDataAnalysis where JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",jcid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["strID"].ToString();
                JiCheID = PageBase.static_ext_int(dt.Rows[0]["JiCheID"].ToString());
                dtPostTime = PageBase.static_ext_date(dt.Rows[0]["dtPostTime"].ToString());
                dtAnalysisDate = PageBase.static_ext_date(dt.Rows[0]["dtAnalysisDate"].ToString());
                strFileName = dt.Rows[0]["strFileName"].ToString();
            }
        }
        #endregion 构造函数
        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_LKJDataAnalysis (JiCheID,dtAnalysisDate,strFileName) values (@JiCheID,@dtAnalysisDate,@strFileName)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("dtAnalysisDate",dtAnalysisDate),
                                           new SqlParameter("strFileName",strFileName)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_LKJDataAnalysis set dtAnalysisDate=@dtAnalysisDate,strFileName=@strFileName where JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("dtAnalysisDate",dtAnalysisDate),
                                           new SqlParameter("strFileName",strFileName)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_LKJDataAnalysis where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int JiCheID)
        {
            string strSql = "select count(*) from TAB_LKJDataAnalysis where JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改
        #region 扩展方法

        public static int GetAllTAB_LKJDataAnalysis(int JiCheID)
        {
            string strSql = "select count(*) from TAB_LKJDataAnalysis where 1=1";
            if (JiCheID != 0)
            {
                strSql += " and JiCheID = @JiCheID ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID)
                                       };
            return PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).ToString());
        }
        #endregion 扩展方法
    }
}
