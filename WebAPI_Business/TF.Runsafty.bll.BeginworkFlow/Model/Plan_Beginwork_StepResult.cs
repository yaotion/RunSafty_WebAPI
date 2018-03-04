using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.BeginworkFlow
{
  /// <summary>
  ///类名: Plan_Beginwork_StepResult
  ///说明: 出勤计划GUID
  /// </summary>
  public class Plan_Beginwork_StepResult
  {
    private int m_nID;
    /// <summary>
    /// 
    /// </summary>
    public int nID
    {
      get {return m_nID;}
      set {m_nID = value;}
    }
    private string m_strTrainPlanGUID;
    /// <summary>
    /// 出勤计划GUID
    /// </summary>
    public string strTrainPlanGUID
    {
      get {return m_strTrainPlanGUID;}
      set {m_strTrainPlanGUID = value;}
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
    private int m_nStepResult;
    /// <summary>
    /// 字段值(整形)
    /// </summary>
    public int nStepResult
    {
      get {return m_nStepResult;}
      set {m_nStepResult = value;}
    }
    private DateTime? m_dtBeginTime;
    /// <summary>
    /// 步骤开始执行时间
    /// </summary>
    public DateTime? dtBeginTime
    {
      get {return m_dtBeginTime;}
      set {m_dtBeginTime = value;}
    }
    private DateTime? m_dtEndTime;
    /// <summary>
    /// 步骤结束执行时间
    /// </summary>
    public DateTime? dtEndTime
    {
      get {return m_dtEndTime;}
      set {m_dtEndTime = value;}
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
    private DateTime? m_dtCreateTime;
    /// <summary>
    /// 步骤上传时间
    /// </summary>
    public DateTime? dtCreateTime
    {
      get {return m_dtCreateTime;}
      set {m_dtCreateTime = value;}
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

    private string _strTrainmanNumber;

    public string strTrainmanNumber
    {
        get { return _strTrainmanNumber; }
        set { _strTrainmanNumber = value; }
    }
    private string _strTrainmanName;

    public string strTrainmanName
    {
        get { return _strTrainmanName; }
        set { _strTrainmanName = value; }
    }


    public int nIsNecessary { get; set; }

    public object ExInfo { get; set; }

  }


  /// <summary>
  ///类名: Plan_Beginwork_StepResultList
  ///说明: 出勤计划GUID列表类
  /// </summary>
  public class Plan_Beginwork_StepResultList : List<Plan_Beginwork_StepResult>
  {
    public Plan_Beginwork_StepResultList()
    {}
  }
}
