<%@ WebHandler Language="C#" Class="TF.RunSaftyAPI.App_Api.Public.Plan.EndWorkSubmit" %>
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

namespace TF.RunSaftyAPI.App_Api.Public.Plan
{

    /// <summary>
    /// 2.4	退勤信息提交
    /// 
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class EndWorkSubmit : IHttpHandler
    {
        JavaScriptSerializer jser = new JavaScriptSerializer();
        string sRequest = "";

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


                if (!string.IsNullOrEmpty(context.Request["cid"]))
                    strcid = context.Request["cid"];
                //strdata = GetFileContent("c:\\json.txt");
                //gotoEndWorkSubmit();
                if (!string.IsNullOrEmpty(context.Request["data"]))
                {
                    strdata = context.Request["data"];
                    if (strdata.StartsWith("[{") && strdata.EndsWith("}]"))
                        sRequest += gotoEndWorkSubmit();
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
        /// 2.4	退勤信息提交
        /// </summary>
        /// 1000 身份验证 //1002 退勤计划信息 //1003测酒步骤 //1007运行记录上传
        /// 1006小组会议录音 //1006行调通话录音 //3000值班员确认信息
        private string gotoEndWorkSubmit()
        {
            string s1007 = @"[{'stepID':1007,'data':{'workid':'','worktype':1,'sid':'sssss','FileList':[{'fid':'fffff'},{'fid':'iiiii'}]}}]";
            //string strJson = @"[{'stepID':1000,'data':[{'verify':0,'trainmanID':'830BC230-8FB6-4BE0-BA0C-E0C7BC9A1076','planID':'1DF3AD37-0AA9-4901-B62C-F19E81971E46','flowid':'A3EDCCCB-AF58-4A93-9B23-6C463059A151'},{'verify':0,'trainmanID':'0F86CD47-9001-4BF7-A9BE-D85BAE9F3808','planID':'1DF3AD37-0AA9-4901-B62C-F19E81971E46','flowid':'CA2E6468-1DD4-41B5-9840-637C955F4A08'}]},{'stepID':1001,'data':{'startTime':'2014-06-06 12:00:00','trainmanList':[{'trainmanIndex':'1','trainmanID':'830BC230-8FB6-4BE0-BA0C-E0C7BC9A1076'},{'trainmanIndex':'2','trainmanID':'0F86CD47-9001-4BF7-A9BE-D85BAE9F3808'}],'trainTypeName':'DF8B','trainNo':'54002','result':0,'trainNumber':'2900','planID':'1DF3AD37-0AA9-4901-B62C-F19E81971E46','resultStr':'杩斿洖鎴愬姛'}},{'stepID':1003,'data':[{'testTime':'2014-06-06 17:08:21','verify':0,'result':3,'trainmanID':'830BC230-8FB6-4BE0-BA0C-E0C7BC9A1076','strid':'E7B16F2F-8D49-47BD-B824-031C8BDB1DD7','flowID':'A3EDCCCB-AF58-4A93-9B23-6C463059A151'},{'testTime':'2014-06-06 17:08:28','verify':0,'result':3,'trainmanID':'0F86CD47-9001-4BF7-A9BE-D85BAE9F3808','strid':'C73AC890-E40C-4758-AFCD-A80733198DCE','flowID':'CA2E6468-1DD4-41B5-9840-637C955F4A08'}]},{'stepID':1004,'data':{'sid':'7d7efbc9-3de2-475b-96e7-c9328de0f233','workid':'1DF3AD37-0AA9-4901-B62C-F19E81971E46','worktype':0,'recordList':[{'rid':'15A017CD-DBFE-4EFD-9444-821CCD28571E','rtime':'2014-06-06 17:11:30'}]}},{'cid':'7d7efbc9-3de2-475b-96e7-c9328de0f233','stepID':1006,'data':{'voiceLength':1,'workID':'1DF3AD37-0AA9-4901-B62C-F19E81971E46','fileName':'E:\\姚新\\畅想自动化\\软件项目\\运用安全信息平台\\04_软件工程\\02_开发\\运安触摸屏操作台\\自助端\\自助操作端\\Execute\\SoundRecordFiles\\65353D11-6D60-4890-9C11-30A745586E82.wav','beginTime':'2014-06-06 17:11:31','endTime':'2014-06-06 17:11:33','strid':'65353D11-6D60-4890-9C11-30A745586E82','workType':0,'voiceType':0}},{'cid':'7d7efbc9-3de2-475b-96e7-c9328de0f233','stepID':1006,'data':{'voiceLength':1,'workID':'1DF3AD37-0AA9-4901-B62C-F19E81971E46','fileName':'E:\\姚新\\畅想自动化\\软件项目\\运用安全信息平台\\04_软件工程\\02_开发\\运安触摸屏操作台\\自助端\\自助操作端\\Execute\\SoundRecordFiles\\609ABD12-A25C-4DA1-A688-78E988BEDD52.wav','beginTime':'2014-06-06 17:11:33','endTime':'2014-06-06 17:11:35','strid':'609ABD12-A25C-4DA1-A688-78E988BEDD52','workType':0,'voiceType':1}},{'stepID':3000,'data':{'vid':0,'sid':'07d973d3-27f0-4c9e-ab4d-a3320833421c','did':'42119B55-41BB-4F7B-B42B-8F90842257AC','cresult':1,'workid':'1DF3AD37-0AA9-4901-B62C-F19E81971E46','dname':'管理员','sname':'唐山东出退勤确认端','time':'2014-06-06 17:11:45','worktype':0}}]";
            //string sss = @"[{'stepID':1004,'data':{'workid':'111','worktype':1,'sid':'222','recordList':[{'rid':'aaa','rtime':'2014-6-5 14:41:11'},{'rid':'bbb','rtime':'2014-6-5 14:41:22'}]}},{'stepID':3000,'data':{'workid':'aaa','worktype':1,'did':'bbb','dname':'ccc','sid':'ddd','sname':'eee','time':'2014-6-5 14:41:56','cresult':0,'vid':0}}]";

            List<JSONObject2> jObject = jser.Deserialize<List<JSONObject2>>(strdata);   //strdata
            foreach (JSONObject2 item in jObject)
            {
                string strID = item.stepID;
                string sjs = jser.Serialize(item.data);

                if (strID == "1000")
                {
                    //身份验证
                    string strERR = _Goto_1000_TQ(sjs);
                    if (!string.IsNullOrEmpty(strERR))
                        return strERR;
                }
                if (strID == "1002")
                {
                    //退勤计划信息
                    string strERR = _Goto_1002(sjs);
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
                if (strID == "1006")
                {
                    //通话
                    string strERR = _Goto_1006(sjs);
                    if (!string.IsNullOrEmpty(strERR))
                        return strERR;
                }
                if (strID == "1007")
                {
                    //运行记录转储
                    string strERR = _Goto_1007(sjs);
                    if (!string.IsNullOrEmpty(strERR))
                        return strERR;
                }
                if (strID == "1010")
                {
                    //运行记录转储
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
                    return "stepID=1000 身份验证部分 错误";
            }
            return null;
        }
        /// 1000 身份验证
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        string _Goto_1000_TQ(string strJson)
        {
            List<Plan_BeginWork> obj1 = jser.Deserialize<List<Plan_BeginWork>>(strJson);
            foreach (Plan_BeginWork obj in obj1)
            {
                SqlParameter[] sqlParams = {
                                                    new SqlParameter("strEndWorkGUID",obj.flowid),
                                                    new SqlParameter("strTrainPlanGUID",obj.planID),
                                                    new SqlParameter("strTrainmanGUID",obj.trainmanID),
                                                    new SqlParameter("nVerifyID",obj.verify)
                                                 };

                string strSql = "delete from TAB_Plan_EndWork where strEndWorkGUID=@strEndWorkGUID";
                int iErr = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

                strSql = "INSERT INTO TAB_Plan_EndWork "
                                + "(strEndWorkGUID, strTrainPlanGUID, strTrainmanGUID, nVerifyID, dtCreateTime) "
                                + "VALUES (@strEndWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@nVerifyID,getdate())";

                int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                if (iExs != 1)
                    return "stepID=1000 身份验证部分 错误";
            }
            return null;
        }
        /// <summary>
        /// 1002 退勤计划信息
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        string _Goto_1002(string strJson)
        {
            Plan_Train planTrain = jser.Deserialize<Plan_Train>(strJson);
            string strSql = "exec Proc_Plan_EndWork @planID,@SiteID";
            SqlParameter[] sqlParams = {
                                                    new SqlParameter("SiteID",strcid),
                                                    new SqlParameter("planID",planTrain.planID)
                                                 };
            int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
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
            string strSql = "delete from TAB_Voice_Information where strVoiceGUID=@strVoiceGUID";
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
        /// 1007 运行记录上传
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        string _Goto_1007(string strJson)
        {
            RunRecord_File obj = jser.Deserialize<RunRecord_File>(strJson);
            string srl = jser.Serialize(obj.FileList);
            List<RunRecord_File_FileList> obj1 = jser.Deserialize<List<RunRecord_File_FileList>>(srl);
            foreach (RunRecord_File_FileList _obj in obj1)
            {
                SqlParameter[] sqlParams = {
                                                    new SqlParameter("strWorkID",obj.workid),
                                                    new SqlParameter("nWorkType",obj.worktype),
                                                    new SqlParameter("strSiteGUID",obj.sid),
                                                    new SqlParameter("strRunRecordGUID",_obj.fid)
                                                 };

                //先删除
                string strSql = "delete from TAB_RunRecord_File where strWorkID =@strWorkID";
                int iErr = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

                //插入
                strSql = "INSERT INTO TAB_RunRecord_File "
                                    + "(strWorkID, nWorkType, strSiteGUID, strRunRecordGUID) "
                                    + "VALUES (@strWorkID,@nWorkType,@strSiteGUID,@strRunRecordGUID)";


                int iExs = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                if (iExs != 1)
                    return "stepID=1007 运行记录转储部分 错误";
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
            string strSql = "delete from TAB_Plan_Confirm where strWorkID=@workid";
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










        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <returns></returns>
        private string gotoHelp()
        {
            string strHelp = @"2.4	退勤信息提交
支持步骤
	1000 身份验证
	1002 退勤计划信息
	1003测酒步骤
	1007运行记录上传
    1006小组会议录音
	1006行调通话录音
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
    }


}

