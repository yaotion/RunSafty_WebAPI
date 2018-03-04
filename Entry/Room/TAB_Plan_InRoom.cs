/**  版本信息模板在安装目录下，可自行修改。
* TAB_Plan_InRoom.cs
*
* 功 能： N/A
* 类 名： TAB_Plan_InRoom
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-27 09:20:12   N/A    初版
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
	/// 入寓记录表
	/// </summary>
	[Serializable]
	public partial class TAB_Plan_InRoom
	{
		public TAB_Plan_InRoom()
		{}
		#region Model
		private string _strinroomguid;
		private string _strtrainplanguid;
		private string _strtrainmanguid;
		private DateTime? _dtinroomtime;
		private int? _ninroomverifyid;
		private string _strdutyuserguid;
		private string _strtrainmannumber;
		private DateTime? _dtcreatettime= DateTime.Now;
		private string _strsiteguid;
		private string _strroomnumber;
		private int? _nbednumber;
		/// <summary>
		/// 入寓记录GUID
		/// </summary>
		public string strInRoomGUID
		{
			set{ _strinroomguid=value;}
			get{return _strinroomguid;}
		}
		/// <summary>
		/// 机车计划GUID
		/// </summary>
		public string strTrainPlanGUID
		{
			set{ _strtrainplanguid=value;}
			get{return _strtrainplanguid;}
		}
		/// <summary>
		/// 入寓司机GUID
		/// </summary>
		public string strTrainmanGUID
		{
			set{ _strtrainmanguid=value;}
			get{return _strtrainmanguid;}
		}
		/// <summary>
		/// 入寓时间
		/// </summary>
		public DateTime? dtInRoomTime
		{
			set{ _dtinroomtime=value;}
			get{return _dtinroomtime;}
		}
		/// <summary>
		/// 是否验证类型
		/// </summary>
		public int? nInRoomVerifyID
		{
			set{ _ninroomverifyid=value;}
			get{return _ninroomverifyid;}
		}
		/// <summary>
		/// 值班员GUID
		/// </summary>
		public string strDutyUserGUID
		{
			set{ _strdutyuserguid=value;}
			get{return _strdutyuserguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strTrainmanNumber
		{
			set{ _strtrainmannumber=value;}
			get{return _strtrainmannumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtCreatetTime
		{
			set{ _dtcreatettime=value;}
			get{return _dtcreatettime;}
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
		public string strRoomNumber
		{
			set{ _strroomnumber=value;}
			get{return _strroomnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? nBedNumber
		{
			set{ _nbednumber=value;}
			get{return _nbednumber;}
		}
		#endregion Model

	}
}

