/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_TrainJiaoluInSite.cs
*
* 功 能： N/A
* 类 名： TAB_Base_TrainJiaoluInSite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/25 10:33:25   N/A    初版
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
	/// 客户端管辖机车交路
	/// </summary>
	[Serializable]
	public partial class TAB_Base_TrainJiaoluInSite
	{
		public TAB_Base_TrainJiaoluInSite()
		{}
		#region Model
		private string _strsiteguid;
		private string _strtrainjiaoluguid;
		private string _strjiaoluinsiteguid= "newid";
		/// <summary>
		/// 客户端GUID
		/// </summary>
		public string strSiteGUID
		{
			set{ _strsiteguid=value;}
			get{return _strsiteguid;}
		}
		/// <summary>
		/// 机车交路GUID
		/// </summary>
		public string strTrainJiaoluGUID
		{
			set{ _strtrainjiaoluguid=value;}
			get{return _strtrainjiaoluguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strJiaoluInSiteGUID
		{
			set{ _strjiaoluinsiteguid=value;}
			get{return _strjiaoluinsiteguid;}
		}
		#endregion Model

	}
}

