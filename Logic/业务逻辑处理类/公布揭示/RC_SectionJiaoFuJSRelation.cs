using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace ThinkFreely.RunSafty
{
    /// <summary>
    /// 类名：DBRC_SectionJiaoFuJSRelation
    /// 描述：交付揭示文件和区段关系表数据库操作类
    /// </summary>
    public class DBRC_SectionJiaoFuJSRelation
    {
        #region 属性
        public int nid;
        public string strPUBJieShiGUID;
        public string strSectionGUID;
        #endregion

        #region 扩展方法


        /// <summary>
        /// 获取所有区段
        /// </summary>
        public static DataTable GetAll()
        {
            string strSql = "SELECT * FROM TAB_Base_Writecard_Section";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }


        /// <summary>
        /// 根据交付揭示ID获取所有区段对应关系
        /// </summary>
        public static DataTable GetAllByJsID(string strPUBJieShiGUID)
        {
            string strSql = "SELECT b.strSectionID,b.strSectionName FROM TAB_PUBJieShi_PublishSection as a "
                + " INNER JOIN TAB_Base_Writecard_Section as b ON a.strSectionGUID=b.strSectionID WHERE a.strPUBJieShiGUID=@strPUBJieShiGUID";
            SqlParameter sqlParam = new SqlParameter("@strPUBJieShiGUID", strPUBJieShiGUID);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam).Tables[0];
        }


        #region 插入或更新揭示区段关系
        public static bool InsertJsSectionRelation(string strPUBJieShiGUID, string strSectionGUID)
        {
            string strSql = "if (not exists(select * from TAB_PUBJieShi_PublishSection "
                + " where strPUBJieShiGUID=@strPUBJieShiGUID and strSectionGUID=@strSectionGUID)) "
                + " begin insert into TAB_PUBJieShi_PublishSection (strPUBJieShiGUID,strSectionGUID) values (@strPUBJieShiGUID,@strSectionGUID) end";
            SqlParameter[] sqlParams = { 
                                        new SqlParameter("@strPUBJieShiGUID",strPUBJieShiGUID),
                                        new SqlParameter("@strSectionGUID",strSectionGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion

        #region
        /// <summary>
        /// 删除区段对应关系
        /// </summary>
        public static bool DeleteRelation(string strPUBJieShiGUID, string strSectionGUID)
        {
            string strSql = "delete from [TAB_PUBJieShi_PublishSection] where  strPUBJieShiGUID=@strPUBJieShiGUID and strSectionGUID=@strSectionGUID";
            SqlParameter[] sqlParams = { 
                                        new SqlParameter("@strPUBJieShiGUID",strPUBJieShiGUID),
                                        new SqlParameter("@strSectionGUID",strSectionGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;

        }
        #endregion

        #region 删除揭示所有对应关系
        /// <summary>
        /// 删除揭示所有对应关系
        /// </summary>
        public static bool DeleteRelation(string strPUBJieShiGUID)
        {
            string strSql = "delete from [TAB_PUBJieShi_PublishSection] where  strPUBJieShiGUID=@strPUBJieShiGUID";
            SqlParameter[] sqlParams = { 
                                        new SqlParameter("@strPUBJieShiGUID",strPUBJieShiGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;

        }
        #endregion

        #endregion
    }
}
