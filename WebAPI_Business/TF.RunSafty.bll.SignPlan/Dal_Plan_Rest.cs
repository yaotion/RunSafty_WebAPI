using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.SignPlan
{
    public partial class Dal_Plan_Rest
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddPlan(Modal_Plan_Rest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Sign(");
            strSql.Append("strGUID,dtArriveTime,dtCallTime,dtChuQinTime,strTrainNo,strTrainJiaoLuGUID,nNeedRest,dtStartTrainTime)");
            strSql.Append(" values (");
            strSql.Append("@strGUID,@dtArriveTime,@dtCallTime,@dtChuQinTime,@strTrainNo,@strTrainJiaoLuGUID,@nNeedRest,@dtStartTrainTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@strGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtArriveTime", SqlDbType.VarChar,50),
					new SqlParameter("@dtCallTime", SqlDbType.VarChar,50),
                    new SqlParameter("@dtChuQinTime", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainNo", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainJiaoLuGUID", SqlDbType.VarChar,50),
					new SqlParameter("@nNeedRest", SqlDbType.Int,4),
                    new SqlParameter("@dtStartTrainTime", SqlDbType.VarChar,50)
                                       };
            parameters[0].Value = model.strGUID;
            parameters[1].Value = model.dtArriveTime;
            parameters[2].Value = model.dtCallTime;
            parameters[3].Value = model.dtChuQinTime;
            parameters[4].Value = model.strTrainNo;
            parameters[5].Value = model.strTrainJiaoLuGUID;
            parameters[6].Value = model.nNeedRest;
            parameters[7].Value = model.dtStartTrainTime;

            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdatePlan(Modal_Plan_Rest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_Plan_Sign set ");
            strSql.Append("dtArriveTime=@dtArriveTime,");
            strSql.Append("dtCallTime=@dtCallTime,");
            strSql.Append("strTrainNo=@strTrainNo,");
            strSql.Append("dtChuQinTime=@dtChuQinTime,");
            strSql.Append("strTrainJiaoLuGUID=@strTrainJiaoLuGUID,");
            strSql.Append("nNeedRest=@nNeedRest,");
            strSql.Append("dtStartTrainTime=@dtStartTrainTime");
            strSql.Append(" where strGUID=@strGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@strGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtArriveTime", SqlDbType.DateTime),
					new SqlParameter("@dtCallTime", SqlDbType.DateTime),
					new SqlParameter("@strTrainNo", SqlDbType.VarChar,50),
					new SqlParameter("@dtChuQinTime", SqlDbType.DateTime),
                    new SqlParameter("@strTrainJiaoLuGUID", SqlDbType.VarChar,50),
                    new SqlParameter("@nNeedRest", SqlDbType.Int,4),
					new SqlParameter("@dtStartTrainTime", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.strGUID;
            parameters[1].Value = model.dtArriveTime;
            parameters[2].Value = model.dtCallTime;
            parameters[3].Value = model.strTrainNo;
            parameters[4].Value = model.dtChuQinTime;
            parameters[5].Value = model.strTrainJiaoLuGUID;
            parameters[6].Value = model.nNeedRest;
            parameters[7].Value = model.dtStartTrainTime;

            int rows1 = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);

          

            if (rows1 > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public int deletePlan(string strGUID)
        {
            string strSql = "delete from TAB_Plan_Sign where strGUID='" + strGUID + "'";
            return (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateAndFirst(Modal_Plan_Rest model, string jiaoluID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_Plan_Sign set ");
            strSql.Append("strTrainJiaoLuGUID=@strTrainJiaoLuGUID,");
            strSql.Append("dtArriveTime=@dtArriveTime,");
            strSql.Append("dtCallTime=@dtCallTime,");
            strSql.Append("strTrainNo=@strTrainNo,");
            strSql.Append("dtChuQinTime=@dtChuQinTime,");
            strSql.Append("dtStartTrainTime=@dtStartTrainTime,");
            strSql.Append("strTrainNoGUID=@strTrainNoGUID,");
            strSql.Append("strWorkGrouGUID=@strWorkGrouGUID,");
            strSql.Append("strTrainmanGUID1=@strTrainmanGUID1,");
            strSql.Append("strTrainmanGUID2=@strTrainmanGUID2,");
            strSql.Append("strTrainmanGUID3=@strTrainmanGUID3,");
            strSql.Append("strTrainmanGUID4=@strTrainmanGUID4");
            strSql.Append(" where strGUID=@strGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@strTrainJiaoLuGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtArriveTime", SqlDbType.DateTime),
					new SqlParameter("@dtCallTime", SqlDbType.DateTime),
					new SqlParameter("@strTrainNo", SqlDbType.VarChar,50),
					new SqlParameter("@dtChuQinTime", SqlDbType.DateTime),
					new SqlParameter("@dtStartTrainTime", SqlDbType.DateTime),
					new SqlParameter("@strTrainNoGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strWorkGrouGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID1", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID2", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID3", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID4", SqlDbType.VarChar,50),
					new SqlParameter("@strGUID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.strTrainJiaoLuGUID;
            parameters[1].Value = model.dtArriveTime;
            parameters[2].Value = model.dtCallTime;
            parameters[3].Value = model.strTrainNo;
            parameters[4].Value = model.dtChuQinTime;
            parameters[5].Value = model.dtStartTrainTime;
            parameters[6].Value = model.strTrainNoGUID;
            parameters[7].Value = model.strWorkGrouGUID;
            parameters[8].Value = model.strTrainmanGUID1;
            parameters[9].Value = model.strTrainmanGUID2;
            parameters[10].Value = model.strTrainmanGUID3;
            parameters[11].Value = model.strTrainmanGUID4;
            parameters[12].Value = model.strGUID;
            int rows1 = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);

            string strSql2 = "update TAB_Plan_SignJiaoLuIndex set strTrainJiaoLuGUID=" + jiaoluID + " where nID=1";
            int rows2 = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql2.ToString());

            if (rows1 + rows2 > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


 

        public bool UpdateTrainman(string strPlanGUID, string strNewTrainmanGUID, string strOldTrainmanGUID, string nTrainmanIndex, string strReason, string dtModifyTime, string strWorkGrouGUID, int ePlanState)
        {
            string strTrainmanGUIDx = "";
            if (nTrainmanIndex == "4")
            {
                strTrainmanGUIDx = "strTrainmanGUID4";
            }
            else if (nTrainmanIndex == "2")
            {
                strTrainmanGUIDx = "strTrainmanGUID2";
            }
            else if (nTrainmanIndex == "3")
            {
                strTrainmanGUIDx = "strTrainmanGUID3";
            }
            else if (nTrainmanIndex == "1" || nTrainmanIndex == "")
            {
                strTrainmanGUIDx = "strTrainmanGUID1";
            }


            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {

                    string strSql = "update TAB_Plan_sign set " + strTrainmanGUIDx + "='" + strNewTrainmanGUID + "',strWorkGrouGUID='" + strWorkGrouGUID + "',ePlanState=" + ePlanState + " where strGUID='" + strPlanGUID + "'";
                    int rows = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString());
                    if (rows > 0)
                    {
                        string strSql1 = "insert into TAB_Plan_Sign_ModifyTrainman(strNewTrainmanGUID,strOldTrainmanGUID,strReason,dtModifyTime)values('"
                       + strNewTrainmanGUID + "','" + strOldTrainmanGUID + "','" + TF.CommonUtility.TextClass.replace(strReason) + "','" + dtModifyTime + "')";
                        int rows1 = (int)SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql1.ToString());
                        if (rows1 > 0)
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



        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Modal_Plan_Rest model)
        {

            if (string.IsNullOrEmpty(model.strWorkGrouGUID))
                throw new Exception("找不到人员所属机组，或者人员处于请假状态，不能签点！");


            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_Plan_Sign set ");
            strSql.Append("strTrainJiaoLuGUID=@strTrainJiaoLuGUID,");
            strSql.Append("dtArriveTime=@dtArriveTime,");
            strSql.Append("dtCallTime=@dtCallTime,");
            strSql.Append("strTrainNo=@strTrainNo,");
            strSql.Append("dtChuQinTime=@dtChuQinTime,");
            strSql.Append("strTrainNoGUID=@strTrainNoGUID,");
            strSql.Append("strWorkGrouGUID=@strWorkGrouGUID,");
            strSql.Append("strTrainmanGUID1=@strTrainmanGUID1,");
            strSql.Append("strTrainmanGUID2=@strTrainmanGUID2,");
            strSql.Append("strTrainmanGUID3=@strTrainmanGUID3,");
            strSql.Append("strTrainmanGUID4=@strTrainmanGUID4,");
            strSql.Append("ePlanState=@ePlanState,");
            strSql.Append("nNeedRest=@nNeedRest,");
            strSql.Append("nFinished=@nFinished,");
            strSql.Append("dtSignTime=@dtSignTime,");
            strSql.Append("dtStartTrainTime=@dtStartTrainTime");
            strSql.Append(" where strGUID=@strGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@strTrainJiaoLuGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtArriveTime", SqlDbType.DateTime),
					new SqlParameter("@dtCallTime", SqlDbType.DateTime),
					new SqlParameter("@strTrainNo", SqlDbType.VarChar,50),
					new SqlParameter("@dtChuQinTime", SqlDbType.DateTime),
					new SqlParameter("@strTrainNoGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strWorkGrouGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID1", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID2", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID3", SqlDbType.VarChar,50),
                    new SqlParameter("@strTrainmanGUID4", SqlDbType.VarChar,50),
					new SqlParameter("@ePlanState", SqlDbType.VarChar,50),
                    new SqlParameter("@nNeedRest", SqlDbType.Int,4),
                     new SqlParameter("@nFinished", SqlDbType.Int,4),
                    new SqlParameter("@dtSignTime", SqlDbType.VarChar,50),
                    new SqlParameter("@dtStartTrainTime", SqlDbType.VarChar,50),
					new SqlParameter("@strGUID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.strTrainJiaoLuGUID;
            parameters[1].Value = model.dtArriveTime;
            parameters[2].Value = model.dtCallTime;
            parameters[3].Value = model.strTrainNo;
            parameters[4].Value = model.dtChuQinTime;
            parameters[5].Value = model.strTrainNoGUID;
            parameters[6].Value = model.strWorkGrouGUID;
            parameters[7].Value = model.strTrainmanGUID1;
            parameters[8].Value = model.strTrainmanGUID2;
            parameters[9].Value = model.strTrainmanGUID3;
            parameters[10].Value = model.strTrainmanGUID4;
            parameters[11].Value = model.ePlanState;
            parameters[12].Value = model.nNeedRest;
            parameters[13].Value = model.nFinished;
            parameters[14].Value = model.dtSignTime;
            parameters[15].Value = model.dtStartTrainTime;
            parameters[16].Value = model.strGUID;
            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void checkIsLeave(Modal_Plan_Rest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select strTrainmanGUID from TAB_Org_Trainman where nTrainmanState = 0  ");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["strTrainmanGUID"].ToString() == model.strTrainmanGUID1)
                    throw new Exception("人员处于非运转状态，不能签点！");
                if (dt.Rows[i]["strTrainmanGUID"].ToString() == model.strTrainmanGUID2)
                    throw new Exception("人员处于非运转状态，不能签点！");
                if (dt.Rows[i]["strTrainmanGUID"].ToString() == model.strTrainmanGUID3)
                    throw new Exception("人员处于非运转状态，不能签点！");
                if (dt.Rows[i]["strTrainmanGUID"].ToString() == model.strTrainmanGUID4)
                    throw new Exception("人员处于非运转状态，不能签点！");
            
            }


                if (!string.IsNullOrEmpty(model.strTrainmanGUID1))
                {



                }
        }




        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Modal_Plan_Rest GetModel(string strGUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from TAB_Plan_Sign ");
            strSql.Append(" where strGUID=@strGUID ");
            SqlParameter[] parameters = {
					new SqlParameter("@strGUID", SqlDbType.VarChar,50)};
            parameters[0].Value = strGUID;

            Modal_Plan_Rest model = new Modal_Plan_Rest();
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }





        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM VIEW_Plan_Sign ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by  dtChuQinTime asc");


            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        public DataSet GetPlanRest(string DtBeginDate, string DtEndDate, string strJiaoluGUID)
        {
            string strWhere = " dtChuQinTime>='" + DtBeginDate + "' and dtChuQinTime<'" + DtEndDate + "'";


            if (strJiaoluGUID != "")
            {

                strWhere += "and strTrainJiaoLuGUID='" + strJiaoluGUID + "'";
            }
            return GetList(strWhere);
        }


        public DataSet GetGetPlanRestByTimeAndWorkShop(string strWorkShopGUID,string DtBeginDate, string DtEndDate)
        {
            string strWhere = " dtArriveTime>='" + DtBeginDate + "' and dtArriveTime<='" + DtEndDate + "'";


            if (strWorkShopGUID != "")
            {
                strWhere += "and strTrainJiaoLuGUID in(" + strWorkShopGUID + ")";
            }

            strWhere += "and ePlanState =4";
            return GetList(strWhere);
        
        }






        public DataTable GetTuDingCheCi()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM VIEW_Base_TrainNo ");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

        }

        public DataTable LodTuDingCheCi(string DtBeginDate, string DtEndDate, string strJiaoluGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM VIEW_Base_TrainNo ");
            strSql.Append(" where convert(varchar(100),dtStartTime,8)>='" + DtBeginDate + "'");
            strSql.Append(" and convert(varchar(100),dtStartTime,8)<='" + DtEndDate + "'");
            strSql.Append(" and strTrainJiaoluGUID='" + strJiaoluGUID + "'");
            strSql.Append(" order by convert(varchar(100),dtStartTime,8)");


            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

        }





        public int CountPlan(string DtBeginDate, string DtEndDate, string strJiaoluGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM VIEW_Plan_Sign");
            strSql.Append(" where  dtChuQinTime>'" + DtBeginDate + "'");
            strSql.Append(" and  dtChuQinTime<'" + DtEndDate + "'");

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }



        



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Modal_Plan_Rest DataRowToModel(DataRow row)
        {
            Modal_Plan_Rest model = new Modal_Plan_Rest();
            if (row != null)
            {
                if (row["nID"] != null && row["nID"].ToString() != "")
                {
                    model.nID = int.Parse(row["nID"].ToString());
                }
                if (row["strGUID"] != null)
                {
                    model.strGUID = row["strGUID"].ToString();
                }
                if (row["strTrainJiaoLuGUID"] != null)
                {
                    model.strTrainJiaoLuGUID = row["strTrainJiaoLuGUID"].ToString();
                }
                if (row["dtArriveTime"] != null && row["dtArriveTime"].ToString() != "1900-01-01 00:00:00")
                {
                    model.dtArriveTime = row["dtArriveTime"].ToString();
                }
                else
                {
                    model.dtArriveTime = "";
                }

                if (row["dtCallTime"] != null && row["dtCallTime"].ToString() != "1900-01-01 00:00:00")
                {
                    model.dtCallTime = row["dtCallTime"].ToString();
                }
                else
                {
                    model.dtCallTime = "";
                }

                if (row["strTrainNo"] != null)
                {
                    model.strTrainNo = row["strTrainNo"].ToString();
                }
                if (row["dtChuQinTime"] != null && row["dtChuQinTime"].ToString() != "")
                {
                    model.dtChuQinTime = row["dtChuQinTime"].ToString();
                }
                else
                {
                    model.dtChuQinTime = "";
                }


                if (row["strTrainNoGUID"] != null)
                {
                    model.strTrainNoGUID = row["strTrainNoGUID"].ToString();
                }
                if (row["strWorkGrouGUID"] != null)
                {
                    model.strWorkGrouGUID = row["strWorkGrouGUID"].ToString();
                }
                if (row["strTrainmanGUID1"] != null)
                {
                    model.strTrainmanGUID1 = row["strTrainmanGUID1"].ToString();
                }
                if (row["strTrainmanGUID2"] != null)
                {
                    model.strTrainmanGUID2 = row["strTrainmanGUID2"].ToString();
                }
                if (row["strTrainmanGUID3"] != null)
                {
                    model.strTrainmanGUID3 = row["strTrainmanGUID3"].ToString();
                }
                if (row["strTrainmanGUID4"] != null)
                {
                    model.strTrainmanGUID4 = row["strTrainmanGUID4"].ToString();
                }
                if (row["nNeedRest"] != null && row["nNeedRest"].ToString() != "")
                {
                    model.nNeedRest = int.Parse(row["nNeedRest"].ToString());
                }

                if (row["nFinished"] != null && row["nFinished"].ToString() != "")
                {
                    model.nFinished = int.Parse(row["nFinished"].ToString());
                }

                if (row["ePlanState"] != null && row["ePlanState"].ToString() != "")
                {
                    model.ePlanState = int.Parse(row["ePlanState"].ToString());
                }
                if (row["dtSignTime"] != null && row["dtSignTime"].ToString() != "" && row["dtSignTime"].ToString() != "1900-01-01 00:00:00")
                {
                    model.dtSignTime = row["dtSignTime"].ToString();
                }
                else
                {
                    model.dtSignTime = "";
                }
                if (row["dtStartTrainTime"] != null && row["dtStartTrainTime"].ToString() != "")
                {
                    model.dtStartTrainTime = row["dtStartTrainTime"].ToString();
                }
                else
                {
                    model.dtStartTrainTime = "";
                }

            }
            return model;
        }


        public int IsTure(string strTrainNo ,string dtStartTime)
        {
            string strSql = "select * from TAB_Plan_Train where strTrainNo='" + strTrainNo + "'and  dtStartTime='" + dtStartTime + "' and nPlanState>0";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0].Rows.Count;
        
        
        }


       



        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public int AddPlanTrain(TF.RunSafty.Model.TAB_Plan_Train model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into TAB_Plan_Train(");
        //    strSql.Append("strTrainNo,dtChuQinTime,nNeedRest,dtArriveTime,dtCallTime,nDragType,nKehuoID,nPlanType,nRemarkType,nTrainmanTypeID,strTrainJiaoluGUID,strTrainTypeName,dtStartTime,dtFirstStartTime,dtRealStartTime,nPlanState,strPlaceID,strStartStation,strEndStation,strRemark,strTrainNumber,strCreateSiteGUID,strCreateUserGUID");
        //    strSql.Append(") values (");
        //    strSql.Append("@strTrainNo,@dtChuQinTime,@nNeedRest,@dtArriveTime,@dtCallTime,@nDragType,@nKehuoID,@nPlanType,@nRemarkType,@nTrainmanTypeID,@strTrainJiaoluGUID,@strTrainTypeName,@dtStartTime,@dtFirstStartTime,@dtRealStartTime,@nPlanState,@strPlaceID,@strStartStation,@strEndStation,@strRemark,@strTrainNumber,@strCreateSiteGUID,@strCreateUserGUID");
        //    strSql.Append(") ");
        //    SqlParameter[] parameters = {
        //                new SqlParameter("@strTrainNo", SqlDbType.VarChar,50) ,            
        //                new SqlParameter("@dtChuQinTime", SqlDbType.DateTime) ,            
        //                new SqlParameter("@dtArriveTime", SqlDbType.DateTime) ,            
        //                new SqlParameter("@dtCallTime", SqlDbType.DateTime),
        //                new SqlParameter("@nDragType", SqlDbType.Int,4),
        //                new SqlParameter("@nKehuoID", SqlDbType.Int,4),
        //                new SqlParameter("@nNeedRest", SqlDbType.Int,4),
        //                new SqlParameter("@nPlanType", SqlDbType.Int,4),
        //                new SqlParameter("@nRemarkType", SqlDbType.Int,4),
        //                new SqlParameter("@nTrainmanTypeID", SqlDbType.Int,4),
        //                new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50),
        //                new SqlParameter("@strTrainTypeName", SqlDbType.VarChar,50),
        //                new SqlParameter("@dtStartTime", SqlDbType.DateTime),
        //                new SqlParameter("@dtFirstStartTime", SqlDbType.DateTime),
        //                new SqlParameter("@dtRealStartTime", SqlDbType.DateTime),
        //                new SqlParameter("@nPlanState", SqlDbType.Int,4),
        //                new SqlParameter("@strPlaceID", SqlDbType.VarChar,50),
        //                new SqlParameter("@strStartStation", SqlDbType.VarChar,50),
        //                new SqlParameter("@strEndStation", SqlDbType.VarChar,50),
        //                new SqlParameter("@strRemark", SqlDbType.VarChar,50),
        //                new SqlParameter("@strTrainNumber", SqlDbType.VarChar,50),
        //                new SqlParameter("@strCreateSiteGUID", SqlDbType.VarChar,50),
        //                new SqlParameter("@strCreateUserGUID", SqlDbType.VarChar,50)
              
        //    };
        //    parameters[0].Value = model.strTrainNo;
        //    parameters[1].Value = model.dtChuQinTime;
        //    parameters[2].Value = model.dtArriveTime;
        //    parameters[3].Value = model.dtCallTime;
        //    parameters[4].Value = model.nDragType;
        //    parameters[5].Value = model.nKehuoID;
        //    parameters[6].Value = model.nNeedRest;
        //    parameters[7].Value = model.nPlanType;
        //    parameters[8].Value = model.nRemarkType;
        //    parameters[9].Value = model.nTrainmanTypeID;
        //    parameters[10].Value = model.strTrainJiaoluGUID;
        //    parameters[11].Value = model.strTrainTypeName;
        //    parameters[12].Value = model.dtStartTime;
        //    parameters[13].Value = model.dtFirstStartTime;
        //    parameters[14].Value = model.dtRealStartTime;
        //    parameters[15].Value = model.nPlanState;
        //    parameters[16].Value = model.strPlaceID;
        //    parameters[17].Value = model.strStartStation;
        //    parameters[18].Value = model.strEndStation;
        //    parameters[19].Value = model.strRemark;
        //    parameters[20].Value = model.strTrainNumber;
        //    parameters[21].Value = model.strCreateSiteGUID;
        //    parameters[22].Value = model.strCreateUserGUID;

        //    int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        //    if (rows > 0)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Modal_Plan_Rest DataRowToModel2(DataRow row)
        {
            Modal_Plan_Rest model = new Modal_Plan_Rest();
            if (row != null)
            {
                if (row["nID"] != null && row["nID"].ToString() != "")
                {
                    model.nID = int.Parse(row["nID"].ToString());
                }
                if (row["strGUID"] != null)
                {
                    model.strGUID = row["strGUID"].ToString();
                }
                if (row["strTrainJiaoLuGUID"] != null)
                {
                    model.strTrainJiaoLuGUID = row["strTrainJiaoLuGUID"].ToString();
                }
                if (row["dtArriveTime"] != null && row["dtArriveTime"].ToString() != "1900-01-01 00:00:00")
                {
                    model.dtArriveTime = Convert.ToDateTime(row["dtArriveTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    model.dtArriveTime = "";
                }

                if (row["dtCallTime"] != null && row["dtCallTime"].ToString() != "1900-01-01 00:00:00")
                {
                    model.dtCallTime =  Convert.ToDateTime(row["dtCallTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    model.dtCallTime = "";
                }

                if (row["strTrainNo"] != null)
                {
                    model.strTrainNo = row["strTrainNo"].ToString();
                }
                if (row["dtChuQinTime"] != null && row["dtChuQinTime"].ToString() != "")
                {
                    model.dtChuQinTime = Convert.ToDateTime(row["dtChuQinTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    model.dtChuQinTime = "";
                }
             
             
                if (row["strTrainNoGUID"] != null)
                {
                    model.strTrainNoGUID = row["strTrainNoGUID"].ToString();
                }
                if (row["strWorkGrouGUID"] != null)
                {
                    model.strWorkGrouGUID = row["strWorkGrouGUID"].ToString();
                }
                if (row["strTrainmanGUID1"] != null)
                {
                    model.strTrainmanGUID1 = row["strTrainmanGUID1"].ToString();
                }
                if (row["strTrainmanGUID2"] != null)
                {
                    model.strTrainmanGUID2 = row["strTrainmanGUID2"].ToString();
                }
                if (row["strTrainmanGUID3"] != null)
                {
                    model.strTrainmanGUID3 = row["strTrainmanGUID3"].ToString();
                }
                if (row["strTrainmanGUID4"] != null)
                {
                    model.strTrainmanGUID4 = row["strTrainmanGUID4"].ToString();
                }
                if (row["nNeedRest"] != null && row["nNeedRest"].ToString() != "")
                {
                    model.nNeedRest = int.Parse(row["nNeedRest"].ToString());
                }

                if (row["nFinished"] != null && row["nFinished"].ToString() != "")
                {
                    model.nFinished = int.Parse(row["nFinished"].ToString());
                }

                if (row["ePlanState"] != null && row["ePlanState"].ToString() != "")
                {
                    model.ePlanState = int.Parse(row["ePlanState"].ToString());
                }
                if (row["dtSignTime"] != null && row["dtSignTime"].ToString() != "" && row["dtSignTime"].ToString() != "1900-01-01 00:00:00")
                {
                    model.dtSignTime = Convert.ToDateTime(row["dtSignTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"); 
                }
                else
                {
                    model.dtSignTime = "";
                }
                if (row["dtStartTrainTime"] != null && row["dtStartTrainTime"].ToString() != "")
                {
                    model.dtStartTrainTime = Convert.ToDateTime(row["dtStartTrainTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"); 
                }
                else
                {
                    model.dtStartTrainTime = "";
                }


                if (row["strTrainmanName1"] != null && row["strTrainmanName1"].ToString() != "")
                {
                    model.strTrainmanName1 = row["strTrainmanName1"].ToString();
                }
                else
                {
                    model.strTrainmanName1 = "";
                }


                if (row["strTrainmanName2"] != null && row["strTrainmanName2"].ToString() != "")
                {
                    model.strTrainmanName2 = row["strTrainmanName2"].ToString();
                }
                else
                {
                    model.strTrainmanName2 = "";
                }
                if (row["strTrainmanName3"] != null && row["strTrainmanName3"].ToString() != "")
                {
                    model.strTrainmanName3 = row["strTrainmanName3"].ToString();
                }
                else
                {
                    model.strTrainmanName3 = "";
                }

                if (row["strTrainmanName4"] != null && row["strTrainmanName4"].ToString() != "")
                {
                    model.strTrainmanName4 = row["strTrainmanName4"].ToString();
                }
                else
                {
                    model.strTrainmanName4 = "";
                }

                if (row["strTrainmanNumber4"] != null && row["strTrainmanNumber4"].ToString() != "")
                {
                    model.strTrainmanNumber4 = row["strTrainmanNumber4"].ToString();
                }
                else
                {
                    model.strTrainmanNumber4 = "";
                }

                if (row["strTrainmanNumber3"] != null && row["strTrainmanNumber3"].ToString() != "")
                {
                    model.strTrainmanNumber3 = row["strTrainmanNumber3"].ToString();
                }
                else
                {
                    model.strTrainmanNumber3 = "";
                }

                if (row["strTrainmanNumber2"] != null && row["strTrainmanNumber2"].ToString() != "")
                {
                    model.strTrainmanNumber2 = row["strTrainmanNumber2"].ToString();
                }
                else
                {
                    model.strTrainmanNumber2 = "";
                }

                if (row["strTrainmanNumber1"] != null && row["strTrainmanNumber1"].ToString() != "")
                {
                    model.strTrainmanNumber1 = row["strTrainmanNumber1"].ToString();
                }
                else
                {
                    model.strTrainmanNumber1 = "";
                }
                
            }
            return model;
        }
    }
}
