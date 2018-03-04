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
    ///System_SiteConfig功能：
    /// </summary>
    public class System_SiteConfig
    {
        #region 属性
        public int nID;
        public string strSiteNumber;
        public string strName;
        public string strValue;
        #endregion 属性

        #region 构造函数
        public System_SiteConfig()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public System_SiteConfig(string id)
        {
            string strSql = "select * from TAB_System_SiteConfig where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",PageBase.static_ext_int(id))
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static System_SiteConfig SetValue(System_SiteConfig PlanParam, DataRow dr)
        {
            if (dr!=null)
            {
                PlanParam.nID = PageBase.static_ext_int(dr["nID"]);
                PlanParam.strSiteNumber = dr["strSiteNumber"].ToString();
                PlanParam.strName = dr["strName"].ToString();
                PlanParam.strValue = dr["strValue"].ToString();
            }
            return PlanParam;
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_System_SiteConfig (strSiteNumber,strName,strValue) values (@strSiteNumber,@strName,@strValue)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSiteNumber",strSiteNumber),
                                           new SqlParameter("strName",strName),
                                           new SqlParameter("strValue",strValue)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_System_SiteConfig set strSiteNumber = @strSiteNumber,strName=@strName,strValue=@strValue where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",nID),
                                           new SqlParameter("strSiteNumber",strSiteNumber),
                                           new SqlParameter("strName",strName),
                                           new SqlParameter("strValue",strValue)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_System_SiteConfig where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",PageBase.static_ext_int(strid))
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string number,string id)
        {
            string strSql = "select count(*) from TAB_System_SiteConfig where strSiteNumber=@strSiteNumber ";
            if (id != "")
            {
                strSql += " and nID = @id ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSiteNumber",number),
                                           new SqlParameter("id",PageBase.static_ext_int(id))
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法

        ///// <summary>
        ///// 返回list对象
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public static List<System_SiteConfig> ListValue(DataTable dt)
        //{
        //    List<System_SiteConfig> planparamList = new List<System_SiteConfig>();
        //    System_SiteConfig planparam;
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            planparam = new System_SiteConfig();
        //            planparam = SetValue(planparam, dt.Rows[i]);
        //            planparamList.Add(planparam);
        //        }
        //    }
        //    return planparamList;
        //}
        #endregion
    }
}
