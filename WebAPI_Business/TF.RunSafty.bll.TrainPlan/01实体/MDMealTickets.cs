using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Plan
{
     public class MDMealTicket
    {
         public int ID { get; set; }
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
