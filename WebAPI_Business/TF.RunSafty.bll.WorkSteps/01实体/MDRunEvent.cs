using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkSteps
{
    public class MDRunEvent
    {
        private string m_strRunEventGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strRunEventGUID
        {
            get { return m_strRunEventGUID; }
            set { m_strRunEventGUID = value; }
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
        private int m_nEventID;
        /// <summary>
        /// 
        /// </summary>
        public int nEventID
        {
            get { return m_nEventID; }
            set { m_nEventID = value; }
        }
        private DateTime? m_dtEventTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtEventTime
        {
            get { return m_dtEventTime; }
            set { m_dtEventTime = value; }
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
        private string m_nTMIS;
        /// <summary>
        /// 
        /// </summary>
        public string nTMIS
        {
            get { return m_nTMIS; }
            set { m_nTMIS = value; }
        }
        private int m_nKeHuo;
        /// <summary>
        /// 
        /// </summary>
        public int nKeHuo
        {
            get { return m_nKeHuo; }
            set { m_nKeHuo = value; }
        }
        private string m_strGroupGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strGroupGUID
        {
            get { return m_strGroupGUID; }
            set { m_strGroupGUID = value; }
        }
        private string m_strTrainmanNumber1;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber1
        {
            get { return m_strTrainmanNumber1; }
            set { m_strTrainmanNumber1 = value; }
        }
        private string m_strTrainmanNumber2;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber2
        {
            get { return m_strTrainmanNumber2; }
            set { m_strTrainmanNumber2 = value; }
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
        private int m_nID;
        /// <summary>
        /// 
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }
        private int m_nResult;
        /// <summary>
        /// 
        /// </summary>
        public int nResult
        {
            get { return m_nResult; }
            set { m_nResult = value; }
        }
        private string m_strResult;
        /// <summary>
        /// 
        /// </summary>
        public string strResult
        {
            get { return m_strResult; }
            set { m_strResult = value; }
        }
        private string m_strFlowID;
        /// <summary>
        /// 
        /// </summary>
        public string strFlowID
        {
            get { return m_strFlowID; }
            set { m_strFlowID = value; }
        }
    }

    public class RunEvent_TrainmanDetail
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
        /// 
        /// </summary>
        public string strGUID
        {
            get { return m_strGUID; }
            set { m_strGUID = value; }
        }
        private string m_strRunEventGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strRunEventGUID
        {
            get { return m_strRunEventGUID; }
            set { m_strRunEventGUID = value; }
        }
        private int m_nEventID;
        /// <summary>
        /// 
        /// </summary>
        public int nEventID
        {
            get { return m_nEventID; }
            set { m_nEventID = value; }
        }
        private DateTime? m_dtEventTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtEventTime
        {
            get { return m_dtEventTime; }
            set { m_dtEventTime = value; }
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
        private string m_nTMIS;
        /// <summary>
        /// 
        /// </summary>
        public string nTMIS
        {
            get { return m_nTMIS; }
            set { m_nTMIS = value; }
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
        private int m_nResultID;
        /// <summary>
        /// 
        /// </summary>
        public int nResultID
        {
            get { return m_nResultID; }
            set { m_nResultID = value; }
        }
        private string m_strResult;
        /// <summary>
        /// 
        /// </summary>
        public string strResult
        {
            get { return m_strResult; }
            set { m_strResult = value; }
        }
        private int m_nSubmitResult;
        /// <summary>
        /// 
        /// </summary>
        public int nSubmitResult
        {
            get { return m_nSubmitResult; }
            set { m_nSubmitResult = value; }
        }
        private string m_strSubmitRemark;
        /// <summary>
        /// 
        /// </summary>
        public string strSubmitRemark
        {
            get { return m_strSubmitRemark; }
            set { m_strSubmitRemark = value; }
        }
        private int m_nKeHuo;
        /// <summary>
        /// 
        /// </summary>
        public int nKeHuo
        {
            get { return m_nKeHuo; }
            set { m_nKeHuo = value; }
        }
    }
}
