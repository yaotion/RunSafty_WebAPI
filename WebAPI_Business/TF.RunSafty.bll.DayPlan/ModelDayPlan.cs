using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.DayPlan
{
    public class DayPlanPlace
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

    }
    public class DayPlanItemGroup
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private int _IsDaWen;

        public int IsDaWen
        {
            get { return _IsDaWen; }
            set { _IsDaWen = value; }
        }
        private string _DayPlanID;

        public string DayPlanID
        {
            get { return _DayPlanID; }
            set { _DayPlanID = value; }
        }
        private int _ExcelSide;

        public int ExcelSide
        {
            get { return _ExcelSide; }
            set { _ExcelSide = value; }
        }
        private int _ExcelPos;

        public int ExcelPos
        {
            get { return _ExcelPos; }
            set { _ExcelPos = value; }
        }

    }

    public class PlanModule
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _DayPlanType;

        public int DayPlanType
        {
            get { return _DayPlanType; }
            set { _DayPlanType = value; }
        }
        private string _TrainNo1;

        public string TrainNo1
        {
            get { return _TrainNo1; }
            set { _TrainNo1 = value; }
        }
        private string _TrainInfo;

        public string TrainInfo
        {
            get { return _TrainInfo; }
            set { _TrainInfo = value; }
        }
        private string _TrainNo2;

        public string TrainNo2
        {
            get { return _TrainNo2; }
            set { _TrainNo2 = value; }
        }
        private string _TrainNo;

        public string TrainNo
        {
            get { return _TrainNo; }
            set { _TrainNo = value; }
        }
        private string _Remark;

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private int _IsTomorrow;

        public int IsTomorrow
        {
            get { return _IsTomorrow; }
            set { _IsTomorrow = value; }
        }
        private string _DaWenCheXing;

        public string DaWenCheXing
        {
            get { return _DaWenCheXing; }
            set { _DaWenCheXing = value; }
        }
        private string _DaWenCheHao1;

        public string DaWenCheHao1
        {
            get { return _DaWenCheHao1; }
            set { _DaWenCheHao1 = value; }
        }
        private string _DaWenCheHao2;

        public string DaWenCheHao2
        {
            get { return _DaWenCheHao2; }
            set { _DaWenCheHao2 = value; }
        }
        private string _DaWenCheHao3;

        public string DaWenCheHao3
        {
            get { return _DaWenCheHao3; }
            set { _DaWenCheHao3 = value; }
        }
        private int _GroupID;

        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }
    }

    public class DayPlan
    {
        private string m_strDayPlanGUID;
        /// <summary>
        /// 所属日班计划GUID
        /// </summary>
        public string strDayPlanGUID
        {
            get { return m_strDayPlanGUID; }
            set { m_strDayPlanGUID = value; }
        }
        private DateTime? m_dtBeginTime;
        /// <summary>
        /// 有效开始时间
        /// </summary>
        public DateTime? dtBeginTime
        {
            get { return m_dtBeginTime; }
            set { m_dtBeginTime = value; }
        }
        private DateTime? m_dtEndTime;
        /// <summary>
        /// 有效结束时间
        /// </summary>
        public DateTime? dtEndTime
        {
            get { return m_dtEndTime; }
            set { m_dtEndTime = value; }
        }
        private DateTime? m_dtCreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? dtCreateTime
        {
            get { return m_dtCreateTime; }
            set { m_dtCreateTime = value; }
        }
        private string m_strTrainNo;
        /// <summary>
        /// 车次
        /// </summary>
        public string strTrainNo
        {
            get { return m_strTrainNo; }
            set { m_strTrainNo = value; }
        }
        private string m_strTrainTypeName;
        /// <summary>
        /// 车型
        /// </summary>
        public string strTrainTypeName
        {
            get { return m_strTrainTypeName; }
            set { m_strTrainTypeName = value; }
        }
        private string m_strTrainNumber;
        /// <summary>
        /// 车号
        /// </summary>
        public string strTrainNumber
        {
            get { return m_strTrainNumber; }
            set { m_strTrainNumber = value; }
        }
        private int m_nid;
        /// <summary>
        /// 自助编号
        /// </summary>
        public int nid
        {
            get { return m_nid; }
            set { m_nid = value; }
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
        private string m_strTrainNo1;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNo1
        {
            get { return m_strTrainNo1; }
            set { m_strTrainNo1 = value; }
        }
        private string m_strTrainInfo;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainInfo
        {
            get { return m_strTrainInfo; }
            set { m_strTrainInfo = value; }
        }
        private string m_strTrainNo2;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNo2
        {
            get { return m_strTrainNo2; }
            set { m_strTrainNo2 = value; }
        }
        private int m_nQuDuanID;
        /// <summary>
        /// 
        /// </summary>
        public int nQuDuanID
        {
            get { return m_nQuDuanID; }
            set { m_nQuDuanID = value; }
        }
        private int m_nPlanID;
        /// <summary>
        /// 
        /// </summary>
        public int nPlanID
        {
            get { return m_nPlanID; }
            set { m_nPlanID = value; }
        }
        private int m_bIsTomorrow;
        /// <summary>
        /// 
        /// </summary>
        public int bIsTomorrow
        {
            get { return m_bIsTomorrow; }
            set { m_bIsTomorrow = value; }
        }
        private int m_nDayPlanID;
        /// <summary>
        /// 
        /// </summary>
        public int nDayPlanID
        {
            get { return m_nDayPlanID; }
            set { m_nDayPlanID = value; }
        }
        private int m_nPlanState;
        /// <summary>
        /// 
        /// </summary>
        public int nPlanState
        {
            get { return m_nPlanState; }
            set { m_nPlanState = value; }
        }
        private string m_strDaWenCheXing;
        /// <summary>
        /// 
        /// </summary>
        public string strDaWenCheXing
        {
            get { return m_strDaWenCheXing; }
            set { m_strDaWenCheXing = value; }
        }
        private string m_strDaWenCheHao1;
        /// <summary>
        /// 
        /// </summary>
        public string strDaWenCheHao1
        {
            get { return m_strDaWenCheHao1; }
            set { m_strDaWenCheHao1 = value; }
        }
        private string m_strDaWenCheHao2;
        /// <summary>
        /// 
        /// </summary>
        public string strDaWenCheHao2
        {
            get { return m_strDaWenCheHao2; }
            set { m_strDaWenCheHao2 = value; }
        }
        private string m_strDaWenCheHao3;
        /// <summary>
        /// 
        /// </summary>
        public string strDaWenCheHao3
        {
            get { return m_strDaWenCheHao3; }
            set { m_strDaWenCheHao3 = value; }
        }
        private int m_bIsSend;
        /// <summary>
        /// 
        /// </summary>
        public int bIsSend
        {
            get { return m_bIsSend; }
            set { m_bIsSend = value; }
        }
        private string m_strTrainPlanGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainPlanGUID
        {
            get { return m_strTrainPlanGUID; }
            set { m_strTrainPlanGUID = value; }
        }
    
    }

    public class ChangeLog
    {
        private string m_strLogGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strLogGUID
        {
            get { return m_strLogGUID; }
            set { m_strLogGUID = value; }
        }
        private string m_strlogType;
        /// <summary>
        /// 
        /// </summary>
        public string strlogType
        {
            get { return m_strlogType; }
            set { m_strlogType = value; }
        }
        private string m_strDayPlanGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strDayPlanGUID
        {
            get { return m_strDayPlanGUID; }
            set { m_strDayPlanGUID = value; }
        }
        private string m_strTrainNo1;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNo1
        {
            get { return m_strTrainNo1; }
            set { m_strTrainNo1 = value; }
        }
        private string m_strTrainInfo;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainInfo
        {
            get { return m_strTrainInfo; }
            set { m_strTrainInfo = value; }
        }
        private string m_strTrainNo2;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNo2
        {
            get { return m_strTrainNo2; }
            set { m_strTrainNo2 = value; }
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
        private DateTime? m_dtChangeTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtChangeTime
        {
            get { return m_dtChangeTime; }
            set { m_dtChangeTime = value; }
        }
    }

    public class ShortTrain
    {
        private int _nID;

        public int nID
        {
            get { return _nID; }
            set { _nID = value; }
        }
        private string _strShortName;

        public string strShortName
        {
            get { return _strShortName; }
            set { _strShortName = value; }
        }
        private string _strLongName;

        public string strLongName
        {
            get { return _strLongName; }
            set { _strLongName = value; }
        }
    
    }

   

       


}
