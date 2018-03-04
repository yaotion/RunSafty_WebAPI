/**  版本信息模板在安装目录下，可自行修改。
* VIEW_Base_Site_DutyPlace.cs
*
* 功 能： N/A
* 类 名： VIEW_Base_Site_DutyPlace
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/17 11:20:50   N/A    初版
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
	/// VIEW_Base_Site_DutyPlace:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class VIEW_Base_Site_DutyPlace
	{
		public VIEW_Base_Site_DutyPlace()
		{}
		#region Model
		private int _nid;
		private string _strtrainjiaoluguid;
		private string _strplaceid;
		private string _strsiteguid;
		private int? _nplaceindex;
		private string _strplacename;
		private string _strworkshopguid;
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
		public string strTrainJiaoluGUID
		{
			set{ _strtrainjiaoluguid=value;}
			get{return _strtrainjiaoluguid;}
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
		public string strSiteGUID
		{
			set{ _strsiteguid=value;}
			get{return _strsiteguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? nPlaceIndex
		{
			set{ _nplaceindex=value;}
			get{return _nplaceindex;}
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
		public string strWorkShopGUID
		{
			set{ _strworkshopguid=value;}
			get{return _strworkshopguid;}
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

