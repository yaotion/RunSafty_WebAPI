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
    ///AttentionTypeConfig 的摘要说明
    /// </summary>
    public class AttentionTypeConfig
    {
        #region 属性
        public int clientID = 0;
        public string clientName = "";
        public string attentionTypeids = "";
        public string attentionNames = "";
        #endregion 属性

        #region 构造函数
        public AttentionTypeConfig()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public AttentionTypeConfig(string id)
        {
            string strSql = "select * from TAB_Msg_ClientAttentionConfig where clientID=@id";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                clientID =Convert.ToInt32(dt.Rows[0]["clientID"].ToString());
                clientName = dt.Rows[0]["clientName"].ToString();
                attentionTypeids = dt.Rows[0]["attentionTypeids"].ToString();
                attentionNames = dt.Rows[0]["attentionNames"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_Msg_ClientAttentionConfig (clientID,clientName,attentionTypeids,attentionNames) values (@clientID,@clientName,@attentionTypeids,@attentionNames)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("clientID",clientID),
                                           new SqlParameter("clientName",clientName),
                                           new SqlParameter("attentionTypeids",attentionTypeids),
                                           new SqlParameter("attentionNames",attentionNames)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Msg_ClientAttentionConfig set clientName = @clientName,attentionTypeids = @attentionTypeids,attentionNames = @attentionNames where clientID=@clientID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("clientID",clientID),
                                           new SqlParameter("clientName",clientName),
                                           new SqlParameter("attentionTypeids",attentionTypeids),
                                           new SqlParameter("attentionNames",attentionNames)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string clientID)
        {
            string strSql = "delete TAB_Msg_ClientAttentionConfig where clientID=@clientID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("clientID",clientID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string clientID, string clientName)
        {
            string strSql = "select count(*) from TAB_Msg_ClientAttentionConfig where clientName=@clientName ";
            if (clientID != "")
            {
                strSql += " and clientID <> @clientID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("clientID",clientID),
                                           new SqlParameter("clientName",clientName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        public static DataTable GetClient(int id)
        {
            string strSql = "select * from TAB_Msg_ClientAttentionConfig where clientID<>@id";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0]; 
        }


        public static DataTable GetAllClients()
        {
            string strSql = "select * from TAB_Msg_ClientAttentionConfig  "; 
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0]; 
        }
    }
}
