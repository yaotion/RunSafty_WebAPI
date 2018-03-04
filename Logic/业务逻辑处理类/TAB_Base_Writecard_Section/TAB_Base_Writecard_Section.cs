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

namespace TF.RunSafty.Logic
{
    /// <summary>
    ///类名：TAB_Base_Writecard_Section
    /// 描述：TAB_Base_Writecard_Section数据库操作类
    /// </summary>
    public class TAB_Base_Writecard_Section
    {
        #region 属性
        public int nid;
        public string strJWDNumber;
        public string strSectionName;
        public string strSectionID;
        #endregion

        #region 构造函数
        public TAB_Base_Writecard_Section()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public TAB_Base_Writecard_Section(string nid)
        {
            string strSql = "select * from [TAB_Base_Writecard_Section] where nid=@nid";
            SqlParameter sqlParam = new SqlParameter("@nid", nid);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.nid = PageBase.static_ext_int(dt.Rows[0]["nid"]);
                this.strJWDNumber = PageBase.static_ext_string(dt.Rows[0]["strJWDNumber"]);
                this.strSectionID = PageBase.static_ext_string(dt.Rows[0]["strSectionID"]);
                this.strSectionName = PageBase.static_ext_string(dt.Rows[0]["strSectionName"]);
            }
        }
        #endregion

        #region 增删改
        public bool Delete()
        {
            string strSql = "delete from [TAB_Base_Writecard_Section] where nid=@nid";
            SqlParameter sqlParam = new SqlParameter("@nid", nid);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }
        #endregion

        #region 扩展方法

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public static bool Delete(string nid)
        {
            string strSql = "delete from [TAB_Base_Writecard_Section] where nid=@nid";
            SqlParameter sqlParam = new SqlParameter("@nid", nid);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }

        /// <summary>
        /// 插入或更新
        /// </summary>
        public static bool InsertOrUpdate(TAB_Base_Writecard_Section tbws)
        {
            string strSql = "if (Exists(select * from [TAB_Base_Writecard_Section] where nid=@nid)) begin update [TAB_Base_Writecard_Section] set strJWDNumber=@strJWDNumber,strSectionName=@strSectionName,strSectionID=@strSectionID where nid=@nid end else begin insert into [TAB_Base_Writecard_Section] (strJWDNumber, strSectionName, strSectionID) values (@strJWDNumber,@strSectionName,@strSectionID) end;";
            SqlParameter[] sqlParams ={
                                                new SqlParameter("@nid",tbws.nid),
                                                new SqlParameter("@strJWDNumber",tbws.strJWDNumber),
                                                new SqlParameter("@strSectionName",tbws.strSectionName),
                                                new SqlParameter("@strSectionID",tbws.strSectionID)       
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 获取所有区段信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAll()
        {
            string strSql = "select * from [TAB_Base_Writecard_Section]";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        #endregion
    }
}