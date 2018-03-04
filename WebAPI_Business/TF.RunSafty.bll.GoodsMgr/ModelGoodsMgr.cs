using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.GoodsMgr
{

    public class LendingType
    {
        private int m_nID;
        /// <summary>
        /// 
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }
        private int m_nLendingTypeID;
        /// <summary>
        /// 
        /// </summary>
        public int nLendingTypeID
        {
            get { return m_nLendingTypeID; }
            set { m_nLendingTypeID = value; }
        }
        private string m_strLendingTypeName;
        /// <summary>
        /// 
        /// </summary>
        public string strLendingTypeName
        {
            get { return m_strLendingTypeName; }
            set { m_strLendingTypeName = value; }
        }
        private string m_strAlias;
        /// <summary>
        /// 
        /// </summary>
        public string strAlias
        {
            get { return m_strAlias; }
            set { m_strAlias = value; }
        }
    }


    public class ReturnStateType
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
        private int m_nReturnStateID;
        /// <summary>
        /// 
        /// </summary>
        public int nReturnStateID
        {
            get { return m_nReturnStateID; }
            set { m_nReturnStateID = value; }
        }
        private string m_strStateName;
        /// <summary>
        /// 
        /// </summary>
        public string strStateName
        {
            get { return m_strStateName; }
            set { m_strStateName = value; }
        }
    }


    public class LendingInfoDetail
    {
        private string m_strLendingInfoGUID;       
        private int m_nLendingType;
        private int m_strLendingExInfo;
        private int m_nReturnState;
        private DateTime? m_dtModifyTime;
        private string m_strGUID;
        private DateTime? m_dtBorrowTime;
        private int m_nID;
        private DateTime? m_dtGiveBackTime;
        private int m_nBorrowLoginType;
        private int m_nGiveBackLoginType;
        private string m_strLenderGUID;
        private string m_strBorrowTrainmanGUID;
        private string m_strGiveBackTrainmanGUID;
        private string m_strBorrowTrainmanNumber;
        private string m_strBorrowTrainmanName;
        private string m_strLenderNumber;
        private string m_strLenderName;
        private string m_strGiveBackTrainmanNumber;
        private string m_strBorrowLoginTypeName;
        private string m_strGiveBackLoginTypeName;
        private string m_strLendingTypeName;
        private string m_strAlias;
        private string m_strStateName;
        private int m_nMinutes;
        private string m_strWorkShopGUID;
        private string m_strGiveBackTrainmanName;

        /// <summary>
        /// 
        /// </summary>
        public int nLendingType
        {
            get { return m_nLendingType; }
            set { m_nLendingType = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strLendingInfoGUID
        {
            get { return m_strLendingInfoGUID; }
            set { m_strLendingInfoGUID = value; }
        }      
        /// <summary>
        /// 
        /// </summary>
        public int strLendingExInfo
        {
            get { return m_strLendingExInfo; }
            set { m_strLendingExInfo = value; }
        }        
        /// <summary>
        /// 
        /// </summary>
        public int nReturnState
        {
            get { return m_nReturnState; }
            set { m_nReturnState = value; }
        }        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtModifyTime
        {
            get { return m_dtModifyTime; }
            set { m_dtModifyTime = value; }
        }       
        /// <summary>
        /// 
        /// </summary>
        public string strGUID
        {
            get { return m_strGUID; }
            set { m_strGUID = value; }
        }       
        /// <summary>
        /// 
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtBorrwoTime
        {
            get { return m_dtBorrowTime; }
            set { m_dtBorrowTime = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtGiveBackTime
        {
            get { return m_dtGiveBackTime; }
            set { m_dtGiveBackTime = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int nBorrowVerifyType
        {
            get { return m_nBorrowLoginType; }
            set { m_nBorrowLoginType = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int nGiveBackVerifyType
        {
            get { return m_nGiveBackLoginType; }
            set { m_nGiveBackLoginType = value; }
        }  
        /// <summary>
        /// 
        /// </summary>
        public string strLenderGUID
        {
            get { return m_strLenderGUID; }
            set { m_strLenderGUID = value; }
        }    
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID
        {
            get { return m_strBorrowTrainmanGUID; }
            set { m_strBorrowTrainmanGUID = value; }
        } 
        /// <summary>
        /// 
        /// </summary>
        public string strGiveBackTrainmanGUID
        {
            get { return m_strGiveBackTrainmanGUID; }
            set { m_strGiveBackTrainmanGUID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber
        {
            get { return m_strBorrowTrainmanNumber; }
            set { m_strBorrowTrainmanNumber = value; }
        } 
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName
        {
            get { return m_strBorrowTrainmanName; }
            set { m_strBorrowTrainmanName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strLenderNumber
        {
            get { return m_strLenderNumber; }
            set { m_strLenderNumber = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strLenderName
        {
            get { return m_strLenderName; }
            set { m_strLenderName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strGiveBackTrainmanNumber
        {
            get { return m_strGiveBackTrainmanNumber; }
            set { m_strGiveBackTrainmanNumber = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string strGiveBackTrainmanName
        {
            get { return m_strGiveBackTrainmanName; }
            set { m_strGiveBackTrainmanName = value; }
        }   
        /// <summary>
        /// 
        /// </summary>
        public string strBorrowVerifyTypeName
        {
            get { return m_strBorrowLoginTypeName; }
            set { m_strBorrowLoginTypeName = value; }
        }    
        /// <summary>
        /// 
        /// </summary>
        public string strGiveBackVerifyTypeName
        {
            get { return m_strGiveBackLoginTypeName; }
            set { m_strGiveBackLoginTypeName = value; }
        }     
        /// <summary>
        /// 
        /// </summary>
        public string strLendingTypeName
        {
            get { return m_strLendingTypeName; }
            set { m_strLendingTypeName = value; }
        }      
        /// <summary>
        /// 
        /// </summary>
        public string strLendingTypeAlias
        {
            get { return m_strAlias; }
            set { m_strAlias = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strStateName
        {
            get { return m_strStateName; }
            set { m_strStateName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int nKeepMunites
        {
            get { return m_nMinutes; }
            set { m_nMinutes = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID
        {
            get { return m_strWorkShopGUID; }
            set { m_strWorkShopGUID = value; }
        }
    }


    public class GoodsQueryParam
    {
        private DateTime _dtBeginTime;

        public DateTime dtBeginTime
        {
            get { return _dtBeginTime; }
            set { _dtBeginTime = value; }
        }
        private DateTime _dtEndTime;

        public DateTime dtEndTime
        {
            get { return _dtEndTime; }
            set { _dtEndTime = value; }
        }
        private int _nReturnState;

        public int nReturnState
        {
            get { return _nReturnState; }
            set { _nReturnState = value; }
        }
        private int _nLendingType;

        public int nLendingType
        {
            get { return _nLendingType; }
            set { _nLendingType = value; }
        }
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
        private string _strWorkShopGUID;

        public string strWorkShopGUID
        {
            get { return _strWorkShopGUID; }
            set { _strWorkShopGUID = value; }
        }
        private int _nLendingNumber;

        public int nLendingNumber
        {
            get { return _nLendingNumber; }
            set { _nLendingNumber = value; }
        }
    
    }

    public class LendingInfo
    {
        private string m_strStateName;
        private string m_strGiveBackLoginTypeName;
        private string m_strRemark;
        private string m_strBorrowLoginTypeName;
        private string m_strGiveBackTrainmanNumber;
        private string m_strLenderName;
        private string m_strGiveBackTrainmanName;
        private string m_strLenderNumber;
        private string m_strBorrowTrainmanName;
        private string m_strBorrowTrainmanNumber;
        private string m_strGiveBackTrainmanGUID;
        private string m_strBorrowTrainmanGUID;
        private string m_strLenderGUID;
        private string m_strGUID;
        private int m_nReturnState;
        private string m_strLendingDetail;
        private string m_strLendingInfoGUID;
        private DateTime? m_dtModifyTime;
        private DateTime? m_dtGiveBackTime;
        private int m_nBorrowLoginType;
        private DateTime? m_dtBorrowTime;
        private int m_nGiveBackLoginType;
        private string m_strWorkShopGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strStateName
        {
            get { return m_strStateName; }
            set { m_strStateName = value; }
        }        
        /// <summary>
        /// 
        /// </summary>
        public string strGiveBackLoginName
        {
            get { return m_strGiveBackLoginTypeName; }
            set { m_strGiveBackLoginTypeName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strRemark
        {
            get { return m_strRemark; }
            set { m_strRemark = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strBorrowLoginName
        {
            get { return m_strBorrowLoginTypeName; }
            set { m_strBorrowLoginTypeName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strGiveBackTrainmanNumber
        {
            get { return m_strGiveBackTrainmanNumber; }
            set { m_strGiveBackTrainmanNumber = value; }
        }        
        /// <summary>
        /// 
        /// </summary>
        public string strLenderName
        {
            get { return m_strLenderName; }
            set { m_strLenderName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strGiveBackTrainmanName;
        /// <summary>
        /// 
        /// </summary>
        public string strLenderNumber
        {
            get { return m_strLenderNumber; }
            set { m_strLenderNumber = value; }
        }  
        /// <summary>
        /// 
        /// </summary>
        public string strBorrowTrainmanName
        {
            get { return m_strBorrowTrainmanName; }
            set { m_strBorrowTrainmanName = value; }
        }   
        /// <summary>
        /// 
        /// </summary>
        public string strBorrowTrainmanNumber
        {
            get { return m_strBorrowTrainmanNumber; }
            set { m_strBorrowTrainmanNumber = value; }
        } 
        /// <summary>
        /// 
        /// </summary>
        public string strGiveBackTrainmanGUID
        {
            get { return m_strGiveBackTrainmanGUID; }
            set { m_strGiveBackTrainmanGUID = value; }
        }     
        /// <summary>
        /// 
        /// </summary>
        public string strBorrowTrainmanGUID
        {
            get { return m_strBorrowTrainmanGUID; }
            set { m_strBorrowTrainmanGUID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strLenderGUID
        {
            get { return m_strLenderGUID; }
            set { m_strLenderGUID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strGUID
        {
            get { return m_strGUID; }
            set { m_strGUID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int nReturnState
        {
            get { return m_nReturnState; }
            set { m_nReturnState = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strDetails
        {
            get { return m_strLendingDetail; }
            set { m_strLendingDetail = value; }
        }     
        /// <summary>
        /// 
        /// </summary>
        public string strLendingInfoGUID
        {
            get { return m_strLendingInfoGUID; }
            set { m_strLendingInfoGUID = value; }
        }   
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtModifyTime
        {
            get { return m_dtModifyTime; }
            set { m_dtModifyTime = value; }
        }    
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtGiveBackTime
        {
            get { return m_dtGiveBackTime; }
            set { m_dtGiveBackTime = value; }
        }  
        /// <summary>
        /// 
        /// </summary>
        public int nBorrowLoginType
        {
            get { return m_nBorrowLoginType; }
            set { m_nBorrowLoginType = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtBorrowTime
        {
            get { return m_dtBorrowTime; }
            set { m_dtBorrowTime = value; }
        }        
        /// <summary>
        /// 
        /// </summary>
        public int nGiveBackLoginType
        {
            get { return m_nGiveBackLoginType; }
            set { m_nGiveBackLoginType = value; }
        }        
        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID
        {
            get { return m_strWorkShopGUID; }
            set { m_strWorkShopGUID = value; }
        }
    }


    public class GoodsDetailQueryParam
    {

        private int _nReturnState;

        public int nReturnState
        {
            get { return _nReturnState; }
            set { _nReturnState = value; }
        }
        private int _nLendingType;

        public int nLendingType
        {
            get { return _nLendingType; }
            set { _nLendingType = value; }
        }
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
        private int _nBianHao;

        public int nBianHao
        {
            get { return _nBianHao; }
            set { _nBianHao = value; }
        }
        private string _WorkShopGUID;

        public string workShopGUID
        {
            get { return _WorkShopGUID; }
            set { _WorkShopGUID = value; }
        }
        private string _strOrderField;

        public string strOrderField
        {
            get { return _strOrderField; }
            set { _strOrderField = value; }
        }

    }


    public class LendingTjInfo
    {
        private int _nLendingType;

        public int nLendingType
        {
            get { return _nLendingType; }
            set { _nLendingType = value; }
        }
        private string _strLendingTypeName;

        public string strLendingTypeName
        {
            get { return _strLendingTypeName; }
            set { _strLendingTypeName = value; }
        }
        private string _strTypeAlias;

        public string strTypeAlias
        {
            get { return _strTypeAlias; }
            set { _strTypeAlias = value; }
        }
        private int _nTotalCount;

        public int nTotalCount
        {
            get { return _nTotalCount; }
            set { _nTotalCount = value; }
        }
        private int _nNoReturnCount;

        public int nNoReturnCount
        {
            get { return _nNoReturnCount; }
            set { _nNoReturnCount = value; }
        }

    }


    public class LendingManager
    {
        private int m_nID;
        /// <summary>
        /// 
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }
        private string m_strGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strGUID
        {
            get { return m_strGUID; }
            set { m_strGUID = value; }
        }
        private int m_nLendingTypeID;
        /// <summary>
        /// 
        /// </summary>
        public int nLendingTypeID
        {
            get { return m_nLendingTypeID; }
            set { m_nLendingTypeID = value; }
        }
        private int m_nStartCode;
        /// <summary>
        /// 
        /// </summary>
        public int nStartCode
        {
            get { return m_nStartCode; }
            set { m_nStartCode = value; }
        }
        private int m_nStopCode;
        /// <summary>
        /// 
        /// </summary>
        public int nStopCode
        {
            get { return m_nStopCode; }
            set { m_nStopCode = value; }
        }
        private string m_strExceptCodes;
        /// <summary>
        /// 
        /// </summary>
        public string strExceptCodes
        {
            get { return m_strExceptCodes; }
            set { m_strExceptCodes = value; }
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
    }

        


        
        




}
