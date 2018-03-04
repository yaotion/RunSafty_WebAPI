/**  版本信息模板在安装目录下，可自行修改。
* TAB_Plan_Train.cs
*
* 功 能： N/A
* 类 名： TAB_Plan_Train
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/29 9:25:00   N/A    初版
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
	/// TAB_Plan_Train:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_Plan_Train
	{
		public TAB_Plan_Train()
		{}
		#region Model
		private int _nid;
		private string _strtrainplanguid= "newid";
		private string _strtraintypename;
		private string _strtrainnumber;
		private string _strtrainno;
		private DateTime _dtstarttime;
		private DateTime? _dtrealstarttime;
		private string _strtrainjiaoluguid;
		private string _strstartstation;
		private string _strendstation;
		private int? _ntrainmantypeid;
		private int? _nplantype;
		private int? _ndragtype;
		private int? _nkehuoid;
		private int? _nremarktype;
		private string _strremark;
		private int _nplanstate;
		private DateTime? _dtcreatetime;
		private string _strcreatesiteguid;
		private string _strcreateuserguid;
		private DateTime? _dtfirststarttime;
		private DateTime? _dtchuqintime;
		private int? _nneedrest;
		private DateTime? _dtarrivetime;
		private DateTime? _dtcalltime;
		private string _strbak1;
		private DateTime? _dtlastarrivetime;
		private string _strmainplanguid;
		private string _strplaceid;
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
		public string strTrainPlanGUID
		{
			set{ _strtrainplanguid=value;}
			get{return _strtrainplanguid;}
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
		public string strTrainNumber
		{
			set{ _strtrainnumber=value;}
			get{return _strtrainnumber;}
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
		public DateTime dtStartTime
		{
			set{ _dtstarttime=value;}
			get{return _dtstarttime;}
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
		public string strTrainJiaoluGUID
		{
			set{ _strtrainjiaoluguid=value;}
			get{return _strtrainjiaoluguid;}
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
		public int? nTrainmanTypeID
		{
			set{ _ntrainmantypeid=value;}
			get{return _ntrainmantypeid;}
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
		public int? nKehuoID
		{
			set{ _nkehuoid=value;}
			get{return _nkehuoid;}
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
		public int nPlanState
		{
			set{ _nplanstate=value;}
			get{return _nplanstate;}
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
		public DateTime? dtFirstStartTime
		{
			set{ _dtfirststarttime=value;}
			get{return _dtfirststarttime;}
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
		public int? nNeedRest
		{
			set{ _nneedrest=value;}
			get{return _nneedrest;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtArriveTime
		{
			set{ _dtarrivetime=value;}
			get{return _dtarrivetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtCallTime
		{
			set{ _dtcalltime=value;}
			get{return _dtcalltime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strBak1
		{
			set{ _strbak1=value;}
			get{return _strbak1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? dtLastArriveTime
		{
			set{ _dtlastarrivetime=value;}
			get{return _dtlastarrivetime;}
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
        public string strTrackNumber = "";
        public string strWaiQinClientGUID = "";
        public string strWaiQinClientNumber = "";
        public string strWaiQinClientName = "";
		#endregion Model

	}
}

