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
using TF.RunSafty.Entry;

namespace TF.RunSafty.Plan
{
	/// <summary>
	///类名: Plan_RunEvent_ZXYJQueryCondition
	///说明: 在线预警信息查询条件类
	/// </summary>
    public class ZXYJQueryCondition
	{
		public int page =0;
		public int rows = 0;
		//
        public int nid = 0;
		//计划guid
		public string strTrainPlanGUID = "";
		//工号1
		public string strTrainmanNumber1 = "";
		//工号2
		public string strTrainmanNumber2 = "";
		//事件发生事件
		public DateTime? dtEventTime;
		//项点名称
		public string strXDMC = "";
		//车次
		public string strTrainNo = "";
		//车号
		public string strTrainNumber = "";
		//车型
		public string strTrainTypeName = "";
		//段号
		public string strDWH = "";
		//创建事件
		public DateTime? dtCreateTime;
		//tmis号
		public string nTMIS = "";
		//tmis名称
		public string strTMISName = "";
		public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
		{
			SqlCondition = new StringBuilder();
			SqlCondition.Append(nid != null ? " and nid = @nid" : "");
			SqlCondition.Append(strTrainPlanGUID != "" ? " and strTrainPlanGUID = @strTrainPlanGUID" : "");
			SqlCondition.Append(strTrainmanNumber1 != "" ? " and strTrainmanNumber1 = @strTrainmanNumber1" : "");
			SqlCondition.Append(strTrainmanNumber2 != "" ? " and strTrainmanNumber2 = @strTrainmanNumber2" : "");
			SqlCondition.Append(dtEventTime != null ? " and dtEventTime = @dtEventTime" : "");
			SqlCondition.Append(strXDMC != "" ? " and strXDMC = @strXDMC" : "");
			SqlCondition.Append(strTrainNo != "" ? " and strTrainNo = @strTrainNo" : "");
			SqlCondition.Append(strTrainNumber != "" ? " and strTrainNumber = @strTrainNumber" : "");
			SqlCondition.Append(strTrainTypeName != "" ? " and strTrainTypeName = @strTrainTypeName" : "");
			SqlCondition.Append(strDWH != "" ? " and strDWH = @strDWH" : "");
			SqlCondition.Append(dtCreateTime != null ? " and dtCreateTime = @dtCreateTime" : "");
			SqlCondition.Append(nTMIS != "" ? " and nTMIS = @nTMIS" : "");
			SqlCondition.Append(strTMISName != "" ? " and strTMISName = @strTMISName" : "");
			SqlParameter[] sqlParams ={
					new SqlParameter("nid",nid),
					new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
					new SqlParameter("strTrainmanNumber1",strTrainmanNumber1),
					new SqlParameter("strTrainmanNumber2",strTrainmanNumber2),
					new SqlParameter("dtEventTime",dtEventTime),
					new SqlParameter("strXDMC",strXDMC),
					new SqlParameter("strTrainNo",strTrainNo),
					new SqlParameter("strTrainNumber",strTrainNumber),
					new SqlParameter("strTrainTypeName",strTrainTypeName),
					new SqlParameter("strDWH",strDWH),
					new SqlParameter("dtCreateTime",dtCreateTime),
					new SqlParameter("nTMIS",nTMIS),
					new SqlParameter("strTMISName",strTMISName)};
			Params = sqlParams;
		}
	}
	/// <summary>
	///类名: DBPlan_RunEvent_ZXYJ
	///说明: 在线预警信息数据操作类
	/// </summary>
	public class DBPlan_RunEvent_ZXYJ
	{
		#region 增删改
		/// <summary>
		/// 添加数据
		/// </summary>
		public bool Add(ZXYJInfo model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into TAB_Plan_RunEvent_ZXYJ");
			strSql.Append("(nid,strTrainPlanGUID,strTrainmanNumber1,strTrainmanNumber2,dtEventTime,strXDMC,strTrainNo,strTrainNumber,strTrainTypeName,strDWH,dtCreateTime,nTMIS,strTMISName)");
			strSql.Append("values(@nid,@strTrainPlanGUID,@strTrainmanNumber1,@strTrainmanNumber2,@dtEventTime,@strXDMC,@strTrainNo,@strTrainNumber,@strTrainTypeName,@strDWH,@dtCreateTime,@nTMIS,@strTMISName)");
			SqlParameter[] parameters = {
					new SqlParameter("@nid", model.nid),
					new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
					new SqlParameter("@strTrainmanNumber1", model.strTrainmanNumber1),
					new SqlParameter("@strTrainmanNumber2", model.strTrainmanNumber2),
					new SqlParameter("@dtEventTime", model.dtEventTime),
					new SqlParameter("@strXDMC", model.strXDMC),
					new SqlParameter("@strTrainNo", model.strTrainNo),
					new SqlParameter("@strTrainNumber", model.strTrainNumber),
					new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
					new SqlParameter("@strDWH", model.strDWH),
					new SqlParameter("@dtCreateTime", model.dtCreateTime),
					new SqlParameter("@nTMIS", model.nTMIS),
					new SqlParameter("@strTMISName", model.strTMISName)};

			return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) >0;
		}
		/// <summary>
		/// 更新数据
		/// </summary>
        public bool Update(ZXYJInfo model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("Update TAB_Plan_RunEvent_ZXYJ set ");
			strSql.Append(" nid = @nid, ");
			strSql.Append(" strTrainPlanGUID = @strTrainPlanGUID, ");
			strSql.Append(" strTrainmanNumber1 = @strTrainmanNumber1, ");
			strSql.Append(" strTrainmanNumber2 = @strTrainmanNumber2, ");
			strSql.Append(" dtEventTime = @dtEventTime, ");
			strSql.Append(" strXDMC = @strXDMC, ");
			strSql.Append(" strTrainNo = @strTrainNo, ");
			strSql.Append(" strTrainNumber = @strTrainNumber, ");
			strSql.Append(" strTrainTypeName = @strTrainTypeName, ");
			strSql.Append(" strDWH = @strDWH, ");
			strSql.Append(" dtCreateTime = @dtCreateTime, ");
			strSql.Append(" nTMIS = @nTMIS, ");
			strSql.Append(" strTMISName = @strTMISName ");
			strSql.Append(" where nid = @nid ");

			SqlParameter[] parameters = {
					new SqlParameter("@nid", model.nid),
					new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
					new SqlParameter("@strTrainmanNumber1", model.strTrainmanNumber1),
					new SqlParameter("@strTrainmanNumber2", model.strTrainmanNumber2),
					new SqlParameter("@dtEventTime", model.dtEventTime),
					new SqlParameter("@strXDMC", model.strXDMC),
					new SqlParameter("@strTrainNo", model.strTrainNo),
					new SqlParameter("@strTrainNumber", model.strTrainNumber),
					new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
					new SqlParameter("@strDWH", model.strDWH),
					new SqlParameter("@dtCreateTime", model.dtCreateTime),
					new SqlParameter("@nTMIS", model.nTMIS),
					new SqlParameter("@strTMISName", model.strTMISName)};

			return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		/// <summary>
		/// 删除数据
		/// </summary>
		public bool Delete(string nid)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from TAB_Plan_RunEvent_ZXYJ ");
			strSql.Append(" where nid = @nid ");
			SqlParameter[] parameters = {
					new SqlParameter("nid",nid)};

			return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		#endregion
		/// <summary>
		/// 检查数据是否存在
		/// </summary>
        public bool Exists(ZXYJInfo _ZXYJInfo)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) from TAB_Plan_RunEvent_ZXYJ where nid=@nid");
			 SqlParameter[] parameters = {
					 new SqlParameter("nid",_ZXYJInfo.nid)};

