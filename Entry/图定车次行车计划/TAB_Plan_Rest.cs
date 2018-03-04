using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Model
{
    public partial class TAB_Plan_Rest
    {
        public TAB_Plan_Rest()
		{}
		#region Model
		private int _nid;
		private string _strguid;
		private string _strtrainjiaoluguid;
        private string _dtarrivetime;
        private string _dtcalltime;
		private string _strtrainno;
        private string _dtchuqintime;
		private string _strtrainnoguid;
		private string _strworkgrouguid;
		private string _strtrainmanguid1;
		private string _strtrainmanguid2;
		private string _strtrainmanguid3;
		private string _strtrainmanguid4;

        private int _nFinished;

        public int nFinished
        {
            get { return _nFinished; }
            set { _nFinished = value; }
        }


        private string _dtStartTrainTime;

        public string dtStartTrainTime
        {
            get { return _dtStartTrainTime; }
            set { _dtStartTrainTime = value; }
        }

        private int _ePlanState;

        public int ePlanState
        {
            get { return _ePlanState; }
            set { _ePlanState = value; }
        }


        private string _dtSignTime; 

        public string dtSignTime
        {
            get { return _dtSignTime; }
            set { _dtSignTime = value; }
        }

        private int _nNeedRest;

        public int nNeedRest
        {
            get { return _nNeedRest; }
            set { _nNeedRest = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int nID
		{
			set{ _nid=value;}
			get{return _nid;}
		}
		/// <summary>
		/// 待乘计划GUID
		/// </summary>
		public string strGUID
		{
			set{ _strguid=value;}
			get{return _strguid;}
		}
		/// <summary>
		/// 签点计划
		/// </summary>
		public string strTrainJiaoLuGUID
		{
			set{ _strtrainjiaoluguid=value;}
			get{return _strtrainjiaoluguid;}
		}
		/// <summary>
		/// 候班时间
		/// </summary>
        public string dtArriveTime
		{
			set{ _dtarrivetime=value;}
			get{return _dtarrivetime;}
		}
		/// <summary>
		/// 叫班时间
		/// </summary>
        public string dtCallTime
		{
			set{ _dtcalltime=value;}
			get{return _dtcalltime;}
		}
		/// <summary>
		/// 车次
		/// </summary>
		public string strTrainNo
		{
			set{ _strtrainno=value;}
			get{return _strtrainno;}
		}
		/// <summary>
		/// 计划开车时间
		/// </summary>
		public string dtChuQinTime
		{
			set{ _dtchuqintime=value;}
			get{return _dtchuqintime;}
		}
		/// <summary>
		/// 图钉车次GUID
		/// </summary>
		public string strTrainNoGUID
		{
			set{ _strtrainnoguid=value;}
			get{return _strtrainnoguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strWorkGrouGUID
		{
			set{ _strworkgrouguid=value;}
			get{return _strworkgrouguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strTrainmanGUID1
		{
			set{ _strtrainmanguid1=value;}
			get{return _strtrainmanguid1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strTrainmanGUID2
		{
			set{ _strtrainmanguid2=value;}
			get{return _strtrainmanguid2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strTrainmanGUID3
		{
			set{ _strtrainmanguid3=value;}
			get{return _strtrainmanguid3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strTrainmanGUID4
		{
			set{ _strtrainmanguid4=value;}
			get{return _strtrainmanguid4;}
		}

        private string _strTrainmanName1;

        public string strTrainmanName1
        {
            get { return _strTrainmanName1; }
            set { _strTrainmanName1 = value; }
        }
        private string _strTrainmanName2;

        public string strTrainmanName2
        {
            get { return _strTrainmanName2; }
            set { _strTrainmanName2 = value; }
        }
        private string _strTrainmanName3;

        public string strTrainmanName3
        {
            get { return _strTrainmanName3; }
            set { _strTrainmanName3 = value; }
        }
        private string _strTrainmanName4;

        public string strTrainmanName4
        {
            get { return _strTrainmanName4; }
            set { _strTrainmanName4 = value; }
        }


        private string _strTrainmanNumber4;

        public string strTrainmanNumber4
        {
            get { return _strTrainmanNumber4; }
            set { _strTrainmanNumber4 = value; }
        }
        private string _strTrainmanNumber3;

        public string strTrainmanNumber3
        {
            get { return _strTrainmanNumber3; }
            set { _strTrainmanNumber3 = value; }
        }
        private string _strTrainmanNumber2;

        public string strTrainmanNumber2
        {
            get { return _strTrainmanNumber2; }
            set { _strTrainmanNumber2 = value; }
        }
        private string _strTrainmanNumber1;

        public string strTrainmanNumber1
        {
            get { return _strTrainmanNumber1; }
            set { _strTrainmanNumber1 = value; }
        }


		#endregion Model

	}
}
