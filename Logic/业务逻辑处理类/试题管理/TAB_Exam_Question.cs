using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///TAB_Exam_Question 的摘要说明
    /// </summary>
    public class TAB_Exam_Question
    {
        #region 属性
        public string strQuestionGUID;
        public string strQuestionContent;
        public DateTime? dtCreateTime;
        public int nQuestionTypeCode;
        public int nQuestionScore;
        #endregion

        public TAB_Exam_Question()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public TAB_Exam_Question(string strQuestionGUID)
        {
            string strSql = "select top 1 * from [TAB_Exam_Question] where strQuestionGUID=@strQuestionGUID";
            SqlParameter sqlParam = new SqlParameter("@strQuestionGUID", strQuestionGUID);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.strQuestionGUID = PageBase.static_ext_string(dt.Rows[0]["strQuestionGUID"]);
                this.strQuestionContent = PageBase.static_ext_string(dt.Rows[0]["strQuestionContent"]);
                this.nQuestionTypeCode = PageBase.static_ext_int(dt.Rows[0]["nQuestionTypeCode"]);
                this.nQuestionScore = PageBase.static_ext_int(dt.Rows[0]["nQuestionScore"]);
            }
        }

        #region 增删改

        public bool Add()
        {
            string strSql = "insert into [TAB_Exam_Question] (strQuestionContent,dtCreateTime,nQuestionTypeCode,nQuestionScore)values(@strQuestionContent,@dtCreateTime,@nQuestionTypeCode,@nQuestionScore)";
            SqlParameter[] sqlParams ={
                                     new SqlParameter("@strQuestionContent",strQuestionContent),
                                     new SqlParameter("@dtCreateTime",DateTime.Now),
                                     new SqlParameter("@nQuestionTypeCode",nQuestionTypeCode),
                                     new SqlParameter("@nQuestionScore",nQuestionScore)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool Update()
        {
            string strSql = "update [TAB_Exam_Question] set [strQuestionContent]=@strQuestionContent ,[nQuestionTypeCode]=@nQuestionTypeCode ,[nQuestionScore] =@nQuestionScore where [strQuestionGUID]=@strQuestionGUID";
            SqlParameter[] sqlParams ={
                                     new SqlParameter("@strQuestionContent",strQuestionContent),
                                     new SqlParameter("@nQuestionTypeCode",nQuestionTypeCode),
                                     new SqlParameter("@nQuestionScore",nQuestionScore),
                                     new SqlParameter("@strQuestionGUID",strQuestionGUID)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool Delete()
        {
            string strSql = "delete from [TAB_Exam_Question] where strQuestionGUID=@strQuestionGUID";
            SqlParameter sqlParam = new SqlParameter("@strQuestionGUID", strQuestionGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }
        #endregion

        #region  扩展方法
        /// <summary>
        /// 删除问题
        /// </summary>
        public static bool Delete(string strQuestionGUID)
        {
            //删除该试题下对应的答案
            TAB_Exam_Answer.DeleteAllByQuestionGUID(strQuestionGUID);

            string strSql = "delete from [TAB_Exam_Question] where strQuestionGUID=@strQuestionGUID";
            SqlParameter sqlParam = new SqlParameter("@strQuestionGUID", strQuestionGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }
        #endregion
    }
}