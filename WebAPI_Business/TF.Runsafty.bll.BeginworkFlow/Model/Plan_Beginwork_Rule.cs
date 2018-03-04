using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.BeginworkFlow
{
  /// <summary>
  ///类名: Plan_Beginwork_Rule
  ///说明: 车间GUID
  /// </summary>
  public class Plan_Beginwork_Rule
  {
    private string m_strWorkShopGUID;
    /// <summary>
    /// 车间GUID
    /// </summary>
    public string strWorkShopGUID
    {
      get {return m_strWorkShopGUID;}
      set {m_strWorkShopGUID = value;}
    }
    private string m_strWorkShopName;
    /// <summary>
    /// 车间名称
    /// </summary>
    public string strWorkShopName
    {
      get {return m_strWorkShopName;}
      set {m_strWorkShopName = value;}
    }
    private string m_strKeyStepName;
    /// <summary>
    /// 关键步骤名称
    /// </summary>
    public string strKeyStepName
    {
      get {return m_strKeyStepName;}
      set {m_strKeyStepName = value;}
    }
    private int m_nExecByStepIndex;
    /// <summary>
    /// 是否按顺序执行(0不是，1是)
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
  ///类名: Plan_Beginwork_RuleList
  ///说明: 车间GUID列表类
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
