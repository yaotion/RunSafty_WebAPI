using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
  /// <summary>
  ///类名: BoardChangeLog
  ///说明: 名牌变动日志
  /// </summary>
  public class BoardChangeLog
  {
    public BoardChangeLog()
    {}

    /// <summary>
    /// GUID
    /// </summary>
    public string strLogGUID;

    /// <summary>
    /// 人员交路GUID
    /// </summary>
    public string strTrainmanJiaoluGUID;

    /// <summary>
    /// 人员交路名称
    /// </summary>
    public string strTrainmanJiaoluName;

    /// <summary>
    /// 变动类型
    /// </summary>
    public int nBoardChangeType;

    /// <summary>
    /// 变动内容
    /// </summary>
    public string strContent;

    /// <summary>
    /// 值班员GUID
    /// </summary>
    public string strDutyUserGUID;

    /// <summary>
    /// 值班员工号
    /// </summary>
    public string strDutyUserNumber;

    /// <summary>
    /// 值班员姓名
    /// </summary>
    public string strDutyUserName;

    /// <summary>
    /// 变动时间
    /// </summary>
    public DateTime dtEventTime;

    /// <summary>
    /// 自增编号
    /// </summary>
    public int nid;

  }


  /// <summary>
  ///类名: BoardChangeLogList
  ///说明: 名牌变动日志列表
  /// </summary>
  public class BoardChangeLogList : List<BoardChangeLog>
  {
      public BoardChangeLogList()
      { }

  }
}
