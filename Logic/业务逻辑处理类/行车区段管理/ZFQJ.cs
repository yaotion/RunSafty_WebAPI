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

namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///ZFQJ功能：提供折返区间增删改查
    /// </summary>
    public class ZFQJ
    {
        #region 属性
        public string strZFQJGUID;
        public string strTrainJiaoluGUID;
        public string strBeginStationGUID;
        public string strEndStationGUID;
        public string strBeginStationName;
        public string strEndStationName;
        public int nSortid;
        #endregion 属性

        #region 构造函数
        public ZFQJ()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public ZFQJ(string strid)
        {
            string strSql = "select * from VIEW_Base_ZFQJ where strZFQJGUID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strZFQJGUID = dt.Rows[0]["strZFQJGUID"].ToString();
                strTrainJiaoluGUID = dt.Rows[0]["strTrainJiaoluGUID"].ToString();
                strBeginStationGUID = dt.Rows[0]["strBeginStationGUID"].ToString();
                strEndStationGUID = dt.Rows[0]["strEndStationGUID"].ToString();
                strBeginStationName = dt.Rows[0]["strBeginStationName"].ToString();
                strEndStationName = dt.Rows[0]["strEndStationName"].ToString();
                nSortid = Convert.ToInt32(dt.Rows[0]["nSortid"]);
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_ZFQJ (strZFQJGUID,strTrainJiaoluGUID,strBeginStationGUID,strEndStationGUID,nSortid) values (@strZFQJGUID,@strTrainJiaoluGUID,@strBeginStationGUID,@strEndStationGUID,@nSortid)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strZFQJGUID",guid),
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("strBeginStationGUID",strBeginStationGUID),
                                           new SqlParameter("strEndStationGUID",strEndStationGUID),
                                           new SqlParameter("nSortid",nSortid) 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_ZFQJ set strBeginStationGUID = @strBeginStationGUID,strEndStationGUID=@strEndStationGUID,nSortid=@nSortid where strZFQJGUID=@strZFQJGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strBeginStationGUID",strBeginStationGUID),
                                           new SqlParameter("strEndStationGUID",strEndStationGUID),
                                           new SqlParameter("nSortid",nSortid),
                                           new SqlParameter("strZFQJGUID",strZFQJGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Base_ZFQJ where strZFQJGUID=@strZFQJGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strZFQJGUID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string jlid,string strid, string beginid,string endid)
        {
            string strSql = "select count(*) from TAB_Base_ZFQJ where 1=1";
            if (jlid != "")
            {
                strSql += " and strTrainJiaoluGUID = @strTrainJiaoluGUID";
            }
            if (strid != "")
            {
                strSql += " and strZFQJGUID <> @strZFQJGUID";
            }
            strSql += beginid != "" ? " and strBeginStationGUID=@strBeginStationGUID" : "";
            strSql += endid != "" ? " and strEndStationGUID=@strEndStationGUID" : "";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",jlid),
                                           new SqlParameter("strZFQJGUID",strid),
                                           new SqlParameter("strBeginStationGUID",beginid),
                                           new SqlParameter("strEndStationGUID",endid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        public bool UpdateSortid()
        {
            string strSql = "update TAB_Base_ZFQJ set nSortid=@nSortid where strZFQJGUID=@strZFQJGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nSortid",nSortid),
                                           new SqlParameter("strZFQJGUID",strZFQJGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllZfqj(string id)
        {
            string strCondition = "";
            if (id != "")
            {
                strCondition += " and strTrainJiaoluGUID = @strTrainJiaoluGUID ";
            }
            string strSql = "select * from VIEW_Base_ZFQJ where 1=1 " + strCondition + @" order by nSortid asc";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",id)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static int GetAllZfqjCount(string id)
        {
            string strSql = "select count(*) from VIEW_Base_ZFQJ";
            if (id != "")
            {
                strSql += " where strTrainJiaoluGUID = @strTrainJiaoluGUID ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",id)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams));
        }
        //public static DataTable GetAllAreasDic(string DefaultName)
        //{
        //    DataTable dtResult = GetAllAreas("");
        //    DataRow dr = dtResult.NewRow();
        //    dr["strGUID"] = "";
        //    dr["strAreaGUID"] = DefaultName;
        //    dtResult.Rows.InsertAt(dr, 0);
        //    return dtResult;
        //}

        #endregion
    }
}
