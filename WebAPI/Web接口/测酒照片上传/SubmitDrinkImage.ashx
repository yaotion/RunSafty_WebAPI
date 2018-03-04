<%@ WebHandler Language="C#" Class="SubmitDrinkImage" %>

using System;
using System.Web;
using System.IO;

public class SubmitDrinkImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

   

        HttpPostedFile file = context.Request.Files["drinkimg"];
        
        string serverPath = "/DrinkImage/" + DateTime.Now.Year.ToString() + "/" +DateTime.Now.Month.ToString() +"/" + DateTime.Now.Day.ToString() +"/";
        string uploadPath = HttpContext.Current.Server.MapPath(serverPath);
        string trainmanid = context.Request["tid"];
        string responseText = "{ \"nResult\":false,\"strResult\":\"\"}";
        if (file != null)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + trainmanid + System.IO.Path.GetExtension(file.FileName);
            string filePath = uploadPath + fileName;
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            file.SaveAs(filePath);
            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失            
            responseText = "{ \"nResult\":true,\"strResult\":\"" + serverPath + fileName + "\"}";
            TF.CommonUtility.LogClass.log(serverPath + fileName);
        }
        else
        {
            TF.CommonUtility.LogClass.log("测酒照片为空");
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(responseText);
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}