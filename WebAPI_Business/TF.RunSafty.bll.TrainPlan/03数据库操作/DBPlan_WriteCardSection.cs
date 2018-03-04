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
	///类名: Plan_WriteCardSectionQueryCondition
	///说明: 行车计划对应的调令写卡区段查询条件类
	/// </summary>
	public class PlanWriteCardSectionQueryCondition
	{
		public int page = 0;
		public int rows = 0;
		//
		public int? nID = null;
		//行车计划GUID
		public string strTrainPlanGUID = "";
		//写卡区段编号
		public string strSectionID = "";
		//写卡区段名称
		public string strSectionName = "";
		//机务段号
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
	///类名: DBPlan_WriteCardSection
	///说明: 行车计划对应的调令写卡区段数据操作类
	/// </summary>
	public class DBPlan_WriteCardSection : DBOperator
	{
		public DBPlan_WriteCardSection(string ConnectionString) : base(ConnectionString) { }
		#region 增删改
		/// <summary>
		/// 添加数据
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
		/// 更新数据
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
		/// 删除数据
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
		/// 检查数据是否存在
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
		/// 获得数据DataTable
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
		/// 获得数据List
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
		/// 获得记录总数
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
		/// 获得一个实体对象
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
		/// 读取DataRow数据到Model中
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
