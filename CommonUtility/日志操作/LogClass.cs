using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TF.CommonUtility
{
    /// <summary>
    ///PageBase 的摘要说明
    /// </summary>
    public class LogClass
    {
        private static object obj = new object();
        public LogClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 日志（字符串）
        /// </summary>
        /// <param name="_str"></param>
        public static void log(string _str)
        {
            string directory = "~/log/" + DateTime.Now.ToString("yyyy-MM") + "/";
            string filepath = directory + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string logFile = System.Web.HttpContext.Current.Server.MapPath(filepath);

            StreamWriter sw = null;
            try
            {
                lock (obj)
                {
                    if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(directory)))
                    {
                        System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(directory));
                    }


                    if (File.Exists(logFile))
                    {
                        sw = File.AppendText(logFile);
                    }
                    else
                    {
                        sw = File.CreateText(logFile);
                    }
                    sw.WriteLine(System.DateTime.Now.ToString() + "   IP:" + System.Web.HttpContext.Current.Request.UserHostAddress);
                    sw.WriteLine("出错信息：" + _str);
                    sw.WriteLine("---------------------------------------------------------------------");
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                    sw = null;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 日志（Exception）
        /// </summary>
        /// <param name="_str"></param>
        public static void logex(Exception erroy, string url)
        {
            string directory = "~/log/" + DateTime.Now.ToString("yyyy-MM") + "/";
            string filepath = directory + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string logFile = System.Web.HttpContext.Current.Server.MapPath(filepath);
            StreamWriter sw = null;
            try
            {
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(directory)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(directory));
                }
                if (File.Exists(logFile))
                {
                    sw = File.AppendText(logFile);
                }
                else
                {
                    sw = File.CreateText(logFile);
                }
                sw.WriteLine(System.DateTime.Now.ToString() + "   IP:" + System.Web.HttpContext.Current.Request.UserHostAddress);
                sw.WriteLine("出错页面是：" + url);
                if (erroy.InnerException != null)
                {
                    sw.WriteLine("出错信息：" + erroy.InnerException.Message);
                }
                else
                    sw.WriteLine("出错信息：" + erroy.Message);

                sw.WriteLine("Source:" + erroy.Source);
                sw.WriteLine("StackTrace：" + erroy.StackTrace);
                sw.WriteLine("---------------------------------------------------------------------");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
            }
            catch (Exception)
            {
            }
        }
    }
}


