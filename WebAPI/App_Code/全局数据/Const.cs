using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Runtime.InteropServices;

/// <summary>
///Const 的摘要说明
/// </summary>
public class ConstCommon
{
    public ConstCommon()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public static string TestIPString
    {
        get
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["TestIPString"] == null)
            {
                return "1.0.0.0000";
            }
            return System.Configuration.ConfigurationSettings.AppSettings["TestIPString"].ToString();
        }
    }


    public static string LingSuiXiuIPString
    {
        get
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["LingSuiXiuIPString"] == null)
            {
                return "1.0.0.0000";
            }
            return System.Configuration.ConfigurationSettings.AppSettings["LingSuiXiuIPString"].ToString();
        }
    }
}
