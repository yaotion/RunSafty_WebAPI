using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.KeyMan
{
    public enum EKeyTrainmanOpt
    { KTMAdd, KTMModify, KTMdel };

    public class LCOutput
    {
        public void Clear()
        {
            result = 0;
            resultStr = string.Empty;
            data = null;
        }
        public int result;
        public string resultStr = string.Empty;
        public object data;
        
    }
    public class KMQueryQC
    { 
        //车间编号
        public string WorkShopGUID;
        //车队编号
        public string CheDuiGUID;
        //关键人工号
        public string KeyTMNumber;
        //关键人姓名
        public string KeyTMName;
        //登记开始时间
        public DateTime? RegisterStartTime;
        //登记截止时间
        public DateTime? RegisterEndTime;
    }
    public class KeyMan
    {
        //id
        public string ID;
        //关键人工号
        public string KeyTMNumber;
        //关键人姓名
        public string KeyTMName;
        //关键人所属车间id
        public string KeyTMWorkShopGUID;
        //关键人所属车间名称
        public string KeyTMWorkShopName;
        //关键人所属车队id
        public string KeyTMCheDuiGUID;
        //关键人所属车队名称
        public string KeyTMCheDuiName;
        //关键人开始时间
        public DateTime? KeyStartTime;
        //关键人截止时间
        public DateTime? KeyEndTime;
        //登记原因
        public string KeyReason;
        //登记注意事项
        public string KeyAnnouncements;
        //登记人工号
        public string RegisterNumber;
        //登记人姓名
        public string RegisterName;
        //登记日期
        public DateTime? RegisterTime;
        //客户端编号
        public string ClientNumber;
        //客户端名称
        public string ClientName;
        //操作类型
        public int eOpt;
    }
}
