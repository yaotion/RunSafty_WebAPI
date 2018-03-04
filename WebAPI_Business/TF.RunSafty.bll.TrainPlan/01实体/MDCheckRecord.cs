using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.Plan.MD
{
  /// <summary>
  ///类名: CheckRecord
  ///说明: 出勤卡控项
  /// </summary>
  public class CheckRecord
  {
    public CheckRecord()
    {}

    /// <summary>
    /// 项编号
    /// </summary>
    public int nPointID;

    /// <summary>
    /// 项名称
    /// </summary>
    public string strPointName;

    /// <summary>
    /// 是否卡空
    /// </summary>
    public int nIsHold;

    /// <summary>
    /// 工号
    /// </summary>
    public string strTrainmanNumber;

    /// <summary>
    /// 描述
    /// </summary>
    public string strItemContent;

    /// <summary>
    /// 卡控结果
    /// </summary>
    public int nCheckResult;

    /// <summary>
    /// 检测时间
    /// </summary>
    public DateTime dtCheckTime;

    /// <summary>
    /// 生成时间
    /// </summary>
    public DateTime dtCreateTime;

  }

  /// <summary>
  ///类名: CheckRecordList
  ///说明: 卡控项列表
  /// </summary>
  public class CheckRecordList : List<CheckRecord>
  {
      public CheckRecordList()
      { }

  }
}
