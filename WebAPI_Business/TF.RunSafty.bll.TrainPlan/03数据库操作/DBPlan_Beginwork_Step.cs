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

namespace TF.RunSafty.Plan
{
	/// <summary>
	///类名: Plan_Beginwork_StepQueryCondition
	///说明: 出勤步骤查询条件类
	/// </summary>
	public class Plan_Beginwork_StepQueryCondition
	{
		public int page = 0;
		public int rows = 0;
		//计划guid
		public string strTrainPlanGUID = "";
		//步骤编号
		public int? nStepID = null;
		//人员guid
		public string strTrainmanGUID = "";
		//人员工号
		public string strTrainmanNumber = "";
		//人员姓名
		public string strTrainmanName = "";
		//步骤执行结果
		public int? nStepResultID = null;
		//步骤结果描述
		public string strStepResultText = "";
		//记录创建时间
		public DateTime? dtCreateTime;
		//事件发生时间
		public DateTime? dtEventTime;
        //eventEndTime
        public DateTime? dtEventEndTime;
        //记录自增ID
        public int? nID;
		public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
		{
			SqlCondition = new StringBuilder();
			SqlCondition.Append(strTrainPlanGUID != "" ? " and strTrainPlanGUID = @strTrainPlanGUID" : "");
			SqlCondition.Append(nStepID != null ? " and nStepID = @nStepID" : "");
			SqlCondition.Append(strTrainmanGUID != "" ? " and strTrainmanGUID = @strTrainmanGUID" : "");
			SqlCondition.Append(strTrainmanNumber != "" ? " and strTrainmanNumber = @strTrainmanNumber" : "");
			SqlCondition.Append(strTrainmanName != "" ? " and strTrainmanName = @strTrainmanName" : "");
			SqlCondition.Append(nStepResultID != null ? " and nStepResultID = @nStepResultID" : "");
			SqlCondition.Append(strStepResultText != "" ? " and strStepResultText = @strStepResultText" : "");
			SqlCondition.Append(dtCreateTime != null ? " and dtCreateTime = @dtCreateTime" : "");
			SqlCondition.Append(dtEventTime != null ? " and dtEventTime = @dtEventTime" : "");
            SqlCondition.Append(dtEventEndTime != null ? " and dtEventEndTime = @dtEventEndTime" : "");
            SqlCondition.Append(nID != null ? " and nID = @nID" : "");
			SqlParameter[] sqlParams ={
					new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
					new SqlParameter("nStepID",nStepID),
					new SqlParameter("strTrainmanGUID",strTrainmanGUID),
					new SqlParameter("strTrainmanNumber",strTrainmanNumber),
					new SqlParameter("strTrainmanName",strTrainmanName),
					new SqlParameter("nStepResultID",nStepResultID),
					new SqlParameter("strStepResultText",strStepResultText),
					new SqlParameter("dtCreateTime",dtCreateTime),
					new SqlParameter("dtEventTime",dtEventTime),
                    new SqlParameter("dtEventEndTime",dtEventEndTime),
                    new SqlParameter("nID",nID) };
			Params = sqlParams;
		}
	}
	/// <summary>
	///类名: DBPlan_Beginwork_Step
	///说明: 出勤步骤数据操作类
	/// </summary>
	public class DBPlan_Beginwork_Step
	{		

		#region 增删改 

        /// <summary>
        /// 添加数据
        /// </summary>
        public bool AddList(List<Plan_Beginwork_Step> modelList)
        {
            bool result = true;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.ConnectionString = SqlHelper.ConnString;

            connection.Open();
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            Plan_Beginwork_StepQueryCondition QueryCondition = new Plan_Beginwork_StepQueryCondition();
            try
            {
                foreach (Plan_Beginwork_Step model in modelList)
                {
                    QueryCondition.strTrainPlanGUID = model.strTrainPlanGUID;
                    QueryCondition.strTrainmanGUID = model.strTrainmanGUID;
                    QueryCondition.nStepID = model.nStepID;
                    Plan_Beginwork_Step tempModel = GetModel(model.strTrainPlanGUID, model.strTrainmanGUID, model.nStepID, command);
                   if (tempModel == null)
                   {
                       Add(model, command);
                   }
                   else
                   {
                       model.nID = tempModel.nID;
                       Update(model, command);
                   }

                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                result = false;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                command.Dispose();
                connection.Dispose();
            }
            return result;            
        }


		/// <summary>
		/// 添加数据
		/// </summary>
        public void Add(Plan_Beginwork_Step model, SqlCommand command)
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
            command.CommandText = NewSql.ToString();
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();

		}

        public void Add(Plan_Beginwork_Step model)
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

