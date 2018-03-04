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
    ///ChuQinDiDian功能：提供出勤地点信息增删改查
    /// </summary>
    public class ChuQinDiDian
    {
        #region 属性
        public string strStationName;
        public string strWorkShopGUID;
        public string strStationGUID;
        public string strWorkShopName;
        public string nLocalRest;
        public string nLocalPre;
        public string nOutRest;
        public string nOutPre;
        public string nZJPre;
        public string nNightReset;
        public string bIsRest;
        public DateTime? dtRestTime;
        public string nContinueHours;
        public DateTime? dtCallTime;
        public string bRuKuFanPai;
        public string bLocalChaoLao;
        public string bOutChaoLao;
        public string strGUID;
        #endregion 属性

        #region 构造函数
        public ChuQinDiDian()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public ChuQinDiDian(string strid)
        {
            string strSql = "select * from VIEW_Base_ChuQinDiDian where strGUID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strStationName = dt.Rows[0]["strStationName"].ToString();
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                strStationGUID = dt.Rows[0]["strStationGUID"].ToString();
                strWorkShopName = dt.Rows[0]["strWorkShopName"].ToString();
                nLocalRest = dt.Rows[0]["nLocalRest"].ToString();
                nLocalPre = dt.Rows[0]["nLocalPre"].ToString();
                nOutRest = dt.Rows[0]["nOutRest"].ToString();
                nOutPre = dt.Rows[0]["nOutPre"].ToString();
                nZJPre = dt.Rows[0]["nZJPre"].ToString();
                nNightReset = dt.Rows[0]["nNightReset"].ToString();
                bIsRest = dt.Rows[0]["bIsRest"].ToString();
                if (dt.Rows[0]["dtRestTime"] is DBNull==false)
                {
                    dtRestTime = Convert.ToDateTime(dt.Rows[0]["dtRestTime"]);
                }
                else {
                    dtRestTime = null;
                }
                nContinueHours = dt.Rows[0]["nContinueHours"].ToString();
                if (dt.Rows[0]["dtCallTime"] is DBNull == false)
                {
                    dtCallTime = Convert.ToDateTime(dt.Rows[0]["dtCallTime"]);
                }
                else
                {
                    dtCallTime = null;
                }
                bRuKuFanPai = dt.Rows[0]["bRuKuFanPai"].ToString();
                bLocalChaoLao = dt.Rows[0]["bLocalChaoLao"].ToString();
                bOutChaoLao = dt.Rows[0]["bOutChaoLao"].ToString();
                strGUID = dt.Rows[0]["strGUID"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_ChuQinDiDian (strGUID,strStationGUID,strWorkShopGUID,nLocalRest,nLocalPre,nOutRest,nOutPre,nZJPre,nNightReset,bIsRest,dtRestTime,dtCallTime,nContinueHours,bRuKuFanPai,bLocalChaoLao,bOutChaoLao) values (@strGUID,@strStationGUID,@strWorkShopGUID,@nLocalRest,@nLocalPre,@nOutRest,@nOutPre,@nZJPre,@nNightReset,@bIsRest,@dtRestTime,@dtCallTime,@nContinueHours,@bRuKuFanPai,@bLocalChaoLao,@bOutChaoLao)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",guid),
                                           new SqlParameter("strStationGUID",strStationGUID),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("nLocalRest",nLocalRest),
                                           new SqlParameter("nLocalPre",nLocalPre),
                                           new SqlParameter("nOutRest",nOutRest),
                                           new SqlParameter("nOutPre",nOutPre),
                                           new SqlParameter("nZJPre",nZJPre),
                                           new SqlParameter("nNightReset",nNightReset),
                                           new SqlParameter("bIsRest",bIsRest),
                                           new SqlParameter("dtRestTime",dtRestTime),
                                           new SqlParameter("dtCallTime",dtCallTime),
                                           new SqlParameter("nContinueHours",nContinueHours),
                                           new SqlParameter("bRuKuFanPai",bRuKuFanPai),
                                           new SqlParameter("bLocalChaoLao",bLocalChaoLao),
                                           new SqlParameter("bOutChaoLao",bOutChaoLao)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_ChuQinDiDian set strStationGUID=@strStationGUID,strWorkShopGUID=@strWorkShopGUID,nLocalRest=@nLocalRest,nLocalPre=@nLocalPre,nOutRest=@nOutRest,nOutPre=@nOutPre,nZJPre=@nZJPre,nNightReset=@nNightReset,bIsRest=@bIsRest,dtRestTime=@dtRestTime,dtCallTime=@dtCallTime,nContinueHours=@nContinueHours,bRuKuFanPai=@bRuKuFanPai,bLocalChaoLao=@bLocalChaoLao,bOutChaoLao=@bOutChaoLao where strGUID=@strGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",strGUID),
                                           new SqlParameter("strStationGUID",strStationGUID),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("nLocalRest",nLocalRest),
                                           new SqlParameter("nLocalPre",nLocalPre),
                                           new SqlParameter("nOutRest",nOutRest),
                                           new SqlParameter("nOutPre",nOutPre),
                                           new SqlParameter("nZJPre",nZJPre),
                                           new SqlParameter("nNightReset",nNightReset),
                                           new SqlParameter("bIsRest",bIsRest),
                                           new SqlParameter("dtRestTime",dtRestTime),
                                           new SqlParameter("dtCallTime",dtCallTime),
                                           new SqlParameter("nContinueHours",nContinueHours),
                                           new SqlParameter("bRuKuFanPai",bRuKuFanPai),
                                           new SqlParameter("bLocalChaoLao",bLocalChaoLao),
                                           new SqlParameter("bOutChaoLao",bOutChaoLao)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Base_ChuQinDiDian where strGUID=@strGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string Name)
        {
            string strSql = "select count(*) from TAB_Base_ChuQinDiDian where strStationGUID=@strStationGUID ";
            if (strid != "")
            {
                strSql += " and strGUID <> @strGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",strid),
                                           new SqlParameter("strStationGUID",Name)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllWorkShop(string name)
        {
            string strSql = "select * from VIEW_Base_ChuQinDiDian";
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
