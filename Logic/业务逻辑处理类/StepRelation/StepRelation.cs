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

namespace TF.RunSafty.Logic
{
    /// <summary>
    /// 类名：StepRelation
    /// 描述：整备步骤关系数据操作类
    /// </summary>
    public class StepRelation
    {
        #region 属性
        public string nID;
        public string PID;//父级步骤id
        public string SID;//子id
        public string strCaseID;//环节id
        #endregion 属性

        #region 构造函数
        public StepRelation()
        {
        }

        public StepRelation(string nId)
        {
            string strSql = "select * from [Tab_StepRelation] where [nID]=@nID";
            SqlParameter[] sqlParams = {
                    new SqlParameter("@nID",nId)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                nID = PageBase.static_ext_string(dt.Rows[0]["nID"]);
                PID = PageBase.static_ext_string(dt.Rows[0]["PID"]);
                SID = PageBase.static_ext_string(dt.Rows[0]["SID"]);
                strCaseID = PageBase.static_ext_string(dt.Rows[0]["strCaseID"]);
            }
        }
        #endregion 构造函数


        #region 增删改
        public bool Add()
        {
            string strSql = "INSERT INTO [Tab_StepRelation]  ([PID] ,[SID] ,[strCaseID])  VALUES (@PID, @SID, @strCaseID)";
            SqlParameter[] sqlParams = {
                new SqlParameter("@PID",PID),
                        new SqlParameter("@SID",SID),
                        new SqlParameter("@strCaseID",strCaseID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "UPDATE [Tab_StepRelation] SET [PID]=@PID, [SID]=@SID,[strCaseID]=@strCaseID WHERE [nID]=@nID";
            SqlParameter[] sqlParams = {
                new SqlParameter("@PID",PID),
                new SqlParameter("@SID",SID),
                new SqlParameter("@strCaseID",strCaseID),
                new SqlParameter("@nID",nID)
        };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string nId)
        {
            string strSql = "delete [Tab_StepRelation] where [nID]=@nID";
            SqlParameter[] sqlParams = {
            new SqlParameter("@nID",nId)
            };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string nId)
        {
            string strSql = "select count(*) from [Tab_StepRelation] where [nID]=@nId";
            SqlParameter[] sqlParams = {
                new SqlParameter("@nID",nId)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法


        /// <summary>
        /// 获取表中所有数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAll(string nId, string sID, string pID, string strCaseId)
        {
            string strSql = "select * from [Tab_StepRelation] where 1=1";
            strSql += ((nId.Trim() != string.Empty) ? " and [nID]=@nId" : string.Empty);
            strSql += ((sID.Trim() != string.Empty) ? " and [SID]=@sID" : string.Empty);
            strSql += ((pID.Trim() != string.Empty) ? " and [PID]=@pID" : string.Empty);
            strSql += ((strCaseId.Trim() != string.Empty) ? " and [strCaseId]=@strCaseId" : string.Empty);

            SqlParameter[] sqlParams = {
                new SqlParameter("@nID",nId),
                   new SqlParameter("@SID",sID),
                      new SqlParameter("@pID",pID),
                         new SqlParameter("@strCaseId",strCaseId)
            };

            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 根据删除指定SID下属的所有记录
        /// </summary>
        /// <param name="sID">sID</param>
        /// <returns></returns>
        public static bool DeleteAllItemBySID(string sID)
        {
            string strSql = "delete [Tab_StepRelation] where [sID]=@sID";
            SqlParameter[] sqlParams = {
            new SqlParameter("@sID",sID)
            };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }


        #endregion
    }
}