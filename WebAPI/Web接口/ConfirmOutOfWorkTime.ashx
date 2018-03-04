<%@ WebHandler Language="C#" Class="ConfirmOutOfWorkTime" %>

using System;
using System.Web;

public class ConfirmOutOfWorkTime : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) 
    {
        PageBase p = new PageBase();
        string result = "{ \"nResult\":\"0\",\"strResult\":\"提交成功\"}";
        try
        {
            try
            {
                string nid = p.ext_string(context.Request["nid"]);
                WebInterface.ConfirmOutOfWorkTime(nid);
            }
            catch (Exception e)
            {
                result = "{ \"nResult\":\"1\",\"strResult\":\"" + e.Message + "\"}";
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