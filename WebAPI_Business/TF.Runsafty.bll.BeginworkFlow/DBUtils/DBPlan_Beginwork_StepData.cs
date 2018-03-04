using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using System.Collections.Generic;

namespace TF.RunSafty.BeginworkFlow
{
    /// <summary>
    ///类名: Plan_Beginwork_StepDataQueryCondition
    ///说明: 出勤计划GUID查询条件类
    /// </summary>
    public class Plan_Beginwork_StepDataQueryCondition
    {
        public int page = 0;
        public int rows = 0;
        //
        public int nID = 0;
        //出勤计划GUID
        public string strTrainPlanGUID = "";
        //字段名称
        public string strFieldName = "";
        //字段值(整形)
        public int? nStepData = 0;
        //字段值(日期型)
        public DateTime? dtStepData;
        //字段值(字符串型)
        public string strStepData = "";
        public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
        {
            SqlCondition = new StringBuilder();
            SqlCondition.Append(nID != null ? " and nID = @nID" : "");
            SqlCondition.Append(strTrainPlanGUID != "" ? " and strTrainPlanGUID = @strTrainPlanGUID" : "");
            SqlCondition.Append(strFieldName != "" ? " and strFieldName = @strFieldName" : "");
            SqlCondition.Append(nStepData != null ? " and nStepData = @nStepData" : "");
            SqlCondition.Append(dtStepData != null ? " and dtStepData = @dtStepData" : "");
            SqlCondition.Append(strStepData != "" ? " and strStepData = @strStepData" : "");
            SqlParameter[] sqlParams ={
          new SqlParameter("nID",nID),
          new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
          new SqlParameter("strFieldName",strFieldName),
          new SqlParameter("nStepData",nStepData),
          new SqlParameter("dtStepData",dtStepData),
          new SqlParameter("strStepData",strStepData)};
            Params = sqlParams;
        }
    }
    /// <summary>
    ///类名: DBPlan_Beginwork_StepData
    ///说明: 出勤计划GUID数据操作类
    /// </summary>
    public class DBPlan_Beginwork_StepData
    {
        #region 增删改
        /// <summary>
        /// 添加数据
        /// </summary>
        public int Add(Plan_Beginwork_StepData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_Beginwork_StepData");
            strSql.Append("(strTrainPlanGUID,strFieldName,nStepData,dtStepData,strStepData)");
            strSql.Append("values(@strTrainPlanGUID,@strFieldName,@nStepData,@dtStepData,@strStepData)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@strFieldName", model.strFieldName),
          new SqlParameter("@nStepData", model.nStepData),
          new SqlParameter("@dtStepData", model.dtStepData),
          new SqlParameter("@strStepData", model.strStepData)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        public bool Update(Plan_Beginwork_StepData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_Plan_Beginwork_StepData set ");
            strSql.Append(" strTrainPlanGUID = @strTrainPlanGUID, ");
            strSql.Append(" strFieldName = @strFieldName, ");
            strSql.Append(" nStepData = @nStepData, ");
            strSql.Append(" dtStepData = @dtStepData, ");
            strSql.Append(" strStepData = @strStepData ");
            strSql.Append(" where nID = @nID ");

            SqlParameter[] parameters = {
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@strFieldName", model.strFieldName),
          new SqlParameter("@nStepData", model.nStepData),
          new SqlParameter("@dtStepData", model.dtStepData),
          new SqlParameter("@strStepData", model.strStepData)};

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        public bool Delete(string nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Plan_Beginwork_StepData ");
            strSql.Append(" where nID = @nID ");
            SqlParameter[] parameters = {
          new SqlParameter("nID",nID)};

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion



        public DataTable GetStepDatas(string strTrainplanGUID)
        {
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select  * ");
            strSql2.Append(" FROM TAB_Plan_Beginwork_StepData where 1=1  and strTrainPlanGUID='" + strTrainplanGUID + "'");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()).Tables[0];
        }




        public LCBeginworkFlow.Beginwork_Flow GetStepData(string strTrainplanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM TAB_Plan_Beginwork_Flow where 1=1  and strTrainPlanGUID='" + strTrainplanGUID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select  * ");
            strSql2.Append(" FROM TAB_Plan_Beginwork_StepData where 1=1  and strTrainPlanGUID='" + strTrainplanGUID + "'");
            DataTable dtStepData = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()).Tables[0];
            LCBeginworkFlow.Beginwork_Flow bf = new LCBeginworkFlow.Beginwork_Flow();


            if (dt.Rows.Count > 0)
            {
                bf.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainPlanGUID"]);
                bf.strUserName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strUserName"]);
                bf.strUserNumber = ObjectConvertClass.static_ext_string(dt.Rows[0]["strUserNumber"]);
                bf.strConfirmBrief = ObjectConvertClass.static_ext_string(dt.Rows[0]["strConfirmBrief"]);
                bf.nFlowState = ObjectConvertClass.static_ext_int(dt.Rows[0]["nFlowState"]);
                bf.dtCreateTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtCreateTime"]);
                bf.dtBeginTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtBeginTime"]);
                bf.dtEndTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtEndTime"]);
            }
            StepDrink SD = new StepDrink();
            StepCheckCard Scc = new StepCheckCard();

            for (int i = 0; i < dtStepData.Rows.Count; i++)
            {
                string strStepName = ObjectConvertClass.static_ext_string(dtStepData.Rows[i]["strStepName"]);
                string strFieldName = ObjectConvertClass.static_ext_string(dtStepData.Rows[i]["strFieldName"]);

                if (strStepName == "DrinkTest")
                {
                    if (strFieldName == "nDrinkResult")
                        SD.nDrinkResult = ObjectConvertClass.static_ext_int(dtStepData.Rows[i]["nStepData"]);
                    if (strFieldName == "strDrinkResult")
                        SD.strDrinkResult = ObjectConvertClass.static_ext_string(dtStepData.Rows[i]["strStepData"]);
                   
                }
                else if (strStepName == "WriteCard")
                {
                    if (strFieldName == "nWriteCard")
                        Scc.CheckResult = ObjectConvertClass.static_ext_string(dtStepData.Rows[i]["nStepData"]);
                    if (strFieldName == "strWriteCard")
                        Scc.CreateTime = DateTime.Now;
                }
            }
            bf.Steps.Add(SD);
            bf.Steps.Add(Scc);
            return bf;
        }

        public class StepDrink
        {
            public string stepName = "测酒步骤";
            public string stepID = "cejiu";
            public int nDrinkResult;
            public string strDrinkResult;
        }

        public class StepCheckCard
        {
            public string stepName = "验卡步骤";
            public string stepID = "yanka";
            public string CheckResult;
            public DateTime CreateTime;
        }




        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        public bool Exists(Plan_Beginwork_StepData _Plan_Beginwork_StepData)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from TAB_Plan_Beginwork_StepData where nID=@nID");
            SqlParameter[] parameters = {
           new SqlParameter("nID",_Plan_Beginwork_StepData.nID)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
        }
        /// <summary>
        /// 获得数据DataTable
        /// </summary>
        public DataTable GetDataTable(Plan_Beginwork_StepDataQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            if (QueryCondition.page == 0)
            {
                strSql.Append("select * ");
                strSql.Append(" FROM TAB_Plan_Beginwork_StepData where 1=1 " + strSqlOption.ToString());
            }
            else
            {
                strSql.Append(@"select top " + QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_StepData where 1 = 1 " +
                strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
                " nID from TAB_Plan_Beginwork_StepData where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
        }
        /// <summary>
        /// 获得数据List
        /// </summary>
        public List<Plan_Beginwork_StepData> GetDataList(string strTrainmanNumber, string dtBeginStartTime, string dtEndStartTime)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select top 1 strTrainPlanGUID ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepIndex where 1=1 ");

            if (!string.IsNullOrEmpty(strTrainmanNumber))
                strSql.Append(" and strTrainmanNumber='" + strTrainmanNumber + "'");
            if (!string.IsNullOrEmpty(dtBeginStartTime))
                strSql.Append(" and dtStartTime>='" + dtBeginStartTime + "'");
            if (!string.IsNullOrEmpty(dtEndStartTime))
                strSql.Append(" and dtStartTime<'" + dtEndStartTime + "'");

        
            object TrainPlanGUID = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            string strTrainPlanGUID = "";
            if (TrainPlanGUID != null)
                strTrainPlanGUID = TrainPlanGUID.ToString();
            else
                return null;

            string strSqlData = "select * from  TAB_Plan_Beginwork_StepData where strTrainPlanGUID='" + strTrainPlanGUID + "' ";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlData.ToString()).Tables[0];

            List<Plan_Beginwork_StepData> list = new List<Plan_Beginwork_StepData>();

            foreach (DataRow dr in dt.Rows)
            {
                Plan_Beginwork_StepData _Plan_Beginwork_StepData = new Plan_Beginwork_StepData();
                DataRowToModel(_Plan_Beginwork_StepData, dr);
                list.Add(_Plan_Beginwork_StepData);
            }
            return list;
        }
        /// <summary>
        /// 获得记录总数
        /// </summary>
        public int GetDataCount(Plan_Beginwork_StepDataQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepData where 1=1" + strSqlOption.ToString());
            return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams));
        }
        /// <summary>
        /// 获得一个实体对象
        /// </summary>
        public Plan_Beginwork_StepData GetModel(Plan_Beginwork_StepDataQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM TAB_Plan_Beginwork_StepData where 1=1 " + strSqlOption.ToString());
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            Plan_Beginwork_StepData _Plan_Beginwork_StepData = null;
            if (dt.Rows.Count > 0)
            {
                _Plan_Beginwork_StepData = new Plan_Beginwork_StepData();
                DataRowToModel(_Plan_Beginwork_StepData, dt.Rows[0]);
            }
            return _Plan_Beginwork_StepData;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void DataRowToModel(Plan_Beginwork_StepData model, DataRow dr)
        {
            model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
            model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
            model.strFieldName = ObjectConvertClass.static_ext_string(dr["strFieldName"]);
            model.nStepData = ObjectConvertClass.static_ext_int(dr["nStepData"]);
            model.dtStepData = ObjectConvertClass.static_ext_date(dr["dtStepData"]);
            model.strStepData = ObjectConvertClass.static_ext_string(dr["strStepData"]);
        }
    }
}
