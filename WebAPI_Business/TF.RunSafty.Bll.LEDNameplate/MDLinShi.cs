using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.LEDNameplate
{

    /// <summary>
    ///类名: TrainPlanMin
    ///说明: 行车计划简单形式
    /// </summary>
    public class TrainPlanMin
    {
        public TrainPlanMin()
        { }

        /// <summary>
        /// 行车计划GUID
        /// </summary>
        public string strTrainPlanGUID;

        /// <summary>
        /// 车次
        /// </summary>
        public string strTrainNo;

        /// <summary>
        /// 计划出勤时间
        /// </summary>
        public DateTime dtChuQinTime;

        /// <summary>
        /// 区段GUID
        /// </summary>
        public string strTrainJiaoluGUID;

        /// <summary>
        /// 行车区段名称
        /// </summary>
        public string strTrainJiaoluName;

        /// <summary>
        /// 计划状态
        /// </summary>
        public int nPlanState;

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
    /// <summary>
    ///类名: DutyUser
    ///说明: 值班员信息
    /// </summary>
    public class DutyUser
    {
        public DutyUser()
        { }

        /// <summary>
        /// 值班员GUID
        /// </summary>
        public string strDutyGUID;

        /// <summary>
        /// 值班员工号
        /// </summary>
        public string strDutyNumber;

        /// <summary>
        /// 值班员姓名
        /// </summary>
        public string strDutyName;

    }

    /// <summary>
    /// 行车区段
    /// </summary>
    public class TrainJiaolu
    {
        public TrainJiaolu()
        { }

        //行车交路GUID
        public string strTrainJiaoluGUID = "";

        //行车交路
        public string strTrainJiaoluName = "";

        //始发站
        public string strStartStation = "";

        //终到站
        public string strEndStation = "";

        //车间GUID
        public string strWorkShopGUID = "";

        //bIsBeginWorkFP
        public int bIsBeginWorkFP;

        //始发站名称
        public string strStartStationName = "";

        //终到站名称
        public string strEndStationName = "";

    }
    /// <summary>
    /// 行车区段列表
    /// </summary>
    public class TrainJiaoluList : List<TrainJiaolu>
    {
    }
    /// <summary>
    /// 人员交路
    /// </summary>
    public class TrainmanJiaolu
    {
        public TrainmanJiaolu()
        { }

        //人员交路编号
        public string strTrainmanJiaoluGUID = "";

        //人员交路
        public string strTrainmanJiaoluName = "";

        //交路类型
        public int nJiaoluType;

        //所属线路
        public string strLineGUID = "";

        //客货类型
        public int nKeHuoID;

        //值乘类型
        public int nTrainmanTypeID;

        //牵引类型
        public int nDragTypeID;

        //交路运行类型
        public int nTrainmanRunType;

        //开始车站GUID
        public string strStartStationGUID = "";

        //开始车站名称
        public string strStartStationName = "";

        //结束车站GUID
        public string strEndStationGUID = "";

        //结束车站名称
        public string strEndStationName = "";

        //所属区域
        public string strAreaGUID = "";

    }
    /// <summary>
    /// 人员交路列表
    /// </summary>
    public class TrainmanJiaoluList : List<TrainmanJiaolu>
    {
    }

    public class TrainmanJiaoluMin
    {
        public string jiaoluID;
        public string jiaoluName;
        public int jiaoluType;
    }

    /// <summary>
    /// 字符串列表
    /// </summary>
    public class Strings : List<string>
    { }

    /// <summary>
    /// 车站
    /// </summary>
    public class Station
    {
        public string stationID = "";
        public string stationNumber = "";
        public string stationName = "";
    }
    /// <summary>
    /// 出勤点
    /// </summary>
    public class ChuqinPlace
    {
        public string placeID = "";
        public string placeName = "";
    }

    /// <summary>
    /// 人员
    /// </summary>
    public class Trainman
    {
        public string trainmanID = "";
        public string trainmanNumber = "";
        public string trainmanName = "";
        public int postID;
        public string postName = "";
        public int driverTypeID;
        public string ABCD = "";
        public int isKey;
        public string telNumber = "";
        public int trainmanState = 0;
        public int nKehuoID = 0;
        public string strFixedGroupID = "";
    }
    /// <summary>
    /// 人员列表
    /// </summary>
    public class TrainmanList : List<Trainman>
    {
    }

    /// <summary>
    /// 用于显示在名牌上的乘务员信息
    /// </summary>
    public class TrainmanNamePlate
    {
        public string trainmanID = "";
        public string trainmanNumber = "";
        public string trainmanName = "";
        public int postID;
        public string postName = "";
        public int driverTypeID;        
        public string ABCD = "";
        public int isKey;
        public string telNumber = "";
        public int trainmanState = 0;        
        public int nKehuoID = 0;
    }

    /// <summary>
    /// 用于显示在名牌上的信息列表
    /// </summary>
    public class TrainmanNamePlateList : List<TrainmanNamePlate>
    {
    }

    public class TrainmanMin
    {
        public string strTrainmanGUID;
        public string strTrainmanNumber;
        public string strTrainmanName;
        public int nTrainmanState;
    }
    public class TrainmanMinList : List<TrainmanMin>
    { }

    /// <summary>
    /// 人员2
    /// </summary>
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

    /// <summary>
    /// 人员2列表
    /// </summary>
    public class RRsTrainmanList : List<RRsTrainman>
    {
    }

    public class JsonOutBase
    {
        public string result = "";
        public string resultStr = "";
    }
    public class InterfaceOutPut
    {
        public int result;
        public string resultStr;
        public object data;
    }
 
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


    /// <summary>
    /// 机车计划
    /// </summary>
    public class TrainPlan
    {
        public TrainPlan()
        { }

        //计划状态
        public int planStateID;

        //计划状态名称
        public string planStateName;

        //出勤点编号
        public string placeID;

        //出勤点名称
        public string placeName;

        //行车区段GUID
        public string trainJiaoluGUID;

        //行车区段
        public string trainJiaoluName;

        //机型
        public string trainTypeName;

        //车号
        public string trainNumber;

        //车次
        public string trainNo;

        //计划出勤时间
        public DateTime startTime;

        //实际出勤时间
        public DateTime realStartTime;

        //firstStartTime
        public DateTime firstStartTime;

        //开车时间
        public DateTime kaiCheTime;

        //出发站编号
        public string startStationID;

        //出发站名称
        public string startStationName;

        //终到站编号
        public string endStationID;

        //终到站名称
        public string endStationName;

        //乘务员类型编号
        public string trainmanTypeID;

        //乘务员类型
        public string trainmanTypeName;

        //计划类型编号
        public string planTypeID;

        //计划类型名称
        public string planTypeName;

        //牵引类型编号
        public string dragTypeID;

        //牵引类型
        public string dragTypeName;

        //客货编号
        public string kehuoID;

        //客货
        public string kehuoName;

        //备注类型编号
        public string remarkTypeID;

        //备注类型
        public string remarkTypeName;

        //备注
        public string strRemark;

        //计划编号
        public string planID;

        //创建时间
        public DateTime createTime;

        //客户端编号
        public string createSiteGUID;

        //客户端名称
        public string createSiteName;

        //用户编号
        public string createUserGUID;

        //用户姓名
        public string createUserName;

        //主计划编号
        public string mainPlanGUID;

        //行车计划编号
        public string strTrainPlanGUID;

        //最后到达时间
        public DateTime lastArriveTime;

    }

}
