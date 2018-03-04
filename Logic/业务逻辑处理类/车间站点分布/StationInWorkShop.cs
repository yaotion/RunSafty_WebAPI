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
    ///TrainZone 功能：行车区段数据增删查
    /// </summary>
    public class StationInWorkShop
    {
        #region 属性
        public string strRecordGUID = "";
        public string strWorkShopGUID = "";
        public string strStationGUID = "";
        public string nTMIS = "";
        public int nStationIndex =0;
        public int nIsLocal = 0;
        public int nIsGoBack = 0;
        public string strStationName = "";
        #endregion 属性

        #region 构造函数
        public StationInWorkShop()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public StationInWorkShop(string strid)
        {
            string strSql = "select * from VIEW_Base_StationInWorkShop where strRecordGUID=@strRecordGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strRecordGUID",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strRecordGUID = dt.Rows[0]["strRecordGUID"].ToString();
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                strStationGUID = dt.Rows[0]["strStationGUID"].ToString();
                nTMIS = dt.Rows[0]["nTMIS"].ToString();
                nStationIndex = PageBase.static_ext_int(dt.Rows[0]["nStationIndex"]);
                nIsLocal = PageBase.static_ext_int(dt.Rows[0]["nIsLocal"]);
                nIsGoBack = PageBase.static_ext_int(dt.Rows[0]["nIsGoBack"]);
                strStationName = dt.Rows[0]["strStationName"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = @"insert into TAB_Base_StationInWorkShop (strRecordGUID,strWorkShopGUID,strStationGUID,nTMIS,nStationIndex,nIsLocal,nIsGoBack,strStationName)
                  (select @strRecordGUID,@strWorkShopGUID,@strStationGUID,@nTMIS,(case when  (max(nStationIndex)) is null then 1 else Max(nStationIndex) + 1 end),@nIsLocal,@nIsGoBack,@strStationName from TAB_Base_StationInWorkShop)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strRecordGUID",guid),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strStationGUID",strStationGUID),
                                           new SqlParameter("nTMIS",nTMIS),
                                           new SqlParameter("nIsLocal",nIsLocal),
                                           new SqlParameter("nIsGoBack",nIsGoBack),
                                           new SqlParameter("strStationName",strStationName)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;

        }
        public bool Update()
        {
            string strSql = "update TAB_Base_StationInWorkShop set strWorkShopGUID = @strWorkShopGUID,strStationGUID = @strStationGUID,nTMIS = @nTMIS,nIsLocal = @nIsLocal,nIsGoBack = @nIsGoBack,strStationName = @strStationName where strRecordGUID=@strRecordGUID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strStationGUID",strStationGUID),
                                           new SqlParameter("nTMIS",nTMIS),
                                           new SqlParameter("nIsLocal",nIsLocal),
                                           new SqlParameter("nIsGoBack",nIsGoBack),
                                           new SqlParameter("strStationName",strStationName),
                                           new SqlParameter("strRecordGUID",strRecordGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool UpdateSortid()
        {
            string strSql = "update TAB_Base_StationInWorkShop set nStationIndex=@nStationIndex where strRecordGUID=@strRecordGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nStationIndex",nStationIndex),
                                           new SqlParameter("strRecordGUID",strRecordGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string StationGUID)
        {
            string strSql = "delete TAB_Base_StationInWorkShop where strRecordGUID=@strRecordGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strRecordGUID",StationGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string workshopid,string stationid)
        {
            string strSql = "select count(*) from TAB_Base_StationInWorkShop where 1=1 ";
            strSql += strid == "" ? "" : " and strRecordGUID <> @strRecordGUID";
            strSql += workshopid == "" ? "" : " and strWorkShopGUID = @strWorkShopGUID";
            strSql += stationid == "" ? "" : " and strStationGUID = @strStationGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strRecordGUID",strid),
                                           new SqlParameter("strWorkShopGUID",workshopid),
                                           new SqlParameter("strStationGUID",stationid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改


        #region 扩展方法
        public static DataTable GetAllStationInWorkShop(string workshopid)
        {
            string strSql = "select * from VIEW_Base_StationInWorkShop where 1=1";
            if (workshopid != "")
            {
                strSql += " and strWorkShopGUID = @strWorkShopGUID ";
            }
            strSql += " order by nStationIndex ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",workshopid)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //public static DataTable GetAllStationDic(string DefaultName)
        //{
        //    DataTable dtResult = GetAllStation("", "");
        //    DataRow dr = dtResult.NewRow();
        //    dr["strStationGUID"] = "";
        //    dr["strStationName"] = DefaultName;
        //    dtResult.Rows.InsertAt(dr, 0);
        //    return dtResult;
        //}
        #endregion
    }

}
