using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Api.Utilities;

namespace  TF.RunSafty.Entry
{
    public class InterfaceOutPut
    {
        public int result;
        public string resultStr;
        public object data;
    }

}

namespace TF.RunSafty.Model.InterfaceModel
{
    /// <summary>
    /// 接口参数实体类
    /// </summary>
    public class ParamBase
    {
        public string cid;
    }

    public class ResultJsonBase
    {
        public int result;
        public string resultStr;
    }

    public class JsonOutBase
    {
        public string result = "";
        public string resultStr = "";
    }

    #region 枚举类型

    /// <summary>
    /// 计划状态枚举
    /// </summary>
    public enum TRsPlanState
    {
        psCancel,
        psEdit,
        psSent,
        psReceive,
        psPublish,
        psInRoom,
        psOutRoom,
        psBeginWork,
        psEndWork,
        psErrorFinish
    }

    public enum TRsCallWorkState
    {
        cwsNil = 1,
        cwsCommit,
        cwsSend,
        cwsRecv
    }

    /// <summary>
    /// 司机状态
    /// </summary>
    public enum TRsTrainmanState
    {
        tsUnRuning,
        tsReady,
        tsNormal,
        tsPlaning,
        tsInRoom,
        tsOutRoom,
        tsRuning,
        tsNil
    }

    public enum TRsJiaoluType
    {
        jltAny = -1,
        jltUnrun,
        jltReady,
        jltNamed,
        jltOrder,
        jltTogether
    }

    public enum TRsGroupType
    {
        NamedGroup,
        OrderGroup
    }

    //岗位枚举
    public enum TRsSiteJob
    {
        sjAdmin,
        sjDiaodu,
        sjPaiBan,
        sjChuQin,
        sjTuiQin,
        sjHouBan,
        sjGuanLi,
        sjDuanWang
    }

    //操作权限枚举
    public enum TRsJobLimit
    {
        jlBrowser,
        jlOperate
    }

    public enum TRsWorkTypeID
    {
        wtBeginWork = 2,
        wtEndWork = 3,
        wtForeign = 10
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
    #endregion

    public class ParamModel
    {
        public string cid;
        public TF.RunSafty.Model.TAB_Base_TrainNo_Json data;
    }
    public class PlanRestModel
    {
        public string cid;
        public TF.RunSafty.Model.TAB_Base_PlanRest_Json data;
    }

    public class rests
    {
        public string nNeedRest = "";
        public string dtArriveTime = "";
        public string dtCallTime = "";
    }
    public class PlanToTakes
    {
        [NotNull]
        public string strPlanGUID { get; set; }
        public string strCheCi { get; set; }
        public string dtCallWorkTime { get; set; }
        public string dtWaitWorkTime { get; set; }
        public string strTrainmanGUID1 { get; set; }
        public string strTrainmanGUID2 { get; set; }
        public string strTrainmanGUID3 { get; set; }
        public string strTrainmanGUID4 { get; set; }

    }


    public class PlanSigns
    {
        public string cid;
        public SignPlanGUIDs data;


    }

    public class SignPlanGUIDs
    {
        public PlanSign SignPlan;


    }


    //签点计划
    public class PlanSign
    {

        public string strTrainJiaoLuGUID { get; set; }
        public string dtArriveTime { get; set; }
        public string dtCallTime { get; set; }
        public string strTrainNo { get; set; }
        public string dtChuQinTime { get; set; }
        public string dtStartTrainTime { get; set; }
        public int nNeedRest { get; set; }

        public string strGUID { get; set; }



    }

    //入寓记录
    public class PlanInRoom
    {
        public string strInRoomGUID { get; set; }
        public string strTrainPlanGUID { get; set; }
        public string strTrainmanGUID { get; set; }
        public DateTime dtInRoomTime { get; set; }
        public int nInRoomVerifyID { get; set; }
        public string strDutyUserGUID { get; set; }
        public string strTrainmanNumber { get; set; }
        public DateTime dtCreateTime { get; set; }
        public string strSiteGUID { get; set; }
        public string strRoomNumber { get; set; }
        public int nBedNumber { get; set; }
        public DateTime dtArriveTime { get; set; }
        public string strWaitPlanGUID { get; set; }
        public int ePlanType { get; set; }
    }


