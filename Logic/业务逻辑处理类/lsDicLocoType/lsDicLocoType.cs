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

namespace TF.RunSafty.Logic
{
    /// <summary>
    /// 类名：lsDicLocoType
    /// 描述：机车类型数据库操作类
    /// </summary>
    public class lsDicLocoType
    {
        const string PAGEID = "72";

        #region  扩展方法
        /// <summary>
        /// 获取所有机车类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllType()
        {
            string strSql = "select distinct * from [lsDicLocoType] order by [LocoType] asc";
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(PAGEID), CommandType.Text, strSql).Tables[0];
        }
        #endregion
    }

}
