using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.Plan
{
	/// <summary>
	///����: Plan_Beginwork_Step
	///˵��: ���ڲ���
	/// </summary>
	public class Plan_Beginwork_Step
	{

        private int m_nID;
        /// <summary>
        /// ����ID
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }

		private string m_strTrainPlanGUID;
		/// <summary>
		/// �ƻ�guid
		/// </summary>
		public string strTrainPlanGUID
		{
			get {return m_strTrainPlanGUID;}
			set {m_strTrainPlanGUID = value;}
		}


		private int m_nStepID;
		/// <summary>
		/// ������
		/// </summary>
		public int nStepID
		{
			get {return m_nStepID;}
			set {m_nStepID = value;}
		}
		private string m_strTrainmanGUID;
		/// <summary>
		/// ��Աguid
		/// </summary>
		public string strTrainmanGUID
		{
			get {return m_strTrainmanGUID;}
			set {m_strTrainmanGUID = value;}
		}
		private string m_strTrainmanNumber;
		/// <summary>
		/// ��Ա����
		/// </summary>
		public string strTrainmanNumber
		{
			get {return m_strTrainmanNumber;}
			set {m_strTrainmanNumber = value;}
		}
		private string m_strTrainmanName;
		/// <summary>
		/// ��Ա����
		/// </summary>
		public string strTrainmanName
		{
			get {return m_strTrainmanName;}
			set {m_strTrainmanName = value;}
		}
		private int m_nStepResultID;
		/// <summary>
		/// ����ִ�н��
		/// </summary>
		public int nStepResultID
		{
			get {return m_nStepResultID;}
			set {m_nStepResultID = value;}
		}
		private string m_strStepResultText;
		/// <summary>
		/// ����������
		/// </summary>
		public string strStepResultText
		{
			get {return m_strStepResultText;}
			set {m_strStepResultText = value;}
		}
		private DateTime? m_dtCreateTime;
		/// <summary>
		/// ��¼����ʱ��
		/// </summary>
		public DateTime? dtCreateTime
		{
			get {return m_dtCreateTime;}
			set {m_dtCreateTime = value;}
		}
		private DateTime? m_dtEventTime;
		/// <summary>
		/// �¼�����ʱ��
		/// </summary>
		public DateTime? dtEventTime
		{
			get {return m_dtEventTime;}
			set {m_dtEventTime = value;}
		}
        private DateTime? m_dtEventEndTime;
        /// <summary>
        /// �¼�jieshuʱ��
        /// </summary>
        public DateTime? dtEventEndTime
        {
            get { return m_dtEventEndTime; }
            set { m_dtEventEndTime = value; }
        }
	}
	/// <summary>
	///����: Plan_Beginwork_StepList
	///˵��: ���ڲ����б���
	/// </summary>
	public class Plan_Beginwork_StepList : List<Plan_Beginwork_Step>
	{
		public Plan_Beginwork_StepList()
		{}
	}
}
