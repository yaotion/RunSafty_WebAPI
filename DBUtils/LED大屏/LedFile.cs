using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TF.Api.Entity;
using ThinkFreely.DBUtility;

namespace TF.Api.DBUtility
{
    public class ILedFile
    {
        public DataTable GetLedFileList(string clientId)
        {
            string sqlCommandText = string.Format("select * from VIEW_LED_Files_Clients where clientid=@clientid");
            SqlParameter[] sqlParams = {
                                           new SqlParameter("clientid",clientId)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams).Tables[0];
        }
    }
}
