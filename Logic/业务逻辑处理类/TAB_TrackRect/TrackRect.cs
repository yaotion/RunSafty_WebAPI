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
    public class TrackRect
    {
        #region 属性
        public string strID;
        public int nTrackID;
        public int nRectLeft;
        public int nRectTop;
        public int UnitId;
        public int ApanageId;
        public int nRectRight;
        public int nRectBottom;
        #endregion 属性

        #region 构造函数
        public TrackRect()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public TrackRect(string strid)
        {
            string strSql = "select * from TAB_TrackRect where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static TrackRect SetValue(TrackRect trackrect, DataRow dr)
        {
            if (dr!=null)
            {
                trackrect.strID = dr["strID"].ToString();
                trackrect.nTrackID = PageBase.static_ext_int(dr["nTrackID"]);
                trackrect.nRectLeft = PageBase.static_ext_int(dr["nRectLeft"]);
                trackrect.nRectTop = PageBase.static_ext_int(dr["nRectTop"]);
                trackrect.UnitId = PageBase.static_ext_int(dr["UnitId"]);
                trackrect.ApanageId = PageBase.static_ext_int(dr["ApanageId"]);
                trackrect.nRectRight = PageBase.static_ext_int(dr["nRectRight"]);
                trackrect.nRectBottom = PageBase.static_ext_int(dr["nRectBottom"]);
            }
            return trackrect;
        }
 
        #endregion 构造函数

        #region 扩展方法
        public static List<TrackRect> GetAllTrackRect(int apanageid)
        {
            string strSql = "select distinct * from TAB_TrackRect where 1=1 and ApanageId=@ApanageId";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ApanageId",apanageid )
                                       };
            return ListValue(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql,sqlParams).Tables[0]);
        }
        ///// <summary>
        ///// 获取指定value
        ///// </summary>
        ///// <param name="strSection"></param>
        ///// <param name="strIdent"></param>
        ///// <returns></returns>
        //public static string GetSingleTrackRect(string strSection, string strIdent)
        //{
        //    string strSql = "select strValue from TAB_TrackRect where 1=1";
        //    if (strSection != "")
        //    {
        //        strSql += " and strSection  = @strSection  ";
        //    }
        //    strSql += strIdent == "" ? "" : " and strIdent  = @strIdent ";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("strSection ",strSection ),
        //                                   new SqlParameter("strIdent ",strIdent )
        //                               };
        //    return SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).ToString();
        //}

        public static List<TrackRect> ListValue(DataTable dt)
        {
            List<TrackRect> listConfig = new List<TrackRect>();
            TrackRect trackrect;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    trackrect = new TrackRect();
                    listConfig.Add(SetValue(trackrect, dt.Rows[i]));
                }
            }
            return listConfig;
        }
        #endregion
    }
}
