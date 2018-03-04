using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.Plan.MD
{
  /// <summary>
  ///����: PlanSendLog
  ///˵��: �ƻ��·���־
  /// </summary>
  public class PlanSendLog
  {
    public PlanSendLog()
    {}

    /// <summary>
    /// ����
    /// </summary>
    public string strTrainNo;

    /// <summary>
    /// ��¼GUID
    /// </summary>
    public string strSendGUID;

    /// <summary>
    /// �ƻ�GUID
    /// </summary>
    public string strTrainPlanGUID;

    /// <summary>
    /// ��·����
    /// </summary>
    public string strTrainJiaoluName;

    /// <summary>
    /// �ƻ�����ʱ��
    /// </summary>
    public DateTime dtStartTime;

    /// <summary>
    /// ʵ�ʳ���ʱ��
    /// </summary>
    public DateTime dtRealStartTime;

    /// <summary>
    /// �·��ͻ�������
    /// </summary>
    public string strSendSiteName;

    /// <summary>
    /// �·�ʱ��
    /// </summary>
    public DateTime dtSendTime;

  }

     /// <summary>
  ///����: PlanSendLogList
  ///˵��: �·���־�б�
  /// </summary>
  public class PlanSendLogList : List<PlanSendLog>
  {
    public PlanSendLogList()
    {}

  }
}
