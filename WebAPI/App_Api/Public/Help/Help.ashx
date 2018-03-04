<%@ WebHandler Language="C#"  Class="TF.RunSaftyAPI.Help" %>
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using System.IO;

namespace TF.RunSaftyAPI
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Help : IHttpHandler
    {
        StringBuilder sb = new StringBuilder();
        String[] sDir = null;
        String[] sFile = null;

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                FindFile(HttpContext.Current.Server.MapPath("~") + "App_Api\\Public\\");
            }
            finally
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
            }
        }

        public void FindFile(string dirPath) //参数dirPath为指定的目录
        {
            DirectoryInfo Dir = new DirectoryInfo(dirPath);
            try
            {
                foreach (DirectoryInfo d in Dir.GetDirectories())//查找子目录 
                {
                    string fDir = Dir.ToString() + d.ToString() + "\\";
                    //sb.AppendLine(fDir);
                    FindFile(fDir);
                    //sDir.SetValue(fDir,0);
                }
                foreach (FileInfo f in Dir.GetFiles("*.ashx")) //查找文件类型
                {
                    string dFile = HttpContext.Current.Request.MapPath(f.DirectoryName);
                    sb.AppendLine(dFile);
                }
            }
            catch (Exception e)
            {
                sb.AppendLine(e.Message);
            }
        } 

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
