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
    ///ModuleRelation 的摘要说明
    /// </summary>
    public class ModuleRelation
    {
        #region 属性
        public int nID = 0;
        public string nModuleID = "";
        public int nRoleID = 0;
        public int nTabIndex = 0;
        public string charRoleMark = "";
        #endregion 属性

        #region 构造函数
        public ModuleRelation()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "delete TAB_Module_Relation where nRoleID="+nRoleID+";";
            string[]str=nModuleID.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                strSql += " insert into TAB_Module_Relation (nModuleID,nRoleID) values ("+str[i]+","+nRoleID+")";
            }
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql)> 0;
        }
        public bool AddAll()
        {
            string strSql = " insert into TAB_Module_Relation (nModuleID,nRoleID) values ( select nID,@nRoleID from TAB_Module_Information)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nRoleID",nRoleID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion 增删改

        public static DataTable GetAllModuleRelation(string nRoleID)
        {
            string strSql = "select distinct nModuleID,nID,nRoleID,charRoleMark,nTabIndex from TAB_Module_Relation where nRoleID=" + nRoleID;
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
    }
}
