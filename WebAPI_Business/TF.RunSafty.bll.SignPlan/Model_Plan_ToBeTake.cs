using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.SignPlan
{
   public class Model_Plan_ToBeTake
    {

       public Model_Plan_ToBeTake()
		{}
		#region Model
       private string _strTrainNo;

       public string StrTrainNo
       {
           get { return _strTrainNo; }
           set { _strTrainNo = value; }
       }
		private string _strplanguid;
		private string _strcheci;
        private string _dtcallworktime;
        private string _dtwaitworktime;
		private string _strtrainmanguid1;
		private string _strtrainmanguid2;
		private string _strtrainmanguid3;
		private string _strtrainmanguid4;



        private string _strtrainmanNumber1;

        public string strTrainmanNumber1
        {
            get { return _strtrainmanNumber1; }
            set { _strtrainmanNumber1 = value; }
        }
        private string _strtrainmanNumber2;

        public string strTrainmanNumber2
        {
            get { return _strtrainmanNumber2; }
            set { _strtrainmanNumber2 = value; }
        }
        private string _strtrainmanNumber3;

        public string strTrainmanNumber3
        {
            get { return _strtrainmanNumber3; }
            set { _strtrainmanNumber3 = value; }
        }
        private string _strtrainmanNumber4;

        public string strTrainmanNumber4
        {
            get { return _strtrainmanNumber4; }
            set { _strtrainmanNumber4 = value; }
        }



        private string _strtrainmanName1;

        public string strTrainmanName1
        {
            get { return _strtrainmanName1; }
            set { _strtrainmanName1 = value; }
        }
        private string _strtrainmanName2;

        public string strTrainmanName2
        {
            get { return _strtrainmanName2; }
            set { _strtrainmanName2 = value; }
        }
        private string _strtrainmanName3;

        public string strTrainmanName3
        {
            get { return _strtrainmanName3; }
            set { _strtrainmanName3 = value; }
        }
        private string _strtrainmanName4;

        public string strTrainmanName4
        {
            get { return _strtrainmanName4; }
            set { _strtrainmanName4 = value; }
        }





		/// <summary>
		/// 
		/// </summary>
		public string strPlanGUID
		{
			set{ _strplanguid=value;}
			get{return _strplanguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strCheCi
		{
			set{ _strcheci=value;}
			get{return _strcheci;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dtCallWorkTime
		{
			set{ _dtcallworktime=value;}
			get{return _dtcallworktime;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string dtWaitWorkTime
		{
			set{ _dtwaitworktime=value;}
			get{return _dtwaitworktime;}
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

        private string _nNeedRest;

        public string NNeedRest
        {
            get { return _nNeedRest; }
            set { _nNeedRest = value; }
        }

        private string _nPlanState;

        public string NPlanState
        {
            get { return _nPlanState; }
            set { _nPlanState = value; }
        }

		#endregion Model

	}
}