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
    public class ProofFiles
    {
        #region 扩展方法
        /// <summary>
        /// 根据where条件获取取证信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable ProofFilesRecord(string where)
        {
            string strSql = "select * from TAB_ProofFiles where 1=1 "+where;
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
        }
        #endregion
    }
}
