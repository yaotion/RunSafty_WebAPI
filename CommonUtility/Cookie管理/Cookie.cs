using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace TF.CommonUtility
{
    /// <summary>
    ///Cookie 的摘要说明
    /// </summary>
    public class Cookie
    {
        public Cookie()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string ReadCookie(string cookieName, string itemName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null) return "";
            return Convert.ToString(HttpContext.Current.Request.Cookies[cookieName].Values[itemName]);
        }

        public static void WriteCookie(Page page, string cookieName, string itemName, string itemValue, long ValidSecond)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddSeconds(ValidSecond);
            cookie.Values.Add(itemName, itemValue);
            page.Response.AppendCookie(cookie);
        }
    }
}