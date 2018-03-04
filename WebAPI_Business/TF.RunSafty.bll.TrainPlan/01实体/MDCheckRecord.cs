using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.Plan.MD
{
  /// <summary>
  ///����: CheckRecord
  ///˵��: ���ڿ�����
  /// </summary>
  public class CheckRecord
  {
    public CheckRecord()
    {}

    /// <summary>
    /// ����
    /// </summary>
    public int nPointID;

    /// <summary>
    /// ������
    /// </summary>
    public string strPointName;

    /// <summary>
    /// �Ƿ񿨿�
    /// </summary>
    public int nIsHold;

    /// <summary>
    /// ����
    /// </summary>
    public string strTrainmanNumber;

    /// <summary>
    /// ����
    /// </summary>
    public string strItemContent;

    /// <summary>
    /// ���ؽ��
    /// </summary>
    public int nCheckResult;

    /// <summary>
    /// ���ʱ��
    /// </summary>
    public DateTime dtCheckTime;

    /// <summary>
    /// ����ʱ��
    /// </summary>
    public DateTime dtCreateTime;

  }

  /// <summary>
  ///����: CheckRecordList
  ///˵��: �������б�
  /// </summary>
  public class CheckRecordList : List<CheckRecord>
  {
      public CheckRecordList()
      { }

  }
}
