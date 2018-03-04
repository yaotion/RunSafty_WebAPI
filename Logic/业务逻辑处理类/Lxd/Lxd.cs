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
    public class Lxd
    {
        #region 属性
        public string strID;
        public DateTime? dtPostTime;
        public string strTrainType;
        public string strTrainNumber;
        public string strGZLX;
        public string strPSCS;
        public string strPostTicketNumber;
        public DateTime? strPostTicketTime;
        public string strEndNumber;
        public string strRepairman;
        public string strProcessing;
        public DateTime? dtEndTime;
        public int nState;
        public string strTrainHandleID;
        
        #endregion 属性


        #region 增删改
        public bool Add()
        {
            string strSql = @"insert into TAB_LXD (strID,dtPostTime,strTrainType,strTrainNumber,strGZLX,strPSCS,strPostTicketNumber,strPostTicketTime,
strEndNumber,strRepairman,strProcessing,dtEndTime,nState,strTrainHandleID) values (@strID,@dtPostTime,@strTrainType,@strTrainNumber,@strGZLX,@strPSCS,@strPostTicketNumber,@strPostTicketTime,@strEndNumber,@strRepairman,@strProcessing,@dtEndTime,@nState,@strTrainHandleID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("dtPostTime",dtPostTime),
                                           new SqlParameter("strTrainType",strTrainType),
                                           new SqlParameter("strTrainNumber",strTrainNumber),
                                           new SqlParameter("strGZLX",strGZLX),
                                           new SqlParameter("strPSCS",strPSCS),
                                           new SqlParameter("strPostTicketNumber",strPostTicketNumber),
                                           new SqlParameter("strPostTicketTime",strPostTicketTime),
                                           new SqlParameter("strEndNumber",strEndNumber),
                                           new SqlParameter("strRepairman",strRepairman),
                                           new SqlParameter("strProcessing",strProcessing),
                                           new SqlParameter("dtEndTime",dtEndTime),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("strTrainHandleID",strTrainHandleID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = @"update TAB_LXD set dtPostTime = @dtPostTime,strTrainType = @strTrainType,strTrainNumber = @strTrainNumber,strGZLX = @strGZLX,
            strPSCS = @strPSCS,
            strPostTicketNumber = @strPostTicketNumber,strPostTicketTime = @strPostTicketTime,strEndNumber = @strEndNumber,strRepairman = @strRepairman,
            strProcessing = @strProcessing,dtEndTime = @dtEndTime,nState = @nState,strTrainHandleID = @strTrainHandleID where strID=@strID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strID",strID),
                                           new SqlParameter("dtPostTime",dtPostTime),
                                           new SqlParameter("strTrainType",strTrainType),
                                           new SqlParameter("strTrainNumber",strTrainNumber),
                                           new SqlParameter("strGZLX",strGZLX),
                                           new SqlParameter("strPSCS",strPSCS),
                                           new SqlParameter("strPostTicketNumber",strPostTicketNumber),
                                           new SqlParameter("strPostTicketTime",strPostTicketTime),
                                           new SqlParameter("strEndNumber",strEndNumber),
                                           new SqlParameter("strRepairman",strRepairman),
                                           new SqlParameter("strProcessing",strProcessing),
                                           new SqlParameter("dtEndTime",dtEndTime),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("strTrainHandleID",strTrainHandleID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_LXD where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid)
        {
            string strSql = "select count(*) from TAB_LXD where strID=@strID ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        /// <summary>
        /// 由id获取一条jt6
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetAllLxdFromStrID(string id)
        {
            string strSql = "select * from TAB_LXD where strID=@id";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetCountJt6FromJiCheID(int id)
        {
            string strSql = "SELECT * FROM lsDatJT6 WHERE (JiCheID =@id and RepairState<>'作废')";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 获取提票未结的数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetCountJt6tpFromJiCheID(int id)
        {
            string strSql = "SELECT count(*) FROM lsDatJT6 WHERE (JiCheID =@id and RepairState='提票')";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id)
                                       };
            return PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }

        /// <summary>
        /// 获取问题描述和解决方法根据车型、车号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetJt6RecordFromCxCh(string LocoType, string LocoNum)
        {
            string strSql = "SELECT top 15 a.RepairMethod,a.Question FROM lsDatJT6 a,lsDatJT6JiChe b WHERE a.JiCheID =b.ID and b.LocoType=@LocoType and b.LocoNum=@LocoNum and a.RepairState<>'作废' and CancelState = '未销号' And (RepairID in ('01','02','03','04')) order by PresenterDate desc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 获取重点活数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetCountJt6RecordFromCxCh(string LocoType, string LocoNum)
        {
            string strSql = "SELECT Count(*) FROM lsDatJT6 a,lsDatJT6JiChe b WHERE a.JiCheID =b.ID and b.LocoType=@LocoType and b.LocoNum=@LocoNum and a.RepairState<>'作废' and CancelState = '未销号' And (RepairID in ('01','02','03','04'))";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum)
                                       };
            return PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }

        /// <summary>
        /// 获取dayCount天内机统6数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSevenDaylsDatJT6(int apanageid, int dayCount)
        {
            string strSql = @"select CONVERT(varchar(100), PresenterDate, 23) as PresenterDate,RepairState
 from lsViewJT6 
where RuDuanDate > DATEADD(dd, -"+dayCount+ @",CONVERT(varchar(100), getdate(), 23)) and RuDuanDate< getdate()
 and apanageid=@apanageid and RepairState<>'作废' and mark<>4
order by CONVERT(varchar(100), PresenterDate, 23)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("apanageid",apanageid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        #endregion
    }
}
