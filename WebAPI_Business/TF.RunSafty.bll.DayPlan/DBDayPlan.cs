using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.DayPlan
{

    public class DBsystem
    {
        #region IsAdmin 1.6.1获取系统管理员密码
        public Boolean IsAdmin(string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_System_Config where strSection = 'SysConfig' and strIdent= 'DayPlanPassWord'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["strValue"].ToString() == password;
            }
            else
            {
                return password == "";
            }
        }
        #endregion

        #region GetTrainTypes方法（1.6.19获取计划车型定义信息）
        public List<ShortTrain> GetTrainTypes()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select * from Tab_Base_DayPlan_ShortTrain order by nid");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                return new List<ShortTrain>();
            }
            else
            {
                return GetTrainTypes_DTToList(dt);

            }
        }

        public List<ShortTrain> GetTrainTypes_DTToList(DataTable dt)
        {
            List<ShortTrain> modelList = new List<ShortTrain>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ShortTrain model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetTrainTypes_DRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public ShortTrain GetTrainTypes_DRToModel(DataRow dr)
        {
            ShortTrain model = new ShortTrain();
            if (dr != null)
            {
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.strLongName = ObjectConvertClass.static_ext_string(dr["strLongName"]);
                model.strShortName = ObjectConvertClass.static_ext_string(dr["strShortName"]);

            }
            return model;
        }
        #endregion


    }
    public class DBPlace
    {
        #region QueryPlace方法（获取所有的交路信息）
        public List<DayPlanPlace> QueryPlace()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select * from Tab_Base_DayPlan order by nid");
            return QueryPlace_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<DayPlanPlace> QueryPlace_DTToList(DataTable dt)
        {
            List<DayPlanPlace> modelList = new List<DayPlanPlace>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DayPlanPlace model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = QueryPlace_DRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public DayPlanPlace QueryPlace_DRToModel(DataRow dr)
        {
            DayPlanPlace model = new DayPlanPlace();
            if (dr != null)
            {
                model.ID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.Name = ObjectConvertClass.static_ext_string(dr["strName"]);
            }
            return model;
        }
        #endregion

    }
    public class DBGroup
    {
        #region QueryPlace方法（获取所有的交路信息）
        public List<DayPlanItemGroup> QueryGroups(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Tab_Base_DayPlan_Group where nDayPlanID = " + ID + " order by  nExcelSide,nExcelPos,nid");
            return QueryGroups_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<DayPlanItemGroup> QueryGroups_DTToList(DataTable dt)
        {
            List<DayPlanItemGroup> modelList = new List<DayPlanItemGroup>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DayPlanItemGroup model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = QueryGroups_DRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public DayPlanItemGroup QueryGroups_DRToModel(DataRow dr)
        {
            DayPlanItemGroup model = new DayPlanItemGroup();
            if (dr != null)
            {
                model.ID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.Name = ObjectConvertClass.static_ext_string(dr["strName"]);
                model.ExcelSide = ObjectConvertClass.static_ext_int(dr["nExcelSide"]);
                model.ExcelPos = ObjectConvertClass.static_ext_int(dr["nExcelPos"]);
                model.DayPlanID = ObjectConvertClass.static_ext_string(dr["nDayPlanID"]);
                model.IsDaWen = ObjectConvertClass.static_ext_int(dr["bIsDawen"]);
            }
            return model;
        }
        #endregion


        #region GetGroup方法（获取所有的交路信息）
        public DayPlanItemGroup GetGroup(int DayPlanID, int GroupID, ref bool bIsNull)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from Tab_Base_DayPlan_Group where  nDayPlanID = " + DayPlanID + " and  nID = " + GroupID + " ");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                bIsNull = false;
                return null;
            }
            else
            {
                bIsNull = true;
                return GetGroup_DRToModel(dt.Rows[0]);                

            }
        }

        public List<DayPlanItemGroup> GetGroup_DTToList(DataTable dt)
        {
            List<DayPlanItemGroup> modelList = new List<DayPlanItemGroup>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DayPlanItemGroup model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetGroup_DRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public DayPlanItemGroup GetGroup_DRToModel(DataRow dr)
        {
            DayPlanItemGroup model = new DayPlanItemGroup();
            if (dr != null)
            {
                model.ID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.Name = ObjectConvertClass.static_ext_string(dr["strName"]);
                model.ExcelSide = ObjectConvertClass.static_ext_int(dr["nExcelSide"]);
                model.ExcelPos = ObjectConvertClass.static_ext_int(dr["nExcelPos"]);
                model.DayPlanID = ObjectConvertClass.static_ext_string(dr["nDayPlanID"]);
                model.IsDaWen = ObjectConvertClass.static_ext_int(dr["bIsDawen"]);
            }
            return model;
        }
        #endregion




        #region 添加机组
        public int Add(DayPlanItemGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_Base_DayPlan_Group");
            strSql.Append("(strName,nExcelSide,nExcelPos,nDayPlanID,bIsDawen)");
            strSql.Append("values(@strName,@nExcelSide,@nExcelPos,@nDayPlanID,@bIsDawen)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strName", model.Name),
          new SqlParameter("@nExcelSide", model.ExcelSide),
          new SqlParameter("@nExcelPos", model.ExcelPos),
          new SqlParameter("@nDayPlanID", model.DayPlanID),
          new SqlParameter("@bIsDawen", model.IsDaWen)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion


        #region 修改机组
        public int Update(DayPlanItemGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Tab_Base_DayPlan_Group set ");
            strSql.Append(" strName = @strName, ");
            strSql.Append(" nExcelSide = @nExcelSide, ");
            strSql.Append(" nExcelPos = @nExcelPos, ");
            strSql.Append(" nDayPlanID = @nDayPlanID, ");
            strSql.Append(" bIsDawen = @bIsDawen ");
            strSql.Append(" where nID = @nID ");
            SqlParameter[] parameters = {
          new SqlParameter("@strName", model.Name),
           new SqlParameter("@nID", model.ID),
          new SqlParameter("@nExcelSide", model.ExcelSide),
          new SqlParameter("@nExcelPos", model.ExcelPos),
          new SqlParameter("@nDayPlanID", model.DayPlanID),
          new SqlParameter("@bIsDawen", model.IsDaWen)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion


        #region 删除机组
        public int Delete(int DayPlanID,int GroupID)
        {
            string strSql = "delete from Tab_Base_DayPlan_Group where nid = " + GroupID + " and nDayPlanID = " + DayPlanID + "";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }
        #endregion








    }
    public class DBModules
    {
        #region QueryModules方法（1.6.5获取指定机车组内的计划信息）
        public List<PlanModule> QueryModules(int GroupID, int DayPlanType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Tab_Base_DayPlan_Item where nGroupID = " + GroupID + " and  nDayPlanType = " + DayPlanType + " order by nid ");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            return QueryModules_DTToList(dt);
        }

        public List<PlanModule> QueryModules_DTToList(DataTable dt)
        {
            List<PlanModule> modelList = new List<PlanModule>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PlanModule model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = QueryModules_DRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public PlanModule QueryModules_DRToModel(DataRow dr)
        {
            PlanModule model = new PlanModule();
            if (dr != null)
            {
                model.ID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.TrainNo1 = ObjectConvertClass.static_ext_string(dr["strTrainNo1"]);
                model.TrainInfo = ObjectConvertClass.static_ext_string(dr["strTrainInfo"]);
                model.TrainNo2 = ObjectConvertClass.static_ext_string(dr["strTrainNo2"]);
                model.TrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
                model.Remark = ObjectConvertClass.static_ext_string(dr["strRemark"]);
                model.IsTomorrow = ObjectConvertClass.static_ext_int(dr["bIsTomorrow"]);
                model.GroupID = ObjectConvertClass.static_ext_int(dr["nGroupID"]);
                model.DayPlanType = ObjectConvertClass.static_ext_int(dr["nDayPlanType"]);
                model.DaWenCheXing = ObjectConvertClass.static_ext_string(dr["strDaWenCheXing"]);
                model.DaWenCheHao1 = ObjectConvertClass.static_ext_string(dr["strDaWenCheHao1"]);
                model.DaWenCheHao2 = ObjectConvertClass.static_ext_string(dr["strDaWenCheHao2"]);
                model.DaWenCheHao3 = ObjectConvertClass.static_ext_string(dr["strDaWenCheHao3"]);
            }
            return model;
        }
        #endregion

        #region AddModule（1.6.6添加机车计划模版信息）
        public int AddModule(PlanModule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_Base_DayPlan_Item");
            strSql.Append("(strTrainNo1,strTrainInfo,strTrainNo2,strTrainNo,strRemark,bIsTomorrow,nGroupID,nDayPlanType,strDaWenCheXing,strDaWenCheHao1,strDaWenCheHao2,strDaWenCheHao3)");
            strSql.Append("values(@strTrainNo1,@strTrainInfo,@strTrainNo2,@strTrainNo,@strRemark,@bIsTomorrow,@nGroupID,@nDayPlanType,@strDaWenCheXing,@strDaWenCheHao1,@strDaWenCheHao2,@strDaWenCheHao3)");
            SqlParameter[] parameters = {
          new SqlParameter("@strTrainNo1", model.TrainNo1),
          new SqlParameter("@strTrainInfo", model.TrainInfo),
          new SqlParameter("@strTrainNo2", model.TrainNo2),
          new SqlParameter("@strTrainNo", model.TrainNo),
          new SqlParameter("@strRemark", model.Remark),
          new SqlParameter("@bIsTomorrow", model.IsTomorrow),
          new SqlParameter("@nGroupID", model.GroupID),
          new SqlParameter("@nDayPlanType", model.DayPlanType),
          new SqlParameter("@strDaWenCheXing", model.DaWenCheXing),
          new SqlParameter("@strDaWenCheHao1", model.DaWenCheHao1),
          new SqlParameter("@strDaWenCheHao2", model.DaWenCheHao2),
          new SqlParameter("@strDaWenCheHao3", model.DaWenCheHao3)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        #region UpdateModule（1.6.7修改机车计划模版信息）
        public int UpdateModule(PlanModule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Tab_Base_DayPlan_Item set ");
            strSql.Append(" strTrainNo1 = @strTrainNo1, ");
            strSql.Append(" strTrainInfo = @strTrainInfo, ");
            strSql.Append(" strTrainNo2 = @strTrainNo2, ");
            strSql.Append(" strTrainNo = @strTrainNo, ");
            strSql.Append(" strRemark = @strRemark, ");
            strSql.Append(" bIsTomorrow = @bIsTomorrow, ");
            strSql.Append(" nGroupID = @nGroupID, ");
            strSql.Append(" nDayPlanType = @nDayPlanType, ");
            strSql.Append(" strDaWenCheXing = @strDaWenCheXing, ");
            strSql.Append(" strDaWenCheHao1 = @strDaWenCheHao1, ");
            strSql.Append(" strDaWenCheHao2 = @strDaWenCheHao2, ");
            strSql.Append(" strDaWenCheHao3 = @strDaWenCheHao3 ");
            strSql.Append(" where nID = @nID ");
            SqlParameter[] parameters = {
          new SqlParameter("@nID", model.ID),
          new SqlParameter("@strTrainNo1", model.TrainNo1),
          new SqlParameter("@strTrainInfo", model.TrainInfo),
          new SqlParameter("@strTrainNo2", model.TrainNo2),
          new SqlParameter("@strTrainNo", model.TrainNo),
          new SqlParameter("@strRemark", model.Remark),
          new SqlParameter("@bIsTomorrow", model.IsTomorrow),
          new SqlParameter("@nGroupID", model.GroupID),
          new SqlParameter("@nDayPlanType", model.DayPlanType),
          new SqlParameter("@strDaWenCheXing", model.DaWenCheXing),
          new SqlParameter("@strDaWenCheHao1", model.DaWenCheHao1),
          new SqlParameter("@strDaWenCheHao2", model.DaWenCheHao2),
          new SqlParameter("@strDaWenCheHao3", model.DaWenCheHao3)
                                        };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        #region DeleteModule 1.6.8 删除机车计划模版信息
        public int DeleteModule(int nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tab_Base_DayPlan_Item ");
            strSql.Append(" where nID = @nID ");
            SqlParameter[] parameters = {
          new SqlParameter("nID",nID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        #region GetModule方法（1.6.9获取机车信息模版信息）
        public PlanModule GetModule(int ID, ref bool bIsNull)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from Tab_Base_DayPlan_Item where  nID  = " + ID + "");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                bIsNull = false;
                return null;
            }
            else
            {
                bIsNull = true;
                return GetModule_DRToModel(dt.Rows[0]);

            }
        }

        public List<PlanModule> GetModule_DTToList(DataTable dt)
        {
            List<PlanModule> modelList = new List<PlanModule>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PlanModule model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetModule_DRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public PlanModule GetModule_DRToModel(DataRow dr)
        {
            PlanModule model = new PlanModule();
            if (dr != null)
            {
                model.ID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.TrainNo1 = ObjectConvertClass.static_ext_string(dr["strTrainNo1"]);
                model.TrainInfo = ObjectConvertClass.static_ext_string(dr["strTrainInfo"]);
                model.TrainNo2 = ObjectConvertClass.static_ext_string(dr["strTrainNo2"]);
                model.TrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
                model.Remark = ObjectConvertClass.static_ext_string(dr["strRemark"]);
                model.IsTomorrow = ObjectConvertClass.static_ext_int(dr["bIsTomorrow"]);
                model.GroupID = ObjectConvertClass.static_ext_int(dr["nGroupID"]);
                model.DayPlanType = ObjectConvertClass.static_ext_int(dr["nDayPlanType"]);
                model.DaWenCheXing = ObjectConvertClass.static_ext_string(dr["strDaWenCheXing"]);
                model.DaWenCheHao1 = ObjectConvertClass.static_ext_string(dr["strDaWenCheHao1"]);
                model.DaWenCheHao2 = ObjectConvertClass.static_ext_string(dr["strDaWenCheHao2"]);
                model.DaWenCheHao3 = ObjectConvertClass.static_ext_string(dr["strDaWenCheHao3"]);
            }
            return model;
        }
        #endregion

    }
    public class DBDayPlan
    {
        #region QueryPlans方法（1.6.10获取指定机车的所有机车计划信息）
        public List<DayPlan> QueryPlans(DateTime BeginDate, DateTime EndDate, int GroupID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_DayPlan_TrainRelation  where nQuDuanID =" + GroupID + "  and ( dtBeginTime >= '" + BeginDate + "' and dtEndTime <= '" + EndDate + "')");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                return new List<DayPlan>();
            }
            else
            {
                return QueryPlans_DTToList(dt);

            }
        }

        public List<DayPlan> QueryPlans_DTToList(DataTable dt)
        {
            List<DayPlan> modelList = new List<DayPlan>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                
                for (int n = 0; n < rowsCount; n++)
                {
                    DayPlan model = new DayPlan();
                    QueryPlans_DRToModel(dt.Rows[n],model);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public DayPlan QueryPlans_DRToModel(DataRow dr, DayPlan Plan)
        {
 
            if (dr != null)
            {
                Plan.strDayPlanGUID = ObjectConvertClass.static_ext_string(dr["strDayPlanGUID"]);
                Plan.dtBeginTime = ObjectConvertClass.static_ext_date(dr["dtBeginTime"]);
                Plan.dtEndTime = ObjectConvertClass.static_ext_date(dr["dtEndTime"]);
                Plan.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
                Plan.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
                Plan.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
                Plan.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
                Plan.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                Plan.strRemark = ObjectConvertClass.static_ext_string(dr["strRemark"]);
                Plan.strTrainNo1 = ObjectConvertClass.static_ext_string(dr["strTrainNo1"]);
                Plan.strTrainInfo = ObjectConvertClass.static_ext_string(dr["strTrainInfo"]);
                Plan.strTrainNo2 = ObjectConvertClass.static_ext_string(dr["strTrainNo2"]);
                Plan.nQuDuanID = ObjectConvertClass.static_ext_int(dr["nQuDuanID"]);
                Plan.nPlanID = ObjectConvertClass.static_ext_int(dr["nPlanID"]);
                Plan.bIsTomorrow = ObjectConvertClass.static_ext_int(dr["bIsTomorrow"]);
                Plan.nDayPlanID = ObjectConvertClass.static_ext_int(dr["nDayPlanID"]);
                Plan.nPlanState = ObjectConvertClass.static_ext_int(dr["nPlanState"]);
                Plan.strDaWenCheXing = ObjectConvertClass.static_ext_string(dr["strDaWenCheXing"]);
                Plan.strDaWenCheHao1 = ObjectConvertClass.static_ext_string(dr["strDaWenCheHao1"]);
                Plan.strDaWenCheHao2 = ObjectConvertClass.static_ext_string(dr["strDaWenCheHao2"]);
                Plan.strDaWenCheHao3 = ObjectConvertClass.static_ext_string(dr["strDaWenCheHao3"]);
                Plan.bIsSend = ObjectConvertClass.static_ext_int(dr["bIsSend"]);
                Plan.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
            }
            return Plan;
        }
        #endregion


        #region QueryPublishPlans方法（1.6.11获取指定机车的已下发的计划信息）
        public List<DayPlan> QueryPublishPlans(DateTime BeginDate, DateTime EndDate, int GroupID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_DayPlan_TrainRelation  where nPlanState = 2 and nQuDuanID =" + GroupID + "  and ( dtBeginTime >= '" + BeginDate + "' and dtEndTime <= '" + EndDate + "')");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                return new List<DayPlan>();
            }
            else
            {
                return QueryPlans_DTToList(dt);

            }
        }
        #endregion


        #region GetPlan方法（1.6.12获取指定的机车计划信息）
        public bool GetPlan(string PlanGUID, DayPlan Plan)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from TAB_DayPlan_TrainRelation where  strDayPlanGUID ='" + PlanGUID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                return false;
                
            }
            QueryPlans_DRToModel(dt.Rows[0], Plan);
            
            return true;
        }
        #endregion


        #region AddDayPlan（1.6.13添加机车计划信息）
        public int AddDayPlan(DayPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_DayPlan_TrainRelation");
            strSql.Append("(strDayPlanGUID,dtBeginTime,dtEndTime,dtCreateTime,strTrainNo,strTrainTypeName,strTrainNumber,strRemark,strTrainNo1,strTrainInfo,strTrainNo2,nQuDuanID,nPlanID,bIsTomorrow,nDayPlanID,nPlanState,strDaWenCheXing,strDaWenCheHao1,strDaWenCheHao2,strDaWenCheHao3,bIsSend,strTrainPlanGUID)");
            strSql.Append("values(@strDayPlanGUID,@dtBeginTime,@dtEndTime,@dtCreateTime,@strTrainNo,@strTrainTypeName,@strTrainNumber,@strRemark,@strTrainNo1,@strTrainInfo,@strTrainNo2,@nQuDuanID,@nPlanID,@bIsTomorrow,@nDayPlanID,@nPlanState,@strDaWenCheXing,@strDaWenCheHao1,@strDaWenCheHao2,@strDaWenCheHao3,@bIsSend,@strTrainPlanGUID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strDayPlanGUID", model.strDayPlanGUID),
          new SqlParameter("@dtBeginTime", model.dtBeginTime),
          new SqlParameter("@dtEndTime", model.dtEndTime),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@strTrainNo", model.strTrainNo),
          new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
          new SqlParameter("@strTrainNumber", model.strTrainNumber),
          new SqlParameter("@strRemark", model.strRemark),
          new SqlParameter("@strTrainNo1", model.strTrainNo1),
          new SqlParameter("@strTrainInfo", model.strTrainInfo),
          new SqlParameter("@strTrainNo2", model.strTrainNo2),
          new SqlParameter("@nQuDuanID", model.nQuDuanID),
          new SqlParameter("@nPlanID", model.nPlanID),
          new SqlParameter("@bIsTomorrow", model.bIsTomorrow),
          new SqlParameter("@nDayPlanID", model.nDayPlanID),
          new SqlParameter("@nPlanState", model.nPlanState),
          new SqlParameter("@strDaWenCheXing", model.strDaWenCheXing),
          new SqlParameter("@strDaWenCheHao1", model.strDaWenCheHao1),
          new SqlParameter("@strDaWenCheHao2", model.strDaWenCheHao2),
          new SqlParameter("@strDaWenCheHao3", model.strDaWenCheHao3),
          new SqlParameter("@bIsSend", model.bIsSend),
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }


        public int AddModifyPlanLog(ChangeLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_DayPlan_ChangeLog");
            strSql.Append("(strLogGUID,strlogType,strDayPlanGUID,strTrainNo1,strTrainInfo,strTrainNo2,strRemark,dtChangeTime)");
            strSql.Append("values(@strLogGUID,@strlogType,@strDayPlanGUID,@strTrainNo1,@strTrainInfo,@strTrainNo2,@strRemark,@dtChangeTime)");
            SqlParameter[] parameters = {
          new SqlParameter("@strLogGUID", model.strLogGUID),
          new SqlParameter("@strlogType", model.strlogType),
          new SqlParameter("@strDayPlanGUID", model.strDayPlanGUID),
          new SqlParameter("@strTrainNo1", model.strTrainNo1),
          new SqlParameter("@strTrainInfo", model.strTrainInfo),
          new SqlParameter("@strTrainNo2", model.strTrainNo2),
          new SqlParameter("@strRemark", model.strRemark),
          new SqlParameter("@dtChangeTime", model.dtChangeTime)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion



        #region ModifyDayPlanInfo（1.6.14修改机车计划）
        public int ModifyDayPlanInfo(DayPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_DayPlan_TrainRelation set ");
            strSql.Append(" dtBeginTime = @dtBeginTime, ");
            strSql.Append(" dtEndTime = @dtEndTime, ");
            strSql.Append(" dtCreateTime = @dtCreateTime, ");
            strSql.Append(" strTrainNo = @strTrainNo, ");
            strSql.Append(" strTrainTypeName = @strTrainTypeName, ");
            strSql.Append(" strTrainNumber = @strTrainNumber, ");
            strSql.Append(" strRemark = @strRemark, ");
            strSql.Append(" strTrainNo1 = @strTrainNo1, ");
            strSql.Append(" strTrainInfo = @strTrainInfo, ");
            strSql.Append(" strTrainNo2 = @strTrainNo2, ");
            strSql.Append(" nQuDuanID = @nQuDuanID, ");
            strSql.Append(" nPlanID = @nPlanID, ");
            strSql.Append(" bIsTomorrow = @bIsTomorrow, ");
            strSql.Append(" nDayPlanID = @nDayPlanID, ");
            strSql.Append(" nPlanState = @nPlanState, ");
            strSql.Append(" strDaWenCheXing = @strDaWenCheXing, ");
            strSql.Append(" strDaWenCheHao1 = @strDaWenCheHao1, ");
            strSql.Append(" strDaWenCheHao2 = @strDaWenCheHao2, ");
            strSql.Append(" strDaWenCheHao3 = @strDaWenCheHao3, ");
            strSql.Append(" bIsSend = @bIsSend, ");
            strSql.Append(" strTrainPlanGUID = @strTrainPlanGUID ");
            strSql.Append(" where strDayPlanGUID = @strDayPlanGUID ");

            SqlParameter[] parameters = {
          new SqlParameter("@strDayPlanGUID", model.strDayPlanGUID),
          new SqlParameter("@dtBeginTime", model.dtBeginTime),
          new SqlParameter("@dtEndTime", model.dtEndTime),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@strTrainNo", model.strTrainNo),
          new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
          new SqlParameter("@strTrainNumber", model.strTrainNumber),
          new SqlParameter("@strRemark", model.strRemark),
          new SqlParameter("@strTrainNo1", model.strTrainNo1),
          new SqlParameter("@strTrainInfo", model.strTrainInfo),
          new SqlParameter("@strTrainNo2", model.strTrainNo2),
          new SqlParameter("@nQuDuanID", model.nQuDuanID),
          new SqlParameter("@nPlanID", model.nPlanID),
          new SqlParameter("@bIsTomorrow", model.bIsTomorrow),
          new SqlParameter("@nDayPlanID", model.nDayPlanID),
          new SqlParameter("@nPlanState", model.nPlanState),
          new SqlParameter("@strDaWenCheXing", model.strDaWenCheXing),
          new SqlParameter("@strDaWenCheHao1", model.strDaWenCheHao1),
          new SqlParameter("@strDaWenCheHao2", model.strDaWenCheHao2),
          new SqlParameter("@strDaWenCheHao3", model.strDaWenCheHao3),
          new SqlParameter("@bIsSend", model.bIsSend),
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }



        public int UpdatePaiPlan(string strTrainPlanGUID, string strTrainTypeName, string strTrainNumber, string strRemark, string strSiteGUID, string strDutyGUID)
        {
           string strSql = "Select strTrainPlanGUID from TAB_DayPlan_TrainRelation where strDayPlanGUID = '" + strTrainPlanGUID + "'";
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           if (dt.Rows.Count > 0)
           {
               if (dt.Rows[0]["strTrainPlanGUID"] != null && dt.Rows[0]["strTrainPlanGUID"].ToString() != "")
               {
                   string ustrSql = "update TAB_Plan_Train set strTrainTypeName ='" + strTrainTypeName + "'";
                   ustrSql += ",strTrainNumber = '" + strTrainNumber + "' , strRemark = '" + strRemark + "' where strTrainPlanGUID = '" + dt.Rows[0]["strTrainPlanGUID"].ToString() + "'";
                   int k = 0;
                   k += SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, ustrSql.ToString());
                   SqlParameter[] parameters = {
                          new SqlParameter("@strTrainPlanGUID", dt.Rows[0]["strTrainPlanGUID"].ToString()),
                          new SqlParameter("@strDutyUserGUID", strDutyGUID),
                          new SqlParameter("@strSiteGUID", strSiteGUID)
                                                };

                   k += SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.StoredProcedure, "PROC_Plan_WriteChangeLog", parameters);

                   if (k >= 2)
                   {
                       return 1;
                   }
                   else
                   {
                       return 0;
                   }
               }
               else
               {
                   return 0;

               }
           }
           else
           {
               return 0;
           }
        
        }





        #endregion


        #region DeleteDayPlan（1.6.15删除机车计划）
        public int DeleteDayPlan(string DayPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_DayPlan_TrainRelation ");
            strSql.Append(" where strDayPlanGUID = @strDayPlanGUID ");
            SqlParameter[] parameters = {
          new SqlParameter("strDayPlanGUID",DayPlanGUID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion


        #region SendDayPlan（1.6.16下发机车计划）
        public int SendDayPlan(DateTime BeginTime, DateTime EndTime, int DayPlanID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_DayPlan_TrainRelation set nPlanState = 2  where nDayPlanID =" + DayPlanID + "  and ( dtBeginTime >= '" + BeginTime + "' and dtEndTime <= '" + EndTime + "' ) ");
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }
        #endregion


        #region 1.6.17从模版中加载机车计划

        public DataTable QueryItemList(int GroupID,int DayPlanTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Tab_Base_DayPlan_Item where nGroupID = " + GroupID + " and  nDayPlanType = " + DayPlanTypeID + " order by nid");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }

        public int LoadDayPlanInfo(DateTime DayPlanDate, DateTime dtNow, int DayPlanTypeID, int DayPlanID, int GroupID, DataTable dt)
        {
            DateTime dtBeginTime,dtEndTime;
            DayPlan dp = new DayPlan();
            int k = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dp.strDayPlanGUID = Guid.NewGuid().ToString();
                dp.nPlanState = 1;
                DateTime DayPlanDate_Day = Convert.ToDateTime(DayPlanDate.ToShortDateString());
                if (DayPlanTypeID == 0)  //白班
                {
                    dtBeginTime = DayPlanDate_Day.AddHours(8);
                    dtEndTime = DayPlanDate_Day.AddHours(18).AddSeconds(-1);
                }
                else if (DayPlanTypeID == 1)  //夜班
                {
                    dtBeginTime = DayPlanDate_Day.AddHours(18);
                    dtEndTime = DayPlanDate_Day.AddDays(1).AddHours(8).AddSeconds(-1);
                }
                else //全天
                {
                    dtBeginTime = DayPlanDate_Day.AddHours(18);
                    dtEndTime = DayPlanDate_Day.AddDays(1).AddHours(18).AddSeconds(-1);
                }
                dp.dtBeginTime = dtBeginTime;   //开始时间
                dp.dtEndTime = dtEndTime;    //结束时间
                dp.dtCreateTime = dtNow;  //产生事件
                dp.strTrainNo1 = dt.Rows[i]["strTrainNo1"].ToString(); //车次1
                dp.strTrainInfo = dt.Rows[i]["strTrainInfo"].ToString();    //机车信息
                dp.strTrainNo2 = dt.Rows[i]["strTrainNo2"].ToString();      //车次 2
                dp.strTrainNo = dt.Rows[i]["strTrainNo"].ToString();      //车次
                dp.strTrainTypeName = ""; //车型
                dp.strTrainNumber = "";  //车号
                dp.strRemark = dt.Rows[i]["strRemark"].ToString();

                if (Convert.ToInt32(dt.Rows[i]["bIsTomorrow"]) > 0)
                {
                    dp.strRemark = DayPlanDate_Day.AddDays(1).ToString() + "日";
                }
                dp.bIsTomorrow = Convert.ToInt32(dt.Rows[i]["bIsTomorrow"]);
                dp.nDayPlanID = DayPlanID;
                dp.nQuDuanID = Convert.ToInt32(dt.Rows[i]["nGroupID"]);     //区段信息
                //打温专用
                dp.strDaWenCheXing = dt.Rows[i]["strDaWenCheXing"].ToString();
                dp.strDaWenCheHao1 = dt.Rows[i]["strDaWenCheHao1"].ToString();
                dp.strDaWenCheHao2 = dt.Rows[i]["strDaWenCheHao2"].ToString();
                dp.strDaWenCheHao3 = dt.Rows[i]["strDaWenCheHao3"].ToString();
                dp.nPlanID = Convert.ToInt32(dt.Rows[i]["nID"].ToString());
                dp.bIsSend = 0;
                dp.strTrainPlanGUID = "";
                k += AddDayPlanInfo(dp);
            }
            return k;

        }

        public int AddDayPlanInfo(DayPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_DayPlan_TrainRelation");
            strSql.Append("(strDayPlanGUID,dtBeginTime,dtEndTime,dtCreateTime,strTrainNo,strTrainTypeName,strTrainNumber,strRemark,strTrainNo1,strTrainInfo,strTrainNo2,nQuDuanID,nPlanID,bIsTomorrow,nDayPlanID,nPlanState,strDaWenCheXing,strDaWenCheHao1,strDaWenCheHao2,strDaWenCheHao3,bIsSend,strTrainPlanGUID)");
            strSql.Append("values(@strDayPlanGUID,@dtBeginTime,@dtEndTime,@dtCreateTime,@strTrainNo,@strTrainTypeName,@strTrainNumber,@strRemark,@strTrainNo1,@strTrainInfo,@strTrainNo2,@nQuDuanID,@nPlanID,@bIsTomorrow,@nDayPlanID,@nPlanState,@strDaWenCheXing,@strDaWenCheHao1,@strDaWenCheHao2,@strDaWenCheHao3,@bIsSend,@strTrainPlanGUID)");
            SqlParameter[] parameters = {
          new SqlParameter("@strDayPlanGUID", model.strDayPlanGUID),
          new SqlParameter("@dtBeginTime", model.dtBeginTime),
          new SqlParameter("@dtEndTime", model.dtEndTime),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@strTrainNo", model.strTrainNo),
          new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
          new SqlParameter("@strTrainNumber", model.strTrainNumber),
          new SqlParameter("@strRemark", model.strRemark),
          new SqlParameter("@strTrainNo1", model.strTrainNo1),
          new SqlParameter("@strTrainInfo", model.strTrainInfo),
          new SqlParameter("@strTrainNo2", model.strTrainNo2),
          new SqlParameter("@nQuDuanID", model.nQuDuanID),
          new SqlParameter("@nPlanID", model.nPlanID),
          new SqlParameter("@bIsTomorrow", model.bIsTomorrow),
          new SqlParameter("@nDayPlanID", model.nDayPlanID),
          new SqlParameter("@nPlanState", model.nPlanState),
          new SqlParameter("@strDaWenCheXing", model.strDaWenCheXing),
          new SqlParameter("@strDaWenCheHao1", model.strDaWenCheHao1),
          new SqlParameter("@strDaWenCheHao2", model.strDaWenCheHao2),
          new SqlParameter("@strDaWenCheHao3", model.strDaWenCheHao3),
          new SqlParameter("@bIsSend", model.bIsSend),
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }


        public int AddDayPlan_Send(DateTime BeginTime, DateTime dtEndTime,  int DayPlanID, int DayPlanTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_DayPlan_Send");
            strSql.Append("(strGUID,nDayPlanID,dtBeginTime,dtEndTime,dtCreateTime,nDayOrNight)");
            strSql.Append("values(@strGUID,@nDayPlanID,@dtBeginTime,@dtEndTime,@dtCreateTime,@nDayOrNight)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strGUID", Guid.NewGuid().ToString()),
          new SqlParameter("@nDayPlanID", DayPlanID),
          new SqlParameter("@dtBeginTime", BeginTime),
          new SqlParameter("@dtEndTime", dtEndTime),
          new SqlParameter("@dtCreateTime", DateTime.Now),
          new SqlParameter("@nDayOrNight", DayPlanTypeID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);


        }

        #endregion

        #region 1.6.17从模版中加载机车计划

        #region 获取数量
        //public int GetTotalPlanCount(DateTime BeginDate, DateTime EndDate,int DayPlanID)
        //{
        //    string strSql = "select count(*) as nCount from TAB_DayPlan_TrainRelation  where  nDayPlanID = " + DayPlanID + " and dtBeginTime Between '" + BeginDate + "' and '" + EndDate + "'";
        //    DataTable dt= SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        return Convert.ToInt32(dt.Rows[0]["nCount"].ToString());
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        #endregion



        public List<LCDayPlan.PlanItemGroup> QueryLeftGroupListByID(DateTime BeginDate, DateTime EndDate, int DayPlanID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Tab_Base_DayPlan_Group where nDayPlanID =" + DayPlanID + " and nExcelSide = 1 order by  nExcelPos,nid");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                return new List<LCDayPlan.PlanItemGroup>();
            }
            else
            {
                return QueryLeftGroupListByID_DTToList(dt, BeginDate,EndDate);
            }
        }

        public List<LCDayPlan.PlanItemGroup> QueryLeftGroupListByID_DTToList(DataTable dt, DateTime BeginDate, DateTime EndDate)
        {
            List<LCDayPlan.PlanItemGroup> modelList = new List<LCDayPlan.PlanItemGroup>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LCDayPlan.PlanItemGroup model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = QueryLeftGroupListByID_DRToModel(dt.Rows[n], BeginDate, EndDate);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public LCDayPlan.PlanItemGroup QueryLeftGroupListByID_DRToModel(DataRow dr, DateTime BeginDate, DateTime EndDate)
        {
            LCDayPlan.PlanItemGroup model = new LCDayPlan.PlanItemGroup();
            if (dr != null)
            {
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.strName = ObjectConvertClass.static_ext_string(dr["strName"]);
                model.nExcelSide = ObjectConvertClass.static_ext_int(dr["nExcelSide"]);
                model.nExcelPos = ObjectConvertClass.static_ext_int(dr["nExcelPos"]);
                model.nDayPlanID = ObjectConvertClass.static_ext_int(dr["nDayPlanID"]);
                model.bIsDawen = ObjectConvertClass.static_ext_int(dr["bIsDawen"]);
                //model.DayPlanInfoList = QueryDayPlanInfoListByGroupID(BeginDate, EndDate, model.nID);
              

            }
            return model;
        }

        public List<DayPlan> QueryDayPlanInfoListByGroupID(DateTime BeginDate, DateTime EndDate, int GroupID)
        {
            string strSql = "select * from TAB_DayPlan_TrainRelation  where  nQuDuanID =" + GroupID + "  and ( dtBeginTime >= '" + BeginDate + "' and dtEndTime <= '" + EndDate + "' )";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                return new List<DayPlan>();
            }
            else
            {
                return QueryPlans_DTToList(dt);

            }
        }

        public List<LCDayPlan.PlanItemGroup> QueryRightGroupListByID(DateTime BeginDate, DateTime EndDate, int DayPlanID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Tab_Base_DayPlan_Group where nDayPlanID =" + DayPlanID + " and nExcelSide = 2 order by  nExcelPos,nid");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                return new List<LCDayPlan.PlanItemGroup>();
            }
            else
            {
                return QueryLeftGroupListByID_DTToList(dt, BeginDate, EndDate);
            }
        }


        #endregion


        #region 1.6.5.10修改计划为已发送
        public int SetSended(DateTime dtBeginTime, DateTime dtEndTime, int nDayPlanID)
        {
            string strsql = "update TAB_DayPlan_TrainRelation set bIsSend = 1 where nDayPlanID";
            strsql += " =" + nDayPlanID + " and bIsSend = 0  and ( dtBeginTime >= '" + dtBeginTime + "' and dtEndTime <= '" + dtEndTime + "' )";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strsql.ToString());
        }

        public  List<string> GetPaiBanPlans(DateTime dtBeginTime, DateTime dtEndTime, int nDayPlanID)
        {
            string strSql = "select strTrainPlanGUID from TAB_DayPlan_TrainRelation where nDayPlanID =" + nDayPlanID + " and bIsSend = 1";
            strSql += "and ( strTrainPlanGUID <> '' and strTrainPlanGUID is not null )";
            strSql += " and ( dtBeginTime >= '" + dtBeginTime + "' and dtEndTime <= '" + dtEndTime + "' )";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            List<string> strID = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strID.Add(dt.Rows[0]["strTrainPlanGUID"].ToString());
            }
            return strID;
        }
        #endregion


        #region 1.6.5.11清除计划

        public int ClearPlan(DateTime dtBeginTime, DateTime dtEndTime, int nDayPlanID)
        {
            string strsql = "delete from TAB_DayPlan_TrainRelation where  nDayPlanID =" + nDayPlanID + " and dtBeginTime Between '" + dtBeginTime + "' and '" + dtEndTime + "'";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strsql.ToString());
        }
        #endregion



        #region 1.6.5.12是否已经加载过计划
        public int IsLoadedPlan(DateTime BeginDate, DateTime EndDate, int nDayPlanID, int nPlanType)
        {
            string strSql = "select top 1 strGUID from Tab_DayPlan_Send  where nDayPlanID =" + nDayPlanID + "  and nDayOrNight =" + nPlanType + "";
            strSql += "and ( dtBeginTime >= '" + BeginDate + "' and dtEndTime <= '" + EndDate + "' )";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0].Rows.Count;
        }
        #endregion





    }
}
