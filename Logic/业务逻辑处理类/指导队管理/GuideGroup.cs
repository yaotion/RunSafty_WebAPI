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
    ///GuideGroup功能：提供指导队信息增删改查
    /// </summary>
    public class GuideGroup
    {
        #region 属性
        public string strGuideGroupGUID;
        public string strWorkShopGUID;
        public string strGuideGroupName;
        public string strWorkShopName;
        #endregion 属性

        #region 构造函数
        public GuideGroup()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public GuideGroup(string strid)
        {
            string strSql = "select * from VIEW_Org_GuidGroup where strGuideGroupGUID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strGuideGroupGUID = dt.Rows[0]["strGuideGroupGUID"].ToString();
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                strGuideGroupName = dt.Rows[0]["strGuideGroupName"].ToString();
                strWorkShopName = dt.Rows[0]["strWorkShopName"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Org_GuideGroup (strGuideGroupGUID,strWorkShopGUID,strGuideGroupName) values (@strGuideGroupGUID,@strWorkShopGUID,@strGuideGroupName)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGuideGroupGUID",guid),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strGuideGroupName",strGuideGroupName) 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Org_GuideGroup set strWorkShopGUID = @strWorkShopGUID,strGuideGroupName=@strGuideGroupName where strGuideGroupGUID=@strGuideGroupGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGuideGroupGUID",strGuideGroupGUID),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strGuideGroupName",strGuideGroupName)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Org_GuideGroup where strGuideGroupGUID=@strGuideGroupGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGuideGroupGUID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string AreaName,string workshop)
        {
            string strSql = "select count(*) from TAB_Org_GuideGroup where strGuideGroupName=@strGuideGroupName and strWorkShopGUID=@strWorkShopGUID ";
            if (strid != "")
            {
                strSql += " and strGuideGroupGUID <> @strGuideGroupGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGuideGroupGUID",strid),
                                           new SqlParameter("strWorkShopGUID",workshop),
                                           new SqlParameter("strGuideGroupName",AreaName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllGuideGroup(string id)
        {
            string strSql = "select * from TAB_Org_GuideGroup where 1=1";
            if (id != "")
            {
                strSql += " and strWorkShopGUID = @id ";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static DataTable GetGuideGroupByWorkShopGUID(string strWorkShopGUID)
        {
            string strSql = "select * from TAB_Org_GuideGroup where 1=1";
            if (strWorkShopGUID != "")
            {
                strSql += " and strWorkShopGUID = @strWorkShopGUID";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID)
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
