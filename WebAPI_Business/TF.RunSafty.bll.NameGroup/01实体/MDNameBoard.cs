using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.NamePlate.MD
{    
    public class Group
    {
        public string groupID = "";
        public int groupState;
        public string trainPlanID = "";
        public string trainNo = "";
        public string trainTypeName = "";
        public string trainNumber = "";
        public string startTime = "";
        public string arriveTime = "";
        public string lastInRoomTime1 = "";
        public string lastInRoomTime2 = "";
        public string lastInRoomTime3 = "";
        public string lastInRoomTime4 = "";
        public string LastOutRoomTime1 = "";
        public string LastOutRoomTime2 = "";
        public string LastOutRoomTime3 = "";
        public string LastOutRoomTime4 = "";
        public ChuqinPlace place = new ChuqinPlace();
        public Station station = new Station();
        public Trainman trainman1 = new Trainman();
        public Trainman trainman2 = new Trainman();
        public Trainman trainman3 = new Trainman();
        public Trainman trainman4 = new Trainman();
        public int nTxState = 0;
        public DateTime dtTXBeginTime;

    }
    public class GroupList : List<Group>
    {}
    public class RRsZheFanQuJian
    {
        public string strZFQJGUID { get; set; }
        public string strTrainJiaoluGUID { get; set; }
        public int nQuJianIndex { get; set; }
        public string strBeginStationGUID { get; set; }
        public string strBeginStationName { get; set; }
        public string strEndStationGUID { get; set; }
        public string strEndStationName { get; set; }
    }
    public class RRsGroup
    {
        public string strGroupGUID { get; set; }
        //出勤低点
        public ChuqinPlace place { get; set; }
        //机班所在车站
        public Station Station { get; set; }
        //所在折返区间的GUID
        public RRsZheFanQuJian ZFQJ { get; set; }
        //正司机
        public RRsTrainmanFull Trainman1 { get; set; }
        public RRsTrainmanFull Trainman2 { get; set; }
        public RRsTrainmanFull Trainman3 { get; set; }
        public RRsTrainmanFull Trainman4 { get; set; }
        //机组状态
        public TRsTrainmanState GroupState { get; set; }
        //当前值乘的计划的GUID
        public string strTrainPlanGUID { get; set; }
        //最近到达时间
        public DateTime dtArriveTime { get; set; }
        //四i最近一次入寓时间
        public DateTime dtLastInRoomTime1 { get; set; }
        //司机2最近一次入寓时间
        public DateTime dtLastInRoomTime2 { get; set; }
        //学员最后一次入寓时间
        public DateTime dtLastInRoomTime3 { get; set; }
        //学员最后一次入寓时间
        public DateTime dtLastInRoomTime4 { get; set; }
    }
    public class RRsNamedGroup
    {
        public RRsNamedGroup()
        { }

        //车次GUID
        public string strCheciGUID = "";

        //所属人员交路GUID
        public string strTrainmanJiaoluGUID = "";

        //交路内序号
        public int nCheciOrder;

        //车次类型
        public int nCheciType;

        //车次1
        public string strCheci1 = "";

        //车次2
        public string strCheci2 = "";

        //当班机组
        public Group Group = new Group();

        //最后一次到达时间
        public DateTime dtLastArriveTime;

    }
    public class NameGroupList : List<RRsNamedGroup>
    {
    }
    public class RRsTrainmanFull
    {
        public string strTrainmanGUID { get; set; }
        public string strTrainmanName { get; set; }
        public string strTrainmanNumber { get; set; }
        public TRsPost nPostID { get; set; }
        public string strPostName { get; set; }
        public string strWorkShopGUID { get; set; }
        public string strWorkShopName { get; set; }
        public string FingerPrint1 { get; set; }
        public string FingerPrint2 { get; set; }
        public string Picture { get; set; }
        public string strGuideGroupGUID { get; set; }
        public string strGuideGroupName { get; set; }
        public string strTelNumber { get; set; }
        public string strMobileNumber { get; set; }
        public string strAdddress { get; set; }
        public TRsDriverType nDriverType { get; set; }
        public string strDriverTypeName { get; set; }
        public TRsCallWorkState nCallWorkState { get; set; }
        public string strCallWorkGUID { get; set; }
        public int bIsKey { get; set; }
        public DateTime dtRuZhiTime { get; set; }
        public DateTime dtJiuZhiTime { get; set; }
        public int nDriverLevel { get; set; }
        public string strABCD { get; set; }
        public string strRemark { get; set; }
        public TRsKehuo nKeHuoID { get; set; }
        public string strKeHuoName { get; set; }
        public string strTrainJiaoluGUID { get; set; }
        public string strTrainmanJiaoluGUID { get; set; }
        public string strTrainJiaoluName { get; set; }
        public DateTime dtLastEndworkTime { get; set; }
        public int nID { get; set; }
        public DateTime dtCreateTime { get; set; }
        public TRsTrainmanState nTrainmanState { get; set; }
        public string strJP { get; set; }
    }
    /// <summary>
    /// 客货类型
    /// </summary>
    public enum TRsKehuo
    {
        khNone,
        khKe,
        khHuo,
        khDiao
    }
    /// <summary>
    /// 驾驶工种
    /// </summary>
    public enum TRsDriverType
    {
        drtNone, drtNeiran, drtDian, drtDong
    }
    public enum TRsCallWorkState
    {
        cwsNil = 1,
        cwsCommit,
        cwsSend,
        cwsRecv
    }
    public enum TRsPost { ptNone, ptTrainman, ptSubTrainman, ptLearning, }
    /// <summary>
    /// 轮乘机组信息
    /// </summary>
    public class OrderGroup
    {
        //轮乘机组ID
        public string orderID = "";
        //所属交路GUID
        public string trainmanjiaoluID = "";
        //序号
        public int order = 0;
        //最后一次到达时间
        public string lastArriveTime = "";
        //机组信息
        public Group group = new Group();
    }

    public class RRsTogetherTrain
    {
        public RRsTogetherTrain()
        { }

        //包乘机车GUID
        public string strTrainGUID;

        //所属交路GUID
        public string strTrainmanJiaoluGUID;

        //机车类型
        public string strTrainTypeName;

        //机车号
        public string strTrainNumber;

        //包含的机组
        public TRsOrderGroupInTrainArray Groups = new TRsOrderGroupInTrainArray();

    }

    public class RRsOrderGroupInTrain
    {
        public RRsOrderGroupInTrain()
        { }

        //排序GUID
        public string strOrderGUID;

        //所属机车GUID
        public string strTrainGUID;

        //序号
        public int nOrder;

        //最后一次到达时间
        public DateTime dtLastArriveTime;

        //当班机组
        public Group Group = new Group();

    }


    public class TRsTogetherTrainArray : List<RRsTogetherTrain>
    {
    }

    public class TRsOrderGroupInTrainArray : List<RRsOrderGroupInTrain>
    {
    }
  
}
