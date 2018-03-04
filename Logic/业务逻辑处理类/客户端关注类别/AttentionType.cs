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
    ///AttentionType 的摘要说明
    /// </summary>
    public class AttentionType
    {
        #region 属性
        public int nID = 0;
        public int TypeID = 0;
        public string TypeName = "";
        #endregion 属性

        #region 构造函数
        public AttentionType()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_Msg_AttentionType (TypeID,TypeName) values (@TypeID,@TypeName)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TypeName",TypeName),
                                           new SqlParameter("TypeID",TypeID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Msg_ClientAttentionConfig set attentionTypeids=replace(attentionTypeids,(select TypeID from TAB_Msg_AttentionType where nID=" + nID + "),'" + TypeID + "'), attentionNames=replace(attentionNames,(select TypeName from TAB_Msg_AttentionType where nID=" + nID + "),'" + TypeName + "'); ";
            strSql += " update TAB_Msg_AttentionType set TypeName = '" + TypeName + "',TypeID=" + TypeID + " where nID=" + nID;
           
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql) > 0;
        }
        public static bool Delete(int nID)
        {
            string strSql = "delete TAB_Msg_AttentionType where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",nID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int TypeID, string TypeName)
        {
            string strSql = "select count(*) from TAB_Msg_AttentionType where TypeName=@TypeName ";
            if (TypeID != 0)
            {
                strSql += " and nid <> @TypeID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TypeID",TypeID),
                                           new SqlParameter("TypeName",TypeName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        public static DataTable GetAllAttentionType()
        {
            string strSql = "select * from TAB_Msg_AttentionType";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
    }
}
