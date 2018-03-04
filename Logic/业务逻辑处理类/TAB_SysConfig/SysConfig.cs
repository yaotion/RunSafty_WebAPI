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
    public class SysConfig
    {
        #region 属性
        public string strID;
        public string strSection;
        public string strIdent;
        public string strValue;
        #endregion 属性

        #region 构造函数
        public SysConfig()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public SysConfig(string strid)
        {
            string strSql = "select * from TAB_SysConfig where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static SysConfig SetValue(SysConfig config, DataRow dr)
        {
            if (dr!=null)
            {
                config.strID = dr["strID"].ToString();
                config.strSection = dr["strSection"].ToString();
                config.strValue = dr["strValue"].ToString();
                config.strIdent = dr["strIdent"].ToString();
            }
            return config;
        }
 
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_SysConfig (strID,strSection,strIdent,strValue) values (@strID,@strSection,@strIdent,@strValue)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",guid),
                                           new SqlParameter("strSection",strSection),
                                           new SqlParameter("strIdent",strIdent),
                                           new SqlParameter("strValue",strValue)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_SysConfig set strValue=@strValue where strIdent=@strIdent";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strValue",strValue),
                                           new SqlParameter("strIdent",strIdent)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_SysConfig where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strSection, string strIdent)
        {
            string strSql = "select count(*) from TAB_SysConfig where strSection=@strSection ";
            if (strSection != "")
            {
                strSql += " and strIdent = @strIdent ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSection",strSection),
                                           new SqlParameter("strIdent",strIdent)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static List<SysConfig> GetAllSysconfig(string strSection)
        {
            string strSql = "select * from TAB_SysConfig where 1=1";
            if (strSection != "")
            {
                strSql += " and strSection  = @strSection  ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSection ",strSection )
                                       };
            return ListValue(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0]);
        }
        /// <summary>
        /// 获取指定value
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strIdent"></param>
        /// <returns></returns>
        public static string GetSingleSysconfig(string strSection, string strIdent)
        {
            string strSql = "select strValue from TAB_SysConfig where 1=1";
            if (strSection != "")
            {
                strSql += " and strSection  = @strSection  ";
            }
            strSql += strIdent == "" ? "" : " and strIdent  = @strIdent ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSection ",strSection ),
                                           new SqlParameter("strIdent ",strIdent )
                                       };
            return SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).ToString();
        }

        public static List<SysConfig> ListValue(DataTable dt)
        {
            List<SysConfig> listConfig = new List<SysConfig>();
            SysConfig config;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    config = new SysConfig();
                    config.strID =dt.Rows[i]["strID"].ToString();
                    config.strSection = dt.Rows[i]["strSection"].ToString();
                    config.strValue = dt.Rows[i]["strValue"].ToString();
                    config.strIdent = dt.Rows[i]["strIdent"].ToString();
                    listConfig.Add(config);
                }
            }
            return listConfig;
        }
        #endregion
    }
}
