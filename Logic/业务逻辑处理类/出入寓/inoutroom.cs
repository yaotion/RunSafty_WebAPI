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
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///Trainman 的摘要说明
    /// </summary>
    public class inoutroom
    {
        public inoutroom()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

/// <summary>
        /// 查询出入寓信息
/// </summary>
/// <param name="PageIndex"></param>
/// <param name="PageCount"></param>
/// <param name="number"></param>
/// <param name="name"></param>
/// <param name="trainnumber"></param>
/// <param name="jcjl"></param>
/// <param name="ssarea"></param>
/// <param name="begintime"></param>
/// <param name="endtime"></param>
/// <returns></returns>
        public static DataTable Queryinout(int PageIndex, int PageCount, string number, string name, string trainnumber, string jcjl, string ssarea, string begintime, string endtime,string pid)
        {
            string where = " where 1=1 ";
            where += begintime != "" ? " and dtArriveTime >= @begintime " : " ";
            where += endtime != "" ? " and dtArriveTime <= @endtime " : " ";
            where += trainnumber != "" ? " and strTrainNo like @trainnumber" : " ";
            where += name != "" ? " and (strTrainmanName1 like @strTrainmanName or strTrainmanName2 like @strTrainmanName or strTrainmanName3 like @strTrainmanName)" : " ";
            where += number != "" ? " and (strTrainmanNumber1 like @strTrainmanNumber or strTrainmanNumber2 like @strTrainmanNumber or strTrainmanNumber3 like @strTrainmanNumber) " : " ";
            where += jcjl != "" ? " and strTrainJiaoluGUID = @jcjl " : " ";
            where += ssarea != "" ? " and strAreaGUID = @ssarea" : " ";
            where += pid != "" ? " and strTrainPlanGUID = @pid" : " ";

            string strSql = "SELECT TOP " + PageCount + " * FROM VIEW_Plan_InOut " + where + " and strTrainJiaoluGUID >(SELECT ISNULL(MAX(strTrainJiaoluGUID),0) FROM (SELECT TOP " + PageCount * (PageIndex - 1) + " strTrainJiaoluGUID FROM VIEW_Plan_InOut " + where + " ORDER BY strTrainJiaoluGUID asc)A) ORDER BY strTrainJiaoluGUID asc";
            strSql = "select * from (" + strSql + ")B order by dtArriveTime desc";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("begintime",begintime ),
                                       new SqlParameter("endtime",endtime),
                                       new SqlParameter("trainnumber","%"+trainnumber+"%"),
                                       new SqlParameter("strTrainmanName","%"+name+"%"),
                                       new SqlParameter("strTrainmanNumber","%"+number+"%"),
                                       new SqlParameter("jcjl",jcjl),
                                       new SqlParameter("ssarea",ssarea),
                                       new SqlParameter("pid",pid)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }

        /// <summary>
        /// 获取出入寓信息总数
        /// </summary>
        public static int GetinoutCount()
        {
            string strSql = "SELECT COUNT(*) FROM VIEW_Plan_InOut";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql));
        }

        /// <summary>
        /// 计算查询条件记录总数
        /// </summary>
        /// <returns></returns>
        public static int GetSelectCount(string number, string name, string trainnumber, string jcjl, string ssarea, string begintime, string endtime,string pid)
        {
            string where = " where 1=1  and nNeedRest=1 ";
            where += begintime != "" ? " and dtArriveTime > @begintime " : " ";
            where += endtime != "" ? " and dtArriveTime < @endtime " : " ";
            where += trainnumber != "" ? " and strTrainNo like @trainnumber" : " ";
            where += name != "" ? " and (strTrainmanName1 like @strTrainmanName or strTrainmanName2 like @strTrainmanName or strTrainmanName3 like @strTrainmanName)" : " ";
            where += number != "" ? " and (strTrainmanNumber1 like @strTrainmanNumber or strTrainmanNumber2 like @strTrainmanNumber or strTrainmanNumber3 like @strTrainmanNumber) " : " ";
            where += jcjl != "" ? " and strTrainJiaoluGUID = @jcjl " : " ";
            where += ssarea != "" ? " and strAreaGUID = @ssarea" : " ";
            where += pid != "" ? " and strTrainPlanGUID = @pid" : " ";
            string Sql = "select Count(*) from  VIEW_Plan_InOut " + where;
            SqlParameter[] sqlParams = {
                                       new SqlParameter("begintime",begintime ),
                                       new SqlParameter("endtime",endtime),
                                       new SqlParameter("trainnumber","'"+trainnumber+"'"),
                                       new SqlParameter("strTrainmanName","'"+name+"'"),
                                       new SqlParameter("strTrainmanNumber","'"+number+"'"),
                                       new SqlParameter("jcjl",jcjl),
                                       new SqlParameter("ssarea",ssarea),
                                       new SqlParameter("pid",pid)
                                   };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams));
        }



        

    }
}