<%@ WebHandler Language="C#" Class="GetNewVersion" %>

using System;
using System.Web;
using ThinkFreely.RunSafty;

public class GetNewVersion : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string responseString = "{\"NeedUpdate\":false,\"strProjectVersion\":\"\",\"strUpdateBrief\":\"未知的版本信息\",\"strPackageUrl\":\"\",\"strMainExeName\":\"\"}";
        try
        {

            string strProjectID = GetProjectID(context);
            string strProjectVersion = GetProjectVersion(context);
            DBUpdateVersion newVersion = new DBUpdateVersion();

            if (DBUpdateVersion.GetProjectVersion(strProjectID, newVersion))
            {
                if (newVersion.strProjectVersion != strProjectVersion)
                {
                    responseString = "{\"NeedUpdate\":true,\"strProjectVersion\":\"" + newVersion.strProjectVersion + "\",\"strUpdateBrief\":\"" +
                        newVersion.strUpdateBrief + "\",\"strPackageUrl\":\"http://" + context.Request.Url.Authority + newVersion.strPackageUrl + "\",\"strMainExeName\":\"" + newVersion.strMainExeName + "\"}";
                }
            }

        }
        finally
        {


            context.Response.ContentType = "text/plain";
            context.Response.Write(responseString);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public string GetProjectID(HttpContext context)
    {        
        if (context.Request["pid"] == null)
            return "";

        return context.Request["pid"].ToString();
    }

    public string GetProjectVersion(HttpContext context)
    {
        if (context.Request["version"] == null)
            return "";
        return context.Request["version"].ToString();
    }

}