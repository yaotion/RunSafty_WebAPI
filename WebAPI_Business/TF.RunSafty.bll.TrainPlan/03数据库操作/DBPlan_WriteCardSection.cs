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
using TF.RunSafty.DBUtils;


namespace TF.RunSafty.ICCard
{
	/// <summary>
	///����: Plan_WriteCardSectionQueryCondition
	///˵��: �г��ƻ���Ӧ�ĵ���д�����β�ѯ������
	/// </summary>
	public class PlanWriteCardSectionQueryCondition
	{
		public int page = 0;
		public int rows = 0;
		//
		public int? nID = null;
		//�г��ƻ�GUID
		public string strTrainPlanGUID = "";
		//д�����α��
		public string strSectionID = "";
		//д����������
		public string strSectionName = "";
		//����κ�
		public string strJWDNumber = "";
		public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
		{
			SqlCondition = new StringBuilder();
			SqlCondition.Append(nID != null ? " and nID = @nID" : "");
			SqlCondition.Append(strTrainPlanGUID != "" ? " and strTrainPlanGUID = @strTrainPlanGUID" : "");
			SqlCondition.Append(strSectionID != "" ? " and strSectionID = @strSectionID" : "");
			SqlCondition.Append(strSectionName != "" ? " and strSectionName = @strSectionName" : "");
			SqlCondition.Append(strJWDNumber != "" ? " and strJWDNumber = @strJWDNumber" : "");
			SqlParameter[] sqlParams ={
					new SqlParameter("niD",nID),
					new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
					new SqlParameter("strSectionID",strSectionID),
					new SqlParameter("strSectionName",strSectionName),
					new SqlParameter("strJWDNumber",strJWDNumber)};
			Params = sqlParams;
		}
	}
	/// <summary>
	///����: DBPlan_WriteCardSection
	///˵��: �г��ƻ���Ӧ�ĵ���д���������ݲ�����
	/// </summary>
	public class DBPlan_WriteCardSection : DBOperator
	{
		public DBPlan_WriteCardSection(string ConnectionString) : base(ConnectionString) { }
		#region ��ɾ��
		/// <summary>
		/// �������
		/// </summary>
		public int Add(PlanWriteCardSection model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Tab_Plan_WriteCardSection");
			strSql.Append("(strTrainPlanGUID,strSectionID,strSectionName,strJWDNumber)");
			strSql.Append("values(@strTrainPlanGUID,@strSectionID,@strSectionName,@strJWDNumber)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
					new SqlParameter("@strSectionID", model.strSectionID),
					new SqlParameter("@strSectionName", model.strSectionName),
					new SqlParameter("@strJWDNumber", model.strJWDNumber)};

			return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters));
		}
		/// <summary>
		/// ��������
		/// </summary>
		public bool Update(PlanWriteCardSection model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("Update Tab_Plan_WriteCardSection set ");
			strSql.Append(" strTrainPlanGUID = @strTrainPlanGUID, ");
			strSql.Append(" strSectionID = @strSectionID, ");
			strSql.Append(" strSectionName = @strSectionName, ");
			strSql.Append(" strJWDNumber = @strJWDNumber ");
			strSql.Append(" where niD = @niD ");

			SqlParameter[] parameters = {
					new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
					new SqlParameter("@strSectionID", model.strSectionID),
					new SqlParameter("@strSectionName", model.strSectionName),
					new SqlParameter("@strJWDNumber", model.strJWDNumber)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		/// <summary>
		/// ɾ������
		/// </summary>
		public bool Delete(string niD)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Tab_Plan_WriteCardSection ");
			strSql.Append(" where niD = @niD ");
			SqlParameter[] parameters = {
					new SqlParameter("niD",niD)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		#endregion
		/// <summary>
		/// ��������Ƿ����
		/// </summary>
		public bool Exists(PlanWriteCardSection _PlanWriteCardSection)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) from Tab_PlanWriteCardSection where niD=@niD");
			 SqlParameter[] parameters = {
					 new SqlParameter("niD",_PlanWriteCardSection.nID)};

			return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
		}
		/// <summary>
		/// �������DataTable
		/// </summary>
		public DataTable GetDataTable(PlanWriteCardSectionQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM Tab_PlanWriteCardSection where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from Tab_PlanWriteCardSection where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from Tab_PlanWriteCardSection where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
		}
		/// <summary>
		/// �������List
		/// </summary>
		public PlanWriteCardSectionList GetDataList(PlanWriteCardSectionQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM Tab_Plan_WriteCardSection where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from Tab_Plan_WriteCardSection where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from Tab_Plan_WriteCardSection where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
			PlanWriteCardSectionList list = new PlanWriteCardSectionList();
			foreach (DataRow dr in dt.Rows)
			{
				PlanWriteCardSection _PlanWriteCardSection = new PlanWriteCardSection();
				DataRowToModel(_PlanWriteCardSection,dr);
				list.Add(_PlanWriteCardSection);
			}
			return list;
		}
		/// <summary>
		/// ��ü�¼����
		/// </summary>
		public int GetDataCount(PlanWriteCardSectionQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) ");
			strSql.Append(" FROM Tab_Plan_WriteCardSection where 1=1" + strSqlOption.ToString());
			return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
		}
		/// <summary>
		/// ���һ��ʵ�����
		/// </summary>
		public PlanWriteCardSection GetModel(PlanWriteCardSectionQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 * ");
			strSql.Append(" FROM Tab_Plan_WriteCardSection where 1=1 " + strSqlOption.ToString());
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
			PlanWriteCardSection _PlanWriteCardSection = null;
			if (dt.Rows.Count > 0)
			{
				_PlanWriteCardSection = new PlanWriteCardSection();
				DataRowToModel(_PlanWriteCardSection,dt.Rows[0]);
			}
			return _PlanWriteCardSection;
		}
		/// <summary>
		/// ��ȡDataRow���ݵ�Model��
		/// </summary>
		private void DataRowToModel(PlanWriteCardSection model,DataRow dr)
		{
			model.nID = ObjectConvertClass.static_ext_int(dr["niD"]);
			model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
			model.strSectionID = ObjectConvertClass.static_ext_string(dr["strSectionID"]);
			model.strSectionName = ObjectConvertClass.static_ext_string(dr["strSectionName"]);
            model.strJWDNumber = ObjectConvertClass.static_ext_string(dr["strJWDNumber"]);
		}
	}
}
