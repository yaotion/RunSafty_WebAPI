using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkSteps
{
    public class MDBeginWorkFlow
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
        private string m_strTrainPlanGUID;
        /// <summary>
        /// 出勤计划GUID
        /// </summary>
        public string strTrainPlanGUID
        {
            get { return m_strTrainPlanGUID; }
            set { m_strTrainPlanGUID = value; }
        }
        private string m_strUserName;
        /// <summary>
        /// 值班员姓名
        /// </summary>
        public string strUserName
        {
            get { return m_strUserName; }
            set { m_strUserName = value; }
        }
        private string m_strUserNumber;
        /// <summary>
        /// 值班员工号
        /// </summary>
        public string strUserNumber
        {
            get { return m_strUserNumber; }
            set { m_strUserNumber = value; }
        }

        private int _nWorkTypeID;

        public int nWorkTypeID
        {
            get { return _nWorkTypeID; }
            set { _nWorkTypeID = value; }
        }


        private DateTime? m_dtConfirmTime;
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? dtConfirmTime
        {
            get { return m_dtConfirmTime; }
            set { m_dtConfirmTime = value; }
        }
        private int m_nConfirmType;
        /// <summary>
        /// 确认类型(0自动,1手工)
        /// </summary>
        public int nConfirmType
        {
            get { return m_nConfirmType; }
            set { m_nConfirmType = value; }
        }
        private string m_strConfirmBrief;
        /// <summary>
        /// 
        /// </summary>
        public string strConfirmBrief
        {
            get { return m_strConfirmBrief; }
            set { m_strConfirmBrief = value; }
        }
        private int m_nFlowState;
        /// <summary>
        /// 流程状态
        /// </summary>
        public int nFlowState
        {
            get { return m_nFlowState; }
            set { m_nFlowState = value; }
        }
        private DateTime m_dtCreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime dtCreateTime
        {
            get { return m_dtCreateTime; }
            set { m_dtCreateTime = value; }
        }
        private DateTime m_dtBeginTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime dtBeginTime
        {
            get { return m_dtBeginTime; }
            set { m_dtBeginTime = value; }
        }
        private DateTime m_dtEndTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime dtEndTime
        {
            get { return m_dtEndTime; }
            set { m_dtEndTime = value; }
        }
        private int m_nExecLength;
        /// <summary>
        /// 执行时长(单位分钟)
        /// </summary>
        public int nExecLength
        {
            get { return m_nExecLength; }
            set { m_nExecLength = value; }
        }
    }
}
