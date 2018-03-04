using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Plan
{

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

    public class Station
    {
        public Station()
        { }
        public string stationID = "";
        public string stationNumber = "";
        public string stationName = "";
    }

    public class Trainman
    {
        public Trainman()
        { }
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

    public class DutyPlace
    {
        public string placeID;
        public string placeName;
    }

    public class NameGroup
    {
        public NameGroup()
        { }

        //机组编号
        public string groupID;

        //机组所在出勤点
        public DutyPlace place = new DutyPlace();

        //机组所在车站
        public Station station = new Station();

        //司机信息
        public Trainman trainman1 = new Trainman();

        //副司机信息
        public Trainman trainman2 = new Trainman();

        //学员
        public Trainman trainman3 = new Trainman();

        //学员2
        public Trainman trainman4 = new Trainman();

        public DateTime? LastEndworkTime1;
        public DateTime? LastEndworkTime2;
        public DateTime? LastEndworkTime3;
        public DateTime? LastEndworkTime4;        
    }

    public class TrainmanPlan
    {
        public TrainmanPlan()
        { }

        //机车计划
        public TrainPlan trainPlan = new TrainPlan();

        //机组信息
        public ChuqinGroup chuqinGroup = new ChuqinGroup();
        //beginWorkTime
        public DateTime beginWorkTime;

        //icCheckResult
        public string icCheckResult;

    }

    public class TrainmanPlanList : List<TrainmanPlan>
    {
    }

    public class ChuqinGroup
    {
        public ChuqinGroup()
        { }

        //机组人员信息
        public NameGroup group = new NameGroup();

        //司机身份验证类型
        public int verifyID1;

        //司机1测酒结果
        public TestAlcoholInfo testAlcoholInfo1 = new TestAlcoholInfo();

        //司机2身份验证类型
        public int verifyID2;
        //司机2身份验证类型
        public int verifyID3;
        //司机2身份验证类型
        public int verifyID4;
        //司机2测酒结果
        public TestAlcoholInfo testAlcoholInfo2 = new TestAlcoholInfo();
        //司机3测酒结果
        public TestAlcoholInfo testAlcoholInfo3 = new TestAlcoholInfo();
        //司机4测酒结果
        public TestAlcoholInfo testAlcoholInfo4 = new TestAlcoholInfo();

    }

    public class TestAlcoholInfo
    {
        public TestAlcoholInfo()
        { }
        public int testAlcoholResult;
        public string picture = "";
        public string testTime = "";
    }

    public class ChuQinPlan
    {
        public ChuQinPlan()
        { }

        //机车计划
        public TrainPlan trainPlan = new TrainPlan();

        //机组出勤信息
        public ChuqinGroup chuqinGroup = new ChuqinGroup();

    }

    public class ChuQinPlanList : List<ChuQinPlan>
    {
    }

    public class TuiQinPlan
    {
        public TuiQinPlan()
        { }
        public TrainPlan trainPlan = new TrainPlan();
        public string beginWorkTime = "";
        public TuiQinGroup tuiqinGroup = new TuiQinGroup();
    }

    public class TuiQinPlanList : List<TuiQinPlan>
    {
    }

    public class TuiQinGroup
    {
        public TuiQinGroup()
        { }

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

    public class DutyUser
    {
        public DutyUser()
        { }

        public string userGUID;

        //值班员编号
        public string userID;

        //值班员姓名
        public string userName;

    }

    public class Client
    {
        public Client()
        { }

        //客户端ID
        public string siteID;

        //客户端名称
        public string siteName;

    }

    public class RRsTrainPlanSendLog
    {
        public string strSendGUID { get; set; }
        public string strTrainNo { get; set; }
        public string strTrainPlanGUID { get; set; }
        public string strTrainJiaoluName { get; set; }
        public DateTime dtStartTime { get; set; }
        public DateTime dtRealStartTime { get; set; }
        public string strSendSiteName { get; set; }
        public DateTime dtSendTime { get; set; }
    }

    [Serializable]
    public partial class RRsTrainPlan
    {
        public RRsTrainPlan()
        { }
        #region Model
        private string _strtrainjiaoluname;
        private string _strtrainmantypename;
        private string _strtrainplanguid;
        private string _strtrainjiaoluguid;
        private string _strtrainno;
        private string _strtrainnumber;
        private DateTime? _dtstarttime;
        private DateTime? _dtchuqintime;
        private string _strstartstation;
        private string _strendstation;
        private DateTime? _dtcreatetime;
        private int? _nplanstate;
        private string _strtraintypename;
        private int? _ntrainmantypeid;
        private string _strstartstationname;
        private int _sendplan;
        private int? _nneedrest;
        private DateTime? _dtarrivetime;
        private DateTime? _dtcalltime;
        private int? _nkehuoid;
        private DateTime? _dtrealstarttime;
        private int? _nplantype;
        private int? _ndragtype;
        private int? _nremarktype;
        private string _strremark;
        private string _strcreatesiteguid;
        private string _strcreateuserguid;
        private string _strendstationname;
        private string _strcreateuserid;
        private string _strcreateusername;
        private string _strcreatesitename;
        private DateTime? _dtfirststarttime;
        private int _nid;
        private string _ndragtypename;
        private string _strkehuoname;
        private string _strbak1;
        private DateTime? _dtlastarrivetime;
        private string _strmainplanguid;
        private string _strworkshopguid;
        private string _strplaceid;
        private string _strplanstatename;
        private string _strplacename;
        private string _strremarktypename;
        private string _strplantypename;
        private DateTime? _dtbeginworktime;
        private DateTime? _dtLastArriveTime;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluName
        {
            set { _strtrainjiaoluname = value; }
            get { return _strtrainjiaoluname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string strTrainPlanGUID
        {
            set { _strtrainplanguid = value; }
            get { return _strtrainplanguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluGUID
        {
            set { _strtrainjiaoluguid = value; }
            get { return _strtrainjiaoluguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNo
        {
            set { _strtrainno = value; }
            get { return _strtrainno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNumber
        {
            set { _strtrainnumber = value; }
            get { return _strtrainnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dtStartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        
        /// <summary>
        /// 
        /// </summary>
        public string strStartStation
        {
            set { _strstartstation = value; }
            get { return _strstartstation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strEndStation
        {
            set { _strendstation = value; }
            get { return _strendstation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dtCreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? nPlanState
        {
            set { _nplanstate = value; }
            get { return _nplanstate; }
        }
        /// <summary>
        /// 和nPlanState重复，由于反序列化代码不统一，暂时两个都保留
        /// </summary>
        public int? planStateID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainTypeName
        {
            set { _strtraintypename = value; }
            get { return _strtraintypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nTrainmanTypeID
        {
            set { _ntrainmantypeid = value; }
            get { return _ntrainmantypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strStartStationName
        {
            set { _strstartstationname = value; }
            get { return _strstartstationname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SendPlan
        {
            set { _sendplan = value; }
            get { return _sendplan; }
        }



        /// <summary>
        /// 
        /// </summary>
        public int? nKeHuoID
        {
            set { _nkehuoid = value; }
            get { return _nkehuoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dtRealStartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? nPlanType
        {
            set { _nplantype = value; }
            get { return _nplantype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nDragType
        {
            set { _ndragtype = value; }
            get { return _ndragtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nRemarkType
        {
            set { _nremarktype = value; }
            get { return _nremarktype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strRemark
        {
            set { _strremark = value; }
            get { return _strremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCreateSiteGUID
        {
            set { _strcreatesiteguid = value; }
            get { return _strcreatesiteguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCreateUserGUID
        {
            set { _strcreateuserguid = value; }
            get { return _strcreateuserguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strEndStationName
        {
            set { _strendstationname = value; }
            get { return _strendstationname; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string dtFirstStartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int nid
        {
            set { _nid = value; }
            get { return _nid; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string strMainPlanGUID
        {
            set { _strmainplanguid = value; }
            get { return _strmainplanguid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string strPlaceID
        {
            set { _strplaceid = value; }
            get { return _strplaceid; }
        }
 
        public string dtLastArriveTime { get; set; }
        #endregion Model

    }
}
