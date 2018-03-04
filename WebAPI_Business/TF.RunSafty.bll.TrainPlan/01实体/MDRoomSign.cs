using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.Plan.MD
{


   public enum RoomSignType
   {
       //入寓
       stInRoom,
       //离寓
       stOutRoom
   }
  /// <summary>
  ///类名: RoomSign
  ///说明: 本段出入寓信息
  /// </summary>
  public class RoomSign
  {
    public RoomSign()
    {}

    /// <summary>
    /// 记录GUID
    /// </summary>
    public string strInRoomGUID;

    /// <summary>
    /// 所属计划GUID
    /// </summary>
    public string strTrainPlanGUID;

    /// <summary>
    /// 乘务员GUID
    /// </summary>
    public string strTrainmanGUID;

    /// <summary>
    /// 入寓时间
    /// </summary>
    public DateTime dtInRoomTime;

    /// <summary>
    /// 入寓验证方式
    /// </summary>
    public int nInRoomVerifyID;

    /// <summary>
    /// 值班员GUID
    /// </summary>
    public string strDutyUserGUID;

    /// <summary>
    /// 工号
    /// </summary>
    public string strTrainmanNumber;

    /// <summary>
    /// 姓名
    /// </summary>
    public string strTrainmanName;

    /// <summary>
    /// 类型(0入寓,1离寓);
    /// </summary>
    public int SignType;

  }


      /// <summary>
  ///类名: RoomSignList
  ///说明: 本段出入寓信息列表
  /// </summary>
  public class RoomSignList : List<RoomSign>
  {
    public RoomSignList()
    {}

  }
}
