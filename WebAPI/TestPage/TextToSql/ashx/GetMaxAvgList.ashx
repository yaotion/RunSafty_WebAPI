<%@ WebHandler Language="C#" Class="GetMaxAvgList" %>

using System;
using System.Web;
using ThinkFreely.DBUtility;
using System.Data;

public class GetMaxAvgList : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write(OutputJson());
    }
    
    
    private string OutputJson()
    {
        string strHTML = string.Empty;
        System.Text.StringBuilder strSql = new System.Text.StringBuilder();
        strSql.Append("SELECT strName,AVG(nTimes) as AvgTimes,COUNT(*) as strTimes  FROM [Tab_System_TxtLog] group by strName order by AvgTimes desc");
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        strHTML = TF.CommonUtility.JsonConvert.Serialize(dt);
        return strHTML;
    }
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}