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
    ///ZFQJ功能：提供折返区间增删改查
    /// </summary>
    public class ZFStation
    {
        #region 属性
        public string strTrainJiaoluGUID;
        public string strStationGUID;
        public int nSortid;
        public int nid;
        #endregion 属性

        #region 构造函数
        public ZFStation()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public ZFStation(string id)
        {
            string strSql = "select * from TAB_Base_StationInTrainJiaolu where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nid",id)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strTrainJiaoluGUID = dt.Rows[0]["strTrainJiaoluGUID"].ToString();
                strStationGUID = dt.Rows[0]["strStationGUID"].ToString();
                nSortid =PageBase.static_ext_int(dt.Rows[0]["nSortid"]);
                nid = PageBase.static_ext_int(dt.Rows[0]["nid"]);
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_Base_StationInTrainJiaolu (strTrainJiaoluGUID,strStationGUID) values (@strTrainJiaoluGUID,@strStationGUID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("strStationGUID",strStationGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_StationInTrainJiaolu set strStationGUID=@strStationGUID where nid=@nid";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("strStationGUID",strStationGUID),
                                           new SqlParameter("nSortid",nSortid),
                                           new SqlParameter("nid",nid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Base_StationInTrainJiaolu where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nid",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string nid, string strStationGUID, string strTrainJiaoluGUID)
        {
            string strSql = "select count(*) from TAB_Base_StationInTrainJiaolu where 1=1";
            strSql += nid != "" ? " and nid<>@nid" : "";
            strSql += strStationGUID != "" ? " and strStationGUID=@strStationGUID" : "";
            strSql += strTrainJiaoluGUID != "" ? " and strTrainJiaoluGUID=@strTrainJiaoluGUID" : "";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationGUID",strStationGUID),
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("nid",nid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        public bool UpdateSortid()
        {
            string strSql = "update TAB_Base_StationInTrainJiaolu set nSortid=@nSortid where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nSortid",nSortid),
                                           new SqlParameter("nid",nid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion 增删改
        #region 扩展方法
        public static DataTable GetAllZFStation(string id)
        {
            string strCondition = "";
            if (id != "")
            {
                strCondition += " and strTrainJiaoluGUID = @strTrainJiaoluGUID ";
            }
            string strSql = "select * from VIEW_Base_StationInTrainJiaolu where 1=1 " + strCondition + @" order by nSortid asc";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",id)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static int GetAllZFStationCount(string id)
        {
            string strSql = "select count(*) from VIEW_Base_StationInTrainJiaolu";
            if (id != "")
            {
                strSql += " where strTrainJiaoluGUID = @strTrainJiaoluGUID ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",id)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams));
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
