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
    public class exam
    {
        public exam()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //g 
        }
        /// <summary>
        /// 查询干部检查记录
        /// </summary>
        /// <returns></returns>
        public static DataTable QueryExam(int PageIndex, int PageCount,string name, string BeginTime, string EndTime, string area,string type)
        {
            string strSql = " where 1=1 ";
            strSql += name != "" ? " and strTrainmanName like @name " : " ";
            strSql += BeginTime != "" ? " and dtCreateTime>=@BeginTime" : " ";
            strSql += EndTime != "" ? " and dtCreateTime<=@EndTime" : " ";
            strSql += area != "" ? " and strAreaGUID=@area" : " ";
            strSql += type != "" ? " and nVerifyID=@type" : " ";

            string Sql = "(select top " + (PageIndex * PageCount).ToString() +
                " strTrainmanNumber, strTrainmanName,strVerifyName, strDutyName,strDutyNumber,strGUID,strLeaderGUID,strAreaGUID,nVerifyID,dtCreateTime,strDutyGUID,strAreaName " +
                " from VIEW_Exam_Information " + strSql +
                " GROUP BY strTrainmanNumber, strTrainmanName,strVerifyName, strDutyName,strDutyNumber,strGUID,strLeaderGUID,strAreaGUID,nVerifyID,dtCreateTime,strDutyGUID,strAreaName order by dtCreateTime desc) as tmp2";
            Sql = "(select top " + PageCount.ToString() + " * from " + Sql + " order by dtCreateTime) as tmp1";
            Sql = "select * from " + Sql + " order by dtCreateTime desc";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("name","%"+name + "%"),
                                       new SqlParameter("BeginTime",BeginTime),
                                       new SqlParameter("EndTime",EndTime),
                                       new SqlParameter("area",area),
                                       new SqlParameter("type",type)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];

        }

        /// <summary>
        /// 获取干部检查记录总数
        /// </summary>
        public static int GetexamCount()
        {
            string strSql = "select Count(*) from (select strGUID from VIEW_Exam_Information  Group by strGUID ) as tmp1";

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql));
        }

        /// <summary>
        /// 计算查询条件记录总数
        /// </summary>
        /// <returns></returns>
        public static int GetexamCount(string name, string BeginTime, string EndTime, string area, string type)
        {

            string strSql = " where 1=1 ";
            strSql += name != "" ? " and strTrainmanName like @name " : " ";
            strSql += BeginTime != "" ? " and dtCreateTime>=@BeginTime" : " ";
            strSql += EndTime != "" ? " and dtCreateTime<=@EndTime" : " ";
            strSql += area != "" ? " and strAreaGUID=@area" : " ";
            strSql += type != "" ? " and nVerifyID=@type" : " ";

            string Sql = "select Count(*) from (select strGUID from VIEW_Exam_Information "+strSql+" ) as tmp1";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("name","%"+name + "%"),
                                       new SqlParameter("BeginTime",BeginTime),
                                       new SqlParameter("EndTime",EndTime),
                                       new SqlParameter("area",area),
                                       new SqlParameter("type",type)
                                   };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams));
        }
    }
}