using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.BaseDict
{
  /// <summary>
  ///����: KernelTimeConfig
  ///˵��: �˰�ϵͳʱ�����
  /// </summary>
  public class KernelTimeConfig
  {
    public KernelTimeConfig()
    {}

    /// <summary>
    /// �а�ʱ�� ���� �ƻ�����ʱ�� ������
    /// </summary>
    public int nMinCallBeforeChuQing;

    /// <summary>
    /// վ�ӷ�ʽ �ƻ�����ʱ�� ���� �ƻ�����ʱ�� ������
    /// </summary>
    public int nMinChuQingBeforeStartTrain_Z;

    /// <summary>
    /// ��ӷ�ʽ �ƻ�����ʱ�� ���� �ƻ�����ʱ�� ������
    /// </summary>
    public int nMinChuQingBeforeStartTrain_K;

    /// <summary>
    /// �װ࿪ʼʱ�� ����� 0��ķ�����
    /// </summary>
    public int nMinDayWorkStart;

    /// <summary>
    /// ҹ�࿪ʼʱ�� ����� 0��ķ�����
    /// </summary>
    public int nMinNightWokrStart;

  }
}
