using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;
using TF.CommonUtility;
using ThinkFreely.RunSafty;

namespace TF.RunSafty.WorkSteps
{
    public class DBStep
    {
        #region 根绝车间和步骤的编号判断步骤执行的顺序
        public int getIndexOfStep(string strWorkShopGUID, string strStepId, int nWorkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 nStepIndex ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepDef where strWorkShopGUID= '" + strWorkShopGUID + "' and strStepID='" + strStepId + "' and nWorkTypeID=" + nWorkTypeID);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["nStepIndex"].ToString());
            else
                return 0;
        }
        #endregion

        #region 插入步骤信息
        public void AddStep(List<StepIndex> LstepIndex, List<StepData> LstepData, StepResult StepResult)
        {
            getIndexOfStep(StepResult.strTrainPlanGUID, StepResult.strStepName, StepResult.strTrainmanGUID, StepResult.strTrainmanNumber, StepResult.nWorkTypeID);
            DataTable tbStepIndex = CreateTable_StepIndex();
            if (LstepIndex != null)
            {
                foreach (StepIndex si in LstepIndex)
                {
                    DataRow SiRow = tbStepIndex.NewRow();
                    SiRow["strTrainPlanGUID"] = si.strTrainPlanGUID;
                    SiRow["strTrainmanNumber"] = si.strTrainmanNumber;
                    SiRow["dtStartTime"] = si.dtStartTime;
                    SiRow["strFieldName"] = si.strFieldName;
                    SiRow["nStepData"] = si.nStepData;
                    SiRow["nWorkTypeID"] = si.nWorkTypeID;
                    if (si.dtStepData == null)
                        SiRow["dtStepData"] = DBNull.Value;
                    else
                        SiRow["dtStepData"] = si.dtStepData;

                    if (si.strStepData == null)
                        SiRow["strStepData"] = DBNull.Value;
                    else
                        SiRow["strStepData"] = si.strStepData;

                    tbStepIndex.Rows.Add(SiRow);
                }
            }
            DataTable tbStepData = CreateTable_StepData();

            if (LstepData != null)
            {
                foreach (StepData sd in LstepData)
                {
                    DataRow SiRow = tbStepData.NewRow();
                    SiRow["strTrainPlanGUID"] = sd.strTrainPlanGUID;
                    SiRow["strFieldName"] = sd.strFieldName;
                    SiRow["strStepName"] = sd.strStepName;
                    SiRow["nStepData"] = sd.nStepData;
                    SiRow["strTrainmanNumber"] = sd.strTrainmanNumber;
                    SiRow["nWorkTypeID"] = sd.nWorkTypeID;
                    if (sd.dtStepData == null)
                        SiRow["dtStepData"] = DBNull.Value;
                    else
                        SiRow["dtStepData"] = sd.dtStepData;

                    if (sd.strStepData == null)
                        SiRow["strStepData"] = DBNull.Value;
                    else
                        SiRow["strStepData"] = sd.strStepData;
                    tbStepData.Rows.Add(SiRow);
                }
            }

            AddStepIndex(tbStepIndex);
            AddStepInfo(tbStepData);
            AddStepResult(StepResult);
        }


