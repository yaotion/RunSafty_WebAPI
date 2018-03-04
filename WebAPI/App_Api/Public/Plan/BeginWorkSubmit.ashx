<%@ WebHandler Language="C#" Class="BeginWorkSubmit" %>
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Text;
using ThinkFreely.DBUtility;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TF.Api.Entity;

/// <summary>
/// 2.3	出勤信息提交
/// </summary>
public class BeginWorkSubmit : IHttpHandler
{
    string sRequest = "";
    JavaScriptSerializer jser = new JavaScriptSerializer();

    string strcid = "";
    string strdata = "";

    private string GetFileContent(string FileName)
    {
        StreamReader sm = new StreamReader(FileName);
        return sm.ReadToEnd();

    }
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string strQuery = context.Request.QueryString.ToString().ToLower();
            if (strQuery == "help" || strQuery == "h") //帮助
            {
                sRequest += gotoHelp();
                return;
            }
            //strdata = GetFileContent("c:\\json.txt");
            //gotoBeginWorkSubmit();
            if (!string.IsNullOrEmpty(context.Request["cid"]))
                strcid = context.Request["cid"];



            if (!string.IsNullOrEmpty(context.Request["data"]))
            {
                strdata = context.Request["data"];

                if (strdata.StartsWith("[{") && strdata.EndsWith("}]"))
                    sRequest += gotoBeginWorkSubmit();
                else
                {
                    sRequest += sRetErrJSON("data格式错误");
                    return;
                }
            }
            else
            {
                sRequest += sRetErrJSON("data不能为空");
                return;
            }
        }
        finally
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(sRequest);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// 2.3	出勤信息提交
    /// </summary>
    /// 1000 身份验证 //1001 出勤计划信息 //1003测酒步骤 //1004记名式传达阅读记录
    /// 1006小组会议录音 //1006行调通话录音 //1008 验卡记录上传 //3000值班员确认信息
    private string gotoBeginWorkSubmit()
    {

        List<JSONObject2> jObject = jser.Deserialize<List<JSONObject2>>(strdata);   //strdata
        foreach (JSONObject2 item in jObject)
        {
            string strID = item.stepID;
            string sjs = jser.Serialize(item.data);

            if (strID == "1000")
            {
                //身份验证
                string strERR = _Goto_1000(sjs);
                if (!string.IsNullOrEmpty(strERR))
                    return strERR;
            }
            if (strID == "1001")
            {
                //出勤
                string strERR = _Goto_1001(sjs);
                if (!string.IsNullOrEmpty(strERR))
                    return strERR;
            }
            if (strID == "1003")
            {
                //饮酒
                string strERR = _Goto_1003(sjs);
                if (!string.IsNullOrEmpty(strERR))
                    return strERR;
            }
            if (strID == "1004")
            {
                //出乘指导阅读记录
                string strERR = _Goto_1004(sjs);
                if (!string.IsNullOrEmpty(strERR))
                    return strERR;
            }
            if (strID == "1006")
            {
                //通话  1006小组会议录音 1006行调通话录音
                string strERR = _Goto_1006(sjs);
                if (!string.IsNullOrEmpty(strERR))
                    return strERR;
            }
            if (strID == "1008")
            {
                ///验卡记录上传
                string strERR = _Goto_1008(sjs);
                if (!string.IsNullOrEmpty(strERR))
                    return strERR;
            }
            if (strID == "1010")
            {
                string strERR = _Goto_1010(sjs);
                if (!string.IsNullOrEmpty(strERR))
                    return strERR;
            }
            if (strID == "3000")
            {
                //值班员确认步骤
                string strERR = _Goto_3000(sjs);
                if (!string.IsNullOrEmpty(strERR))
                    return strERR;
            }
        }
        return "{\"result\":0,\"resultStr\":\"返回成功\"}";
    }

    /// <summary>
    /// 1000 身份验证
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    string _Goto_1000(string strJson)
    {
        List<Plan_BeginWork> obj1 = jser.Deserialize<List<Plan_BeginWork>>(strJson);
        foreach (Plan_BeginWork obj in obj1)
        {
            SqlParameter[] sqlParams = {
                                                    new SqlParameter("strBeginWorkGUID",obj.flowid),
                                                    new SqlParameter("strTrainPlanGUID",obj.planID),
                                                    new SqlParameter("strTrainmanGUID",obj.trainmanID),
                                                    new SqlParameter("nVerifyID",obj.verify)
                                                 };

            string strSql = "delete from TAB_Plan_BeginWork where strBeginWorkGUID=@strBeginWorkGUID";
            int iErr = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            strSql = "INSERT INTO TAB_Plan_BeginWork "
                            + "(strBeginWorkGUID, strTrainPlanGUID, strTrainmanGUID, nVerifyID, dtCreateTime) "
                            + "VALUES (@strBeginWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@nVerifyID,getdate())";

            int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            if (iExs != 1)
                return "stepID=1000  身份验证部分 错误";
        }
        return null;
    }

    /// <summary>
    /// 1001 出勤计划信息
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    string _Goto_1001(string strJson)
    {
        Plan_Train planTrain = jser.Deserialize<Plan_Train>(strJson);
        string strSql = "UPDATE TAB_Plan_Train SET nPlanState = @nPlanState WHERE (strTrainPlanGUID = @strTrainPlanGUID)";
        SqlParameter[] sqlParams = {
                                                    new SqlParameter("nPlanState",7),
                                                    new SqlParameter("strTrainPlanGUID",planTrain.planID)
                                                 };
        int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        if (iExs != 1)
            return "stepID=1001  乘车记录部分 错误";

        return null;
    }

    /// <summary>
    /// 1003 测酒步骤
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    string _Goto_1003(string strJson)
    {
        List<Drink_Information> obj1 = jser.Deserialize<List<Drink_Information>>(strJson);
        foreach (Drink_Information obj in obj1)
        {
            SqlParameter[] sqlParams = {
                                                       new SqlParameter("strGUID",obj.strid),
                                                       new SqlParameter("strTrainmanGUID",obj.trainmanID),
                                                       new SqlParameter("nDrinkResult",obj.result),
                                                       new SqlParameter("dtCreateTime",obj.testTime),
                                                       new SqlParameter("nVerifyID",0),
                                                       new SqlParameter("strWorkID",obj.flowID),
                                                       new SqlParameter("nWorkTypeID",obj.workType),
                                                       new SqlParameter("strImagePath","")
                                                   };
            //先删除
            string strSql = "delete from TAB_Drink_Information where strGUID=@strGUID";
            int iErr = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            //插入
            strSql = @"insert into TAB_Drink_Information (strGUID,strTrainmanGUID,nDrinkResult,dtCreateTime,nVerifyID,strWorkID,nWorkTypeID,strImagePath) 
                            values (@strGUID,@strTrainmanGUID,@nDrinkResult,@dtCreateTime,@nVerifyID,@strWorkID,@nWorkTypeID,@strImagePath)";

            int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            if (iExs != 1)
                return "stepID=1003 饮酒记录部分 错误";
        }
        return null;
    }

    /// <summary>
    /// 1004 记名式传达阅读记录
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    string _Goto_1004(string strJson)
    {
        DutyReading_ReadRecord obj = jser.Deserialize<DutyReading_ReadRecord>(strJson);
        string srl = jser.Serialize(obj.recordList);
        List<DutyReading_recordList> obj1 = jser.Deserialize<List<DutyReading_recordList>>(srl);
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
        }
        return null;
    }

    /// <summary>
    /// 1006 小组会议录音
    /// 1006 行调通话录音
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    string _Goto_1006(string strJson)
    {
        Voice_Information obj = jser.Deserialize<Voice_Information>(strJson);
        SqlParameter[] sqlParams = {
                                                new SqlParameter("strVoiceGUID",obj.strid),
                                                new SqlParameter("dtBeginTime",obj.beginTime),
                                                new SqlParameter("dtEndTime",obj.endTime),
                                                new SqlParameter("nVoiceLength",obj.voiceLength),
                                                new SqlParameter("strSiteGUID",""),
                                                new SqlParameter("nVoiceType",obj.voiceType),
                                                new SqlParameter("strWorkID",obj.workID),
                                                new SqlParameter("nWorkType",obj.workType),
                                                new SqlParameter("VoiceFile","")
                                             };
        //先删除
        string strSql = "delete from TAB_Voice_Information where strVoiceGUID =@strVoiceGUID";
        int iErr = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

        //插入
        strSql = "INSERT INTO TAB_Voice_Information "
                            + "(strVoiceGUID, dtBeginTime, dtEndTime, nVoiceLength, strSiteGUID, nVoiceType, strWorkID, nWorkType, VoiceFile) "
                            + "VALUES (@strVoiceGUID,@dtBeginTime,@dtEndTime,@nVoiceLength,@strSiteGUID,@nVoiceType,@strWorkID,@nWorkType,@VoiceFile)";


        int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        if (iExs != 1)
            return "stepID=1006 通话部分 错误";
        return null;
    }

    /// <summary>
    /// 1008 验卡记录上传
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    string _Goto_1008(string strJson)
    {
        Plan_VerifyCard obj = jser.Deserialize<Plan_VerifyCard>(strJson);   //sjs
        string srl = jser.Serialize(obj.verifyList);
        List<VerifyCard_verifyList> obj1 = jser.Deserialize<List<VerifyCard_verifyList>>(srl);
        foreach (VerifyCard_verifyList _obj in obj1)
        {
            SqlParameter[] sqlParams = {
                                                new SqlParameter("strWorkID",obj.workid),
                                                new SqlParameter("nWorkTypeID",obj.worktype),
                                                new SqlParameter("strSiteGUID",obj.sid),
                                                new SqlParameter("nResultID",_obj.resultID),
                                                new SqlParameter("strResultContent",_obj.resultContent),
                                                new SqlParameter("dtCreateTime",_obj.vTime),
                                                new SqlParameter("dtVerifyTime",_obj.vTime),
                                                new SqlParameter("sectionID",_obj.sectionID),
                                                new SqlParameter("sectionName",_obj.sectionName)
                                             };
            //先删除
            string strSql = "delete from TAB_Plan_VerifyCard where strWorkID =@strWorkID";
            int iErr = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            //插入
            strSql = "INSERT INTO TAB_Plan_VerifyCard "
                                + "(strWorkID, nWorkTypeID, strSiteGUID, nResultID, strResultContent,dtCreateTime,dtVerifyTime,sectionID,sectionName) "
                                + "VALUES (@strWorkID,@nWorkTypeID,@strSiteGUID,@nResultID,@strResultContent,@dtCreateTime,@dtVerifyTime,@sectionID,@sectionName)";


            int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            if (iExs != 1)
                return "stepID=1008 验卡记录上传步骤 错误";
        }
        return null;
    }



    /// <summary>
    /// 1010 考试记录上传
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    string _Goto_1010(string strJson)
    {
        try
        {
            //反序列化接受到的数据
            List<ExamRecordEntity> examRecordEntityLst = new JavaScriptSerializer().Deserialize<List<ExamRecordEntity>>(strJson);
            //循环插入考试记录
            foreach (ExamRecordEntity recordEntity in examRecordEntityLst)
            {
                AddExamRecord(recordEntity);
            }
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
        return null;
    }
    /// <summary>
    /// 3000 值班员确认信息
    /// </summary>
    /// <param name="strJson"></param>
    /// <returns></returns>
    string _Goto_3000(string strJson)
    {
        Plan_Confirm obj = jser.Deserialize<Plan_Confirm>(strJson);
        SqlParameter[] sqlParams = {
                                                new SqlParameter("workid",obj.workid),
                                                new SqlParameter("worktype",obj.worktype),
                                                new SqlParameter("did",obj.did),
                                                new SqlParameter("dname",obj.dname),
                                                new SqlParameter("sid",obj.sid),
                                                new SqlParameter("sname",obj.sname),
                                                new SqlParameter("time",obj.time),
                                                new SqlParameter("cresult",obj.cresult),
                                                new SqlParameter("vid",obj.vid)
                                             };
        //先删除
        string strSql = "delete from TAB_Plan_Confirm where strWorkID =@workid";
        int iErr = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

        //插入
        strSql = "INSERT INTO TAB_Plan_Confirm "
                            + "(strWorkID, nWorkType, strConfirmDutyUser, strConfirmDutyUserName, strConfirmDutySite, strConfirmDutySiteName, dtConfirmTime, nConfirmResult, nVerifyID) "
                            + "VALUES (@workid,@worktype,@did,@dname,@sid,@sname,@time,@cresult,@vid)";


        int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        if (iExs != 1)
            return "stepID=3000 值班员确认步骤 错误";
        return null;
    }



    //返回错误信息
    string sRetErrJSON(string strErr)
    {
        return "{\"result\":1,\"resultStr\":\"" + strErr + "\"}";
    }

    /// <summary>
    /// 帮助文档
    /// </summary>
    /// <returns></returns>
    private string gotoHelp()
    {
        string strHelp = @"2.3	出勤信息提交
支持步骤
	1000 身份验证
	1001 出勤计划信息
	1003测酒步骤
	1004记名式传达阅读记录
    1006小组会议录音
	1006行调通话录音
    1008 验卡记录上传
    3000值班员确认信息


 参数格式
    cid=xxx&data=

 参数说明
    cid:为客户端编号
    data:出勤信息
    data的格式参照对象说明
    [
        {
            'stepID': 1000,
            'data': [
                {
                    
                },
           },{}…
    ]

    提交的信息为对象数组，每一个对象都具有stepID和data属性。具体信息在data里，详见对象JSON说明
        stepID 
        (获取出勤计划=1001;获取退勤计划=1002;测酒=1003;出乘必读=1004;写卡=1005;通话记录=1006;运行记录转储=1007;值班员确认步骤=3000;)

 返回参数
     参数格式

        {'result':0,'resultStr':'返回成功'}

        {'result':1,'resultStr':'返回错误原因'}
";
        FileInfo fi = new FileInfo(HttpContext.Current.Request.PhysicalPath);
        strHelp += "\r\n--------";
        strHelp += "\r\n创建时间:" + fi.CreationTime.ToString();
        strHelp += "\r\n最后更新:" + fi.LastWriteTime.ToString();
        return strHelp;
    }


    /// <summary>
    /// 添加考试记录
    /// </summary>
    private bool AddExamRecord(ExamRecordEntity examRecordEntity)
    {
        string strSql = "INSERT [TAB_Exam_Record] ( [strRecordGUID],[strTrainmanGUID] ,[strFlowID] ,[strPlanID] ,[nWorkType] ,[dtStartTime] ,[dtEndTime] ,[nQuestionCount] ,[nCorrectCount] ,[nScore] ,[nTotalScore]) VALUES (@strRecordGUID,@strTrainmanGUID ,@strFlowID ,@strPlanID ,@nWorkType ,@dtStartTime ,@dtEndTime ,@nQuestionCount ,@nCorrectCount ,@nScore ,@nTotalScore)";
        SqlParameter[] sqlParams ={
                                     new SqlParameter("@strRecordGUID",examRecordEntity.recordID),
                                     new SqlParameter("@strTrainmanGUID",examRecordEntity.trainmanID),
                                     new SqlParameter("@strFlowID",examRecordEntity.flowID),
                                     new SqlParameter("@strPlanID",examRecordEntity.planID),
                                     new SqlParameter("@nWorkType",examRecordEntity.workType),
                                     new SqlParameter("@dtStartTime",examRecordEntity.startTime),
                                     new SqlParameter("@dtEndTime",examRecordEntity.endTime),
                                     new SqlParameter("@nQuestionCount",examRecordEntity.questionCount),
                                     new SqlParameter("@nCorrectCount",examRecordEntity.correctCount),
                                     new SqlParameter("@nScore",examRecordEntity.score),
                                     new SqlParameter("@nTotalScore",examRecordEntity.totalScore)
                                     
                                     };
        return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
    }
}
