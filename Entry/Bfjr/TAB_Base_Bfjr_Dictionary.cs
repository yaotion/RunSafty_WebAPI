/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_Bfjr_Dictionary.cs
*
* 功 能： N/A
* 类 名： TAB_Base_Bfjr_Dictionary
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-10-18 09:22:02   N/A    初版
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
	/// TAB_Base_Bfjr_Dictionary:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_Base_Bfjr_Dictionary
	{
		public TAB_Base_Bfjr_Dictionary()
		{}
		#region Model
		private string _unitid;
		private string _strjiwuduan;
		/// <summary>
		/// 
		/// </summary>
		public string unitid
		{
			set{ _unitid=value;}
			get{return _unitid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strJiWuDuan
		{
			set{ _strjiwuduan=value;}
			get{return _strjiwuduan;}
		}
		#endregion Model

	}
}

