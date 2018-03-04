using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.Plan.MD
{
  /// <summary>
  ///类名: MealTicketCheCi
  ///说明: 车次规则
  /// </summary>
  public class MealTicketCheCi
  {
    public MealTicketCheCi()
    {}

    /// <summary>
    /// strGUID
    /// </summary>
    public string strGUID;

    /// <summary>
    /// strWorkShopGUID
    /// </summary>
    public string strWorkShopGUID;

    /// <summary>
    /// 交路类型
    /// </summary>
    public int iType;

    /// <summary>
    /// 区段
    /// </summary>
    public string strQuDuan;

    /// <summary>
    /// 车次
    /// </summary>
    public string strCheCi;

    /// <summary>
    /// 开始时间爱你
    /// </summary>
    public DateTime dtStartTime;

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime dtEndTime;

    /// <summary>
    /// 规则
    /// </summary>
    public string strRuleGUID;

  }

  /// <summary>
  ///类名: MealTicketCheCiList
  ///说明: 车次规则列表
  /// </summary>
  public class MealTicketCheCiList : List<MealTicketCheCi>
  {
      public MealTicketCheCiList()
      { }

  }

  /// <summary>
  ///类名: MealTicketPerson
  ///说明: 饭票发放
  /// </summary>
  public class MealTicketPerson
  {
      public MealTicketPerson()
      { }

      /// <summary>
      /// 工号
      /// </summary>
      public string strTrainmanNumber;

      /// <summary>
      /// 所属车间GUID
      /// </summary>
      public string strWorkShopGUID;

      /// <summary>
      /// 担当区段
      /// </summary>
      public string strQuDuan;

      /// <summary>
      /// 担当车次
      /// </summary>
      public string strCheCi;

      /// <summary>
      /// 派班时间
      /// </summary>
      public DateTime dtPaiBan;

  }

  /// <summary>
  ///类名: MealTicketRule
  ///说明: 饭票规则
  /// </summary>
  public class MealTicketRule
  {
      public MealTicketRule()
      { }

      /// <summary>
      /// 记录GUID
      /// </summary>
      public string strGUID;

      /// <summary>
      /// 规则名称
      /// </summary>
      public string strName;

      /// <summary>
      /// 车间GUID
      /// </summary>
      public string strWorkShopGUID;

      /// <summary>
      /// 交路类型
      /// </summary>
      public int iType;

      /// <summary>
      /// 早餐券数
      /// </summary>
      public int iA;

      /// <summary>
      /// 正餐券数
      /// </summary>
      public int iB;

  }

  /// <summary>
  ///类名: MealTicketRuleList
  ///说明: 饭票规则列表
  /// </summary>
  public class MealTicketRuleList : List<MealTicketRule>
  {
      public MealTicketRuleList()
      { }

  }


  /// <summary>
  ///类名: MealTicket
  ///说明: 饭票信息
  /// </summary>
  public class MealTicket
  {
      public MealTicket()
      { }

      /// <summary>
      /// 残卷编号
      /// </summary>
      public int CANJUAN_INFO_ID;

      /// <summary>
      /// PAIBAN_INFO_ID
      /// </summary>
      public int PAIBAN_INFO_ID;

      /// <summary>
      /// 时间+车次
      /// </summary>
      public string PAIBAN_STR;

      /// <summary>
      /// DRIVER_CODE
      /// </summary>
      public string DRIVER_CODE;

      /// <summary>
      /// DRIVER_NAME
      /// </summary>
      public string DRIVER_NAME;

      /// <summary>
      /// CHEJIAN_NAME
      /// </summary>
      public string CHEJIAN_NAME;

      /// <summary>
      /// 早餐饭票张数
      /// </summary>
      public int CANQUAN_A;

      /// <summary>
      /// 正餐饭票张数
      /// </summary>
      public int CANQUAN_B;

      /// <summary>
      /// 晚餐饭票张数
      /// </summary>
      public int CANQUAN_C;

      /// <summary>
      /// PAIBAN_CHECI
      /// </summary>
      public string PAIBAN_CHECI;

      /// <summary>
      /// CHUQIN_TIME
      /// </summary>
      public string CHUQIN_TIME;

      /// <summary>
      /// CHUQIN_YEAR
      /// </summary>
      public int CHUQIN_YEAR;

      /// <summary>
      /// CHUQIN_MONTH
      /// </summary>
      public int CHUQIN_MONTH;

      /// <summary>
      /// CHUQIN_DAY
      /// </summary>
      public int CHUQIN_DAY;

      /// <summary>
      /// CHUQIN_YMD
      /// </summary>
      public int CHUQIN_YMD;

      /// <summary>
      /// CHUQIN_DEPART
      /// </summary>
      public string CHUQIN_DEPART;

      /// <summary>
      /// SHENHEREN_CODE
      /// </summary>
      public string SHENHEREN_CODE;

      /// <summary>
      /// SHENHEREN_NAME
      /// </summary>
      public string SHENHEREN_NAME;

      /// <summary>
      /// CHECK_FLAG
      /// </summary>
      public int CHECK_FLAG;

      /// <summary>
      /// REC_TIME
      /// </summary>
      public string REC_TIME;

  }           
}
