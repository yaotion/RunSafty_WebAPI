using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TF.CommonUtility;
using ThinkFreely.DBUtility;
using System.Data;
using TF.RunSafty.DBUtils;

namespace TF.RunSafty.WorkSteps
{
    public class DBEndWork
    {
        /// <summary>
        /// 提交运行记录转储记录
        /// </summary>
        public void SubMitRunRecordInfo(MDRunRecordFileMain runRecordFileMain)
        {
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.ConnectionString = SqlHelper.ConnString;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                string strTrainPlanGUID = "";
                Boolean isExitPlan = true;
                //客户端如未传入计划GUID,则根据工号和当前时间检索数据库内符合的退勤计划
                if (string.IsNullOrEmpty(runRecordFileMain.strPlanGUID))
                {
                    strTrainPlanGUID = getTrainPlanGUID(runRecordFileMain.strTrainmanNumber1);
                    if (string.IsNullOrEmpty(strTrainPlanGUID))
                    {
                        runRecordFileMain.strPlanGUID = Guid.NewGuid().ToString();
                        isExitPlan = false;
                    }
                    else
                    {
                        runRecordFileMain.strPlanGUID = strTrainPlanGUID;
                        isExitPlan = true;
                    }
                }
                else
                {
                    strTrainPlanGUID = runRecordFileMain.strPlanGUID;
                    isExitPlan = true;
                }

                //判断表中是否已经存在主记录
                int MainCount = getMainCount(strTrainPlanGUID);
                string strGUID = "";
                if (MainCount < 1)
                    strGUID = AddRunRecrod(command, runRecordFileMain, isExitPlan, runRecordFileMain.RunRecordFileDetailList.Count);
                else
                    strGUID = Guid.NewGuid().ToString();


                //向子表中插入转储记录
                for (int i = 0; i < runRecordFileMain.RunRecordFileDetailList.Count; i++)
                {
                    runRecordFileMain.RunRecordFileDetailList[i].strRecordGUID = strGUID;
                    AddRecordFileDetail(command, runRecordFileMain.RunRecordFileDetailList[i]);
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                LogClass.log("Interface.SubMitRunRecordInfo:" + ex.Message);
                throw ex;
            }
        }

        #region 根据当前世间 和工号 获取当前的计划GUID
        public string getTrainPlanGUID(string strTrainNumber)
        {
            string strTrainmanGUID = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select strTrainmanGUID from  TAB_Org_Trainman where strTrainmanNumber='" + strTrainNumber + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                strTrainmanGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID"]);
            else
                return "";
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(@"select top 1 strTrainPlanGUID from TAB_Plan_Trainman where dtCreateTime<'" + DateTime.Now.ToString() + "' and      (strTrainmanGUID1= '" + strTrainmanGUID + "' or strTrainmanGUID2= '" + strTrainmanGUID + "' or strTrainmanGUID3= '" + strTrainmanGUID + "' or  strTrainmanGUID4= '" + strTrainmanGUID + "')");
            DataTable dt2 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()).Tables[0];
            if (dt2.Rows.Count <= 0)
                return "";
            else
                return ObjectConvertClass.static_ext_string(dt2.Rows[0]["strTrainPlanGUID"]);
        }

        #endregion



