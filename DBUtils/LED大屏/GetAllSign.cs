using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.DAL
{
    public partial class GetAllSign
    {
        public DataTable GetJiaoLuForLED(string StrGUID)
        {
            string strSql = "select * from TAB_Base_TrainJiaolu";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }
    }
}
