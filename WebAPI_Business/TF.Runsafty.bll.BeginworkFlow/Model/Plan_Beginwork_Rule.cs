using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.BeginworkFlow
{
  /// <summary>
  ///����: Plan_Beginwork_Rule
  ///˵��: ����GUID
  /// </summary>
  public class Plan_Beginwork_Rule
  {
    private string m_strWorkShopGUID;
    /// <summary>
    /// ����GUID
    /// </summary>
    public string strWorkShopGUID
    {
      get {return m_strWorkShopGUID;}
      set {m_strWorkShopGUID = value;}
    }
    private string m_strWorkShopName;
    /// <summary>
    /// ��������
    /// </summary>
    public string strWorkShopName
    {
      get {return m_strWorkShopName;}
      set {m_strWorkShopName = value;}
    }
    private string m_strKeyStepName;
    /// <summary>
    /// �ؼ���������
    /// </summary>
    public string strKeyStepName
    {
      get {return m_strKeyStepName;}
      set {m_strKeyStepName = value;}
    }
    private int m_nExecByStepIndex;
    /// <summary>
    /// �Ƿ�˳��ִ��(0���ǣ�1��)
    /// </summary>
    public int nExecByStepIndex
    {
      get {return m_nExecByStepIndex;}
      set {m_nExecByStepIndex = value;}
    }
    private int m_nID;
    /// <summary>
    /// 
    /// </summary>
    public int nID
    {
      get {return m_nID;}
      set {m_nID = value;}
    }


    private int _nConfirmType;

    public int nConfirmType
    {
        get { return _nConfirmType; }
        set { _nConfirmType = value; }
    }




  }
  /// <summary>
  ///����: Plan_Beginwork_RuleList
  ///˵��: ����GUID�б���
  /// </summary>
  public class Plan_Beginwork_RuleList : List<Plan_Beginwork_Rule>
  {
    public Plan_Beginwork_RuleList()
    {}
  }




  public class IsExecute
  {
      public int CanExecute;
      public string errStr;
  
  }
}
