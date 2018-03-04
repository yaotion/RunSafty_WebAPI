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
    /// 类名：WorkGuideDetail
    /// 描述：作业指导明细数据库操作类
    /// </summary>
    public class WorkGuideDetail
    {
        #region 属性
        public int nID;
        public int nOrder;
        public string strID;
        public DateTime? dtPostTime;
        public DateTime? dtModifyTime;
        public string strPictureFileName;
        public string strVideoFileName;
        public string strCheckContent;
        public string strRiskPoint;
        public string strPositionID;
        public string strWorkGuideID;
        #endregion

        private const string PAGEID = "72";

        #region 构造函数

        public WorkGuideDetail()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public WorkGuideDetail(string strID)
        {
            string strSql = "SELECT * FROM [TAB_WorkGuideDetail] WHERE [strID]=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strID",strID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.nID = PageBase.static_ext_int(dt.Rows[0]["nID"].ToString());
                this.strID = dt.Rows[0]["strID"].ToString();
                this.dtPostTime = PageBase.static_ext_date(dt.Rows[0]["dtPostTime"].ToString());
                this.strWorkGuideID = dt.Rows[0]["strWorkGuideID"].ToString();
                this.strPositionID = dt.Rows[0]["strPositionID"].ToString();
                this.strCheckContent = dt.Rows[0]["strCheckContent"].ToString();
                this.strPictureFileName = dt.Rows[0]["strPictureFileName"].ToString();
                this.strVideoFileName = dt.Rows[0]["strVideoFileName"].ToString();
                this.strRiskPoint = dt.Rows[0]["strRiskPoint"].ToString();
                this.dtModifyTime = PageBase.static_ext_date(dt.Rows[0]["dtModifyTime"].ToString());
                this.nOrder = PageBase.static_ext_int(dt.Rows[0]["nOrder"].ToString());
            }
        }
        #endregion

        #region 增删改

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            string strSql = @"INSERT INTO [TAB_WorkGuideDetail] (
[dtPostTime]
,[strWorkGuideID]
,[strPositionID]
,[strCheckContent]
,[strPictureFileName]
,[strVideoFileName]
,[strRiskPoint]
,[dtModifyTime]
,[nOrder]
)VALUES(
@dtPostTime,
@strWorkGuideID,
@strPositionID,
@strCheckContent,
@strPictureFileName,
@strVideoFileName,
@strRiskPoint,
@dtModifyTime,
@nOrder
)";
            SqlParameter[] sqlParams = { 
                                                new SqlParameter("@dtPostTime",dtPostTime),
                                                new SqlParameter("@strWorkGuideID",strWorkGuideID),
                                                new SqlParameter("@strPositionID",strPositionID),
                                                new SqlParameter("@strCheckContent",strCheckContent),
                                                new SqlParameter("@strPictureFileName",strPictureFileName),
                                                new SqlParameter("@strVideoFileName",strVideoFileName),
                                                new SqlParameter("@strRiskPoint",strRiskPoint),
                                                new SqlParameter("@dtModifyTime",dtModifyTime),
                                                new SqlParameter("@nOrder",nOrder)
                                   };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = @"UPDATE [TAB_WorkGuideDetail] SET
