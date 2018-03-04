using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using TF.CommonUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.WorkSteps
{
    public class DBStepDef
    {
        #region 获取标准步骤列表
        public List<MDstepDef> GetStepDefList(string strWorkShopGUID, int nWorkTypeID)
        {
            string strWhere = "";

            if (!string.IsNullOrEmpty(strWorkShopGUID))
            {
                strWhere += " and strWorkShopGUID ='" + strWorkShopGUID + "'";
            }

            if (nWorkTypeID != 0)
            {
                strWhere += " and nWorkTypeID =" + nWorkTypeID + "";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_Plan_Beginwork_StepDef where  1=1  " + strWhere + "");
            return GetStepDef(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<MDstepDef> GetStepDef(DataTable dt)
        {
            List<MDstepDef> modelList = new List<MDstepDef>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MDstepDef model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = TostepDefList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public MDstepDef TostepDefList(DataRow dr)
        {
            MDstepDef model = new MDstepDef();
            if (dr != null)
            {
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
                model.strStepName = ObjectConvertClass.static_ext_string(dr["strStepName"]);
                model.strStepBrief = ObjectConvertClass.static_ext_string(dr["strStepBrief"]);
                model.nStepIndex = ObjectConvertClass.static_ext_int(dr["nStepIndex"]);
                model.nIsNecessary = ObjectConvertClass.static_ext_int(dr["nIsNecessary"]);
                model.strStepID = ObjectConvertClass.static_ext_string(dr["strStepID"]);
                model.nWorkTypeID = ObjectConvertClass.static_ext_int(dr["nWorkTypeID"]);

            }
            return model;
        }
        #endregion


        #region 设置是否为必要步骤
        public bool SetIsNecessary(int nID, int nIsNecessary)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_Plan_Beginwork_StepDef set ");
            strSql.Append(" nIsNecessary = @nIsNecessary");
            strSql.Append(" where nID = @nID ");
            SqlParameter[] parameters = {
          new SqlParameter("@nID", nID),
          new SqlParameter("@nIsNecessary", nIsNecessary)
                                  };
            if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0)
                return true;
            else
                return false;
        }
        #endregion
    }

    public class DBNoConfirm
    { 
     #region 获取未确认计划列表
        public List<MDNoConfirmPlan> GetNoConfirmList(string strWorkShopGUID, int nWorkTypeID)
        {
            string strWhere = "";
            if (!string.IsNullOrEmpty(strWorkShopGUID))
            {
                strWhere += " and n.strWorkShopGUID ='" + strWorkShopGUID + "'";
            }

            if (nWorkTypeID != 0)
            {
                strWhere += " and n.nWorkTypeID =" + nWorkTypeID + "";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select t.strTrainNo,t.strTrainNumber,t.strTrainPlanGUID,t.strTrainTypeName,t.dtStartTime, n.nWorkTypeID,n.strWorkShopGUID from 
  TAB_Plan_Beginwork_NoConfirm n , TAB_Plan_Train t  where  t.strTrainPlanGUID=n.strTrainPlanGUID   " + strWhere + "");
            return GetStepDef(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);


        }
        public List<MDNoConfirmPlan> GetStepDef(DataTable dt)
        {
            List<MDNoConfirmPlan> modelList = new List<MDNoConfirmPlan>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MDNoConfirmPlan model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = TostepDefList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public MDNoConfirmPlan TostepDefList(DataRow dr)
        {
            MDNoConfirmPlan model = new MDNoConfirmPlan();
            if (dr != null)
            {
                model.dtStartTime = ObjectConvertClass.static_ext_string(dr["dtStartTime"]);
                model.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
                model.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
                model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
                model.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
             
            }
            return model;
        }
        #endregion
    
    }


    public class DBSetRecord
    {


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddMain(TF.RunSafty.WorkSteps.Steps.Get_InFlowSetRecord model, string strGUID, SqlTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Beginwork_FlowSetRecord_Main(");
            strSql.Append("WorkShopGUID,ModeTypeID,FlowType,BeginTime,EndTime,ModeBrief,strGUID)");
            strSql.Append(" values (");
            strSql.Append("@WorkShopGUID,@ModeTypeID,@FlowType,@BeginTime,@EndTime,@ModeBrief,@strGUID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@WorkShopGUID", SqlDbType.VarChar,50),
					new SqlParameter("@ModeTypeID", SqlDbType.VarChar,50),
					new SqlParameter("@FlowType", SqlDbType.VarChar,50),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@ModeBrief", SqlDbType.VarChar,500),
					new SqlParameter("@strGUID", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.WorkShopGUID;
            parameters[1].Value = model.ModeTypeID;
            parameters[2].Value = model.FlowType;
            parameters[3].Value = model.BeginTime;
            parameters[4].Value = model.EndTime;
            parameters[5].Value = model.ModeBrief;
            parameters[6].Value = strGUID;
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 增加子记录
        /// </summary>
        public int AddChild(TF.RunSafty.WorkSteps.Steps.FlowSetRecord_Detail model, string Main_ID, SqlTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Beginwork_FlowSetRecord_Detail(");
            strSql.Append("strMainGUID,strStepID)");
            strSql.Append(" values (");
            strSql.Append("@strMainGUID,@strStepID)");
            SqlParameter[] parameters = {
					new SqlParameter("@strMainGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strStepID", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = Main_ID;
            parameters[1].Value = model.strStepID;
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters));
        }

    
    
    }



}
