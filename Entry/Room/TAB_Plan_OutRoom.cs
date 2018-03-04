/**  版本信息模板在安装目录下，可自行修改。
* TAB_Plan_OutRoom.cs
*
* 功 能： N/A
* 类 名： TAB_Plan_OutRoom
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-27 09:20:16   N/A    初版
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
	/// 离寓记录表
	/// </summary>
	[Serializable]
	public partial class TAB_Plan_OutRoom
	{
		public TAB_Plan_OutRoom()
		{}
		#region Model
		private string _stroutroomguid;
		private string _strtrainplanguid;
		private string _strtrainmanguid;
		private DateTime? _dtoutroomtime;
		private int? _noutroomverifyid;
		private string _strdutyuserguid;
		private string _strtrainmannumber;
		private DateTime? _dtcreatetime= DateTime.Now;
		private string _strinroomguid;
		private string _strsiteguid;
		/// <summary>
		/// 离寓记录GUID
		/// </summary>
		public string strOutRoomGUID
		{
			set{ _stroutroomguid=value;}
			get{return _stroutroomguid;}
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
		/// 离寓司机GUID
		/// </summary>
		public string strTrainmanGUID
		{
			set{ _strtrainmanguid=value;}
			get{return _strtrainmanguid;}
		}
		/// <summary>
		/// 离寓时间
		/// </summary>
		public DateTime? dtOutRoomTime
		{
			set{ _dtoutroomtime=value;}
			get{return _dtoutroomtime;}
		}
		/// <summary>
		/// 身份验证类型
		/// </summary>
		public int? nOutRoomVerifyID
		{
			set{ _noutroomverifyid=value;}
			get{return _noutroomverifyid;}
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
		public DateTime? dtCreateTime
		{
			set{ _dtcreatetime=value;}
			get{return _dtcreatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strInRoomGUID
		{
			set{ _strinroomguid=value;}
			get{return _strinroomguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strSiteGUID
		{
			set{ _strsiteguid=value;}
			get{return _strsiteguid;}
		}
		#endregion Model

	}
}

