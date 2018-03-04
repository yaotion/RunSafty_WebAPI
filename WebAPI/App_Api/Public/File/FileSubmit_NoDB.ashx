<%@ WebHandler Language="C#"  Class="TF.RunSaftyAPI.App_Api.Public.File.FileSubmit_NoDB" %>
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
using ThinkFreely.DBUtility;

namespace TF.RunSaftyAPI.App_Api.Public.File
{
    /// <summary>
    /// 文件上传,无数据库操作
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FileSubmit_NoDB : IHttpHandler
    {
      

        //传入参数
        public string sRequest = "";
        string strcid = "";
        string strfid = "";
        string strftype = "";
        //文件参数
        string serverPath = "";
        string uploadPath = "";
        string fileName = "";
        string orginName = "";  //原始文件名
        string filePath = "";
        string savePath = "";
        
        //是否是自定义文件 1，否  0，是  以及自定义文件的名称
        string isZiDingYi = "";
        string ZiDingYiName = "";

        HttpPostedFile file = null;

        public void ProcessRequest(HttpContext context)
        {
            try
            {
              
                //接收参数
                string strQuery = context.Request.QueryString.ToString().ToLower();
                if (strQuery == "help" || strQuery == "h") //帮助
                {
                    sRequest += gotoHelp();
                    return;
                }
                if (context.Request.Files.Count == 1)
                {
                    if (!string.IsNullOrEmpty(context.Request["cid"]))
                        strcid = context.Request["cid"];

                    if (!string.IsNullOrEmpty(context.Request["fid"]))
                        strfid = context.Request["fid"];

                    if (!string.IsNullOrEmpty(context.Request["ftype"]))
                        strftype = context.Request["ftype"];


                    if (!string.IsNullOrEmpty(context.Request["isZiDingYi"]))
                        isZiDingYi = context.Request["isZiDingYi"];

                    if (!string.IsNullOrEmpty(context.Request["ZiDingYiName"]))
                        ZiDingYiName = context.Request["ZiDingYiName"];
                    

                    file = context.Request.Files[0];


                    if (DBNull.Value.Equals(file))
                    {
                        context.Response.Write(sRetErrJSON("上传文件为空"));
                        return;
                    }
                  

                    sRequest = gotoFileSubmit_NoDB();
                }
            }
            finally
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(sRequest);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 文件上传,无数据库操作
        /// </summary>
        private string gotoFileSubmit_NoDB()
        {
            try
            {
                if (!string.IsNullOrEmpty(strftype))
                {
                    //  ftype:
                    //获取出勤计划=1001;获取退勤计划=1002;测酒=1003;出乘必读=1004;写卡=1005;通话记录=1006;运行记录转储=1007;
                    //;客户端更新=1100
                    //if (strftype == "1003" || strftype == "1004")
                    //{

                    if (isZiDingYi == "")
                    {
                        if (!SaveToFile(strftype))
                            return sRetErrJSON("保存文件错误");
                    }
                    else
                    {

                        if (!SaveToFileZiDingYi(strftype, ZiDingYiName))
                            return sRetErrJSON("保存文件错误");
                    }
                    
                    
                    
                    
                    //}
                }
                else
                {
                   
                    return sRetErrJSON("ftype错误");
                }
            }
            catch
            {
                return sRetErrJSON("上传时发生错误");
            }

            string strJson = "{\"result\":0,\"resultStr\":\"返回成功\",\"filePath\":\"" + savePath + "\",\"fileID\":\"" + strfid + "\",\"ftype\":\"" + strftype + "\",\"orginName\":\"" + orginName + "\"}";
           
            return strJson;
        }





        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="strft">文件类型</param>
        /// <returns></returns>
        bool SaveToFileZiDingYi(string strft, string ZiDingYiName)
        {
            //url的相对路径地址为: "/RunSaftyAPI/DrinkImage/"
            serverPath = "/uploadFile/" + strft + "/";
            uploadPath = HttpContext.Current.Server.MapPath(serverPath);

            fileName = "";
            filePath = "";

            if (file != null)
            {
                fileName = ZiDingYiName + System.IO.Path.GetExtension(file.FileName);
                orginName = file.FileName;
                filePath = uploadPath + fileName;
                savePath = serverPath + fileName;
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                file.SaveAs(filePath);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="strft">文件类型</param>
        /// <returns></returns>
        bool SaveToFile(string strft)
        {
            //url的相对路径地址为: "/RunSaftyAPI/DrinkImage/"
            serverPath = "/uploadFile/" + strft + "/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
            uploadPath = HttpContext.Current.Server.MapPath(serverPath);

            fileName = "";
            filePath = "";

            if (file != null)
            {
                fileName = System.Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                orginName = file.FileName;
                filePath = uploadPath + fileName;
                savePath = serverPath + fileName;
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                file.SaveAs(filePath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否存在的类型
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        private bool isExistType(string st)
        {
            string[] sType = { "1003", "1004"};
            foreach (string s in sType)
            {
                if (s == st)
                {
                    return true;
                }
            }
            return false;
        }

        //返回错误信息
        string sRetErrJSON(string strErr)
        {
            return "{\"result\":1,\"resultStr\":\"" + strErr + "\",\"filePath\":\"" + savePath + "\",\"fileID\":\"" + strfid + "\",\"ftype\":\"" + strftype + "\",\"orginName\":\"" + orginName + "\"}";
        }

        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <returns></returns>
        private string gotoHelp()
        {
            string strHelp = @"2.1	大文件上传
 调用参数
     参数格式
        cid=xxx&fid=&ftype=&f=

     参数说明

        cid:为客户端编号
        fid:文件编号
        ftype:文件类型(获取出勤计划=1001;获取退勤计划=1002;测酒=1003;出乘必读=1004;写卡=1005;通话记录=1006;运行记录转储=1007;)
        f:文件内容

 返回参数
     参数格式

        {'result':0,'resultStr':'返回成功','filePath':'','fileID':'','ftype':''}

        {'result':1,'resultStr':'返回错误'}

     参数说明
        filePath：文件在服务器存储的相对路径
        ftype：文件类型
";
            FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
            strHelp += "\r\n--------";
            strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
            strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
            return strHelp;
        }
    }
}

