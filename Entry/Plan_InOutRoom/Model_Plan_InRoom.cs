using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Model
{
    public partial class Model_Plan_InRoom
    {

		private string _strinroomguid;
		private string _strtrainplanguid;
		private string _strtrainmanguid;
		private DateTime? _dtinroomtime;
		private int? _ninroomverifyid;
		private string _strdutyuserguid;
		private string _strtrainmannumber;
		private DateTime? _dtcreatetime= DateTime.Now;
		private string _strsiteguid;
		private string _strroomnumber;
		private int? _nbednumber;
		private DateTime? _dtarrivetime;
		private string _strwaitplanguid;
		private int? _eplantype;
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
		public DateTime? dtCreateTime
		{
			set{ _dtcreatetime=value;}
			get{return _dtcreatetime;}
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
