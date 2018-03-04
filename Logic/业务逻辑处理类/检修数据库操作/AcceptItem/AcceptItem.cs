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
    public class AcceptItem
    {
        #region 属性
        public int nID;
        public int JDID;
        public int nAcceptDay;
        public string strID;
        public string ItemName;
        public string LocoType;
        
        #endregion 属性

        #region 构造函数
        public AcceptItem()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public AcceptItem(string strid)
        {
            string strSql = "select * from TAB_AcceptItem where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static AcceptItem SetValue(AcceptItem acceptitem, DataRow dr)
        {
            if (dr!=null)
            {
                acceptitem.nID = PageBase.static_ext_int(dr["nID"]);
                acceptitem.JDID = PageBase.static_ext_int(dr["JDID"]);
                acceptitem.nAcceptDay = PageBase.static_ext_int(dr["nAcceptDay"]);
                acceptitem.strID = dr["strID"].ToString();
                acceptitem.ItemName = dr["ItemName"].ToString();
                acceptitem.LocoType = dr["LocoType"].ToString();
            }
            return acceptitem;
        }
 
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_AcceptItem (ItemName,nAcceptDay,JDID,LocoType) values (@ItemName,@nAcceptDay,@JDID,@LocoType)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ItemName",ItemName),
                                           new SqlParameter("nAcceptDay",nAcceptDay),
                                           new SqlParameter("JDID",JDID),
                                           new SqlParameter("LocoType",LocoType)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams) > 0;
        }
        //public bool Update()
        //{
        //    string strSql = "update TAB_AcceptItem set nAcceptDay=@nAcceptDay where nAcceptID=@nAcceptID and dtCheckTime=@dtCheckTime";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nAcceptID",nAcceptID),
        //                                   new SqlParameter("dtCheckTime",dtCheckTime),
        //                                   new SqlParameter("nAcceptDay",nAcceptDay)
        //                               };
        //    return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams) > 0;
        //}
        //public static bool Delete(int jcid,int userid)
        //{
        //    string strSql = "delete TAB_AcceptItem where nAcceptID=@nAcceptID and dtCheckTime=@dtCheckTime";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nAcceptID",jcid),
        //                                   new SqlParameter("dtCheckTime",userid)
        //                               };
        //    return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams) > 0;
        //}
        //public static bool Exist(int nacid, int jhid)
        //{
        //    string strSql = "select count(*) from TAB_AcceptItem where nAcceptDay=@nAcceptDay and nAcceptID=@nAcceptID";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("nAcceptID",nacid),
        //                                   new SqlParameter("nAcceptDay",jhid)
        //                               };
        //    return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams)) > 0;
        //}
        #endregion 增删改

        #region 扩展方法

        /// <summary>
        /// type 1返回datatable 2 返回list
        /// </summary>
        /// <param name="locotype"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetAcceptItemList(string locotype,int type)
        {
            string strSql = "select * from TAB_AcceptItem where LocoType=@LocoType";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("LocoType",locotype)
                                       };
            DataTable dtAcceptItem = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (type == 1)
            {
                return dtAcceptItem;
            }
            return ListValue(dtAcceptItem);
        }


        /// <summary>
        /// 返回list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<AcceptItem> ListValue(DataTable dt)
        {
            List<AcceptItem> AcceptItemList = new List<AcceptItem>();
            AcceptItem acceptitem;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    acceptitem = new AcceptItem();
                    AcceptItemList.Add(SetValue(acceptitem, dt.Rows[i]));
                }
            }
            return AcceptItemList;
        }
        #endregion
    }
}
