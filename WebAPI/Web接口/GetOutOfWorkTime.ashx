<%@ WebHandler Language="C#" Class="GetOutOfWorkTime" %>

using System;
using System.Web;

public class GetOutOfWorkTime : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) 
    {
        PageBase p = new PageBase();
        string result = "[]";
        try
        {
            try
            {

                result = WebInterface.GetOutOfWorkTimeJson();
            }
            catch (Exception e)
            {
                result = "[]";
            }
        }
        finally
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}