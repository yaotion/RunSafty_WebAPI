using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.Plan.MD
{
  /// <summary>
  ///类名: PlanSendLog
  ///说明: 计划下发日志
  /// </summary>
  public class PlanSendLog
  {
    public PlanSendLog()
    {}

    /// <summary>
    /// 车次
    /// </summary>
    public string strTrainNo;

    /// <summary>
    /// 记录GUID
    /// </summary>
    public string strSendGUID;

    /// <summary>
    /// 计划GUID
    /// </summary>
    public string strTrainPlanGUID;

    /// <summary>
    /// 交路名称
    /// </summary>
    public string strTrainJiaoluName;

    /// <summary>
    /// 计划出勤时间
    /// </summary>
    public DateTime dtStartTime;

    /// <summary>
    /// 实际出勤时间
    /// </summary>
    public DateTime dtRealStartTime;

    /// <summary>
    /// 下发客户端名称
    /// </summary>
    public string strSendSiteName;

    /// <summary>
    /// 下发时间
    /// </summary>
    public DateTime dtSendTime;

  }

     /// <summary>
  ///类名: PlanSendLogList
  ///说明: 下发日志列表
  /// </summary>
  public class PlanSendLogList : List<PlanSendLog>
  {
    public PlanSendLogList()
    {}

  }
}
