using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using TF.Api.Entity;
using ThinkFreely.DBUtility;

namespace TF.Api.DBUtility
{
    public class DBDutyReading
    {
        public static void AddRecord(string SiteGUID,string WorkID,int WorkTypeID ,string TrainmanGUID,DutyReading_Record Record)
        {
            string strSql = "insert into TAB_DutyReading_ReadRecord (strReadingGUID,dtReadTime,strSiteGUID,strWorkID,nWorkTypeID,strTrainmanGUID) values (@strReadingGUID,@dtReadTime,@strSiteGUID,@strWorkID,@nWorkTypeID,@strTrainmanGUID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strReadingGUID",Record.rid),
                                           new SqlParameter("dtReadTime",Record.rtime),
                                           new SqlParameter("strSiteGUID",SiteGUID),
                                           new SqlParameter("strWorkID",WorkID),
                                           new SqlParameter("nWorkTypeID",WorkID),
                                           new SqlParameter("strTrainmanGUID",TrainmanGUID)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams);

            strSql = "update TAB_DutyReading_TransmitDetails set readState = 1,readCount=readCount + 1 where strOrgTrainManGUID=@strTrainmanGUID and strPublicationID=@strReadingGUID ";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }
    }
}
