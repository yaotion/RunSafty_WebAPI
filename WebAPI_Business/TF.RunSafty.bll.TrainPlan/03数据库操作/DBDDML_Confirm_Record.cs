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


namespace TF.RunSafty.DDML
{
	/// <summary>
	///类名: DDML_Confirm_RecordQueryCondition
	///说明: 调令审核记录查询条件类
	/// </summary>
	public class DDML_Confirm_RecordQueryCondition
	{
		public int page = 0;
		public int rows = 0;
		//
		public int? nID;
		//车间GUID
		public string strWorkShopGUID = "";
		//车间编号
		public string strWorkShopNumber = "";
		//车间名称
		public string strWorkShopName = "";
		//审核时间
		public DateTime? dtConfirmTime;
		//值班员工号
        public string strDutyUserNumber = "";
		//值班员姓名
		public string strDutyUserName = "";
		public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
		{
			SqlCondition = new StringBuilder();
			SqlCondition.Append(nID != null ? " and nID = @nID" : "");
			SqlCondition.Append(strWorkShopGUID != "" ? " and strWorkShopGUID = @strWorkShopGUID" : "");
			SqlCondition.Append(strWorkShopNumber != "" ? " and strWorkShopNumber = @strWorkShopNumber" : "");
			SqlCondition.Append(strWorkShopName != "" ? " and strWorkShopName = @strWorkShopName" : "");
			SqlCondition.Append(dtConfirmTime != null ? " and dtConfirmTime = @dtConfirmTime" : "");
			SqlCondition.Append(strDutyUserNumber != null ? " and strDutyUserNumber = @strDutyUserNumber" : "");
			SqlCondition.Append(strDutyUserName != "" ? " and strDutyUserName = @strDutyUserName" : "");
			SqlParameter[] sqlParams ={
					new SqlParameter("nID",nID),
					new SqlParameter("strWorkShopGUID",strWorkShopGUID),
					new SqlParameter("strWorkShopNumber",strWorkShopNumber),
					new SqlParameter("strWorkShopName",strWorkShopName),
					new SqlParameter("dtConfirmTime",dtConfirmTime),
					new SqlParameter("strDutyUserNumber",strDutyUserNumber),
					new SqlParameter("strDutyUserName",strDutyUserName)};
			Params = sqlParams;
		}
	}
	/// <summary>
	///类名: DBDDML_Confirm_Record
	///说明: 调令审核记录数据操作类
	/// </summary>
	public class DBDDML_Confirm_Record : DBOperator
	{
		public DBDDML_Confirm_Record(string ConnectionString) : base(ConnectionString) { }
		#region 增删改
		/// <summary>
		/// 添加数据
		/// </summary>
		public bool Add(DDML_Confirm model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into TAB_DDML_Confirm_Record");
			strSql.Append("(nID,strWorkShopGUID,strWorkShopNumber,strWorkShopName,dtConfirmTime,strDutyUserNumber,strDutyUserName)");
			strSql.Append("values(@nID,@strWorkShopGUID,@strWorkShopNumber,@strWorkShopName,@dtConfirmTime,@strDutyUserNumber,@strDutyUserName)");
			SqlParameter[] parameters = {
					new SqlParameter("@nID", model.nID),
					new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID),
					new SqlParameter("@strWorkShopNumber", model.strWorkShopNumber),
					new SqlParameter("@strWorkShopName", model.strWorkShopName),
					new SqlParameter("@dtConfirmTime", model.dtConfirmTime),
					new SqlParameter("@strDutyUserNumber", model.strDutyUserNumber),
					new SqlParameter("@strDutyUserName", model.strDutyUserName)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) >0;
		}
		/// <summary>
		/// 更新数据
		/// </summary>
		public bool Update(DDML_Confirm model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("Update TAB_DDML_Confirm_Record set ");
			strSql.Append(" nID = @nID, ");
			strSql.Append(" strWorkShopGUID = @strWorkShopGUID, ");
			strSql.Append(" strWorkShopNumber = @strWorkShopNumber, ");
			strSql.Append(" strWorkShopName = @strWorkShopName, ");
			strSql.Append(" dtConfirmTime = @dtConfirmTime, ");
			strSql.Append(" strDutyUserNumber = @strDutyUserNumber, ");
			strSql.Append(" strDutyUserName = @strDutyUserName ");
			strSql.Append(" where strID = @strID ");

			SqlParameter[] parameters = {
					new SqlParameter("@nID", model.nID),
					new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID),
					new SqlParameter("@strWorkShopNumber", model.strWorkShopNumber),
					new SqlParameter("@strWorkShopName", model.strWorkShopName),
					new SqlParameter("@dtConfirmTime", model.dtConfirmTime),
					new SqlParameter("@strDutyUserNumber", model.strDutyUserNumber),
					new SqlParameter("@strDutyUserName", model.strDutyUserName)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		/// <summary>
		/// 删除数据
		/// </summary>
        public bool Delete(string strGUID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from TAB_DDML_Confirm_Record ");
			strSql.Append(" where strGUID = @strGUID ");
			SqlParameter[] parameters = {
					new SqlParameter("strGUID",strGUID)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		#endregion
		/// <summary>
		/// 检查数据是否存在
		/// </summary>
		public bool Exists(DDML_Confirm _DDML_Confirm)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) from TAB_DDML_Confirm_Record where strGUID=@strGUID");
			 SqlParameter[] parameters = {
					 new SqlParameter("strGUID",_DDML_Confirm.strGUID)};

			return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
		}
		/// <summary>
		/// 获得数据DataTable
		/// </summary>
		public DataTable GetDataTable(DDML_Confirm_RecordQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM TAB_DDML_Confirm_Record where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_DDML_Confirm_Record where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from TAB_DDML_Confirm_Record where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
		}
		/// <summary>
		/// 获得数据List
		/// </summary>
		public DDML_ConfirmList GetDataList(DDML_Confirm_RecordQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM TAB_DDML_Confirm_Record where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_DDML_Confirm_Record where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from TAB_DDML_Confirm_Record where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            DDML_ConfirmList list = new DDML_ConfirmList();
			foreach (DataRow dr in dt.Rows)
			{
                DDML_Confirm _DDML_Confirm = new DDML_Confirm();
                DataRowToModel(_DDML_Confirm, dr);
                list.Add(_DDML_Confirm);
			}
			return list;
		}
		/// <summary>
		/// 获得记录总数
		/// </summary>
		public int GetDataCount(DDML_Confirm_RecordQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) ");
			strSql.Append(" FROM TAB_DDML_Confirm_Record where 1=1" + strSqlOption.ToString());
			return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
		}
		/// <summary>
		/// 获得一个实体对象
		/// </summary>
		public DDML_Confirm GetModel(DDML_Confirm_RecordQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 * ");
			strSql.Append(" FROM TAB_DDML_Confirm_Record where 1=1 " + strSqlOption.ToString());
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            DDML_Confirm _DDML_Confirm = null;
			if (dt.Rows.Count > 0)
			{
                _DDML_Confirm = new DDML_Confirm();
                DataRowToModel(_DDML_Confirm, dt.Rows[0]);
			}
            return _DDML_Confirm;
		}
		/// <summary>
		/// 读取DataRow数据到Model中
		/// </summary>
		private void DataRowToModel(DDML_Confirm model,DataRow dr)
		{
			model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
			model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
			model.strWorkShopNumber = ObjectConvertClass.static_ext_string(dr["strWorkShopNumber"]);
			model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
			model.dtConfirmTime = ObjectConvertClass.static_ext_date(dr["dtConfirmTime"]);
            model.strDutyUserNumber = ObjectConvertClass.static_ext_string(dr["strDutyUserNumber"]);
			model.strDutyUserName = ObjectConvertClass.static_ext_string(dr["strDutyUserName"]);
		}
	}
}
