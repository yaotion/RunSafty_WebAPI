using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.Plan.MD
{


   public enum RoomSignType
   {
       //��Ԣ
       stInRoom,
       //��Ԣ
       stOutRoom
   }
  /// <summary>
  ///����: RoomSign
  ///˵��: ���γ���Ԣ��Ϣ
  /// </summary>
  public class RoomSign
  {
    public RoomSign()
    {}

    /// <summary>
    /// ��¼GUID
    /// </summary>
    public string strInRoomGUID;

    /// <summary>
    /// �����ƻ�GUID
    /// </summary>
    public string strTrainPlanGUID;

    /// <summary>
    /// ����ԱGUID
    /// </summary>
    public string strTrainmanGUID;

    /// <summary>
    /// ��Ԣʱ��
    /// </summary>
    public DateTime dtInRoomTime;

    /// <summary>
    /// ��Ԣ��֤��ʽ
    /// </summary>
    public int nInRoomVerifyID;

    /// <summary>
    /// ֵ��ԱGUID
    /// </summary>
    public string strDutyUserGUID;

    /// <summary>
    /// ����
    /// </summary>
    public string strTrainmanNumber;

    /// <summary>
    /// ����
    /// </summary>
    public string strTrainmanName;

    /// <summary>
    /// ����(0��Ԣ,1��Ԣ);
    /// </summary>
    public int SignType;

  }


      /// <summary>
  ///����: RoomSignList
  ///˵��: ���γ���Ԣ��Ϣ�б�
  /// </summary>
  public class RoomSignList : List<RoomSign>
  {
    public RoomSignList()
    {}

  }
}
