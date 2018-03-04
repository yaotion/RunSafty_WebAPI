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

namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///GroupSortDiff功能：提供班序比对查
    /// </summary>
    public class GroupSortDiff
    {
        #region 扩展方法
        public DataTable GetDispatchHistory(string xdate, string wsguid,string type)
        {
            string strSql = "select * from VIEW_Plan_DispatchHistory where dtAutoSaveTime=@xdate and strWorkShopGUID=@wsguid and nGroupState in("+type+") order by nRowIndex asc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("xdate",xdate),
                                           new SqlParameter("wsguid",wsguid)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        #endregion
    }
}
