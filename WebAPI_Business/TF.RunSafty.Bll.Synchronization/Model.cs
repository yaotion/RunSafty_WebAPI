using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Synchronization
{

    #region 获取Base_Site
    //获取客户端 

    public class Base_Site
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
    }

    #endregion


    #region Base_Site_Limit
    public class Base_Site_Limit
    {
        private string m_strSiteGUID;
        /// <summary>
        /// 客户端GUID
        /// </summary>
        public string strSiteGUID
        {
            get { return m_strSiteGUID; }
            set { m_strSiteGUID = value; }
        }
        private int m_nJobID;
        /// <summary>
        /// 岗位ID
        /// </summary>
        public int nJobID
        {
            get { return m_nJobID; }
            set { m_nJobID = value; }
        }
        private int m_nJobLimit;
        /// <summary>
        /// 权限ID
        /// </summary>
        public int nJobLimit
        {
            get { return m_nJobLimit; }
            set { m_nJobLimit = value; }
        }
    }
    #endregion

    #region Base_Station

    public class Base_Station
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
        private string m_strWorkShopGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID
        {
            get { return m_strWorkShopGUID; }
            set { m_strWorkShopGUID = value; }
        }
        private string m_strStationPY;
        /// <summary>
        /// 
        /// </summary>
        public string strStationPY
        {
            get { return m_strStationPY; }
            set { m_strStationPY = value; }
        }
    }

    #endregion

    #region Base_TrainJiaolu
    public class Base_TrainJiaolu
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
        private int m_bIsDir;
        /// <summary>
        /// 是否为虚拟目录
        /// </summary>
        public int bIsDir
        {
            get { return m_bIsDir; }
            set { m_bIsDir = value; }
        }
        private int m_nWorkTimeTypeID;
        /// <summary>
        /// 
        /// </summary>
        public int nWorkTimeTypeID
        {
            get { return m_nWorkTimeTypeID; }
            set { m_nWorkTimeTypeID = value; }
        }
    }
    #endregion

    #region Base_TrainJiaoluInSite
    public class Base_TrainJiaoluInSite
    {
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
        private string m_strJiaoluInSiteGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strJiaoluInSiteGUID
        {
            get { return m_strJiaoluInSiteGUID; }
            set { m_strJiaoluInSiteGUID = value; }
        }


    }
    #endregion


    #region Base_ZFQJ

    public class Base_ZFQJ
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
        private string m_strZFQJGUID;
        /// <summary>
        /// 折返区间GUID
        /// </summary>
        public string strZFQJGUID
        {
            get { return m_strZFQJGUID; }
            set { m_strZFQJGUID = value; }
        }
        private string m_strTrainJiaoluGUID;
        /// <summary>
        /// 所属行车区段GUID
        /// </summary>
        public string strTrainJiaoluGUID
        {
            get { return m_strTrainJiaoluGUID; }
            set { m_strTrainJiaoluGUID = value; }
        }
        private int m_nQuJianIndex;
        /// <summary>
        /// 区间所在区段序号
        /// </summary>
        public int nQuJianIndex
        {
            get { return m_nQuJianIndex; }
            set { m_nQuJianIndex = value; }
        }
        private string m_strBeginStationGUID;
        /// <summary>
        /// 开始车站GUID
        /// </summary>
        public string strBeginStationGUID
        {
            get { return m_strBeginStationGUID; }
            set { m_strBeginStationGUID = value; }
        }
        private string m_strEndStationGUID;
        /// <summary>
        /// 结束车站GUID
        /// </summary>
        public string strEndStationGUID
        {
            get { return m_strEndStationGUID; }
            set { m_strEndStationGUID = value; }
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

    #region Org_Area


    public class Org_Area
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
        private string m_strGUID;
        /// <summary>
        /// 机务段GUID
        /// </summary>
        public string strGUID
        {
            get { return m_strGUID; }
            set { m_strGUID = value; }
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
    }
    #endregion


    #region Org_DutyUser

    public class Org_DutyUser
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
        private string m_strDutyGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strDutyGUID
        {
            get { return m_strDutyGUID; }
            set { m_strDutyGUID = value; }
        }
        private string m_strDutyNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strDutyNumber
        {
            get { return m_strDutyNumber; }
            set { m_strDutyNumber = value; }
        }
        private string m_strDutyName;
        /// <summary>
        /// 
        /// </summary>
        public string strDutyName
        {
            get { return m_strDutyName; }
            set { m_strDutyName = value; }
        }
        private string m_strPassword;
        /// <summary>
        /// 
        /// </summary>
        public string strPassword
        {
            get { return m_strPassword; }
            set { m_strPassword = value; }
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
        private string m_strAreaGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strAreaGUID
        {
            get { return m_strAreaGUID; }
            set { m_strAreaGUID = value; }
        }
        private int m_nRoleID;
        /// <summary>
        /// 
        /// </summary>
        public int nRoleID
        {
            get { return m_nRoleID; }
            set { m_nRoleID = value; }
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
        private string m_strTokenID;
        /// <summary>
        /// 
        /// </summary>
        public string strTokenID
        {
            get { return m_strTokenID; }
            set { m_strTokenID = value; }
        }
        private DateTime? m_dtTokenTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtTokenTime
        {
            get { return m_dtTokenTime; }
            set { m_dtTokenTime = value; }
        }
        private DateTime? m_dtLoginTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLoginTime
        {
            get { return m_dtLoginTime; }
            set { m_dtLoginTime = value; }
        }
    }
    #endregion

    #region Org_WorkShop
    public class Org_WorkShop
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
        private string m_strWorkShopGUID;
        /// <summary>
        /// 车间GUID
        /// </summary>
        public string strWorkShopGUID
        {
            get { return m_strWorkShopGUID; }
            set { m_strWorkShopGUID = value; }
        }
        private string m_strAreaGUID;
        /// <summary>
        /// 所属机务段
        /// </summary>
        public string strAreaGUID
        {
            get { return m_strAreaGUID; }
            set { m_strAreaGUID = value; }
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
        private string m_strWorkShopNumber;
        /// <summary>
        /// 车间号
        /// </summary>
        public string strWorkShopNumber
        {
            get { return m_strWorkShopNumber; }
            set { m_strWorkShopNumber = value; }
        }
    }
    #endregion


    #region Base_TrainNo
    public class Base_TrainNo
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
        private string m_strTrainTypeName;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainTypeName
        {
            get { return m_strTrainTypeName; }
            set { m_strTrainTypeName = value; }
        }
        private string m_strTrainNumber;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNumber
        {
            get { return m_strTrainNumber; }
            set { m_strTrainNumber = value; }
        }
        private string m_strTrainNo;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNo
        {
            get { return m_strTrainNo; }
            set { m_strTrainNo = value; }
        }
        private DateTime? m_dtStartTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtStartTime
        {
            get { return m_dtStartTime; }
            set { m_dtStartTime = value; }
        }
        private DateTime? m_dtRealStartTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtRealStartTime
        {
            get { return m_dtRealStartTime; }
            set { m_dtRealStartTime = value; }
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
        private int m_nTrainmanTypeID;
        /// <summary>
        /// 
        /// </summary>
        public int nTrainmanTypeID
        {
            get { return m_nTrainmanTypeID; }
            set { m_nTrainmanTypeID = value; }
        }
        private int m_nPlanType;
        /// <summary>
        /// 
        /// </summary>
        public int nPlanType
        {
            get { return m_nPlanType; }
            set { m_nPlanType = value; }
        }
        private int m_nDragType;
        /// <summary>
        /// 
        /// </summary>
        public int nDragType
        {
            get { return m_nDragType; }
            set { m_nDragType = value; }
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
        private int m_nRemarkType;
        /// <summary>
        /// 
        /// </summary>
        public int nRemarkType
        {
            get { return m_nRemarkType; }
            set { m_nRemarkType = value; }
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
        private DateTime? m_dtCreateTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtCreateTime
        {
            get { return m_dtCreateTime; }
            set { m_dtCreateTime = value; }
        }
        private string m_strCreateSiteGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strCreateSiteGUID
        {
            get { return m_strCreateSiteGUID; }
            set { m_strCreateSiteGUID = value; }
        }
        private string m_strCreateUserGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strCreateUserGUID
        {
            get { return m_strCreateUserGUID; }
            set { m_strCreateUserGUID = value; }
        }
        private string m_strPlaceID;
        /// <summary>
        /// 
        /// </summary>
        public string strPlaceID
        {
            get { return m_strPlaceID; }
            set { m_strPlaceID = value; }
        }
        private DateTime? m_dtPlanStartTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtPlanStartTime
        {
            get { return m_dtPlanStartTime; }
            set { m_dtPlanStartTime = value; }
        }
        private int m_nNeedRest;
        /// <summary>
        /// 
        /// </summary>
        public int nNeedRest
        {
            get { return m_nNeedRest; }
            set { m_nNeedRest = value; }
        }
        private DateTime? m_dtArriveTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtArriveTime
        {
            get { return m_dtArriveTime; }
            set { m_dtArriveTime = value; }
        }
        private DateTime? m_dtCallTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtCallTime
        {
            get { return m_dtCallTime; }
            set { m_dtCallTime = value; }
        }
        private string m_strWorkDay;
        /// <summary>
        /// 
        /// </summary>
        public string strWorkDay
        {
            get { return m_strWorkDay; }
            set { m_strWorkDay = value; }
        }
    }
    #endregion

}