			return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
		}
		/// <summary>
		/// 获得数据DataTable
		/// </summary>
		public DataTable GetDataTable(ZXYJQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM TAB_Plan_RunEvent_ZXYJ where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_RunEvent_ZXYJ where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from TAB_Plan_RunEvent_ZXYJ where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
		}
		/// <summary>
		/// 获得数据List
		/// </summary>
		public ZXYJInfoList GetDataList(ZXYJQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM TAB_Plan_RunEvent_ZXYJ where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_RunEvent_ZXYJ where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from TAB_Plan_RunEvent_ZXYJ where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            ZXYJInfoList list = new ZXYJInfoList();
			foreach (DataRow dr in dt.Rows)
			{
                ZXYJInfo ZXYJ = new ZXYJInfo();
                DataRowToModel(ZXYJ, dr);
                list.Add(ZXYJ);
			}
			return list;
		}
		/// <summary>
		/// 获得记录总数
		/// </summary>
		public int GetDataCount(ZXYJQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) ");
			strSql.Append(" FROM TAB_Plan_RunEvent_ZXYJ where 1=1" + strSqlOption.ToString());
			return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams));
		}
		/// <summary>
		/// 获得一个实体对象
		/// </summary>
		public ZXYJInfo GetModel(ZXYJQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 * ");
			strSql.Append(" FROM TAB_Plan_RunEvent_ZXYJ where 1=1 " + strSqlOption.ToString());
			DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            ZXYJInfo _ZXYJ = null;
			if (dt.Rows.Count > 0)
			{
                _ZXYJ = new ZXYJInfo();
                DataRowToModel(_ZXYJ, dt.Rows[0]);
			}
            return _ZXYJ;
		}
		/// <summary>
		/// 读取DataRow数据到Model中
		/// </summary>
		private void DataRowToModel(ZXYJInfo model,DataRow dr)
		{
			model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
			model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
			model.strTrainmanNumber1 = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber1"]);
			model.strTrainmanNumber2 = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber2"]);
			model.dtEventTime = ObjectConvertClass.static_ext_Date(dr["dtEventTime"]);
			model.strXDMC = ObjectConvertClass.static_ext_string(dr["strXDMC"]);
			model.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
			model.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
			model.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
			model.strDWH = ObjectConvertClass.static_ext_string(dr["strDWH"]);
			model.dtCreateTime = ObjectConvertClass.static_ext_Date(dr["dtCreateTime"]);
			model.nTMIS = ObjectConvertClass.static_ext_string(dr["nTMIS"]);
			model.strTMISName = ObjectConvertClass.static_ext_string(dr["strTMISName"]);
		}
	}
}
