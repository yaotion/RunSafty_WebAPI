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
    ///BeginWork 的摘要说明
    /// </summary>
    public class askleave
    {
        public askleave()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //g 
        }
        /// <summary>
        /// 查询请假记录
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="TrainmanName"></param>
        /// <param name="TrainmanNumber"></param>
        /// <param name="StationGUID"></param>
        /// <param name="DrinkResult"></param>
        /// <returns></returns>
        public static DataTable Queryaskleave(int PageIndex, int PageCount, string name, string BeginTime, string EndTime, string timefrom, string timeto,string type)
        {
            string strSql = " where 1=1 ";
            strSql += name != "" ? " and strTrainmanName like @name " : " ";
            strSql += BeginTime != "" ? " and dtLeaveTime>=@BeginTime" : " ";
            strSql += EndTime != "" ? " and dtLeaveTime<=@EndTime" : " ";
            strSql += timefrom != "" ? " and nLeaveLength>=@timefrom" : " ";
            strSql += timeto != "" ? " and nLeaveLength<=@timeto" : " ";
            strSql += type != "" ? " and nLeaveTypeID=@type" : " ";

            string Sql = "(select top " + (PageIndex * PageCount).ToString() +
                " strTrainmanName, strTrainmanGUID,nLeaveTypeID, strLeaveReason,dtLeaveTime,nLeaveLength,dtCreateTime,strDutyUserGUID,strLeaveTypeName,strDutyName " +
                " from VIEW_AskLeave " + strSql +
                " GROUP BY strTrainmanName, strTrainmanGUID,nLeaveTypeID,strLeaveReason,dtLeaveTime,nLeaveLength,dtCreateTime,strDutyUserGUID,strLeaveTypeName,strDutyName order by dtCreateTime desc) as tmp2";
            Sql = "(select top " + PageCount.ToString() + " * from " + Sql + " order by dtCreateTime) as tmp1";
            Sql = "select * from " + Sql + " order by dtCreateTime desc";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("name","%"+name + "%"),
                                       new SqlParameter("BeginTime",BeginTime),
                                       new SqlParameter("EndTime",EndTime),
                                       new SqlParameter("timefrom",timefrom),
                                       new SqlParameter("timeto",timeto),
                                       new SqlParameter("type",type)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];

        }

        /// <summary>
        /// 获取请假记录总数
        /// </summary>
        public static int GetaskleaveCount()
        {
            string strSql = "SELECT COUNT(*) FROM VIEW_AskLeave";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql));
        }

        /// <summary>
        /// 计算查询条件记录总数
        /// </summary>
        /// <returns></returns>
        public static int GetSelectCount(string name, string BeginTime, string EndTime, string timefrom, string timeto, string type)
        {

            string strSql = " where 1=1 ";
            strSql += name != "" ? " and strTrainmanName like @name " : " ";
            strSql += BeginTime != "" ? " and dtLeaveTime>=@BeginTime" : " ";
            strSql += EndTime != "" ? " and dtLeaveTime<=@EndTime" : " ";
            strSql += timefrom != "" ? " and nLeaveLength>=@timefrom" : " ";
            strSql += timeto != "" ? " and nLeaveLength<=@timeto" : " ";
            strSql += type != "" ? " and nLeaveTypeID=@type" : " ";

            string Sql = "select Count(*) from  VIEW_AskLeave " + strSql;
            SqlParameter[] sqlParams = {
                                       new SqlParameter("name","%"+name + "%"),
                                       new SqlParameter("BeginTime",BeginTime),
                                       new SqlParameter("EndTime",EndTime),
                                       new SqlParameter("timefrom",timefrom),
                                       new SqlParameter("timeto",timeto),
                                       new SqlParameter("type",type)
                                   };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams));
        }
    }
}