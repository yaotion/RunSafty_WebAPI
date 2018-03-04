using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.BaseDict
{
  /// <summary>
  ///类名: KernelTimeConfig
  ///说明: 运安系统时间参数
  /// </summary>
  public class KernelTimeConfig
  {
    public KernelTimeConfig()
    {}

    /// <summary>
    /// 叫班时间 早于 计划出勤时间 分钟数
    /// </summary>
    public int nMinCallBeforeChuQing;

    /// <summary>
    /// 站接方式 计划出勤时间 早于 计划开车时间 分钟数
    /// </summary>
    public int nMinChuQingBeforeStartTrain_Z;

    /// <summary>
    /// 库接方式 计划出勤时间 早于 计划开车时间 分钟数
    /// </summary>
    public int nMinChuQingBeforeStartTrain_K;

    /// <summary>
    /// 白班开始时间 相对于 0点的分钟数
    /// </summary>
    public int nMinDayWorkStart;

    /// <summary>
    /// 夜班开始时间 相对于 0点的分钟数
    /// </summary>
    public int nMinNightWokrStart;

  }
}
