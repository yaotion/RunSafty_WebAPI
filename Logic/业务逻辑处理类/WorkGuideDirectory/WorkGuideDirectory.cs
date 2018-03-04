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
    /// <summary>
    /// 类名：WorkGuideDirectory
    /// 描述：作业指导目录数据库操作类
    /// </summary>
    public class WorkGuideDirectory
    {
        #region 属性
        public int nID;
        public string strID;
        public DateTime? dtPostTime;
        public string strTitle;
        public string strDirectoryID;
        #endregion 属性

        #region 构造函数

        public WorkGuideDirectory()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public WorkGuideDirectory(string strID)
        {
            string strSql = "select * from [TAB_WorkGuideDirectory] where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strID",strID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.nID = PageBase.static_ext_int(dt.Rows[0]["nID"].ToString());
                this.strID = dt.Rows[0]["strID"].ToString();
                this.dtPostTime = PageBase.static_ext_date(dt.Rows[0]["dtPostTime"].ToString());
                this.strTitle = dt.Rows[0]["strTitle"].ToString();
                this.strDirectoryID = dt.Rows[0]["strDirectoryID"].ToString();
            }
        }

        #endregion 构造函数

        #region 增删改

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            string strSql = "insert into [TAB_WorkGuideDirectory] ([dtPostTime],[strTitle],[strDirectoryID])values(@dtPostTime,@strTitle,@strDirectoryID)";
            SqlParameter[] sqlParams = { 
                                       new SqlParameter("@dtPostTime",dtPostTime),
                                       new SqlParameter("@strTitle",strTitle),
                                       new SqlParameter("@strDirectoryID",strDirectoryID)
                                   };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = "update [TAB_WorkGuideDirectory] set [dtPostTime]=@dtPostTime,[strTitle]=@strTitle,[strDirectoryID]=@strDirectoryID where [strID]=@strID";
            SqlParameter[] sqlParams = { 
                                       new SqlParameter("@dtPostTime",dtPostTime),
                                       new SqlParameter("@strTitle",strTitle),
                                       new SqlParameter("@strDirectoryID",strDirectoryID),
                                       new SqlParameter("@strID",strID)
                                   };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = "delete from [TAB_WorkGuideDirectory] where [strID]=@strID";
            SqlParameter sqlParam = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParam) > 0;
        }

        #endregion 增删改

        #region 扩展方法

        /// <summary>
        /// 返回层次下属所有节点
        /// </summary>
        /// <param name="strDirectoryID">上级层次ID</param>
        /// <returns></returns>
        public static DataTable GetAll(string strDirectoryID)
        {
            string strSql = "select * from [TAB_WorkGuideDirectory] where [strDirectoryID]=@strDirectoryID";
            SqlParameter sqlParam = new SqlParameter("@strDirectoryID", strDirectoryID);
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParam).Tables[0];
        }

        /// <summary>
        /// 判断是否存在子目录
        /// </summary>
        /// <returns></returns>
        public static bool HasChildren(string strID)
        {
            SqlParameter sqlParam = new SqlParameter("@strDirectoryID", strID);
    
            string strDirSql = "select count(*) from [TAB_WorkGuideDirectory] where [strDirectoryID]=@strDirectoryID";
            int nDirCount = PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strDirSql, sqlParam).ToString());

            string strNodeSql = "select count(*) from [TAB_WorkGuide] where [strDirectoryID]=@strDirectoryID";
            int nNodeCount = PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strNodeSql, sqlParam).ToString());

            return nDirCount + nNodeCount > 0;
        }

        /// <summary>
        ///安全删除，避免存在子节点时被删除
        /// </summary>
        /// <returns></returns>
        public static bool SafeDelete(string strID)
        {
            if (HasChildren(strID))
                return false;
            string strSql = "delete from [TAB_WorkGuideDirectory] where [strID]=@strID";
            SqlParameter sqlParam = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParam) > 0;
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static bool IsExist(string strID)
        {
            string strSql = "select count(*) from [TAB_WorkGuideDirectory] where [strID]=@strID";
            SqlParameter sqlParam = new SqlParameter("@strID", strID);
            return PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParam).ToString()) > 0;
        }

        #endregion
    }
}
