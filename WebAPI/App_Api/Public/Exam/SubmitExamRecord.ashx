<%@ WebHandler Language="C#" Class="SubmitExamRecord" %>
using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using ThinkFreely.DBUtility;
using ThinkFreely.RunSafty;
using TF.Api.Utilities;
using TF.Api.Entity;

/// <summary>
/// 类名：SubmitExamRecord
/// 描述：试题提交接口
/// </summary>
public class SubmitExamRecord : IHttpHandler
{

    /// <summary>
    /// 提交信息实体类
    /// </summary>
    public class SubmitEntity
    {
        public int stepID { get; set; }
        public List<ExamRecordEntity> data { get; set; }
    }

    /// <summary>
    /// 返回结果实体类
    /// </summary>
    public class ResponseEntity
    {
        public int result { get; set; }
        public string resultStr { get; set; }
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        string clientID = PageBase.static_ext_string(context.Request["cid"]);
        string requestJsonData = PageBase.static_ext_string(context.Request["data"]);

        ResponseEntity resEntity = new ResponseEntity();

        string exceptInfo = string.Empty;
        resEntity.result = 0;
        resEntity.resultStr = "返回成功";
        //反序列化接受到的数据
        SubmitEntity submitEntity = new JavaScriptSerializer().Deserialize<SubmitEntity>(requestJsonData);
        //循环插入考试记录
        foreach (ExamRecordEntity recordEntity in submitEntity.data)
        {
            if(!TF.Api.BLL.ExamRecordBLL.Add(recordEntity, out exceptInfo))
            {
                 resEntity.result = 1;
                resEntity.resultStr = exceptInfo;
                break;
            }
        }
        context.Response.Write(new JavaScriptSerializer().Serialize(resEntity));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}