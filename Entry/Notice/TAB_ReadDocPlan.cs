/**  版本信息模板在安装目录下，可自行修改。
* TAB_ReadDocPlan.cs
*
* 功 能： N/A
* 类 名： TAB_ReadDocPlan
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/15 10:42:19   N/A    初版
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
	/// TAB_ReadDocPlan:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_ReadDocPlan
	{
		public TAB_ReadDocPlan()
		{}
		#region Model
		private int _nid;
		private string _strtrainmanguid;
		private string _strfileguid;
		private int? _nreadcount;
		private DateTime? _dtfirstreadtime;
		private DateTime? _dtlastreadtime;
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
		public string StrTrainmanGUID
		{
			set{ _strtrainmanguid=value;}
			get{return _strtrainmanguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StrFileGUID
		{
			set{ _strfileguid=value;}
			get{return _strfileguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NReadCount
		{
			set{ _nreadcount=value;}
			get{return _nreadcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DtFirstReadTime
		{
			set{ _dtfirstreadtime=value;}
			get{return _dtfirstreadtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DtLastReadTime
		{
			set{ _dtlastreadtime=value;}
			get{return _dtlastreadtime;}
		}
		#endregion Model

	}
}

