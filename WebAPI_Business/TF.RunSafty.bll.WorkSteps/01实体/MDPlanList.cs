using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkSteps
{

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

   public class TrainPlan
   {

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


       public TestCardResult testCardResult1 = new TestCardResult();
       public TestCardResult testCardResult2= new TestCardResult();
       public TestCardResult testCardResult3 = new TestCardResult();
       public TestCardResult testCardResult4 = new TestCardResult();



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

   public class DutyPlace
   {
       public string placeID;
       public string placeName;
   }

   public class TestAlcoholInfo
   {
       public TestAlcoholInfo()
       { }
       public int testAlcoholResult;
       public string picture = "";
       public string testTime = "";
   }


   public class TestCardResult
   {
       public int nTestCardResult;
       public string strTestCardResult;
   
   }


   public class Station
   {
       public Station()
       { }
       public string stationID = "";
       public string stationNumber = "";
       public string stationName = "";
   }


}
