using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.Plan.MD
{
  /// <summary>
  ///����: MealTicketCheCi
  ///˵��: ���ι���
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
    /// ��·����
    /// </summary>
    public int iType;

    /// <summary>
    /// ����
    /// </summary>
    public string strQuDuan;

    /// <summary>
    /// ����
    /// </summary>
    public string strCheCi;

    /// <summary>
    /// ��ʼʱ�䰮��
    /// </summary>
    public DateTime dtStartTime;

    /// <summary>
    /// ����ʱ��
    /// </summary>
    public DateTime dtEndTime;

    /// <summary>
    /// ����
    /// </summary>
    public string strRuleGUID;

  }

  /// <summary>
  ///����: MealTicketCheCiList
  ///˵��: ���ι����б�
  /// </summary>
  public class MealTicketCheCiList : List<MealTicketCheCi>
  {
      public MealTicketCheCiList()
      { }

  }

  /// <summary>
  ///����: MealTicketPerson
  ///˵��: ��Ʊ����
  /// </summary>
  public class MealTicketPerson
  {
      public MealTicketPerson()
      { }

      /// <summary>
      /// ����
      /// </summary>
      public string strTrainmanNumber;

      /// <summary>
      /// ��������GUID
      /// </summary>
      public string strWorkShopGUID;

      /// <summary>
      /// ��������
      /// </summary>
      public string strQuDuan;

      /// <summary>
      /// ��������
      /// </summary>
      public string strCheCi;

      /// <summary>
      /// �ɰ�ʱ��
      /// </summary>
      public DateTime dtPaiBan;

  }

  /// <summary>
  ///����: MealTicketRule
  ///˵��: ��Ʊ����
  /// </summary>
  public class MealTicketRule
  {
      public MealTicketRule()
      { }

      /// <summary>
      /// ��¼GUID
      /// </summary>
      public string strGUID;

      /// <summary>
      /// ��������
      /// </summary>
      public string strName;

      /// <summary>
      /// ����GUID
      /// </summary>
      public string strWorkShopGUID;

      /// <summary>
      /// ��·����
      /// </summary>
      public int iType;

      /// <summary>
      /// ���ȯ��
      /// </summary>
      public int iA;

      /// <summary>
      /// ����ȯ��
      /// </summary>
      public int iB;

  }

  /// <summary>
  ///����: MealTicketRuleList
  ///˵��: ��Ʊ�����б�
  /// </summary>
  public class MealTicketRuleList : List<MealTicketRule>
  {
      public MealTicketRuleList()
      { }

  }


  /// <summary>
  ///����: MealTicket
  ///˵��: ��Ʊ��Ϣ
  /// </summary>
  public class MealTicket
  {
      public MealTicket()
      { }

      /// <summary>
      /// �о���
      /// </summary>
      public int CANJUAN_INFO_ID;

      /// <summary>
      /// PAIBAN_INFO_ID
      /// </summary>
      public int PAIBAN_INFO_ID;

      /// <summary>
      /// ʱ��+����
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
      /// ��ͷ�Ʊ����
      /// </summary>
      public int CANQUAN_A;

      /// <summary>
      /// ���ͷ�Ʊ����
      /// </summary>
      public int CANQUAN_B;

      /// <summary>
      /// ��ͷ�Ʊ����
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
