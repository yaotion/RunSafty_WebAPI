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
    public class WorkTime_Turn_Branch
    {
        #region 属性
        public int nid;
        public string strSignBrief;
        public int bSigned;
        public string strSignUser;
        public string dtSignTime;
        #endregion 属性

        #region 构造函数
        public WorkTime_Turn_Branch()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public WorkTime_Turn_Branch(int id)
        {
            string strSql = "select * from TAB_WorkTime_Turn_Branch where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nid",id)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                nid = PageBase.static_ext_int(dt.Rows[0]["nid"].ToString());
                strSignBrief = dt.Rows[0]["strSignBrief"].ToString();
                bSigned = PageBase.static_ext_int(dt.Rows[0]["bSigned"].ToString());
                strSignUser = dt.Rows[0]["strSignUser"].ToString();
                dtSignTime = dt.Rows[0]["dtSignTime"].ToString();
            }
        }
        public WorkTime_Turn_Branch(string strFlowID)
        {
            string strSql = "select * from TAB_WorkTime_Turn_Branch where strFlowID=@strFlowID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strFlowID",strFlowID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                nid = PageBase.static_ext_int(dt.Rows[0]["nid"].ToString());
                strSignBrief = dt.Rows[0]["strSignBrief"].ToString();
                bSigned = PageBase.static_ext_int(dt.Rows[0]["bSigned"].ToString());
                strSignUser = dt.Rows[0]["strSignUser"].ToString();
                dtSignTime = dt.Rows[0]["dtSignTime"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        /// <summary>
        /// 超劳签署意见
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateClSignBrif()
        {
            string strSql = "update TAB_WorkTime_Turn_Branch set bSigned = 1,strSignBrief=@strSignBrief,dtSignTime='" + DateTime.Now + "',strSignUser=@strSignUser where nid = @nid";
            SqlParameter[] sqlParams = { 
                                       new SqlParameter("nid",nid),
                                       new SqlParameter("strSignBrief",strSignBrief),
                                       new SqlParameter("strSignUser",strSignUser)
                                   };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion 增删改

        #region 扩展方法

        /// <summary>
        /// 根据flowid获取dt数据
        /// </summary>
        /// <param name="strFlowID"></param>
        /// <returns></returns>
        public static DataTable GetWorkTime_Turn_BranchFromStrFlowID(string strFlowID)
        {
            string strSql = "select * from TAB_WorkTime_Turn_Branch where strFlowID=@strFlowID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strFlowID",strFlowID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        #endregion
    }
}
