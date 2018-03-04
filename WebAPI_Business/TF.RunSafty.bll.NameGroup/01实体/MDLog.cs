using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.NamePlate.MD
{
    /// <summary>
    /// 名牌变动类型
    /// </summary>
    public enum LBoardChangeType
    {
        btNone = 0,
        //{添加人员}
        btcAddTrainman,
        //{删除人员}
        btcDeleteTrainman,
        //{交换人员}
        btcExchangeTrainman,
        //{添加机组}
        btcAddGroup,
        //{删除机组}
        btcDeleteGroup,
        //{交换机组}
        btcExchangeGroup,
        //{交换机组}
        btcChangeGroupOrder,
        //{更改交路}
        btcChangeJiaoLu,
        //清除机组到达时间
        btcClearArriveTime,
        //修改最后到达时间
        btcChangeArriveTime,
            //移动记名式机组
        MovenamedGrp
    }
    public class TuiQinTimeLog
    {
        //机组GUID
        public string strGroupGUID = "";
        public int nType;    //类型    
        public DateTime dtOldArriveTime;  //旧时间
        public DateTime dtNewArriveTime;  //新时间
        public string strDutyUserNumber;  //修改人名字
        public string strDutyUserName;   //修改人名字
        public DateTime dtCreateTime;    //修改时间

    }
    public class TuiQinTimeLogList : List<TuiQinTimeLog>
    {
    }
}
