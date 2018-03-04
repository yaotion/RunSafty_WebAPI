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
    ///OneWorkTime功能：提供趟劳详细信息查询
    /// </summary>
    public class OneWorkTime
    {
        #region 扩展方法
        public static DataTable GetDetailFromstrflowid(string strTrainPlanGUID)
        {
            string strSql = "select * from VIEW_Plan_RunEvent where 1=1 ";
            if (strTrainPlanGUID != "")
            {
                strSql += " and strTrainPlanGUID = @strTrainPlanGUID ";
            }
            strSql += " order by dteventtime asc ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainPlanGUID",strTrainPlanGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }


        public static DataTable GetTlsInfo(string where)
        {
            string strSql = "select  * from View_WorkTime_Turn where 1=1 and dtEndWorkTime is not null and strTrainJiaoluGUID not in (select strTrainJiaoluGUID from TAB_WorkTime_MonthSectionFilter)  " + where;
           
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        #endregion
    }
}
