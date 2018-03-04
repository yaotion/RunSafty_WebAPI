using System;
using System.Text;
using System.Linq;
using TF.CommonUtility;
using ThinkFreely.DBUtility;

using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.NamePlate.MD;

namespace TF.RunSafty.NamePlate.DB
{
    public class DBTuiQinTimeLog
    {
        public static void Save(TuiQinTimeLog Log)
        {
            string strSql = @"insert into Tab_Plan_ModifyLastArriveTime_Log (strGroupGUID,nType,dtOldArriveTime,dtNewArriveTime,strDutyUserNumber,strDubyUserName,dtCreateTime) 
            values (@strGroupGUID,@nType,@dtOldArriveTime,@dtNewArriveTime,@strDutyUserNumber,@strDubyUserName,@dtCreateTime)";

            SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("strGroupGUID",Log.strGroupGUID),
                    new SqlParameter("nType",Log.nType),
                    new SqlParameter("dtOldArriveTime",Log.dtOldArriveTime),
                    new SqlParameter("dtNewArriveTime",Log.dtNewArriveTime),
                    new SqlParameter("strDutyUserNumber",Log.strDutyUserNumber),
                    new SqlParameter("strDubyUserName",Log.strDutyUserName),
                    new SqlParameter("dtCreateTime",Log.dtCreateTime)
                };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }
    }
}
