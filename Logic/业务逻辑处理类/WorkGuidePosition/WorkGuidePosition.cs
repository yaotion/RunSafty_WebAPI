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
    /// 类名：WorkGuidePosition
    /// 描述：作业指导步骤数据库操作类
    /// </summary>
    public class WorkGuidePosition
    {
        #region 属性
        public int nID;
        public string strID;
        public int nOrder;
        public int nStepType;
        public DateTime? dtPostTime;
        public DateTime? dtModifyTime;
        public string strPositionName;
        public string strWorkGuideID;
        #endregion


        private const string PAGEID = "72";

        #region 构造函数

        public WorkGuidePosition()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public WorkGuidePosition(string strID)
        {
            string strSql = "select * from [TAB_WorkGuidePosition] where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strID",strID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.nID = PageBase.static_ext_int(dt.Rows[0]["nID"].ToString());
                this.strID = dt.Rows[0]["strID"].ToString();
                this.dtPostTime = PageBase.static_ext_date(dt.Rows[0]["dtPostTime"].ToString());
                this.dtModifyTime = PageBase.static_ext_date(dt.Rows[0]["dtModifyTime"].ToString());
                this.nOrder = PageBase.static_ext_int(dt.Rows[0]["nOrder"].ToString());
                this.strWorkGuideID = dt.Rows[0]["strWorkGuideID"].ToString();
                this.strPositionName = dt.Rows[0]["strPositionName"].ToString();
                this.nStepType = PageBase.static_ext_int(dt.Rows[0]["nStepType"].ToString());
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
            string strSql = @"INSERT INTO [TAB_WorkGuidePosition] (
[dtPostTime]
,[strPositionName]
,[strWorkGuideID]
,[nStepType]
,[dtModifyTime]
,[nOrder])
VALUES(
@dtPostTime,
@strPositionName,
@strWorkGuideID,
@nStepType,
@dtModifyTime,
@nOrder
)";
            SqlParameter[] sqlParams = { 
                                       new SqlParameter("@dtPostTime",dtPostTime),
                                       new SqlParameter("@strPositionName",strPositionName),
                                       new SqlParameter("@strWorkGuideID",strWorkGuideID),
                                       new SqlParameter("@nStepType",nStepType),
                                       new SqlParameter("@nOrder",nOrder),
                                       new SqlParameter("@dtModifyTime",dtModifyTime)
                                   };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = @"UPDATE [TAB_WorkGuidePosition] SET 
[dtPostTime]=@dtPostTime,
[strPositionName]=@strPositionName,
[strWorkGuideID]=@strWorkGuideID,
[nStepType]=@nStepType,
[dtModifyTime]=@dtModifyTime,
[nOrder]=@nOrder
WHERE [strID]=@strID";
            SqlParameter[] sqlParams = { 
                                       new SqlParameter("@dtPostTime",dtPostTime),
                                       new SqlParameter("@strPositionName",strPositionName),
                                       new SqlParameter("@strWorkGuideID",strWorkGuideID),
                                       new SqlParameter("@nStepType",nStepType),
                                       new SqlParameter("@dtModifyTime",dtModifyTime),
                                       new SqlParameter("@nOrder",nOrder),
                                       new SqlParameter("@strID",strID)
                                   };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = "DELETE FROM [TAB_WorkGuidePosition] WHERE [strID]=@strID";
            SqlParameter sqlParam = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParam) > 0;
        }

        #endregion 增删改
        #region 扩展操作

        /// <summary>
        /// 返回所有记录
        /// </summary>
        public static DataTable GetAll(string strWorkGuideID)
        {
            string strSql = "select * from [View_WorkGuidePosition] where [strWorkGuideID]=@strWorkGuideID order by [nOrder] asc";
            SqlParameter sqlParam = new SqlParameter("@strWorkGuideID", strWorkGuideID);
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParam).Tables[0];
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        public static int GetCount(string strWorkGuideID)
        {
            string strSql = "select count(*) from [TAB_WorkGuide] where [strWorkGuideID]=@strWorkGuideID";
            SqlParameter sqlParam = new SqlParameter("@strWorkGuideID", strWorkGuideID);
            return int.Parse(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParam).ToString());
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public static bool Delete(string strID)
        {
            string strSql = "DELETE FROM [TAB_WorkGuidePosition] WHERE [strID]=@strID";
            SqlParameter sqlParam = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParam) > 0;
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public static bool UpdateSortid(string strID, int nOrder)
        {
            string strSql = "update [TAB_WorkGuidePosition] set [nOrder]=@nOrder where [strID]=@strID";
            SqlParameter[] sqlParams ={
                                          new SqlParameter("@nOrder", nOrder),
                                          new SqlParameter("@strID",strID)
            };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion
    }
}
