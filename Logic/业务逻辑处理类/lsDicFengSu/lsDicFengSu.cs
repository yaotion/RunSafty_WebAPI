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
    public class lsDicFengSu
    {
        #region 属性
        public string Id;
        public string LocoType;
        public string Name;
        public int nCheckCount;
        #endregion 属性

        #region 构造函数
        public lsDicFengSu()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDicFengSu(string id)
        {
            string strSql = "select * from [lsDicFengSu] where [ID]=@Id";
            SqlParameter[] sqlParams = {
                    new SqlParameter("@Id",Id)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Id = dt.Rows[0]["ID"].ToString();
                LocoType = dt.Rows[0]["LocoType"].ToString();
                Name = dt.Rows[0]["Name"].ToString();
                nCheckCount = PageBase.static_ext_int(dt.Rows[0]["nCheckCount"].ToString());
            }
        }

        #endregion 构造函数


        #region 增删改
        public bool Add()
        {
            string strSql = "INSERT INTO [lsDicFengSu]  ([LocoType] ,[Name] ,[nCheckCount])  VALUES (@LocoType, @Name, @nCheckCount)";
            SqlParameter[] sqlParams = {
                new SqlParameter("@LocoType",LocoType),
                        new SqlParameter("@Name",Name),
                        new SqlParameter("@nCheckCount",nCheckCount)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "UPDATE [lsDicFengSu] SET [LocoType]=@LocoType, [Name]=@Name,[nCheckCount]=@nCheckCount WHERE [ID]=@Id";
            SqlParameter[] sqlParams = {
                new SqlParameter("@LocoType",LocoType),
                new SqlParameter("@Name",Name),
                new SqlParameter("@nCheckCount",nCheckCount),
                new SqlParameter("@Id",Id)
        };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete [lsDicFengSu] where [ID]=@Id";
            SqlParameter[] sqlParams = {
            new SqlParameter("@Id",strid)
            };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strId, string name, int nCheckCount)
        {
            string strSql = "select count(*) from [lsDicFengSu] where 1=1 and Name=@name and nCheckCount=@nCheckCount";
            strSql += strId != "" ? " and ID<>@Id" : "";
            SqlParameter[] sqlParams = {
                new SqlParameter("@Id",strId),
                new SqlParameter("@name",name),
                new SqlParameter("@nCheckCount",nCheckCount)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法

        #endregion
    }
}
