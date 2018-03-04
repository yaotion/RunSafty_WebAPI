using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.NamePlate
{
    public class DBNameplateLimit
    {
        public static bool GetUserLimit(string UserNumber,NameplateLimit Limit)
        {
            string strSql =
                              "select * from TAB_Nameplate_TrainmanJiaolu_UserLimit where UserNumber = @UserNumber";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("UserNumber",UserNumber)

            };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Limit.UserName = TF.Utils.TFConvert.DBToString(dt.Rows[0]["UserName"], "");
                Limit.UserNumber = TF.Utils.TFConvert.DBToString(dt.Rows[0]["UserNumber"], "");
                Limit.SetNameboard = TF.Utils.TFConvert.DBToInt(dt.Rows[0]["SetNameboard"], 0);
                return true;
            }
            return false;
        }
    }
}
