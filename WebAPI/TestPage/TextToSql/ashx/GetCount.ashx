<%@ WebHandler Language="C#" Class="GetCount" %>

using System;
using System.Web;
using ThinkFreely.DBUtility;
using System.Data;

public class GetCount : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write(getCount());
    }

    public string getCount()
    {
        System.Text.StringBuilder strSql = new System.Text.StringBuilder();
        strSql.Append("SELECT count(1)  FROM  [Tab_System_TxtLog]");
        string strCount = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString())).ToString();
        return "当前数据库中共有数据" + strCount.ToString() + "条";
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}