using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Data;

namespace TF.RunSafty.BeginworkFlow
{
    public class CheckCard
    {
        public void AddStepResult(card StepResult)
        {

            //添加验卡记录前
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("  update   TAB_Plan_RunEvent_IDICCard set strTrainPlanGUID='" + Guid.NewGuid().ToString() + "' where strTrainPlanGUID='" + StepResult.strTrainPlanGUID + "'");
            SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql1.ToString());

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_RunEvent_IDICCard");
            strSql.Append("(strTrainPlanGUID,dtCreateTime,dtEventTime,strEventBrief,strTrainmanName,strTrainmanNumber)");
            strSql.Append("values(@strTrainPlanGUID,@dtCreateTime,@dtEventTime,@strEventBrief,@strTrainmanName,@strTrainmanNumber)");
            SqlParameter[] parameters = {
                  new SqlParameter("@strTrainPlanGUID", StepResult.strTrainPlanGUID),
                  new SqlParameter("@dtCreateTime", StepResult.dtCreateTime),
                  new SqlParameter("@dtEventTime", StepResult.dtEventTime),
                  new SqlParameter("@strEventBrief", StepResult.strEventBrief),
                  new SqlParameter("@strTrainmanName", StepResult.strTrainmanName),
                  new SqlParameter("@strTrainmanNumber", StepResult.strTrainmanNumber)
                                        };
            SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }

    }

  public class card
  {
      public string strTrainPlanGUID;
      public string dtCreateTime;
      public string dtEventTime;
      public string strEventBrief;
      public string strTrainmanName;
      public string strTrainmanNumber;

  }
}
