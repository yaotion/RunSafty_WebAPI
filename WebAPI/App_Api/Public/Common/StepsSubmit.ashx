<%@ WebHandler Language="C#" Class="StepsSubmit" %>

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


public class StepsSubmit : IHttpHandler
{
    ResponseEntity responseEntity;
    JavaScriptSerializer jser = new JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {

        ResponseHelper.SetJsonHeader();

        int clientID = RequestHelper.GetRequestToInt("cid");
        string data = RequestHelper.GetRequestToString("data");
        //data = "[{\"stepID\":1004,\"data\":{\"sid\":\"b0b2ae82-fd3a-4baa-91f5-05ae294145fe\",\"trainmanList\":[{\"trainmanGuid\":\"21121EAF-38CE-45B4-85C4-D67E8DB01E0B\"}],\"workid\":\"\",\"worktype\":0,\"recordList\":[{\"rid\":\"3a20a69e-c6df-4361-9d58-f3e0ed3ca9e0\",\"rtime\":\"2014-07-16 11:56:47\"}]}}]";
        responseEntity = new ResponseEntity();

        List<SubmitItemEntity> submitItemEntityLst = GetSubmitItemEntityLst(data);
        if (submitItemEntityLst == null)
        {
            responseEntity.result = 1;
            responseEntity.resultStr = "提交数据为空";
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

    #region 处理数据
    /// <summary>
    /// 处理数据
    /// </summary>
    /// <param name="strJsonData"></param>
    private void ProcessData(List<SubmitItemEntity> submitItemLst)
    {
        responseEntity.result = 0;
        responseEntity.resultStr = "返回成功";
        foreach (SubmitItemEntity submitItem in submitItemLst)
        {
            //根据不同的步骤ID选取对应处理方法
            switch (submitItem.stepID)
            {
                //测酒记录
                case 1003:
                    {
                        if (!ProcessDrinkInfo(submitItem.data)) { return; }
                        break;
                    }
                //运行记录
                case 1008:
                    {
                        if (!ProcessRunRecord(submitItem.data)) { return; }
                        break;
                    }
                //考试提交
                case 1010:
                    {
                        if (!ProcessExamRecord(submitItem.data)) { return; }
                        break;
                    }
                    //记名式传达
                case 1004:
                    {
                        if (!ProcessReadingRecord(submitItem.data)) { return; }
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
    #endregion


    #region 记录处理方法

    /// <summary>
    /// 处理测酒信息
    /// </summary>
    /// <param name="objJson"></param>
    /// <returns></returns>
    private bool ProcessDrinkInfo(object objJson)
    {
        bool bFlag = true;
        string drinkExceptStr;
        List<Drink_InformationEntity> drinkInfoLst = new JavaScriptSerializer().Deserialize<List<Drink_InformationEntity>>(new JavaScriptSerializer().Serialize(objJson));
        foreach (Drink_InformationEntity drinkInfoEntity in drinkInfoLst)
        {
            if (!TF.Api.BLL.Drink_InformationBLL.Add(drinkInfoEntity, out drinkExceptStr))
            {
                responseEntity.result = 1;
                responseEntity.resultStr = drinkExceptStr;
                bFlag = false;
                break;
            }
        }
        return bFlag;
    }


    /// <summary>
    /// 处理记名式发布阅读记录
    /// </summary>
    /// <param name="objJson"></param>
    /// <returns></returns>
    private bool ProcessReadingRecord(object objJson)
    {
        DutyReading_ReadRecordList recordList = new JavaScriptSerializer().Deserialize<DutyReading_ReadRecordList>(new JavaScriptSerializer().Serialize(objJson));
        for (int i = 0; i < recordList.recordList.Count; i++)
        {
            for (int k = 0; k < recordList.trainmanList.Count; k++)
            {
                TF.Api.DBUtility.DBDutyReading.AddRecord(recordList.sid, recordList.workid, recordList.worktype, recordList.trainmanList[k].trainmanguid, recordList.recordList[i]);
            }
        }
        return true;
    }
    /// <summary>
    /// 处理运行记录
    /// </summary>
    /// <param name="objJson"></param>
    /// <returns></returns>
    private bool ProcessRunRecord(object objJson)
    {
        bool bFlag = true;
        string runRecordExceptInfo;
        List<RunRecord_FileEntity> RunRecord_FileLst = new JavaScriptSerializer().Deserialize<List<RunRecord_FileEntity>>(new JavaScriptSerializer().Serialize(objJson));
        foreach (RunRecord_FileEntity runRecordFileEntity in RunRecord_FileLst)
        {
            if (!TF.Api.BLL.RunRecord_FileEntityBLL.Add(runRecordFileEntity, out runRecordExceptInfo))
            {
                responseEntity.result = 1;
                responseEntity.resultStr = runRecordExceptInfo;
                bFlag = false;
                break;
            }
        }
        return bFlag;
    }

    /// <summary>
    /// 处理考试信息
    /// </summary>
    private bool ProcessExamRecord(object objJson)
    {
        bool bFlag = true;
        string examRecordExceptInfo;
        List<ExamRecordEntity> ExamRecordLst = new JavaScriptSerializer().Deserialize<List<ExamRecordEntity>>(new JavaScriptSerializer().Serialize(objJson));
        foreach (ExamRecordEntity examRecordEntity in ExamRecordLst)
        {
            if (!TF.Api.BLL.ExamRecordBLL.Add(examRecordEntity, out examRecordExceptInfo))
            {
                responseEntity.result = 1;
                responseEntity.resultStr = examRecordExceptInfo;
                bFlag = false;
                break;
            }
        }
        return bFlag;
        
    }

    #endregion

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    /// <summary>
    /// 1004 记名式传达阅读记录
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    string _Goto_1004(string strJson)
    {
        DutyReading_ReadRecord obj = jser.Deserialize<DutyReading_ReadRecord>(strJson);
        DutyReading_ReadRecordWithMan manObj=jser.Deserialize<DutyReading_ReadRecordWithMan>(strJson);
        string srl = jser.Serialize(obj.recordList);
        List<DutyReading_recordList> obj1 = jser.Deserialize<List<DutyReading_recordList>>(srl);
        List<Trainman_recordList> manList = jser.Deserialize<List<Trainman_recordList>>(jser.Serialize(manObj.trainmanList));
        foreach (DutyReading_recordList _obj in obj1)
        {
            SqlParameter[] sqlParams = {
                                                    new SqlParameter("workid",obj.workid),
                                                    new SqlParameter("worktype",obj.worktype),
                                                    new SqlParameter("rid",_obj.rid),
                                                    new SqlParameter("rtime",_obj.rtime),
                                                    new SqlParameter("sid",obj.sid)
                                                 };

            //先删除
            string strSql = "delete from TAB_DutyReading_ReadRecord where strReadingGUID=@rid";
            int iErr = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            //插入
            strSql = "INSERT INTO TAB_DutyReading_ReadRecord "
                                + "(strWorkID, nWorkTypeID, strReadingGUID, dtReadTime, strSiteGUID) "
                                + "VALUES (@workid,@worktype,@rid,@rtime,@sid)";


            int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            if (iExs != 1)
                return "stepID=1004 出乘指导阅读记录 错误";
            try
            {
                if (manList != null)
                {
                    foreach (Trainman_recordList record in manList)
                    {
                        UpdateReadingStatisticsData(_obj.rid, record.trainmanguid);
                    }
                }
            }
            catch(Exception ex)
            {
                return "stepID=1004 出乘指导阅读记录 错误";
                
            }
        }
        return null;
    }

    public void UpdateReadingStatisticsData(string rid, string strTrainmanGUID)
    {
       
     
            string strSql = @"update [TAB_System_TransmitDetails] 
                        set [readCount]=[readCount]+1 ,[readState]=1 
                    where [strOrgTrainManGUID]=@strOrgTrainManGUID and [strPublicationID]=@strPublicationID";
            SqlParameter[] sqlParams = {
                                   new SqlParameter("strOrgTrainManGUID",strTrainmanGUID),
                                   new SqlParameter("strPublicationID",rid)
                                   };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        
    }

}