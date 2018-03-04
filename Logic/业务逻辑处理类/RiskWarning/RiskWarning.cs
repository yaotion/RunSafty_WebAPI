using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.Logic
{

    /// <summary>
    /// 类名：RiskWarning
    /// 描述：风险预警提示数据库操作类
    /// </summary>
    public class RiskWarning
    {
        #region 属性
        public int nID;
        public int nStepType;
        public string strID;
        public string strContent;
        public DateTime? dtPostTime;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        #endregion 属性

        private const string PAGEID = "71";

        #region 构造函数
        public RiskWarning()
        {
        }

        public RiskWarning(string strID)
        {
            string strSql = "select * from [TAB_RiskWarning] where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strID",strID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                nID = PageBase.static_ext_int(dt.Rows[0]["nID"].ToString());
                strID = dt.Rows[0]["strID"].ToString();
                nStepType = PageBase.static_ext_int(dt.Rows[0]["nStepType"].ToString());
                strContent = dt.Rows[0]["strContent"].ToString();
                dtPostTime = PageBase.static_ext_date(dt.Rows[0]["dtPostTime"].ToString());
                dtBeginTime = PageBase.static_ext_date(dt.Rows[0]["dtBeginTime"].ToString());
                dtEndTime = PageBase.static_ext_date(dt.Rows[0]["dtEndTime"].ToString());
            }
        }
        #endregion 构造函数

        #region 增删改
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            string strSql = "insert into [TAB_RiskWarning] (nStepType,strContent,dtPostTime,dtBeginTime,dtEndTime)values(@nStepType,@strContent,@dtPostTime,@dtBeginTime,@dtEndTime)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nStepType",nStepType),
                                           new SqlParameter("strContent",strContent),
                                           new SqlParameter("dtPostTime",dtPostTime),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("dtEndTime",dtEndTime)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = "update [TAB_RiskWarning] set nStepType=@nStepType,strContent=@strContent,dtPostTime=@dtPostTime,dtBeginTime=@dtBeginTime,dtEndTime=@dtEndTime where strID=@strID";
            SqlParameter[] sqlParams = {
                                    new SqlParameter("@nStepType",nStepType),
                                    new SqlParameter("@strContent",strContent),
                                    new SqlParameter("@dtPostTime",dtPostTime),
                                    new SqlParameter("@dtBeginTime",dtBeginTime),
                                    new SqlParameter("@dtEndTime",dtEndTime),
                                    new SqlParameter("@strID",strID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 删除风险预警提示记录以及下属附件
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            //删除相关附件记录
            Attachment.DeleteByRiskWarning(strID);
            string strSql = "delete from [TAB_RiskWarning] where strID=@strID";
            SqlParameter sqlParams = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }

        #endregion 增删改


        #region 扩展方法

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static bool IsExist(string strID)
        {
            string strSql = "select count(*) from [TAB_RiskWarning] where strID=@strID";
            SqlParameter sqlParams = new SqlParameter("@strID", strID);
            return int.Parse(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams).ToString()) > 0;
        }


        /// <summary>
        /// 删除风险预警提示记录以及下属附件
        /// </summary>
        /// <returns></returns>
        public static bool Delete(string strID)
        {
            //删除相关附件记录
            Attachment.DeleteByRiskWarning(strID);
            string strSql = "delete from [TAB_RiskWarning] where strID=@strID";
            SqlParameter sqlParams = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }
        #region
        /// <summary>
        /// 获取表中所有数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAll()
        {
            string strSql = "select * from [TAB_RiskWarning]";
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql).Tables[0];

        }
        #endregion
        #endregion
    }
}
