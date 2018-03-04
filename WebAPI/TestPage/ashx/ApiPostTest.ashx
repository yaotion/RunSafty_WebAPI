<%@ WebHandler Language="C#" Class="ApiPostTest" %>

using System;
using System.Web;
using System.Net;
using System.Text;
using System.IO;

public class ApiPostTest : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string formUrl = "http://localhost:7777/AshxService/QueryProcess.ashx";
      
        string data = context.Request["data"];
        string DataType = context.Request["DataType"];
        data = "DataType=" + DataType + "&data=" + data;
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(data);
        HttpWebRequest request = WebRequest.Create(formUrl) as System.Net.HttpWebRequest;
        System.Text.Encoding myEncoding = Encoding.UTF8;
        request.Method = "POST";
        request.KeepAlive = false;
        request.AllowAutoRedirect = true;
        request.ContentType = "application/x-www-form-urlencoded";
        System.IO.Stream outputStream = request.GetRequestStream();
        outputStream.Write(postData, 0, postData.Length);
        outputStream.Close();

        HttpWebResponse response;
        Stream responseStream;
        StreamReader reader;
        string srcString;
        response = request.GetResponse() as HttpWebResponse;
        responseStream = response.GetResponseStream();
        reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
        srcString = reader.ReadToEnd();
        string result = srcString;   //返回值赋值
        reader.Close();
        context.Response.ContentType = "text/plain";
        context.Response.Write(result);
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}