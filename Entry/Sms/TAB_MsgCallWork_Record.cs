/**  版本信息模板在安装目录下，可自行修改。
* TAB_MsgCallWork_Record.cs
*
* 功 能： N/A
* 类 名： TAB_MsgCallWork_Record
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-10-10 13:06:46   N/A    初版
*
* Copyright (c) 2014 thinkfreely Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：郑州畅想高科股份有限公司　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace TF.RunSafty.Model
{
	/// <summary>
	/// TAB_MsgCallWork_Record:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_MsgCallWork_Record
	{
		public TAB_MsgCallWork_Record()
		{}
		#region Model
		private int _nid;
		private string _strguid;
		private string _strmsgcontent;
		private string _strcallworkguid;
		private DateTime? _dttime;
		private int? _ntype;
		private int? _nresult;
		private string _strsenderphone;
		private string _strreceiverphone;
		/// <summary>
		/// 
		/// </summary>
		public int nId
		{
			set{ _nid=value;}
			get{return _nid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strGUID
		{
			set{ _strguid=value;}
			get{return _strguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strMsgContent
		{
			set{ _strmsgcontent=value;}
			get{return _strmsgcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strCallWorkGUID
		{
			set{ _strcallworkguid=value;}
			get{return _strcallworkguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtTime
		{
			set{ _dttime=value;}
			get{return _dttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? nType
		{
			set{ _ntype=value;}
			get{return _ntype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? nResult
		{
			set{ _nresult=value;}
			get{return _nresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strSenderPhone
		{
			set{ _strsenderphone=value;}
			get{return _strsenderphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strReceiverPhone
		{
			set{ _strreceiverphone=value;}
			get{return _strreceiverphone;}
		}
		#endregion Model

	}
}

