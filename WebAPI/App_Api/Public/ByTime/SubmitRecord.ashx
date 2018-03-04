<%@ WebHandler Language="C#" Class="SubmitRecord" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using ThinkFreely.DBUtility;
using ThinkFreely.RunSafty;
using TF.Api.Entity;
using TF.Api.Utilities;


#region
/// <summary>
/// 提交信息实体类
/// </summary>
public class SubmitItemEntity
{
    public int stepID { get; set; }
    public object data { get; set; }
}

/// <summary>
/// 返回结果实体类
/// </summary>
public class ResponseEntity
{
    public int result { get; set; }
    public string resultStr { get; set; }
}
#endregion


/// <summary>
/// 类名：SubmitRecord
/// 说明：提交添乘记录
/// </summary>
public class SubmitRecord : IHttpHandler
{
    ResponseEntity responseEntity;
    public void ProcessRequest(HttpContext context)
    {
        ResponseHelper.SetJsonHeader();

        int clientID = RequestHelper.GetRequestToInt("cid");
        string data = RequestHelper.GetRequestToString("data");
        
        responseEntity=new ResponseEntity();

        List<SubmitItemEntity > submitItemEntityLst=GetSubmitItemEntityLst(data);
        if (submitItemEntityLst == null)
        {
            responseEntity.result=1;
            responseEntity.resultStr="提交数据为空";
            context.Response.Write(new JavaScriptSerializer().Serialize(responseEntity));
            return;
        }
        else
        {
            ProcessData(submitItemEntityLst);
            context.Response.Write(new JavaScriptSerializer().Serialize(responseEntity));
        }
    }

    #region 获取提交实体列表
    /// <summary>
    /// 获取提交实体列表
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    private List<SubmitItemEntity> GetSubmitItemEntityLst(string strJson)
    {
        List<SubmitItemEntity> submitItemLst;
        try
        {
            submitItemLst = new JavaScriptSerializer().Deserialize<List<SubmitItemEntity>>(strJson);
            return submitItemLst;
        }
        catch (Exception ex)
        {
            responseEntity.result = 1;
            responseEntity.resultStr = ex.ToString();
            submitItemLst = null;
            return submitItemLst;
        }
    }
    #endregion


    /// <summary>
    /// 处理数据
    /// </summary>
    /// <param name="strJsonData"></param>
    private void ProcessData(List<SubmitItemEntity> submitItemLst)
    {
        foreach (SubmitItemEntity submitItem in submitItemLst)
        {
            responseEntity.result = 0;
            responseEntity.resultStr = "返回成功";

            switch (submitItem.stepID)
            {
                //测酒记录
                case 1003:
                    {
                        string drinkExceptStr;
                        List<Drink_InformationEntity> drinkInfoLst = new JavaScriptSerializer().Deserialize<List<Drink_InformationEntity>>(new JavaScriptSerializer().Serialize(submitItem.data));
                        foreach (Drink_InformationEntity drinkInfoEntity in drinkInfoLst)
                        {
                            if (!TF.Api.BLL.Drink_InformationBLL.Add(drinkInfoEntity, out drinkExceptStr))
                            {
                                responseEntity.result = 1;
                                responseEntity.resultStr = drinkExceptStr;
                                return;
                            }
                        }
                        break;
                    }
                //添乘记录
                case 1011:
                    {
                        string byTimeExceptStr;
                        List<ByTimeRecordEntity> byTimeRecordLst = new JavaScriptSerializer().Deserialize<List<ByTimeRecordEntity>>(new JavaScriptSerializer().Serialize(submitItem.data));
                        foreach (ByTimeRecordEntity byTimeRecordEntity in byTimeRecordLst)
                        {
                            if (!TF.Api.BLL.BytimeBLL.Add(byTimeRecordEntity, out byTimeExceptStr))
                            {
                                responseEntity.result = 1;
                                responseEntity.resultStr = byTimeExceptStr;
                                return;
                            }
                        }
                        break;
                    }
                default:
                    {
                        responseEntity.result = 1;
                        responseEntity.resultStr = "步骤不存在";
                        return;
                    }
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}