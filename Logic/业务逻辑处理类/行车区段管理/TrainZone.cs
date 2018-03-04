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
    ///TrainZone 功能：行车区段数据增删查
    /// </summary>
    public class TrainZone
    {
        #region 属性
        public string strTrainJiaoluGUID = "";
        public string strTrainJiaoluName = "";
        public string strStartStation = "";
        public string strEndStation = "";
        public string strWorkShopGUID = "";
        public int bIsBeginWorkFP = 0;
        public int bIsDir = 0;
        #endregion 属性

        #region 构造函数
        public TrainZone()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public TrainZone(string strid)
        {
            string strSql = "select * from TAB_Base_TrainJiaolu where strTrainJiaoluGUID=@strTrainJiaoluGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strTrainJiaoluGUID = dt.Rows[0]["strTrainJiaoluGUID"].ToString();
                strTrainJiaoluName = dt.Rows[0]["strTrainJiaoluName"].ToString();
                strStartStation = dt.Rows[0]["strStartStation"].ToString();
                strEndStation = dt.Rows[0]["strEndStation"].ToString();
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                PageBase p = new PageBase();
                bIsBeginWorkFP = p.ext_int(dt.Rows[0]["bIsBeginWorkFP"]);
                bIsDir = p.ext_int(dt.Rows[0]["bIsDir"]);
            }
        }
        #endregion 构造函数

        #region 增删改
        public string Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = @"insert into TAB_Base_TrainJiaolu (strTrainJiaoluGUID,strTrainJiaoluName,strStartStation,strEndStation,strWorkShopGUID,bIsBeginWorkFP,bIsDir) 
                    values (@strTrainJiaoluGUID,@strTrainJiaoluName,@strStartStation,@strEndStation,@strWorkShopGUID,@bIsBeginWorkFP,@bIsDir)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",guid),
                                           new SqlParameter("strTrainJiaoluName",strTrainJiaoluName),
                                           new SqlParameter("strStartStation",strStartStation),
                                           new SqlParameter("strEndStation",strEndStation),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("bIsBeginWorkFP",bIsBeginWorkFP),
                                           new SqlParameter("bIsDir",bIsDir)
                                       };
            if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0)
            {
                return guid;
            }
            return "";
        }
        public bool Update()
        {
            string strSql = @"update TAB_Base_TrainJiaolu set strTrainJiaoluName = @strTrainJiaoluName,strStartStation = @strStartStation,strEndStation = @strEndStation,
                strWorkShopGUID = @strWorkShopGUID,bIsBeginWorkFP = @bIsBeginWorkFP,bIsDir=@bIsDir where strTrainJiaoluGUID=@strTrainJiaoluGUID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("strTrainJiaoluName",strTrainJiaoluName),
                                           new SqlParameter("strStartStation",strStartStation),
                                           new SqlParameter("strEndStation",strEndStation),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("bIsBeginWorkFP",bIsBeginWorkFP),
                                           new SqlParameter("bIsDir",bIsDir)
                                           
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string StationGUID)
        {
            string strSql = "delete TAB_Base_TrainJiaolu where strTrainJiaoluGUID=@strTrainJiaoluGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",StationGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool ExistName(string strid, string name)
        {
            string strSql = "select count(*) from TAB_Base_TrainJiaolu where strTrainJiaoluName=@strTrainJiaoluName ";
            if (strid != "")
            {
                strSql += " and strTrainJiaoluGUID <> @strTrainJiaoluGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",strid),
                                           new SqlParameter("strTrainJiaoluName",name)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改


        //#region 扩展方法
        public static DataTable GetAllTrainZone(string strTrainJiaoluName)
        {
            string strSql = "select * from TAB_Base_TrainJiaolu ";
            if (strTrainJiaoluName != "")
            {
                strSql += " and strTrainJiaoluName like @strTrainJiaoluName ";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluName","%" +strTrainJiaoluName+ "%")
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }

    
    }
}
