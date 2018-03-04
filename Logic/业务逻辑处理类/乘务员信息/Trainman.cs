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
    /// <summary>
    ///Trainman 的摘要说明
    /// </summary>
    public class Trainman
    {
        public string TrainmanGUID = "";
        public string TrainmanName = "";
        public string TrainmanNumber = "";
        public int TrainmanState = 0;
        public Trainman()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //

        }
        public static object GetTrainmanImage(string TrainmanGUID)
        {
            string strSql = "select top 1 Picture from TAB_Org_Trainman where strGUID=@strGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strGUID",TrainmanGUID)
                                       };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            return obj;
        }
        /// <summary>
        /// 查询乘务员信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="TrainmanName"></param>
        /// <param name="TrainmanNumber"></param>
        /// <param name="StationGUID"></param>
        /// <param name="DrinkResult"></param>
        /// <returns></returns>
        public static DataTable Querytrainman(int PageIndex, int PageCount, string number, string name, string work, string state, string area, string phone,string count,string picture)
        {
            string strSql = " where 1=1 ";
            strSql += number != "" ? " and strTrainmanNumber like @number " : " ";
            strSql += name != "" ? " and strTrainmanName like @name " : " ";
            strSql += work != "" ? " and nPost = @work" : " ";
            strSql += state != "" ? " and nTrainmanState = @state" : " ";
            strSql += area != "" ? " and strAreaGUID = @area" : " ";
            strSql += phone != "" ? " and strTelNumber like @phone" : " ";
            if (count != "")
            {
                strSql += count == "1" ? " and (FingerPrint1 is null or FingerPrint2 is null)" : " and (FingerPrint1 is not null or FingerPrint2 is not null)";
            }
            if (picture != "")
            {
                strSql += picture == "true" ? " and Picture is not null" : " and Picture is null ";
            }
            string Sql = "(select top " + (PageIndex * PageCount).ToString() +
                " *,(select top 1 strTrainPlanGUID from VIEW_Plan_Trainman where (strTrainmanGUID1=strTrainmanGUID or strTrainmanGUID2=strTrainmanGUID or strTrainmanGUID3=strTrainmanGUID) and nPlanState<10 order by dtChuQinTime desc) as strTrainPlanGUID " +
                " from VIEW_Nameplate_TrainmanInJiaolu_All " + strSql + " order by strTrainmanNumber desc) as tmp2";
            Sql = "(select top " + PageCount.ToString() + " * from " + Sql + " order by strTrainmanNumber) as tmp1";
            Sql = "select * from " + Sql + " order by strTrainmanNumber desc";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("number","%"+number + "%"),
                                       new SqlParameter("name","%"+name + "%"),
                                       new SqlParameter("work",work),
                                       new SqlParameter("state",state),
                                       new SqlParameter("area",area),
                                       new SqlParameter("phone","%"+phone + "%"),
                                       new SqlParameter("picture",picture)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];

        }

        /// <summary>
        /// 获取乘务员信息总数
        /// </summary>
        public static int GettrainmanCount()
        {
            string strSql = "SELECT COUNT(*) FROM VIEW_Org_Trainman";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql));
        }

        /// <summary>
        /// 计算查询条件记录总数
        /// </summary>
        /// <returns></returns>
        public static int GetselectCount(string number, string name, string work, string state, string area, string phone, string count, string picture)
        {

            string strSql = " where 1=1 ";
            strSql += number != "" ? " and strTrainmanNumber like @number " : " ";
            strSql += name != "" ? " and strTrainmanName like @name " : " ";
            strSql += work != "" ? " and nPost = @work" : " ";
            strSql += state != "" ? " and nTrainmanState = @state" : " ";
            strSql += area != "" ? " and strAreaGUID = @area" : " ";
            strSql += phone != "" ? " and strTelNumber like @phone" : " ";
            if (count != "")
            {
                strSql += count == "1" ? " and (FingerPrint1 is null or FingerPrint2 is null)" : " and (FingerPrint1 is not null or FingerPrint2 is not null)";
            }
            if (picture != "")
            {
                strSql += picture == "true" ? " and Picture is not null" : " and Picture is null ";
            }

            string Sql = "select Count(*) from (select strTrainmanNumber from VIEW_Nameplate_TrainmanInJiaolu_All " + strSql + " ) as tmp1";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("number","%"+number + "%"),
                                       new SqlParameter("name","%"+name + "%"),
                                       new SqlParameter("work",work),
                                       new SqlParameter("state",state),
                                       new SqlParameter("area",area),
                                       new SqlParameter("phone","%"+phone + "%"),
                                       new SqlParameter("picture",picture)
                                   };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams));
        }

        
        /// <summary>
        ///功能：获得根据乘务员照片获得乘务员照片
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static object GetPicture(string strID)
        {
            string strSql = "select Picture from TAB_Org_Trainman where strTrainmanGUID=@strid";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("strid",strID)
                                          };
            return SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameter);
        }

        public static DataTable QueryTrainmanDtLastArriveTime( string number, string number1)
        {
            string strSql = " where 1=1 ";
            strSql += number != "" ? " and strTrainmanNumber1 = @number " : " ";
            strSql += number1 != "" ? " and strTrainmanNumber2 = @number1 " : " ";
            string Sql = "select top 1 strGroupGUID,dtArriveTime,strTrainPlanGUID from VIEW_Plan_Trainman " + strSql + " order by dtArriveTime desc";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("number",number),
                                       new SqlParameter("number1",number1)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];

        }

        public static DataTable GetTrainmanRunDetail(string TrainmanNumber, int QueryYear, int QueryMonth)
        {
            DateTime dtBeginTime = new DateTime(QueryYear, QueryMonth, 1);
            DateTime dtEndTime = dtBeginTime.AddMonths(1).AddSeconds(-1);
            string strSql = "select * from VIEW_Plan_RunEvent where 1=1 ";
            if (TrainmanNumber != "")
            {
                strSql += " and (strTrainmanNumber1=@TrainmanNumber or strTrainmanNumber1=@TrainmanNumber)";
            }
            strSql += " and (dtEventTime between @dtBeginTime and @dtEndTime) and nEventID<>10007 and nEventID<>10008 ";
            strSql += " order by dtEventTime asc ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TrainmanNumber",TrainmanNumber),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("dtEndTime",dtEndTime)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static bool GetTrainmanByGUID(string TrainmanGUID, out Trainman QueryTrainman)
        {
            QueryTrainman = new Trainman();
            string strSql = @"select top 1 *  from TAB_Org_Trainman Where strTrainmanGUID = @strTrainmanGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainmanGUID",TrainmanGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {

                QueryTrainman.TrainmanGUID = dt.Rows[0]["strTrainmanGUID"].ToString();
                QueryTrainman.TrainmanName = dt.Rows[0]["strTrainmanName"].ToString();
                QueryTrainman.TrainmanNumber = dt.Rows[0]["strTrainmanNumber"].ToString();
                QueryTrainman.TrainmanState = int.Parse(dt.Rows[0]["strTrainmanNumber"].ToString());

                return true;
            }
            return false;

        }

        public static DataTable GetTrainmanByTelephone(string strTelephone)
        {
            string Sql = "select * from  VIEW_Org_Trainman where strTelNumber=@strTelNumber";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("strTelNumber",strTelephone), 
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];
        }

        public static bool GetTrainmanByName(string TrainmanName, out Trainman QueryTrainman)
        {
            QueryTrainman = new Trainman();
            string strSql = @"select top 1 *  from TAB_Org_Trainman Where strTrainmanName = @TrainmanName";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TrainmanName",TrainmanName)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                
                QueryTrainman.TrainmanGUID = dt.Rows[0]["strTrainmanGUID"].ToString();
                QueryTrainman.TrainmanName = dt.Rows[0]["strTrainmanName"].ToString();
                QueryTrainman.TrainmanNumber = dt.Rows[0]["strTrainmanNumber"].ToString();
                QueryTrainman.TrainmanState = int.Parse(dt.Rows[0]["strTrainmanNumber"].ToString());
                
                return true;
            }
            return false;

        }
        public static bool GetTrainmanByNumber(string TrainmanNumber, out Trainman QueryTrainman)
        {
            QueryTrainman = new Trainman();
            string strSql = @"select top 1 *  from TAB_Org_Trainman Where strTrainmanNumber = @TrainmanNumber";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TrainmanNumber",TrainmanNumber)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                
                QueryTrainman.TrainmanGUID = dt.Rows[0]["strTrainmanGUID"].ToString();
                QueryTrainman.TrainmanName = dt.Rows[0]["strTrainmanName"].ToString();
                QueryTrainman.TrainmanNumber = dt.Rows[0]["strTrainmanNumber"].ToString();
                QueryTrainman.TrainmanState = int.Parse(dt.Rows[0]["strTrainmanNumber"].ToString());
                
                return true;
            }
            return false;

        }

        /// <summary>
        /// 根据车间和职务获取人员
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="nPost"></param>
        /// <returns></returns>
        public static DataTable GetTrainmanByCheJianAndPost(string strWorkShopGUID, int nPost)
        {
            string Sql = "select * from  VIEW_Org_Trainman where strWorkShopGUID=@strWorkShopGUID and [nPostID]=@nPostID";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                       new SqlParameter("nPostID",nPost)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 根据车间和职务获取人员
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="nPost"></param>
        /// <returns></returns>
        public static DataTable GetTrainmanByCheJian(string strWorkShopGUID)
        {
            string Sql = "select * from  VIEW_Org_Trainman where strWorkShopGUID=@strWorkShopGUID ";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 根据行车计划GUID获取司机类型
        /// </summary>
        /// <param name="strPlanGUID"></param>
        /// <returns></returns>
        public static DataTable GetTrainmanTypeByPlanGUID(string strPlanGUID)
        {
            string Sql = " select * from view_plan_trainman where strTrainPlanGUID =@strTrainPlanGUID";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("strTrainPlanGUID",strPlanGUID)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];
        }
    }
}