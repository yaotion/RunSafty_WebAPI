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
    public class BearingAlarm
    {
        #region 属性
        public string strID;
        public int JiCheID;
        public DateTime? cdatetime;
        public int shaftcode;
        public int pointccode;
        public string alarmtype;
        #endregion 属性

        #region 构造函数
        public BearingAlarm()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public BearingAlarm(string strid)
        {
            string strSql = "select * from TAB_BearingAlarm where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static BearingAlarm SetValue(BearingAlarm bearing, DataRow dr)
        {
            if (dr!=null)
            {
                bearing.strID = dr["strID"].ToString();
                bearing.JiCheID =PageBase.static_ext_int(dr["JiCheID"]);
                bearing.cdatetime = PageBase.static_ext_date(dr["cdatetime"]);
                bearing.shaftcode = PageBase.static_ext_int(dr["shaftcode"]);
                bearing.pointccode = PageBase.static_ext_int(dr["pointccode"]);
                bearing.alarmtype = dr["alarmtype"].ToString();
            }
            return bearing;
        }
 
        #endregion 构造函数

        #region 扩展方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jcid"></param>
        /// <returns></returns>
        public static DataTable GetAllBearingAlarmDT(int jcid)
        {
            string strSql = "select * from TAB_BearingAlarm where JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",jcid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static List<BearingAlarm> GetAllBearingAlarmList(string jcid)
        {
            string strSql = "select * from TAB_BearingAlarm where JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",jcid)
                                       };
            return ListValue(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0]);
        }

        /// <summary>
        /// 返回list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<BearingAlarm> ListValue(DataTable dt)
        {
            List<BearingAlarm> bearingList = new List<BearingAlarm>();
            BearingAlarm bearing;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bearing = new BearingAlarm();
                    bearing.JiCheID = PageBase.static_ext_int(dt.Rows[i]["JiCheID"]);
                    bearing.cdatetime = PageBase.static_ext_date(dt.Rows[i]["cdatetime"]);
                    bearing.shaftcode = PageBase.static_ext_int(dt.Rows[i]["shaftcode"]);
                    bearing.pointccode = PageBase.static_ext_int(dt.Rows[i]["pointccode"]);
                    bearing.alarmtype = dt.Rows[i]["alarmtype"].ToString();
                    bearingList.Add(bearing);
                }
            }
            return bearingList;
        }
        #endregion
    }
}
