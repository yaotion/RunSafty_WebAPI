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
    ///Nameplate 的摘要说明
    /// </summary>
    public class Nameplate
    {
        public Nameplate()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static Boolean DeleteNamedGroupByJiaoLu(string TrainmanjiaoluID)
        {
            string strSql = "DELETE FROM dbo.TAB_Nameplate_TrainmanJiaolu_Named WHERE strTrainmanJiaoluGUID =@strTrainmanJiaoluGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("strTrainmanJiaoluGUID",TrainmanjiaoluID), 
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters) > 0;
        }


        public static DataTable GetNameplate(string TrainmanJiaoluGUID,int nJiaoluType)
        {
            string strSql = "";
            switch (nJiaoluType)
            {
                    ///非运转
                case 0: { strSql = "select * from VIEW_Nameplate_TrainmanJiaolu_Other where strTrainmanJiaoluGUID= @strTrainmanJiaoluGUID"; break; };
                    //预备
                case 1: { strSql = "select * from VIEW_Nameplate_TrainmanJiaolu_Other where strTrainmanJiaoluGUID= @strTrainmanJiaoluGUID"; break; };
                    //记名
                case 2: { strSql = "select *,(select top 1 strTrainPlanGUID from VIEW_Plan_Trainman where (VIEW_Plan_Trainman.strGroupGUID=VIEW_Nameplate_TrainmanJiaolu_Named.strGroupGUID) and nPlanState<10 order by dtChuQinTime desc) as strTrainPlanGUID from VIEW_Nameplate_TrainmanJiaolu_Named  where strTrainmanJiaoluGUID= @strTrainmanJiaoluGUID order by nCheciOrder"; break; };
                    //轮
                case 3: { strSql = "select *,(select top 1 strTrainPlanGUID from VIEW_Plan_Trainman where (VIEW_Plan_Trainman.strGroupGUID=VIEW_Nameplate_TrainmanJiaolu_Order.strGroupGUID) and nPlanState<10 order by dtChuQinTime desc) as strTrainPlanGUID from VIEW_Nameplate_TrainmanJiaolu_Order   where strTrainmanJiaoluGUID= @strTrainmanJiaoluGUID order by strStationGUID,nOrder"; break; };
                    //包
                case 4: { strSql = "select *,(select top 1 strTrainPlanGUID from VIEW_Plan_Trainman where (VIEW_Plan_Trainman.strGroupGUID=VIEW_Nameplate_TrainmanJiaolu_TogetherTrain.strGroupGUID) and nPlanState<10 order by dtChuQinTime desc) as strTrainPlanGUID from VIEW_Nameplate_TrainmanJiaolu_TogetherTrain where strTrainmanJiaoluGUID= @strTrainmanJiaoluGUID   order by dtCreateTime,strTrainGUID,nOrder"; break; };
                default: break;
            }

            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strTrainmanJiaoluGUID",TrainmanJiaoluGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
           
        }


        public static string GetTrainplanOfTrainman(string TrainmanGUID)
        {
            string strSql = @"select * from TAB_Nameplate_Group where strTrainmanGUID1 = @TrainmanGUID 
                or strTrainmanGUID2 = @TrainmanGUID or strTrainmanGUID3 = @TrainmanGUID or strTrainmanGUID4 = @TrainmanGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TrainmanGUID",TrainmanGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["strTrainPlanGUID"].ToString();
            }
            return "";
        }

        public static int GetTrainPlanState(string TrainPlanGUID)
        {
            string strSql = @"select * from TAB_Plan_Train where strTrainPlanGUID = @TrainPlanGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("TrainPlanGUID",TrainPlanGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0]["nPlanState"].ToString());
            }
            return 0;
        }

    }
}
