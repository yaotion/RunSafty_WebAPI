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
///JiaoluRelation 的摘要说明
/// </summary>
    public class JiaoluRelation
    {
        #region 属性
        public string strRelationGUID = "";
        public string strTrainJiaoluGUID = "";
        public int nKehuoID = 0;
        public string strTrainmanJiaoluGUID = "";
        public int nTrainmanTypeID = 0;
        #endregion 属性

        #region 构造函数
        public JiaoluRelation()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public JiaoluRelation(string RelationGUID)
        {
            string strSql = "select * from TAB_Base_JiaoluRelation where strRelationGUID=@strRelationGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strRelationGUID",RelationGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strRelationGUID = dt.Rows[0]["strRelationGUID"].ToString();
                strTrainJiaoluGUID = dt.Rows[0]["strTrainJiaoluGUID"].ToString();
                strTrainmanJiaoluGUID = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                nKehuoID = int.Parse(dt.Rows[0]["nKehuoID"].ToString());
                nTrainmanTypeID = int.Parse(dt.Rows[0]["nTrainmanTypeID"].ToString());
            }
        }
        #endregion 构造函数

        #region 增删改
        public string Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_JiaoluRelation (strRelationGUID,strTrainJiaoluGUID,strTrainmanJiaoluGUID,nKehuoID,nTrainmanTypeID) " +
                " values (@strRelationGUID,@strTrainJiaoluGUID,@strTrainmanJiaoluGUID,@nKehuoID,@nTrainmanTypeID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strRelationGUID",guid),
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID),
                                           new SqlParameter("nKehuoID",nKehuoID),
                                           new SqlParameter("nTrainmanTypeID",nTrainmanTypeID)
                                           
                                       };
            if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams) == 0)
                return "";
            return guid;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_JiaoluRelation set strTrainJiaoluGUID = @strTrainJiaoluGUID,strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID,nKehuoID=@nKehuoID,nTrainmanTypeID=@nTrainmanTypeID where strRelationGUID=@strRelationGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strRelationGUID",strRelationGUID),
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID),
                                           new SqlParameter("nKehuoID",nKehuoID),
                                           new SqlParameter("nTrainmanTypeID",nTrainmanTypeID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string RelationGUID)
        {
            string strSql = "delete from TAB_Base_JiaoluRelation where strRelationGUID=@strRelationGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strRelationGUID",RelationGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string RelationGUID, string TrainJiaoluGUID, string TrainmanJiaoluGUID,int TrainmanTypeID,int KeHuoID)
        {
            string strSql = "select count(*) from TAB_Base_JiaoluRelation where strTrainJiaoluGUID=@strTrainJiaoluGUID and strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID and nTrainmanTypeID=@nTrainmanTypeID and nKehuoID=@nKeHuoID";
            if (RelationGUID != "")
            {
                strSql += " and strRelationGUID <> @strRelationGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID),
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID),
                                           new SqlParameter("strRelationGUID",RelationGUID),
                                           new SqlParameter("nTrainmanTypeID",TrainmanTypeID),
                                           new SqlParameter("nKehuoID",KeHuoID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        
        #endregion 增删改


        #region 扩展方法
        public static DataTable GetRelations(int PageIndex, int PageCount,string AreaGUID,string LineGUID, string TrainJiaoluGUID, string TrainmanJiaoluGUID,int KehuoID)
        {
            string strSqlCondition = " where 1 = 1 ";
            if (AreaGUID != "")
            {
                strSqlCondition += " and strAreaGUID = @strAreaGUID ";
            }
            if (LineGUID != "")
            {
                strSqlCondition += " and strLineGUID = @strLineGUID ";
            }
            if (TrainJiaoluGUID != "")
            {
                strSqlCondition += " and strTrainJiaoluGUID = @strTrainJiaoluGUID ";
            }
            if (TrainmanJiaoluGUID != "")
            {
                strSqlCondition += " and strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID ";
            }
            if (KehuoID > 0)
            {
                strSqlCondition += " and nKehuoID = @nKehuoID ";
            }

            string strSql = "select top " + PageCount.ToString() + " * from VIEW_Base_JiaoluRelation " + strSqlCondition +
                " and strRelationGUID not in (select top " + ((PageIndex - 1) * PageCount).ToString() + " strRelationGUID from VIEW_Base_JiaoluRelation " + strSqlCondition +
                "  order by strTrainJiaoluName,strTrainmanJiaoluName)  order by strTrainJiaoluName,strTrainmanJiaoluName";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strAreaGUID",AreaGUID),
                                           new SqlParameter("strLineGUID",LineGUID),
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID),
                                           new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID),
                                           new SqlParameter("nKehuoID",KehuoID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

           
        }

        public static int GetRelationCount(string AreaGUID,string LineGUID,string TrainJiaoluGUID, string TrainmanJiaoluGUID, int KehuoID)
        {
            string strSqlCondition = " where 1 = 1 ";
            if (AreaGUID != "")
            {
                strSqlCondition += " and strAreaGUID = @strAreaGUID ";
            }
            if (LineGUID != "")
            {
                strSqlCondition += " and strLineGUID = @strLineGUID ";
            }
            if (TrainJiaoluGUID != "")
            {
                strSqlCondition += " and strTrainJiaoluGUID = @strTrainJiaoluGUID ";
            }
            if (TrainmanJiaoluGUID != "")
            {
                strSqlCondition += " and strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID ";
            }
            if (KehuoID > 0)
            {
                strSqlCondition += " and nKehuoID = @nKehuoID ";
            }

            string strSql = "select count(*) from VIEW_Base_JiaoluRelation " + strSqlCondition;

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strAreaGUID",AreaGUID),
                                           new SqlParameter("strLineGUID",LineGUID),
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID),
                                           new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID),
                                           new SqlParameter("nKehuoID",KehuoID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams));


        }

        
        #endregion
    }
}
    
