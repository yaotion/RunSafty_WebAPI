using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.Plan;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Data; 

namespace TF.Runsafty.Plan
{
   public class DBDispatchStation
    {
       #region （添加调度台统计信息）
       public bool AddDispatchStation(LCDispatchStation.Get_In_AddDispatchStation model)
       {
           string strCreateSiteGUID = "";
           string strStartTime = "";
           string strSql = "select strCreateSiteGUID,dtStartTime from TAB_Plan_Train where strTrainPlanGUID='" + model.Key + "'";
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
           if (dt.Rows.Count > 0)
           {
               strCreateSiteGUID = dt.Rows[0]["strCreateSiteGUID"].ToString();
               strStartTime = dt.Rows[0]["dtStartTime"].ToString();
           }
           else
           {
               return false;
           }


           strSql = "insert into Tab_Plan_SiteWorkInfo (strTrainPlanGUID,strType,strSiteGuid,dtStarttime) values (@strTrainPlanGUID,@strType,@strSiteGuid,@dtStarttime)";
           SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("strTrainPlanGUID",model.Key),
                new SqlParameter("strType",model.Action),
                new SqlParameter("strSiteGuid",strCreateSiteGUID),
                new SqlParameter("dtStarttime",strStartTime)
            };
           return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters) > 0;
       }
       #endregion
    }
}
