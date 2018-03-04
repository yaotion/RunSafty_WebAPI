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
    ///TrainBelong功能：提供机车配属信息增删改查
    /// </summary>
    public class TrainBelong
    {
        #region 属性
        public string strGUID;
        public string strTrainTypeCode;
        public string strTrainTypeName;
        public string strTrainNumber;
        public string dtBeginTime;
        public string strWorkShopGUID;
        public string strWorkShopName;
        public string strAreaName;
        public string strJWDNumber;
        public string strOrginAreaGUID;
        public string strOrginJwdName;
        #endregion 属性

        #region 构造函数
        public TrainBelong()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public TrainBelong(string strid)
        {
            string strSql = "select * from VIEW_Base_TrainBelong where strGUID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strGUID = dt.Rows[0]["strGUID"].ToString();
                strTrainTypeCode = dt.Rows[0]["strTrainTypeCode"].ToString();
                strTrainTypeName = dt.Rows[0]["strTrainTypeCode"].ToString();
                strTrainNumber = dt.Rows[0]["strTrainNumber"].ToString();
                dtBeginTime = dt.Rows[0]["dtBeginTime"].ToString();
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                strWorkShopName = dt.Rows[0]["strWorkShopName"].ToString();
                strAreaName = dt.Rows[0]["strAreaName"].ToString();
                strJWDNumber = dt.Rows[0]["strJWDNumber"].ToString();
                strOrginAreaGUID = dt.Rows[0]["strOrginAreaGUID"].ToString();
                strOrginJwdName = dt.Rows[0]["strOrginJwdName"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_TrainBelong (strGUID,strTrainTypeCode,strTrainNumber,dtBeginTime,strWorkShopGUID,strOrginAreaGUID) values (@strGUID,@strTrainTypeCode,@strTrainNumber,@dtBeginTime,@strWorkShopGUID,@strOrginAreaGUID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",guid),
                                           new SqlParameter("strTrainTypeCode",strTrainTypeCode),
                                           new SqlParameter("strTrainNumber",strTrainNumber),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strOrginAreaGUID",strOrginAreaGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_TrainBelong set strTrainTypeCode=@strTrainTypeCode,strTrainNumber=@strTrainNumber,dtBeginTime=@dtBeginTime,strWorkShopGUID=@strWorkShopGUID,strOrginAreaGUID=@strOrginAreaGUID where strGUID=@strGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",strGUID),
                                           new SqlParameter("strTrainTypeCode",strTrainTypeCode),
                                           new SqlParameter("strTrainNumber",strTrainNumber),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strOrginAreaGUID",strOrginAreaGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Base_TrainBelong where strGUID=@strGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string name)
        {
            string strSql = "select count(*) from TAB_Base_TrainBelong where strTrainNumber=@name ";
            if (strid != "")
            {
                strSql += " and strGUID <> @strGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",strid),
                                           new SqlParameter("name",name)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        //public static DataTable GetAllWorkShop(string name)
        //{
        //    string strSql = "select * from TAB_Base_TrainBelong";
        //    if (name != "")
        //    {
        //        strSql += " and strWorkShopName like @name ";
        //    }
        //    strSql += " order by nid ";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("name","%" +name+ "%")
        //                               };
        //    return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        //}
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
