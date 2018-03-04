<%@ WebHandler Language="C#" Class="IsAccess" %>

using System;
using System.Web;
using ThinkFreely.DBUtility;
public class IsAccess : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/json";

        result r = new result();
        if (LoginIn() == "1")
        {
            r.nResult = 0;
            r.strResult = "数据库访问成功！";
        }
        else
        {
            r.strResult = "数据库访问失败，请检查连接设置！";
            r.nResult = 1;
        }

        string s = Newtonsoft.Json.JsonConvert.SerializeObject(r);
        context.Response.Write(s);
    }


    public string LoginIn()
    {
        try
        {
            string strSql = "select 1 ";
            int nCount = SqlHelper.ExecuteDataset(SqlHelper.ConnString, System.Data.CommandType.Text, strSql).Tables[0].Rows.Count;
            return "1";
        }
        catch
        {
            return "2";
        }
    }

    public class result
    {
        public string strResult;
        public int nResult; 
    
    }
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}