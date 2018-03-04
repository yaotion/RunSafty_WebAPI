<%@ WebHandler Language="C#" Class="GetListInfo" %>

using System;
using System.Web;
using ThinkFreely.DBUtility;
using System.Data;

public class GetListInfo : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/json";

        int pageIndex = int.Parse(context.Request["page"].ToString());
        int pageCount = int.Parse(context.Request["rows"].ToString());
        
        string strName = "";
        if (context.Request["strName"] == null)
            strName = "";
        else
            strName = context.Request["strName"].ToString();
        context.Response.Write(OutputJson(pageIndex, pageCount,strName));
    }

    private string OutputJson(int pageIndex,int pageCount,string strName)
    {
        string strHTML = string.Empty;
        System.Text.StringBuilder strSql = new System.Text.StringBuilder();
        DataTable dt = Query(pageIndex, pageCount, strName);
        int nCount = QueryCount(strName);
        strHTML = TF.CommonUtility.JsonConvert.static_SerializeP(dt, nCount);
        return strHTML;
    }


    public DataTable Query(int pageIndex, int pageCount, string strName)
    {
        string strWhere = " ";
        if (strName != "")
        {
            strWhere += " and strname = '" + strName + "' ";
        }
        string sql = @"select top  " + pageCount.ToString() + "* from Tab_System_TxtLog  where 1=1  " + strWhere + " and nID not in (select top  " + ((pageIndex - 1) * pageCount).ToString() + " nID from Tab_System_TxtLog where 1=1 " + strWhere + " order by nID) order by nID";

        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
        return dt;
    }


    public int QueryCount(string strName)
    {
        string strWhere = " ";
        if (strName != "")
        {
            strWhere += " and strname = '" + strName + "' ";
        }

        System.Text.StringBuilder strSql = new System.Text.StringBuilder();
        strSql.Append(@"select count (*) from Tab_System_TxtLog where  1=1 " + strWhere + "");
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
    }
    

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}