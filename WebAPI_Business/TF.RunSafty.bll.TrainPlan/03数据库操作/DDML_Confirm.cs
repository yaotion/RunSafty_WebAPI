using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.DDML
{
	/// <summary>
	///����: DDML_Confirm
	///˵��: ����ID
	/// </summary>
	public class DDML_Confirm
	{
		private int m_nID;
		/// <summary>
		/// ����ID
		/// </summary>
		public int nID
		{
			get {return m_nID;}
			set {m_nID = value;}
		}
		private string m_strGUID;
		/// <summary>
		/// 
		/// </summary>
		public string strGUID
		{
			get {return m_strGUID;}
			set {m_strGUID = value;}
		}
		private string m_strWorkShopGUID;
		/// <summary>
		/// ����GUID
		/// </summary>
		public string strWorkShopGUID
		{
			get {return m_strWorkShopGUID;}
			set {m_strWorkShopGUID = value;}
		}
		private string m_strWorkShopNumber;
		/// <summary>
		/// ������
		/// </summary>
		public string strWorkShopNumber
		{
			get {return m_strWorkShopNumber;}
			set {m_strWorkShopNumber = value;}
		}
		private string m_strWorkShopName;
		/// <summary>
		/// ��������
		/// </summary>
		public string strWorkShopName
		{
			get {return m_strWorkShopName;}
			set {m_strWorkShopName = value;}
		}
		private DateTime? m_dtConfirmTime;
		/// <summary>
		/// ��Ȩʱ��
		/// </summary>
		public DateTime? dtConfirmTime
		{
			get {return m_dtConfirmTime;}
			set {m_dtConfirmTime = value;}
		}
		private string m_strDutyUserNumber;
		/// <summary>
		/// ֵ��Ա����
		/// </summary>
		public string strDutyUserNumber
		{
			get {return m_strDutyUserNumber;}
			set {m_strDutyUserNumber = value;}
		}
		private string m_strDutyUserName;
		/// <summary>
		/// ֵ��Ա����
		/// </summary>
		public string strDutyUserName
		{
			get {return m_strDutyUserName;}
			set {m_strDutyUserName = value;}
		}
	}
	/// <summary>
	///����: DDML_ConfirmList
	///˵��: ����ID�б���
	/// </summary>
	public class DDML_ConfirmList : List<DDML_Confirm>
	{
		public DDML_ConfirmList()
		{}
	}
}
