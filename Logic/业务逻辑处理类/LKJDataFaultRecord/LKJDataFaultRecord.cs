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
    public class LKJDataFaultRecord
    {
        #region 属性
        public string strID;
        public int JiCheID;
        public string strCode;
        public int nCount;
        #endregion 属性

         #region 构造函数
        public LKJDataFaultRecord()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public LKJDataFaultRecord(string jcid)
        {
            string strSql = "select * from TAB_LKJDataFaultRecord where JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",jcid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["strID"].ToString();
                JiCheID = PageBase.static_ext_int(dt.Rows[0]["JiCheID"].ToString());
                strCode = dt.Rows[0]["strCode"].ToString();
            }
        }
        #endregion 构造函数
        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_LKJDataFaultRecord (JiCheID,strCode,nCount) values (@JiCheID,@strCode,@nCount)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("strCode",strCode),
                                           new SqlParameter("nCount",nCount)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_LKJDataFaultRecord set strCode=@strCode,nCount=@nCount where JiCheID=@JiCheID and strCode=@strCode";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("strCode",strCode),
                                           new SqlParameter("nCount",nCount)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_LKJDataFaultRecord where JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int JiCheID, string strCode)
        {
            string strSql = "select count(*) from TAB_LKJDataFaultRecord where JiCheID=@JiCheID and strCode=@strCode";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("strCode",strCode)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams))> 0;
        }
        #endregion 增删改
        #region 扩展方法

        //public static int GetAllTAB_LKJDataAnalysis(string JiCheID)
        //{
        //    string strSql = "select count(*) from TAB_LKJDataFaultRecord where 1=1";
        //    if (JiCheID != "")
        //    {
        //        strSql += " and JiCheID = @JiCheID ";
        //    }
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("JiCheID",JiCheID)
        //                               };
        //    return PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).ToString());
        //}
        #endregion 扩展方法
    }
}
