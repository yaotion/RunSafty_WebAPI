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
  ///类名: Plan_Beginwork_StepDefQueryCondition
  ///说明: 车间GUID查询条件类
  /// </summary>
  public class Plan_Beginwork_StepDefQueryCondition
  {
    public int page = 0;
    public int rows = 0;
    //车间GUID
    public string strWorkShopGUID = "";
    //车间名称
    public string strWorkShopName = "";
    //步骤名称
    public string strStepName = "";
    //步骤描述
    public string strStepBrief = "";
    //步骤顺序
    public int? nStepIndex = 0;
    //
    public int nID = 0;
    public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
    {
      SqlCondition = new StringBuilder();
      SqlCondition.Append(strWorkShopGUID != "" ? " and strWorkShopGUID = @strWorkShopGUID" : "");
      SqlCondition.Append(strWorkShopName != "" ? " and strWorkShopName = @strWorkShopName" : "");
      SqlCondition.Append(strStepName != "" ? " and strStepName = @strStepName" : "");
      SqlCondition.Append(strStepBrief != "" ? " and strStepBrief = @strStepBrief" : "");
      SqlCondition.Append(nStepIndex != null ? " and nStepIndex = @nStepIndex" : "");
      SqlCondition.Append(nID != null ? " and nID = @nID" : "");
      SqlParameter[] sqlParams ={
          new SqlParameter("strWorkShopGUID",strWorkShopGUID),
          new SqlParameter("strWorkShopName",strWorkShopName),
          new SqlParameter("strStepName",strStepName),
          new SqlParameter("strStepBrief",strStepBrief),
          new SqlParameter("nStepIndex",nStepIndex),
          new SqlParameter("nID",nID)};
      Params = sqlParams;
    }
  }
  /// <summary>
  ///类名: DBPlan_Beginwork_StepDef
  ///说明: 车间GUID数据操作类
  /// </summary>
  public class DBPlan_Beginwork_StepDef 
  {
    #region 增删改
    /// <summary>
    /// 添加数据
    /// </summary>
    public int Add(Plan_Beginwork_StepDef model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("insert into TAB_Plan_Beginwork_StepDef");
      strSql.Append("(strWorkShopGUID,strWorkShopName,strStepName,strStepBrief,nStepIndex)");
      strSql.Append("values(@strWorkShopGUID,@strWorkShopName,@strStepName,@strStepBrief,@nStepIndex)");
      strSql.Append(";select @@IDENTITY");
      SqlParameter[] parameters = {
          new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID),
          new SqlParameter("@strWorkShopName", model.strWorkShopName),
          new SqlParameter("@strStepName", model.strStepName),
          new SqlParameter("@strStepBrief", model.strStepBrief),
          new SqlParameter("@nStepIndex", model.nStepIndex),
                                  };
      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    public bool Update(Plan_Beginwork_StepDef model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("Update TAB_Plan_Beginwork_StepDef set ");
      strSql.Append(" strWorkShopGUID = @strWorkShopGUID, ");
      strSql.Append(" strWorkShopName = @strWorkShopName, ");
      strSql.Append(" strStepName = @strStepName, ");
      strSql.Append(" strStepBrief = @strStepBrief, ");
      strSql.Append(" nStepIndex = @nStepIndex, ");
      strSql.Append(" where nID = @nID ");

      SqlParameter[] parameters = {
          new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID),
          new SqlParameter("@strWorkShopName", model.strWorkShopName),
          new SqlParameter("@strStepName", model.strStepName),
          new SqlParameter("@strStepBrief", model.strStepBrief),
          new SqlParameter("@nStepIndex", model.nStepIndex),
                                  };
      return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    public bool Delete(string nID)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("delete from TAB_Plan_Beginwork_StepDef ");
      strSql.Append(" where nID = @nID ");
      SqlParameter[] parameters = {
          new SqlParameter("nID",nID)};

      return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    #endregion
    /// <summary>
    /// 检查数据是否存在
    /// </summary>
    public bool Exists(Plan_Beginwork_StepDef _Plan_Beginwork_StepDef)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) from TAB_Plan_Beginwork_StepDef where nID=@nID");
       SqlParameter[] parameters = {
           new SqlParameter("nID",_Plan_Beginwork_StepDef.nID)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
    }
    /// <summary>
    /// 获得数据DataTable
    /// </summary>
    public DataTable GetDataTable(Plan_Beginwork_StepDefQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      if (QueryCondition.page == 0)
      {
        strSql.Append("select * ");
        strSql.Append(" FROM TAB_Plan_Beginwork_StepDef where 1=1 " + strSqlOption.ToString());
      }else
      {
        strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_StepDef where 1 = 1 "+
        strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
        " nID from TAB_Plan_Beginwork_StepDef where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
      }
      return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
    }
    /// <summary>
    /// 获得数据List
    /// </summary>
    public Plan_Beginwork_StepDefList GetDataList(Plan_Beginwork_StepDefQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      if (QueryCondition.page == 0)
      {
        strSql.Append("select * ");
        strSql.Append(" FROM TAB_Plan_Beginwork_StepDef where 1=1 " + strSqlOption.ToString());
      }else
      {
        strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_StepDef where 1 = 1 "+
        strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
        " nID from TAB_Plan_Beginwork_StepDef where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
      }
      DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
      Plan_Beginwork_StepDefList list = new Plan_Beginwork_StepDefList();
      foreach (DataRow dr in dt.Rows)
      {
        Plan_Beginwork_StepDef _Plan_Beginwork_StepDef = new Plan_Beginwork_StepDef();
        DataRowToModel(_Plan_Beginwork_StepDef,dr);
        list.Add(_Plan_Beginwork_StepDef);
      }
      return list;
    }
    /// <summary>
    /// 获得记录总数
    /// </summary>
    public int GetDataCount(Plan_Beginwork_StepDefQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) ");
      strSql.Append(" FROM TAB_Plan_Beginwork_StepDef where 1=1" + strSqlOption.ToString());
      return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams));
    }
    /// <summary>
    /// 获得一个实体对象
    /// </summary>
    public Plan_Beginwork_StepDef GetModel(Plan_Beginwork_StepDefQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select top 1 * ");
      strSql.Append(" FROM TAB_Plan_Beginwork_StepDef where 1=1 " + strSqlOption.ToString());
      DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
      Plan_Beginwork_StepDef _Plan_Beginwork_StepDef = null;
      if (dt.Rows.Count > 0)
      {
        _Plan_Beginwork_StepDef = new Plan_Beginwork_StepDef();
        DataRowToModel(_Plan_Beginwork_StepDef,dt.Rows[0]);
      }
      return _Plan_Beginwork_StepDef;
    }
    /// <summary>
    /// 读取DataRow数据到Model中
    /// </summary>
    private void DataRowToModel(Plan_Beginwork_StepDef model,DataRow dr)
    {
      model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
      model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
      model.strStepName = ObjectConvertClass.static_ext_string(dr["strStepName"]);
      model.strStepBrief = ObjectConvertClass.static_ext_string(dr["strStepBrief"]);
      model.nStepIndex = ObjectConvertClass.static_ext_int(dr["nStepIndex"]);
      model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
    }
  }
}
