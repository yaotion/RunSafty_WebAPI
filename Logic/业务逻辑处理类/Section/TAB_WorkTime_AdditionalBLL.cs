using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;


namespace TF.RunSafty.Logic
{
    public class TAB_WorkTime_AdditionalBLL
    {


        public bool Add(string strTrainJiaoLuGUID, string nTMIS, int nEndWorkType, int nAddMinutes)
        {
            string strSql = "insert into  TAB_WorkTime_Additional (strTrainJiaoLuGUID,nTMIS,nEndWorkType,nAddMinutes)values(@strTrainJiaoLuGUID,@nTMIS,@nEndWorkType,@nAddMinutes)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID),
                                           new SqlParameter("nTMIS",nTMIS),
                                           new SqlParameter("nEndWorkType",nEndWorkType),
                                           new SqlParameter("nAddMinutes",nAddMinutes), 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update(string strTrainJiaoLuGUID, string nTMIS, string nid,int nAddMinutes)
        {
            string strSql = " update TAB_WorkTime_Additional set strTrainJiaoLuGUID=@strTrainJiaoLuGUID,nTMIS=@nTMIS,nAddMinutes=@nAddMinutes where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID),
                                           new SqlParameter("nTMIS",nTMIS),
                                           new SqlParameter("nid",nid), 
                                           new SqlParameter("nAddMinutes",nAddMinutes)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update(string strTrainJiaoLuGUID, string nTMIS, string nid)
        {
            string strSql = " update TAB_WorkTime_Additional set strTrainJiaoLuGUID=@strTrainJiaoLuGUID,nTMIS=@nTMIS where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID),
                                           new SqlParameter("nTMIS",nTMIS),
                                           new SqlParameter("nid",nid), 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
       

        public bool IsTrainJiaoLuExists(string strTrainJiaoLuGUID, string nTMIS)
        {
            string strSql = " select count(*) from  TAB_WorkTime_Additional where strTrainJiaoLuGUID=@strTrainJiaoLuGUID and nTMIS=@nTMIS ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID),
                                           new SqlParameter("nTMIS",nTMIS), 
                                       };
            return ((int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        public bool IsTrainJiaoLuExists(string strTrainJiaoLuGUID, string nTMIS,string nid)
        {
            string strSql = " select count(*) from  TAB_WorkTime_Additional where strTrainJiaoLuGUID=@strTrainJiaoLuGUID and nTMIS=@nTMIS and nid!=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoLuGUID",strTrainJiaoLuGUID),
                                           new SqlParameter("nTMIS",nTMIS), 
                                           new SqlParameter("nid",nid), 
                                       };
            return ((int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        public bool Delete(int nid)
        {
            string strSql = "delete from  TAB_WorkTime_Additional where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nid",nid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;

        }
    }
}
