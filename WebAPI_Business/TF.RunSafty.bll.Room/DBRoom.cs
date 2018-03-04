using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.Room
{
    public class DBRoom
    {

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsInRoom(string strInRoomGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TAB_Plan_InRoom");
            strSql.Append(" where strInRoomGUID=@strInRoomGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@strInRoomGUID", SqlDbType.VarChar,50)
			};
            parameters[0].Value = strInRoomGUID;

            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }


        /// <summary>
        /// 更新入寓数据
        /// </summary>
        public bool UpdateInRoom(TAB_Plan_InRoom model)
        {

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            try
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update TAB_Plan_InRoom set ");
                        strSql.Append("strTrainPlanGUID=@strTrainPlanGUID,");
                        strSql.Append("strTrainmanGUID=@strTrainmanGUID,");
                        strSql.Append("dtInRoomTime=@dtInRoomTime,");
                        strSql.Append("nInRoomVerifyID=@nInRoomVerifyID,");
                        strSql.Append("strDutyUserGUID=@strDutyUserGUID,");
                        strSql.Append("strTrainmanNumber=@strTrainmanNumber,");
                        strSql.Append("strSiteGUID=@strSiteGUID,");
                        strSql.Append("strRoomNumber=@strRoomNumber,");
                        strSql.Append("nBedNumber=@nBedNumber");
                        strSql.Append(" where strInRoomGUID=@strInRoomGUID");
                        SqlParameter[] parameters = {
					new SqlParameter("@strInRoomGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtInRoomTime", SqlDbType.DateTime),
					new SqlParameter("@nInRoomVerifyID", SqlDbType.Int,4),
					new SqlParameter("@strDutyUserGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanNumber", SqlDbType.VarChar,50),
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strRoomNumber", SqlDbType.VarChar,50),
					new SqlParameter("@nBedNumber", SqlDbType.Int,4)
                                        };
                        parameters[0].Value = model.strInRoomGUID;
                        parameters[1].Value = model.strTrainPlanGUID;
                        parameters[2].Value = model.strTrainmanGUID;
                        parameters[3].Value = model.dtInRoomTime;
                        parameters[4].Value = model.nInRoomVerifyID;
                        parameters[5].Value = model.strDutyUserGUID;
                        parameters[6].Value = model.strTrainmanNumber;
                        parameters[7].Value = model.strSiteGUID;
                        parameters[8].Value = model.strRoomNumber;
                        parameters[9].Value = model.nBedNumber;
                        int rows = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                        if (rows > 0)
                        {
                            string strSqlUpTrainman = "update TAB_Org_Trainman set dtLastInRoomTime='" + model.dtInRoomTime + "' where strTrainmanGUID='" + model.strTrainmanGUID + "'";
                            int rows2 = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSqlUpTrainman.ToString());
                            if (rows2 > 0)
                            {
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                trans.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }

            }
            finally
            {
                conn.Close();
            }

        }





        /// <summary>
        /// 增加一条数据InRoom
        /// </summary>
        public bool AddInRoom(TAB_Plan_InRoom model)
        {

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            try
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into TAB_Plan_InRoom(");
                        strSql.Append("strInRoomGUID,strTrainPlanGUID,strTrainmanGUID,dtInRoomTime,nInRoomVerifyID,strDutyUserGUID,strTrainmanNumber,strSiteGUID,strRoomNumber,nBedNumber)");
                        strSql.Append(" values (");
                        strSql.Append("@strInRoomGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtInRoomTime,@nInRoomVerifyID,@strDutyUserGUID,@strTrainmanNumber,@strSiteGUID,@strRoomNumber,@nBedNumber)");
                        SqlParameter[] parameters = {
					new SqlParameter("@strInRoomGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtInRoomTime", SqlDbType.DateTime),
					new SqlParameter("@nInRoomVerifyID", SqlDbType.Int,4),
					new SqlParameter("@strDutyUserGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanNumber", SqlDbType.VarChar,50),
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strRoomNumber", SqlDbType.VarChar,50),
					new SqlParameter("@nBedNumber", SqlDbType.Int,4)
                                        };
                        parameters[0].Value = model.strInRoomGUID;
                        parameters[1].Value = model.strTrainPlanGUID;
                        parameters[2].Value = model.strTrainmanGUID;
                        parameters[3].Value = model.dtInRoomTime;
                        parameters[4].Value = model.nInRoomVerifyID;
                        parameters[5].Value = model.strDutyUserGUID;
                        parameters[6].Value = model.strTrainmanNumber;
                        parameters[7].Value = model.strSiteGUID;
                        parameters[8].Value = model.strRoomNumber;
                        parameters[9].Value = model.nBedNumber;

                        int rows = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                        if (rows > 0)
                        {
                            string strSqlUpTrainman = "update TAB_Org_Trainman set dtLastInRoomTime='" + model.dtInRoomTime + "' where strTrainmanGUID='" + model.strTrainmanGUID + "'";
                            int rows2 = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSqlUpTrainman.ToString());
                            if (rows2 > 0)
                            {
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                trans.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
        }




        /// <summary>
        /// 是否存在出寓该记录
        /// </summary>
        public bool ExistsOutRoom(string strOutRoomGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TAB_Plan_OutRoom");
            strSql.Append(" where strOutRoomGUID=@strOutRoomGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@strOutRoomGUID", SqlDbType.VarChar,50)
			};
            parameters[0].Value = strOutRoomGUID;

            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }


        /// <summary>
        /// 更新入寓数据
        /// </summary>
        public bool UpdateOutRoom(TAB_Plan_OutRoom model)
        {
            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            try
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update TAB_Plan_OutRoom set ");
                        strSql.Append("strTrainPlanGUID=@strTrainPlanGUID,");
                        strSql.Append("strTrainmanGUID=@strTrainmanGUID,");
                        strSql.Append("dtOutRoomTime=@dtOutRoomTime,");
                        strSql.Append("nOutRoomVerifyID=@nOutRoomVerifyID,");
                        strSql.Append("strDutyUserGUID=@strDutyUserGUID,");
                        strSql.Append("strTrainmanNumber=@strTrainmanNumber,");
                        strSql.Append("strInRoomGUID=@strInRoomGUID,");
                        strSql.Append("strSiteGUID=@strSiteGUID");
                        strSql.Append(" where strOutRoomGUID=@strOutRoomGUID");
                        SqlParameter[] parameters = {
					new SqlParameter("@strOutRoomGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtOutRoomTime", SqlDbType.DateTime),
					new SqlParameter("@nOutRoomVerifyID", SqlDbType.Int,4),
					new SqlParameter("@strDutyUserGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanNumber", SqlDbType.VarChar,50),
					new SqlParameter("@strInRoomGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50)
                                        };
                        parameters[0].Value = model.strOutRoomGUID;
                        parameters[1].Value = model.strTrainPlanGUID;
                        parameters[2].Value = model.strTrainmanGUID;
                        parameters[3].Value = model.dtOutRoomTime;
                        parameters[4].Value = model.nOutRoomVerifyID;
                        parameters[5].Value = model.strDutyUserGUID;
                        parameters[6].Value = model.strTrainmanNumber;
                        parameters[7].Value = model.strInRoomGUID;
                        parameters[8].Value = model.strSiteGUID;
                        int rows = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                        if (rows > 0)
                        {
                            string strSql3 = "update TAB_Org_Trainman set dtLastOutRoomTime='" + model.dtOutRoomTime + "' where strTrainmanGUID='" + model.strTrainmanGUID + "'";
                            int rows2 = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql3.ToString());
                            if (rows2 > 0)
                            {
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                trans.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
        }



        /// <summary>
        /// 增加一条数据OutRoom
        /// </summary>
        public bool AddOutRoom(TAB_Plan_OutRoom model)
        {
            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            try
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into TAB_Plan_OutRoom(");
                        strSql.Append("strOutRoomGUID,strTrainPlanGUID,strTrainmanGUID,dtOutRoomTime,nOutRoomVerifyID,strDutyUserGUID,strTrainmanNumber,strInRoomGUID,strSiteGUID)");
                        strSql.Append(" values (");
                        strSql.Append("@strOutRoomGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtOutRoomTime,@nOutRoomVerifyID,@strDutyUserGUID,@strTrainmanNumber,@strInRoomGUID,@strSiteGUID)");
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
					new SqlParameter("@strSiteGUID", SqlDbType.VarChar,50)
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


                        int rows = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
                        if (rows > 0)
                        {
                            string strSql3 = "update TAB_Org_Trainman set dtLastOutRoomTime='" + model.dtOutRoomTime + "' where strTrainmanGUID='" + model.strTrainmanGUID + "'";
                            int rows2 = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql3.ToString());
                            if (rows2 > 0)
                            {
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                trans.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
        }



    }
}
