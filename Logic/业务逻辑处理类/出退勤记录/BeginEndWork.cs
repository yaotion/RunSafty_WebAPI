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
///EndWork 的摘要说明
/// </summary>
    public class BeginEndWork
    {
        public BeginEndWork()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static object GetPicture(string guid)
        {
            string strSql = "select strImagePath from TAB_Drink_Information where strWorkID=@guid";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("guid",guid)
                                          };
            object  obj =  SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameter);
            if ((obj == null) || (DBNull.Value.Equals(obj))) return null;
            string fileName = (string)obj;
            if (fileName != "")
            {
                fileName = HttpContext.Current.Server.MapPath(fileName);
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    return buffer;
                }
            }
            return null;
        }

    }
}
