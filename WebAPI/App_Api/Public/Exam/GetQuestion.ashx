<%@ WebHandler Language="C#" Class="GetQuestion" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using ThinkFreely.RunSafty;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using TF.Api.Utilities;

/// <summary>
/// 类名：GetQuestion
/// 描述：获取试题接口
/// </summary>
public class GetQuestion : IHttpHandler
{
    #region 实体类
    /// <summary>
    /// 返回结果实体类
    /// </summary>
    sealed public class ResultJsonEntity
    {
        public int result { get; set; }
        public string resultStr { get; set; }
        public List<QuestionEntity> questionList { get; set; }
    }

    /// <summary>
    /// 问题实体类
    /// </summary>
    public class QuestionEntity
    {
        public string qid { get; set; }
        public string content { get; set; }
        public int qtype { get; set; }
        public int score { get; set; }
        public List<AnswerEntity> answer { get; set; }
    }


    /// <summary>
    /// 答案实体类
    /// </summary>
    public class AnswerEntity
    {
        public string aid { get; set; }
        public string content { get; set; }
        public int isRight { get; set; }
        public int nIndex { get; set; }
    }

    #endregion

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        string clientID = PageBase.static_ext_string(context.Request["cid"]);
        int nCount = PageBase.static_ext_int(context.Request["nCount"]);
        context.Response.Write(GetJson(nCount));
    }

    /// <summary>
    /// 获取序列化的Json字符串
    /// </summary>
    private string GetJson(int nReturnCount)
    {
        ResultJsonEntity resultJsonEntity = new ResultJsonEntity();
        List<QuestionEntity> questionLst = new List<QuestionEntity>();
        try
        {
            questionLst = GetQuestions(nReturnCount);
            resultJsonEntity.result = 0;
            resultJsonEntity.resultStr = "返回成功";
        }
        catch (Exception ex)
        {
            resultJsonEntity.result = 1;
            resultJsonEntity.resultStr = ex.ToString();
        }
        resultJsonEntity.questionList = questionLst;
        return new JavaScriptSerializer().Serialize(resultJsonEntity);
    }

    /// <summary>
    /// 根据问题ID获取答案列表
    /// </summary>
    /// <param name="strQuestionGUID"></param>
    /// <returns></returns>
    private List<AnswerEntity> GetAnswers(string strQuestionGUID)
    {
        string strSql = "select * from [TAB_Exam_Answer] where strQuestionGUID=@strQuestionGUID order by [nIndex] asc";
        SqlParameter sqlParam = new SqlParameter("@strQuestionGUID", strQuestionGUID);
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam).Tables[0];
        List<AnswerEntity> teaLst = new List<AnswerEntity>();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            AnswerEntity answerEntity = new AnswerEntity();
            answerEntity.nIndex = PageBase.static_ext_int(dt.Rows[i]["nIndex"]);
            answerEntity.isRight = PageBase.static_ext_int(dt.Rows[i]["nIsRight"]);
            answerEntity.content = PageBase.static_ext_string(dt.Rows[i]["strAnswerContent"]);
            answerEntity.aid = PageBase.static_ext_string(dt.Rows[i]["strAnswerGUID"]);
            teaLst.Add(answerEntity);
        }
        return teaLst;
    }


    /// <summary>
    /// 获取指定数量的试题
    /// </summary>
    /// <param name="nCount"></param>
    /// <returns></returns>
    private List<QuestionEntity> GetQuestions(int nCount)
    {
        List<QuestionEntity> questionLst = new List<QuestionEntity>();

        string strSql = "select top  " + nCount.ToString() + "  * from  [TAB_Exam_Question]  order by newid()";
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];

        foreach (DataRow dr in dt.Rows)
        {
            QuestionEntity questionEntity = new QuestionEntity();

            questionEntity.qid = PageBase.static_ext_string(dr["strQuestionGUID"]);
            questionEntity.qtype = PageBase.static_ext_int(dr["nQuestionTypeCode"]);
            questionEntity.content = PageBase.static_ext_string(dr["strQuestionContent"]);
            questionEntity.score = PageBase.static_ext_int(dr["nQuestionScore"]);
            questionEntity.answer = GetAnswers(questionEntity.qid);
            questionLst.Add(questionEntity);
        }
        return questionLst;
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }



    /*
    接口功能
    说明：完成试题信息获取功能及考试结果提交功能
      试题信息地址：http://webhost/App_API/Public/Exam/GetQuestion.ashx
      调用参数
      参数格式
      cid=xxx&nCount=1
      参数说明
      cid:客户端ID
      nCount:获取试题数量
　　
      返回格式
    {“result”:0,”resultStr”:”返回成功”,”questionList”:
    [
    {“qid”:””,”content”:””,”qtype”:0,”score”:0,”answer”:
    [
    {“aid”:””,”content”:””,”isRight”:0,”index”:0}…
    ]}…
    ]
      }
　　
      Qid:试题ID
      Content:试题内容
      Score:试题分值
      Qtype:试题类型(0选择题1判断题)
      Answer:试题答案
            Aid:答案ID
            Content:答案内容
            isRight:是否为正确答案
            index:答案序号

    */
}


