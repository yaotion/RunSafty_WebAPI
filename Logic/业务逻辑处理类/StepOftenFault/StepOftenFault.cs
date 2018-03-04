using System;
using System.Data;
using System.Data.SqlClient;
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

namespace TF.RunSafty.Logic
{
    /// <summary>
    /// 整备步骤常见故障管理
    /// </summary>

    public class StepOftenFault
    {
        #region 属性
        public int nID;
        public string strID;
        public int nStepType;
        public string strFaultText;
        public string strPinYinHead;
        #endregion 属性

        #region 构造函数
        public StepOftenFault()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public StepOftenFault(string strId)
        {
            string strSql = "select * from TAB_StepOftenFault where strID=@strId";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strId",strId)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                nStepType = PageBase.static_ext_int(dt.Rows[0]["nStepType"].ToString());
                strFaultText = dt.Rows[0]["strFaultText"].ToString();
                strPinYinHead = dt.Rows[0]["strPinYinHead"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_StepOftenFault (strID,nStepType,strFaultText,strPinYinHead) values (@strID,@nStepType,@strFaultText,@strPinYinHead)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strID",guid),
                                           new SqlParameter("@nStepType",nStepType),
                                           new SqlParameter("@strFaultText",strFaultText),
                                           new SqlParameter("@strPinYinHead",strPinYinHead) 
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_StepOftenFault set  nStepType=@nStepType,strFaultText=@strFaultText,strPinYinHead=@strPinYinHead where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@nStepType",nStepType),
                                           new SqlParameter("@strFaultText",strFaultText),
                                           new SqlParameter("@strID",strID),
                                           new SqlParameter("@strPinYinHead",strPinYinHead) 
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strId)
        {
            string strSql = "delete TAB_StepOftenFault where strID=@strId";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strId",strId)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
       
        


        /// <summary>
        /// 是否已存在同样的错误记录
        /// </summary>
        /// <param name="nStepType">整备ID</param>
        /// <param name="strFaultText">故障描述</param>
        /// <returns></returns>
        public static bool Exist(string strID, string nStepType, string strFaultText)
        {
            string strSql = "select count(*) from TAB_StepOftenFault where nStepType=@nStepType and strFaultText=@strFaultText ";
           strSql += strID !=""? " and strID<>@strID ":"";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strID",strID),
                                           new SqlParameter("@nStepType",nStepType),
                                           new SqlParameter("@strFaultText",strFaultText)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        #endregion 扩展方法

    }
}

