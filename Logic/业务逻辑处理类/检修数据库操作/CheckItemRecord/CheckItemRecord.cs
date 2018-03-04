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
    public class CheckItemRecord
    {
        #region 属性
        public string strID;
        public DateTime? dtCheckTime;
        public int nAcceptID;
        public int nUserID;
        public int JiHuaID;
        #endregion 属性

        #region 构造函数
        public CheckItemRecord()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public CheckItemRecord(string strid)
        {
            string strSql = "select * from TAB_CheckItemRecord where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static CheckItemRecord SetValue(CheckItemRecord cir, DataRow dr)
        {
            if (dr!=null)
            {
                cir.strID = dr["strID"].ToString();
                cir.dtCheckTime = PageBase.static_ext_date(dr["dtCheckTime"]);
                cir.nAcceptID = PageBase.static_ext_int(dr["nAcceptID"]);
                cir.nUserID = PageBase.static_ext_int(dr["nUserID"]);
                cir.JiHuaID = PageBase.static_ext_int(dr["JiHuaID"]);
            }
            return cir;
        }
 
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_CheckItemRecord (nAcceptID,nUserID,dtCheckTime,JiHuaID) values (@nAcceptID,@nUserID,@dtCheckTime,@JiHuaID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nAcceptID",nAcceptID),
                                           new SqlParameter("nUserID",nUserID),
                                           new SqlParameter("dtCheckTime",dtCheckTime),
                                           new SqlParameter("JiHuaID",JiHuaID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams) > 0;
        }
        //public bool Update()
        //{
        //    string strSql = "update TAB_CheckItemRecord set JiHuaID=@JiHuaID where nAcceptID=@nAcceptID and dtCheckTime=@dtCheckTime";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nAcceptID",nAcceptID),
        //                                   new SqlParameter("dtCheckTime",dtCheckTime),
        //                                   new SqlParameter("JiHuaID",JiHuaID)
        //                               };
        //    return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams) > 0;
        //}
        //public static bool Delete(int jcid,int userid)
        //{
        //    string strSql = "delete TAB_CheckItemRecord where nAcceptID=@nAcceptID and dtCheckTime=@dtCheckTime";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nAcceptID",jcid),
        //                                   new SqlParameter("dtCheckTime",userid)
        //                               };
        //    return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams) > 0;
        //}
        public static bool Exist(int nacid, int jhid)
        {
            string strSql = "select count(*) from TAB_CheckItemRecord where JiHuaID=@JiHuaID and nAcceptID=@nAcceptID";
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
        public static object GetCheckItemRecordList(int JhID, int type)
        {
            string strSql = "select * from VIEW_CheckItemRecord where JiHuaID=@JiHuaID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiHuaID",JhID)
                                       };
            DataTable dtCheckItemRecordList = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (type == 1)
            {
                return dtCheckItemRecordList;
            }
            return ListValue(dtCheckItemRecordList);
        }


        /// <summary>
        /// 返回list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<CheckItemRecord> ListValue(DataTable dt)
        {
            List<CheckItemRecord> CheckItemRecordList = new List<CheckItemRecord>();
            CheckItemRecord checkitemrecord;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    checkitemrecord = new CheckItemRecord();
                    CheckItemRecordList.Add(SetValue(checkitemrecord, dt.Rows[i]));
                }
            }
            return CheckItemRecordList;
        }
        #endregion
    }
}
