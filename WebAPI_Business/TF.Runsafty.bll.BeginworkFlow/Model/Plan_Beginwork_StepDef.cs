using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.BeginworkFlow
{
    public class Plan_Beginwork_StepDef_Result : Plan_Beginwork_StepResult
    {
        public string strTrainmanNumber_Step { get; set; }
        public string strStepID { get; set; }
      
    }

  /// <summary>
  ///类名: Plan_Beginwork_StepDef
  ///说明: 车间GUID
  /// </summary>
  public class Plan_Beginwork_StepDef
  {
      public string strTrainmanNumber_Step { get; set; }
      public string strTrainmanName { get; set; }
      public int nIsNecessary { get; set; }

      public bool IsExcuted { get; set; }
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
    private string m_strStepName;
    /// <summary>
    /// 步骤名称
    /// </summary>
    public string strStepName
    {
      get {return m_strStepName;}
      set {m_strStepName = value;}
    }
    private string m_strStepBrief;
    /// <summary>
    /// 步骤描述
    /// </summary>
    public string strStepBrief
    {
      get {return m_strStepBrief;}
      set {m_strStepBrief = value;}
    }



    private int m_nStepIndex;
    /// <summary>
    /// 步骤顺序
    /// </summary>
    public int nStepIndex
    {
      get {return m_nStepIndex;}
      set {m_nStepIndex = value;}
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

    public string strStepID { get; set; }
  }
  /// <summary>
  ///类名: Plan_Beginwork_StepDefList
  ///说明: 车间GUID列表类
  /// </summary>
  public class Plan_Beginwork_StepDefList : List<Plan_Beginwork_StepDef>
  {
    public Plan_Beginwork_StepDefList()
    {}
  }
}
