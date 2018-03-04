using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.BeginworkFlow
{
  /// <summary>
  ///����: Plan_Beginwork_StepIndex
  ///˵��: ���ڼƻ�GUID
  /// </summary>
  public class Plan_Beginwork_StepIndex
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
    /// ���ڼƻ�GUID
    /// </summary>
    public string strTrainPlanGUID
    {
      get {return m_strTrainPlanGUID;}
      set {m_strTrainPlanGUID = value;}
    }
    private string m_strTrainmanNumber;
    /// <summary>
    /// ������Ա����
    /// </summary>
    public string strTrainmanNumber
    {
      get {return m_strTrainmanNumber;}
      set {m_strTrainmanNumber = value;}
    }
    private DateTime? m_dtStartTime;
    /// <summary>
    /// �ƻ�����ʱ����������
    /// </summary>
    public DateTime? dtStartTime
    {
      get {return m_dtStartTime;}
      set {m_dtStartTime = value;}
    }
    private string m_strFieldName;
    /// <summary>
    /// �����ֶ�����
    /// </summary>
    public string strFieldName
    {
      get {return m_strFieldName;}
      set {m_strFieldName = value;}
    }
    private int m_nStepData;
    /// <summary>
    /// ����ֵ(����)
    /// </summary>
    public int nStepData
    {
      get {return m_nStepData;}
      set {m_nStepData = value;}
    }
    private DateTime? m_dtStepData;
    /// <summary>
    /// ����ֵ(������)
    /// </summary>
    public DateTime? dtStepData
    {
      get {return m_dtStepData;}
      set {m_dtStepData = value;}
    }
    private string m_strStepData;
    /// <summary>
    /// ����ֵ(�ַ�����)
    /// </summary>
    public string strStepData
    {
      get {return m_strStepData;}
      set {m_strStepData = value;}
    }
  }
  /// <summary>
  ///����: Plan_Beginwork_StepIndexList
  ///˵��: ���ڼƻ�GUID�б���
  /// </summary>
  public class Plan_Beginwork_StepIndexList : List<Plan_Beginwork_StepIndex>
  {
    public Plan_Beginwork_StepIndexList()
    {}
  }
}
