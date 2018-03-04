using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.CommonUtility;

namespace TF.RunSafty.WorkSteps
{
    public class WorkType
    {
        /// <summary>
        /// 记名式传达
        /// </summary>
        public const string WorkReading = "RS.STEP.WORKREADING";
        /// <summary>
        /// 个性化出勤
        /// </summary>
        public const string Gxhcq = "RS.STEP.PRINT.GXHCQ";
        /// <summary>
        /// 验卡
        /// </summary>
        public const string CheckCard = "RS.STEP.CHECKCARD";
        /// <summary>
        /// 勾划
        /// </summary>
        public const string PubJiShiGouHua = "RS.STEP.PUBJIESHI.GOUHUA";
        /// <summary>
        /// 阅读
        /// </summary>
        public const string PubJiShiRead = "RS.STEP.PUBJIESHI.READ";
        /// <summary>
        /// 测酒
        /// </summary>
        public const string Drink = "RS.STEP.DRINKTEST";
        /// <summary>
        /// 转储
        /// </summary>
        public const string TQZhuanChu = "RS.STEP.ICCARDRUNRECORD";
    }

    #region 类工厂，实现每一个步骤提交的方法

    //简单工厂，通过判断参数，实例化相应的对象  
    public class OperationFactoty
    {
        public static Operate createOperate(string operate, int nWorkTypeID)
        {
            Operate oper = null;
            switch (operate)
            {
                case WorkType.WorkReading:
                    oper = new WorkReading();
                    break;
                case WorkType.Gxhcq:
                    oper = new Gxhcq();
                    break;
                case WorkType.CheckCard:
                    oper = new CheckCard();
                    break;
                case WorkType.PubJiShiGouHua:
                    oper = new PubJiShiGouHua();
                    break;
                case WorkType.PubJiShiRead:
                    oper = new PubJiShiRead();
                    break;
                case WorkType.Drink:
                    oper = new Drink();
                    break;
                case WorkType.TQZhuanChu:
                    oper = new TQZhuanChu();
                    break;
                    　default :
                    oper = new QiTa();
                    break;
            }
            return oper;
        }
      //  private WorkReading a;

    }

  
    #endregion
}
