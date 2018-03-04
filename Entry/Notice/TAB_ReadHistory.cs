/**  版本信息模板在安装目录下，可自行修改。
* TAB_ReadHistory.cs
*
* 功 能： N/A
* 类 名： TAB_ReadHistory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/20 9:47:08   N/A    初版
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
	/// TAB_ReadHistory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_ReadHistory
	{
		public TAB_ReadHistory()
		{}
		#region Model
		private int _nid;
		private string _strfileguid;
		private string _strtrainmanguid;
		private string _dtreadtime;
		private string _siteguid;
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
		public string strFileGUID
		{
			set{ _strfileguid=value;}
			get{return _strfileguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strTrainmanGUID
		{
			set{ _strtrainmanguid=value;}
			get{return _strtrainmanguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DtReadTime
		{
			set{ _dtreadtime=value;}
			get{return _dtreadtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SiteGUID
		{
			set{ _siteguid=value;}
			get{return _siteguid;}
		}
		#endregion Model

	}
}

