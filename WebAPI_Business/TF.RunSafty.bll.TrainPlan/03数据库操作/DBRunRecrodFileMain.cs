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
	///类名: EndWork_RunRecrod_MainQueryCondition
	///说明: 退勤运行记录转储主查询条件类
	/// </summary>
    public class RunRecordMainQueryCondition
	{
		public int page = 0;
		public int rows = 0;
		//
		public int nID = 0;
		//转储时间
        public DateTime dtUploadTime;
		//出勤计划GUID
		public string strPlanGUID = "";
		//计划出勤时间
		public DateTime? dtPlanChuQinTime;
		//车次
		public string strTrainNo = "";
		//车号
		public string strTrainNum = "";
		//车型
		public string strTrainTypeName = "";
		//司机1工号
		public string strTrainmanNumber1 = "";
		//司机1姓名
		public string strTrainmanName1 = "";
		//司机2工号
		public string strTrainmanNumber2 = "";
		//司机2姓名
		public string strTrainmanName2 = "";
		//司机3工号
		public string strTrainmanNumber3 = "";
		//司机3姓名
		public string strTrainmanName3 = "";
		//司机4工号
		public string strTrainmanNumber4 = "";
		//司机4姓名
		public string strTrainmanName4 = "";
		//IC卡号
		public string strCardNumber = "";
        //客户端编号
        public string strSiteNumber = "";
        //客户端名称
        public string strSiteName = "";
		public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
		{
			SqlCondition = new StringBuilder();
			SqlCondition.Append(nID != null ? " and nID = @nID" : "");
            SqlCondition.Append(dtUploadTime != null ? " and dtUploadTime = @dtUploadTime" : "");
			SqlCondition.Append(strPlanGUID != "" ? " and strPlanGUID = @strPlanGUID" : "");
			SqlCondition.Append(dtPlanChuQinTime != null ? " and dtPlanChuQinTime = @dtPlanChuQinTime" : "");
			SqlCondition.Append(strTrainNo != "" ? " and strTrainNo = @strTrainNo" : "");
			SqlCondition.Append(strTrainNum != "" ? " and strTrainNum = @strTrainNum" : "");
			SqlCondition.Append(strTrainTypeName != "" ? " and strTrainTypeName = @strTrainTypeName" : "");
			SqlCondition.Append(strTrainmanNumber1 != "" ? " and strTrainmanNumber1 = @strTrainmanNumber1" : "");
			SqlCondition.Append(strTrainmanName1 != "" ? " and strTrainmanName1 = @strTrainmanName1" : "");
			SqlCondition.Append(strTrainmanNumber2 != "" ? " and strTrainmanNumber2 = @strTrainmanNumber2" : "");
			SqlCondition.Append(strTrainmanName2 != "" ? " and strTrainmanName2 = @strTrainmanName2" : "");
			SqlCondition.Append(strTrainmanNumber3 != "" ? " and strTrainmanNumber3 = @strTrainmanNumber3" : "");
			SqlCondition.Append(strTrainmanName3 != "" ? " and strTrainmanName3 = @strTrainmanName3" : "");
			SqlCondition.Append(strTrainmanNumber4 != "" ? " and strTrainmanNumber4 = @strTrainmanNumber4" : "");
			SqlCondition.Append(strTrainmanName4 != "" ? " and strTrainmanName4 = @strTrainmanName4" : "");
			SqlCondition.Append(strCardNumber != "" ? " and strCardNumber = @strCardNumber" : "");
            SqlCondition.Append(strSiteNumber != "" ? " and strSiteNumber = @strSiteNumber" : "");
            SqlCondition.Append(strSiteName != "" ? " and strSiteName = @strSiteName" : "");
			SqlParameter[] sqlParams ={
					new SqlParameter("nID",nID),
					new SqlParameter("dtUpdateTime",dtUploadTime),
					new SqlParameter("strPlanGUID",strPlanGUID),
					new SqlParameter("dtPlanChuQinTime",dtPlanChuQinTime),
					new SqlParameter("strTrainNo",strTrainNo),
					new SqlParameter("strTrainNum",strTrainNum),
					new SqlParameter("strTrainTypeName",strTrainTypeName),
					new SqlParameter("strTrainmanNumber1",strTrainmanNumber1),
					new SqlParameter("strTrainmanName1",strTrainmanName1),
					new SqlParameter("strTrainmanNumber2",strTrainmanNumber2),
					new SqlParameter("strTrainmanName2",strTrainmanName2),
					new SqlParameter("strTrainmanNumber3",strTrainmanNumber3),
					new SqlParameter("strTrainmanName3",strTrainmanName3),
					new SqlParameter("strTrainmanNumber4",strTrainmanNumber4),
					new SqlParameter("strTrainmanName4",strTrainmanName4),
					new SqlParameter("strCardNumber",strCardNumber),
                    new SqlParameter("strSiteNumber",strSiteNumber),
                    new SqlParameter("strSiteName",strSiteName)
                                      };
			Params = sqlParams;
		}
	}


	/// <summary>
	///类名: DBEndWork_RunRecrod_Main
	///说明: 退勤运行记录转储主数据操作类
	/// </summary>
	public class DBRunRecordFileMain : DBOperator
	{

        public static string ZhuanChuString
        {
            get
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuString"] == null)
                {
                    return "";
                }
                return System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuString"].ToString();
            }
        }

        public static string ZhuanChuIsEnable
        {
            get
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuIsEnable"] == null)
                {
                    return "";
                }
                return System.Configuration.ConfigurationSettings.AppSettings["ZhuanChuIsEnable"].ToString();
            }
        }


        

        #region 从转储记录表中获取最后一条记录的时间 和文件的数量

        public DataTable getMaxTimeAndCount(string dtStartTime, string strTrainManNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select max(jg71) as DetailTime,count(*) as DetaieCount from cljg where jg02 between '" + dtStartTime + "' and '" + DateTime.Now + "' and (jg38 = '" + strTrainManNumber + "' or jg39 = '" + strTrainManNumber + "') ");
            return SqlHelper.ExecuteDataset(ZhuanChuString, CommandType.Text, strSql.ToString()).Tables[0];
        }
        #endregion




        public DBRunRecordFileMain(string ConnectionString) : base(ConnectionString) { }
		#region 增删改
		/// <summary>
		/// 添加数据
		/// </summary>
        public string Add(SqlCommand command, RunRecordFileMain model, Boolean isExitPlan,int a)
		{
            string sqlConnString = ZhuanChuString;
            string DetailTime = DateTime.Now.ToString();
            int DetaieCount = a;
            if (ZhuanChuIsEnable == "1")
            {
                try
                {
                    if (!string.IsNullOrEmpty(model.strTrainmanNumber1) && model.dtPlanChuQinTime > DateTime.Now.AddDays(-1000))
                    {
                        DataTable dt = getMaxTimeAndCount(model.dtPlanChuQinTime.ToString(), model.strTrainmanNumber1);
                        if (ObjectConvertClass.static_ext_int(dt.Rows[0]["DetaieCount"]) != 0)
                        {
                            DetaieCount = ObjectConvertClass.static_ext_int(dt.Rows[0]["DetaieCount"]);
                            DetailTime = ObjectConvertClass.static_ext_string(dt.Rows[0]["DetailTime"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
			StringBuilder strSql = new StringBuilder();
			strSql.Append("DECLARE @ROWGUID_EndWork_RunRecrod_Main TABLE(ID uniqueidentifier) ");
            strSql.Append("insert into Tab_EndWork_RunRecrod_Main ");
            strSql.Append("(dtUploadTime,strPlanGUID,dtPlanChuQinTime,strTrainNo,strTrainNum,strTrainTypeName,strTrainmanNumber1,strTrainmanName1,strTrainmanNumber2,strTrainmanName2,strTrainmanNumber3,strTrainmanName3,strTrainmanNumber4,strTrainmanName4,strCardNumber,strSiteNumber,strSiteName,dtLastUpDateTime,nDetailCount)");
            strSql.Append("OUTPUT INSERTED.STRGUID INTO @ROWGUID_EndWork_RunRecrod_Main ");
            strSql.Append("values(@dtUploadTime,@strPlanGUID,@dtPlanChuQinTime,@strTrainNo,@strTrainNum,@strTrainTypeName,@strTrainmanNumber1,@strTrainmanName1,@strTrainmanNumber2,@strTrainmanName2,@strTrainmanNumber3,@strTrainmanName3,@strTrainmanNumber4,@strTrainmanName4,@strCardNumber,@strSiteNumber,@strSiteName,@dtLastUpDateTime,@nDetailCount)");
			strSql.Append(";SELECT ID FROM @ROWGUID_EndWork_RunRecrod_Main");
			SqlParameter[] parameters = {
					new SqlParameter("@dtUploadTime", model.dtUploadTime),
					new SqlParameter("@strPlanGUID", model.strPlanGUID),
					new SqlParameter("@dtPlanChuQinTime", model.dtPlanChuQinTime),
					new SqlParameter("@strTrainNo", model.strTrainNo),
					new SqlParameter("@strTrainNum", model.strTrainNum),
					new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
					new SqlParameter("@strTrainmanNumber1", model.strTrainmanNumber1),
					new SqlParameter("@strTrainmanName1", model.strTrainmanName1),
					new SqlParameter("@strTrainmanNumber2", model.strTrainmanNumber2),
					new SqlParameter("@strTrainmanName2", model.strTrainmanName2),
					new SqlParameter("@strTrainmanNumber3", model.strTrainmanNumber3),
					new SqlParameter("@strTrainmanName3", model.strTrainmanName3),
					new SqlParameter("@strTrainmanNumber4", model.strTrainmanNumber4),
					new SqlParameter("@strTrainmanName4", model.strTrainmanName4),
					new SqlParameter("@strCardNumber", model.strCardNumber),
                    new SqlParameter("@strSiteNumber", model.strSiteNumber),
                    new SqlParameter("@strSiteName", model.strSiteName),
                    new SqlParameter("@nDetailCount", DetaieCount),
                    new SqlParameter("@dtLastUpDateTime", DetailTime)
                                        };

            command.CommandText = strSql.ToString();
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);
            
            return  ObjectConvertClass.static_ext_string(command.ExecuteScalar());
            //return  command.ExecuteNonQuery();
		}
        #endregion 增删改


        #region 获取主表中的个数
        public int getMainCount(string strTrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM Tab_EndWork_RunRecrod_Main where strPlanGUID=@strPlanGUID");

            SqlParameter[] sqlParams ={
                                   new SqlParameter("@strPlanGUID",strTrainPlanGUID) 
                                   };
            return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
        }

        #endregion

        #region 根据当前世间 和工号 获取当前的计划GUID
        public string getTrainPlanGUID(string strTrainNumber)
        {
            string strTrainmanGUID="";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select strTrainmanGUID from  TAB_Org_Trainman where strTrainmanNumber='" + strTrainNumber + "'");
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                strTrainmanGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID"]);
            else
                return "";
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(@"select top 1 strTrainPlanGUID from TAB_Plan_Trainman where dtCreateTime<'" + DateTime.Now.ToString() + "' and      (strTrainmanGUID1= '" + strTrainmanGUID + "' or strTrainmanGUID2= '" + strTrainmanGUID + "' or strTrainmanGUID3= '" + strTrainmanGUID + "' or  strTrainmanGUID4= '" + strTrainmanGUID + "')");
            DataTable dt2 = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql2.ToString()).Tables[0];
            if (dt2.Rows.Count <= 0)
                return "";
            else
                return ObjectConvertClass.static_ext_string(dt2.Rows[0]["strTrainPlanGUID"]);
        }

        #endregion

        









        /// <summary>
		/// 获得数据DataTable
		/// </summary>
        public DataTable GetDataTable(RunRecordMainQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM Tab_EndWork_RunRecrod_Main where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from Tab_EndWork_RunRecrod_Main where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from Tab_EndWork_RunRecrod_Main where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
		}
		/// <summary>
		/// 获得数据List
		/// </summary>
        public RunRecordFileMainList GetDataList(RunRecordMainQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM Tab_EndWork_RunRecrod_Main where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from Tab_EndWork_RunRecrod_Main where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
				" nID from Tab_EndWork_RunRecrod_Main where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
			}
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            RunRecordFileMainList list = new RunRecordFileMainList();
			foreach (DataRow dr in dt.Rows)
			{
                RunRecordFileMain _EndWork_RunRecrod_Main = new RunRecordFileMain();
				DataRowToModel(_EndWork_RunRecrod_Main,dr);
				list.Add(_EndWork_RunRecrod_Main);
			}
			return list;
		}
		/// <summary>
		/// 获得记录总数
		/// </summary>
        public int GetDataCount(RunRecordMainQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) ");
			strSql.Append(" FROM Tab_EndWork_RunRecrod_Main where 1=1" + strSqlOption.ToString());
			return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
		}
		/// <summary>
		/// 获得一个实体对象
		/// </summary>
        public RunRecordFileMain GetModel(RunRecordMainQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 * ");
			strSql.Append(" FROM Tab_EndWork_RunRecrod_Main where 1=1 " + strSqlOption.ToString());
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            RunRecordFileMain _EndWork_RunRecrod_Main = null;
			if (dt.Rows.Count > 0)
			{
                _EndWork_RunRecrod_Main = new RunRecordFileMain();
				DataRowToModel(_EndWork_RunRecrod_Main,dt.Rows[0]);
			}
			return _EndWork_RunRecrod_Main;
		}
		/// <summary>
		/// 读取DataRow数据到Model中
		/// </summary>
        private void DataRowToModel(RunRecordFileMain model, DataRow dr)
		{
			model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
            model.dtUploadTime = ObjectConvertClass.static_ext_date(dr["dtUploadTime"]);
			model.strPlanGUID = ObjectConvertClass.static_ext_string(dr["strPlanGUID"]);
			model.dtPlanChuQinTime = ObjectConvertClass.static_ext_date(dr["dtPlanChuQinTime"]);
			model.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
			model.strTrainNum = ObjectConvertClass.static_ext_string(dr["strTrainNum"]);
			model.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
			model.strTrainmanNumber1 = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber1"]);
			model.strTrainmanName1 = ObjectConvertClass.static_ext_string(dr["strTrainmanName1"]);
			model.strTrainmanNumber2 = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber2"]);
			model.strTrainmanName2 = ObjectConvertClass.static_ext_string(dr["strTrainmanName2"]);
			model.strTrainmanNumber3 = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber3"]);
			model.strTrainmanName3 = ObjectConvertClass.static_ext_string(dr["strTrainmanName3"]);
			model.strTrainmanNumber4 = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber4"]);
			model.strTrainmanName4 = ObjectConvertClass.static_ext_string(dr["strTrainmanName4"]);
			model.strCardNumber = ObjectConvertClass.static_ext_string(dr["strCardNumber"]);
            model.strSiteNumber = ObjectConvertClass.static_ext_string(dr["strSiteNumber"]);
            model.strSiteName = ObjectConvertClass.static_ext_string(dr["strSiteName"]);
		}
	}
}
