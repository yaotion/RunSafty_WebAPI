/**  版本信息模板在安装目录下，可自行修改。
* VIEW_Plan_Train.cs
*
* 功 能： N/A
* 类 名： VIEW_Plan_Train
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-01-20 11:06:58   N/A    初版
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
	/// VIEW_Plan_Train:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class VIEW_Plan_Train
	{
		public VIEW_Plan_Train()
		{}
		#region Model
		private string _strtrainjiaoluname;
		private string _strtrainmantypename;
		private string _strtrainplanguid;
		private string _strtrainjiaoluguid;
		private string _strtrainno;
		private string _strtrainnumber;
		private DateTime? _dtstarttime;
		private DateTime? _dtchuqintime;
		private string _strstartstation;
		private string _strendstation;
		private DateTime? _dtcreatetime;
		private int? _nplanstate;
		private string _strtraintypename;
		private int? _ntrainmantypeid;
		private string _strstartstationname;
		private int _sendplan;
		private int? _nneedrest;
		private DateTime? _dtarrivetime;
		private DateTime? _dtcalltime;
		private int? _nkehuoid;
		private DateTime? _dtrealstarttime;
		private int? _nplantype;
		private int? _ndragtype;
		private int? _nremarktype;
		private string _strremark;
		private string _strcreatesiteguid;
		private string _strcreateuserguid;
		private string _strendstationname;
		private string _strcreateuserid;
		private string _strcreateusername;
		private string _strcreatesitename;
		private DateTime? _dtfirststarttime;
		private int _nid;
		private string _ndragtypename;
		private string _strkehuoname;
		private string _strbak1;
		private DateTime? _dtlastarrivetime;
		private string _strmainplanguid;
		private string _strworkshopguid;
		private string _strplaceid;
		private string _strplanstatename;
		private string _strplacename;
		private string _strremarktypename;
		private string _strplantypename;
		private DateTime? _dtbeginworktime;
		/// <summary>
		/// 
		/// </summary>
		public string strTrainJiaoluName
		{
			set{ _strtrainjiaoluname=value;}
			get{return _strtrainjiaoluname;}
		}
		 
		/// <summary>
		/// 
		/// </summary>
		public string strTrainPlanGUID
		{
			set{ _strtrainplanguid=value;}
			get{return _strtrainplanguid;}
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
		public string strTrainNo
		{
			set{ _strtrainno=value;}
			get{return _strtrainno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strTrainNumber
		{
			set{ _strtrainnumber=value;}
			get{return _strtrainnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtStartTime
		{
			set{ _dtstarttime=value;}
			get{return _dtstarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtChuQinTime
		{
			set{ _dtchuqintime=value;}
			get{return _dtchuqintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strStartStation
		{
			set{ _strstartstation=value;}
			get{return _strstartstation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strEndStation
		{
			set{ _strendstation=value;}
			get{return _strendstation;}
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
		public int? nPlanState
		{
			set{ _nplanstate=value;}
			get{return _nplanstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strTrainTypeName
		{
			set{ _strtraintypename=value;}
			get{return _strtraintypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? nTrainmanTypeID
		{
			set{ _ntrainmantypeid=value;}
			get{return _ntrainmantypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strStartStationName
		{
			set{ _strstartstationname=value;}
			get{return _strstartstationname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SendPlan
		{
			set{ _sendplan=value;}
			get{return _sendplan;}
		}
		 
		 
		 
		/// <summary>
		/// 
		/// </summary>
        public int? nKeHuoID
		{
			set{ _nkehuoid=value;}
			get{return _nkehuoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtRealStartTime
		{
			set{ _dtrealstarttime=value;}
			get{return _dtrealstarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? nPlanType
		{
			set{ _nplantype=value;}
			get{return _nplantype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? nDragType
		{
			set{ _ndragtype=value;}
			get{return _ndragtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? nRemarkType
		{
			set{ _nremarktype=value;}
			get{return _nremarktype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strRemark
		{
			set{ _strremark=value;}
			get{return _strremark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strCreateSiteGUID
		{
			set{ _strcreatesiteguid=value;}
			get{return _strcreatesiteguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strCreateUserGUID
		{
			set{ _strcreateuserguid=value;}
			get{return _strcreateuserguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strEndStationName
		{
			set{ _strendstationname=value;}
			get{return _strendstationname;}
		}
		 
		 
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtFirstStartTime
		{
			set{ _dtfirststarttime=value;}
			get{return _dtfirststarttime;}
		}
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
		public string strMainPlanGUID
		{
			set{ _strmainplanguid=value;}
			get{return _strmainplanguid;}
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
		 
		 
		#endregion Model

	}
}