    //出寓记录
    public class PlanOutRoom
    {
        public string strOutRoomGUID { get; set; }
        public string strTrainPlanGUID { get; set; }
        public string strTrainmanGUID { get; set; }
        public DateTime dtOutRoomTime { get; set; }
        public int nOutRoomVerifyID { get; set; }
        public string strDutyUserGUID { get; set; }
        public string strTrainmanNumber { get; set; }
        public DateTime dtCreateTime { get; set; }
        public string strInRoomGUID { get; set; }
        public string strSiteGUID { get; set; }
        public DateTime dtArriveTime { get; set; }
        public string strWaitPlanGUID { get; set; }
        public int ePlanType { get; set; }
        public int nBedNumber { get; set; }
        public string strRoomNumber { get; set; }
    }

    public class Trainman
    {
        public string trainmanID = "";
        public string trainmanNumber = "";
        public string trainmanName = "";
        public int postID;
        public string postName = "";
        public int driverTypeID;
        public string driverTypeName = "";
        public string ABCD = "";
        public int isKey;
        public string telNumber = "";
        public int trainmanState = 0;
        public int callWorkState = 1;
        public string callWorkID = "";
    }
    public class TrainmanList : List<Trainman>
    {
    }
    public class NameGroup
    {
        public string groupID = "";
        public int groupState;
        public string trainPlanID = "";
        public string arriveTime = "";
        public string lastInRoomTime1 = "";
        public string lastInRoomTime2 = "";
        public string lastInRoomTime3 = "";
        public string lastInRoomTime4 = "";
        public ChuqinPlace place = new ChuqinPlace();
        public Station station = new Station();
        public Trainman trainman1 = new Trainman();
        public Trainman trainman2 = new Trainman();
        public Trainman trainman3 = new Trainman();
        public Trainman trainman4 = new Trainman();
    }

    public class PlansOfClient
    {
        public TrainPlan trainPlan;
        public NameGroup group;
        public rests rest;
    }

    public class TuiqinGroup
    {
        public NameGroup group = new NameGroup();
        public int verifyID1;
        public TestAlcoholInfo testAlcoholInfo1 = new TestAlcoholInfo();
        public int verifyID2;
        public TestAlcoholInfo testAlcoholInfo2 = new TestAlcoholInfo();
        public int verifyID3;
        public TestAlcoholInfo testAlcoholInfo3 = new TestAlcoholInfo();
        public int verifyID4;
        public TestAlcoholInfo testAlcoholInfo4 = new TestAlcoholInfo();
        public string turnStartTime = "";
        public string signed = "";
        public string isOver = "";
        public string turnMinutes = "";
        public string turnAlarmMinutes = "";
    }

    public class mTuiqinPlansOfSite
    {
        public TrainPlan trainPlan = new TrainPlan();
        public string beginWorkTime = "";
        public TuiqinGroup tuiqinGroup = new TuiqinGroup();

    }


    /// <summary>
    /// 出勤计划
    /// </summary>
    public class mChuqinPlansOfClient
    {
        public TrainPlan trainPlan;
        public string beginWorkTime = "";
        public string icCheckResult = "";
        public ChuqinGroup chuqinGroup;
        public int nFlowState;

    }

    public class TestAlcoholInfo
    {
        public int testAlcoholResult;
        public string picture = "";
        public string testTime = "";
    }

    public class ChuqinGroup
    {
        public NameGroup group;
        public int verifyID1;
        public TestAlcoholInfo testAlcoholInfo1;
        public int verifyID2;
        public TestAlcoholInfo testAlcoholInfo2;
        public int verifyID3;
        public TestAlcoholInfo testAlcoholInfo3;
        public int verifyID4;
        public TestAlcoholInfo testAlcoholInfo4;
        public string chuQinMemo = "";
    }

    public class ChuqinTrainman
    {

    }

    public class Station
    {
        public string stationID = "";
        public string stationNumber = "";
        public string stationName = "";
    }

    public class ChuqinPlace
    {
        public string placeID = "";
        public string placeName = "";
    }

