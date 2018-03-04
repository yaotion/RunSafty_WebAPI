/**  版本信息模板在安装目录下，可自行修改。
* Tab_DeliverJSPrint.cs
*
* 功 能： N/A
* 类 名： Tab_DeliverJSPrint
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/16 15:30:35   N/A    初版
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
	/// Tab_DeliverJSPrint:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Tab_DeliverJSPrint
	{
		public Tab_DeliverJSPrint()
		{}
		#region Model
		private int _nid;
		private string _strtrainmanguid;
		private string _strplanguid;
		private string _strsiteguid;
		private DateTime? _dtprinttime;
		/// <summary>
		/// 
		/// </summary>
		public int nID
		{
			set{ _nid=value;}
			get{return _nid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StrTrainmanGUID
		{
			set{ _strtrainmanguid=value;}
			get{return _strtrainmanguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StrPlanGUID
		{
			set{ _strplanguid=value;}
			get{return _strplanguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StrSiteGUID
		{
			set{ _strsiteguid=value;}
			get{return _strsiteguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtPrintTime
		{
			set{ _dtprinttime=value;}
			get{return _dtprinttime;}
		}
		#endregion Model

	}
}