[strWorkGuideID]=@strWorkGuideID
,[strPositionID]=@strPositionID
,[strCheckContent]=@strCheckContent
,[strPictureFileName]=@strPictureFileName
,[strVideoFileName]=@strVideoFileName
,[strRiskPoint]=@strRiskPoint
,[nOrder]=@nOrder
WHERE [strID]=@strID";
            SqlParameter[] sqlParams = { 
                                       new SqlParameter("@strWorkGuideID",strWorkGuideID),
                                       new SqlParameter("@strPositionID",strPositionID),
                                       new SqlParameter("@strCheckContent",strCheckContent),
                                       new SqlParameter("@strPictureFileName",strPictureFileName),
                                       new SqlParameter("@strVideoFileName",strVideoFileName),
                                       new SqlParameter("@strRiskPoint",strRiskPoint),
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
            string strSql = "DELETE FROM [TAB_WorkGuideDetail] WHERE [strID]=@strID";
            SqlParameter sqlParam = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParam) > 0;
        }

        #endregion

        #region 扩展方法
        /// <summary>
        /// 返回所有记录
        /// </summary>
        public static DataTable GetAll(string strWorkGuideID, string strPositionID)
        {
            string strSql = "select * from [TAB_WorkGuideDetail] where [strWorkGuideID]=@strWorkGuideID and [strPositionID]=@strPositionID order by [nOrder] asc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strWorkGuideID", strWorkGuideID),
                                            new SqlParameter("@strPositionID", strPositionID)
                                       };

            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        public static int GetCount(string strWorkGuideID, string strPositionID)
        {
            string strSql = "select count(*) from [TAB_WorkGuideDetail] where [strWorkGuideID]=@strWorkGuideID and [strPositionID]=@strPositionID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strWorkGuideID", strWorkGuideID),
                                            new SqlParameter("@strPositionID", strPositionID)
                                       };
            return int.Parse(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams).ToString());
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public static bool UpdateSortid(string strID, int nOrder)
        {
            string strSql = "update [TAB_WorkGuideDetail] set [nOrder]=@nOrder where [strID]=@strID";
            SqlParameter[] sqlParams ={
                                          new SqlParameter("@nOrder", nOrder),
                                          new SqlParameter("@strID",strID)
            };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParams) > 0;
        }


        /// <summary>
        /// 清理图片文件
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static bool CleanImageFile(string strID)
        {
            WorkGuideDetail dal = new WorkGuideDetail(strID);
            if (string.IsNullOrEmpty(dal.strID)) { return false; }
            string savePath = System.Web.HttpContext.Current.Request.MapPath(@"/文件/机车整备场管理信息系统/作业指导文件/");
            try
            {
                if (!string.IsNullOrEmpty(dal.strPictureFileName))
                    if (System.IO.File.Exists(savePath + dal.strPictureFileName))
                    {
                        System.IO.File.Delete(savePath + dal.strPictureFileName);
                    }
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 清理视频文件
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static bool CleanVideoFile(string strID)
        {
            WorkGuideDetail dal = new WorkGuideDetail(strID);
            if (string.IsNullOrEmpty(dal.strID)) { return false; }
            string savePath = System.Web.HttpContext.Current.Request.MapPath(@"/文件/机车整备场管理信息系统/作业指导文件/");
            try
            {
                if (!string.IsNullOrEmpty(dal.strVideoFileName))
                    if (System.IO.File.Exists(savePath + dal.strVideoFileName))
                    {
                        System.IO.File.Delete(savePath + dal.strVideoFileName);
                    }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 清理文件（所有 图片和视频）
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static bool CleanFile(string strID)
        {
            WorkGuideDetail dal = new WorkGuideDetail(strID);
            if (string.IsNullOrEmpty(dal.strID)) { return false; }
            string savePath = System.Web.HttpContext.Current.Request.MapPath(@"/文件/机车整备场管理信息系统/作业指导文件/");
            try
            {
                if (!string.IsNullOrEmpty(dal.strPictureFileName))
                    if (System.IO.File.Exists(savePath + dal.strPictureFileName))
                    {
                        System.IO.File.Delete(savePath + dal.strPictureFileName);
                    }
                if (!string.IsNullOrEmpty(dal.strVideoFileName))
                    if (System.IO.File.Exists(savePath + dal.strVideoFileName))
                    {
                        System.IO.File.Delete(savePath + dal.strVideoFileName);
                    }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除记录和文件
        /// </summary>
        /// <returns></returns>
        public static bool DeleteRecordAndFile(string strID)
        {
            CleanFile(strID);
            string strSql = "DELETE FROM [TAB_WorkGuideDetail] WHERE [strID]=@strID";
            SqlParameter sqlParam = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql, sqlParam) > 0;
        }
        #endregion

    }
}
