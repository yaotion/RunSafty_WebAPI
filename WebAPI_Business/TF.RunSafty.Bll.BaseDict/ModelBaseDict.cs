using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TF.RunSafty.BaseDict
{
    public class InterfaceOutPut
    {
        public int result;
        public string resultStr;
        public object data;
    }

    #region 车站
    public class Station
    {
        private int _nid;

        public int nid
        {
            get { return _nid; }
            set { _nid = value; }
        }
        private string _strStationGUID;

        public string strStationGUID
        {
            get { return _strStationGUID; }
            set { _strStationGUID = value; }
        }
        private int _strStationNumber;

        public int strStationNumber
        {
            get { return _strStationNumber; }
            set { _strStationNumber = value; }
        }
        private string _strStationName;

        public string strStationName
        {
            get { return _strStationName; }
            set { _strStationName = value; }
        }
        private string _strWorkShopGUID;

        public string strWorkShopGUID
        {
            get { return _strWorkShopGUID; }
            set { _strWorkShopGUID = value; }
        }
        private string _strStationPY;

        public string strStationPY
        {
            get { return _strStationPY; }
            set { _strStationPY = value; }
        }

    }

    public class StationInTrainJiaolu
    {
        private string m_strTrainJiaoluGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluGUID
        {
            get { return m_strTrainJiaoluGUID; }
            set { m_strTrainJiaoluGUID = value; }
        }
        private string m_strStationGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strStationGUID
        {
            get { return m_strStationGUID; }
            set { m_strStationGUID = value; }
        }
        private int m_strStationNumber;
        /// <summary>
        /// 
        /// </summary>
        public int strStationNumber
        {
            get { return m_strStationNumber; }
            set { m_strStationNumber = value; }
        }
        private string m_strStationName;
        /// <summary>
        /// 
        /// </summary>
        public string strStationName
        {
            get { return m_strStationName; }
            set { m_strStationName = value; }
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
        private int m_nSortid;
        /// <summary>
        /// 
        /// </summary>
        public int nSortid
        {
            get { return m_nSortid; }
            set { m_nSortid = value; }
        }
    }
    #endregion

    #region 人员交路

    public class TrainManJiaoLu
    {
        private string m_strTrainmanJiaoluGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanJiaoluGUID
        {
            get { return m_strTrainmanJiaoluGUID; }
            set { m_strTrainmanJiaoluGUID = value; }
        }
        private string m_strTrainmanJiaoluName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanJiaoluName
        {
            get { return m_strTrainmanJiaoluName; }
            set { m_strTrainmanJiaoluName = value; }
        }
        private int m_nJiaoluType;
        /// <summary>
        /// 
        /// </summary>
        public int nJiaoluType
        {
            get { return m_nJiaoluType; }
            set { m_nJiaoluType = value; }
        }
        private string m_strJiaoluTypeName;
        /// <summary>
        /// 
        /// </summary>
        public string strJiaoluTypeName
        {
            get { return m_strJiaoluTypeName; }
            set { m_strJiaoluTypeName = value; }
        }
        private string m_strLineGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strLineGUID
        {
            get { return m_strLineGUID; }
            set { m_strLineGUID = value; }
        }
        private string m_strLineName;
        /// <summary>
        /// 
        /// </summary>
        public string strLineName
        {
            get { return m_strLineName; }
            set { m_strLineName = value; }
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
        private string m_strTrainJiaoluName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluName
        {
            get { return m_strTrainJiaoluName; }
            set { m_strTrainJiaoluName = value; }
        }
        private int m_nKehuoID;
        /// <summary>
        /// 
        /// </summary>
        public int nKehuoID
        {
            get { return m_nKehuoID; }
            set { m_nKehuoID = value; }
        }
        private string m_strKehuoName;
        /// <summary>
        /// 
        /// </summary>
        public string strKehuoName
        {
            get { return m_strKehuoName; }
            set { m_strKehuoName = value; }
        }
        private int m_nDragTypeID;
        /// <summary>
        /// 
        /// </summary>
        public int nDragTypeID
        {
            get { return m_nDragTypeID; }
            set { m_nDragTypeID = value; }
        }
        private int m_nTrainmanTypeID;
        /// <summary>
        /// 
        /// </summary>
        public int nTrainmanTypeID
        {
            get { return m_nTrainmanTypeID; }
            set { m_nTrainmanTypeID = value; }
        }
        private string m_strTrainmanTypeName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanTypeName
        {
            get { return m_strTrainmanTypeName; }
            set { m_strTrainmanTypeName = value; }
        }
        private int m_nid;
        /// <summary>
        /// 
        /// </summary>
        public int nid
        {
            get { return m_nid; }
            set { m_nid = value; }
        }
        private string m_nDragTypeName;
        /// <summary>
        /// 
        /// </summary>
        public string nDragTypeName
        {
            get { return m_nDragTypeName; }
            set { m_nDragTypeName = value; }
        }
        private int m_nTrainmanRunType;
        /// <summary>
        /// 
        /// </summary>
        public int nTrainmanRunType
        {
            get { return m_nTrainmanRunType; }
            set { m_nTrainmanRunType = value; }
        }
        private string m_strRunTypeName;
        /// <summary>
        /// 
        /// </summary>
        public string strRunTypeName
        {
            get { return m_strRunTypeName; }
            set { m_strRunTypeName = value; }
        }
    }

    public class TrainManJiaoluRelation
    {
        private string m_strTrainmanJiaoluGUID;
        /// <summary>
        /// 人员交路UIGD
        /// </summary>
        public string strTrainmanJiaoluGUID
        {
            get { return m_strTrainmanJiaoluGUID; }
            set { m_strTrainmanJiaoluGUID = value; }
        }
        private string m_strTrainJiaoluGUID;
        /// <summary>
        /// 机车交路GUID
        /// </summary>
        public string strTrainJiaoluGUID
        {
            get { return m_strTrainJiaoluGUID; }
            set { m_strTrainJiaoluGUID = value; }
        }
        private string m_strTrainmanJiaoluName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanJiaoluName
        {
            get { return m_strTrainmanJiaoluName; }
            set { m_strTrainmanJiaoluName = value; }
        }
        private int m_nJiaoluType;
        /// <summary>
        /// 
        /// </summary>
        public int nJiaoluType
        {
            get { return m_nJiaoluType; }
            set { m_nJiaoluType = value; }
        }
        private string m_strJiaoluTypeName;
        /// <summary>
        /// 
        /// </summary>
        public string strJiaoluTypeName
        {
            get { return m_strJiaoluTypeName; }
            set { m_strJiaoluTypeName = value; }
        }
        private int m_nDragTypeID;
        /// <summary>
        /// 
        /// </summary>
        public int nDragTypeID
        {
            get { return m_nDragTypeID; }
            set { m_nDragTypeID = value; }
        }
        private string m_strKehuoName;
        /// <summary>
        /// 
        /// </summary>
        public string strKehuoName
        {
            get { return m_strKehuoName; }
            set { m_strKehuoName = value; }
        }
        private int m_nTrainmanTypeID;
        /// <summary>
        /// 
        /// </summary>
        public int nTrainmanTypeID
        {
            get { return m_nTrainmanTypeID; }
            set { m_nTrainmanTypeID = value; }
        }
        private string m_strTrainmanTypeName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanTypeName
        {
            get { return m_strTrainmanTypeName; }
            set { m_strTrainmanTypeName = value; }
        }
        private string m_nDragTypeName;
        /// <summary>
        /// 
        /// </summary>
        public string nDragTypeName
        {
            get { return m_nDragTypeName; }
            set { m_nDragTypeName = value; }
        }
        private int m_nKehuoID;
        /// <summary>
        /// 
        /// </summary>
        public int nKehuoID
        {
            get { return m_nKehuoID; }
            set { m_nKehuoID = value; }
        }
        private int m_strStartStationNumber;
        /// <summary>
        /// 
        /// </summary>
        public int strStartStationNumber
        {
            get { return m_strStartStationNumber; }
            set { m_strStartStationNumber = value; }
        }
        private string m_strStartStationName;
        /// <summary>
        /// 
        /// </summary>
        public string strStartStationName
        {
            get { return m_strStartStationName; }
            set { m_strStartStationName = value; }
        }
        private int m_strEndStationNumber;
        /// <summary>
        /// 
        /// </summary>
        public int strEndStationNumber
        {
            get { return m_strEndStationNumber; }
            set { m_strEndStationNumber = value; }
        }
        private string m_strEndStationName;
        /// <summary>
        /// 
        /// </summary>
        public string strEndStationName
        {
            get { return m_strEndStationName; }
            set { m_strEndStationName = value; }
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
        private string m_strStartStation;
        /// <summary>
        /// 
        /// </summary>
        public string strStartStation
        {
            get { return m_strStartStation; }
            set { m_strStartStation = value; }
        }
        private string m_strEndStation;
        /// <summary>
        /// 
        /// </summary>
        public string strEndStation
        {
            get { return m_strEndStation; }
            set { m_strEndStation = value; }
        }
        private int m_bIsBeginWorkFP;
        /// <summary>
        /// 
        /// </summary>
        public int bIsBeginWorkFP
        {
            get { return m_bIsBeginWorkFP; }
            set { m_bIsBeginWorkFP = value; }
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
        private string m_strWorkShopName;
        /// <summary>
        /// 车间名称
        /// </summary>
        public string strWorkShopName
        {
            get { return m_strWorkShopName; }
            set { m_strWorkShopName = value; }
        }
        private int m_ZfqjCount;
        /// <summary>
        /// 
        /// </summary>
        public int ZfqjCount
        {
            get { return m_ZfqjCount; }
            set { m_ZfqjCount = value; }
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
        private int m_nTrainmanRunType;
        /// <summary>
        /// 
        /// </summary>
        public int nTrainmanRunType
        {
            get { return m_nTrainmanRunType; }
            set { m_nTrainmanRunType = value; }
        }
    }

    #endregion

    #region 机务段
    public class JWDCoding
    {
        private string m_strGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strGUID
        {
            get { return m_strGUID; }
            set { m_strGUID = value; }
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
        private string m_strCode;
        /// <summary>
        /// 
        /// </summary>
        public string strCode
        {
            get { return m_strCode; }
            set { m_strCode = value; }
        }
        private string m_strName;
        /// <summary>
        /// 
        /// </summary>
        public string strName
        {
            get { return m_strName; }
            set { m_strName = value; }
        }
        private string m_strShortName;
        /// <summary>
        /// 
        /// </summary>
        public string strShortName
        {
            get { return m_strShortName; }
            set { m_strShortName = value; }
        }
        private string m_strPinYinCode;
        /// <summary>
        /// 
        /// </summary>
        public string strPinYinCode
        {
            get { return m_strPinYinCode; }
            set { m_strPinYinCode = value; }
        }
        private string m_strStatCode;
        /// <summary>
        /// 
        /// </summary>
        public string strStatCode
        {
            get { return m_strStatCode; }
            set { m_strStatCode = value; }
        }
        private string m_strUserCode;
        /// <summary>
        /// 
        /// </summary>
        public string strUserCode
        {
            get { return m_strUserCode; }
            set { m_strUserCode = value; }
        }
        private string m_strLJCode;
        /// <summary>
        /// 
        /// </summary>
        public string strLJCode
        {
            get { return m_strLJCode; }
            set { m_strLJCode = value; }
        }
        private DateTime? m_dtLastModify;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastModify
        {
            get { return m_dtLastModify; }
            set { m_dtLastModify = value; }
        }
        private int m_bIsVisible;
        /// <summary>
        /// 
        /// </summary>
        public int bIsVisible
        {
            get { return m_bIsVisible; }
            set { m_bIsVisible = value; }
        }
    }
    #endregion

    #region 行车区段
    public class TrainJiaoluInSite
    {
        private string m_strSiteName;
        /// <summary>
        /// 
        /// </summary>
        public string strSiteName
        {
            get { return m_strSiteName; }
            set { m_strSiteName = value; }
        }
        private string m_strSiteNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strSiteNumber
        {
            get { return m_strSiteNumber; }
            set { m_strSiteNumber = value; }
        }
        private string m_strSiteGUID;
        /// <summary>
        /// 客户端GUID
        /// </summary>
        public string strSiteGUID
        {
            get { return m_strSiteGUID; }
            set { m_strSiteGUID = value; }
        }
        private string m_strTrainJiaoluGUID;
        /// <summary>
        /// 机车交路GUID
        /// </summary>
        public string strTrainJiaoluGUID
        {
            get { return m_strTrainJiaoluGUID; }
            set { m_strTrainJiaoluGUID = value; }
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
        private string m_strStartStation;
        /// <summary>
        /// 
        /// </summary>
        public string strStartStation
        {
            get { return m_strStartStation; }
            set { m_strStartStation = value; }
        }
        private string m_strEndStation;
        /// <summary>
        /// 
        /// </summary>
        public string strEndStation
        {
            get { return m_strEndStation; }
            set { m_strEndStation = value; }
        }
        private string m_strAreaGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strAreaGUID
        {
            get { return m_strAreaGUID; }
            set { m_strAreaGUID = value; }
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
        private int m_bIsBeginWorkFP;
        /// <summary>
        /// 
        /// </summary>
        public int bIsBeginWorkFP
        {
            get { return m_bIsBeginWorkFP; }
            set { m_bIsBeginWorkFP = value; }
        }
        private string m_strStartStationName;
        /// <summary>
        /// 
        /// </summary>
        public string strStartStationName
        {
            get { return m_strStartStationName; }
            set { m_strStartStationName = value; }
        }
        private string m_strEndStationName;
        /// <summary>
        /// 
        /// </summary>
        public string strEndStationName
        {
            get { return m_strEndStationName; }
            set { m_strEndStationName = value; }
        }
        private string m_Expr1;
        /// <summary>
        /// 
        /// </summary>
        public string Expr1
        {
            get { return m_Expr1; }
            set { m_Expr1 = value; }
        }
        private string m_strJiaoluInSiteGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strJiaoluInSiteGUID
        {
            get { return m_strJiaoluInSiteGUID; }
            set { m_strJiaoluInSiteGUID = value; }
        }
        private int m_bIsDir;
        /// <summary>
        /// 是否为虚拟目录
        /// </summary>
        public int bIsDir
        {
            get { return m_bIsDir; }
            set { m_bIsDir = value; }
        }
    }
    public class TrainJiaolu
    {


        private int m_strStartStationNumber;
        /// <summary>
        /// 
        /// </summary>
        public int strStartStationNumber
        {
            get { return m_strStartStationNumber; }
            set { m_strStartStationNumber = value; }
        }
        private string m_strStartStationName;
        /// <summary>
        /// 
        /// </summary>
        public string strStartStationName
        {
            get { return m_strStartStationName; }
            set { m_strStartStationName = value; }
        }
        private int m_strEndStationNumber;
        /// <summary>
        /// 
        /// </summary>
        public int strEndStationNumber
        {
            get { return m_strEndStationNumber; }
            set { m_strEndStationNumber = value; }
        }
        private string m_strEndStationName;
        /// <summary>
        /// 
        /// </summary>
        public string strEndStationName
        {
            get { return m_strEndStationName; }
            set { m_strEndStationName = value; }
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
        private string m_strTrainJiaoluName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluName
        {
            get { return m_strTrainJiaoluName; }
            set { m_strTrainJiaoluName = value; }
        }
        private string m_strStartStation;
        /// <summary>
        /// 
        /// </summary>
        public string strStartStation
        {
            get { return m_strStartStation; }
            set { m_strStartStation = value; }
        }
        private string m_strEndStation;
        /// <summary>
        /// 
        /// </summary>
        public string strEndStation
        {
            get { return m_strEndStation; }
            set { m_strEndStation = value; }
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
        private int m_bIsBeginWorkFP;
        /// <summary>
        /// 
        /// </summary>
        public int bIsBeginWorkFP
        {
            get { return m_bIsBeginWorkFP; }
            set { m_bIsBeginWorkFP = value; }
        }
        private int m_nid;
        /// <summary>
        /// 
        /// </summary>
        public int nid
        {
            get { return m_nid; }
            set { m_nid = value; }
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
        private int m_ZfqjCount;
        /// <summary>
        /// 
        /// </summary>
        public int ZfqjCount
        {
            get { return m_ZfqjCount; }
            set { m_ZfqjCount = value; }
        }
        private int m_bIsDir;
        /// <summary>
        /// 是否为虚拟目录
        /// </summary>
        public int bIsDir
        {
            get { return m_bIsDir; }
            set { m_bIsDir = value; }
        }
    }
    #endregion

    #region 指导组
    public class GuideGroup
    {
        private int m_nid;
        /// <summary>
        /// 
        /// </summary>
        public int nid
        {
            get { return m_nid; }
            set { m_nid = value; }
        }
        private string m_strGuideGroupGUID;
        /// <summary>
        /// 指导队GUID
        /// </summary>
        public string strGuideGroupGUID
        {
            get { return m_strGuideGroupGUID; }
            set { m_strGuideGroupGUID = value; }
        }
        private string m_strWorkShopGUID;
        /// <summary>
        /// 所属车间GUID
        /// </summary>
        public string strWorkShopGUID
        {
            get { return m_strWorkShopGUID; }
            set { m_strWorkShopGUID = value; }
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
    }

    #endregion

    #region 常用意见

    public class SignType
    {
        private int m_nid;
        /// <summary>
        /// 
        /// </summary>
        public int nid
        {
            get { return m_nid; }
            set { m_nid = value; }
        }
        private string m_strSignText;
        /// <summary>
        /// 
        /// </summary>
        public string strSignText
        {
            get { return m_strSignText; }
            set { m_strSignText = value; }
        }
    }
    #endregion

    #region 测酒数据
    public class DictTable
    {
        public int TypeID;
        public string TypeName;
    }
    #endregion


    #region 客户端
    public class Site
    {
        private int m_nid;
        /// <summary>
        /// 
        /// </summary>
        public int nid
        {
            get { return m_nid; }
            set { m_nid = value; }
        }
        private string m_strSiteGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strSiteGUID
        {
            get { return m_strSiteGUID; }
            set { m_strSiteGUID = value; }
        }
        private string m_strSiteNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strSiteNumber
        {
            get { return m_strSiteNumber; }
            set { m_strSiteNumber = value; }
        }
        private string m_strSiteName;
        /// <summary>
        /// 
        /// </summary>
        public string strSiteName
        {
            get { return m_strSiteName; }
            set { m_strSiteName = value; }
        }
        private string m_strAreaGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strAreaGUID
        {
            get { return m_strAreaGUID; }
            set { m_strAreaGUID = value; }
        }
        private int m_nSiteEnable;
        /// <summary>
        /// 
        /// </summary>
        public int nSiteEnable
        {
            get { return m_nSiteEnable; }
            set { m_nSiteEnable = value; }
        }
        private string m_strSiteIP;
        /// <summary>
        /// 
        /// </summary>
        public string strSiteIP
        {
            get { return m_strSiteIP; }
            set { m_strSiteIP = value; }
        }
        private int m_nSiteJob;
        /// <summary>
        /// 
        /// </summary>
        public int nSiteJob
        {
            get { return m_nSiteJob; }
            set { m_nSiteJob = value; }
        }
        private string m_strStationGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strStationGUID
        {
            get { return m_strStationGUID; }
            set { m_strStationGUID = value; }
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
        private string m_strTMIS;
        /// <summary>
        /// 客户端所在车间TMIS号
        /// </summary>
        public string strTMIS
        {
            get { return m_strTMIS; }
            set { m_strTMIS = value; }
        }

        public List<string> TrainJiaolus = new List<string>();
        public List<LJobLimits> JobLimits = new List<LJobLimits>();

    }

    public class LJobLimits
    {
        public int Job;
        public int Limimt;

    }





    #endregion

    #region 嵌入查询页
    public class EmbeddedPage
    {
        public string Catalog = string.Empty;
        public string Caption = string.Empty;
        public string URL = string.Empty;
        public int ClientJobType;
    }
    #endregion


    #region 干部管理
    public class GanBuType
    {
        public string TypeID;
        public string WorkShopGUID = string.Empty;
        public string TypeName = string.Empty;
    }

    public class GanBu
    {
        public int RecID;
        public string TypeID;
        public string TypeName;
        public string TrainmanGUID;
        public string TrainmanNumber;
        public string TrainmanName;
        public string WorkShopGUID;
    }
    #endregion
    


}
