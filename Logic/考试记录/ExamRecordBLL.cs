using System;
using TF.Api.Entity;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace TF.Api.BLL
{
    public class ExamRecordBLL
    {
        #region 添加考试记录
        /// <summary>
        /// 添加考试记录
        /// </summary>
        public static bool Add(ExamRecordEntity examRecordEntity, out string exceptInfo)
        {
            exceptInfo = string.Empty;
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
            try
            {
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                exceptInfo = ex.ToString();
                return false;
            }
        }
        #endregion
    }

}
