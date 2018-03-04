using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.DAL
{
    public partial class GetAllSign
    {

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList(string strGuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM VIEW_Plan_Sign ");
            if (strGuid.Trim() != "")
            {
                strSql.Append(" where strGUID= '" + strGuid+"'");
            }
            strSql.Append(" order by  dtChuQinTime asc");


            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.TAB_Plan_Rest DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.TAB_Plan_Rest model = new TF.RunSafty.Model.TAB_Plan_Rest();
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
                if (row["ePlanState"] != null && row["ePlanState"].ToString() != "")
                {
                    model.ePlanState = int.Parse(row["ePlanState"].ToString());
                }
                if (row["dtSignTime"] != null && row["dtSignTime"].ToString() != "")
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



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.Model_Plan_ToBeTake DataRowToModelBeginWork(DataRow row)
        {
            TF.RunSafty.Model.Model_Plan_ToBeTake model = new TF.RunSafty.Model.Model_Plan_ToBeTake();
            if (row != null)
            {
                

                if (row["dtArriveTime"] != null && row["dtArriveTime"].ToString() != "")
                {
                    model.dtWaitWorkTime = row["dtArriveTime"].ToString();
                }
                else
                {
                    model.dtWaitWorkTime = "";
                }


                if (row["dtCallTime"] != null && row["dtCallTime"].ToString() != "")
                {
                    model.dtCallWorkTime = row["dtCallTime"].ToString();
                }
                else
                {
                    model.dtCallWorkTime = "";
                }



                if (row["strGUID"] != null && row["strGUID"].ToString() != "")
                {
                    model.strPlanGUID = row["strGUID"].ToString();
                }
                else
                {
                    model.strPlanGUID = "";
                }

                if (row["strTrainNo"] != null)
                {
                    model.StrTrainNo = row["strTrainNo"].ToString();
                }
                else
                {
                    model.StrTrainNo = "";
                }


                if (row["strTrainNo"] != null)
                {
                    model.strCheCi = row["strTrainNo"].ToString();
                }
                else
                {
                    model.strCheCi = "";
                }


                if (row["strTrainmanGUID1"] != null)
                {
                    model.strTrainmanGUID1 = row["strTrainmanGUID1"].ToString();
                }
                else
                {
                    model.strTrainmanGUID1 = "";
                
                }
                if (row["strTrainmanGUID2"] != null)
                {
                    model.strTrainmanGUID2 = row["strTrainmanGUID2"].ToString();
                }
                else
                {
                    model.strTrainmanGUID2 = "";
                }
                if (row["strTrainmanGUID3"] != null)
                {
                    model.strTrainmanGUID3 = row["strTrainmanGUID3"].ToString();
                }
                else
                {
                    model.strTrainmanGUID3 = "";
                }
                if (row["strTrainmanGUID4"] != null)
                {
                    model.strTrainmanGUID4 = row["strTrainmanGUID4"].ToString();
                }
                else
                {
                    model.strTrainmanGUID4 = "";
                }
                if (row["nNeedRest"] != null && row["nNeedRest"].ToString() != "")
                {
                    model.NNeedRest = row["nNeedRest"].ToString();
                }
                else
                {
                    model.NNeedRest = "0";
                
                }
                if (row["ePlanState"] != null && row["ePlanState"].ToString() != "")
                {
                    model.NPlanState = row["ePlanState"].ToString();
                }
                else
                {
                    model.NPlanState = "0";
                }




                if (row["strTrainmanNumber1"] != null)
                {
                    model.strTrainmanNumber1 = row["strTrainmanNumber1"].ToString();
                }
                if (row["strTrainmanNumber2"] != null)
                {
                    model.strTrainmanNumber2 = row["strTrainmanNumber2"].ToString();
                }
                if (row["strTrainmanNumber3"] != null)
                {
                    model.strTrainmanNumber3 = row["strTrainmanNumber3"].ToString();
                }
                if (row["strTrainmanNumber4"] != null)
                {
                    model.strTrainmanNumber4 = row["strTrainmanNumber4"].ToString();
                }


                if (row["strTrainmanName1"] != null)
                {
                    model.strTrainmanName1 = row["strTrainmanName1"].ToString();
                }
                if (row["strTrainmanName2"] != null)
                {
                    model.strTrainmanName2 = row["strTrainmanName2"].ToString();
                }
                if (row["strTrainmanName3"] != null)
                {
                    model.strTrainmanName3 = row["strTrainmanName3"].ToString();
                }
                if (row["strTrainmanName4"] != null)
                {
                    model.strTrainmanName4 = row["strTrainmanName4"].ToString();
                }








            }
            return model;
        }



        //(1, 100000000, begintime, endtime, gonghao, name, checi, result, bbtimediff, etimediff)
        public DataTable QueryCheckAttendanceWorkOutList(int pageIndex, int pageCount, string begintime, string endtime, string gonghao, string name, string checi, string result, string bbtimediff, string etimediff)
        {
            StringBuilder strSqlWhere = new StringBuilder();

            if (!string.IsNullOrEmpty(begintime))
                strSqlWhere.Append(" and dtStartTime>='" + begintime + "'");

            if (!string.IsNullOrEmpty(endtime))
                strSqlWhere.Append(" and dtStartTime<='" + endtime + "'");

            if (!string.IsNullOrEmpty(gonghao))
            {
                strSqlWhere.Append(" and (strTrainmanNumber1 like '%" + gonghao + "%'");
                strSqlWhere.Append(" or strTrainmanNumber2 like '%" + gonghao + "%'");
                strSqlWhere.Append(" or strTrainmanNumber3 like '%" + gonghao + "%')");

            }

            if (name != "")
            {

                strSqlWhere.Append(" and (strTrainmanName1 like '%" + name + "%'");
                strSqlWhere.Append(" or strTrainmanName2 like '%" + name + "%'");
                strSqlWhere.Append(" or strTrainmanName3 like '%" + name + "%')");
            }

            if (!string.IsNullOrEmpty(checi))
                strSqlWhere.Append(" and strTrainNo like '%" + checi + "%'");

             if (!string.IsNullOrEmpty(result))
                 strSqlWhere.Append(" and cqcj1'" + result + "'");

             if (!string.IsNullOrEmpty(bbtimediff))
                 strSqlWhere.Append(" and BeginTimeDiff>='" + bbtimediff + "'");

              if (!string.IsNullOrEmpty(etimediff))
                  strSqlWhere.Append(" and EndTimeDiff>='" + etimediff + "'");



            string strSql = @"select top " + pageCount.ToString()
                + " * from VIEW_Plan_BeginEndWork where nID not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" nID from VIEW_Plan_BeginEndWork where 1=1"
                + strSqlWhere.ToString() + " order by nID desc)" + strSqlWhere.ToString() + " order by nID desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }



        //获取所有的图定车次
        public DataTable QueryCheckListForGetAllTuDingCheCi(string strJiaoLuGUID,string IsVisOk)
        {
            StringBuilder strSqlWhere = new StringBuilder();

            if (strJiaoLuGUID != "1")
                strSqlWhere.Append(" and strTrainJiaoluGUID='" + strJiaoLuGUID + "'");


            if (IsVisOk == "checked")
                strSqlWhere.Append(" and nNeedRest=1");



            string strSql = @"select * from VIEW_TrainNo_ForRoom where 1=1 " + strSqlWhere.ToString() + " order by strPlaceID";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }




        public DataTable QueryCheckListTuDingCheCi(int pageIndex, int pageCount, string strJiaoLuGUID, string IsVisOk)
        {
            StringBuilder strSqlWhere = new StringBuilder();
            if (strJiaoLuGUID != "1" && strJiaoLuGUID != "")
                strSqlWhere.Append(" and strTrainJiaoluGUID='" + strJiaoLuGUID + "'");


            if (IsVisOk == "checked")
                strSqlWhere.Append(" and nNeedRest=1");


            string strSql = @"select top " + pageCount.ToString()
                + " * from VIEW_TrainNo_ForRoom where nID not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" nID from VIEW_TrainNo_ForRoom where 1=1"
                + strSqlWhere.ToString() + " order by nID desc)" + strSqlWhere.ToString() + " order by nID desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }

        public int QueryCheckListTuDingCheCiCount(string strJiaoLuGUID, string IsVisOk)
        {

            StringBuilder strSqlWhere = new StringBuilder();

            if (strJiaoLuGUID != "1" && strJiaoLuGUID != "")
                strSqlWhere.Append(" and strTrainJiaoluGUID='" + strJiaoLuGUID + "'");


            if (IsVisOk == "checked")
                strSqlWhere.Append(" and nNeedRest=1");

            string strSql = "select count(*) from VIEW_TrainNo_ForRoom where 1=1  " + strSqlWhere + "";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }





        
        public DataTable QueryCheckList(int pageIndex, int pageCount, string strBeginChuQinTime, string stEndChuQinTime,string strJiaoLuGUID,string strTrainNo)
        {
            StringBuilder strSqlWhere = new StringBuilder();

            if (strBeginChuQinTime != "")
                strSqlWhere.Append(" and dtChuQinTime>'" + strBeginChuQinTime + "'");

            if (stEndChuQinTime != "")
                strSqlWhere.Append(" and dtChuQinTime<'" + stEndChuQinTime + "'");

            if (strJiaoLuGUID != "" && strJiaoLuGUID != "1")
                strSqlWhere.Append(" and strTrainJiaoLuGUID= '" + strJiaoLuGUID + "'");

            if (strTrainNo != "")
                strSqlWhere.Append(" and strTrainNo like '%" + strTrainNo + "%'");


            string strSql = @"select top " + pageCount.ToString()
                + " * from VIEW_Plan_Sign where nID not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" nID from VIEW_Plan_Sign where 1=1"
                + strSqlWhere.ToString() + " order by nID desc)" + strSqlWhere.ToString() + " order by nID desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }


        public int QueryCheckListCount(string strBeginChuQinTime, string stEndChuQinTime, string strJiaoLuGUID, string strTrainNo)
        {

            StringBuilder strSqlWhere = new StringBuilder();


            if (strBeginChuQinTime != "")
                strSqlWhere.Append(" and dtChuQinTime>'" + strBeginChuQinTime + "'");

            if (stEndChuQinTime != "")
                strSqlWhere.Append(" and dtChuQinTime<'" + stEndChuQinTime + "'");

            if (strJiaoLuGUID != "" && strJiaoLuGUID != "1")
                strSqlWhere.Append(" and strTrainJiaoLuGUID = '" + strJiaoLuGUID + "'");

            if (strTrainNo != "")
                strSqlWhere.Append(" and strTrainNo like '%" + strTrainNo + "%'");

            string strSql = "select count(*) from VIEW_Plan_Sign where 1=1  " + strSqlWhere + "";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }

        public DataTable GetJiaoLu()
        {
            string strSql = "select * from TAB_Base_TrainJiaolu";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        
        }

        //public DataTable GetJiaoLuForLED(string StrGUID)
        //{
        //    string strSql = "select * from TAB_Base_TrainJiaolu";
        //    return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

        //}




    }
}
