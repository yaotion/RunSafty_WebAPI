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
    ///Site 的摘要说明
    /// </summary>
    public class Site
    {
        #region 属性
        public string strSiteGUID = "";
        public string strSiteNumber = "";
        public string strSiteName = "";
        public int nSiteEnable = 0;
        public string strSiteIP = "";
        public int nSiteJob = 0;
        public string strAreaGUID = "";
        public string strStationGUID = "";
        public string strStationName = "";
        public string strWorkShopGUID = "";
        public string strTMIS = "";
        /// <summary>
        /// 管理端IP
        /// </summary>
        public string strConfirmIP = string.Empty;
        #endregion

        #region 构造函数
        public Site()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public Site(string SiteGUID)
        {
            string strSql = "select top 1 * from VIEW_Base_Site where strSiteGUID = @SiteGUID";
            SqlParameter[] sqlParams = {
                                        new SqlParameter("SiteGUID",SiteGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strSiteGUID = dt.Rows[0]["strSiteGUID"].ToString();
                strSiteNumber = dt.Rows[0]["strSiteNumber"].ToString();
                strSiteName = dt.Rows[0]["strSiteName"].ToString();
                nSiteEnable = int.Parse(dt.Rows[0]["nSiteEnable"].ToString());
                strSiteIP = dt.Rows[0]["strSiteIP"].ToString();
                nSiteJob = int.Parse(dt.Rows[0]["nSiteJob"].ToString());
                strAreaGUID = dt.Rows[0]["strAreaGUID"].ToString();
                strStationGUID = dt.Rows[0]["strStationGUID"].ToString();
                strStationName = dt.Rows[0]["strStationName"].ToString();
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                strTMIS = dt.Rows[0]["strTMIS"].ToString(); 

            }
        }

        public static Site GetSiteByIP(string SiteIP)
        {
            Site resultSite = new Site();
            string strSql = "select top 1 * from TAB_Base_Site where strSiteIP = @SiteIP";
            SqlParameter[] sqlParams = {
                                        new SqlParameter("SiteIP",SiteIP)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {

                resultSite.strSiteGUID = dt.Rows[0]["strSiteGUID"].ToString();
                resultSite.strSiteNumber = dt.Rows[0]["strSiteNumber"].ToString();
                resultSite.strSiteName = dt.Rows[0]["strSiteName"].ToString();
                resultSite.nSiteEnable = int.Parse(dt.Rows[0]["nSiteEnable"].ToString());
                resultSite.strSiteIP = dt.Rows[0]["strSiteIP"].ToString();
                resultSite.nSiteJob = int.Parse(dt.Rows[0]["nSiteJob"].ToString());
                resultSite.strAreaGUID = dt.Rows[0]["strAreaGUID"].ToString();
                resultSite.strStationGUID = dt.Rows[0]["strStationGUID"].ToString();
                resultSite.strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                resultSite.strTMIS = dt.Rows[0]["strTMIS"].ToString();
            }
            return resultSite;
        }

        #endregion 构造函数


        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_Site (strSiteGUID,strSiteNumber,strSiteName,nSiteEnable,strSiteIP,nSiteJob,strStationGUID,strAreaGUID,strWorkShopGUID,strTMIS)  " +
                " values (@strSiteGUID,@strSiteNumber,@strSiteName,@nSiteEnable,@strSiteIP,@nSiteJob,@strStationGUID,@strAreaGUID,@strWorkShopGUID,@strTMIS);" +
                "insert into TAB_Base_Site_Limit(strSiteGUID,nJobID,nJobLimit)(select @strSiteGUID,@nSiteJob,nLimitID from TAB_System_Job_Limit)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSiteGUID",guid),
                                           new SqlParameter("strSiteNumber",strSiteNumber),
                                           new SqlParameter("strSiteName",strSiteName),
                                           new SqlParameter("nSiteEnable",nSiteEnable),
                                           new SqlParameter("strSiteIP",strSiteIP),
                                           new SqlParameter("nSiteJob",nSiteJob),
                                           new SqlParameter("strStationGUID",strStationGUID),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strAreaGUID",strAreaGUID),
                                           new SqlParameter("strTMIS",strTMIS)
                                           
                                       };
            bool bflag = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
            //更新管理端IP
            Base_Site_Additional.InsertOrUpdate(guid, strConfirmIP);
            return bflag;

        }
        public bool Update()
        {
            string strSql = @"update TAB_Base_Site set strSiteNumber=@strSiteNumber,nSiteEnable=@nSiteEnable,strSiteName=@strSiteName,strSiteIP=@strSiteIP,
                nSiteJob=@nSiteJob,strStationGUID=@strStationGUID,strAreaGUID=@strAreaGUID,strWorkShopGUID=@strWorkShopGUID,strTMIS=@strTMIS  where strSiteGUID=@strSiteGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSiteGUID",strSiteGUID),
                                           new SqlParameter("strSiteNumber",strSiteNumber),
                                           new SqlParameter("strSiteName",strSiteName),
                                           new SqlParameter("nSiteEnable",nSiteEnable),
                                           new SqlParameter("strSiteIP",strSiteIP),
                                           new SqlParameter("nSiteJob",nSiteJob),
                                           new SqlParameter("strStationGUID",strStationGUID),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                            new SqlParameter("strAreaGUID",strAreaGUID),
                                           new SqlParameter("strTMIS",strTMIS)
                                       };
            bool bflag = (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0);
            Base_Site_Additional.InsertOrUpdate(strSiteGUID, strConfirmIP);
            return bflag;
        }
        public static bool Delete(string SiteGUID)
        {
            string strSql = "delete TAB_Base_Site where strSiteGUID=@strSiteGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSiteGUID",SiteGUID)                                         
                                       };
            return (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0);
        }

        public static bool ExistSiteNumber(string SiteGUID, string SiteNumber)
        {
            string strSql = "select count(*) from TAB_Base_Site where 1= 1 ";
            if (SiteGUID != "")
            {
                strSql += " and strSiteGUID <> @strSiteGUID ";
            }

            if (SiteNumber != "")
            {
                strSql += " and strSiteNumber = @strSiteNumber ";
            }

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSiteGUID",SiteGUID),
                                           new SqlParameter("strSiteNumber",SiteNumber)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }

        public static bool ExistSiteName(string SiteGUID, string SiteName)
        {
            string strSql = "select count(*) from TAB_Base_Site where 1= 1 ";
            if (SiteGUID != "")
            {
                strSql += " and strSiteGUID <> @strSiteGUID ";
            }

            if (SiteName != "")
            {
                strSql += " and strSiteName = @strSiteName ";
            }

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSiteGUID",SiteGUID),
                                           new SqlParameter("strSiteName",SiteName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }

        public static bool ExistSiteIP(string SiteGUID, string SiteIP)
        {
            string strSql = "select count(*) from TAB_Base_Site where 1= 1 ";
            if (SiteGUID != "")
            {
                strSql += " and strSiteGUID <> @strSiteGUID ";
            }

            if (SiteIP != "")
            {
                strSql += " and strSiteIP = @strSiteIP ";
            }

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSiteGUID",SiteGUID),
                                           new SqlParameter("strSiteIP",SiteIP)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }

        public static bool EnableSite(string SiteGUID, int SiteEnable)
        {
            string strSql = "update  TAB_Base_Site set nSiteEnable = @SiteEnable where strSiteGUID = @SiteGUID";


            SqlParameter[] sqlParams = {
                                           new SqlParameter("SiteGUID",SiteGUID),
                                           new SqlParameter("SiteEnable",SiteEnable)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion 增删改


        #region 扩展方法
        public static DataTable GetSites(string SiteName, string SiteNumber, string SiteIP, int SiteEnable)
        {
            string strSql = "select * from VIEW_Base_Site where 1=1 ";
            if (SiteName != "")
            {
                strSql += " and strSiteName like @SiteName ";
            }

            if (SiteNumber != "")
            {
                strSql += " and strSiteNumber like @SiteName ";
            }

            if (SiteIP != "")
            {
                strSql += " and strSiteIP = @SiteIP ";
            }
            if (SiteEnable >= 0)
            {
                strSql += " and nSiteEnable = @SiteEnable ";
            }

            strSql += " order by strSiteNumber ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("SiteName",SiteName),
                                           new SqlParameter("SiteNumber",SiteNumber),
                                           new SqlParameter("SiteIP",SiteIP),
                                           new SqlParameter("SiteEnable",SiteEnable)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //获取站点管辖线路
        public static DataTable GetLineInSite(string SiteGUID)
        {
            string strSql = "select * from VIEW_Base_LineInSite where strSiteGUID=@strSiteGUID order by strLineName";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strSiteGUID",SiteGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //更新站点管辖线路信息
        public static void UpdateLineInSite(string SiteGUID, string LineGUIDs)
        {
            string strSql = "delete from TAB_Base_LineInSite where strSiteGUID = @strSiteGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strSiteGUID",SiteGUID)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            string[] jiaolus = LineGUIDs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < jiaolus.Length; i++)
            {
                strSql = "insert into TAB_Base_LineInSite (strSiteGUID,strLineGUID) values (@strSiteGUID,@strLineGUID)";
                SqlParameter[] sqlParamsSub = {
                                           new SqlParameter("@strSiteGUID",SiteGUID),
                                           new SqlParameter("@strLineGUID",jiaolus[i])
                                       };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSub);
            }
        }

        //获取模块管理权限
        public static DataTable GetSiteLimit(string SiteGUID, string jobid)
        {
            string strSql = "select * from TAB_Base_Site_Limit where strSiteGUID=@strSiteGUID and nJobID=@nJobID";
            SqlParameter[] sqlParams = {
                                            new SqlParameter("@strSiteGUID",SiteGUID),
                                           new SqlParameter("@nJobID",jobid)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //更新模块管理权限
        public static bool AddSiteLimit(string SiteGUID, string jobid, string joblimit)
        {
            string strSql = "delete TAB_Base_Site_Limit where strSiteGUID = @strSiteGUID and nJobID=@nJobID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strSiteGUID",SiteGUID),
                                           new SqlParameter("@nJobID",jobid)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            string[] joblimits = joblimit.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int SucessCount = 0;
            for (int i = 0; i < joblimits.Length; i++)
            {
                strSql = "insert into TAB_Base_Site_Limit (strSiteGUID,nJobID,nJobLimit) values (@strSiteGUID,@nJobID,@nJobLimit)";
                SqlParameter[] sqlParamsSub = {
                                           new SqlParameter("@strSiteGUID",SiteGUID),
                                           new SqlParameter("@nJobLimit",joblimits[i]),
                                           new SqlParameter("@nJobID",jobid)
                                       };
                SucessCount += SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSub);
            }
            return SucessCount > 0;
        }
        #endregion 扩展方法
    }
}
