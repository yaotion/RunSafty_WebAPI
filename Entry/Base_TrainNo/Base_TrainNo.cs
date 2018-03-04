using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.Entry
{
	/// <summary>
	//类名: Base_TrainNo
	//说明: 图定车次信息
	/// <summary>
	public class Base_TrainNo
	{
		private string m_strGUID;
		/// <summary>
		/// 
		/// <summary>
		public string strGUID
		{
			get {return m_strGUID;}
			set {m_strGUID = value;}
		}
		private string m_strTrainTypeName;
		/// <summary>
		/// 机车类型
		/// <summary>
		public string strTrainTypeName
		{
			get {return m_strTrainTypeName;}
			set {m_strTrainTypeName = value;}
		}
		private string m_strTrainNumber;
		/// <summary>
		/// 车号
		/// <summary>
		public string strTrainNumber
		{
			get {return m_strTrainNumber;}
			set {m_strTrainNumber = value;}
		}
		private string m_strTrainNo;
		/// <summary>
		/// 车次 
		/// <summary>
		public string strTrainNo
		{
			get {return m_strTrainNo;}
			set {m_strTrainNo = value;}
		}
		private DateTime? m_dtStartTime;
		/// <summary>
		/// 计划开车时间
		/// <summary>
		public DateTime? dtStartTime
		{
			get {return m_dtStartTime;}
			set {m_dtStartTime = value;}
		}
		private DateTime? m_dtRealStartTime;
		/// <summary>
		/// 实际开车时间
		/// <summary>
		public DateTime? dtRealStartTime
		{
			get {return m_dtRealStartTime;}
			set {m_dtRealStartTime = value;}
		}
		private string m_strTrainJiaoluGUID;
		/// <summary>
		/// 行车区段GUID
		/// <summary>
		public string strTrainJiaoluGUID
		{
			get {return m_strTrainJiaoluGUID;}
			set {m_strTrainJiaoluGUID = value;}
		}
		private string m_strStartStation;
		/// <summary>
		/// 开始站
		/// <summary>
		public string strStartStation
		{
			get {return m_strStartStation;}
			set {m_strStartStation = value;}
		}
		private string m_strEndStation;
		/// <summary>
		/// 结束站
		/// <summary>
		public string strEndStation
		{
			get {return m_strEndStation;}
			set {m_strEndStation = value;}
		}
		private int m_nTrainmanTypeID;
		/// <summary>
		/// 乘务员类型ID
		/// <summary>
		public int nTrainmanTypeID
		{
			get {return m_nTrainmanTypeID;}
			set {m_nTrainmanTypeID = value;}
		}
		private int m_nPlanType;
		/// <summary>
		/// 计划类型ID
		/// <summary>
		public int nPlanType
		{
			get {return m_nPlanType;}
			set {m_nPlanType = value;}
		}
		private int m_nDragType;
		/// <summary>
		/// 牵引类型ID
		/// <summary>
		public int nDragType
		{
			get {return m_nDragType;}
			set {m_nDragType = value;}
		}
		private int m_nKehuoID;
		/// <summary>
		/// 客货类型ID
		/// <summary>
		public int nKehuoID
		{
			get {return m_nKehuoID;}
			set {m_nKehuoID = value;}
		}
		private int m_nRemarkType;
		/// <summary>
		/// 备注类型
		/// <summary>
		public int nRemarkType
		{
			get {return m_nRemarkType;}
			set {m_nRemarkType = value;}
		}
		private string m_strRemark;
		/// <summary>
		/// 备注
		/// <summary>
		public string strRemark
		{
			get {return m_strRemark;}
			set {m_strRemark = value;}
		}
		private DateTime? m_dtCreateTime;
		/// <summary>
		/// 创建时间
		/// <summary>
		public DateTime? dtCreateTime
		{
			get {return m_dtCreateTime;}
			set {m_dtCreateTime = value;}
		}
		private string m_strCreateSiteGUID;
		/// <summary>
		/// 创建地点ID
		/// <summary>
		public string strCreateSiteGUID
		{
			get {return m_strCreateSiteGUID;}
			set {m_strCreateSiteGUID = value;}
		}
		private string m_strCreateUserGUID;
		/// <summary>
		/// 创建人ID
		/// <summary>
		public string strCreateUserGUID
		{
			get {return m_strCreateUserGUID;}
			set {m_strCreateUserGUID = value;}
		}
		private string m_strPlaceID;
		/// <summary>
		/// 出勤点编号
		/// <summary>
		public string strPlaceID
		{
			get {return m_strPlaceID;}
			set {m_strPlaceID = value;}
		}
		private DateTime? m_dtPlanStartTime;
		/// <summary>
		/// 创建时间
		/// <summary>
		public DateTime? dtPlanStartTime
		{
			get {return m_dtPlanStartTime;}
			set {m_dtPlanStartTime = value;}
		}
	}
}
