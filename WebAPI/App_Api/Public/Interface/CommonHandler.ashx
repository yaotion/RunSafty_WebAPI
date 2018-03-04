<%@ WebHandler Language="C#" Class="CommonHandler" %>

using System;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;
    
    
public class CommonHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/json";
        string action = context.Request.Params["Action"];
        string data = context.Request.Params["Data"];
        string result = "";
        IQueryResult iQuery = null;
        ResultFactory factory = new ResultFactory();
        if (!string.IsNullOrEmpty(action) && factory.ContainsKey(action))
        {
            iQuery = (IQueryResult)factory[action];
            iQuery.Data = data;
            result = iQuery.QueryResult();
        }
        context.Response.Write(result);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}