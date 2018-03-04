using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace TF.RunSafty.Trainman
{
    public class DBUtility
    {
        protected static SqlConnection GetConnection()
        {
            SqlConnection _Connection = null;
            try
            {
                var conn = ThinkFreely.DBUtility.SqlHelper.ConnString;
                if (conn != null && string.IsNullOrEmpty(conn) == false)
                    _Connection = new SqlConnection(conn);


                _Connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _Connection;
        }
    }
}