    public class TrainPlan
    {
        public string planStateID = "";
        public string planStateName = "";
        public string placeID = "";
        public string placeName = "";
        public string trainJiaoluGUID = "";
        public string trainJiaoluName = "";
        public string trainTypeName = "";
        public string trainNumber = "";
        public string trainNo = "";
        public string startTime = "";
        public string realStartTime = "";
        public string firstStartTime = "";
        public string kaiCheTime = "";
        public string startStationID = "";
        public string startStationName = "";
        public string endStationID = "";
        public string endStationName = "";
        public string trainmanTypeID = "";
        public string trainmanTypeName = "";
        public string planTypeID = "";
        public string planTypeName = "";
        public string dragTypeID = "";
        public string dragTypeName = "";
        public string kehuoID = "";
        public string kehuoName = "";
        public string remarkTypeID = "";
        public string remarkTypeName = "";
        public string strRemark = "";
        public string planID = "";
        public string createTime = "";
        public string createSiteGUID = "";
        public string createSiteName = "";
        public string createUserGUID = "";
        public string createUserName = "";
        public string mainPlanGUID = "";
        public string strTrainPlanGUID = "";
        public string lastArriveTime = "";
        public string beginWorkTime = "";
        public string dtArriveTime = "";
        public string dtCallTime = "";
        public int nNeedRest;

        public string strTrackNumber = "";
        public string strWaiQinClientGUID = "";
        public string strWaiQinClientNumber = "";
        public string strWaiQinClientName = "";
        public string sendPlanTime = "";
        public string recvPlanTime = "";
    }

    public class CreatedTrainPlan
    {
        public string trainjiaoluID = "";
        public string placeID = "";
        public string trainTypeName = "";
        public string trainNumber = "";
        public string trainNo = "";
        public string startTime = "";
        public string kaiCheTime = "";
        public string startStationID = "";
        public string endStationID = "";
        public string trainmanTypeID = "";
        public string planTypeID = "";
        public string dragTypeID = "";
        public string kehuoID = "";
        public string remarkTypeID = "";
        public string strMainPlanGUID = "";

        public string strTrackNumber = "";
        public string strWaiQinClientGUID = "";
        public string strWaiQinClientNumber = "";
        public string strWaiQinClientName = "";
    }

    /// <summary>
    ///出乘计划
    /// </summary>
    public class PlanBeginWork
    {

        public string strCheCi = "";
        public string dtCallWorkTime = "";
        public string dtWaitWorkTime = "";
        public string strTrainmanGUID1 = "";
        public string strTrainmanGUID2 = "";
        public string strTrainmanGUID3 = "";
        public string strTrainmanGUID4 = "";
        public string NNeedRest = "";
        public string NPlanState = "";

        public string strTrainmanNumber1 = "";
        public string strTrainmanNumber2 = "";
        public string strTrainmanNumber3 = "";
        public string strTrainmanNumber4 = "";


        public string strTrainmanName1 = "";
        public string strTrainmanName2 = "";
        public string strTrainmanName3 = "";
        public string strTrainmanName4 = "";
    }


    /// <summary>
    ///行车计划
    /// </summary>
    public class GetTrainPlanById
    {

        public string strTrainNo = "";
        public string dtCallTime = "";
        public string dtStartTime = "";
        public string dtChuqinTime = "";
        public string strStartStation = "";

        public string strWorkShopGUID = "";
        public string strTrainJiaoluGUID = "";


        public string strTrainmanGUID1 = "";
        public string strTrainmanGUID2 = "";
        public string strTrainmanGUID3 = "";
        public string strTrainmanGUID4 = "";

        public string strTrainmanNumber1 = "";
        public string strTrainmanNumber2 = "";
        public string strTrainmanNumber3 = "";
        public string strTrainmanNumber4 = "";


        public string strTrainmanName1 = "";
        public string strTrainmanName2 = "";
        public string strTrainmanName3 = "";
        public string strTrainmanName4 = "";
    }

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
        public NameGroup group = new NameGroup();
    }

