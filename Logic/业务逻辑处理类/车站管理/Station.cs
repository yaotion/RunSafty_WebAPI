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
    ///Station 的摘要说明
    /// </summary>
    public class Station
    {
        #region 属性
        public string strStationGUID = "";
        public string strStationNumber = "";
        public string strStationName = "";
        public string strStationPY = "";
        #endregion 属性

        #region 构造函数
        public Station()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public Station(string StationGUID)
        {
            string strSql = "select * from TAB_Base_Station where strStationGUID=@strStationGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationGUID",StationGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strStationGUID = dt.Rows[0]["strStationGUID"].ToString();
                strStationNumber = dt.Rows[0]["strStationNumber"].ToString();
                strStationName = dt.Rows[0]["strStationName"].ToString();
                strStationPY = dt.Rows[0]["strStationPY"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_Station (strStationGUID,strStationNumber,strStationName,strStationPY) values (@strStationGUID,@strStationNumber,@strStationName,@strStationPY)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationGUID",guid),
                                           new SqlParameter("strStationNumber",strStationNumber),
                                           new SqlParameter("strStationName",strStationName),
                                           new SqlParameter("strStationPY",strStationPY)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_Station set strStationNumber = @strStationNumber,strStationName = @strStationName,strStationPY=@strStationPY where strStationGUID=@strStationGUID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strStationGUID",strStationGUID),
                                           new SqlParameter("strStationNumber",strStationNumber),
                                           new SqlParameter("strStationName",strStationName),
                                           new SqlParameter("strStationPY",strStationPY)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string StationGUID)
        {
            string strSql = "delete TAB_Base_Station where strStationGUID=@strStationGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationGUID",StationGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool ExistName(string StationGUID, string StationName)
        {
            string strSql = "select count(*) from TAB_Base_Station where strStationName=@strStationName ";
            if (StationGUID != "")
            {
                strSql += " and strStationGUID <> @strStationGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationGUID",StationGUID),
                                           new SqlParameter("strStationName",StationName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        public static bool ExistNumber(string StationGUID, string StationNumber)
        {
            string strSql = "select count(*) from TAB_Base_Station where strStationNumber=@strStationNumber ";
            if (StationGUID != "")
            {
                strSql += " and strStationGUID <> @strStationGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationGUID",StationGUID),
                                           new SqlParameter("strStationNumber",StationNumber)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改


        #region 扩展方法


        public static DataTable GetAllStationByName(string stationName)
        {
            string strSql = "select * from TAB_Base_Station where 1=1 ";
            if (stationName != "")
            {
                strSql += " and (strStationName like @strStationName or strStationPY like @strStationName)";
            }
             
            strSql += " order by strStationNumber ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationName","%" +stationName+ "%"), 
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static DataTable GetAllStation(string StationName, string StationNumber)
        {
            string strSql = "select * from TAB_Base_Station where 1=1 ";
            if (StationName != "")
            {
                strSql += " and strStationName like @strStationName ";
            }
            if (StationNumber != "")
            {
                strSql += " and strStationNumber like @strStationNumber ";
            }
            strSql += " order by strStationNumber ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationName","%" +StationName+ "%"),
                                           new SqlParameter("strStationNumber","%" +StationNumber+ "%")
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static DataTable GetAllStationDic(string DefaultName)
        {
            DataTable dtResult = GetAllStation("", "");
            DataRow dr = dtResult.NewRow();
            dr["strStationGUID"] = "";
            dr["strStationName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }

        /// <summary>
        /// 获取车站的所有TMIS号
        /// </summary>
        /// <param name="StationGUID"></param>
        /// <returns></returns>
        public static DataTable GetAllStationTMIS(string StationGUID)
        {
            string strSql = "select * from TAB_Base_Station_TMIS where strStationGUID=@StationGUID order by nTMIS ";

            SqlParameter[] sqlParams = {
                                            new SqlParameter("StationGUID",StationGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //添加TMIS
        public static void AddStationTMIS(string StationGUID, string TMIS)
        {
            string strSql = "insert into TAB_Base_Station_TMIS (strStationGUID,nTMIS) values (@strStationGUID,@nTMIS)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationGUID",StationGUID),
                                           new SqlParameter("nTMIS",TMIS)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }
        //删除TMIS
        public static void DeleteStationTMIS(string StationGUID, string TMIS)
        {
            string strSql = "delete from TAB_Base_Station_TMIS where strStationGUID=@strStationGUID and nTMIS=@nTMIS";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStationGUID",StationGUID),
                                           new SqlParameter("nTMIS",TMIS)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }
        #endregion
    }

}
