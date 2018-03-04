using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;


namespace TF.RunSafty.Logic
{
    public class WorkTimeMonthSectionFilterBLL
    {
        /// <summary>
        /// 获取所有过滤的行车区段
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSection()
        {
            DataTable table = null;
            string sqlCommandText = "select * from TAB_WorkTime_MonthSectionFilter";
            table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlCommandText).Tables[0];
            return table;
        }

        public bool InsertSection(string strTrainJiaoluName,string strTrainJiaoGUID)
        {
            string sqlCommandText = "insert into TAB_WorkTime_MonthSectionFilter(strTrainJiaoluName,strTrainJiaoluGUID) values(@name,@id)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("name",strTrainJiaoluName),
                                           new SqlParameter("id",strTrainJiaoGUID) 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams) > 0;
        }
        public bool DeleteSection(string strTrainJiaoluGUID)
        {
            string sqlCommandText = "delete from TAB_WorkTime_MonthSectionFilter where strTrainJiaoluGUID=@strTrainJiaoluGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID), 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams) > 0;
        }
        public bool IsSectionExists(string strTrainJiaoLuGUID)
        {
            string strSql = "select count(*) from TAB_WorkTime_MonthSectionFilter where strTrainJiaoLuGUID=@strTrainJiaoLuGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID)
                                       };
            return ((int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }

        public bool IsSectionExists(string strTrainJiaoLuGUID, string nid)
        {
            string strSql = "select count(*) from TAB_WorkTime_MonthSectionFilter where strTrainJiaoLuGUID=@strTrainJiaoLuGUID and nid !=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID),
                                           new SqlParameter("nid",nid)
                                       };
            return ((int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }

        public bool Update(string strTrainJiaoLuGUID, string nid)
        {
            string strSql = "update TAB_WorkTime_MonthSectionFilter set strTrainJiaoLuGUID=@strTrainJiaoLuGUID where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID),
                                           new SqlParameter("nid",nid)
                                       };
            return ((int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
    }
}