    public class RRsTrainJiaolu
    {
        public string strTrainJiaoluGUID = string.Empty;
        public string strTrainJiaoluName = string.Empty;
        public string strStartStation = string.Empty;
        public string strEndStation = string.Empty;
        public string strWorkShopGUID = string.Empty;
        public int bIsBeginWorkFP;
        public string strStartStationName = string.Empty;
        public string strEndStationName = string.Empty;
    }

    public class RRsWorkShop
    {
        public string strWorkShopGUID = string.Empty;
        public string strAreaGUID = string.Empty;
        public string strWorkShopName = string.Empty;
    }

    public class TRsDrinkType
    {
        public int nDrinkTypeID;
        public string strDrinkTypeName = string.Empty;
    }

    public class TRsVerify
    {
        public int nVerifyID;
        public string strVerifyName = string.Empty;
    }

    public class TRsDrinkResult
    {
        public int nDrinkResult;
        public string strDrinkResultName = string.Empty;
    }

    public class RRsDrink
    {
        public string strGUID = string.Empty;
        public string strTrainmanName = string.Empty;
        public string strTrainmanNumber = string.Empty;
        public string strWorkShopName = string.Empty;
        public int nDrinkResult;
        public string strDrinkResultName = string.Empty;
        public string dtCreateTime = string.Empty;
        public int nVerifyID;
        public string strVerifyName = string.Empty;
        public int nWorkTypeID;
        public string strWorkTypeName = string.Empty;
        public string strPictureURL = string.Empty;
    }

    public class RRsDutyPlace
    {
        public string placeID { get; set; }  //出勤点ID
        public string placeName { get; set; } //出勤点名字
    }

    public class RRsStation
    {
        public string strStationGUID { get; set; }
        public string strStationName { get; set; }
        public string strStationNumber { get; set; }
    }
    public class TRsStationArray:List<RRsStation>{}
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

    public class TRsZheFanQuJianArray : List<RRsZheFanQuJian>
    {
    }

    public class RRsGroup
    {
        public string strGroupGUID { get; set; }
        //出勤低点
        public RRsDutyPlace place { get; set; }
        //机班所在车站
        public RRsStation Station { get; set; }   
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
    public class RRsTrainman
    {
        public RRsTrainman()
        { }

        //乘务员编号
        public string trainmanID = "";

        //乘务员工号
        public string trainmanNumber = "";

        //乘务员姓名
        public string trainmanName = "";

        //职务编号
        public int postID;

        //职务
        public string postName = "";

        //司机类型
        public int driverTypeID;

        //bIsKey
        public int isKey;

        //strABCD
        public string ABCD = "";

        //客货
        public int nKeHuoID;

        //乘务员状态
        public int trainmanState;

        //司机类型名称
        public string driverTypeName = "";

        //电话号码
        public string telNumber = "";

        //叫班状态
        public int callWorkState;

        //叫班编号
        public string callWorkID = "";
        //请假类型编号
        public string strLeaveTypeGUID;

        //请假类型名称
        public string strLeaveTypeName;

    }

    public class RRsTrainmanList : List<RRsTrainman>
    {
    }
    public enum TRsPost { ptNone, ptTrainman, ptSubTrainman, ptLearning ,}
    public class RRsTrainmanFull
    {
        public string strTrainmanGUID{get;set;}
        public string strTrainmanName{get;set;}
        public string strTrainmanNumber{get;set;}
        public TRsPost nPostID { get; set; }
    public string strPostName {get;set;}
        public string strWorkShopGUID {get;set;}
        public string strWorkShopName  {get;set;}
        public string FingerPrint1 {get;set;}
    public string FingerPrint2 {get;set;}
        public string Picture {get;set;}
        public string strGuideGroupGUID {get;set;}
        public string strGuideGroupName {get;set;}
    public string strTelNumber{get;set;}
        public string strMobileNumber {get;set;}
        public string strAdddress  {get;set;}
        public TRsDriverType nDriverType {get;set;}
    public string strDriverTypeName{get;set;}
        public TRsCallWorkState  nCallWorkState {get;set;}
        public string strCallWorkGUID{get;set;}
        public int bIsKey {get;set;}
    public DateTime dtRuZhiTime {get;set;}
        public DateTime dtJiuZhiTime {get;set;}
        public int nDriverLevel {get;set;}
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
}
