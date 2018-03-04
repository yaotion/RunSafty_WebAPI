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
using System.Data.SqlClient;
using ThinkFreely.DBUtility;


namespace ThinkFreely.RunSafty
{
    public class Model_TransmitDetails
    {
        public int nid { get; set; }
        public string strOrgTrainManGUID { get; set; }
        public string strPublicationID { get; set; }
        public int readState { get; set; }
        public int readCount { get; set; }
    }


    /// <summary>
    ///TAB_TransmitDetails 的摘要说明
    /// </summary>
    public class TAB_TransmitDetails
    {
        public TAB_TransmitDetails()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region 增删改查
        public bool AddTransmitDetails(Model_TransmitDetails model)
        {
            string strSql = @"insert into [TAB_System_TransmitDetails] ([strOrgTrainManGUID]
           ,[strPublicationID]
           ,[readState]
           ,[readCount]) values (@strOrgTrainManGUID
           ,@strPublicationID
           ,@readState
           ,@readCount)";
            SqlParameter[] sqlParams ={
                                        new SqlParameter("strOrgTrainManGUID",SqlDbType.VarChar,50,model.strOrgTrainManGUID),
                                        new SqlParameter("strPublicationID",model.strPublicationID),
                                        new SqlParameter("readState",model.readState),
                                        new SqlParameter("readCount",model.readCount)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool UpdateTransmitDetails(Model_TransmitDetails model)
        {
            string strSql = @"update TAB_System_TransmitDetails 
                            set strOrgTrainManGUID=@strOrgTrainManGUID,
                            strPublicationID=@strPublicationID
                            readState=@readState
                            readCount=@readCount
                            where nid=@nid";
            SqlParameter[] sqlParams ={ 
                                        new SqlParameter("strOrgTrainManGUID",SqlDbType.VarChar,50,model.strOrgTrainManGUID),
                                        new SqlParameter("strPublicationID",model.strPublicationID),
                                        new SqlParameter("readState",model.readState),
                                        new SqlParameter("readCount",model.readCount),
                                       new SqlParameter("nid",model.nid)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 传达阅读统计
        /// </summary>
        /// <param name="strPublicationID"></param>
        /// <returns></returns>
        public DataTable GetStatisticsData(string strPublicationID)
        {
            string strSql = @"select readcount,(case readstate when 0 then '未阅读' else '已阅读' end )as readstate from( select COUNT(1) as readcount,readstate
            from [TAB_System_TransmitDetails] 
                where strPublicationID=@rid
                    group by readState) b";
            SqlParameter[] sqlParams ={ 
                                          new SqlParameter("rid",strPublicationID)
                                     };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public int GetReadingStatistics(string strReadingGUID, int readState)
        {
            string strSql = @"select COUNT(1) as readcount,readstate
            from [TAB_System_TransmitDetails] 
                where strPublicationID=@rid and readstate=@readstate
                    group by readState";
            SqlParameter[] sqlParams ={ 
                                          new SqlParameter("rid",strReadingGUID),
                                          new SqlParameter("readstate",readState)
                                     };
            object count=SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            return count == null ? 0 : Convert.ToInt32(count);
        }

        public DataTable GetTrainmansOfReading(string strReadingGUID, int readState, int startIndex, int endIndex)
        {
            string  strSql = @"   SELECT * FROM ( 
                         SELECT ROW_NUMBER() OVER(order by t.strTrainmanName) as Row, t.* from(
                       select v.strTrainmanName from TAB_System_TransmitDetails d
                       left join VIEW_Org_Trainman v
                       on d.strOrgTrainManGUID=v.strTrainmanGUID
                    where d.strPublicationID=@strReadingGUID and d.readState=@readState
                       ) t
                      )tt
                      where Row between @startIndex and @endIndex ";
            SqlParameter[] sqlParams = { 
                                       new SqlParameter("strReadingGUID",strReadingGUID),
                                       new SqlParameter("readState",readState),
                                       new SqlParameter("startIndex",startIndex),
                                       new SqlParameter("endIndex",endIndex)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        #endregion
    }
}