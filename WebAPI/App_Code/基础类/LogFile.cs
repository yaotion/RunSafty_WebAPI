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
using System.IO;

namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    /// 写入文本流日志
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    public class LogFile
    {

        public string path =  "";
        public bool isOpenLog = false;

        public LogFile(string logPath, bool openLog)
        {
            path = logPath;
            isOpenLog = openLog;
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log">写入的内容</param>
        public void WriteLog(string log)
        {
            try
            {
                if (!isOpenLog)   //是否开启日志
                    return;

                string sp = HttpContext.Current.Server.MapPath("~");
                path = sp + "DrinkImage\\Log\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path += DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                //创建文件流
                FileStream myfs = new FileStream(path, FileMode.Append);
                //创建写入器
                StreamWriter mySw = new StreamWriter(myfs);
                //写入文件
                mySw.WriteLine(log);
                //关闭写入器
                mySw.Close();
                //关闭文件流
                myfs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取日志
        /// </summary>
        public string ReadLog()
        {
            string log = "";
            try
            {

                //创建文件流
                FileStream myfs = new FileStream(path, FileMode.Open);
                //创建读入器
                StreamReader mySr = new StreamReader(myfs);
                //读文件
                log = mySr.ReadToEnd();
                //关闭写入器
                mySr.Close();
                //关闭文件流
                myfs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return log;
        }
    }
}
