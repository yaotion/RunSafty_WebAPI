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

namespace TF.RunSafty.DBUtils
{
	/// <summary>
	//类名: Base_TrainNoQueryCondition
	//说明: 图定车次信息查询条件类
	/// <summary>
	public class Base_TrainNoQueryCondition
	{
		public int page = 0;
		public int rows = 0;
		//
		public string strGUID = "";
		//机车类型
		public string strTrainTypeName = "";
		//车号
		public string strTrainNumber = "";
		//车次 
		public string strTrainNo = "";
		//计划开车时间
		public DateTime? dtStartTime;
		//实际开车时间
		public DateTime? dtRealStartTime;
		//行车区段GUID
		public string strTrainJiaoluGUID = "";
		//开始站
		public string strStartStation = "";
		//结束站
		public string strEndStation = "";
		//乘务员类型ID
		public int? nTrainmanTypeID ;
		//计划类型ID
		public int? nPlanType ;
		//牵引类型ID
		public int? nDragType ;
		//客货类型ID
		public int? nKehuoID ;
		//备注类型
		public int? nRemarkType ;
		//备注
		public string strRemark = "";
		//创建时间
		public DateTime? dtCreateTime;
		//创建地点ID
		public string strCreateSiteGUID = "";
		//创建人ID
		public string strCreateUserGUID = "";
		//出勤点编号
		public string strPlaceID = "";
		//创建时间
		public DateTime? dtPlanStartTime;
		public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
		{
			SqlCondition = new StringBuilder();
			SqlCondition.Append(strGUID != "" ? " and strGUID = @strGUID" : "");
			SqlCondition.Append(strTrainTypeName != "" ? " and strTrainTypeName = @strTrainTypeName" : "");
			SqlCondition.Append(strTrainNumber != "" ? " and strTrainNumber = @strTrainNumber" : "");
			SqlCondition.Append(strTrainNo != "" ? " and strTrainNo = @strTrainNo" : "");
			SqlCondition.Append(dtStartTime != null ? " and dtStartTime = @dtStartTime" : "");
			SqlCondition.Append(dtRealStartTime != null ? " and dtRealStartTime = @dtRealStartTime" : "");
			SqlCondition.Append(strTrainJiaoluGUID != "" ? " and strTrainJiaoluGUID = @strTrainJiaoluGUID" : "");
			SqlCondition.Append(strStartStation != "" ? " and strStartStation = @strStartStation" : "");
			SqlCondition.Append(strEndStation != "" ? " and strEndStation = @strEndStation" : "");
			SqlCondition.Append(nTrainmanTypeID != null ? " and nTrainmanTypeID = @nTrainmanTypeID" : "");
			SqlCondition.Append(nPlanType != null ? " and nPlanType = @nPlanType" : "");
			SqlCondition.Append(nDragType != null ? " and nDragType = @nDragType" : "");
			SqlCondition.Append(nKehuoID != null ? " and nKehuoID = @nKehuoID" : "");
			SqlCondition.Append(nRemarkType != null ? " and nRemarkType = @nRemarkType" : "");
			SqlCondition.Append(strRemark != "" ? " and strRemark = @strRemark" : "");
			SqlCondition.Append(dtCreateTime != null ? " and dtCreateTime = @dtCreateTime" : "");
			SqlCondition.Append(strCreateSiteGUID != "" ? " and strCreateSiteGUID = @strCreateSiteGUID" : "");
			SqlCondition.Append(strCreateUserGUID != "" ? " and strCreateUserGUID = @strCreateUserGUID" : "");
			SqlCondition.Append(strPlaceID != "" ? " and strPlaceID = @strPlaceID" : "");
			SqlCondition.Append(dtPlanStartTime != null ? " and dtPlanStartTime = @dtPlanStartTime" : "");
			SqlParameter[] sqlParams ={
					new SqlParameter("strGUID",strGUID),
					new SqlParameter("strTrainTypeName",strTrainTypeName),
					new SqlParameter("strTrainNumber",strTrainNumber),
					new SqlParameter("strTrainNo",strTrainNo),
					new SqlParameter("dtStartTime",dtStartTime),
					new SqlParameter("dtRealStartTime",dtRealStartTime),
					new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
					new SqlParameter("strStartStation",strStartStation),
					new SqlParameter("strEndStation",strEndStation),
					new SqlParameter("nTrainmanTypeID",nTrainmanTypeID),
					new SqlParameter("nPlanType",nPlanType),
					new SqlParameter("nDragType",nDragType),
					new SqlParameter("nKehuoID",nKehuoID),
					new SqlParameter("nRemarkType",nRemarkType),
					new SqlParameter("strRemark",strRemark),
					new SqlParameter("dtCreateTime",dtCreateTime),
					new SqlParameter("strCreateSiteGUID",strCreateSiteGUID),
					new SqlParameter("strCreateUserGUID",strCreateUserGUID),
					new SqlParameter("strPlaceID",strPlaceID),
					new SqlParameter("dtPlanStartTime",dtPlanStartTime)};
			Params = sqlParams;
		}
	}
	/// <summary>
	//类名: DBBase_TrainNo
	//说明: 图定车次信息数据操作类
	/// <summary>
	public class DBBase_TrainNo : DBOperator
	{
		public DBBase_TrainNo(string ConnectionString) : base(ConnectionString) { }
		#region 增删改
		/// <summary>
		/// 添加数据
		/// <summary>
		public bool Add(Base_TrainNo model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into TAB_Base_TrainNo");
			strSql.Append("(strGUID,strTrainTypeName,strTrainNumber,strTrainNo,dtStartTime,dtRealStartTime,strTrainJiaoluGUID,strStartStation,strEndStation,nTrainmanTypeID,nPlanType,nDragType,nKehuoID,nRemarkType,strRemark,dtCreateTime,strCreateSiteGUID,strCreateUserGUID,strPlaceID,dtPlanStartTime)");
			strSql.Append("values(@strGUID,@strTrainTypeName,@strTrainNumber,@strTrainNo,@dtStartTime,@dtRealStartTime,@strTrainJiaoluGUID,@strStartStation,@strEndStation,@nTrainmanTypeID,@nPlanType,@nDragType,@nKehuoID,@nRemarkType,@strRemark,@dtCreateTime,@strCreateSiteGUID,@strCreateUserGUID,@strPlaceID,@dtPlanStartTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@strGUID", model.strGUID),
					new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
					new SqlParameter("@strTrainNumber", model.strTrainNumber),
					new SqlParameter("@strTrainNo", model.strTrainNo),
					new SqlParameter("@dtStartTime", model.dtStartTime),
					new SqlParameter("@dtRealStartTime", model.dtRealStartTime),
					new SqlParameter("@strTrainJiaoluGUID", model.strTrainJiaoluGUID),
					new SqlParameter("@strStartStation", model.strStartStation),
					new SqlParameter("@strEndStation", model.strEndStation),
					new SqlParameter("@nTrainmanTypeID", model.nTrainmanTypeID),
					new SqlParameter("@nPlanType", model.nPlanType),
					new SqlParameter("@nDragType", model.nDragType),
					new SqlParameter("@nKehuoID", model.nKehuoID),
					new SqlParameter("@nRemarkType", model.nRemarkType),
					new SqlParameter("@strRemark", model.strRemark),
					new SqlParameter("@dtCreateTime", model.dtCreateTime),
					new SqlParameter("@strCreateSiteGUID", model.strCreateSiteGUID),
					new SqlParameter("@strCreateUserGUID", model.strCreateUserGUID),
					new SqlParameter("@strPlaceID", model.strPlaceID),
					new SqlParameter("@dtPlanStartTime", model.dtPlanStartTime)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		/// <summary>
		/// 更新数据
		/// <summary>
		public bool Update(Base_TrainNo model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("Update TAB_Base_TrainNo set ");
			strSql.Append(" strGUID = @strGUID, ");
			strSql.Append(" strTrainTypeName = @strTrainTypeName, ");
			strSql.Append(" strTrainNumber = @strTrainNumber, ");
			strSql.Append(" strTrainNo = @strTrainNo, ");
			strSql.Append(" dtStartTime = @dtStartTime, ");
			strSql.Append(" dtRealStartTime = @dtRealStartTime, ");
			strSql.Append(" strTrainJiaoluGUID = @strTrainJiaoluGUID, ");
			strSql.Append(" strStartStation = @strStartStation, ");
			strSql.Append(" strEndStation = @strEndStation, ");
			strSql.Append(" nTrainmanTypeID = @nTrainmanTypeID, ");
			strSql.Append(" nPlanType = @nPlanType, ");
			strSql.Append(" nDragType = @nDragType, ");
			strSql.Append(" nKehuoID = @nKehuoID, ");
			strSql.Append(" nRemarkType = @nRemarkType, ");
			strSql.Append(" strRemark = @strRemark, ");
			strSql.Append(" dtCreateTime = @dtCreateTime, ");
			strSql.Append(" strCreateSiteGUID = @strCreateSiteGUID, ");
			strSql.Append(" strCreateUserGUID = @strCreateUserGUID, ");
			strSql.Append(" strPlaceID = @strPlaceID, ");
			strSql.Append(" dtPlanStartTime = @dtPlanStartTime ");
			strSql.Append(" where strGUID = @strGUID ");

			SqlParameter[] parameters = {
					new SqlParameter("@strGUID", model.strGUID),
					new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
					new SqlParameter("@strTrainNumber", model.strTrainNumber),
					new SqlParameter("@strTrainNo", model.strTrainNo),
					new SqlParameter("@dtStartTime", model.dtStartTime),
					new SqlParameter("@dtRealStartTime", model.dtRealStartTime),
					new SqlParameter("@strTrainJiaoluGUID", model.strTrainJiaoluGUID),
					new SqlParameter("@strStartStation", model.strStartStation),
					new SqlParameter("@strEndStation", model.strEndStation),
					new SqlParameter("@nTrainmanTypeID", model.nTrainmanTypeID),
					new SqlParameter("@nPlanType", model.nPlanType),
					new SqlParameter("@nDragType", model.nDragType),
					new SqlParameter("@nKehuoID", model.nKehuoID),
					new SqlParameter("@nRemarkType", model.nRemarkType),
					new SqlParameter("@strRemark", model.strRemark),
					new SqlParameter("@dtCreateTime", model.dtCreateTime),
					new SqlParameter("@strCreateSiteGUID", model.strCreateSiteGUID),
					new SqlParameter("@strCreateUserGUID", model.strCreateUserGUID),
					new SqlParameter("@strPlaceID", model.strPlaceID),
					new SqlParameter("@dtPlanStartTime", model.dtPlanStartTime)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		/// <summary>
		/// 删除数据
		/// <summary>
		public bool Delete(string strGUID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from TAB_Base_TrainNo ");
			strSql.Append(" where strGUID = @strGUID ");
			SqlParameter[] parameters = {
					new SqlParameter("strGUID",strGUID)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		#endregion
		/// <summary>
		/// 检查数据是否存在
		/// <summary>
		public bool Exists(Base_TrainNo _Base_TrainNo)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) from TAB_Base_TrainNo where strGUID=@strGUID");
			 SqlParameter[] parameters = {
					 new SqlParameter("strGUID",_Base_TrainNo.strGUID)};

			return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
		}
		/// <summary>
		/// 获得数据DataTable
		/// <summary>
		public DataTable GetDataTable(Base_TrainNoQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM TAB_Base_TrainNo where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Base_TrainNo where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from TAB_Base_TrainNo where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
		}
		/// <summary>
		/// 获得数据List
		/// <summary>
		public List<Base_TrainNo> GetDataList(Base_TrainNoQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM TAB_Base_TrainNo where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Base_TrainNo where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from TAB_Base_TrainNo where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
			List<Base_TrainNo> list = new List<Base_TrainNo>();
			foreach (DataRow dr in dt.Rows)
			{
				Base_TrainNo _Base_TrainNo = new Base_TrainNo();
				DataRowToModel(_Base_TrainNo,dr);
				list.Add(_Base_TrainNo);
			}
			return list;
		}
		/// <summary>
		/// 获得记录总数
		/// <summary>
		public int GetDataCount(Base_TrainNoQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) ");
			strSql.Append(" FROM TAB_Base_TrainNo where 1=1" + strSqlOption.ToString());
			return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
		}
		/// <summary>
		/// 获得一个实体对象
		/// <summary>
		public Base_TrainNo GetModel(Base_TrainNoQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 * ");
			strSql.Append(" FROM TAB_Base_TrainNo where 1=1 " + strSqlOption.ToString());
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
			Base_TrainNo _Base_TrainNo = null;
			if (dt.Rows.Count > 0)
			{
				_Base_TrainNo = new Base_TrainNo();
				DataRowToModel(_Base_TrainNo,dt.Rows[0]);
			}
			return _Base_TrainNo;
		}
		/// <summary>
		/// 读取DataRow数据到Model中
		/// <summary>
		private void DataRowToModel(Base_TrainNo model,DataRow dr)
		{
			model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
			model.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
			model.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
			model.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
			model.dtStartTime = ObjectConvertClass.static_ext_date(dr["dtStartTime"]);
			model.dtRealStartTime = ObjectConvertClass.static_ext_date(dr["dtRealStartTime"]);
			model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
			model.strStartStation = ObjectConvertClass.static_ext_string(dr["strStartStation"]);
			model.strEndStation = ObjectConvertClass.static_ext_string(dr["strEndStation"]);
			model.nTrainmanTypeID = ObjectConvertClass.static_ext_int(dr["nTrainmanTypeID"]);
			model.nPlanType = ObjectConvertClass.static_ext_int(dr["nPlanType"]);
			model.nDragType = ObjectConvertClass.static_ext_int(dr["nDragType"]);
			model.nKehuoID = ObjectConvertClass.static_ext_int(dr["nKehuoID"]);
			model.nRemarkType = ObjectConvertClass.static_ext_int(dr["nRemarkType"]);
			model.strRemark = ObjectConvertClass.static_ext_string(dr["strRemark"]);
			model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
			model.strCreateSiteGUID = ObjectConvertClass.static_ext_string(dr["strCreateSiteGUID"]);
			model.strCreateUserGUID = ObjectConvertClass.static_ext_string(dr["strCreateUserGUID"]);
			model.strPlaceID = ObjectConvertClass.static_ext_string(dr["strPlaceID"]);
			model.dtPlanStartTime = ObjectConvertClass.static_ext_date(dr["dtPlanStartTime"]);
		}
	}
}
