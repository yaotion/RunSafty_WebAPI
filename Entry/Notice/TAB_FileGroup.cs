/**  版本信息模板在安装目录下，可自行修改。
* TAB_FileGroup.cs
*
* 功 能： N/A
* 类 名： TAB_FileGroup
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/15 13:13:34   N/A    初版
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
	/// TAB_FileGroup:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_FileGroup
	{
		public TAB_FileGroup()
		{}
		#region Model
		private int _nid;
		private string _strtypeguid;
		private string _strtypename;
		private string _strtype;
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
		public string strTypeGUID
		{
			set{ _strtypeguid=value;}
			get{return _strtypeguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strTypeName
		{
			set{ _strtypename=value;}
			get{return _strtypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strType
		{
			set{ _strtype=value;}
			get{return _strtype;}
		}
		#endregion Model

	}
}

