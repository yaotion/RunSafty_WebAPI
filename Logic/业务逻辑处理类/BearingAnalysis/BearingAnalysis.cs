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
    public class BearingAnalysis
    {
        #region 属性
        public string strID;
        public int JiCheID;
        public string traintype;
        public string traincode;
        public DateTime? begindt;
        public DateTime? enddt;
        public double maxtemper;
        public double maxteemper;
        public DateTime? maxtemperdate;
        public string maxtemperpos;
        public double maxtemperrasie;
        public double maxteemperrasie;
        public DateTime? maxtemperrasiedate;
        public string maxtemperrasiepos;
        #endregion 属性

        #region 构造函数
        public BearingAnalysis()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public BearingAnalysis(string strid)
        {
            string strSql = "select * from TAB_BearingAnalysis where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static BearingAnalysis SetValue(BearingAnalysis bearing, DataRow dr)
        {
            if (dr!=null)
            {
                bearing.strID = dr["strID"].ToString();
                bearing.JiCheID =PageBase.static_ext_int(dr["JiCheID"]);
                bearing.traintype = dr["traintype"].ToString();
                bearing.traincode = dr["traincode"].ToString();
                bearing.begindt = PageBase.static_ext_date(dr["begindt"]);
                bearing.enddt = PageBase.static_ext_date(dr["enddt"]);
                bearing.maxtemper = PageBase.static_ext_double(dr["maxtemper"]);
                bearing.maxteemper = PageBase.static_ext_double(dr["maxteemper"]);
                bearing.maxtemperdate = PageBase.static_ext_date(dr["maxtemperdate"]);
                bearing.maxtemperpos = dr["maxtemperpos"].ToString();
                bearing.maxtemperrasie = PageBase.static_ext_double(dr["maxtemperrasie"]);
                bearing.maxteemperrasie = PageBase.static_ext_double(dr["maxteemperrasie"]);
                bearing.maxtemperrasiedate = PageBase.static_ext_date(dr["maxtemperrasiedate"]);
                bearing.maxtemperrasiepos = dr["maxtemperrasiepos"].ToString();
            }
            return bearing;
        }
 
        #endregion 构造函数

        #region 扩展方法
        public static List<BearingAnalysis> GetAllSysconfig(string strSection)
        {
            string strSql = "select * from TAB_BearingAnalysis where 1=1";
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
        /// 根据传入where条件获取轴报记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static BearingAnalysis BearingAnalysisRecords(string where)
        {
            string strSql = "select * from TAB_BearingAnalysis where 1=1 "+where;
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
            BearingAnalysis bearing = new BearingAnalysis();
            if (dt.Rows.Count > 0)
            {
                return SetValue(bearing, dt.Rows[0]);
            }
            return bearing;
        }

        /// <summary>
        /// 返回list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<BearingAnalysis> ListValue(DataTable dt)
        {
            List<BearingAnalysis> bearingList = new List<BearingAnalysis>();
            BearingAnalysis bearing;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bearing = new BearingAnalysis();
                    bearing.strID = dt.Rows[i]["strID"].ToString();
                    bearing.JiCheID = PageBase.static_ext_int(dt.Rows[i]["JiCheID"]);
                    bearing.traintype = dt.Rows[i]["strValue"].ToString();
                    bearing.traincode = dt.Rows[i]["traincode"].ToString();
                    bearing.begindt = PageBase.static_ext_date(dt.Rows[i]["begindt"]);
                    bearing.enddt = PageBase.static_ext_date(dt.Rows[i]["enddt"]);
                    bearing.maxtemper = PageBase.static_ext_double(dt.Rows[i]["maxtemper"]);
                    bearing.maxteemper = PageBase.static_ext_double(dt.Rows[i]["maxteemper"]);
                    bearing.maxtemperdate = PageBase.static_ext_date(dt.Rows[i]["maxtemperdate"]);
                    bearing.maxtemperpos = dt.Rows[i]["maxtemperpos"].ToString();
                    bearing.maxtemperrasie = PageBase.static_ext_double(dt.Rows[i]["maxtemperrasie"]);
                    bearing.maxteemperrasie = PageBase.static_ext_double(dt.Rows[i]["maxteemperrasie"]);
                    bearing.maxtemperrasiedate = PageBase.static_ext_date(dt.Rows[i]["maxtemperrasiedate"]);
                    bearing.maxtemperrasiepos = dt.Rows[i]["maxtemperrasiepos"].ToString();
                    bearingList.Add(bearing);
                }
            }
            return bearingList;
        }
        #endregion
    }
}