        /// <summary>
        ///  添加索引信息
        /// </summary>
        /// <param name="dt"></param>
        public void AddStepIndex(DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnString))
            {
                SqlBulkCopy bulk = new SqlBulkCopy(conn);
                bulk.DestinationTableName = "TAB_Plan_Beginwork_StepIndex";
                bulk.ColumnMappings.Add("strTrainPlanGUID", "strTrainPlanGUID");
                bulk.ColumnMappings.Add("strTrainmanNumber", "strTrainmanNumber");
                bulk.ColumnMappings.Add("dtStartTime", "dtStartTime");
                bulk.ColumnMappings.Add("strFieldName", "strFieldName");
                bulk.ColumnMappings.Add("nStepData", "nStepData");
                bulk.ColumnMappings.Add("dtStepData", "dtStepData");
                bulk.ColumnMappings.Add("strStepData", "strStepData");
                bulk.ColumnMappings.Add("nWorkTypeID", "nWorkTypeID");
                bulk.BatchSize = dt.Rows.Count;
                if (dt != null && dt.Rows.Count != 0)
                {
                    conn.Open();
                    bulk.WriteToServer(dt);
                }
                bulk.Close();
            }
        }


        /// <summary>
        /// 添加步骤的详细信息
        /// </summary>
        /// <param name="dt"></param>
        public void AddStepInfo(DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnString))
            {
                SqlBulkCopy bulk = new SqlBulkCopy(conn);
                bulk.DestinationTableName = "TAB_Plan_Beginwork_StepData";
                bulk.ColumnMappings.Add("strTrainPlanGUID", "strTrainPlanGUID");
                bulk.ColumnMappings.Add("strFieldName", "strFieldName");
                bulk.ColumnMappings.Add("nStepData", "nStepData");
                bulk.ColumnMappings.Add("dtStepData", "dtStepData");
                bulk.ColumnMappings.Add("strStepData", "strStepData");
                bulk.ColumnMappings.Add("strStepName", "strStepName");
                bulk.ColumnMappings.Add("strTrainmanNumber", "strTrainmanNumber");
                bulk.ColumnMappings.Add("nWorkTypeID", "nWorkTypeID");
                bulk.BatchSize = dt.Rows.Count;
                if (dt != null && dt.Rows.Count != 0)
                {
                    conn.Open();
                    bulk.WriteToServer(dt);
                }
                bulk.Close();
            }
        }



        public void AddStepResult(StepResult StepResult)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Beginwork_StepResult");
            strSql.Append("(strTrainPlanGUID,strStepName,nStepResult,dtBeginTime,dtEndTime,strStepBrief,dtCreateTime,nStepIndex,strTrainmanGUID,strTrainmanNumber,strTrainmanName,nWorkTypeID)");
            strSql.Append("values(@strTrainPlanGUID,@strStepName,@nStepResult,@dtBeginTime,@dtEndTime,@strStepBrief,@dtCreateTime,@nStepIndex,@strTrainmanGUID,@strTrainmanNumber,@strTrainmanName,@nWorkTypeID)");
            SqlParameter[] parameters = {
                  new SqlParameter("@strTrainPlanGUID", StepResult.strTrainPlanGUID),
                  new SqlParameter("@strStepName", StepResult.strStepName),
                  new SqlParameter("@nStepResult", StepResult.nStepResult),
                  new SqlParameter("@dtBeginTime", StepResult.dtBeginTime),
                  new SqlParameter("@dtEndTime", StepResult.dtEndTime),
                  new SqlParameter("@strStepBrief", StepResult.strStepBrief),
                  new SqlParameter("@dtCreateTime", StepResult.dtCreateTime),
                  new SqlParameter("@nStepIndex", StepResult.nStepIndex),
                  new SqlParameter("@strTrainmanGUID", StepResult.strTrainmanGUID),
                  new SqlParameter("@strTrainmanNumber", StepResult.strTrainmanNumber),
                  new SqlParameter("@strTrainmanName", StepResult.strTrainmanName),
                  new SqlParameter("@nWorkTypeID", StepResult.nWorkTypeID)
                                        };
            SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        public class msg
        {
            public string StepName;
            public string PlanGUID;
            public int WorkType;
            public int msgType;
        
        }

        /// <summary>
        /// 插入测酒的消息记录
        /// </summary>
        /// <param name="input"></param>
        public void creatDrinkMsg(InSubmitDrink input)
        {
            string strMsg = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            AttentionMsg msg = new AttentionMsg();
            List<int> clients = msg.GetMsgClients(10602);
            foreach (int client in clients)
            {
                msg.msgType = 10602;
                msg.param = strMsg;
                msg.clientID = client;
                msg.Add();
            }
        }

        /// <summary>
        /// 插入步骤消息记录
        /// </summary>
        /// <param name="StepName"></param>
        /// <param name="PlanGUID"></param>
        /// <param name="ClientID"></param>
        public void creatMsg(string StepName, string PlanGUID, string ClientID, int nWorkTypeID, string strWorkShopGUID)
        {
            msg m = new msg();
            m.PlanGUID = PlanGUID;
            m.StepName = StepName;
            m.WorkType = nWorkTypeID;
            m.msgType = 51001;
            string strMsg = Newtonsoft.Json.JsonConvert.SerializeObject(m);
            AttentionMsg msg = new AttentionMsg();
            List<int> clients = msg.GetMsgClients(m.msgType);
            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {

                    msg.msgType = 51001;
                    msg.param = strMsg;
                    msg.CreatMsg();

                    //如果表中不存在 则向表中增加未确认的计划信息
                    if (!getExistNoConfirm(nWorkTypeID, PlanGUID) && !getExistConfirm(nWorkTypeID, PlanGUID))
                        creatNoConfirm(nWorkTypeID, strWorkShopGUID, PlanGUID, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
          
        }

        private bool getExistConfirm(int nWorkTypeID, string PlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM TAB_Plan_Beginwork_Flows where strTrainPlanGUID= '" + PlanGUID + "' and   nWorkTypeID=" + nWorkTypeID);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private bool getExistNoConfirm(int nWorkTypeID,string PlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM TAB_Plan_Beginwork_NoConfirm where strTrainPlanGUID= '" + PlanGUID + "' and   nWorkTypeID=" + nWorkTypeID);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }


        //向表中增加未确认的计划信息
        private int creatNoConfirm(int nWorkTypeID, string strWorkShopGUID, string PlanGUID,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Beginwork_NoConfirm ");
            strSql.Append("(strTrainPlanGUID,strWorkShopGUID,nWorkTypeID)");
            strSql.Append("values(@strTrainPlanGUID,@strWorkShopGUID,@nWorkTypeID)");
            SqlParameter[] parameters = {
                  new SqlParameter("@strTrainPlanGUID", PlanGUID),
                  new SqlParameter("@strWorkShopGUID", strWorkShopGUID),
                  new SqlParameter("@nWorkTypeID", nWorkTypeID)
                                            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(trans, CommandType.Text, strSql.ToString(), parameters));
        }


      
    
        #region 为三表分别创建相对应的DateTable
        private DataTable CreateTable_StepIndex()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("strTrainPlanGUID", typeof(string)));
            table.Columns.Add(new DataColumn("strTrainmanNumber", typeof(string)));
            table.Columns.Add(new DataColumn("dtStartTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("strFieldName", typeof(string)));
            table.Columns.Add(new DataColumn("nStepData", typeof(int)));
            table.Columns.Add(new DataColumn("dtStepData", typeof(DateTime)));
            table.Columns.Add(new DataColumn("strStepData", typeof(string)));
            table.Columns.Add(new DataColumn("strStepName", typeof(string)));
            table.Columns.Add(new DataColumn("nWorkTypeID", typeof(int)));
            return table;
        }
        private DataTable CreateTable_StepData()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("strTrainPlanGUID", typeof(string)));
            table.Columns.Add(new DataColumn("strFieldName", typeof(string)));
            table.Columns.Add(new DataColumn("strTrainmanNumber", typeof(string)));
            table.Columns.Add(new DataColumn("nStepData", typeof(int)));
            table.Columns.Add(new DataColumn("dtStepData", typeof(DateTime)));
            table.Columns.Add(new DataColumn("strStepData", typeof(string)));
            table.Columns.Add(new DataColumn("strStepName", typeof(string)));
            table.Columns.Add(new DataColumn("nWorkTypeID", typeof(int)));
            return table;
        }
        private DataTable CreateTable_StepResult()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("strTrainPlanGUID", typeof(string)));
            table.Columns.Add(new DataColumn("strStepName", typeof(string)));
            table.Columns.Add(new DataColumn("nStepResult", typeof(int)));
            table.Columns.Add(new DataColumn("dtBeginTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("dtEndTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("strStepBrief", typeof(string)));
            table.Columns.Add(new DataColumn("dtCreateTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("nStepIndex", typeof(int)));
            table.Columns.Add(new DataColumn("nWorkTypeID", typeof(int)));
            return table;
        }
        #endregion

        #region  判断表中是否已经存在  不存在添加直接执行下一步 否则删除
        public void getIndexOfStep(string strTrainPlanGUID, string strStepName, string strTrainmanGUID, string strTrainmanNumber, int nWorkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID= '" + strTrainPlanGUID + "' and strStepName='" + strStepName + "' and strTrainmanGUID='" + strTrainmanGUID + "' and nWorkTypeID=" + nWorkTypeID);
            int k = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
            if (k > 0)
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("delete FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID= '" + strTrainPlanGUID + "' and strStepName='" + strStepName + "' and strTrainmanGUID='" + strTrainmanGUID + "' and nWorkTypeID=" + nWorkTypeID);
                SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql2.ToString());

                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("delete FROM TAB_Plan_Beginwork_StepData where strTrainPlanGUID= '" + strTrainPlanGUID + "' and strStepName='" + strStepName + "' and strTrainmanNumber='" + strTrainmanNumber + "' and nWorkTypeID=" + nWorkTypeID);
                SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql3.ToString());

                StringBuilder strSql4 = new StringBuilder();
                strSql4.Append("delete FROM TAB_Plan_Beginwork_StepIndex where strTrainPlanGUID= '" + strTrainPlanGUID + "' and strTrainmanNumber='" + strTrainmanNumber + "'and nWorkTypeID=" + nWorkTypeID);
                SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql4.ToString());
            }
        }
        #endregion

        #region 插入验卡记录

        public void AddIDICCard(IdIcCard StepResult)
        {
            //添加验卡记录前
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("  update   TAB_Plan_RunEvent_IDICCard set strTrainPlanGUID='" + Guid.NewGuid().ToString() + "' where strTrainPlanGUID='" + StepResult.strTrainPlanGUID + "'");
            SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql1.ToString());

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_RunEvent_IDICCard");
            strSql.Append("(strTrainPlanGUID,dtCreateTime,dtEventTime,strEventBrief,strTrainmanName,strTrainmanNumber)");
            strSql.Append("values(@strTrainPlanGUID,@dtCreateTime,@dtEventTime,@strEventBrief,@strTrainmanName,@strTrainmanNumber)");
            SqlParameter[] parameters = {
                  new SqlParameter("@strTrainPlanGUID", StepResult.strTrainPlanGUID),
                  new SqlParameter("@dtCreateTime", StepResult.dtCreateTime),
                  new SqlParameter("@dtEventTime", StepResult.dtEventTime),
                  new SqlParameter("@strEventBrief", StepResult.strEventBrief),
                  new SqlParameter("@strTrainmanName", StepResult.strTrainmanName),
                  new SqlParameter("@strTrainmanNumber", StepResult.strTrainmanNumber)
                                        };
            SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        #region 更新记名式传达记录
        public void UpdateReadTime(string strFileGUID, string strTrainmanGUID, string strReadTime)
        {
            string strWhere = string.Format(" StrFileGUID='{0}' AND StrTrainmanGUID='{1}' ", strFileGUID, strTrainmanGUID);
            TF.CommonUtility.LogClass.log(strWhere);
            List<ReadDocPlan> plans = this.GetModelList(strWhere);
            if (plans != null && plans.Count > 0)
            {
                ReadDocPlan plan = plans[0];
                DateTime readTime = DateTime.Parse(strReadTime);
                if (plan.NReadCount.HasValue && plan.NReadCount.Value > 0) //已经阅读
                {
                    plan.NReadCount++;
                    plan.DtLastReadTime = readTime;
                }
                else
                {
                    plan.DtFirstReadTime = readTime;
                    plan.DtLastReadTime = readTime;
                    plan.NReadCount = 1;

                }
                if (this.Update(plan))
                {
                    TF.CommonUtility.LogClass.log("阅读记录更新成功");
                }
                else
                {
                    TF.CommonUtility.LogClass.log("阅读记录更新失败");
                }
            }
        }

        public bool Update(ReadDocPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_ReadDocPlan set ");
            strSql.Append("StrTrainmanGUID=@StrTrainmanGUID,");
            strSql.Append("StrFileGUID=@StrFileGUID,");
            strSql.Append("NReadCount=@NReadCount,");
            strSql.Append("DtFirstReadTime=@DtFirstReadTime,");
            strSql.Append("DtLastReadTime=@DtLastReadTime");
            strSql.Append(" where nId=@nId");
            SqlParameter[] parameters = {
					new SqlParameter("@StrTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrFileGUID", SqlDbType.VarChar,50),
					new SqlParameter("@NReadCount", SqlDbType.Int,4),
					new SqlParameter("@DtFirstReadTime", SqlDbType.DateTime),
					new SqlParameter("@DtLastReadTime", SqlDbType.DateTime),
					new SqlParameter("@nId", SqlDbType.Int,4)};
            parameters[0].Value = model.StrTrainmanGUID;
            parameters[1].Value = model.StrFileGUID;
            parameters[2].Value = model.NReadCount;
            parameters[3].Value = model.DtFirstReadTime;
            parameters[4].Value = model.DtLastReadTime;
            parameters[5].Value = model.nId;

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
        public List<ReadDocPlan> GetModelList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        public List<ReadDocPlan> DataTableToList(DataTable dt)
        {
            List<ReadDocPlan> modelList = new List<ReadDocPlan>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ReadDocPlan model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        #endregion

        #region 获取需要阅读的记名式传达数量
        public int getNeedReadCount(string tmGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM TAB_ReadDocPlan where StrTrainmanGUID= '" + tmGUID + "' and dtEndTime>'" + DateTime.Now + "' ");
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }
        #endregion

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ReadDocPlan DataRowToModel(DataRow row)
        {
            ReadDocPlan model = new ReadDocPlan();
            if (row != null)
            {
                if (row["nId"] != null && row["nId"].ToString() != "")
                {
                    model.nId = int.Parse(row["nId"].ToString());
                }
                if (row["StrTrainmanGUID"] != null)
                {
                    model.StrTrainmanGUID = row["StrTrainmanGUID"].ToString();
                }
                if (row["StrFileGUID"] != null)
                {
                    model.StrFileGUID = row["StrFileGUID"].ToString();
                }
                if (row["NReadCount"] != null && row["NReadCount"].ToString() != "")
                {
                    model.NReadCount = int.Parse(row["NReadCount"].ToString());
                }
                if (row["DtFirstReadTime"] != null && row["DtFirstReadTime"].ToString() != "")
                {
                    model.DtFirstReadTime = DateTime.Parse(row["DtFirstReadTime"].ToString());
                }
                if (row["DtLastReadTime"] != null && row["DtLastReadTime"].ToString() != "")
                {
                    model.DtLastReadTime = DateTime.Parse(row["DtLastReadTime"].ToString());
                }
            }
            return model;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nId,StrTrainmanGUID,StrFileGUID,NReadCount,DtFirstReadTime,DtLastReadTime ");
            strSql.Append(" FROM TAB_ReadDocPlan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        

        #region 检测是否是特殊步骤（特殊步骤触发）
        public bool CheckIsSpecialStep(string strStepName, string strWorkShopGUID, int nWorkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM TAB_Plan_Beginwork_Rule where strKeyStepName= '" + strStepName + "' and strWorkShopGUID='" + strWorkShopGUID + "' and nConfirmType=0 and nWorkTypeID=" + nWorkTypeID);
            int k = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
            if (k > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region 将计划状态改成已经出勤
        public bool UpdateToYiChuQin(string strTrainPlanGUID, DateTime dtConfirmTime, string strConfirmBrief, string strUserName, string strUserNumber, int nWorkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [TAB_Plan_Train] set ");
            strSql.Append("nPlanState=@nPlanState  where");
            strSql.Append(" strTrainPlanGUID=@strTrainPlanGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@nPlanState", SqlDbType.Int,4),
					new SqlParameter("@strTrainPlanGUID", SqlDbType.VarChar,50)};
            parameters[0].Value = 7;
            parameters[1].Value = strTrainPlanGUID;
            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {

                MDBeginWorkFlow md = new MDBeginWorkFlow();
                md.dtBeginTime = getStepDateTime(1, strTrainPlanGUID);
                md.dtConfirmTime = DateTime.Now;
                md.dtCreateTime = DateTime.Now;
                md.dtEndTime = getStepDateTime(2, strTrainPlanGUID);
                md.nConfirmType = 1;
                TimeSpan ts = md.dtEndTime - md.dtBeginTime;
                md.nExecLength = Convert.ToInt32(ts.TotalMinutes);
                md.nFlowState = 1;
                md.strConfirmBrief = strConfirmBrief;
                md.strTrainPlanGUID = strTrainPlanGUID;
                md.strUserName = strUserName;
                md.strUserNumber = strUserNumber;
                md.nWorkTypeID = nWorkTypeID;
                AddStepFlow(md);
                delNoConfirm(strTrainPlanGUID, nWorkTypeID);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 将计划状态改成已退勤
        public bool UpdateToYiTuiQin(string strTrainPlanGUID, string strSiteGUID, DateTime dtConfirmTime, string strConfirmBrief, string strUserName, string strUserNumber, int nWorkTypeID)
        {
            DBEndWork dbEnd = new DBEndWork();
            BeginEndWork logic = new BeginEndWork();
            DateTime arriveTime = logic.GetLastArrvieTime(strTrainPlanGUID);
            dbEnd.Endwork(strTrainPlanGUID, arriveTime, "", 0, strSiteGUID);
            DBStep db = new DBStep();
            MDBeginWorkFlow md = new MDBeginWorkFlow();
            md.dtBeginTime = db.getStepDateTime(1, strTrainPlanGUID);
            md.dtConfirmTime = DateTime.Now;
            md.dtCreateTime = DateTime.Now;
            md.dtEndTime = db.getStepDateTime(2, strTrainPlanGUID);
            md.nConfirmType = 1;
            TimeSpan ts = md.dtEndTime - md.dtBeginTime;
            md.nExecLength = Convert.ToInt32(ts.TotalMinutes);
            md.nFlowState = 1;
            md.strConfirmBrief = "";
            md.strTrainPlanGUID = strTrainPlanGUID;
            md.strUserName = "";
            md.strUserNumber = "";
            md.nWorkTypeID = 2;
            bool i = db.AddStepFlow(md) > 0;
            if (i)
            {
                delNoConfirm(strTrainPlanGUID, nWorkTypeID);
                return i;
            }
            return i;
        }
        #endregion


        #region 获取步骤能否被执行DB操作


        /// <summary>
        /// 判断该车间是否需要按顺序执行
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="nWorkTypeID"></param>
        /// <returns></returns>
        public int getNexecByStepIndex(string strWorkShopGUID, int nWorkTypeID)
        {
            int nExecByStepIndex = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nExecByStepIndex ");
            strSql.Append(" FROM TAB_Plan_Beginwork_Rule where strWorkShopGUID='" + strWorkShopGUID + "' and nWorkTypeID=" + nWorkTypeID);
            object OExecByStepIndex = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            if (OExecByStepIndex != null)
                nExecByStepIndex = Convert.ToInt32(OExecByStepIndex);
            else
                nExecByStepIndex = 0;
            return nExecByStepIndex;
        }



        /// <summary>
        /// 通过车间id 步骤id 获取步骤的详细信息
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="strStepID"></param>
        /// <param name="nWorkTypeID"></param>
        /// <returns></returns>
        public DataTable getDefData(string strWorkShopGUID, string strStepID, int nWorkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepDef where strWorkShopGUID='" + strWorkShopGUID + "' and strStepID='" + strStepID + "' and nWorkTypeID=" + nWorkTypeID);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            return dt;
        }

        /// <summary>
        /// 从步骤列表中获取  步骤最小的编号
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="nWorkTypeID"></param>
        /// <returns></returns>
        public int getMinStepIndex(string strWorkShopGUID, int nWorkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select min(nStepIndex)  ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepDef where strWorkShopGUID='" + strWorkShopGUID + "'  and nWorkTypeID=" + nWorkTypeID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }

        /// <summary>
        /// 获取步骤上一步骤的详细信息
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="strStepIndex"></param>
        /// <param name="nWorkTypeID"></param>
        /// <returns></returns>
        public DataTable getDefDataUp(string strWorkShopGUID, int strStepIndex, int nWorkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepDef where nStepIndex=" + strStepIndex + " and strWorkShopGUID='" + strWorkShopGUID + "' and nWorkTypeID=" + nWorkTypeID);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 判断上一个步骤是否执行过
        /// </summary>
        /// <param name="strTrainPlanGUID"></param>
        /// <param name="nStepIndexUp"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="nWorkTypeID"></param>
        /// <returns></returns>
        public int getResultCount(string strTrainPlanGUID, int nStepIndexUp, string strTrainmanGUID, int nWorkTypeID)
        {
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select count(*) ");
            strSql2.Append(" FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID='" + strTrainPlanGUID + "' and nStepIndex=" + nStepIndexUp + " and strTrainmanGUID='" + strTrainmanGUID + "' and nWorkTypeID=" + nWorkTypeID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()));
        }

        /// <summary>
        /// 判断当前不步骤是否执行过
        /// </summary>
        /// <param name="strTrainPlanGUID"></param>
        /// <param name="strStepName"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="nWorkTypeID"></param>
        /// <returns></returns>
        public int getResultNow(string strTrainPlanGUID, string strStepName, string strTrainmanGUID, int nWorkTypeID)
        {
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select count(*) ");
            strSql2.Append(" FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID='" + strTrainPlanGUID + "' and strStepName='" + strStepName + "' and strTrainmanGUID='" + strTrainmanGUID + "' and nWorkTypeID=" + nWorkTypeID);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()));

        }

        /// <summary>
        /// 判断客户端是否经过刻意的故障设置
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="strStepID"></param>
        /// <param name="nWorkTypeID"></param>
        /// <returns></returns>
        public bool IsGuZhang(string strWorkShopGUID, string strStepID, int nWorkTypeID)
        {
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append(" select top 1 ModeTypeID  from TAB_Plan_Beginwork_FlowSetRecord_Main where  ");
            strSql1.Append("WorkShopGUID='" + strWorkShopGUID + "'  and ");
            strSql1.Append("FlowType=" + nWorkTypeID + " and BeginTime<'" + DateTime.Now.ToString() + "' order by nID desc ");
            DataTable dt1 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql1.ToString()).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["ModeTypeID"].ToString() == "1")
                {
                    return false;
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_Plan_Beginwork_FlowSetRecord_Detail where strMainGUID in ");
            strSql.Append(" (select top 1 strGUID  from TAB_Plan_Beginwork_FlowSetRecord_Main where  ");
            strSql.Append("WorkShopGUID='" + strWorkShopGUID + "' and ");
            strSql.Append("FlowType=" + nWorkTypeID + " and BeginTime<'" + DateTime.Now.ToString() + "' order by nID desc )");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count == 0)
                return false;

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select top 1 nStepIndex ");
            strSql2.Append(" FROM TAB_Plan_Beginwork_StepDef where strWorkShopGUID='" + strWorkShopGUID + "' and strStepID='" + strStepID + "' and nWorkTypeID=" + nWorkTypeID);
            DataTable dt2 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()).Tables[0];
            if (dt2.Rows.Count > 0)
            {
                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("select top 1 strStepID ");
                strSql3.Append(" FROM TAB_Plan_Beginwork_StepDef where strWorkShopGUID='" + strWorkShopGUID + "' and nWorkTypeID=" + nWorkTypeID + " and  nStepIndex=" + (Convert.ToInt32(dt2.Rows[0]["nStepIndex"]) - 1).ToString());
                DataTable dt3 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql3.ToString()).Tables[0];
                if (dt3.Rows.Count > 0)
                {
                    strStepID = dt3.Rows[0]["strStepID"].ToString();
                }
            }


            //如果找不到
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["strStepID"].ToString() == strStepID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion


        #region 获取步骤是否能够执行
        public IsExecute getIsExecute(string strWorkShopGUID, string strTrainPlanGUID, string strTrainmanGUID, string strStepID, int nWorkTypeID)
        {

            int IsExecute = 0;//三种状态  1，可以执行  0，不能执行  2，已经存在该步骤
            int nExecByStepIndex = 0; //是否需要按步骤执行，数据库里的  是否按照顺序 0,不是按照顺序,1,按照顺序，
            int nStepIndexNow = 0;//当前的步骤顺序
            string strStepBrief = "";//上一个步骤的名称
            string strStepBriefNow = "";//当前的步骤名称
            int minStepIndex;//编号最小的步骤（为了排除，第一个编号不为1的情况）
            int nResultCountUp = 0; //上一步骤是否被执行
            int nResultCountNow = 0;//当前步骤是否被执行

            IsExecute i = new IsExecute();

            bool g = IsGuZhang(strWorkShopGUID, strStepID, nWorkTypeID);
            if (g == true)
            {
                IsExecute = 1;
                i.Remark = "可以执行！";
                i.CanExecute = IsExecute;
                return i;
            }

            //通过步骤id 车间流程获取当前的步骤详细信息
            DataTable dtDef = getDefData(strWorkShopGUID, strStepID, nWorkTypeID);
            if (dtDef.Rows.Count > 0)
            {
                nStepIndexNow = Convert.ToInt32(dtDef.Rows[0]["nStepIndex"].ToString());
                strStepBriefNow = dtDef.Rows[0]["strStepName"].ToString();

                //该步骤被执行的结果次数（当前步骤，用来判断是否执行过）
                nResultCountNow = getResultNow(strTrainPlanGUID, strStepID, strTrainmanGUID, nWorkTypeID);
                if (nResultCountNow >= 1)//如果当前的步骤已经执行过
                {
                    IsExecute = 2;
                    i.Remark = "该步骤《" + strStepBriefNow + "》已经执行过";
                    i.CanExecute = IsExecute;
                    return i;
                }

                int nIsNecessary = Convert.ToInt32(dtDef.Rows[0]["nIsNecessary"].ToString());
                if (nIsNecessary == 0)
                {
                    IsExecute = 1;
                    i.Remark = "可以执行！";
                    i.CanExecute = IsExecute;
                    return i;
                }
            }
            else //如果不存在该步骤的话  直接返回失败
            {
                nStepIndexNow = 0;
                IsExecute = 1;
                i.CanExecute = IsExecute;
                i.Remark = "可以执行！";
               // i.Remark = "不存在该步骤《" + strStepID + "》，请让管理员配置！";
                return i;
            }

            //通过流程和车间，查看该车间下的步骤是否需要按顺序执行  默认是不按顺序执行
            nExecByStepIndex = getNexecByStepIndex(strWorkShopGUID, nWorkTypeID);
            if (nExecByStepIndex == 0)  // 不按顺序执行
            {
                IsExecute = 1;
                i.Remark = "可以执行！";
                i.CanExecute = IsExecute;
                return i;
            }


            //从步骤列表中获取  步骤最小的编号，用来判断所需步骤是否是第一个步骤
            minStepIndex = getMinStepIndex(strWorkShopGUID, nWorkTypeID);
            if (nStepIndexNow == minStepIndex)//如果是第一个步骤
            {
                IsExecute = 1;
                i.Remark = "可以执行！";
                i.CanExecute = IsExecute;
                return i;
            }
        

            //获取步骤上一步骤的详细信息
            int nStepIndexUp = nStepIndexNow - 1;//上一个步骤的顺序
            DataTable dtDefUp = getDefDataUp(strWorkShopGUID, nStepIndexUp, nWorkTypeID);
            if (dtDefUp.Rows.Count > 0)
            {
                strStepBrief = dtDefUp.Rows[0]["strStepName"].ToString();
                int nIsNecessary = Convert.ToInt32(dtDefUp.Rows[0]["nIsNecessary"].ToString());
                if (nIsNecessary == 0)
                {
                    IsExecute = 1;
                    i.Remark = "可以执行！";
                    i.CanExecute = IsExecute;
                    return i;
                }
            }

            //从步骤执行结果表中获取该步骤执行的次数(上一步骤)
            nResultCountUp = getResultCount(strTrainPlanGUID, nStepIndexUp, strTrainmanGUID, nWorkTypeID);
            if (nExecByStepIndex == 1 && nResultCountUp >= 1) //按顺序执行,如果前一个步骤已经执行，
            {
                IsExecute = 1;
                i.Remark = "可以执行！";
            }
            else if (nExecByStepIndex == 1 && nResultCountUp == 0) //按照顺序执行，并且前面一个没有执行
            {
                IsExecute = 0;
                i.Remark = "《" + strStepBriefNow + "》不能执行，原因：《" + strStepBrief + "》没有执行！";
            }
            else
            {
                IsExecute = 0;
                i.Remark = "无法判断是否可以被执行，请联系管理员！nExecByStepIndex" + nExecByStepIndex + "nResultCountUp:" + nResultCountUp + "";
            }
            i.CanExecute = IsExecute;
            return i;
        }
        #endregion


        #region 判断所需步骤是否执行完毕
        public bool CheckIsFinished(string strTrainPlanGUID, string strWorkShopGUID, int nWorkTypeID)
        {
            //检测两个人都是否已经测完酒
            DBDrink db=new DBDrink();
            if (!db.CheckIsCejiuAll(strTrainPlanGUID,nWorkTypeID))
            {
                return false;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count (*)  ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID= '" + strTrainPlanGUID + "' and nWorkTypeID=" + nWorkTypeID);
            int k1 = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));


            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select count(*)  ");
            strSql2.Append(" FROM TAB_Plan_Beginwork_StepDef where strWorkShopGUID= '" + strWorkShopGUID + "' and nWorkTypeID=" + nWorkTypeID);
            int k2 = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()));
            if (k1 > k2) //k2为实际的需要执行的步骤  k1是已经执行完毕的步骤
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 根据计划guid 判断步骤的开始结束时间(1,获取该计划下的步骤最早时间  2，步骤的最晚时间)
        public DateTime getStepDateTime(int be, string strTrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 dtCreateTime ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID= '" + strTrainPlanGUID + "'");
            if (be == 2)
                strSql.Append(" order by dtCreateTime desc");
            else
                strSql.Append(" order by dtCreateTime asc");

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                return ObjectConvertClass.static_ext_Date(dt.Rows[0]["dtCreateTime"]);
            else
                return DateTime.Now;
        }
        #endregion

        #region 步骤结束后向接口 ，继续向数据库中添加一条记录
        /// <summary>
        /// 步骤结束后向接口 ，继续向数据库中添加一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddStepFlow(MDBeginWorkFlow model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Beginwork_Flows");
            strSql.Append("(strTrainPlanGUID,strUserName,strUserNumber,dtConfirmTime,nConfirmType,strConfirmBrief,nFlowState,dtCreateTime,dtBeginTime,dtEndTime,nExecLength,nWorkTypeID)");
            strSql.Append("values(@strTrainPlanGUID,@strUserName,@strUserNumber,@dtConfirmTime,@nConfirmType,@strConfirmBrief,@nFlowState,@dtCreateTime,@dtBeginTime,@dtEndTime,@nExecLength,@nWorkTypeID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@strUserName", model.strUserName),
          new SqlParameter("@strUserNumber", model.strUserNumber),
          new SqlParameter("@dtConfirmTime", model.dtConfirmTime),
          new SqlParameter("@nConfirmType", model.nConfirmType),
          new SqlParameter("@strConfirmBrief", model.strConfirmBrief),
          new SqlParameter("@nFlowState", model.nFlowState),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@dtBeginTime", model.dtBeginTime),
          new SqlParameter("@dtEndTime", model.dtEndTime),
          new SqlParameter("@nExecLength", model.nExecLength),
          new SqlParameter("@nWorkTypeID", model.nWorkTypeID)
                                        };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
        }
        #endregion


        #region  向老的表中插入数据  兼容旧的程序
        public void AddPlan_Beginwork_Step(MDPlan_Beginwork_Step model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Beginwork_Step");
            strSql.Append("(strTrainPlanGUID,nStepID,strTrainmanGUID,strTrainmanNumber,strTrainmanName,nStepResultID,strStepResultText,dtCreateTime,dtEventTime,dtEventEndTime)");
            strSql.Append("values('{0}','{1}','{2}','{3}','{4}','{5}','{6}',getdate(),'{7}','{8}')");

            string NewSql = string.Format(strSql.ToString(),
                model.strTrainPlanGUID,
                model.nStepID,
                model.strTrainmanGUID,
                model.strTrainmanNumber,
                model.strTrainmanName,
                model.nStepResultID,
                model.strStepResultText,
                model.dtEventTime,
                model.dtEventEndTime);

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, NewSql.ToString());

        }

        #endregion



        #region 向运行记录表中增加数据

        //向计划表中获取车型车号以及人员信息
        public DataTable getTrainInfo(string strTrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 nKehuoID,strTrainNo,strTrainNumber,strTrainTypeName,strTrainmanNumber1,strTrainmanNumber2 ");
            strSql.Append(" FROM VIEW_Plan_Trainman  where strTrainPlanGUID='" + strTrainPlanGUID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            return dt;
        }

        //判断运行记录表中是否已经有数据
        public int nCheckIsExist(string strTrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count (*)  ");
            strSql.Append(" FROM TAB_Plan_RunEvent where strTrainPlanGUID='" + strTrainPlanGUID + "'");
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }

        //添加数据
        public int AddPlan_RunEvent(MDRunEvent model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_RunEvent");
            strSql.Append("(strRunEventGUID,strTrainPlanGUID,nEventID,dtEventTime,strTrainNo,strTrainTypeName,strTrainNumber,nTMIS,nKeHuo,strGroupGUID,strTrainmanNumber1,strTrainmanNumber2,dtCreateTime,nResult,strResult)");
            strSql.Append("values(@strRunEventGUID,@strTrainPlanGUID,@nEventID,@dtEventTime,@strTrainNo,@strTrainTypeName,@strTrainNumber,@nTMIS,@nKeHuo,@strGroupGUID,@strTrainmanNumber1,@strTrainmanNumber2,@dtCreateTime,@nResult,@strResult)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strRunEventGUID", model.strRunEventGUID),
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@nEventID", model.nEventID),
          new SqlParameter("@dtEventTime", model.dtEventTime),
          new SqlParameter("@strTrainNo", model.strTrainNo),
          new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
          new SqlParameter("@strTrainNumber", model.strTrainNumber),
          new SqlParameter("@nTMIS", model.nTMIS),
          new SqlParameter("@nKeHuo", model.nKeHuo),
          new SqlParameter("@strGroupGUID", model.strGroupGUID),
          new SqlParameter("@strTrainmanNumber1", model.strTrainmanNumber1),
          new SqlParameter("@strTrainmanNumber2", model.strTrainmanNumber2),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@nResult", model.nResult),
          new SqlParameter("@strResult", model.strResult)
                                        };

            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
        }

       
       //修改运行记录
        public int UpdatePlan_RunEvent(MDRunEvent model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_Plan_RunEvent set ");
            strSql.Append(" strRunEventGUID = @strRunEventGUID, ");
            strSql.Append(" nEventID = @nEventID, ");
            strSql.Append(" dtEventTime = @dtEventTime, ");
            strSql.Append(" strTrainNo = @strTrainNo, ");
            strSql.Append(" strTrainTypeName = @strTrainTypeName, ");
            strSql.Append(" strTrainNumber = @strTrainNumber, ");
            strSql.Append(" nTMIS = @nTMIS, ");
            strSql.Append(" nKeHuo = @nKeHuo, ");
            strSql.Append(" strGroupGUID = @strGroupGUID, ");
            strSql.Append(" strTrainmanNumber1 = @strTrainmanNumber1, ");
            strSql.Append(" strTrainmanNumber2 = @strTrainmanNumber2, ");
            strSql.Append(" dtCreateTime = @dtCreateTime, ");
            strSql.Append(" nResult = @nResult, ");
            strSql.Append(" strResult = @strResult ");
            strSql.Append(" where strTrainPlanGUID = @strTrainPlanGUID ");
            SqlParameter[] parameters = {
          new SqlParameter("@strRunEventGUID", model.strRunEventGUID),
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@nEventID", model.nEventID),
          new SqlParameter("@dtEventTime", model.dtEventTime),
          new SqlParameter("@strTrainNo", model.strTrainNo),
          new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
          new SqlParameter("@strTrainNumber", model.strTrainNumber),
          new SqlParameter("@nTMIS", model.nTMIS),
          new SqlParameter("@nKeHuo", model.nKeHuo),
          new SqlParameter("@strGroupGUID", model.strGroupGUID),
          new SqlParameter("@strTrainmanNumber1", model.strTrainmanNumber1),
          new SqlParameter("@strTrainmanNumber2", model.strTrainmanNumber2),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@nResult", model.nResult),
          new SqlParameter("@strResult", model.strResult)
                                        };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }


        //添加运行记录日志文件
        public int AddTrainmanDetail(RunEvent_TrainmanDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_RunEvent_TrainmanDetail");
            strSql.Append("(strRunEventGUID,nEventID,dtEventTime,strTrainmanNumber,nTMIS,dtCreateTime,nResultID,strResult,nSubmitResult,strSubmitRemark,nKeHuo)");
            strSql.Append("values(@strRunEventGUID,@nEventID,@dtEventTime,@strTrainmanNumber,@nTMIS,@dtCreateTime,@nResultID,@strResult,@nSubmitResult,@strSubmitRemark,@nKeHuo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strRunEventGUID", model.strRunEventGUID),
          new SqlParameter("@nEventID", model.nEventID),
          new SqlParameter("@dtEventTime", model.dtEventTime),
          new SqlParameter("@strTrainmanNumber", model.strTrainmanNumber),
          new SqlParameter("@nTMIS", model.nTMIS),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@nResultID", model.nResultID),
          new SqlParameter("@strResult", model.strResult),
          new SqlParameter("@nSubmitResult", model.nSubmitResult),
          new SqlParameter("@strSubmitRemark", model.strSubmitRemark),
          new SqlParameter("@nKeHuo", model.nKeHuo)};

            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
        }







        #endregion


        //删除未确认的计划信息
        public void delNoConfirm(string strTrainPlanGUID, int nWorkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete FROM TAB_Plan_Beginwork_NoConfirm where strTrainPlanGUID= '" + strTrainPlanGUID + "' and nWorkTypeID=" + nWorkTypeID);
            SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }


        #region 根据工号 获取人员的GUID
        public string getTrainManGUID(string strTrainNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select strTrainmanGUID from  TAB_Org_Trainman where strTrainmanNumber='" + strTrainNumber + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                return ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID"]);
            else
                return "";
        }
        #endregion






    }
}