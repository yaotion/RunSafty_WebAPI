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
    public class JiCheAssessConfig
    {
        #region 属性
        public string strID;
        public int nApanageID;
        public int nUnitID;
        public int nIsEnable;
        public int nTqTianShu;
        #endregion 属性

        #region 构造函数
        public JiCheAssessConfig()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public JiCheAssessConfig(int unitid,int apanageid)
        {
            string strSql = "select * from TAB_JiCheAssessConfig where nUnitID=@nUnitID and nApanageID=@nApanageID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nUnitID",unitid),
                                           new SqlParameter("nApanageID",apanageid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static JiCheAssessConfig SetValue(JiCheAssessConfig jcassessconfig, DataRow dr)
        {
            if (dr!=null)
            {
                jcassessconfig.strID = dr["strID"].ToString();
                jcassessconfig.nUnitID = PageBase.static_ext_int(dr["nUnitID"]);
                jcassessconfig.nApanageID = PageBase.static_ext_int(dr["nApanageID"]);
                jcassessconfig.nIsEnable = dr["nIsEnable"].ToString()=="True"?1:0;
                jcassessconfig.nTqTianShu = PageBase.static_ext_int(dr["nTqTianShu"]);
            }
            return jcassessconfig;
        }
 
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_JiCheAssessConfig (nUnitID,nApanageID,nIsEnable,nTqTianShu) values (@nUnitID,@nApanageID,@nIsEnable,@nTqTianShu)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nUnitID",nUnitID),
                                           new SqlParameter("nApanageID",nApanageID),
                                           new SqlParameter("nIsEnable",nIsEnable),
                                           new SqlParameter("nTqTianShu",nTqTianShu)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_JiCheAssessConfig set nIsEnable=@nIsEnable,nTqTianShu=@nTqTianShu where nUnitID=@nUnitID and nApanageID=@nApanageID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nUnitID",nUnitID),
                                           new SqlParameter("nApanageID",nApanageID),
                                           new SqlParameter("nIsEnable",nIsEnable),
                                           new SqlParameter("nTqTianShu",nTqTianShu)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_JiCheAssessConfig where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int unitid, int apanageid)
        {
            string strSql = "select count(*) from TAB_JiCheAssessConfig where nUnitID=@nUnitID and nApanageID=@nApanageID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nUnitID",unitid),
                                           new SqlParameter("nApanageID",apanageid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="jcid"></param>
        ///// <returns></returns>
        //public static DataTable GetAllJiCheAssessConfigDT(string jcid)
        //{
        //    string strSql = "select * from TAB_JiCheAssessConfig where nUnitID=@nUnitID";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nUnitID",jcid)
        //                               };
        //    return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        //}

        //public static List<JiCheAssessConfig> GetAllJiCheAssessConfigList(string jcid)
        //{
        //    string strSql = "select * from TAB_JiCheAssessConfig where nUnitID=@nUnitID";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nUnitID",jcid)
        //                               };
        //    return ListValue(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0]);
        //}

        ///// <summary>
        ///// 返回list对象
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public static List<JiCheAssessConfig> ListValue(DataTable dt)
        //{
        //    List<JiCheAssessConfig> bearingList = new List<JiCheAssessConfig>();
        //    JiCheAssessConfig bearing;
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            bearing = new JiCheAssessConfig();
        //            bearing.nUnitID = PageBase.static_ext_int(dt.Rows[i]["nUnitID"]);
        //            bearing.cdatetime = PageBase.static_ext_date(dt.Rows[i]["cdatetime"]);
        //            bearing.nApanageID = PageBase.static_ext_int(dt.Rows[i]["nApanageID"]);
        //            bearing.nIsEnable = PageBase.static_ext_int(dt.Rows[i]["nIsEnable"]);
        //            bearing.alarmtype = dt.Rows[i]["alarmtype"].ToString();
        //            bearingList.Add(bearing);
        //        }
        //    }
        //    return bearingList;
        //}
        #endregion
    }
}
