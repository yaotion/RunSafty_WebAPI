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
    ///WorkShop功能：提供车间信息增删改查
    /// </summary>
    public class WorkShop
    {
        #region 属性
        public string strWorkShopGUID;
        public string strAreaGUID;
        public string strAreaName;
        public string nid;
        public string strWorkShopName;
        public string strWorkShopNumber;
        #endregion 属性

        #region 构造函数
        public WorkShop()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public WorkShop(string strid)
        {
            string strSql = "select * from VIEW_Org_WorkShop where strWorkShopGUID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                strAreaGUID = dt.Rows[0]["strAreaGUID"].ToString();
                strWorkShopName = dt.Rows[0]["strWorkShopName"].ToString();
                strAreaName = dt.Rows[0]["strAreaName"].ToString();
                strWorkShopNumber = dt.Rows[0]["strWorkShopNumber"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Org_WorkShop (strWorkShopGUID,strAreaGUID,strWorkShopName,strWorkShopNumber) values (@strWorkShopGUID,@strAreaGUID,@strWorkShopName,@strWorkShopNumber)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",guid),
                                           new SqlParameter("strAreaGUID",strAreaGUID),
                                           new SqlParameter("strWorkShopName",strWorkShopName),
                                           new SqlParameter("strWorkShopNumber",strWorkShopNumber)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Org_WorkShop set strAreaGUID = @strAreaGUID,strWorkShopName=@strWorkShopName,strWorkShopNumber=@strWorkShopNumber where strWorkShopGUID=@strWorkShopGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strAreaGUID",strAreaGUID),
                                           new SqlParameter("strWorkShopName",strWorkShopName),
                                           new SqlParameter("strWorkShopNumber",strWorkShopNumber)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Org_WorkShop where strWorkShopGUID=@strWorkShopGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string AreaName)
        {
            string strSql = "select count(*) from TAB_Org_WorkShop where strWorkShopName=@strWorkShopName ";
            if (strid != "")
            {
                strSql += " and strWorkShopGUID <> @strWorkShopGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",strid),
                                           new SqlParameter("strWorkShopName",AreaName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }


        public static DataTable VIEW_Base_Site(string name)
        {
            string strSql = "select * from VIEW_Base_Site";
            if (name != "")
            {
                strSql += " and strWorkShopName like @name ";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("name","%" +name+ "%")
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }


        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllWorkShop(string name)
        {
            string strSql = "select * from TAB_Org_WorkShop";
            if (name != "")
            {
                strSql += " and strWorkShopName like @name ";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("name","%" +name+ "%")
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //public static DataTable GetAllAreasDic(string DefaultName)
        //{
        //    DataTable dtResult = GetAllAreas("");
        //    DataRow dr = dtResult.NewRow();
        //    dr["strGUID"] = "";
        //    dr["strAreaGUID"] = DefaultName;
        //    dtResult.Rows.InsertAt(dr, 0);
        //    return dtResult;
        //}

        #endregion
    }
}
