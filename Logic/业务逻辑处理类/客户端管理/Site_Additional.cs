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
    ///类名：Base_Site_Additional
    ///描述：客户端附加信息数据库操作类
    /// </summary>
    public class Base_Site_Additional
    {
        #region 属性
        public string strSiteGUID;
        public string strConfirmIP;
        #endregion

        #region 构造函数

        public Base_Site_Additional()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public Base_Site_Additional(string strSiteGUID)
        {
            string strSql = "select * from [TAB_Base_Site_Additional] where strSiteGUID=@strSiteGUID";
            SqlParameter sqlParam = new SqlParameter("@strSiteGUID", strSiteGUID);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.strSiteGUID = PageBase.static_ext_string(dt.Rows[0]["strSiteGUID"]);
                this.strConfirmIP = PageBase.static_ext_string(dt.Rows[0]["strConfirmIP"]);
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 插入或更新管理端IP
        /// </summary>
        public static bool InsertOrUpdate(string strSiteGUID, string strConfirmIP)
        {
            string strSql = "if (Exists(select * from [TAB_Base_Site_Additional] where strSiteGUID=@strSiteGUID)) begin update [TAB_Base_Site_Additional] set strConfirmIP=@strConfirmIP where strSiteGUID=@strSiteGUID end else begin insert into [TAB_Base_Site_Additional] (strSiteGUID,strConfirmIP) values (@strSiteGUID,@strConfirmIP) end;";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strSiteGUID", strSiteGUID),
                                            new SqlParameter("@strConfirmIP", strConfirmIP)
             };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }


        /// <summary>
        /// 删除对应客户端的管理端IP记录
        /// </summary>
        public static bool Delete(string strSiteGUID)
        {
            string strSql = "delete from [TAB_Base_Site_Additional] where strSiteGUID=@strSiteGUID";
            SqlParameter sqlParam = new SqlParameter("@strSiteGUID", strSiteGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;

        }
        #endregion


    }
}