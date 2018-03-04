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

namespace TF.RunSafty.Logic
{
    public class ZbcData
    {
        #region 属性
        public string strID;
        public DateTime? etime;
        public string cx;
        public string ch;
        public string gdh;
        public string zbcbs;
        public string jczt;
        public int xPos;
        public int yPos;
        #endregion 属性

        #region 构造函数
        public ZbcData()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public ZbcData(string strID)
        {
            string strSql = "select * from TAB_ZbcData where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["strID"].ToString();
                cx = dt.Rows[0]["cx"].ToString();
                ch = dt.Rows[0]["ch"].ToString();
                gdh = dt.Rows[0]["gdh"].ToString();
                zbcbs = dt.Rows[0]["zbcbs"].ToString();
                jczt = dt.Rows[0]["jczt"].ToString();
                etime =PageBase.static_ext_date(dt.Rows[0]["etime"].ToString());
                xPos = PageBase.static_ext_int(dt.Rows[0]["xPos"].ToString());
                yPos = PageBase.static_ext_int(dt.Rows[0]["yPos"].ToString());
            }
        }
        #endregion 构造函数

        #region 扩展方法
        public static DataTable GetAllZBCData()
        {
            string strSql = "select * from View_lsDatJT6JiChe where (Mark <> 0) and (Mark <> 4) and ApanageId=1 and nMapX<>0 and nMapY<>0";
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
        }
        #endregion
    }
}
