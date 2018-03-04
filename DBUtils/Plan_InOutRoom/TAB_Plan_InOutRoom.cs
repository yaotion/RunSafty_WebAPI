using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.DAL
{
    public partial class TAB_Plan_InOutRoom
    {
        /// <summary>
        /// 增加一条数据InRoom
        /// </summary>
        public bool AddInRoom(TF.RunSafty.Model.Model_Plan_InRoom model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_InRoom(");
            strSql.Append("strInRoomGUID,strTrainPlanGUID,strTrainmanGUID,dtInRoomTime,nInRoomVerifyID,strDutyUserGUID,strTrainmanNumber,dtCreateTime,strSiteGUID,strRoomNumber,nBedNumber,dtArriveTime,strWaitPlanGUID,ePlanType)");
            strSql.Append(" values (");
            strSql.Append("@strInRoomGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtInRoomTime,@nInRoomVerifyID,@strDutyUserGUID,@strTrainmanNumber,@dtCreateTime,@strSiteGUID,@strRoomNumber,@nBedNumber,@dtArriveTime,@strWaitPlanGUID,@ePlanType)");
            SqlParameter[] parameters = {
					new SqlParameter("@strInRoomGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtInRoomTime", SqlDbType.DateTime),
					new SqlParameter("@nInRoomVerifyID", SqlDbType.Int,4),
					new SqlParameter("@strDutyUserGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanNumber", SqlDbType.VarChar,50),
					new SqlParameter("@dtCreateTime", SqlDbType.DateTime),
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strRoomNumber", SqlDbType.VarChar,50),
					new SqlParameter("@nBedNumber", SqlDbType.Int,4),
					new SqlParameter("@dtArriveTime", SqlDbType.DateTime),
					new SqlParameter("@strWaitPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@ePlanType", SqlDbType.Int,4)};
            parameters[0].Value = model.strInRoomGUID;
            parameters[1].Value = model.strTrainPlanGUID;
            parameters[2].Value = model.strTrainmanGUID;
            parameters[3].Value = model.dtInRoomTime;
            parameters[4].Value = model.nInRoomVerifyID;
            parameters[5].Value = model.strDutyUserGUID;
            parameters[6].Value = model.strTrainmanNumber;
            parameters[7].Value = model.dtCreateTime;
            parameters[8].Value = model.strSiteGUID;
            parameters[9].Value = model.strRoomNumber;
            parameters[10].Value = model.nBedNumber;
            parameters[11].Value = model.dtArriveTime;
            parameters[12].Value = model.strWaitPlanGUID;
            parameters[13].Value = model.ePlanType;

            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                string strSql2 = "update TAB_Org_Trainman set dtLastInRoomTime='" + model.dtInRoomTime + "' where strTrainmanGUID='" + model.strTrainmanGUID + "'";
                int rows2 = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql2.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 增加一条数据OutRoom
        /// </summary>
        public bool AddOutRoom(TF.RunSafty.Model.Model_Plan_OutRoom model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_OutRoom(");
            strSql.Append("strOutRoomGUID,strTrainPlanGUID,strTrainmanGUID,dtOutRoomTime,nOutRoomVerifyID,strDutyUserGUID,strTrainmanNumber,dtCreateTime,strInRoomGUID,strSiteGUID,dtArriveTime,strWaitPlanGUID,ePlanType,strRoomNumber,nBedNumber)");
            strSql.Append(" values (");
            strSql.Append("@strOutRoomGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtOutRoomTime,@nOutRoomVerifyID,@strDutyUserGUID,@strTrainmanNumber,@dtCreateTime,@strInRoomGUID,@strSiteGUID,@dtArriveTime,@strWaitPlanGUID,@ePlanType,@strRoomNumber,@nBedNumber)");
            SqlParameter[] parameters = {
					new SqlParameter("@strOutRoomGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtOutRoomTime", SqlDbType.DateTime),
					new SqlParameter("@nOutRoomVerifyID", SqlDbType.Int,4),
					new SqlParameter("@strDutyUserGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanNumber", SqlDbType.VarChar,50),
					new SqlParameter("@dtCreateTime", SqlDbType.DateTime),
					new SqlParameter("@strInRoomGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtArriveTime", SqlDbType.DateTime),
					new SqlParameter("@strWaitPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@ePlanType", SqlDbType.Int,4),
                    new SqlParameter("@strRoomNumber", SqlDbType.VarChar,50),
					new SqlParameter("@nBedNumber", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.strOutRoomGUID;
            parameters[1].Value = model.strTrainPlanGUID;
            parameters[2].Value = model.strTrainmanGUID;
            parameters[3].Value = model.dtOutRoomTime;
            parameters[4].Value = model.nOutRoomVerifyID;
            parameters[5].Value = model.strDutyUserGUID;
            parameters[6].Value = model.strTrainmanNumber;
            parameters[7].Value = model.dtCreateTime;
            parameters[8].Value = model.strInRoomGUID;
            parameters[9].Value = model.strSiteGUID;
            parameters[10].Value = model.dtArriveTime;
            parameters[11].Value = model.strWaitPlanGUID;
            parameters[12].Value = model.ePlanType;
            parameters[13].Value = model.strRoomNumber;
            parameters[14].Value = model.nBedNumber;


            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                string strSql3 = "update TAB_Org_Trainman set dtLastOutRoomTime='" + model.dtOutRoomTime + "' where strTrainmanGUID='" + model.strTrainmanGUID + "'";
                int rows2 = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql3.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
