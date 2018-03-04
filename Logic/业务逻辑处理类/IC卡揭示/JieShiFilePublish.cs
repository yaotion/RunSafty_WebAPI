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
using System.Collections.Generic;

namespace TF.RunSafty.Logic.StudyData
{

    //////////////////////////////////////////////////////////////////
    ///JieShiFilePbulish IC卡管理揭示文件发布管理类
    //////////////////////////////////////////////////////////////////
    public class DBJieShiFilePublish
    {

        //类型：数据改变类型

        #region public

        /// <summary>功能： 获得所有区段揭示文件信息 </summary>
        public DataTable GetAllSectionJieShiInfo()
        {
            string strSql = @"select * from TAB_ZZJieShi_File right join TAB_Base_Writecard_Section on strSectionGUID=strSectionID order by dtCreateTime desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /*
                /// <summary>功能：判断某个区段是否存在</summary>
                public bool Exist(string strSectionName)
                {
                    string strSql = "select count(*) from tabSectionInfo where strSectionName = @strSectionName";
                    SqlParameter[] sqlParameter = {                                              
                                                      new SqlParameter("strSectionName",strSectionName),
                                                  };
                    return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameter)) > 0;

                }

                /// <summary>功能：添加一个新的区段</summary>
                public bool Add(string strSectionName)
                {
                    string strSql = "Insert into tabSectionInfo(strId, strSectionName) values(@strID, @strSectionName)";

                    string guid = Guid.NewGuid().ToString();

                    SqlParameter[] sqlParameter = {
                                                      new SqlParameter("strid",guid),
                                                      new SqlParameter("strSectionName",strSectionName)
                                                  };

                    return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameter) > 0;
                }

        */
        /// <summary>
        /// 只删除区段文件
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public bool Delete(string strID)
        {
            string strSql = "delete from [TAB_ZZJieShi_File] where strSectionGUID=@strID";
            SqlParameter sqlparam = new SqlParameter("@strID", strID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql,sqlparam) > 0;
        }

        /// <summary>功能：设定某个区段的揭示文件信息</summary>
        public bool SetSectionJieShiFile(string strSectionGUID,string strOrginFileName, string strJieShiFileName)
        {
            string strSql = "";
            if (strSectionGUID == string.Empty)
            {
                return false;
            }
            string strSqlDeleteOldFile = "delete from [TAB_ZZJieShi_File] where strSectionGUID=@strSectionGUID";

            strSql += "insert into TAB_ZZJieShi_File (strJieShiGUID,strSectionGUID,strJieShiFile,dtCreateTime,strOrginName) values(@strJieShiGUID,@strSectionGUID,@strJieShiFile,@dtCreateTime,@strOrginName)";

            string strGUID = Guid.NewGuid().ToString();
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("strJieShiGUID",strGUID),
                                              new SqlParameter("strSectionGUID",strSectionGUID),
                                              new SqlParameter("strJieShiFile",strJieShiFileName),
                                              new SqlParameter("dtCreateTime",DateTime.Now),
                                              new SqlParameter("strOrginName",strOrginFileName)                                              
                                              
                                          };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSqlDeleteOldFile, sqlParameter);

            if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameter) == 0)
                return false;
            return true;
        }
        #endregion

        #region proteced


        /// <summary>功能:根据区段ID获得对应的文件ID/// </summary>
        protected string GetSectionJieShiFileID(string strSectionID)
        {
            string strSql = "select top 1 * from tabJieShiFile where strSectionID = @strSectionID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strSectionID",strSectionID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0]["strID"].ToString();
            }
            else
            {
                return "";
            };

        }
        #endregion

    }
}
