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
    /// 类名：Attachment
    /// 描述：风险预警提示附件数据库操作类
    /// </summary>
    public class Attachment
    {
        #region 属性
        public int nID;
        public string strID;
        public DateTime? dtPostTime;
        public string strFileName;
        public string strTableName;
        public string strRecordID;
        public int nType;
        public int nExpand;
        public string strTitle;
        #endregion 属性

        private const string PAGEID = "71";

        #region 构造函数
        public Attachment()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public Attachment(string strID)
        {
            string strSql = "select * from [TAB_Attachment] where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strID",strID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.nID = PageBase.static_ext_int(dt.Rows[0]["nID"].ToString());
                this.strID = dt.Rows[0]["strID"].ToString();
                this.dtPostTime = PageBase.static_ext_date(dt.Rows[0]["dtPostTime"].ToString());
                this.strFileName = dt.Rows[0]["strFileName"].ToString();
                this.strTableName = dt.Rows[0]["strTableName"].ToString();
                this.strRecordID = dt.Rows[0]["strRecordID"].ToString();
                this.nType = PageBase.static_ext_int(dt.Rows[0]["nType"].ToString());
                this.nExpand = PageBase.static_ext_int(dt.Rows[0]["nExpand"].ToString());
                this.strTitle = dt.Rows[0]["strTitle"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into [TAB_Attachment] ([dtPostTime],[strFileName],[strTableName],[strRecordID],[nType],[nExpand],[strTitle])values(@dtPostTime,@strFileName,@strTableName,@strRecordID,@nType,@nExpand,@strTitle)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@dtPostTime",dtPostTime),
                                           new SqlParameter("@strFileName",strFileName),
                                           new SqlParameter("@strTableName",strTableName),
                                           new SqlParameter("@strRecordID",strRecordID),
                                           new SqlParameter("@nType",nType),
                                           new SqlParameter("@nExpand",nExpand),
                                           new SqlParameter("@strTitle",strTitle)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "update [TAB_Attachment] set [dtPostTime]=@dtPostTime,[strFileName]=@strFileName,[strTableName]=@strTableName,[strRecordID]=@strRecordID,[nType]=@nType,[nExpand]=@nExpand,[strTitle]=@strTitle where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@dtPostTime",dtPostTime),
                                           new SqlParameter("@strFileName",strFileName),
                                           new SqlParameter("@strTableName",strTableName),
                                           new SqlParameter("@strRecordID",strRecordID),
                                           new SqlParameter("@nType",nType),
                                           new SqlParameter("@nExpand",nExpand),
                                           new SqlParameter("@strTitle",strTitle),
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
            string strSql = "delete from [TAB_Attachment] where strID=@strID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("@strID",strID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 根据strID删除记录
        /// </summary>
        /// <param name="strid"></param>
        /// <returns></returns>
        public static bool Delete(string strID)
        {
            string strSql = "delete from [TAB_Attachment] where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strID",strID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        /// <summary>
        /// 根据风险预警提示记录编号删除对应的记录
        /// </summary>
        /// <param name="strRecordID"></param>
        /// <returns></returns>
        public static bool DeleteByRiskWarning(string strRecordID)
        {
            string strSql = "delete from [TAB_Attachment] where strRecordID=@strRecordID";
            SqlParameter sqlParams = new SqlParameter("@strRecordID", strRecordID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 根据预警记录删除所有附属文件
        /// </summary>
        /// <param name="strRecordID"></param>
        /// <returns></returns>
        public static bool DeleteAllFilesByRiskWarningStrID(string strRecordID)
        {
            try
            {
                string strSql = "select [strFileName],[dtPostTime] from [TAB_Attachment] where [strRecordID]=@strRecordID";
                SqlParameter sqlParams = new SqlParameter("@strRecordID", strRecordID);
                DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql,sqlParams).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    string fileName = dr["strFileName"].ToString();
                  string filePath = HttpContext.Current.Server.MapPath(@"\文件\机车整备场管理信息系统\附件文件\"+ fileName);
                    System.IO.File.Delete(filePath);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
