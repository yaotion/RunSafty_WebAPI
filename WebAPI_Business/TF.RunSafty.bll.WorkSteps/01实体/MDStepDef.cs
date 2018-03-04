using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkSteps
{
    public class MDstepDef
    {
        public int nIsNecessary { get; set; }
        public int nWorkTypeID { get; set; }

        public bool IsExcuted { get; set; }
        private string m_strWorkShopGUID;
        /// <summary>
        /// 车间GUID
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
        private string m_strStepName;
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string strStepName
        {
            get { return m_strStepName; }
            set { m_strStepName = value; }
        }
        private string m_strStepBrief;
        /// <summary>
        /// 步骤描述
        /// </summary>
        public string strStepBrief
        {
            get { return m_strStepBrief; }
            set { m_strStepBrief = value; }
        }



        private int m_nStepIndex;
        /// <summary>
        /// 步骤顺序
        /// </summary>
        public int nStepIndex
        {
            get { return m_nStepIndex; }
            set { m_nStepIndex = value; }
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

        public string strStepID { get; set; }
    }

    public class MDNoConfirmPlan
    {
        private string _strTrainPlanGUID;

        public string strTrainPlanGUID
        {
            get { return _strTrainPlanGUID; }
            set { _strTrainPlanGUID = value; }
        }
        private string _dtStartTime;

        public string dtStartTime
        {
            get { return _dtStartTime; }
            set { _dtStartTime = value; }
        }
        private string _strTrainTypeName;

        public string strTrainTypeName
        {
            get { return _strTrainTypeName; }
            set { _strTrainTypeName = value; }
        }
        private string _strTrainNumber;

        public string strTrainNumber
        {
            get { return _strTrainNumber; }
            set { _strTrainNumber = value; }
        }
        private string _strTrainNo;

        public string strTrainNo
        {
            get { return _strTrainNo; }
            set { _strTrainNo = value; }
        }

        //计划id  dtstarttime时间  车型 车号 车次



    }
}
