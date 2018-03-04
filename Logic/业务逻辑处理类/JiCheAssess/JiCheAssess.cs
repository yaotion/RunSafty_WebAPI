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
    public class JiCheAssess
    {
        #region 属性
        public string strID;
        public string strUserNum;
        public int nJiCheID;
        public int nPostType;
        #endregion 属性

        #region 构造函数
        public JiCheAssess()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public JiCheAssess(string strid)
        {
            string strSql = "select * from TAB_JiCheAssess where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static JiCheAssess SetValue(JiCheAssess jcassess, DataRow dr)
        {
            if (dr!=null)
            {
                jcassess.strID = dr["strID"].ToString();
                jcassess.nJiCheID = PageBase.static_ext_int(dr["nJiCheID"]);
                jcassess.strUserNum = dr["strUserNum"].ToString();
                jcassess.nPostType = PageBase.static_ext_int(dr["nPostType"]);
            }
            return jcassess;
        }
 
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_JiCheAssess (nJiCheID,strUserNum,nPostType) values (@nJiCheID,@strUserNum,@nPostType)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nJiCheID",nJiCheID),
                                           new SqlParameter("strUserNum",strUserNum),
                                           new SqlParameter("nPostType",nPostType)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_JiCheAssess set nPostType=@nPostType where nJiCheID=@nJiCheID and strUserNum=@strUserNum";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nJiCheID",nJiCheID),
                                           new SqlParameter("strUserNum",strUserNum),
                                           new SqlParameter("nPostType",nPostType)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(int jcid,int userid)
        {
            string strSql = "delete TAB_JiCheAssess where nJiCheID=@nJiCheID and strUserNum=@strUserNum";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nJiCheID",jcid),
                                           new SqlParameter("strUserNum",userid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int nJiCheID, string strUserNum)
        {
            string strSql = "select count(*) from TAB_JiCheAssess where nJiCheID=@nJiCheID and strUserNum=@strUserNum";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nJiCheID",nJiCheID),
                                           new SqlParameter("strUserNum",strUserNum)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jcid"></param>
        /// <returns></returns>
        public static bool isHadRecord(string userid)
        {
            DateTime dt = DateTime.Now;
            DateTime start = new DateTime(dt.Year, dt.Month, 1);  //月初日期
            DateTime end = start.AddMonths(1).AddSeconds(-1);  //月底日期
            string strSql = "select count(*) from TAB_JiCheAssess where strUserNum=@strUserNum and dtPostDateTime>='" + start.ToString("yyyy-MM-dd hh:mm:ss") + "' and dtPostDateTime<='" + end.ToString("yyyy-MM-dd hh:mm:ss") + "'";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strUserNum",userid)
                                       };
            return PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams))>0;
        }

        //public static List<JiCheAssess> GetAllJiCheAssessList(string jcid)
        //{
        //    string strSql = "select * from TAB_JiCheAssess where nJiCheID=@nJiCheID";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nJiCheID",jcid)
        //                               };
        //    return ListValue(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0]);
        //}

        ///// <summary>
        ///// 返回list对象
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public static List<JiCheAssess> ListValue(DataTable dt)
        //{
        //    List<JiCheAssess> bearingList = new List<JiCheAssess>();
        //    JiCheAssess bearing;
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            bearing = new JiCheAssess();
        //            bearing.nJiCheID = PageBase.static_ext_int(dt.Rows[i]["nJiCheID"]);
        //            bearing.cdatetime = PageBase.static_ext_date(dt.Rows[i]["cdatetime"]);
        //            bearing.strUserNum = PageBase.static_ext_int(dt.Rows[i]["strUserNum"]);
        //            bearing.nPostType = PageBase.static_ext_int(dt.Rows[i]["nPostType"]);
        //            bearing.alarmtype = dt.Rows[i]["alarmtype"].ToString();
        //            bearingList.Add(bearing);
        //        }
        //    }
        //    return bearingList;
        //}
        #endregion
    }
}
