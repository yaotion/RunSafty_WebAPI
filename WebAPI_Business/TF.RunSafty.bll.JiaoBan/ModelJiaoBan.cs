using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.JiaoBan
{
    public class CallNotify
    {

        private int _nId;

        public int nId
        {
            get { return _nId; }
            set { _nId = value; }
        }

        private int _nCallWorkType;

        public int nCallWorkType
        {
            get { return _nCallWorkType; }
            set { _nCallWorkType = value; }
        }
        private string _strTrainPlanGUID;

        public string strTrainPlanGUID
        {
            get { return _strTrainPlanGUID; }
            set { _strTrainPlanGUID = value; }
        }
        private string _strTrainNo;

        public string strTrainNo
        {
            get { return _strTrainNo; }
            set { _strTrainNo = value; }
        }
        private DateTime? _dtCallTime;

        public DateTime? dtCallTime
        {
            get { return _dtCallTime; }
            set { _dtCallTime = value; }
        }
        private DateTime? _dtChuQinTime;

        public DateTime? dtChuQinTime
        {
            get { return _dtChuQinTime; }
            set { _dtChuQinTime = value; }
        }
        private DateTime? _dtStartTime;

        public DateTime? dtStartTime
        {
            get { return _dtStartTime; }
            set { _dtStartTime = value; }
        }
        private string _strMsgGUID;

        public string strMsgGUID
        {
            get { return _strMsgGUID; }
            set { _strMsgGUID = value; }
        }
        private string _strSendMsgContent;

        public string strSendMsgContent
        {
            get { return _strSendMsgContent; }
            set { _strSendMsgContent = value; }
        }
        private string _strRecvMsgContent;

        public string strRecvMsgContent
        {
            get { return _strRecvMsgContent; }
            set { _strRecvMsgContent = value; }
        }
        private DateTime? _dtSendTime;

        public DateTime? dtSendTime
        {
            get { return _dtSendTime; }
            set { _dtSendTime = value; }
        }
        private string _strSendUser;

        public string strSendUser
        {
            get { return _strSendUser; }
            set { _strSendUser = value; }
        }
        private DateTime? _dtRecvTime;

        public DateTime? dtRecvTime
        {
            get { return _dtRecvTime; }
            set { _dtRecvTime = value; }
        }
        private string _strRecvUser;

        public string strRecvUser
        {
            get { return _strRecvUser; }
            set { _strRecvUser = value; }
        }
        private int _eCallState;

        public int eCallState
        {
            get { return _eCallState; }
            set { _eCallState = value; }
        }
        private int _eCallType;

        public int eCallType
        {
            get { return _eCallType; }
            set { _eCallType = value; }
        }
        private int _nCancel;

        public int nCancel
        {
            get { return _nCancel; }
            set { _nCancel = value; }
        }
        private string _strCancelReason;

        public string strCancelReason
        {
            get { return _strCancelReason; }
            set { _strCancelReason = value; }
        }
        private DateTime? _dtCancelTime;

        public DateTime? dtCancelTime
        {
            get { return _dtCancelTime; }
            set { _dtCancelTime = value; }
        }
        private string _strCancelUser;

        public string strCancelUser
        {
            get { return _strCancelUser; }
            set { _strCancelUser = value; }
        }
        private string _strTrainmanGUID;

        public string strTrainmanGUID
        {
            get { return _strTrainmanGUID; }
            set { _strTrainmanGUID = value; }
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
        private string _strMobileNumber;

        public string strMobileNumber
        {
            get { return _strMobileNumber; }
            set { _strMobileNumber = value; }
        }

    }
}
