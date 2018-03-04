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
    ///Line 的摘要说明
    /// </summary>
    public class Line
    {
        #region 属性
        public string strLineGUID = "";
        public string strLineName = "";
        public string strWorkShopGUID = "";
        #endregion 属性

        #region 构造函数
        public Line()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public Line(string LineGUID)
        {
            string strSql = "select * from TAB_Base_Line where strLineGUID=@strLineGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineGUID",LineGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strLineGUID = dt.Rows[0]["strLineGUID"].ToString();
                strLineName = dt.Rows[0]["strLineName"].ToString();
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();           
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_Line (strLineGUID,strLineName,strWorkShopGUID) values (@strLineGUID,@strLineName,@strWorkShopGUID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineGUID",guid),
                                           new SqlParameter("strLineName",strLineName),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID)                                           
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_Line set strLineName = @strLineName,strWorkShopGUID=@strWorkShopGUID where strLineGUID=@strLineGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineGUID",strLineGUID),
                                           new SqlParameter("strLineName",strLineName),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                           
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string LineGUID)
        {
            string strSql = "delete TAB_Base_Line where strLineGUID=@strLineGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineGUID",LineGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string LineGUID,string AreaGUID, string LineName)
        {
            string strSql = "select count(*) from TAB_Base_Line where strWorkShopGUID = @strWorkShopGUID and strLineName=@strLineName ";
            if (LineGUID != "")
            {
                strSql += " and strLineGUID <> @strLineGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineGUID",LineGUID),
                                           new SqlParameter("strWorkShopGUID",AreaGUID),
                                           new SqlParameter("strLineName",LineName)


                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllLines(string AreaGUID, string LineName)
        {
            string strSql = "select * from VIEW_Base_Line ";
            if (LineName != "")
            {
                strSql += " and strLineName like @strLineName ";
            }
            if (AreaGUID != "")
            {
                strSql += " and strWorkShopGUID =@strWorkShopGUID";
            }
            strSql += " order by strLineName ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineName","%" +LineName+ "%"),
                                           new SqlParameter("strWorkShopGUID",AreaGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static DataTable GetAllLinesDic(string AreaGUID,string DefaultName)
        {
            string strSql = "select * from VIEW_Base_Line where strWorkShopGUID =@strWorkShopGUID";

            strSql += " order by strAreaName,strLineName ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",AreaGUID)
                                       };
            DataTable dtResult = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

            DataRow dr = dtResult.NewRow();
            dr["strLineGUID"] = "";
            dr["strLineName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }
        /// <summary>
        /// 获取线路包含的人员交路信息
        /// </summary>
        /// <param name="LineGUID"></param>
        /// <returns></returns>
        public static DataTable GetLineTrainmanJiaolus(string LineGUID)
        {
            string strSql = "select * from VIEW_Base_TrainmanJiaoluInLine where  nJiaoluType > 1 and strLineGUID=@strLineGUID";
           
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineGUID",LineGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 返回线路里人员交路的起始站的GUID和名称
        /// Column:strStartStationName,strStartStation
        /// </summary>
        /// <param name="LineGUID"></param>
        /// <returns></returns>
        public static DataTable GetLineStartStation(string LineGUID)
        {
            string strSql = "select strStartStation,strStartStationName from VIEW_Base_TrainmanJiaoluInLine where nJiaoluType > 1  and strLineGUID=@strLineGUID group by strStartStation,strStartStationName";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineGUID",LineGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 添加线路包含的人员交路
        /// </summary>
        /// <param name="LineGUID"></param>
        /// <param name="JiaoluGUIDs"></param>
        public static void UpdateLineTrainmanJiaolus(string LineGUID, string JiaoluGUIDs)
        {
            string strSql = "delete from TAB_Base_TrainmanJiaoluInLine where strLineGUID = @strLineGUID and strTrainmanJiaoluGUID in (select strTrainmanJiaoluGUID from VIEW_Base_TrainmanJiaoluInLine where strLineGUID=@strLineGUID and nJiaoluType > 1)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineGUID",LineGUID)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            string[] guids = JiaoluGUIDs.Split(new char[] { ';' },StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < guids.Length; i++)
            {
                strSql = "insert into TAB_Base_TrainmanJiaoluInLine (strLineGUID,strTrainmanJiaoluGUID) values (@strLineGUID,@strTrainmanJiaoluGUID)";
                SqlParameter[] sqlParams2 = {
                                                new SqlParameter("strLineGUID",LineGUID),
                                                new SqlParameter("strTrainmanJiaoluGUID",guids[i])
                                            };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams2);
            }

        }
        #endregion
    }
}
