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

namespace TF.RunSafty.Logic
{
    public class HandleCaseRecord
    {
        #region 属性
        public string strID;
        public string strCaseName;
        public string dtBeginTime;
        public DateTime? dtEndTime;
        public string strCaseID;
        public int strTrainHandleID;
        public int nIsComplete;
        public string dtPostTime;
        public string strProcessID;
        #endregion 属性

        #region 构造函数
        public HandleCaseRecord()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public HandleCaseRecord(string strCaseID, string strTrainHandleID)
        {
            string strSql = "select * from TAB_HandleCaseRecord where 1=1 and strTrainHandleID = @strTrainHandleID";
            if (strCaseID != "")
            {
                strSql += " and strCaseID=@strCaseID";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strCaseID",strCaseID),
                                           new SqlParameter("strTrainHandleID",strTrainHandleID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["strID"].ToString();
                dtBeginTime = dt.Rows[0]["dtBeginTime"].ToString();
                dtEndTime =PageBase.static_ext_date(dt.Rows[0]["dtEndTime"].ToString());
                strCaseID = dt.Rows[0]["strCaseID"].ToString();
                strTrainHandleID = dt.Rows[0]["strTrainHandleID"].ToString();
                nIsComplete = PageBase.static_ext_int(dt.Rows[0]["nIsComplete"].ToString());
                dtPostTime = dt.Rows[0]["dtPostTime"].ToString();
            }
        }
        #endregion

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_HandleCaseRecord (strID,dtBeginTime,strCaseID,strTrainHandleID,nIsComplete) select @strID,@dtBeginTime,strID,@strTrainHandleID,@nIsComplete from TAB_HandleCase where strProcessID=@strProcessID and nOrder=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",guid),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("strCaseID",strCaseID),
                                           new SqlParameter("strTrainHandleID",strTrainHandleID),
                                           new SqlParameter("strProcessID",strProcessID),
                                           new SqlParameter("nIsComplete",nIsComplete)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Addhadcaseid()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_HandleCaseRecord (strID,dtBeginTime,strCaseID,strTrainHandleID,nIsComplete) values (@strID,@dtBeginTime,@strCaseID,@strTrainHandleID,@nIsComplete)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",guid),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("strCaseID",strCaseID),
                                           new SqlParameter("strTrainHandleID",strTrainHandleID),
                                           new SqlParameter("strProcessID",strProcessID),
                                           new SqlParameter("nIsComplete",nIsComplete)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_HandleCaseRecord set dtEndTime = @dtEndTime,nIsComplete=@nIsComplete where strTrainHandleID=@strTrainHandleID and strCaseID=@strCaseID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strTrainHandleID",strTrainHandleID),
                                          new SqlParameter("strCaseID",strCaseID),
                                           new SqlParameter("dtEndTime",dtEndTime),
                                           new SqlParameter("nIsComplete",nIsComplete)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_HandleCaseRecord where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strCaseID, int strTrainHandleID)
        {
            string strSql = "select count(*) from TAB_HandleCaseRecord where strTrainHandleID=@strTrainHandleID and strCaseID=@strCaseID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strCaseID",strCaseID),
                                           new SqlParameter("strTrainHandleID",strTrainHandleID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllTAB_HandleCaseRecord(string strCaseID, int strTrainHandleID)
        {
            string strSql = "select * from TAB_HandleCaseRecord where 1=1 and strTrainHandleID = @strTrainHandleID";
            if (strCaseID != "")
            {
                strSql += " and strCaseID=@strCaseID";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strCaseID",strCaseID),
                                           new SqlParameter("strTrainHandleID",strTrainHandleID)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        #endregion
    }
}
