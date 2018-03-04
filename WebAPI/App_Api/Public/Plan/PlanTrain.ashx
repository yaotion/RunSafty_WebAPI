<%@ WebHandler Language="C#" Class="PlanTrain" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility; 
using TF.Api.Utilities;

public class PlanTrain : IHttpHandler {
    int Hour = 4;
    public class ErrorJson
    {
        public string Success;
        public string ResultText;
    }
    public class Result
    {
        public int Success;
        public string ResultText;
        public int total;
        public DataTable Items;
    }
    
    /// <summary>
    /// 返回错误信息
    /// </summary>
    /// <param name="context"></param>
    /// <param name="errMsg"></param>
    public void ShowError(HttpContext context, string errMsg)
    {
        ErrorJson error=new ErrorJson();
        error.Success="0";
        error.ResultText=errMsg;
        string json=Newtonsoft.Json.JsonConvert.SerializeObject(error);
        context.Response.Write(json);
        context.Response.End();
    }

    /// <summary>
    /// 获取workshop
    /// </summary>
    /// <param name="context">上下文</param>
    /// <returns></returns>
    public string GetWorkShopGUIDS(HttpContext context)
    {
        string strWorkShopGUID = context.Request.Params["strWorkShopGUID"];
        
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        if (!string.IsNullOrEmpty(strWorkShopGUID))
        {
            string[] tmp = strWorkShopGUID.Split(',');
            for (int i = 0; i < tmp.Length; i++)
            {
                builder.AppendFormat("'{0}'", tmp[i]);
                if (i < tmp.Length - 1)
                    builder.Append(",");
            }
        }
        return builder.ToString();
    }
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";
        string strHour = context.Request.Params["Hours"];
        if (!int.TryParse(strHour, out Hour))
        {
             ShowError(context, "Hour参数不正确");
             return;
        }
        string strWorkShopGUID =GetWorkShopGUIDS(context);
        if (string.IsNullOrEmpty(strWorkShopGUID))
        {
            ShowError(context, "strWorkShopGUID参数不正确");
            return;
        }
        Result result = new Result();
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        try
        {
            string strSql = string.Format(@"select * from VIEW_Plan_Train where strWorkShopGUID in ({0})
        and nPlanState>=2 and nRemarkType=1 and dtStartTime>=getdate()  and dtStartTime<='{1}' order by dtStartTime", strWorkShopGUID, DateTime.Now.AddHours(Hour));       
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];           
            result.Success =1;
            result.ResultText = "成功";
            result.total = dt.Rows.Count;
            result.Items = dt;
           Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(result,Newtonsoft.Json.Formatting.None,timeConverter);
            //LogHelper.log.Info(json);
            context.Response.Write(json);
            context.ApplicationInstance.CompleteRequest();
        }
        catch (System.Threading.ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
           // LogHelper.log.Error(ex);
            ShowError(context, "内部错误，请检查日志");
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}