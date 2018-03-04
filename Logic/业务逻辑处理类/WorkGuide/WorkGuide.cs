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
    /// 类名：WorkGuide
    /// 描述：作业指导文件数据库操作类
    /// </summary>
    public class WorkGuide
    {
        #region 属性
        public int nID;
        public int nFileSize;
        public string strID;
        public DateTime? dtPostTime;
        public string strTitle;
        public string strFileName;
        public string strOriginalFileName;
        public string strDirectoryID;
        public string strTrainType;
        #endregion 属性

        #region 构造函数

        public WorkGuide()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public WorkGuide(string strID)
        {
            string strSql = "select * from [TAB_WorkGuide] where strID=@strID";
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
                this.strFileName = dt.Rows[0]["strFileName"].ToString();
                this.strOriginalFileName = dt.Rows[0]["strOriginalFileName"].ToString();
                this.strTrainType = dt.Rows[0]["strTrainType"].ToString();
                this.nFileSize = PageBase.static_ext_int(dt.Rows[0]["nFileSize"].ToString());
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
            string strSql = "insert into [TAB_WorkGuide] ([dtPostTime],[strTitle],[strDirectoryID],[nFileSize],[strTrainType],[strFileName],[strOriginalFileName])values(@dtPostTime,@strTitle,@strDirectoryID,@nFileSize,@strTrainType,@strFileName,@strOriginalFileName)";
            SqlParameter[] sqlParams = { 
                                       new SqlParameter("@dtPostTime",dtPostTime),
                                       new SqlParameter("@strTitle",strTitle),
                                       new SqlParameter("@strDirectoryID",strDirectoryID),
                                       new SqlParameter("@nFileSize",nFileSize),
                                       new SqlParameter("@strTrainType",strTrainType),
                                       new SqlParameter("@strFileName",strFileName),
                                       new SqlParameter("@strOriginalFileName",strOriginalFileName)
                                   };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = "update [TAB_WorkGuide] set [dtPostTime]=@dtPostTime,[strTitle]=@strTitle,[strDirectoryID]=@strDirectoryID,[nFileSize]=@nFileSize,[strTrainType]=@strTrainType,[strFileName]=@strFileName,[strOriginalFileName]=@strOriginalFileName";
            SqlParameter[] sqlParams = { 
                                       new SqlParameter("@dtPostTime",dtPostTime),
                                       new SqlParameter("@strTitle",strTitle),
                                       new SqlParameter("@strDirectoryID",strDirectoryID),
                                       new SqlParameter("@nFileSize",nFileSize),
                                       new SqlParameter("@strTrainType",strTrainType),
                                       new SqlParameter("@strFileName",strFileName),
                                       new SqlParameter("@strOriginalFileName",strOriginalFileName)
                                   };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = "delete from [TAB_WorkGuide] where [strID]=@strID";
            SqlParameter sqlParam = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParam) > 0;
        }

        #endregion 增删改

        #region 扩展方法


        /// <summary>
        /// 删除上传的文件
        /// </summary>
        /// <param name="strID">记录ID</param>
        /// <returns></returns>
        private static bool DeleteUploadFile(string strID)
        {
            WorkGuide workGuid = new WorkGuide(strID);
            string fileName = workGuid.strFileName;
            string filePath = HttpContext.Current.Server.MapPath(@"\文件\机车整备场管理信息系统\作业指导文件\" + fileName);
            try
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 返回层次下属所有节点
        /// 使用视图[View_WorkGuide]
        /// </summary>
        /// <param name="strDirectoryID">上级层次ID</param>
        /// <returns></returns>
        public static DataTable GetAll(string strDirectoryID)
        {
            string strSql = "select * from [View_WorkGuide] where [strDirectoryID]=@strDirectoryID";
            SqlParameter sqlParam = new SqlParameter("@strDirectoryID", strDirectoryID);

            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParam).Tables[0];
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="strDirectoryID"></param>
        /// <returns></returns>
        public static int GetCount(string strDirectoryID)
        {
            string strSql = "select count(*) from [TAB_WorkGuide] where [strDirectoryID]=@strDirectoryID";
            SqlParameter sqlParam = new SqlParameter("@strDirectoryID", strDirectoryID);
            return int.Parse(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParam).ToString());
        }

        /// <summary>
        /// 同时删除记录和已上传的文件
        /// </summary>
        /// <returns></returns>
        public static bool DeleteReocrdAndFile(string strID)
        {
            //DeleteUploadFile(strID);
            string strSql = "delete from [TAB_WorkGuide] where [strID]=@strID";
            SqlParameter sqlParam = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParam) > 0;
        }
        #endregion
    }
}
