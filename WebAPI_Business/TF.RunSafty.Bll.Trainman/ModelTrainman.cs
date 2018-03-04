using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TF.RunSafty.Trainman
{
    public class Trainman
    {
        private string m_strTrainmanGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID
        {
            get { return m_strTrainmanGUID; }
            set { m_strTrainmanGUID = value; }
        }
        private string m_strTrainmanNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber
        {
            get { return m_strTrainmanNumber; }
            set { m_strTrainmanNumber = value; }
        }
        private string m_strTrainmanName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName
        {
            get { return m_strTrainmanName; }
            set { m_strTrainmanName = value; }
        }
        private int m_nPostID;
        /// <summary>
        /// 
        /// </summary>
        public int nPostID
        {
            get { return m_nPostID; }
            set { m_nPostID = value; }
        }
        private string m_strWorkShopGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID
        {
            get { return m_strWorkShopGUID; }
            set { m_strWorkShopGUID = value; }
        }
        private Object m_FingerPrint1 = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Object FingerPrint1
        {
            get { return m_FingerPrint1; }
            set { m_FingerPrint1 = value; }
        }
        private Object m_FingerPrint2 = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Object FingerPrint2
        {
            get { return m_FingerPrint2; }
            set { m_FingerPrint2 = value; }
        }
        private Object m_Picture = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Object Picture
        {
            get { return m_Picture; }
            set { m_Picture = value; }
        }
        private string m_strGuideGroupGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strGuideGroupGUID
        {
            get { return m_strGuideGroupGUID; }
            set { m_strGuideGroupGUID = value; }
        }
        private string m_strTelNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strTelNumber
        {
            get { return m_strTelNumber; }
            set { m_strTelNumber = value; }
        }
        private string m_strMobileNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strMobileNumber
        {
            get { return m_strMobileNumber; }
            set { m_strMobileNumber = value; }
        }
        private string m_strAddress;
        /// <summary>
        /// 
        /// </summary>
        public string strAddress
        {
            get { return m_strAddress; }
            set { m_strAddress = value; }
        }
        private int m_nDriverType;
        /// <summary>
        /// 
        /// </summary>
        public int nDriverType
        {
            get { return m_nDriverType; }
            set { m_nDriverType = value; }
        }
        private int m_bIsKey;
        /// <summary>
        /// 
        /// </summary>
        public int bIsKey
        {
            get { return m_bIsKey; }
            set { m_bIsKey = value; }
        }
        private DateTime? m_dtRuZhiTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtRuZhiTime
        {
            get { return m_dtRuZhiTime; }
            set { m_dtRuZhiTime = value; }
        }
        private DateTime? m_dtJiuZhiTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtJiuZhiTime
        {
            get { return m_dtJiuZhiTime; }
            set { m_dtJiuZhiTime = value; }
        }
        private int m_nDriverLevel;
        /// <summary>
        /// 
        /// </summary>
        public int nDriverLevel
        {
            get { return m_nDriverLevel; }
            set { m_nDriverLevel = value; }
        }
        private string m_strABCD;
        /// <summary>
        /// 
        /// </summary>
        public string strABCD
        {
            get { return m_strABCD; }
            set { m_strABCD = value; }
        }
        private string m_strRemark;
        /// <summary>
        /// 
        /// </summary>
        public string strRemark
        {
            get { return m_strRemark; }
            set { m_strRemark = value; }
        }
        private string m_nKeHuoID;
        /// <summary>
        /// 
        /// </summary>
        public string nKeHuoID
        {
            get { return m_nKeHuoID; }
            set { m_nKeHuoID = value; }
        }
        private string m_strTrainJiaoluGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluGUID
        {
            get { return m_strTrainJiaoluGUID; }
            set { m_strTrainJiaoluGUID = value; }
        }
        private DateTime? m_dtLastEndWorkTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastEndWorkTime
        {
            get { return m_dtLastEndWorkTime; }
            set { m_dtLastEndWorkTime = value; }
        }
        private int m_nDeleteState;
        /// <summary>
        /// 
        /// </summary>
        public int nDeleteState
        {
            get { return m_nDeleteState; }
            set { m_nDeleteState = value; }
        }
        private int m_nID;
        /// <summary>
        /// 
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }
        private DateTime? m_dtCreateTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtCreateTime
        {
            get { return m_dtCreateTime; }
            set { m_dtCreateTime = value; }
        }
        private int m_nTrainmanState;
        /// <summary>
        /// 
        /// </summary>
        public int nTrainmanState
        {
            get { return m_nTrainmanState; }
            set { m_nTrainmanState = value; }
        }
        private int m_nZDD;
        /// <summary>
        /// 
        /// </summary>
        public int nZDD
        {
            get { return m_nZDD; }
            set { m_nZDD = value; }
        }
        private int m_nZDZ;
        /// <summary>
        /// 
        /// </summary>
        public int nZDZ
        {
            get { return m_nZDZ; }
            set { m_nZDZ = value; }
        }
        private string m_strBZ;
        /// <summary>
        /// 
        /// </summary>
        public string strBZ
        {
            get { return m_strBZ; }
            set { m_strBZ = value; }
        }
        private string m_strJP;
        /// <summary>
        /// 
        /// </summary>
        public string strJP
        {
            get { return m_strJP; }
            set { m_strJP = value; }
        }
        private string m_strTrainmanJiaoluGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanJiaoluGUID
        {
            get { return m_strTrainmanJiaoluGUID; }
            set { m_strTrainmanJiaoluGUID = value; }
        }
        private string m_strareaguid;
        /// <summary>
        /// 
        /// </summary>
        public string strareaguid
        {
            get { return m_strareaguid; }
            set { m_strareaguid = value; }
        }
        private DateTime? m_dtLastInRoomTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastInRoomTime
        {
            get { return m_dtLastInRoomTime; }
            set { m_dtLastInRoomTime = value; }
        }
        private DateTime? m_dtLastOutRoomTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastOutRoomTime
        {
            get { return m_dtLastOutRoomTime; }
            set { m_dtLastOutRoomTime = value; }
        }
    }

    public class VTrainman
    {

        private string m_strTrainmanNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber
        {
            get { return m_strTrainmanNumber; }
            set { m_strTrainmanNumber = value; }
        }
        private string m_strTrainmanName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName
        {
            get { return m_strTrainmanName; }
            set { m_strTrainmanName = value; }
        }
        private string m_strTelNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strTelNumber
        {
            get { return m_strTelNumber; }
            set { m_strTelNumber = value; }
        }
        private string m_strRemark;
        /// <summary>
        /// 
        /// </summary>
        public string strRemark
        {
            get { return m_strRemark; }
            set { m_strRemark = value; }
        }
        private int m_nID;
        /// <summary>
        /// 
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }
        private DateTime? m_dtCreateTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtCreateTime
        {
            get { return m_dtCreateTime; }
            set { m_dtCreateTime = value; }
        }
        private Object m_FingerPrint1 = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Object FingerPrint1
        {
            get { return m_FingerPrint1; }
            set { m_FingerPrint1 = value; }
        }
        private Object m_Picture = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Object Picture
        {
            get { return m_Picture; }
            set { m_Picture = value; }
        }
        private Object m_FingerPrint2 = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Object FingerPrint2
        {
            get { return m_FingerPrint2; }
            set { m_FingerPrint2 = value; }
        }
        private int m_nTrainmanState;
        /// <summary>
        /// 
        /// </summary>
        public int nTrainmanState
        {
            get { return m_nTrainmanState; }
            set { m_nTrainmanState = value; }
        }
        private int m_nPostID;
        /// <summary>
        /// 
        /// </summary>
        public int nPostID
        {
            get { return m_nPostID; }
            set { m_nPostID = value; }
        }
        private string m_strWorkShopGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID
        {
            get { return m_strWorkShopGUID; }
            set { m_strWorkShopGUID = value; }
        }
        private string m_strGuideGroupGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strGuideGroupGUID
        {
            get { return m_strGuideGroupGUID; }
            set { m_strGuideGroupGUID = value; }
        }
        private string m_strMobileNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strMobileNumber
        {
            get { return m_strMobileNumber; }
            set { m_strMobileNumber = value; }
        }
        private string m_strAddress;
        /// <summary>
        /// 
        /// </summary>
        public string strAddress
        {
            get { return m_strAddress; }
            set { m_strAddress = value; }
        }
        private string m_strTrainmanGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID
        {
            get { return m_strTrainmanGUID; }
            set { m_strTrainmanGUID = value; }
        }
        private int m_nDriverType;
        /// <summary>
        /// 
        /// </summary>
        public int nDriverType
        {
            get { return m_nDriverType; }
            set { m_nDriverType = value; }
        }
        private int m_bIsKey;
        /// <summary>
        /// 
        /// </summary>
        public int bIsKey
        {
            get { return m_bIsKey; }
            set { m_bIsKey = value; }
        }
        private DateTime? m_dtRuZhiTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtRuZhiTime
        {
            get { return m_dtRuZhiTime; }
            set { m_dtRuZhiTime = value; }
        }
        private DateTime? m_dtJiuZhiTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtJiuZhiTime
        {
            get { return m_dtJiuZhiTime; }
            set { m_dtJiuZhiTime = value; }
        }
        private int m_nDriverLevel;
        /// <summary>
        /// 
        /// </summary>
        public int nDriverLevel
        {
            get { return m_nDriverLevel; }
            set { m_nDriverLevel = value; }
        }
        private string m_strABCD;
        /// <summary>
        /// 
        /// </summary>
        public string strABCD
        {
            get { return m_strABCD; }
            set { m_strABCD = value; }
        }
        private string m_nKeHuoID;
        /// <summary>
        /// 
        /// </summary>
        public string nKeHuoID
        {
            get { return m_nKeHuoID; }
            set { m_nKeHuoID = value; }
        }
        private string m_strTrainJiaoluGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluGUID
        {
            get { return m_strTrainJiaoluGUID; }
            set { m_strTrainJiaoluGUID = value; }
        }
        private DateTime? m_dtLastEndWorkTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastEndWorkTime
        {
            get { return m_dtLastEndWorkTime; }
            set { m_dtLastEndWorkTime = value; }
        }
        private int m_nDeleteState;
        /// <summary>
        /// 
        /// </summary>
        public int nDeleteState
        {
            get { return m_nDeleteState; }
            set { m_nDeleteState = value; }
        }
        private string m_strTrainJiaoluName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluName
        {
            get { return m_strTrainJiaoluName; }
            set { m_strTrainJiaoluName = value; }
        }
        private string m_strGuideGroupName;
        /// <summary>
        /// 指导队名称
        /// </summary>
        public string strGuideGroupName
        {
            get { return m_strGuideGroupName; }
            set { m_strGuideGroupName = value; }
        }
        private string m_strWorkShopName;
        /// <summary>
        /// 车间名称
        /// </summary>
        public string strWorkShopName
        {
            get { return m_strWorkShopName; }
            set { m_strWorkShopName = value; }
        }
        private string m_strAreaName;
        /// <summary>
        /// 机务段名称
        /// </summary>
        public string strAreaName
        {
            get { return m_strAreaName; }
            set { m_strAreaName = value; }
        }
        private string m_strJWDNumber;
        /// <summary>
        /// 机务段号
        /// </summary>
        public string strJWDNumber
        {
            get { return m_strJWDNumber; }
            set { m_strJWDNumber = value; }
        }
        private string m_strJP;
        /// <summary>
        /// 
        /// </summary>
        public string strJP
        {
            get { return m_strJP; }
            set { m_strJP = value; }
        }
        private string m_strTrainmanJiaoluGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanJiaoluGUID
        {
            get { return m_strTrainmanJiaoluGUID; }
            set { m_strTrainmanJiaoluGUID = value; }
        }
        private string m_strareaguid;
        /// <summary>
        /// 所属机务段
        /// </summary>
        public string strareaguid
        {
            get { return m_strareaguid; }
            set { m_strareaguid = value; }
        }
        private DateTime? m_dtLastInRoomTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastInRoomTime
        {
            get { return m_dtLastInRoomTime; }
            set { m_dtLastInRoomTime = value; }
        }
        private DateTime? m_dtLastOutRoomTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastOutRoomTime
        {
            get { return m_dtLastOutRoomTime; }
            set { m_dtLastOutRoomTime = value; }
        }


        public int nFingerPrint1_Null = 0;
        public int nFingerPrint2_Null = 0;
        public int nPicture_Null = 0;
    }

    public class QurryModel
    {
        private string _strTrainmanNumber;

        public string strTrainmanNumber
        {
            get { return _strTrainmanNumber; }
            set { _strTrainmanNumber = value; }
        }
        private string _strTrainmanName;

        public string strTrainmanName
        {
            get { return _strTrainmanName; }
            set { _strTrainmanName = value; }
        }
        private string _strAreaGUID;

        public string strAreaGUID
        {
            get { return _strAreaGUID; }
            set { _strAreaGUID = value; }
        }
        private string _strWorkShopGUID;

        public string strWorkShopGUID
        {
            get { return _strWorkShopGUID; }
            set { _strWorkShopGUID = value; }
        }
        private string _strTrainJiaoluGUID;

        public string strTrainJiaoluGUID
        {
            get { return _strTrainJiaoluGUID; }
            set { _strTrainJiaoluGUID = value; }
        }
        private string _strGuideGroupGUID;

        public string strGuideGroupGUID
        {
            get { return _strGuideGroupGUID; }
            set { _strGuideGroupGUID = value; }
        }
        private int _nFingerCount;

        public int nFingerCount
        {
            get { return _nFingerCount; }
            set { _nFingerCount = value; }
        }
        private int _nPhotoCount;

        public int nPhotoCount
        {
            get { return _nPhotoCount; }
            set { _nPhotoCount = value; }
        }

    }

    public class InterfaceOutPut
    {
        public int result;
        public string resultStr;
        public object data;
    }

    /// <summary>
    ///类名: TrainmanStateCount
    ///说明: 人员状态数量统计
    /// </summary>
    public class TrainmanStateCount
    {
        public TrainmanStateCount()
        { }

        /// <summary>
        /// 非运转
        /// </summary>
        public int nUnRuning;

        /// <summary>
        /// 预备
        /// </summary>                                         
        public int nReady;

        /// <summary>
        /// 正常或已退勤
        /// </summary>
        public int nNormal;

        /// <summary>
        /// 已安排计划
        /// </summary>
        public int nPlaning;

        /// <summary>
        /// 已入寓
        /// </summary>
        public int nInRoom;

        /// <summary>
        /// 已离寓
        /// </summary>
        public int nOutRoom;

        /// <summary>
        /// 已出勤
        /// </summary>
        public int nRuning;

        /// <summary>
        /// 空人员
        /// </summary>
        public int nNil;

    }
    /// <summary>
    ///类名: TrainmanJiaoLuCount
    ///说明: 交路人员统计信息
    /// </summary>
    public class TrainmanJiaoluCount
    {
        public TrainmanJiaoluCount()
        { }

        /// <summary>
        /// 交路名称
        /// </summary>
        public string strJiaoLuName;

        /// <summary>
        /// 人员数量
        /// </summary>
        public int nCount;

    }

    /// <summary>
    ///类名: TrainJiaoluCountList
    ///说明: 交路人员统计信息列表
    /// </summary>
    public class TrainmanJiaoluCountList : List<TrainmanJiaoluCount>
    {
        public TrainmanJiaoluCountList()
        { }

    }


    /// <summary>
    ///类名: EnumSum
    ///说明: 枚举统计信息
    /// </summary>
    public class EnumSum
    {
        public EnumSum()
        { }

        /// <summary>
        /// 美剧名称
        /// </summary>
        public string EnumName;

        /// <summary>
        /// 枚举编号
        /// </summary>
        public string EnumID;

        /// <summary>
        /// 统计数量
        /// </summary>
        public int EnumCount;

    }

    /// <summary>
    ///类名: EnumSumList
    ///说明: 枚举统计列表
    /// </summary>
    public class EnumSumList : List<EnumSum>
    {
        public EnumSumList()
        { }

    }

    /// <summary>
    ///类名: TrainmanGroup
    ///说明: 简单人员机组
    /// </summary>
    public class TrainmanGroup
    {
        public TrainmanGroup()
        { }

        /// <summary>
        /// 司机工号
        /// </summary>
        public string TrainmanNumber1 = string.Empty;

        /// <summary>
        /// 司机姓名
        /// </summary>
        public string TrainmanName1 = string.Empty;

        /// <summary>
        /// 司机2工号
        /// </summary>
        public string TrainmanNumber2 = string.Empty;

        /// <summary>
        /// 司机2姓名
        /// </summary>
        public string TrainmanName2 = string.Empty;

        /// <summary>
        /// 司机3工号
        /// </summary>
        public string TrainmanNumber3 = string.Empty;

        /// <summary>
        /// 司机3姓名
        /// </summary>
        public string TrainmanName3 = string.Empty;

        /// <summary>
        /// 司机4工号
        /// </summary>
        public string TrainmanNumber4 = string.Empty;

        /// <summary>
        /// 司机4姓名
        /// </summary>
        public string TrainmanName4 = string.Empty;

        /// <summary>
        /// 机组状态
        /// </summary>
        public int GroupState = 0;

    }

    /// <summary>
    ///类名: TrainmanRunStateCount
    ///说明: 交路运转机组统计及人员信息
    /// </summary>
    public class TrainmanRunStateCount
    {
        public TrainmanRunStateCount()
        { }

        /// <summary>
        /// 交路名称
        /// </summary>
        public string strJiaoLuName = string.Empty;

        /// <summary>
        /// 运转数量
        /// </summary>
        public int nRuningCount = 0;

        /// <summary>
        /// 本段休息数量
        /// </summary>
        public int nLocalCount = 0;

        /// <summary>
        /// 外段休息数量
        /// </summary>
        public int nSiteCount = 0;

        /// <summary>
        /// 包含机组列表
        /// </summary>
        public TrainmanGroupList group = new TrainmanGroupList();

    }

    /// <summary>
    ///类名: TrainmanGroupList
    ///说明: 简单机组列表
    /// </summary>
    public class TrainmanGroupList : List<TrainmanGroup>
    {
        public TrainmanGroupList()
        { }

    }

    /// <summary>
    ///类名: TrainmanRunStateCountList
    ///说明: 列表累
    /// </summary>
    public class TrainmanRunStateCountList : List<TrainmanRunStateCount>
    {
        public TrainmanRunStateCountList()
        { }

    }


}
