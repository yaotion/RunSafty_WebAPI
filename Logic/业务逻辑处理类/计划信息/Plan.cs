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
    public class Plan
    {
        public string TrainPlanGUID = "";
        public int NeedRest =0;
        public string TrainmanJiaoluGUID = "";
        public Plan()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public Plan(string TrainPlanGUID)
        {
            string strSql = "select top 1 * from TAB_Plan_Train where strTrainPlanGUID = @TrainPlanGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TrainPlanGUID",TrainPlanGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TrainPlanGUID = dt.Rows[0]["strTrainPlanGUID"].ToString();
                NeedRest = int.Parse(dt.Rows[0]["nNeedRest"].ToString());
                TrainmanJiaoluGUID = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();
            }
        }



        /// <summary>
        /// 查询计划信息
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
        public static DataTable QueryPlan(int PageIndex, int PageCount, string begintime, string endtime, string trainnumber, string traininformation, string planstate, string railway,
                string jcjl,string zclx,string isdc,string khtype,string islate,string TrainPlanGUID)
        {
            string strSql = " where 1=1 ";
            strSql += begintime != "" ? " and dtStartTime > @begintime " : " ";
            strSql += endtime != "" ? " and dtStartTime < @endtime " : " ";
            strSql += trainnumber != "" ? " and strTrainNo = @trainnumber" : " ";
            strSql += traininformation != "" ? " and strTrainTypeName + '-' + strTrainNumber like @traininformation" : " ";
            strSql += planstate != "" ? " and nPlanState = @planstate " : " ";
            strSql += railway != "" ? " and strLineGUID = @railway " : " ";
            strSql += jcjl != "" ? " and strTrainJiaoluGUID = @jcjl" : " ";
            strSql += zclx != "" ? " and nTrainmanTypeID = @zclx" : " ";
            strSql += isdc != "2" ? " and nNeedRest = @isdc" : " ";
            strSql += khtype != "" ? " and nKehuoID = @khtype" : " ";
            if (TrainPlanGUID != "")
                strSql += " and strTrainPlanGUID = @TrainPlanGUID ";
            if (islate != "")
            {
                strSql += islate == "0" ? " and DATEDIFF(second,dtStartTime,dtRealStartTime)>0 " : " and DATEDIFF(second,dtStartTime,dtRealStartTime)=0";
            }

            string Sql = "(select top " + (PageIndex * PageCount).ToString() +
                " * " +
                " from VIEW_Plan_Train_Site " + strSql +
                "  order by dtStartTime desc) as tmp2";
            Sql = "(select top " + PageCount.ToString() + " * from " + Sql + " order by dtStartTime) as tmp1";
            Sql = "select * from " + Sql + " order by dtStartTime desc";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("begintime",begintime ),
                                       new SqlParameter("endtime",endtime),
                                       new SqlParameter("trainnumber",trainnumber),
                                       new SqlParameter("traininformation","%"+traininformation+"%"),
                                       new SqlParameter("planstate",planstate),
                                       new SqlParameter("railway",railway),
                                       new SqlParameter("jcjl",jcjl),
                                       new SqlParameter("zclx",zclx),
                                       new SqlParameter("isdc",isdc),
                                       new SqlParameter("khtype",khtype),
                                       new SqlParameter("TrainPlanGUID",TrainPlanGUID)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];

        }

        /// <summary>
        /// 获取计划信息总数
        /// </summary>
        public static int GetplanCount()
        {
            string strSql = "SELECT COUNT(*) FROM VIEW_Plan_Train_Site";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql));
        }

        /// <summary>
        /// 计算查询条件记录总数
        /// </summary>
        /// <returns></returns>
        public static int GetSelectCount(string begintime, string endtime, string trainnumber, string traininformation, string planstate, string railway, string jcjl, string zclx, string isdc, string khtype, string islate,string TrainPlanGUID)
        {

            string strSql = " where 1=1 ";
            strSql += begintime != "" ? " and dtStartTime > @begintime " : " ";
            strSql += endtime != "" ? " and dtStartTime < @endtime " : " ";
            strSql += trainnumber != "" ? " and strTrainNo = @trainnumber" : " ";
            strSql += traininformation != "" ? " and strTrainNumber like @traininformation" : " ";
            strSql += planstate != "" ? " and nPlanState = @planstate " : " ";
            strSql += railway != "" ? " and strLineGUID = @railway " : " ";
            strSql += jcjl != "" ? " and strTrainJiaoluGUID = @jcjl" : " ";
            strSql += zclx != "" ? " and nTrainmanTypeID = @zclx" : " ";
            strSql += isdc != "2" ? " and nNeedRest = @isdc" : " ";
            strSql += khtype != "" ? " and nKehuoID = @khtype" : " ";
            if (TrainPlanGUID != "")
                strSql += " and strTrainPlanGUID = @TrainPlanGUID ";
            if (islate != "-请选择-")
            {
                strSql += islate == "0" ? " and DATEDIFF(second,dtStartTime,dtRealStartTime)>0 " : " and DATEDIFF(second,dtStartTime,dtRealStartTime)=0";
            }
            string Sql = "select Count(*) from  VIEW_Plan_Train_Site " + strSql;
            SqlParameter[] sqlParams = {
                                       new SqlParameter("begintime",begintime ),
                                       new SqlParameter("endtime",endtime),
                                       new SqlParameter("trainnumber",trainnumber),
                                       new SqlParameter("traininformation","%"+traininformation+"%"),
                                       new SqlParameter("planstate",planstate),
                                       new SqlParameter("railway",railway),
                                       new SqlParameter("jcjl",jcjl),
                                       new SqlParameter("zclx",zclx),
                                       new SqlParameter("isdc",isdc),
                                       new SqlParameter("khtype",khtype),
                                       new SqlParameter("TrainPlanGUID",TrainPlanGUID)
                                   };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams));
        }

        /// <summary>
        /// 查看计划日志
        /// </summary>
        /// <param name="TrainPlanGUID"></param>
        /// <returns></returns>
        public static DataTable QueryLog(string TrainPlanGUID)
        {
            string strSql = " where 1=1 ";
            strSql += TrainPlanGUID != "" ? " and strTrainPlanGUID = @TrainPlanGUID and nDeleteState=0 " : " ";
            string Sql = "select * from VIEW_Plan_log " + strSql + " order by FlowType";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("TrainPlanGUID",TrainPlanGUID)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 整备场获取机车计划
        /// </summary>
        /// <returns></returns>
        public static DataTable ZbcJcjh(string strWorkShopGUID)
        {
            string Sql = "select strTrainNo,dtStartTime,strTrainNumber from VIEW_Plan_Train where strWorkShopGUID=@strWorkShopGUID and nPlanState>=2 and nRemarkType=1 and dtStartTime>=getdate()  and dtStartTime<='" + DateTime.Now.AddHours(4) + "' order by dtStartTime";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];
        }


        /// <summary>
        /// 获取最近10天数据
        /// </summary>
        /// <returns></returns>
        public static DataTable JcjhInfo()
        {
            string Sql = "select CONVERT(varchar(100), dtStartTime, 23) as dtStartDate,count(*) as DatePlanCount from tab_plan_train where dtStartTime<=getdate() and dtStartTime > DATEADD(dd, -9,CONVERT(varchar(100), getdate(), 23)) and nPlanState >=4 group by CONVERT(varchar(100), dtStartTime, 23) order by CONVERT(varchar(100), dtStartTime, 23)";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql).Tables[0];
        }

    }
}