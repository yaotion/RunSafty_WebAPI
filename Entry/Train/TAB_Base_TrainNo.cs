/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_TrainNo.cs
*
* 功 能： N/A
* 类 名： TAB_Base_TrainNo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/9/18 9:17:47   N/A    初版
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

    
    //private class JsonModel
    //{
    //    public int result;
    //    public string resultStr;
    //    public object data;
    //}

	/// <summary>
	/// TAB_Base_TrainNo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_Base_TrainNo
	{
		public TAB_Base_TrainNo()
		{}
		#region Model
		private string _strguid;
		private string _strtraintypename;
		private string _strtrainnumber;
		private string _strtrainno;
		private DateTime? _dtstarttime;
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
		private DateTime? _dtcreatetime;
		private string _strcreatesiteguid;
		private string _strcreateuserguid;
		private string _strplaceid;
        private DateTime? _dtPlanStartTime;
	    private string _strWorkDay;
	    private int? _nNeedRest;
	    private DateTime? _dtArriveTime;
	    private DateTime? _dtCallTime;

	    public string strWorkDay
	    {
	        get { return _strWorkDay; }
            set { _strWorkDay = value; }
	    }
	    public DateTime? dtCallTime
	    {
	        get { return _dtCallTime; }
            set { _dtCallTime = value; }
	    }
	    public DateTime? dtArriveTime
	    {
	        get { return _dtArriveTime; }
            set { _dtArriveTime = value; }
	    }
	    public int? nNeedRest
	    {
	        get { return _nNeedRest; }
            set { _nNeedRest = value; }
	    }
        public DateTime? dtPlanStartTime
        {
            get { return _dtPlanStartTime; }
            set { _dtPlanStartTime = value; }
        }

        
		/// <summary>
		/// 
		/// </summary>
		public string strGUID
		{
			set{ _strguid=value;}
			get{return _strguid;}
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
		public DateTime? dtStartTime
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
		public string strPlaceID
		{
			set{ _strplaceid=value;}
			get{return _strplaceid;}
		}
		#endregion Model

	}
}

