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

namespace ThinkFreely.RunSafty
{

    /// <summary>
    ///类名：JiaoFuJieShi
    ///描述：发布揭示数据库操作类
    /// </summary>
    public class DBRC_JFJieShiFile
    {
        #region 属性

        public string strPUBJieShiGUID;
        public string strTitle;
        public string strFileName;
        public string strOrginName;
        public int nFileSize;
        public DateTime? dtUpTime;

        #endregion

        #region 构造函数
        public DBRC_JFJieShiFile()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public DBRC_JFJieShiFile(string strPUBJieShiGUID)
        {
            string strSql = "SELECT * FROM [TAB_PUBJieShi_File] WHERE [strPUBJieShiGUID]=@strPUBJieShiGUID";
            SqlParameter sqlParam = new SqlParameter("@strPUBJieShiGUID", strPUBJieShiGUID);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.strPUBJieShiGUID = PageBase.static_ext_string(dr["strPUBJieShiGUID"].ToString());
                this.strTitle = PageBase.static_ext_string(dr["strTitle"].ToString());
                this.strFileName = PageBase.static_ext_string(dr["strFileName"].ToString());
                this.strOrginName = PageBase.static_ext_string(dr["strOrginName"].ToString());
                this.nFileSize = PageBase.static_ext_int(dr["nFileSize"].ToString());
                this.dtUpTime = PageBase.static_ext_date(dr["dtUpTime"].ToString());
            }
        }
        #endregion

        #region 增删改

        /// <summary>
        /// 增加交付揭示
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            string strSql = "INSERT INTO [TAB_PUBJieShi_File] ([strTitle] ,[strFileName] ,[strOrginName],[nFileSize] ,[dtUpTime] ) VALUES (@strTitle ,@strFileName ,@strOrginName,@nFileSize ,@dtUpTime)";
            SqlParameter[] sqlParams = {
                                new SqlParameter("@strTitle", this.strTitle),
                                new SqlParameter("@strFileName",this.strFileName),
                                new SqlParameter("@strOrginName",this.strOrginName),
                                new SqlParameter("@nFileSize", this.nFileSize),
                                new SqlParameter("@dtUpTime", this.dtUpTime)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 增加交付揭示
        /// </summary>
        /// <returns></returns>
        public bool Add(string strPUBJieShiGUID)
        {
            string strSql = "INSERT INTO [TAB_PUBJieShi_File] ([strPUBJieShiGUID] ,[strTitle] ,[strFileName] ,[strOrginName],[nFileSize] ,[dtUpTime] ) VALUES (@strPUBJieShiGUID ,@strTitle ,@strFileName ,@strOrginName, @nFileSize ,@dtUpTime)";
            SqlParameter[] sqlParams = {
                                new SqlParameter("@strPUBJieShiGUID", strPUBJieShiGUID),
                                new SqlParameter("@strTitle", this.strTitle),
                                new SqlParameter("@strFileName",this.strFileName),
                                new SqlParameter("@strOrginName",this.strOrginName),
                                new SqlParameter("@nFileSize", this.nFileSize),
                                new SqlParameter("@dtUpTime", this.dtUpTime)
                };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 更新发布揭示
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = "UPDATE [TAB_PUBJieShi_File] SET [strTitle] =@strTitle ,[strFileName] = @strFileName ,[strOrginName]=@strOrginName,[nFileSize] =@nFileSize ,[dtUpTime] = @dtUpTime WHERE strPUBJieShiGUID=@strPUBJieShiGUID";
            SqlParameter[] sqlParams = {
                                            new SqlParameter("@strTitle", this.strTitle),
                                            new SqlParameter("@strFileName",strFileName),
                                            new SqlParameter("@strOrginName",strOrginName),
                                            new SqlParameter("@nFileSize",nFileSize),
                                            new SqlParameter("@dtUpTime", this.dtUpTime),
                                            new SqlParameter("@strPUBJieShiGUID",this.strPUBJieShiGUID)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 删除交付揭示
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = "DELETE FROM [TAB_PUBJieShi_File] where [strPUBJieShiGUID]=@strPUBJieShiGUID";
            SqlParameter sqlParam = new SqlParameter("@strPUBJieShiGUID", this.strPUBJieShiGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }
        #endregion

        #region 扩展方法

        #region
        /// <summary>
        /// 获取所有发布揭示
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAll(int pageIndex, int pageCount)
        {
            string strSql = @"select top " + pageCount.ToString() + @" * from TAB_PUBJieShi_File "
       + " where strPUBJieShiGUID not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" strPUBJieShiGUID from TAB_PUBJieShi_File order by [dtUpTime] desc) order by [dtUpTime] desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        #endregion

        #region 获取交付揭示数量

        public static int GetCount()
        {
            string strSql = "select count(strPUBJieShiGUID) from [TAB_PUBJieShi_File]";
            return int.Parse(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql).ToString());
        }
        #endregion

        #region
        /// <summary>
        /// 删除交付揭示
        /// </summary>
        /// <param name="strTrainCategoryGUID"></param>
        /// <returns></returns>
        public static bool Delete(string strPUBJieShiGUID)
        {
            string strSql = "DELETE FROM [TAB_PUBJieShi_File] where [strPUBJieShiGUID]=@strPUBJieShiGUID";
            SqlParameter sqlParam = new SqlParameter("@strPUBJieShiGUID", strPUBJieShiGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }
        #endregion


        #region
        /// <summary>
        /// 删除交付揭示及对应关系
        /// </summary>
        public static bool DeleteJieShiAndRelation(string strPUBJieShiGUID)
        {
            string strSql = "DELETE FROM [TAB_PUBJieShi_File] where [strPUBJieShiGUID]=@strPUBJieShiGUID ";
            strSql += "DELETE FROM [TAB_PUBJieShi_PublishSection] WHERE [strPUBJieShiGUID]=@strPUBJieShiGUID";
            SqlParameter sqlParam = new SqlParameter("@strPUBJieShiGUID", strPUBJieShiGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }
        #endregion


        #endregion

    }
}
