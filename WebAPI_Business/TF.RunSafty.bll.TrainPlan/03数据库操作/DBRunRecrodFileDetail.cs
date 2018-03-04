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
using TF.RunSafty.Plan.MD;

namespace TF.RunSafty.DBUtils
{
	/// <summary>
	///����: EndWork_RunRecrod_DetailQueryCondition
	///˵��: �������м�¼ת���Ӳ�ѯ������
	/// </summary>
	public class RunRecrodDetailQueryCondition
	{
		public int page = 0;
		public int rows = 0;
		//
		public int nID = 0;
		//���ڼƻ�GUID
		public string strPlanGUID = "";
		//�ļ�����
		public string strFileName = "";
		//������
		public string strTrainNum = "";
		//˾������
		public string strTrainmanNumber = "";
		//����
		public string strTrainNo = "";
		//�ļ���С
		public int? nFileSize = 0;
		//�ļ�ʱ��
		public DateTime? dtFileTime;
		public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
		{
			SqlCondition = new StringBuilder();
			SqlCondition.Append(nID != null ? " and nID = @nID" : "");
			SqlCondition.Append(strPlanGUID != "" ? " and strPlanGUID = @strPlanGUID" : "");
			SqlCondition.Append(strFileName != "" ? " and strFileName = @strFileName" : "");
			SqlCondition.Append(strTrainNum != "" ? " and strTrainNum = @strTrainNum" : "");
			SqlCondition.Append(strTrainmanNumber != "" ? " and strTrainmanNumber = @strTrainmanNumber" : "");
			SqlCondition.Append(strTrainNo != "" ? " and strTrainNo = @strTrainNo" : "");
			SqlCondition.Append(nFileSize != null ? " and nFileSize = @nFileSize" : "");
			SqlCondition.Append(dtFileTime != null ? " and dtFileTime = @dtFileTime" : "");
			SqlParameter[] sqlParams ={
					new SqlParameter("nID",nID),
					new SqlParameter("strPlanGUID",strPlanGUID),
					new SqlParameter("strFileName",strFileName),
					new SqlParameter("strTrainNum",strTrainNum),
					new SqlParameter("strTrainmanNumber",strTrainmanNumber),
					new SqlParameter("strTrainNo",strTrainNo),
					new SqlParameter("nFileSize",nFileSize),
					new SqlParameter("dtFileTime",dtFileTime)};
			Params = sqlParams;
		}
	}
	/// <summary>
	///����: DBEndWork_RunRecrod_Detail
	///˵��: �������м�¼ת�������ݲ�����
	/// </summary>
	public class DBRunRecrodDetail : DBOperator
	{
        public DBRunRecrodDetail(string ConnectionString) : base(ConnectionString) { }
		#region ��ɾ��
		/// <summary>
		/// �������
		/// </summary>
        public int Add(SqlCommand command , RunRecordFileDetail model)
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
                    new SqlParameter("@strRecordGUID", model.strRecordGUID)                    };
            command.CommandText = strSql.ToString();
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
			
		}
		
		#endregion
		/// <summary>
		/// ��������Ƿ����
		/// </summary>
        public bool Exists(RunRecordFileDetail _EndWork_RunRecrod_Detail)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) from Tab_EndWork_RunRecrod_Detail where strID=@strID");
			 SqlParameter[] parameters = {
					 new SqlParameter("strID",_EndWork_RunRecrod_Detail.nID)};

			return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
		}
		/// <summary>
		/// �������DataTable
		/// </summary>
        public DataTable GetDataTable(RunRecrodDetailQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM Tab_EndWork_RunRecrod_Detail where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from Tab_EndWork_RunRecrod_Detail where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from Tab_EndWork_RunRecrod_Detail where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
		}
		/// <summary>
		/// �������List
		/// </summary>
        public RunRecordFileDetailList GetDataList(RunRecrodDetailQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM Tab_EndWork_RunRecrod_Detail where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from Tab_EndWork_RunRecrod_Detail where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from Tab_EndWork_RunRecrod_Detail where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            RunRecordFileDetailList list = new RunRecordFileDetailList();
			foreach (DataRow dr in dt.Rows)
			{
                RunRecordFileDetail _EndWork_RunRecrod_Detail = new RunRecordFileDetail();
				DataRowToModel(_EndWork_RunRecrod_Detail,dr);
				list.Add(_EndWork_RunRecrod_Detail);
			}
			return list;
		}
		/// <summary>
		/// ��ü�¼����
		/// </summary>
        public int GetDataCount(RunRecrodDetailQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) ");
			strSql.Append(" FROM Tab_EndWork_RunRecrod_Detail where 1=1" + strSqlOption.ToString());
			return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
		}
		/// <summary>
		/// ���һ��ʵ�����
		/// </summary>
        public RunRecordFileDetail GetModel(RunRecrodDetailQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 * ");
			strSql.Append(" FROM Tab_EndWork_RunRecrod_Detail where 1=1 " + strSqlOption.ToString());
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            RunRecordFileDetail _EndWork_RunRecrod_Detail = null;
			if (dt.Rows.Count > 0)
			{
                _EndWork_RunRecrod_Detail = new RunRecordFileDetail();
				DataRowToModel(_EndWork_RunRecrod_Detail,dt.Rows[0]);
			}
			return _EndWork_RunRecrod_Detail;
		}
		/// <summary>
		/// ��ȡDataRow���ݵ�Model��
		/// </summary>
        private void DataRowToModel(RunRecordFileDetail model, DataRow dr)
		{
			model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
			model.strPlanGUID = ObjectConvertClass.static_ext_string(dr["strPlanGUID"]);
			model.strFileName = ObjectConvertClass.static_ext_string(dr["strFileName"]);
			model.strTrainNum = ObjectConvertClass.static_ext_string(dr["strTrainNum"]);
			model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
			model.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
			model.nFileSize = ObjectConvertClass.static_ext_int(dr["nFileSize"]);
			model.dtFileTime = ObjectConvertClass.static_ext_date(dr["dtFileTime"]);
		}
	}
}
