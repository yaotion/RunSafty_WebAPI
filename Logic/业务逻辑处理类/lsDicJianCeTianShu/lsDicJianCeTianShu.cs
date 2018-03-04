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
using System.Collections.Generic;

namespace TF.RunSafty.Logic
{
    public class lsDicJianCeTianShu
    {
        #region 构造函数
        public lsDicJianCeTianShu()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static DataTable GetlsDicJianCeTianShu(string locotype,int unitid)
        {
            string strSql = "select * from lsDicJianCeTianShu WHERE (LocoType = @LocoType) AND (UnitId = @UnitId) AND (Status = 1)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("LocoType",locotype),
                                           new SqlParameter("UnitId",unitid)
                                       };
            return  SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        #endregion
    }
}
