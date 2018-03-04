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
  ///类名: Plan_Beginwork_StepResultQueryCondition
  ///说明: 出勤计划GUID查询条件类
  /// </summary>
  public class Plan_Beginwork_StepResultQueryCondition
  {
    public int page = 0;
    public int rows = 0;
    //
    public int nID = 0;
    //出勤计划GUID
    public string strTrainPlanGUID = "";
    //步骤名称
    public string strStepName = "";
    //字段值(整形)
    public int? nStepResult = 0;
    //步骤开始执行时间
    public DateTime? dtBeginTime;
    //步骤结束执行时间
    public DateTime? dtEndTime;
    //步骤描述
    public string strStepBrief = "";
    //步骤上传时间
    public DateTime? dtCreateTime;
    //步骤顺序
    public int? nStepIndex = 0;
    public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
    {
      SqlCondition = new StringBuilder();
      SqlCondition.Append(nID != null ? " and nID = @nID" : "");
      SqlCondition.Append(strTrainPlanGUID != "" ? " and strTrainPlanGUID = @strTrainPlanGUID" : "");
      SqlCondition.Append(strStepName != "" ? " and strStepName = @strStepName" : "");
      SqlCondition.Append(nStepResult != null ? " and nStepResult = @nStepResult" : "");
      SqlCondition.Append(dtBeginTime != null ? " and dtBeginTime = @dtBeginTime" : "");
      SqlCondition.Append(dtEndTime != null ? " and dtEndTime = @dtEndTime" : "");
      SqlCondition.Append(strStepBrief != "" ? " and strStepBrief = @strStepBrief" : "");
      SqlCondition.Append(dtCreateTime != null ? " and dtCreateTime = @dtCreateTime" : "");
      SqlCondition.Append(nStepIndex != null ? " and nStepIndex = @nStepIndex" : "");
      SqlParameter[] sqlParams ={
          new SqlParameter("nID",nID),
          new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
          new SqlParameter("strStepName",strStepName),
          new SqlParameter("nStepResult",nStepResult),
          new SqlParameter("dtBeginTime",dtBeginTime),
          new SqlParameter("dtEndTime",dtEndTime),
          new SqlParameter("strStepBrief",strStepBrief),
          new SqlParameter("dtCreateTime",dtCreateTime),
          new SqlParameter("nStepIndex",nStepIndex)};
      Params = sqlParams;
    }
  }
  /// <summary>
  ///类名: DBPlan_Beginwork_StepResult
  ///说明: 出勤计划GUID数据操作类
  /// </summary>
  public class DBPlan_Beginwork_StepResult
  {

    #region 增删改
    /// <summary>
    /// 添加数据
    /// </summary>
    public int Add(Plan_Beginwork_StepResult model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("insert into TAB_Plan_Beginwork_StepResult");
      strSql.Append("(strTrainPlanGUID,strStepName,nStepResult,dtBeginTime,dtEndTime,strStepBrief,dtCreateTime,nStepIndex)");
      strSql.Append("values(@strTrainPlanGUID,@strStepName,@nStepResult,@dtBeginTime,@dtEndTime,@strStepBrief,@dtCreateTime,@nStepIndex)");
      strSql.Append(";select @@IDENTITY");
      SqlParameter[] parameters = {
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@strStepName", model.strStepName),
          new SqlParameter("@nStepResult", model.nStepResult),
          new SqlParameter("@dtBeginTime", model.dtBeginTime),
          new SqlParameter("@dtEndTime", model.dtEndTime),
          new SqlParameter("@strStepBrief", model.strStepBrief),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@nStepIndex", model.nStepIndex)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    public bool Update(Plan_Beginwork_StepResult model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("Update TAB_Plan_Beginwork_StepResult set ");
      strSql.Append(" strTrainPlanGUID = @strTrainPlanGUID, ");
      strSql.Append(" strStepName = @strStepName, ");
      strSql.Append(" nStepResult = @nStepResult, ");
      strSql.Append(" dtBeginTime = @dtBeginTime, ");
      strSql.Append(" dtEndTime = @dtEndTime, ");
      strSql.Append(" strStepBrief = @strStepBrief, ");
      strSql.Append(" dtCreateTime = @dtCreateTime, ");
      strSql.Append(" nStepIndex = @nStepIndex ");
      strSql.Append(" where nID = @nID ");

      SqlParameter[] parameters = {
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@strStepName", model.strStepName),
          new SqlParameter("@nStepResult", model.nStepResult),
          new SqlParameter("@dtBeginTime", model.dtBeginTime),
          new SqlParameter("@dtEndTime", model.dtEndTime),
          new SqlParameter("@strStepBrief", model.strStepBrief),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@nStepIndex", model.nStepIndex)};

      return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    public bool Delete(string nID)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("delete from TAB_Plan_Beginwork_StepResult ");
      strSql.Append(" where nID = @nID ");
      SqlParameter[] parameters = {
          new SqlParameter("nID",nID)};

      return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    #endregion
    /// <summary>
    /// 检查数据是否存在
    /// </summary>
    public bool Exists(Plan_Beginwork_StepResult _Plan_Beginwork_StepResult)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) from TAB_Plan_Beginwork_StepResult where nID=@nID");
       SqlParameter[] parameters = {
           new SqlParameter("nID",_Plan_Beginwork_StepResult.nID)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
    }
    /// <summary>
    /// 获得数据DataTable
    /// </summary>
    public DataTable GetDataTable(Plan_Beginwork_StepResultQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      if (QueryCondition.page == 0)
      {
        strSql.Append("select * ");
        strSql.Append(" FROM TAB_Plan_Beginwork_StepResult where 1=1 " + strSqlOption.ToString());
      }else
      {
        strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_StepResult where 1 = 1 "+
        strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
        " nID from TAB_Plan_Beginwork_StepResult where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
      }
      return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
    }
    public List<Plan_Beginwork_StepDef> getStep(string strWorkShopGUID, int nWorkTypeID)
    {
        string sql = "select * from TAB_Plan_Beginwork_StepDef where strWorkShopGUID='" + strWorkShopGUID + "' and nWorkTypeID='" + nWorkTypeID + "' order by nStepIndex";
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
        List<Plan_Beginwork_StepDef> list = new List<Plan_Beginwork_StepDef>();
        foreach (DataRow dr in dt.Rows)
        {
            Plan_Beginwork_StepDef Plan_Beginwork_StepDefM = new Plan_Beginwork_StepDef();
            DataRowToStepModel(Plan_Beginwork_StepDefM, dr);
            list.Add(Plan_Beginwork_StepDefM);
        }
        return list;
    }
    public List<Plan_Beginwork_StepResult> getStepResult(string strTrainPlanGUID, int nWorkTypeID)
    {
        string sql = "select * from TAB_Plan_Beginwork_StepResult where strTrainPlanGUID='" + strTrainPlanGUID + "' and nWorkTypeID='" + nWorkTypeID + "'";
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
        Plan_Beginwork_StepResultList list = new Plan_Beginwork_StepResultList();
        foreach (DataRow dr in dt.Rows)
        {
            Plan_Beginwork_StepResult _Plan_Beginwork_StepResult = new Plan_Beginwork_StepResult();
            DataRowToModel(_Plan_Beginwork_StepResult, dr);
            list.Add(_Plan_Beginwork_StepResult);
        }
        return list;
       // return dt;
    }


    public DataTable getPlanDetail(string strTrainPlanGUID)
    {
        string sql = "select * from VIEW_Plan_Trainman where strTrainPlanGUID='" + strTrainPlanGUID + "'";
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
        return dt;
    }

    /// <summary>
    /// 获得数据List
    /// </summary>
    public Plan_Beginwork_StepResultList GetDataList(string strTrainPlanGUID, int nWorkTypeID, string strWorkShopGUID)
    {
      StringBuilder strSql = new StringBuilder();
    
        //strSql.Append("select * ");
        //strSql.Append(" FROM TAB_Plan_Beginwork_StepResult where 1=1  ");

        strSql.Append(" select a.strStepName,b.* from TAB_Plan_Beginwork_StepDef a left join TAB_Plan_Beginwork_StepResult b ");
        strSql.Append(" on  b.strStepName=a.strStepID and b.strTrainPlanGUID='" + strTrainPlanGUID + "' and b.nWorkTypeID=" + nWorkTypeID + " ");
        strSql.Append(" where a.strWorkShopGUID='" + strWorkShopGUID + "' and a.nWorkTypeID=" + nWorkTypeID + "");

        //if (!string.IsNullOrEmpty(strTrainPlanGUID))
        //    strSql.Append(" and strTrainPlanGUID='" + strTrainPlanGUID + "'  ");

      DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
      Plan_Beginwork_StepResultList list = new Plan_Beginwork_StepResultList();
      foreach (DataRow dr in dt.Rows)
      {
        Plan_Beginwork_StepResult _Plan_Beginwork_StepResult = new Plan_Beginwork_StepResult();
        DataRowToModel(_Plan_Beginwork_StepResult,dr);
        list.Add(_Plan_Beginwork_StepResult);
      }
      return list;
    }
    /// <summary>
    /// 获得记录总数
    /// </summary>
    public int GetDataCount(Plan_Beginwork_StepResultQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) ");
      strSql.Append(" FROM TAB_Plan_Beginwork_StepResult where 1=1" + strSqlOption.ToString());
      return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams));
    }
    /// <summary>
    /// 获得一个实体对象
    /// </summary>
    public Plan_Beginwork_StepResult GetModel(Plan_Beginwork_StepResultQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select top 1 * ");
      strSql.Append(" FROM TAB_Plan_Beginwork_StepResult where 1=1 " + strSqlOption.ToString());
      DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
      Plan_Beginwork_StepResult _Plan_Beginwork_StepResult = null;
      if (dt.Rows.Count > 0)
      {
        _Plan_Beginwork_StepResult = new Plan_Beginwork_StepResult();
        DataRowToModel(_Plan_Beginwork_StepResult,dt.Rows[0]);
      }
      return _Plan_Beginwork_StepResult;
    }
    /// <summary>
    /// 读取DataRow数据到Model中
    /// </summary>
    private void DataRowToModel(Plan_Beginwork_StepResult model, DataRow dr)
    {
        model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
        model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
        model.strStepName = ObjectConvertClass.static_ext_string(dr["strStepName"]);
        model.nStepResult = ObjectConvertClass.static_ext_int(dr["nStepResult"]);
        model.dtBeginTime = ObjectConvertClass.static_ext_date(dr["dtBeginTime"]);
        model.dtEndTime = ObjectConvertClass.static_ext_date(dr["dtEndTime"]);
        model.strStepBrief = ObjectConvertClass.static_ext_string(dr["strStepBrief"]);
        model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
        model.nStepIndex = ObjectConvertClass.static_ext_int(dr["nStepIndex"]);
        model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
        model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
    }


    /// <summary>
    /// 读取DataRow数据到Model中
    /// </summary>
    private void DataRowToStepModel(Plan_Beginwork_StepDef model, DataRow dr)
    {
        model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
        model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
        model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
        model.strStepName = ObjectConvertClass.static_ext_string(dr["strStepName"]);
        model.strStepBrief = ObjectConvertClass.static_ext_string(dr["strStepBrief"]);
        model.nStepIndex = ObjectConvertClass.static_ext_int(dr["nStepIndex"]);
        model.strStepID = ObjectConvertClass.static_ext_string(dr["strStepID"]);
        model.nIsNecessary = ObjectConvertClass.static_ext_int(dr["nIsNecessary"]);
        //model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
        //model.nStepIndex = ObjectConvertClass.static_ext_int(dr["nStepIndex"]);
        //model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
        //model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
    }
  }
}
