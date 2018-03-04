/**  版本信息模板在安装目录下，可自行修改。
* TAB_NoticeFile.cs
*
* 功 能： N/A
* 类 名： TAB_NoticeFile
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/12 15:03:25   N/A    初版
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
	/// TAB_NoticeFile:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_NoticeFile
	{
		public TAB_NoticeFile()
		{}
		#region Model
		private int _nid;
		private string _strfileguid;
		private string _strworkshopguid;
		private string _strtypeguid;
		private string _strfilename;
		private string _strfilepath;
		private string _dtbegintime;
		private string _dtendtime;
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
		public string StrTypeGUID
		{
			set{ _strtypeguid=value;}
			get{return _strtypeguid;}
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
		public string strFilePath
		{
			set{ _strfilepath=value;}
			get{return _strfilepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dtBeginTime
		{
			set{ _dtbegintime=value;}
			get{return _dtbegintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dtEndTime
		{
			set{ _dtendtime=value;}
			get{return _dtendtime;}
		}
		#endregion Model

	}
}

