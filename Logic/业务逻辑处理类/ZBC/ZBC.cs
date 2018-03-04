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
    public class ZBC
    {
        #region 属性
        public string UnitId;
        public string Name;
        public int Status;
        public string CreateDate;
        #endregion 属性

        #region 构造函数
        public ZBC()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public ZBC(string UnitId)
        {
            string strSql = "select * from lsDicApanageInfo where UnitId=@UnitId and status=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("UnitId",UnitId)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                UnitId = dt.Rows[0]["UnitId"].ToString();
                Name = dt.Rows[0]["Name"].ToString();
                Status = PageBase.static_ext_int(dt.Rows[0]["Status"].ToString());
                CreateDate = dt.Rows[0]["CreateDate"].ToString();
            }
        }
        #endregion 构造函数

        #region 扩展方法
        public static DataTable GetAllZBC(int uid)
        {
            string strSql = "select * from lsDicApanageInfo where 1=1 and status=1 ";
            if (uid != 0)
            {
                strSql += " and UnitId = @uid ";
            }
            strSql += " order by Id ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("uid",uid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static string GetZBCWorkShopGUID(string apanageid)
        {
            string strSql = "select * from lsDicApanageInfo where Id=@apanageid  and status=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("apanageid",apanageid)
                                       };
            return SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).ToString();
        }
        #endregion
    }
}
