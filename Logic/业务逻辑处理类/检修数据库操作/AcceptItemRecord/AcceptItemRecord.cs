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

namespace ZbcXxgl.TF.ZbcJxDBUtility
{
    public class AcceptItemRecord
    {
        #region 属性
        public string strID;
        public DateTime? dtAcceptTime;
        public int nAcceptID;
        public int nUserID;
        public int JiHuaID;
        #endregion 属性

        #region 构造函数
        public AcceptItemRecord()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public AcceptItemRecord(string strid)
        {
            string strSql = "select * from TAB_AcceptItemRecord where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static AcceptItemRecord SetValue(AcceptItemRecord air, DataRow dr)
        {
            if (dr!=null)
            {
                air.strID = dr["strID"].ToString();
                air.dtAcceptTime = PageBase.static_ext_date(dr["dtAcceptTime"]);
                air.nAcceptID = PageBase.static_ext_int(dr["nAcceptID"]);
                air.nUserID = PageBase.static_ext_int(dr["nUserID"]);
                air.JiHuaID = PageBase.static_ext_int(dr["JiHuaID"]);
            }
            return air;
        }
 
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_AcceptItemRecord (nAcceptID,nUserID,dtAcceptTime,JiHuaID) values (@nAcceptID,@nUserID,@dtAcceptTime,@JiHuaID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nAcceptID",nAcceptID),
                                           new SqlParameter("nUserID",nUserID),
                                           new SqlParameter("dtAcceptTime",dtAcceptTime),
                                           new SqlParameter("JiHuaID",JiHuaID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams) > 0;
        }
        //public bool Update()
        //{
        //    string strSql = "update TAB_AcceptItemRecord set JiHuaID=@JiHuaID where nAcceptID=@nAcceptID and dtAcceptTime=@dtAcceptTime";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nAcceptID",nAcceptID),
        //                                   new SqlParameter("dtAcceptTime",dtAcceptTime),
        //                                   new SqlParameter("JiHuaID",JiHuaID)
        //                               };
        //    return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams) > 0;
        //}
        //public static bool Delete(int jcid,int userid)
        //{
        //    string strSql = "delete TAB_AcceptItemRecord where nAcceptID=@nAcceptID and dtAcceptTime=@dtAcceptTime";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nAcceptID",jcid),
        //                                   new SqlParameter("dtAcceptTime",userid)
        //                               };
        //    return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams) > 0;
        //}
        public static bool Exist(int nacid, int jhid)
        {
            string strSql = "select count(*) from TAB_AcceptItemRecord where JiHuaID=@JiHuaID and nAcceptID=@nAcceptID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nAcceptID",nacid),
                                           new SqlParameter("JiHuaID",jhid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        /// <summary>
        /// type 1返回datatable 2 返回list
        /// </summary>
        /// <param name="locotype"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetAcceptItemRecordList(int JhID, int type)
        {
            string strSql = "select * from VIEW_AcceptItemRecord where JiHuaID=@JiHuaID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiHuaID",JhID)
                                       };
            DataTable dtAcceptItemRecordList = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (type == 1)
            {
                return dtAcceptItemRecordList;
            }
            return ListValue(dtAcceptItemRecordList);
        }


        /// <summary>
        /// 返回list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<AcceptItemRecord> ListValue(DataTable dt)
        {
            List<AcceptItemRecord> AcceptItemRecordList = new List<AcceptItemRecord>();
            AcceptItemRecord acceptitemrecord;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    acceptitemrecord = new AcceptItemRecord();
                    AcceptItemRecordList.Add(SetValue(acceptitemrecord, dt.Rows[i]));
                }
            }
            return AcceptItemRecordList;
        }
        #endregion
    }
}
