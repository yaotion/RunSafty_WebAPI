using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TF.RunSafty.DBUtils
{
    /// <summary>
    /// 数据操作基类
    /// </summary>
    public class DBOperator
    {
        //连接字符串
        public string ConnectionString = "";
        public DBOperator(string strConnectionString)
        {
            ConnectionString = strConnectionString;
        }

    }
}
