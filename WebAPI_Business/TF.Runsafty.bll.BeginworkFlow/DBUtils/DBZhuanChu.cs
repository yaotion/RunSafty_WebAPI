using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TF.CommonUtility;
using ThinkFreely.DBUtility;
using System.Data;

namespace TF.RunSafty.BeginworkFlow
{
    public class DBEndWork
    {
        #region 提交运行记录转储记录
        /// <summary>
        /// 提交运行记录转储记录
        /// </summary>
        public void SubMitRunRecordInfo(RunRecordFileMain runRecordFileMain)
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
                string strGUID = AddRunRecrod(command, runRecordFileMain);
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

        /// <summary>
        /// 添加数据
        /// </summary>
        public string AddRunRecrod(SqlCommand command, RunRecordFileMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @ROWGUID_EndWork_RunRecrod_Main TABLE(ID uniqueidentifier) ");
            strSql.Append("insert into Tab_EndWork_RunRecrod_Main ");
            strSql.Append("(dtUploadTime,strPlanGUID,dtPlanChuQinTime,strTrainNo,strTrainNum,strTrainTypeName,strTrainmanNumber1,strTrainmanName1,strTrainmanNumber2,strTrainmanName2,strTrainmanNumber3,strTrainmanName3,strTrainmanNumber4,strTrainmanName4,strCardNumber,strSiteNumber,strSiteName)");
            strSql.Append("OUTPUT INSERTED.STRGUID INTO @ROWGUID_EndWork_RunRecrod_Main ");
            strSql.Append("values(@dtUploadTime,@strPlanGUID,@dtPlanChuQinTime,@strTrainNo,@strTrainNum,@strTrainTypeName,@strTrainmanNumber1,@strTrainmanName1,@strTrainmanNumber2,@strTrainmanName2,@strTrainmanNumber3,@strTrainmanName3,@strTrainmanNumber4,@strTrainmanName4,@strCardNumber,@strSiteNumber,@strSiteName)");
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
                                        };
            command.CommandText = strSql.ToString();
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);

            return ObjectConvertClass.static_ext_string(command.ExecuteScalar());
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        public int AddRecordFileDetail(SqlCommand command, RunRecordFileDetail model)
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
                    new SqlParameter("@strRecordGUID", model.strRecordGUID)                
                                        };

            command.CommandText = strSql.ToString();
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }
        #endregion

        public void setPlanState(int nStepIndex, string strWorkShopGUID, string strTrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" if not exists(select 1 from TAB_Plan_Beginwork_StepDef where nWorkTypeID=2 and nStepIndex>@nStepIndex and strWorkShopGUID=@strWorkShopGUID)");
            strSql.Append("    update TAB_Plan_Train set nPlanState=8 where strTrainPlanGUID=@strTrainPlanGUID");
            SqlParameter[] sqlParam ={
                                    new SqlParameter("nStepIndex",nStepIndex),
                                    new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                    new SqlParameter("strTrainPlanGUID",strTrainPlanGUID)
                                    };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, commandType: CommandType.Text, commandText: strSql.ToString(), commandParameters: sqlParam);
        }

    }
}
