using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Runtime.InteropServices;


namespace TF.RunSafty.SpecificOnDuty
{
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


      


        public static string JiCheWeiZiZhanJieIPString
        {
            get
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["JiCheWeiZiZhanJieIPString"] == null)
                {
                    return "";
                }
                return System.Configuration.ConfigurationSettings.AppSettings["JiCheWeiZiZhanJieIPString"].ToString();
            }
        }

        public static string JiCheWeiZiKuJieIPString
        {
            get
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["JiCheWeiZiKuJieIPString"] == null)
                {
                    return "";
                }
                return System.Configuration.ConfigurationSettings.AppSettings["JiCheWeiZiKuJieIPString"].ToString();
            }
        }

    }
}