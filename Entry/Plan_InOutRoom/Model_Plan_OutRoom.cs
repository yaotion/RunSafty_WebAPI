using System;
namespace TF.RunSafty.Model
{
	
	public partial class Model_Plan_OutRoom
	{
		
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
		private DateTime? _dtarrivetime;
		private string _strwaitplanguid;
		private int? _eplantype;
        private string _strRoomNumber;

        public string strRoomNumber
        {
            get { return _strRoomNumber; }
            set { _strRoomNumber = value; }
        }
        private int? _nBedNumber;

        public int? nBedNumber
        {
            get { return _nBedNumber; }
            set { _nBedNumber = value; }
        }


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
		public string strWaitPlanGUID
		{
			set{ _strwaitplanguid=value;}
			get{return _strwaitplanguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ePlanType
		{
			set{ _eplantype=value;}
			get{return _eplantype;}
		}

	}
}

