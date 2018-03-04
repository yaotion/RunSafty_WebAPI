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

namespace ThinkFreely.RunSafty.SectionFilter
{
    public class SectionFilterBLL
    {

        public bool IsSectionExists(string strTrainJiaoLuGUID)
        {
            string strSql = "select count(*) from TAB_Base_Branch_TrainJiaoluFilter where strTrainJiaoLuGUID=@strTrainJiaoLuGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID)
                                       };
            return ((int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        public bool IsSectionExists(string strTrainJiaoLuGUID, string nid)
        {
            string strSql = "select count(*) from TAB_Base_Branch_TrainJiaoluFilter where strTrainJiaoLuGUID=@strTrainJiaoLuGUID and nid !=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID),
                                           new SqlParameter("nid",nid)
                                       };
            return ((int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        public bool Add(string strTrainJiaoLuGUID)
        {
            string strSql = "insert into  TAB_Base_Branch_TrainJiaoluFilter values(@strTrainJiaoLuGUID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool Update(string strTrainJiaoLuGUID, string nid)
        {
            string strSql = "update TAB_Base_Branch_TrainJiaoluFilter set strTrainJiaoLuGUID=@strTrainJiaoLuGUID where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID),
                                           new SqlParameter("nid",nid)
                                       };
            return ((int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        public bool Delete(string strTrainJiaoLuGUID)
        {
            string strSql = "delete from  TAB_Base_Branch_TrainJiaoluFilter where strTrainJiaoLuGUID=@strTrainJiaoLuGUID ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

    }
}