        public void Add(Plan_Beginwork_Step model,SqlTransaction trans)
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

            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, NewSql.ToString());

        }




		/// <summary>
		/// 更新数据
		/// </summary>
		public void Update(Plan_Beginwork_Step model,SqlCommand command)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("Update TAB_Plan_Beginwork_Step set ");
			strSql.Append(" strTrainPlanGUID = '{0}', ");
            strSql.Append(" nStepID = {1}, ");
            strSql.Append(" strTrainmanGUID = '{2}', ");
            strSql.Append(" strTrainmanNumber = '{3}', ");
            strSql.Append(" strTrainmanName = '{4}', ");
            strSql.Append(" nStepResultID = {5}, ");
            strSql.Append(" strStepResultText = '{6}', ");
            strSql.Append(" dtCreateTime =getdate(), ");
            strSql.Append(" dtEventTime = '{7}' ");
            strSql.Append(" dtEventEndTime = '{8}' ");
            strSql.Append(" where nID = {9} ");


            string NewSql = string.Format(strSql.ToString(),
                model.strTrainPlanGUID,
                model.nStepID,
                model.strTrainmanGUID,
                model.strTrainmanNumber,
                model.strTrainmanName,
                model.nStepResultID,
                model.strStepResultText,
                model.dtEventTime,
                model.dtEventEndTime,
                model.nID);
            command.CommandText = NewSql.ToString();
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
			
		}
		/// <summary>
		/// 删除数据
		/// </summary>
		public bool Delete(string strID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from TAB_Plan_Beginwork_Step ");
			strSql.Append(" where strID = @strID ");
			SqlParameter[] parameters = {
					new SqlParameter("strID",strID)};

			return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		#endregion
		/// <summary>
		/// 检查数据是否存在
		/// </summary>
		public bool Exists(Plan_Beginwork_Step _Plan_Beginwork_Step)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) from TAB_Plan_Beginwork_Step where strTrainPlanGUID=@strTrainPlanGUID an nStepID = @nStepID and strTrainmanGUID = @strTrainmanGUID");
			 SqlParameter[] parameters = {
					 new SqlParameter("strTrainPlanGUID",_Plan_Beginwork_Step.strTrainPlanGUID),
                     new SqlParameter("nStepID",_Plan_Beginwork_Step.nStepID),
                     new SqlParameter("strTrainmanGUID",_Plan_Beginwork_Step.strTrainmanGUID)
                                         };

			return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
		}
		/// <summary>
		/// 获得数据DataTable
		/// </summary>
		public DataTable GetDataTable(Plan_Beginwork_StepQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM TAB_Plan_Beginwork_Step where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_Step where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from TAB_Plan_Beginwork_Step where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
		}
		/// <summary>
		/// 获得数据List
		/// </summary>
		public Plan_Beginwork_StepList GetDataList(Plan_Beginwork_StepQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM TAB_Plan_Beginwork_Step where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_Step where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from TAB_Plan_Beginwork_Step where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            Plan_Beginwork_StepList list = null;
            if (dt.Rows.Count > 0)
            {
                list = new Plan_Beginwork_StepList();
                foreach (DataRow dr in dt.Rows)
                {
                    Plan_Beginwork_Step _Plan_Beginwork_Step = new Plan_Beginwork_Step();
                    DataRowToModel(_Plan_Beginwork_Step, dr);
                    list.Add(_Plan_Beginwork_Step);
                }
            }
			return list;
		}
		/// <summary>
		/// 获得记录总数
		/// </summary>
		public int GetDataCount(Plan_Beginwork_StepQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) ");
			strSql.Append(" FROM TAB_Plan_Beginwork_Step where 1=1" + strSqlOption.ToString());
			return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams));
		}
		/// <summary>
		/// 获得一个实体对象
		/// </summary>
        public Plan_Beginwork_Step GetModel(string strTrainPlanGUID,string strTrainmanGUID, int nStepID, SqlCommand command)
		{
			
			StringBuilder strSqlOption = new StringBuilder();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 * ");
            strSql.Append(" FROM TAB_Plan_Beginwork_Step where 1=1 and strTrainPlanGUID = '{0}' and strTrainmanGUID = '{1}' and nStepID = {2} ");
            string newSql = string.Format(strSql.ToString(), strTrainPlanGUID, strTrainmanGUID, nStepID);
			//DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            command.CommandText = newSql.ToString();
            command.CommandType = CommandType.Text;


            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            
			Plan_Beginwork_Step _Plan_Beginwork_Step = null;
			if (dt.Rows.Count > 0)
			{
				_Plan_Beginwork_Step = new Plan_Beginwork_Step();
				DataRowToModel(_Plan_Beginwork_Step,dt.Rows[0]);
			}
			return _Plan_Beginwork_Step;
		}
		/// <summary>
		/// 读取DataRow数据到Model中
		/// </summary>
		private void DataRowToModel(Plan_Beginwork_Step model,DataRow dr)
		{
			model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
			model.nStepID = ObjectConvertClass.static_ext_int(dr["nStepID"]);
			model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
			model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
			model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
			model.nStepResultID = ObjectConvertClass.static_ext_int(dr["nStepResultID"]);
			model.strStepResultText = ObjectConvertClass.static_ext_string(dr["strStepResultText"]);
			model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
			model.dtEventTime = ObjectConvertClass.static_ext_date(dr["dtEventTime"]);
            model.dtEventEndTime = ObjectConvertClass.static_ext_date(dr["dtEventEndTime"]);
            model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
		}
	}
}
