using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.Plan
{
	/// <summary>
	///类名: Plan_Beginwork_Step
	///说明: 出勤步骤
	/// </summary>
	public class Plan_Beginwork_Step
	{

        private int m_nID;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }

		private string m_strTrainPlanGUID;
		/// <summary>
		/// 计划guid
		/// </summary>
		public string strTrainPlanGUID
		{
			get {return m_strTrainPlanGUID;}
			set {m_strTrainPlanGUID = value;}
		}


		private int m_nStepID;
		/// <summary>
		/// 步骤编号
		/// </summary>
		public int nStepID
		{
			get {return m_nStepID;}
			set {m_nStepID = value;}
		}
		private string m_strTrainmanGUID;
		/// <summary>
		/// 人员guid
		/// </summary>
		public string strTrainmanGUID
		{
			get {return m_strTrainmanGUID;}
			set {m_strTrainmanGUID = value;}
		}
		private string m_strTrainmanNumber;
		/// <summary>
		/// 人员工号
		/// </summary>
		public string strTrainmanNumber
		{
			get {return m_strTrainmanNumber;}
			set {m_strTrainmanNumber = value;}
		}
		private string m_strTrainmanName;
		/// <summary>
		/// 人员姓名
		/// </summary>
		public string strTrainmanName
		{
			get {return m_strTrainmanName;}
			set {m_strTrainmanName = value;}
		}
		private int m_nStepResultID;
		/// <summary>
		/// 步骤执行结果
		/// </summary>
		public int nStepResultID
		{
			get {return m_nStepResultID;}
			set {m_nStepResultID = value;}
		}
		private string m_strStepResultText;
		/// <summary>
		/// 步骤结果描述
		/// </summary>
		public string strStepResultText
		{
			get {return m_strStepResultText;}
			set {m_strStepResultText = value;}
		}
		private DateTime? m_dtCreateTime;
		/// <summary>
		/// 记录创建时间
		/// </summary>
		public DateTime? dtCreateTime
		{
			get {return m_dtCreateTime;}
			set {m_dtCreateTime = value;}
		}
		private DateTime? m_dtEventTime;
		/// <summary>
		/// 事件发生时间
		/// </summary>
		public DateTime? dtEventTime
		{
			get {return m_dtEventTime;}
			set {m_dtEventTime = value;}
		}
        private DateTime? m_dtEventEndTime;
        /// <summary>
        /// 事件jieshu时间
        /// </summary>
        public DateTime? dtEventEndTime
        {
            get { return m_dtEventEndTime; }
            set { m_dtEventEndTime = value; }
        }
	}
	/// <summary>
	///类名: Plan_Beginwork_StepList
	///说明: 出勤步骤列表类
	/// </summary>
	public class Plan_Beginwork_StepList : List<Plan_Beginwork_Step>
	{
		public Plan_Beginwork_StepList()
		{}
	}
}
