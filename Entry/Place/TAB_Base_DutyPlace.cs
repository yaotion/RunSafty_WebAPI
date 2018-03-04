/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_DutyPlace.cs
*
* 功 能： N/A
* 类 名： TAB_Base_DutyPlace
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/17 9:42:49   N/A    初版
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
	/// TAB_Base_DutyPlace:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_Base_DutyPlace
	{
		public TAB_Base_DutyPlace()
		{}
		#region Model
		private int _nid;
		private string _strworkshopguid;
		private string _strplaceid;
		private string _strplacename;
		private DateTime? _dtcreatetime;
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
		public string strWorkShopGUID
		{
			set{ _strworkshopguid=value;}
			get{return _strworkshopguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strPlaceID
		{
			set{ _strplaceid=value;}
			get{return _strplaceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strPlaceName
		{
			set{ _strplacename=value;}
			get{return _strplacename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtCreateTime
		{
			set{ _dtcreatetime=value;}
			get{return _dtcreatetime;}
		}
		#endregion Model

	}
}

