<%@ WebHandler Language="C#" Class="DataProcess" %>

using System;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;
public class DataProcess : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/json";
        string action = context.Request.Params["DataType"];
        string data = context.Request.Params["Data"];
        string cid = context.Request.Params["cid"];
        string result = "";
        IQueryResult iQuery = null;
        ResultFactory factory = new ResultFactory();
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        if (!string.IsNullOrEmpty(action) && factory.ContainsKey(action.ToLower()))
        {
            action = action.ToLower();
            iQuery = factory.GetFactory(action);
            if (iQuery != null)
            {
                iQuery.Data = data;
                iQuery.Context = context;
                iQuery.cid = cid;
                result = iQuery.QueryResult();
            }
        }
        else
        {
            result = "接口不存在，请检查参数DataType!";
        }
        watch.Stop();
        
        if (this.IsPrint(action))
        {
            string strWatch = string.Format("\r\n*接口名称:{0}\r\n*执行用时:{1}毫秒", action, watch.ElapsedMilliseconds);
            TF.CommonUtility.LogClass.log(strWatch);
        }
        context.Response.Write(result);
    }


    public bool IsPrint(string itemAPIName)
    {
        if (itemAPIName == "syncdoc")//获取文件
            return false;
        else if (itemAPIName == "getbanxu") //获取班序
            return false;
        else if (itemAPIName == "getledfile") //获取led文件
            return false;
        else if (itemAPIName == "getleave") //获取请假记录
            return false;
        return true;
    }
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}