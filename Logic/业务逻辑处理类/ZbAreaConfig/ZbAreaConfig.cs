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
    public class ZbAreaConfig
    {
        #region 属性
        public string strID;
        public int UnitID;
        public int ApanageID;
        public int TrackID;
        #endregion 属性

        #region 构造函数
        public ZbAreaConfig()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public ZbAreaConfig(string strid)
        {
            string strSql = "select * from TAB_ZbcAreaConfig where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ReadCase(this,dt.Rows[0]);
            }
        }
        #endregion 构造函数


        //功能:读取密码信息
        private static void ReadCase(ZbAreaConfig zbareaconfig,DataRow Row)
        {
            zbareaconfig.strID = Row["strID"].ToString();
            zbareaconfig.UnitID = PageBase.static_ext_int(Row["UnitID"].ToString());
            zbareaconfig.ApanageID = PageBase.static_ext_int(Row["ApanageID"].ToString());
            zbareaconfig.TrackID = PageBase.static_ext_int(Row["TrackID"].ToString());
        }

        //功能:获得所有整备场手持机密码信息
        public static List<ZbAreaConfig> getZbAreaConfigList(int unitid, int apanageid)
        {
            List<ZbAreaConfig> ZbAreaConfigList = new List<ZbAreaConfig>();
            string strSql = "select * from TAB_ZbcAreaConfig where UnitID=@UnitID and ApanageID=@ApanageID";
            SqlParameter[] sqlParams = {
                                            new SqlParameter("UnitID",unitid),
                                           new SqlParameter("ApanageID",apanageid)
                                       };
            DataTable dtZbAreaConfig = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            foreach (DataRow dr in dtZbAreaConfig.Rows)
            {
                ZbAreaConfig zbareaconfig = new ZbAreaConfig();
                ZbAreaConfig.ReadCase(zbareaconfig, dr);
                ZbAreaConfigList.Add(zbareaconfig);
            }
            return ZbAreaConfigList;

        }


        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_ZbcAreaConfig (UnitID,ApanageID,TrackID) values (@UnitID,@ApanageID,@TrackID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("UnitID",UnitID),
                                           new SqlParameter("ApanageID",ApanageID),
                                           new SqlParameter("TrackID",TrackID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_ZbcAreaConfig set TrackID=@TrackID where strID=@strID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strID",strID),
                                           new SqlParameter("TrackID",TrackID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
       
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_ZbcAreaConfig where 1=1";
            strSql += strid != "" ? " and strID=@strID" : "";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int unitid, int apanageid)
        {
            string strSql = "select count(*) from TAB_ZbcAreaConfig where UnitID=@UnitID and ApanageID=@ApanageID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("UnitID",unitid),
                                           new SqlParameter("ApanageID",apanageid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法

        ///// <summary>
        ///// 由流程id获取所有环节
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <returns></returns>
        //public static DataTable GetAllTAB_HandleCase(string uid)
        //{
        //    string strSql = "select * from TAB_ZbcAreaConfig where 1=1";
        //    if (uid != "")
        //    {
        //        strSql += " and strProcessID = @uid ";
        //    }
        //    strSql += " order by nOrder ";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("uid",uid)
        //                               };
        //    return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        //}
        #endregion

    }
}
