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

namespace  TF.RunSafty.BeginworkFlow
{
  /// <summary>
  ///����: Plan_Beginwork_RuleQueryCondition
  ///˵��: ����GUID��ѯ������
  /// </summary>
  public class Plan_Beginwork_RuleQueryCondition
  {
    public int page = 0;
    public int rows = 0;
    //����GUID
    public string strWorkShopGUID = "";
    //��������
    public string strWorkShopName = "";
    //�ؼ���������
    public string strKeyStepName = "";
    //�Ƿ�˳��ִ��(0���ǣ�1��)
    public int? nExecByStepIndex = 0;
    //
    public int nID = 0;
    public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
    {
      SqlCondition = new StringBuilder();
      SqlCondition.Append(strWorkShopGUID != "" ? " and strWorkShopGUID = @strWorkShopGUID" : "");
      SqlCondition.Append(strWorkShopName != "" ? " and strWorkShopName = @strWorkShopName" : "");
      SqlCondition.Append(strKeyStepName != "" ? " and strKeyStepName = @strKeyStepName" : "");
      SqlCondition.Append(nExecByStepIndex != null ? " and nExecByStepIndex = @nExecByStepIndex" : "");
      SqlCondition.Append(nID != null ? " and nID = @nID" : "");
      SqlParameter[] sqlParams ={
          new SqlParameter("strWorkShopGUID",strWorkShopGUID),
          new SqlParameter("strWorkShopName",strWorkShopName),
          new SqlParameter("strKeyStepName",strKeyStepName),
          new SqlParameter("nExecByStepIndex",nExecByStepIndex),
          new SqlParameter("nID",nID)};
      Params = sqlParams;
    }
  }
  /// <summary>
  ///����: DBPlan_Beginwork_Rule
  ///˵��: ����GUID���ݲ�����
  /// </summary>
  public class DBPlan_Beginwork_Rule
  {
    #region ��ɾ��
    /// <summary>
    /// �������
    /// </summary>
    public int Add(Plan_Beginwork_Rule model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("insert into TAB_Plan_Beginwork_Rule");
      strSql.Append("(strWorkShopGUID,strWorkShopName,strKeyStepName,nExecByStepIndex)");
      strSql.Append("values(@strWorkShopGUID,@strWorkShopName,@strKeyStepName,@nExecByStepIndex)");
      strSql.Append(";select @@IDENTITY");
      SqlParameter[] parameters = {
          new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID),
          new SqlParameter("@strWorkShopName", model.strWorkShopName),
          new SqlParameter("@strKeyStepName", model.strKeyStepName),
          new SqlParameter("@nExecByStepIndex", model.nExecByStepIndex),
                                  };
      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
    }
    /// <summary>
    /// SetBeginworkRule
    /// </summary>
    public bool SetBeginworkRule(string strWorkShopGUID, string strWorkShopName, int nExecByStepIndex)
    {

        StringBuilder strSql = new StringBuilder();
        strSql.Append("Update TAB_Plan_Beginwork_Rule set ");
        strSql.Append(" nExecByStepIndex = @nExecByStepIndex ");
        strSql.Append(" where strWorkShopGUID = @strWorkShopGUID ");
        SqlParameter[] parameters = {
          new SqlParameter("@strWorkShopGUID", strWorkShopGUID),
          new SqlParameter("@strWorkShopName", strWorkShopName),
          new SqlParameter("@nExecByStepIndex", nExecByStepIndex)
                                  };
        if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0)
            return true;
        else
        {
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into TAB_Plan_Beginwork_Rule");
            strSql2.Append("(strWorkShopGUID,strWorkShopName,nExecByStepIndex)");
            strSql2.Append("values(@strWorkShopGUID,@strWorkShopName,@nExecByStepIndex)");
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql2.ToString(), parameters)) > 0;

        }
    }



    /// <summary>
    /// SetBeginworkRule
    /// </summary>
    public bool SetConfirmType(string strWorkShopGUID,string strWorkShopName,  int nConfirmType,  string strKeyStepName)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("Update TAB_Plan_Beginwork_Rule set ");
        strSql.Append(" nConfirmType = @nConfirmType,strKeyStepName=@strKeyStepName ");
        strSql.Append(" where strWorkShopGUID = @strWorkShopGUID ");
        SqlParameter[] parameters = {
          new SqlParameter("@strWorkShopGUID", strWorkShopGUID),
          new SqlParameter("@strWorkShopName", strWorkShopName),
          new SqlParameter("@nConfirmType", nConfirmType),
          new SqlParameter("@strKeyStepName", strKeyStepName)
                                  };
        if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0)
            return true;
        else
        {
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into TAB_Plan_Beginwork_Rule");
            strSql2.Append("(strWorkShopGUID,strWorkShopName,nConfirmType,strKeyStepName)");
            strSql2.Append("values(@strWorkShopGUID,@strWorkShopName,@nConfirmType,@strKeyStepName)");
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql2.ToString(), parameters)) > 0;

        }
    }


    /// <summary>
    /// �������List
    /// </summary>
    public Boolean GetBeginworkRule(string strWorkShopGUID, string strKeyStepName)
    {
       
        StringBuilder strSql = new StringBuilder();
       
            strSql.Append("select * ");
            strSql.Append(" FROM TAB_Plan_Beginwork_Rule where 1=1 ");
       
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

        return false;
    }




    /// <summary>
    /// ɾ������
    /// </summary>
    public bool Delete(string nID)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("delete from TAB_Plan_Beginwork_Rule ");
      strSql.Append(" where nID = @nID ");
      SqlParameter[] parameters = {
          new SqlParameter("nID",nID)};
      return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    #endregion
    /// <summary>
    /// ��������Ƿ����
    /// </summary>
    public bool Exists(Plan_Beginwork_Rule _Plan_Beginwork_Rule)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) from TAB_Plan_Beginwork_Rule where nID=@nID");
       SqlParameter[] parameters = {
           new SqlParameter("nID",_Plan_Beginwork_Rule.nID)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
    }
    /// <summary>
    /// �������DataTable
    /// </summary>
    public DataTable GetDataTable(Plan_Beginwork_RuleQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      if (QueryCondition.page == 0)
      {
        strSql.Append("select * ");
        strSql.Append(" FROM TAB_Plan_Beginwork_Rule where 1=1 " + strSqlOption.ToString());
      }else
      {
        strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_Rule where 1 = 1 "+
        strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
        " nID from TAB_Plan_Beginwork_Rule where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
      }
      return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
    }

    /// <summary>
    ///��ȡ�����Ƿ��ܹ�ִ��
    /// </summary>
    /// <param name="strWorkShopGUID">����GUID</param>
    /// <param name="nStepIndex">����</param>
    /// <returns></returns>
    public IsExecute getIsExecute(string strWorkShopGUID, string strTrainPlanGUID, string strTrainmanGUID, string strStepID)
    {

        IsExecute i = new IsExecute();
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select nExecByStepIndex ");
        strSql.Append(" FROM TAB_Plan_Beginwork_Rule where strWorkShopGUID='" + strWorkShopGUID + "'");
        int IsExecute = 0;//����״̬  1������ִ��  0������ִ��  2���Ѿ����ڸò���
        int nExecByStepIndex = 0; //�Ƿ���Ҫ������ִ��  ���ݿ����  �Ƿ���˳�� 0,���ǰ���˳��,1,����˳��2��û������
        object OExecByStepIndex = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        if (OExecByStepIndex != null)
            nExecByStepIndex = Convert.ToInt32(OExecByStepIndex);
        else
            nExecByStepIndex = 2;

        int nStepIndex = 0;//��Ҫ�Ӳ��������  ��ȡ��ǰ�Ĳ���
        string strStepBrief = "";
        StringBuilder strSql4 = new StringBuilder();
        strSql4.Append("select top 1 * ");
        strSql4.Append(" FROM TAB_Plan_Beginwork_StepDef where strWorkShopGUID='" + strWorkShopGUID + "' and strStepID='" + strStepID + "'");
        DataTable dt4 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql4.ToString()).Tables[0];
        if (dt4.Rows.Count > 0)
        {
            nStepIndex = Convert.ToInt32(dt4.Rows[0]["nStepIndex"].ToString());
            strStepBrief = dt4.Rows[0]["strStepBrief"].ToString();
        }
        else
            nStepIndex = 0;


        int nCount = 0;
        int nStepIndexUp = nStepIndex - 1;
        StringBuilder strSql2 = new StringBuilder();
        strSql2.Append("select count(*) ");
        strSql2.Append(" FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID='" + strTrainPlanGUID + "' and nStepIndex=" + nStepIndexUp + " and strTrainmanGUID='" + strTrainmanGUID + "'");
        nCount = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()));

        int nCountNow = 0;
        StringBuilder strSql3 = new StringBuilder();
        strSql3.Append("select count(*) ");
        strSql3.Append(" FROM TAB_Plan_Beginwork_StepResult where strTrainPlanGUID='" + strTrainPlanGUID + "' and nStepIndex=" + nStepIndex + "and strTrainmanGUID='" + strTrainmanGUID + "'");
        nCountNow = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql3.ToString()));

        if (nCountNow >= 1)//�����ǰ�Ĳ����Ѿ�ִ�й�
            IsExecute = 2;
        else if (nExecByStepIndex == 1 && nCount >= 1) //�����ǰ�˳��ִ��,���ǰһ�������Ѿ�ִ�У�
            IsExecute = 1;
        else if (nExecByStepIndex == 1 && nCount == 0) //����˳��ִ�У�����ǰ��һ��û��ִ��
        {
            IsExecute = 0;
            i.errStr = "���衶" + strStepBrief + "��û��ִ�У�";
        }
        else if (nExecByStepIndex == 0 && nCount != 0)// ����˳��ִ�У�����ǰ��һ��û��ִ��
            IsExecute = 1;
        else
            IsExecute = 1;
        i.CanExecute = IsExecute;
        return i;
    }


    /// <summary>
    /// �������List
    /// </summary>
    public Plan_Beginwork_RuleList GetConfirmTypeList(string strWorkShopGUID)
    {
      StringBuilder strSql = new StringBuilder();
        strSql.Append("select * ");
        strSql.Append(" FROM TAB_Plan_Beginwork_Rule where 1=1 and strWorkShopGUID='" + strWorkShopGUID + "' and len(nConfirmType)>0 ");
      DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
      Plan_Beginwork_RuleList list = new Plan_Beginwork_RuleList();
      foreach (DataRow dr in dt.Rows)
      {
        Plan_Beginwork_Rule _Plan_Beginwork_Rule = new Plan_Beginwork_Rule();
        DataRowToModel(_Plan_Beginwork_Rule,dr);
        list.Add(_Plan_Beginwork_Rule);
      }
      return list;
    }



    /// <summary>
    /// �������List
    /// </summary>
    public Plan_Beginwork_RuleList GetExecByStepList(string strWorkShopGUID)
    {

        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * ");
        strSql.Append(" FROM TAB_Plan_Beginwork_Rule where 1=1 and strWorkShopGUID='" + strWorkShopGUID + "' and len(nExecByStepIndex)>0 ");
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        Plan_Beginwork_RuleList list = new Plan_Beginwork_RuleList();
        foreach (DataRow dr in dt.Rows)
        {
            Plan_Beginwork_Rule _Plan_Beginwork_Rule = new Plan_Beginwork_Rule();
            DataRowToModel(_Plan_Beginwork_Rule, dr);
            list.Add(_Plan_Beginwork_Rule);
        }
        return list;
    }







    /// <summary>
    /// ��ü�¼����
    /// </summary>
    public int GetDataCount(Plan_Beginwork_RuleQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) ");
      strSql.Append(" FROM TAB_Plan_Beginwork_Rule where 1=1" + strSqlOption.ToString());
      return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams));
    }
    /// <summary>
    /// ���һ��ʵ�����
    /// </summary>
    public Plan_Beginwork_Rule GetModel(Plan_Beginwork_RuleQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select top 1 * ");
      strSql.Append(" FROM TAB_Plan_Beginwork_Rule where 1=1 " + strSqlOption.ToString());
      DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
      Plan_Beginwork_Rule _Plan_Beginwork_Rule = null;
      if (dt.Rows.Count > 0)
      {
        _Plan_Beginwork_Rule = new Plan_Beginwork_Rule();
        DataRowToModel(_Plan_Beginwork_Rule,dt.Rows[0]);
      }
      return _Plan_Beginwork_Rule;
    }
    /// <summary>
    /// ��ȡDataRow���ݵ�Model��
    /// </summary>
    private void DataRowToModel(Plan_Beginwork_Rule model,DataRow dr)
    {
      model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
      model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
      model.strKeyStepName = ObjectConvertClass.static_ext_string(dr["strKeyStepName"]);
      model.nExecByStepIndex = ObjectConvertClass.static_ext_int(dr["nExecByStepIndex"]);
      model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
    }
  }
}
