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
    public class MBVersion
    {
        #region 属性
        public int nID;
        public string strID;
        public int UnitID;
        public int ApanageID;
        public string strVersionName;//版本名称
        public string strAPKFileName;//文件名称
        public DateTime? dtPublished;//发布时间
        #endregion 属性

        #region 构造函数
        public MBVersion()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public MBVersion(int ApanageID,int UnitID)
        {
            string strSql = "select top 1 * from Tab_MBVersion where ApanageID=@ApanageID and UnitID=@UnitID order by dtPublished desc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ApanageID",ApanageID),
                                           new SqlParameter("UnitID",UnitID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Read(this,dt.Rows[0]);
            }
        }
        #endregion 构造函数


        //功能:读取版本信息
        private static void Read(MBVersion mbv, DataRow Row)
        {
            mbv.nID = PageBase.static_ext_int(Row["nID"].ToString());
            mbv.UnitID = PageBase.static_ext_int(Row["UnitID"].ToString());
            mbv.ApanageID = PageBase.static_ext_int(Row["ApanageID"].ToString());
            mbv.strID = Row["strID"].ToString();
            mbv.strVersionName = Row["strVersionName"].ToString();
            mbv.strAPKFileName = Row["strAPKFileName"].ToString();
            mbv.dtPublished =PageBase.static_ext_date(Row["dtPublished"].ToString());
        }

        //功能:获得整备场历史更新信息
        public static List<MBVersion> getZbAreaConfigList(int unitid, int apanageid)
        {
            List<MBVersion> ZbAreaConfigList = new List<MBVersion>();
            string strSql = "select * from Tab_MBVersion where UnitID=@UnitID and ApanageID=@ApanageID";
            SqlParameter[] sqlParams = {
                                            new SqlParameter("UnitID",unitid),
                                           new SqlParameter("ApanageID",apanageid)
                                       };
            DataTable dtZbAreaConfig = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            foreach (DataRow dr in dtZbAreaConfig.Rows)
            {
                MBVersion mbv = new MBVersion();
                MBVersion.Read(mbv, dr);
                ZbAreaConfigList.Add(mbv);
            }
            return ZbAreaConfigList;

        }


        #region 增删改
        public bool Add()
        {
            string strSql = "insert into Tab_MBVersion (UnitID,ApanageID,strVersionName,strAPKFileName,dtPublished) values (@UnitID,@ApanageID,@strVersionName,@strAPKFileName,@dtPublished)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("UnitID",UnitID),
                                           new SqlParameter("ApanageID",ApanageID),
                                           new SqlParameter("strVersionName",strVersionName),
                                           new SqlParameter("strAPKFileName",strAPKFileName),
                                           new SqlParameter("dtPublished",dtPublished)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update Tab_MBVersion set strVersionName=@strVersionName,strAPKFileName=@strAPKFileName,dtPublished=@dtPublished where strID=@strID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strVersionName",strVersionName),
                                           new SqlParameter("strAPKFileName",strAPKFileName),
                                           new SqlParameter("dtPublished",dtPublished)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
       
        public static bool Delete(string strid)
        {
            string strSql = "delete Tab_MBVersion where 1=1";
            strSql += strid != "" ? " and strID=@strID" : "";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int apanageid, string strVersionName, string strAPKFileName)
        {
            string strSql = "select count(*) from Tab_MBVersion where ApanageID=@ApanageID and strVersionName=@strVersionName and strAPKFileName=@strAPKFileName";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ApanageID",apanageid),
                                           new SqlParameter("strVersionName",strVersionName),
                                           new SqlParameter("strAPKFileName",strAPKFileName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法

        /// <summary>
        /// 由站场id 获取当前站场手持机版本号
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string GetVersion(int apanageid,int unitid)
        {
            string strSql = "select strVersionName from Tab_MBVersion where ApanageID = @ApanageID and UnitID=@UnitID";
            strSql += " order by dtPublished desc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ApanageID",apanageid),
                                           new SqlParameter("UnitID",unitid)
                                       };
            return PageBase.static_ext_string(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }
        #endregion

    }
}
