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
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///Drink 的摘要说明
    /// </summary>
    public class Drink
    {
        public Drink()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static object GetDrinkImage(string DrinkGUID)
        {
            string strSql = "select top 1 DrinkImage from TAB_Drink_Information where strGUID=@strGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",DrinkGUID)
                                       };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            return obj;
        }
    }
}