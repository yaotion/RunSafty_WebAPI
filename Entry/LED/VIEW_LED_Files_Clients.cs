/**  版本信息模板在安装目录下，可自行修改。
* VIEW_LED_Files_Clients.cs
*
* 功 能： N/A
* 类 名： VIEW_LED_Files_Clients
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-11-20 10:52:50   N/A    初版
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
	/// VIEW_LED_Files_Clients:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class VIEW_LED_Files_Clients
	{
		public VIEW_LED_Files_Clients()
		{}
		#region Model
		private int _nid;
		private string _strfileguid;
		private string _strworkshopguid;
		private DateTime? _dtupdate;
		private string _strfilepathname;
		private string _strfilename;
		private string _clientid;
		/// <summary>
		/// 
		/// </summary>
		public int nid
		{
			set{ _nid=value;}
			get{return _nid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strFileGUID
		{
			set{ _strfileguid=value;}
			get{return _strfileguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strWorkShopGUID
		{
			set{ _strworkshopguid=value;}
			get{return _strworkshopguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DtUpdate
		{
			set{ _dtupdate=value;}
			get{return _dtupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strFilePathName
		{
			set{ _strfilepathname=value;}
			get{return _strfilepathname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strFileName
		{
			set{ _strfilename=value;}
			get{return _strfilename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string clientid
		{
			set{ _clientid=value;}
			get{return _clientid;}
		}
		#endregion Model

	}
}

