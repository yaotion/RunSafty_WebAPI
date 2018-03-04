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
  ///类名: Plan_Beginwork_StepIndexQueryCondition
  ///说明: 出勤计划GUID查询条件类
  /// </summary>
  public class Plan_Beginwork_StepIndexQueryCondition
  {
    public int page = 0;
    public int rows = 0;
    //
    public int nID = 0;
    //出勤计划GUID
    public string strTrainPlanGUID = "";
    //流程人员工号
    public string strTrainmanNumber = "";
    //计划出勤时间用于排序
    public DateTime? dtStartTime;
    //索引字段名称
    public string strFieldName = "";
    //索引值(整形)
    public int? nStepData = 0;
    //索引值(日期型)
    public DateTime? dtStepData;
    //索引值(字符串形)
    public string strStepData = "";
    public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
    {
      SqlCondition = new StringBuilder();
      SqlCondition.Append(nID != null ? " and nID = @nID" : "");
      SqlCondition.Append(strTrainPlanGUID != "" ? " and strTrainPlanGUID = @strTrainPlanGUID" : "");
      SqlCondition.Append(strTrainmanNumber != "" ? " and strTrainmanNumber = @strTrainmanNumber" : "");
      SqlCondition.Append(dtStartTime != null ? " and dtStartTime = @dtStartTime" : "");
      SqlCondition.Append(strFieldName != "" ? " and strFieldName = @strFieldName" : "");
      SqlCondition.Append(nStepData != null ? " and nStepData = @nStepData" : "");
      SqlCondition.Append(dtStepData != null ? " and dtStepData = @dtStepData" : "");
      SqlCondition.Append(strStepData != "" ? " and strStepData = @strStepData" : "");
      SqlParameter[] sqlParams ={
          new SqlParameter("nID",nID),
          new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
          new SqlParameter("strTrainmanNumber",strTrainmanNumber),
          new SqlParameter("dtStartTime",dtStartTime),
          new SqlParameter("strFieldName",strFieldName),
          new SqlParameter("nStepData",nStepData),
          new SqlParameter("dtStepData",dtStepData),
          new SqlParameter("strStepData",strStepData)};
      Params = sqlParams;
    }
  }
  /// <summary>
  ///类名: DBPlan_Beginwork_StepIndex
  ///说明: 出勤计划GUID数据操作类
  /// </summary>
  public class DBPlan_Beginwork_StepIndex
  {
    #region 增删改
    /// <summary>
    /// 添加数据
    /// </summary>
    public int Add(Plan_Beginwork_StepIndex model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("insert into TAB_Plan_Beginwork_StepIndex");
      strSql.Append("(strTrainPlanGUID,strTrainmanNumber,dtStartTime,strFieldName,nStepData,dtStepData,strStepData)");
      strSql.Append("values(@strTrainPlanGUID,@strTrainmanNumber,@dtStartTime,@strFieldName,@nStepData,@dtStepData,@strStepData)");
      strSql.Append(";select @@IDENTITY");
      SqlParameter[] parameters = {
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@strTrainmanNumber", model.strTrainmanNumber),
          new SqlParameter("@dtStartTime", model.dtStartTime),
          new SqlParameter("@strFieldName", model.strFieldName),
          new SqlParameter("@nStepData", model.nStepData),
          new SqlParameter("@dtStepData", model.dtStepData),
          new SqlParameter("@strStepData", model.strStepData)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    public bool Update(Plan_Beginwork_StepIndex model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("Update TAB_Plan_Beginwork_StepIndex set ");
      strSql.Append(" strTrainPlanGUID = @strTrainPlanGUID, ");
      strSql.Append(" strTrainmanNumber = @strTrainmanNumber, ");
      strSql.Append(" dtStartTime = @dtStartTime, ");
      strSql.Append(" strFieldName = @strFieldName, ");
      strSql.Append(" nStepData = @nStepData, ");
      strSql.Append(" dtStepData = @dtStepData, ");
      strSql.Append(" strStepData = @strStepData ");
      strSql.Append(" where nID = @nID ");

      SqlParameter[] parameters = {
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@strTrainmanNumber", model.strTrainmanNumber),
          new SqlParameter("@dtStartTime", model.dtStartTime),
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
      strSql.Append("delete from TAB_Plan_Beginwork_StepIndex ");
      strSql.Append(" where nID = @nID ");
      SqlParameter[] parameters = {
          new SqlParameter("nID",nID)};

      return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    #endregion
    /// <summary>
    /// 检查数据是否存在
    /// </summary>
    public bool Exists(Plan_Beginwork_StepIndex _Plan_Beginwork_StepIndex)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) from TAB_Plan_Beginwork_StepIndex where nID=@nID");
       SqlParameter[] parameters = {
           new SqlParameter("nID",_Plan_Beginwork_StepIndex.nID)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
    }
    /// <summary>
    /// 获得数据DataTable
    /// </summary>
    public DataTable GetDataTable(Plan_Beginwork_StepIndexQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      if (QueryCondition.page == 0)
      {
        strSql.Append("select * ");
        strSql.Append(" FROM TAB_Plan_Beginwork_StepIndex where 1=1 " + strSqlOption.ToString());
      }else
      {
        strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_StepIndex where 1 = 1 "+
        strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
        " nID from TAB_Plan_Beginwork_StepIndex where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
      }
      return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
    }
    /// <summary>
    /// 获得数据List
    /// </summary>
    public Plan_Beginwork_StepIndexList GetDataList(Plan_Beginwork_StepIndexQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      if (QueryCondition.page == 0)
      {
        strSql.Append("select * ");
        strSql.Append(" FROM TAB_Plan_Beginwork_StepIndex where 1=1 " + strSqlOption.ToString());
      }else
      {
        strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_StepIndex where 1 = 1 "+
        strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
        " nID from TAB_Plan_Beginwork_StepIndex where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
      }
      DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
      Plan_Beginwork_StepIndexList list = new Plan_Beginwork_StepIndexList();
      foreach (DataRow dr in dt.Rows)
      {
        Plan_Beginwork_StepIndex _Plan_Beginwork_StepIndex = new Plan_Beginwork_StepIndex();
        DataRowToModel(_Plan_Beginwork_StepIndex,dr);
        list.Add(_Plan_Beginwork_StepIndex);
      }
      return list;
    }
    /// <summary>
    /// 获得记录总数
    /// </summary>
    public int GetDataCount(Plan_Beginwork_StepIndexQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) ");
      strSql.Append(" FROM TAB_Plan_Beginwork_StepIndex where 1=1" + strSqlOption.ToString());
      return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams));
    }
    /// <summary>
    /// 获得一个实体对象
    /// </summary>
    public Plan_Beginwork_StepIndex GetModel(Plan_Beginwork_StepIndexQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select top 1 * ");
      strSql.Append(" FROM TAB_Plan_Beginwork_StepIndex where 1=1 " + strSqlOption.ToString());
      DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
      Plan_Beginwork_StepIndex _Plan_Beginwork_StepIndex = null;
      if (dt.Rows.Count > 0)
      {
        _Plan_Beginwork_StepIndex = new Plan_Beginwork_StepIndex();
        DataRowToModel(_Plan_Beginwork_StepIndex,dt.Rows[0]);
      }
      return _Plan_Beginwork_StepIndex;
    }
    /// <summary>
    /// 读取DataRow数据到Model中
    /// </summary>
    private void DataRowToModel(Plan_Beginwork_StepIndex model,DataRow dr)
    {
      model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
      model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
      model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
      model.dtStartTime = ObjectConvertClass.static_ext_date(dr["dtStartTime"]);
      model.strFieldName = ObjectConvertClass.static_ext_string(dr["strFieldName"]);
      model.nStepData = ObjectConvertClass.static_ext_int(dr["nStepData"]);
      model.dtStepData = ObjectConvertClass.static_ext_date(dr["dtStepData"]);
      model.strStepData = ObjectConvertClass.static_ext_string(dr["strStepData"]);
    }
  }
}
