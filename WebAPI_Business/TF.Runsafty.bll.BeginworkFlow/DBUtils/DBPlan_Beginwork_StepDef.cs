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
  ///����: Plan_Beginwork_StepDefQueryCondition
  ///˵��: ����GUID��ѯ������
  /// </summary>
  public class Plan_Beginwork_StepDefQueryCondition
  {
    public int page = 0;
    public int rows = 0;
    //����GUID
    public string strWorkShopGUID = "";
    //��������
    public string strWorkShopName = "";
    //��������
    public string strStepName = "";
    //��������
    public string strStepBrief = "";
    //����˳��
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
  ///����: DBPlan_Beginwork_StepDef
  ///˵��: ����GUID���ݲ�����
  /// </summary>
  public class DBPlan_Beginwork_StepDef 
  {
    #region ��ɾ��
    /// <summary>
    /// �������
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
    /// ��������
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
    /// ɾ������
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
    /// ��������Ƿ����
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
    /// �������DataTable
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
    /// �������List
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
    /// ��ü�¼����
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
    /// ���һ��ʵ�����
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
    /// ��ȡDataRow���ݵ�Model��
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
