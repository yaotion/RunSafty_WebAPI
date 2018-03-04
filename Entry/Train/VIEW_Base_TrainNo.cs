using System;
namespace TF.RunSafty.Model
{
    /// <summary>
    /// VIEW_Base_TrainNo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class VIEW_Base_TrainNo
    {
        public VIEW_Base_TrainNo()
        { }
        #region Model
        private string _strtrainjiaoluname;
        private string _strtrainmantypename;
        private string _strguid;
        private string _strtrainjiaoluguid;
        private string _strtrainno;
        private string _strtrainnumber;
        private DateTime? _dtstarttime;
        private string _strstartstation;
        private string _strendstation;
        private DateTime? _dtcreatetime;
        private string _strtraintypename;
        private int? _ntrainmantypeid;
        private string _strstartstationname;
        private int? _nkehuoid;
        private DateTime? _dtrealstarttime;
        private int? _nplantype;
        private int? _ndragtype;
        private int? _nremarktype;
        private string _strremark;
        private string _strcreatesiteguid;
        private string _strcreateuserguid;
        private string _strendstationname;
        private string _ndragtypename;
        private string _strplaceid;
        private string _strplacename;
        private string _strkehuoname;
        private string _strplantypename;
        private string _strremarktypename;
        private DateTime? _dtplanstarttime;

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

        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluName
        {
            set { _strtrainjiaoluname = value; }
            get { return _strtrainjiaoluname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanTypeName
        {
            set { _strtrainmantypename = value; }
            get { return _strtrainmantypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strGUID
        {
            set { _strguid = value; }
            get { return _strguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluGUID
        {
            set { _strtrainjiaoluguid = value; }
            get { return _strtrainjiaoluguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNo
        {
            set { _strtrainno = value; }
            get { return _strtrainno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainNumber
        {
            set { _strtrainnumber = value; }
            get { return _strtrainnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtStartTime
        {
            set { _dtstarttime = value; }
            get { return _dtstarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strStartStation
        {
            set { _strstartstation = value; }
            get { return _strstartstation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strEndStation
        {
            set { _strendstation = value; }
            get { return _strendstation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtCreateTime
        {
            set { _dtcreatetime = value; }
            get { return _dtcreatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainTypeName
        {
            set { _strtraintypename = value; }
            get { return _strtraintypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nTrainmanTypeID
        {
            set { _ntrainmantypeid = value; }
            get { return _ntrainmantypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strStartStationName
        {
            set { _strstartstationname = value; }
            get { return _strstartstationname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nKehuoID
        {
            set { _nkehuoid = value; }
            get { return _nkehuoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtRealStartTime
        {
            set { _dtrealstarttime = value; }
            get { return _dtrealstarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nPlanType
        {
            set { _nplantype = value; }
            get { return _nplantype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nDragType
        {
            set { _ndragtype = value; }
            get { return _ndragtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nRemarkType
        {
            set { _nremarktype = value; }
            get { return _nremarktype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strRemark
        {
            set { _strremark = value; }
            get { return _strremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCreateSiteGUID
        {
            set { _strcreatesiteguid = value; }
            get { return _strcreatesiteguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCreateUserGUID
        {
            set { _strcreateuserguid = value; }
            get { return _strcreateuserguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strEndStationName
        {
            set { _strendstationname = value; }
            get { return _strendstationname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nDragTypeName
        {
            set { _ndragtypename = value; }
            get { return _ndragtypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strPlaceID
        {
            set { _strplaceid = value; }
            get { return _strplaceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strPlaceName
        {
            set { _strplacename = value; }
            get { return _strplacename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strKehuoName
        {
            set { _strkehuoname = value; }
            get { return _strkehuoname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strPlanTypeName
        {
            set { _strplantypename = value; }
            get { return _strplantypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strRemarkTypeName
        {
            set { _strremarktypename = value; }
            get { return _strremarktypename; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtPlanStartTime
        {
            set { _dtplanstarttime = value; }
            get { return _dtplanstarttime; }
        }
        #endregion Model

    }
}

