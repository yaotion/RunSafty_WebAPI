using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.BeginworkFlow
{
  /// <summary>
  ///����: Plan_Beginwork_Flow
  ///˵��: ���ڼƻ�GUID
  /// </summary>
  public class Plan_Beginwork_Flow
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
    private string m_strUserName;
    /// <summary>
    /// ֵ��Ա����
    /// </summary>
    public string strUserName
    {
      get {return m_strUserName;}
      set {m_strUserName = value;}
    }
    private string m_strUserNumber;
    /// <summary>
    /// ֵ��Ա����
    /// </summary>
    public string strUserNumber
    {
      get {return m_strUserNumber;}
      set {m_strUserNumber = value;}
    }
    private DateTime? m_dtConfirmTime;
    /// <summary>
    /// ȷ��ʱ��
    /// </summary>
    public DateTime? dtConfirmTime
    {
      get {return m_dtConfirmTime;}
      set {m_dtConfirmTime = value;}
    }
    private int m_nConfirmType;
    /// <summary>
    /// ȷ������(0�Զ�,1�ֹ�)
    /// </summary>
    public int nConfirmType
    {
      get {return m_nConfirmType;}
      set {m_nConfirmType = value;}
    }
    private string m_strConfirmBrief;
    /// <summary>
    /// 
    /// </summary>
    public string strConfirmBrief
    {
      get {return m_strConfirmBrief;}
      set {m_strConfirmBrief = value;}
    }
    private int m_nFlowState;
    /// <summary>
    /// ����״̬
    /// </summary>
    public int nFlowState
    {
      get {return m_nFlowState;}
      set {m_nFlowState = value;}
    }
    private DateTime? m_dtCreateTime;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public DateTime? dtCreateTime
    {
      get {return m_dtCreateTime;}
      set {m_dtCreateTime = value;}
    }
    private DateTime? m_dtBeginTime;
    /// <summary>
    /// ��ʼʱ��
    /// </summary>
    public DateTime? dtBeginTime
    {
      get {return m_dtBeginTime;}
      set {m_dtBeginTime = value;}
    }
    private DateTime? m_dtEndTime;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public DateTime? dtEndTime
    {
      get {return m_dtEndTime;}
      set {m_dtEndTime = value;}
    }
    private int m_nExecLength;
    /// <summary>
    /// ִ��ʱ��(��λ����)
    /// </summary>
    public int nExecLength
    {
      get {return m_nExecLength;}
      set {m_nExecLength = value;}
    }
  }
  /// <summary>
  ///����: Plan_Beginwork_FlowList
  ///˵��: ���ڼƻ�GUID�б���
  /// </summary>
  public class Plan_Beginwork_FlowList : List<Plan_Beginwork_Flow>
  {
    public Plan_Beginwork_FlowList()
    {}
  }
}
