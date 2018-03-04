using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Model
{
    public partial class Model_Plan_InOutRoom
    {
        private string _strInRoomGUID;

        public string StrInRoomGUID
        {
            get { return _strInRoomGUID; }
            set { _strInRoomGUID = value; }
        }


        private string _strPlanGUID;

        public string StrPlanGUID
        {
            get { return _strPlanGUID; }
            set { _strPlanGUID = value; }
        }
        private string _strTrainmanGUID;

        public string StrTrainmanGUID
        {
            get { return _strTrainmanGUID; }
            set { _strTrainmanGUID = value; }
        }
        private string _dtInRoomTime;

        public string DtInRoomTime
        {
            get { return _dtInRoomTime; }
            set { _dtInRoomTime = value; }
        }

        private string _inOutRoomType;

        public string InOutRoomType
        {
            get { return _inOutRoomType; }
            set { _inOutRoomType = value; }
        }


        private string _dtArriveTime;

        public string dtArriveTime
        {
            get { return _dtArriveTime; }
            set { _dtArriveTime = value; }
        }


        private string _strWaitPlanGUID;

        public string strWaitPlanGUID
        {
            get { return _strWaitPlanGUID; }
            set { _strWaitPlanGUID = value; }
        }


        private int _ePlanType;

        public int ePlanType
        {
            get { return _ePlanType; }
            set { _ePlanType = value; }
        }
             





    }
}