        #region 获取主表中的个数
        public int getMainCount(string strTrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM Tab_EndWork_RunRecrod_Main where strPlanGUID=@strPlanGUID");

            SqlParameter[] sqlParams ={
                                   new SqlParameter("@strPlanGUID",strTrainPlanGUID) 
                                   };
            return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams));
        }

        #endregion



        public static string ZhuanChuString
        {
            get
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuString"] == null)
                {
                    return "";
                }
                return System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuString"].ToString();
            }
        }

        public static string ZhuanChuIsEnable
        {
            get
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuIsEnable"] == null)
                {
                    return "";
                }
                return System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuIsEnable"].ToString();
            }
        }


        #region 从转储记录表中获取最后一条记录的时间 和文件的数量
        public DataTable getMaxTimeAndCount(string dtStartTime, string strTrainManNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select max(jg71) as DetailTime,count(*) as DetaieCount from cljg where jg02 between '" + dtStartTime + "' and '" + DateTime.Now + "' and (jg38 = '" + strTrainManNumber + "' or jg39 = '" + strTrainManNumber + "') ");
            return SqlHelper.ExecuteDataset(ZhuanChuString, CommandType.Text, strSql.ToString()).Tables[0];
        }
        #endregion


        public string AddRunRecrod(SqlCommand command, MDRunRecordFileMain model, Boolean isExitPlan, int a)
        {
            string sqlConnString = ZhuanChuString;
            string DetailTime = DateTime.Now.ToString();
            int DetaieCount = a;
            if (ZhuanChuIsEnable == "1")
            {
                try
                {
                    if (!string.IsNullOrEmpty(model.strTrainmanNumber1) && model.dtPlanChuQinTime > DateTime.Now.AddDays(-1000))
                    {
                        DataTable dt = getMaxTimeAndCount(model.dtPlanChuQinTime.ToString(), model.strTrainmanNumber1);
                        if (ObjectConvertClass.static_ext_int(dt.Rows[0]["DetaieCount"]) != 0)
                        {
                            DetaieCount = ObjectConvertClass.static_ext_int(dt.Rows[0]["DetaieCount"]);
                            DetailTime = ObjectConvertClass.static_ext_string(dt.Rows[0]["DetailTime"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @ROWGUID_EndWork_RunRecrod_Main TABLE(ID uniqueidentifier) ");
            strSql.Append("insert into Tab_EndWork_RunRecrod_Main ");
            strSql.Append("(dtUploadTime,strPlanGUID,dtPlanChuQinTime,strTrainNo,strTrainNum,strTrainTypeName,strTrainmanNumber1,strTrainmanName1,strTrainmanNumber2,strTrainmanName2,strTrainmanNumber3,strTrainmanName3,strTrainmanNumber4,strTrainmanName4,strCardNumber,strSiteNumber,strSiteName,dtLastUpDateTime,nDetailCount)");
            strSql.Append("OUTPUT INSERTED.STRGUID INTO @ROWGUID_EndWork_RunRecrod_Main ");
            strSql.Append("values(@dtUploadTime,@strPlanGUID,@dtPlanChuQinTime,@strTrainNo,@strTrainNum,@strTrainTypeName,@strTrainmanNumber1,@strTrainmanName1,@strTrainmanNumber2,@strTrainmanName2,@strTrainmanNumber3,@strTrainmanName3,@strTrainmanNumber4,@strTrainmanName4,@strCardNumber,@strSiteNumber,@strSiteName,@dtLastUpDateTime,@nDetailCount)");
            strSql.Append(";SELECT ID FROM @ROWGUID_EndWork_RunRecrod_Main");
            SqlParameter[] parameters = {
					new SqlParameter("@dtUploadTime", model.dtUploadTime),
					new SqlParameter("@strPlanGUID", model.strPlanGUID),
					new SqlParameter("@dtPlanChuQinTime", model.dtPlanChuQinTime),
					new SqlParameter("@strTrainNo", model.strTrainNo),
					new SqlParameter("@strTrainNum", model.strTrainNum),
					new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
					new SqlParameter("@strTrainmanNumber1", model.strTrainmanNumber1),
					new SqlParameter("@strTrainmanName1", model.strTrainmanName1),
					new SqlParameter("@strTrainmanNumber2", model.strTrainmanNumber2),
					new SqlParameter("@strTrainmanName2", model.strTrainmanName2),
					new SqlParameter("@strTrainmanNumber3", model.strTrainmanNumber3),
					new SqlParameter("@strTrainmanName3", model.strTrainmanName3),
					new SqlParameter("@strTrainmanNumber4", model.strTrainmanNumber4),
					new SqlParameter("@strTrainmanName4", model.strTrainmanName4),
					new SqlParameter("@strCardNumber", model.strCardNumber),
                    new SqlParameter("@strSiteNumber", model.strSiteNumber),
                    new SqlParameter("@strSiteName", model.strSiteName),
                    new SqlParameter("@nDetailCount", DetaieCount),
                    new SqlParameter("@dtLastUpDateTime", DetailTime)
                                        };
            command.CommandText = strSql.ToString();
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);

            return ObjectConvertClass.static_ext_string(command.ExecuteScalar());
            //return  command.ExecuteNonQuery();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        public int AddRecordFileDetail(SqlCommand command, MDRunRecordFileDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_EndWork_RunRecrod_Detail");
            strSql.Append("(strPlanGUID,strFileName,strTrainNum,strTrainmanNumber,strTrainNo,nFileSize,dtFileTime,strRecordGUID)");
            strSql.Append("values(@strPlanGUID,@strFileName,@strTrainNum,@strTrainmanNumber,@strTrainNo,@nFileSize,@dtFileTime,@strRecordGUID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@strPlanGUID", model.strPlanGUID),
					new SqlParameter("@strFileName", model.strFileName),
					new SqlParameter("@strTrainNum", model.strTrainNum),
					new SqlParameter("@strTrainmanNumber", model.strTrainmanNumber),
					new SqlParameter("@strTrainNo", model.strTrainNo),
					new SqlParameter("@nFileSize", model.nFileSize),
					new SqlParameter("@dtFileTime", model.dtFileTime),
                    new SqlParameter("@strRecordGUID", model.strRecordGUID)                    };
            command.CommandText = strSql.ToString();
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }


        private bool HasEndWorkPlan(string strSiteGUID, string strTrainmanGUID)
        {
            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = '{0}') and 
nPlanState >= {1} AND (strTrainmanGUID1 = '{2}' or strTrainmanGUID2 = '{2}'
or strTrainmanGUID3 = '{2}' or strTrainmanGUID4 = '{2}') order by dtStartTime desc", strSiteGUID, 7, strTrainmanGUID);
            TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork bllEndWork = new TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork();
            return bllEndWork.GetModelList(strWhere).Count > 0;
        }


        public bool Endwork(string strTrainPlanGUID, DateTime arriveTime, string Remark, int VerifyID, string siteGuid)
        {
            try
            {
                //根据planid获取交路，然后根据客户端编号和交路获取退勤端的第一个出勤点
                string strSql = "select strTrainJiaoluGUID from tab_plan_train where strTrainPlanGUID=@strTrainPlanGUID    ";
                SqlParameter[] sqlParamsFind = new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID", strTrainPlanGUID),
                };
                object oPlaceId = null;
                SqlParameter[] sqlParameters;
                object oTrainjiaolu = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsFind);
                if (oTrainjiaolu != null)
                {
                    strSql = "select top 1 strPlaceID from TAB_Base_Site_DutyPlace where strSiteGUID=@strSiteGUID and strTrainJiaoluGUID=strTrainJiaoluGUID";
                    sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("strSiteGUID", siteGuid),
                        new SqlParameter("strTrainJiaoluGUID", oTrainjiaolu.ToString()),
                    };
                    oPlaceId = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
                }
                if (oPlaceId == null)
                {
                    throw new Exception("找不到该退勤端所管理的出勤点，请在网站中配置。");
                }
                string sqlText = "select strGroupGUID from TAB_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
                object obj = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind));
                string strGroupGUID = "";
                if (obj != null)
                {
                    strGroupGUID = Convert.ToString(obj);
                }

                sqlText = @"select strGroupGUID,strTrainmanJiaoluGUID from VIEW_Nameplate_Group_TrainmanJiaolu where strGroupGUID = '" + strGroupGUID + "'";
                DataTable dtJiaolu1 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind).Tables[0];
                DataTable dtJiaolu;
                if (dtJiaolu1.Rows.Count == 0)
                {
                    throw new Exception("该机组没有处于任何人员交路中");
                }
                else
                {
                    string strTrainmanJiaoluGUID = dtJiaolu1.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                    sqlText = @"select * from TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID='" + strTrainmanJiaoluGUID + "'";
                    dtJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind).Tables[0];
                }
                string trainmanJiaoluGUID = dtJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                int nTrainmanJiaoluType, nRunType;
                int.TryParse(dtJiaolu.Rows[0]["nJiaoluType"].ToString(), out nTrainmanJiaoluType);
                int.TryParse(dtJiaolu.Rows[0]["nTrainmanRunType"].ToString(), out nRunType);



                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
                    new SqlParameter("nVerifyID", VerifyID),
                    new SqlParameter("strStationGUID", ""),
                    new SqlParameter("strRemark", Remark),
                    new SqlParameter("strGroupGUID", strGroupGUID),
                    new SqlParameter("strPlaceID", oPlaceId.ToString()),
                    new SqlParameter("nPlanState", 8),
                    new SqlParameter("NullTrainPlanGUID", ""),

                    new SqlParameter("strGUID", Guid.NewGuid().ToString()),
                    new SqlParameter("strTrainmanJiaoluGUID", trainmanJiaoluGUID),
                    new SqlParameter("strDutyGUID", ""),

                    new SqlParameter("nWorkTypeID", 3),
                    new SqlParameter("dtLastArriveTime", arriveTime),
                    new SqlParameter("dtLastEndWorkTime", DateTime.Now),
                    new SqlParameter("nTrainmanState", 2),

                };


                #region '修改计划状态为已退勤'

                sqlText = @"update TAB_Plan_Train set nPlanState=@nPlanState where strTrainPlanGUID=@strTrainPlanGUID";

                if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams) > 0)
                {
                    strSql =
                        "update TAB_Nameplate_Group set strTrainPlanGUID = @NullTrainPlanGUID where strGroupGUID = @strGroupGUID";
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                    //翻牌
                    //jltTogether
                    if (nTrainmanJiaoluType == 4)
                    {
                        string stTrainGUID = "";
                        sqlText = "select strTrainGUID from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strGroupGUID=@strGroupGUID";
                        
                        object objTrainGUID = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
                        if ((objTrainGUID != null) && (!DBNull.Value.Equals(objTrainGUID)))
                        {
                            stTrainGUID = objTrainGUID.ToString();
                        }

                        ///将所有非自己的机组按照从1开始排序
                        sqlText = "select strGroupGUID from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strTrainGUID=@strTrainGUID and strGroupGUID <> @strGroupGUID order by nOrder";
                        SqlParameter[] sqlParamsSubs = new SqlParameter[]{
                            new SqlParameter("strTrainGUID",stTrainGUID),
                            new SqlParameter("strGroupGUID",strGroupGUID)
                        };
                        DataTable dtReOrder = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsSubs).Tables[0];
                        for (int i = 0; i < dtReOrder.Rows.Count; i++)
                        {
                            sqlText = "update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set nOrder = @nOrder where strGroupGUID=@strGroupGUID";
                            SqlParameter[] sqlReOrder = new SqlParameter[] { 
                            new SqlParameter("nOrder",i+ 1),
                            new SqlParameter("strGroupGUID",dtReOrder.Rows[i]["strGroupGUID"].ToString())
                        };
                            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlReOrder);
                        }
                        //将自己设置为最大的
                        sqlText = "update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set nOrder = @nOrder where strGroupGUID=@strGroupGUID";
                        SqlParameter[] sqlReOrder2 = new SqlParameter[] { 
                            new SqlParameter("nOrder",dtReOrder.Rows.Count + 1),
                            new SqlParameter("strGroupGUID",strGroupGUID)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlReOrder2);

                        sqlText =
                            @"update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";

                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
                    }

                    //jltOrder,轮乘交路需要修改出勤点
                    if (nTrainmanJiaoluType == 3)
                    {
                        sqlText = "update TAB_Nameplate_Group set strStationGUID = @strStationGUID,strPlaceID=@strPlaceID where strGroupGUID = @strGroupGUID";
                        int count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);

                        sqlText =
                            "update TAB_Nameplate_TrainmanJiaolu_Order set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
                    }

                    TF.CommonUtility.LogClass.log("修改计划状态为出勤");
                }

                #endregion

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return true;
        }


        public bool getGetLastArrvieTime(string TrainPlanGUID, ref string ArriveTime)
        {

            string strSql = "Select dtLastArriveTime from TAB_Plan_Train where  strTrainPlanGUID = @strTrainPlanGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strTrainPlanGUID",TrainPlanGUID)
                };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

            if (dt.Rows.Count == 0)
            {
                ArriveTime = "";
                return false;
            }
            else
            {
                ArriveTime = dt.Rows[0]["dtLastArriveTime"].ToString();
                return true;
            }

        }
    }
}

