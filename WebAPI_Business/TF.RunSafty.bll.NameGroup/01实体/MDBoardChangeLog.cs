using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
  /// <summary>
  ///����: BoardChangeLog
  ///˵��: ���Ʊ䶯��־
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
    /// ��Ա��·GUID
    /// </summary>
    public string strTrainmanJiaoluGUID;

    /// <summary>
    /// ��Ա��·����
    /// </summary>
    public string strTrainmanJiaoluName;

    /// <summary>
    /// �䶯����
    /// </summary>
    public int nBoardChangeType;

    /// <summary>
    /// �䶯����
    /// </summary>
    public string strContent;

    /// <summary>
    /// ֵ��ԱGUID
    /// </summary>
    public string strDutyUserGUID;

    /// <summary>
    /// ֵ��Ա����
    /// </summary>
    public string strDutyUserNumber;

    /// <summary>
    /// ֵ��Ա����
    /// </summary>
    public string strDutyUserName;

    /// <summary>
    /// �䶯ʱ��
    /// </summary>
    public DateTime dtEventTime;

    /// <summary>
    /// �������
    /// </summary>
    public int nid;

  }


  /// <summary>
  ///����: BoardChangeLogList
  ///˵��: ���Ʊ䶯��־�б�
  /// </summary>
  public class BoardChangeLogList : List<BoardChangeLog>
  {
      public BoardChangeLogList()
      { }

  }
}
