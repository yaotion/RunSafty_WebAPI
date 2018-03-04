using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TF.RunSaftyAPI.App_Api.Public.Plan
{
    public class DataEntityClass
    {
    }

    /// <summary>
    /// 上层JSON对象
    /// </summary>
    //[DataContract]
    public class JSONObject<T>
    {
        [DataMember]
        public string stepID
        {
            get;
            set;
        }

        [DataMember]
        public T data
        {
            get;
            set;
        }
    }

    //[DataContract]
    public class JSONObject2
    {
        [DataMember]
        public string stepID
        {
            get;
            set;
        }

        [DataMember]
        public object data
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Plan_Train 内的对象
    /// </summary>
    public class Plan_Train_trainmanList
    {
        private string _trainmanID;
        public string trainmanID
        {
            get { return _trainmanID; }
            set { _trainmanID = value; }
        }

        private string _trainmanIndex;
        public string trainmanIndex
        {
            get { return _trainmanIndex; }
            set { _trainmanIndex = value; }
        }
    }

    //////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 出勤计划
    /// </summary>
    /// TAB_Plan_BeginWork
    /// {'stepID:1000',data:[{trainmanID:"",planID:"",verify:0,flowid:""},...]}
    [DataContract]
    public class Plan_BeginWork
    {
        private string _trainmanID;
        [DataMember]
        public string trainmanID
        {
            get { return _trainmanID; }
            set { _trainmanID = value; }
        }

        private string _planID;
        [DataMember]
        public string planID
        {
            get { return _planID; }
            set { _planID = value; }
        }

        private int _verify;
        [DataMember]
        public int verify
        {
            get { return _verify; }
            set { _verify = value; }
        }

        private string _flowid;
        [DataMember]
        public string flowid
        {
            get { return _flowid; }
            set { _flowid = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// TAB_Plan_Train
    /// {"stepID":1001,"data":[{”startTime”:””,"trainNo":"","trainTypeName":"","trainNumber":"","trainmanList":[{“trainmanID”:””,”trainmanIndex”:},…]}]},
    [DataContract]
    public class Plan_Train
    {
        private DateTime _startTime;
        public DateTime startTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        private string _trainNo;
        public string trainNo
        {
            get { return _trainNo; }
            set { _trainNo = value; }
        }

        private string _trainTypeName;
        public string trainTypeName
        {
            get { return _trainTypeName; }
            set { _trainTypeName = value; }
        }

        private string _trainNumber;
        public string trainNumber
        {
            get { return _trainNumber; }
            set { _trainNumber = value; }
        }

        private int _result;
        public int result
        {
            get { return _result; }
            set { _result = value; }
        }

        private string _planID;
        public string planID
        {
            get { return _planID; }
            set { _planID = value; }
        }

        private List<Plan_Train_trainmanList> _trainmanList;
        public List<Plan_Train_trainmanList> trainmanList
        {
            get { return _trainmanList; }
            set { _trainmanList = value; }
        }
    }

    /// <summary>
    /// 饮酒
    /// </summary>
    /// TAB_Drink_Information
    /// {"stepID":1003,data:[{"strid":"","trainmanID":"","testTime":"","result":0,"flowID":"","workType":0},...]}
    [DataContract]
    public class Drink_Information
    {
        private string _strid;
        public string strid
        {
            get { return _strid; }
            set { _strid = value; }
        }

        private string _trainmanID;
        public string trainmanID
        {
            get { return _trainmanID; }
            set { _trainmanID = value; }
        }

        private int nDrinkResult;
        public int NDrinkResult
        {
            get { return nDrinkResult; }
            set { nDrinkResult = value; }
        }

        private string _testTime;
        public string testTime
        {
            get { return _testTime; }
            set { _testTime = value; }
        }

        private int _result;
        public int result
        {
            get { return _result; }
            set { _result = value; }
        }

        private string _flowID;
        public string flowID
        {
            get { return _flowID; }
            set { _flowID = value; }
        }

        private int _workType;
        public int workType
        {
            get { return _workType; }
            set { _workType = value; }
        }
    }

    /// <summary>
    ///  出乘指导阅读记录
    /// </summary>
    /// TAB_DutyReading_ReadRecord
    /// {"stepID":1004,
    [DataContract]
    public class DutyReading_ReadRecord
    {
        [DataMember]
        public string workid
        {
            get;
            set;
        }

        [DataMember]
        public int worktype
        {
            get;
            set;
        }
        [DataMember]
        public object recordList
        {
            get;
            set;
        }
        [DataMember]
        public String sid
        {
            get;
            set;
        }
    }

    //recordList
    [DataContract]
    public class DutyReading_recordList
    {
        [DataMember]
        public String rid
        {
            get;
            set;
        }

        [DataMember]
        public DateTime rtime
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 通话
    /// </summary>
    /// TAB_Voice_Information
    /// {"stepID":1006,
    [DataContract]
    public class Voice_Information
    {
        private string _strid;
        public string strid
        {
            get { return _strid; }
            set { _strid = value; }
        }

        private DateTime _beginTime;
        public DateTime beginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        private DateTime _endTime;
        public DateTime endTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        private long _voiceLength;
        public long voiceLength
        {
            get { return _voiceLength; }
            set { _voiceLength = value; }
        }

        private long _voiceType;
        public long voiceType
        {
            get { return _voiceType; }
            set { _voiceType = value; }
        }

        private string _workID;
        public string workID
        {
            get { return _workID; }
            set { _workID = value; }
        }

        private int _workType;
        public int workType
        {
            get { return _workType; }
            set { _workType = value; }
        }

        private string _fileName;
        public string fileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
    }

    /// <summary>
    /// TAB_RunRecord_File
    /// //3.4	运行记录转储
    ///{"stepID":1007,"data":{"workid":"","worktype":1,"sid":"sssss","FileList":[{"fid":"fffff"}]}}
    /// </summary>
    [DataContract]
    public class RunRecord_File
    {
        [DataMember]
        public String workid
        {
            get;
            set;
        }

        [DataMember]
        public String worktype
        {
            get;
            set;
        }

        [DataMember]
        public String sid
        {
            get;
            set;
        }

        [DataMember]
        public object FileList
        {
            get;
            set;
        }
    }

    /// <summary>
    /// FileList
    /// 运行记录转储
    /// {"stepID":1007
    /// </summary>
    [DataContract]
    public class RunRecord_File_FileList
    {
        [DataMember]
        public string fid
        {
            get;
            set;
        }
    }

    /// <summary>
    /// TAB_Plan_VerifyCard
    /// 1008,”data”
    /// </summary>
    [DataContract]
    public class Plan_VerifyCard
    { 
        [DataMember]
        public String workid
        {
            get;
            set;
        }
 
        [DataMember]
        public String worktype
        {
            get;
            set;
        }

        [DataMember]
        public String sid
        {
            get;
            set;
        }
 
        [DataMember]
        public object verifyList
        {
            get;
            set;
        }
    }

    /// <summary>
    /// TAB_Plan_VerifyCard
    /// 1008,”data”: verifyList
    /// </summary>
    [DataContract]
    public class VerifyCard_verifyList
    {
        [DataMember]
        public string sectionID
        {
            get;
            set;
        }
        [DataMember]
        public string sectionName
        {
            get;
            set;
        }
        
        [DataMember]
        public DateTime vTime
        {
            get;
            set;
        }

        [DataMember]
        public int resultID
        {
            get;
            set;
        }

        [DataMember]
        public string resultContent
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 3.1	值班员确认步骤
    /// </summary>
    /// Plan_Confirm
    /// {"stepID":3000,
    [DataContract]
    public class Plan_Confirm
    {
        [DataMember]
        public String workid
        {
            get;
            set;
        }
        [DataMember]
        public int worktype
        {
            get;
            set;
        }
        [DataMember]
        public String did
        {
            get;
            set;
        }
        [DataMember]
        public String dname
        {
            get;
            set;
        }
        [DataMember]
        public String sid
        {
            get;
            set;
        }
        [DataMember]
        public String sname
        {
            get;
            set;
        }
        [DataMember]
        public DateTime time
        {
            get;
            set;
        }
        [DataMember]
        public int cresult
        {
            get;
            set;
        }
        [DataMember]
        public String vid
        {
            get;
            set;
        }
    }
}
