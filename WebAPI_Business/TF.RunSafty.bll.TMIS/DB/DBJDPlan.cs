using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.CommonUtility;
using System.Data;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.TMIS
{
    public class DBJDPlan
    {

        #region 获取指定机务段的阶段计划
        public List<MDJDPlan> GetJDPlan(TF.RunSafty.TMIS.LCTmis.Get_InJDPlan input)
        {
            StringBuilder strSql = new StringBuilder();
            string sqlCondition = " 1=1 ";
            if (input.BTime > DateTime.Now.AddYears(-100))
            {
                sqlCondition += "and time_deptart>='" + input.BTime + "'";
            }

            if (input.ETime > DateTime.Now.AddYears(-100))
            {
                sqlCondition += " and time_deptart<'" + input.ETime + "' ";
            }
		    if (!string.IsNullOrEmpty(input.JlID))
		    {
			    sqlCondition += " and JlID ='" + input.JlID + "' ";
		    }

            if (!string.IsNullOrEmpty(input.SiteNumber))
            {

                string strsql = @"with t as 
            (select j.SectionXB,j.SectionID from Tab_Tmis_ClientAndJDSection c left join TAB_Base_JDSection 
                j on c.strSectionID=j.nID  where strClientID='" + input.SiteNumber + "') select * from  TAB_TMIS_Train  c right join t on t.SectionID=c.section_id and t.SectionXB=c.sxx where " + sqlCondition + " order by time_deptart";
                DataTable dt1 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strsql.ToString()).Tables[0];
                return GetJDPlan(dt1);
            }



            strSql.Append("select * from  TAB_TMIS_Train ");
            strSql.Append("inner join TAB_Base_BYRegion ON TAB_Base_BYRegion.RegionID = TAB_TMIS_Train.section_id where ");
						strSql.Append(sqlCondition);
						strSql.Append(" order by time_deptart");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            return GetJDPlan(dt);
        }

        public List<MDJDPlan> GetJDPlan(DataTable dt)
        {
            List<MDJDPlan> modelList = new List<MDJDPlan>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MDJDPlan model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetJDPlan(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public MDJDPlan GetJDPlan(DataRow dr)
        {
            MDJDPlan model = new MDJDPlan();
            if (dr != null)
            {
                model.Day = ObjectConvertClass.static_ext_string(dr["day"]);
                model.Shift = ObjectConvertClass.static_ext_int(dr["shift"]);
                model.Typ = ObjectConvertClass.static_ext_int(dr["typ"]);
                model.Section_id = ObjectConvertClass.static_ext_string(dr["section_id"]);
                model.Trainkind = ObjectConvertClass.static_ext_int(dr["trainkind"]);
                model.Train_id = ObjectConvertClass.static_ext_string(dr["train_id"]);
                model.Train_code = ObjectConvertClass.static_ext_string(dr["train_code"]);
                model.Time_deptart = ObjectConvertClass.static_ext_Date(dr["time_deptart"]);
                model.Time_arrived = ObjectConvertClass.static_ext_Date(dr["time_arrived"]);
                model.Station_deptart = ObjectConvertClass.static_ext_string(dr["station_deptart"]);
                model.Station_arrived = ObjectConvertClass.static_ext_string(dr["station_arrived"]);
                model.Loco_num = ObjectConvertClass.static_ext_int(dr["loco_num"]);
                model.Weight = (float)ObjectConvertClass.static_ext_double(dr["weight"]);
                model.Car_count = ObjectConvertClass.static_ext_int(dr["car_count"]);
                model.C_length = (float)ObjectConvertClass.static_ext_double(dr["c_length"]);
                model.PlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
                model.Section_name = ObjectConvertClass.static_ext_string(dr["strSectionName"]);
                model.Station_deptart_id = ObjectConvertClass.static_ext_string(dr["strDeptartStationID"]);
                model.Station_arrived_id = ObjectConvertClass.static_ext_string(dr["strArrivedStationID"]);
                model.IsUpdate = ObjectConvertClass.static_ext_int(dr["nIsUpdate"]);
            }
            return model;
        }
        #endregion

        #region 生成阶段计划
        public bool ProduceJDPlan(string SectionID, string SiteName, string SiteNumber, string TrainID, string JiaoluGUID, string SiteGUID, string UserGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  ");
            strSql.Append(" FROM TAB_TMIS_Train where train_id='" + TrainID + "' ");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                throw new Exception("未在TAB_TMIS_Train 表中找到train_id为" + TrainID + "的数据");
            }
            else
            {
                MDTrain model = new MDTrain();
                model.strTrainNo = ObjectConvertClass.static_ext_string(dt.Rows[0]["train_code"]);

                //通过出勤规则，计划开车时间计算出计划出勤时间
                int minutes = GetPlanTimes(1, "2200004");
                DateTime dtStartTime = ObjectConvertClass.static_ext_Date(dt.Rows[0]["time_deptart"]).AddMinutes(-minutes);
                model.dtStartTime = dtStartTime;

                model.strStartStation = getStationGUIDByName(ObjectConvertClass.static_ext_string(dt.Rows[0]["station_deptart"]));
                model.strEndStation = getStationGUIDByName(ObjectConvertClass.static_ext_string(dt.Rows[0]["station_arrived"]));
                model.strTrainPlanGUID = Guid.NewGuid().ToString();
                model.strTrainTypeName = "";
                model.strTrainNumber = "";
                model.dtRealStartTime = Convert.ToDateTime("2000-01-01 00:00:00");
                model.strTrainJiaoluGUID = JiaoluGUID;
                model.nTrainmanTypeID = 1;
                model.nPlanType = 1;
                model.nDragType = 2;
                TF.RunSafty.Utility.TGlobalDM d = new Utility.TGlobalDM();
                string remark = "";
                model.nKehuoID = Convert.ToInt32(d.getKeHuoByTrainNo(model.strTrainNo, ref remark));
                TF.CommonUtility.LogClass.log("获取客货类型----车次：" + model.strTrainNo + "客货类型：" + model.nKehuoID + "备注：" + remark);

                model.nRemarkType = 1;
                model.strRemark = "";
                model.nPlanState = 1;
                model.dtCreateTime = DateTime.Now;
                model.strCreateSiteGUID = SiteGUID;
                model.strCreateUserGUID = UserGUID;
                model.dtFirstStartTime = Convert.ToDateTime("2000-01-01 00:00:00");
                model.dtChuQinTime = ObjectConvertClass.static_ext_Date(dt.Rows[0]["time_deptart"]);
                model.nNeedRest = 0;
                model.dtArriveTime = Convert.ToDateTime("2000-01-01 00:00:00");
                model.dtCallTime = Convert.ToDateTime("2000-01-01 00:00:00");
                model.strBak1 = "";
                model.strMainPlanGUID = "";
                model.strPlaceID = getPlaceIDByTJLGUID(JiaoluGUID);

                if (this.AddPlan(model) >= 1)
                {
                    string sql = "update TAB_TMIS_Train set strTrainPlanGUID = @strTrainPlanGUID where train_id='" + TrainID + "'";
                    SqlParameter[] param = {
                                        new SqlParameter("strTrainPlanGUID",model.strTrainPlanGUID)
                                   };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
                    return true;
                }

            }
            return false;

        }

        //通过站接库接获取出勤规则
        public int GetPlanTimes(int nRemarkType, string strPlaceID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nMinute from Tab_Base_ChuQinTimeRule where nRemarkType = " + nRemarkType + " and strPlaceID = '" + strPlaceID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["nMinute"].ToString());
            }
            else
            {
                return 90;
            }
        }


        #region 通过车站名称获取车站GUID
        public string getStationGUIDByName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strStationGUID  ");
            strSql.Append(" FROM TAB_Base_Station where strStationName='" + name + "' ");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["strStationGUID"].ToString();
            return "";
        }
        #endregion

        #region 通过行车区段获取出勤点
        public string getPlaceIDByTJLGUID(string strTrainJiaoluGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strPlaceID  ");
            strSql.Append(" FROM TAB_Base_Site_DutyPlace where strTrainJiaoluGUID='" + strTrainJiaoluGUID + "' ");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["strPlaceID"].ToString();
            return "";
        }
        #endregion

        #region 生成运安中的计划
        public int AddPlan(MDTrain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Train(");
            strSql.Append("strTrainPlanGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,nPlanState,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,dtFirstStartTime,dtChuQinTime,nNeedRest,dtArriveTime,dtCallTime,strBak1,strMainPlanGUID,strPlaceID)");
            strSql.Append(" values (");
            strSql.Append("@strTrainPlanGUID,@strTrainTypeName,@strTrainNumber,@strTrainNo,@dtStartTime,@dtRealStartTime,@strTrainJiaoluGUID,@strStartStation,@strEndStation,@nTrainmanTypeID,@nPlanType,@nDragType,@nKehuoID,@nRemarkType,@strRemark,@nPlanState,@dtCreateTime,@strCreateSiteGUID,@strCreateUserGUID,@dtFirstStartTime,@dtChuQinTime,@nNeedRest,@dtArriveTime,@dtCallTime,@strBak1,@strMainPlanGUID,@strPlaceID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50),
                    new SqlParameter("@strTrainTypeName", SqlDbType.VarChar,50),
                    new SqlParameter("@strTrainNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@strTrainNo", SqlDbType.VarChar,50),
                    new SqlParameter("@dtStartTime", SqlDbType.DateTime),
                    new SqlParameter("@dtRealStartTime", SqlDbType.DateTime),
                    new SqlParameter("@strTrainJiaoluGUID", SqlDbType.VarChar,50),
                    new SqlParameter("@strStartStation", SqlDbType.VarChar,50),
                    new SqlParameter("@strEndStation", SqlDbType.VarChar,50),
                    new SqlParameter("@nTrainmanTypeID", SqlDbType.Int,4),
                    new SqlParameter("@nPlanType", SqlDbType.Int,4),
                    new SqlParameter("@nDragType", SqlDbType.Int,4),
                    new SqlParameter("@nKehuoID", SqlDbType.Int,4),
                    new SqlParameter("@nRemarkType", SqlDbType.Int,4),
                    new SqlParameter("@strRemark", SqlDbType.VarChar,50),
                    new SqlParameter("@nPlanState", SqlDbType.Int,4),
                    new SqlParameter("@dtCreateTime", SqlDbType.DateTime),
                    new SqlParameter("@strCreateSiteGUID", SqlDbType.VarChar,50),
                    new SqlParameter("@strCreateUserGUID", SqlDbType.VarChar,50),
                    new SqlParameter("@dtFirstStartTime", SqlDbType.DateTime),
                    new SqlParameter("@dtChuQinTime", SqlDbType.DateTime),
                    new SqlParameter("@nNeedRest", SqlDbType.Int,4),
                    new SqlParameter("@dtArriveTime", SqlDbType.DateTime),
                    new SqlParameter("@dtCallTime", SqlDbType.DateTime),
                    new SqlParameter("@strBak1", SqlDbType.VarChar,50),
                    new SqlParameter("@strMainPlanGUID", SqlDbType.VarChar,50),
                    new SqlParameter("@strPlaceID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.strTrainPlanGUID;
            parameters[1].Value = model.strTrainTypeName;
            parameters[2].Value = model.strTrainNumber;
            parameters[3].Value = model.strTrainNo;
            parameters[4].Value = model.dtStartTime;
            parameters[5].Value = model.dtRealStartTime;
            parameters[6].Value = model.strTrainJiaoluGUID;
            parameters[7].Value = model.strStartStation;
            parameters[8].Value = model.strEndStation;
            parameters[9].Value = model.nTrainmanTypeID;
            parameters[10].Value = model.nPlanType;
            parameters[11].Value = model.nDragType;
            parameters[12].Value = model.nKehuoID;
            parameters[13].Value = model.nRemarkType;
            parameters[14].Value = model.strRemark;
            parameters[15].Value = model.nPlanState;
            parameters[16].Value = model.dtCreateTime;
            parameters[17].Value = model.strCreateSiteGUID;
            parameters[18].Value = model.strCreateUserGUID;
            parameters[19].Value = model.dtFirstStartTime;
            parameters[20].Value = model.dtChuQinTime;
            parameters[21].Value = model.nNeedRest;
            parameters[22].Value = model.dtArriveTime;
            parameters[23].Value = model.dtCallTime;
            parameters[24].Value = model.strBak1;
            parameters[25].Value = model.strMainPlanGUID;
            parameters[26].Value = model.strPlaceID;
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion
        
        #endregion

        #region DeleteJDPlan（删除阶段计划）
        public bool DeleteJDPlan(string strTrainPlanGUID)
        {
            //string strSql = "delete from TAB_TMIS_Train where strTrainPlanGUID = @strTrainPlanGUID";
            //SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("strTrainPlanGUID", strTrainPlanGUID) };
            //SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
            //strSql = "delete from TAB_Plan_Train where strTrainPlanGUID = @strTrainPlanGUID";
            //SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
            throw new Exception("此接口暂不支持调用！");
        }
        #endregion

        #region 获取区段列表
        public List<MDQuDuan> GetQDList()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from  TAB_Base_BYRegion ");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            return GetQDList(dt);
        }

        public List<MDQuDuan> GetQDList(DataTable dt)
        {
            List<MDQuDuan> modelList = new List<MDQuDuan>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MDQuDuan model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetQDList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public MDQuDuan GetQDList(DataRow dr)
        {
            MDQuDuan model = new MDQuDuan();
            if (dr != null)
            {
                model.Section_name = ObjectConvertClass.static_ext_string(dr["RegionName"]);
                model.Section_id = ObjectConvertClass.static_ext_string(dr["RegionID"]);
                    
            }
            return model;
        }
        #endregion


        #region ConfirmPlan（确认计划）
        public bool ConfirmPlan(string TrainID)
        {
            string strSql = "update TAB_TMIS_Train set nIsUpdate = 1 where train_id='" + TrainID + "'";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql);
            return true;
        }
        #endregion
    }
}
