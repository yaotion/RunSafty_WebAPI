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
using System.Collections;
using System.Collections.Generic;

namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///TAB_Exam_Answer 的摘要说明
    /// </summary>
    public class TAB_Exam_Answer
    {

        #region 属性
        public string strAnswerGUID;
        public string strQuestionGUID;
        public string strAnswerContent;
        public int nIsRight;
        public int nIndex;


        #endregion
        public TAB_Exam_Answer()
        {

        }


        #region  构造函数

        public bool Add()
        {
            string strSql = "insert into [TAB_Exam_Answer] (strQuestionGUID,strAnswerContent,nIsRight,nIndex)values(@strQuestionGUID,@strAnswerContent,@nIsRight,@nIndex)";
            SqlParameter[] sqlParams ={
                                     new SqlParameter("@strQuestionGUID",strQuestionGUID),
                                     new SqlParameter("@strAnswerContent",strAnswerContent),
                                     new SqlParameter("@nIsRight",nIsRight),
                                     new SqlParameter("@nIndex",nIndex)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool Update()
        {
            string strSql = "update [TAB_Exam_Answer] set [strQuestionGUID]=@strQuestionGUID ,[strAnswerContent] = @strAnswerContent ,[nIsRight]=@nIsRight ,[nIndex] =@nIndex where [strAnswerGUID]=@strAnswerGUID";
            SqlParameter[] sqlParams ={
                                     new SqlParameter("@strQuestionGUID",strQuestionGUID),
                                     new SqlParameter("@strAnswerContent",strAnswerContent),
                                     new SqlParameter("@nIsRight",nIsRight),
                                     new SqlParameter("@nIndex",nIndex),
                                     new SqlParameter("@strAnswerGUID",strAnswerGUID)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool Delete()
        {
            string strSql = "delete from [TAB_Exam_Answer] where strAnswerGUID=@strAnswerGUID";
            SqlParameter sqlParam = new SqlParameter("@strAnswerGUID", strAnswerGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }


        #endregion


        #region 扩展方法
        /// <summary>
        /// 删除答案
        /// </summary>
        public static bool Delete(string strAnswerGUID)
        {
            string strSql = "delete from [TAB_Exam_Answer] where strAnswerGUID=@strAnswerGUID";
            SqlParameter sqlParam = new SqlParameter("@strAnswerGUID", strAnswerGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }

        /// <summary>
        /// 删除某个问题的所有答案
        /// </summary>
        public static bool DeleteAllByQuestionGUID(string strQuestionGUID)
        {
            string strSql = "delete from [TAB_Exam_Answer] where strQuestionGUID=@strQuestionGUID";
            SqlParameter sqlParam = new SqlParameter("@strQuestionGUID", strQuestionGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }


        /// <summary>
        /// 根据试题ID返回答案列表
        /// </summary>
        public static List<TAB_Exam_Answer> GetAllByQuestionGUID(string strQuestionGUID)
        {
            string strSql = "select * from [TAB_Exam_Answer] where strQuestionGUID=@strQuestionGUID order by [nIndex] asc";
            SqlParameter sqlParam = new SqlParameter("@strQuestionGUID", strQuestionGUID);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam).Tables[0];
            List<TAB_Exam_Answer> teaLst = new List<TAB_Exam_Answer>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TAB_Exam_Answer tea = new TAB_Exam_Answer();
                tea.nIndex = PageBase.static_ext_int(dt.Rows[i]["nIndex"]);
                tea.nIsRight = PageBase.static_ext_int(dt.Rows[i]["nIsRight"]);
                tea.strAnswerContent = PageBase.static_ext_string(dt.Rows[i]["strAnswerContent"]);
                tea.strAnswerGUID = PageBase.static_ext_string(dt.Rows[i]["strAnswerGUID"]);
                tea.strQuestionGUID = PageBase.static_ext_string(dt.Rows[i]["strQuestionGUID"]);
                teaLst.Add(tea);
            }
            return teaLst;
        }
        #endregion

    }
}