using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Model
{
    public partial class TrainJiaoLu
    {
        private string _strTrainJiaoluGUID;

        public string strTrainJiaoluGUID
        {
            get { return _strTrainJiaoluGUID; }
            set { _strTrainJiaoluGUID = value; }
        }
        private string _strTrainJiaoluName;

        public string strTrainJiaoluName
        {
            get { return _strTrainJiaoluName; }
            set { _strTrainJiaoluName = value; }
        }
        private string _strStartStation;

        public string strStartStation
        {
            get { return _strStartStation; }
            set { _strStartStation = value; }
        }
        private string _strEndStation;

        public string strEndStation
        {
            get { return _strEndStation; }
            set { _strEndStation = value; }
        }
        private string _strWorkShopGUID;

        public string strWorkShopGUID
        {
            get { return _strWorkShopGUID; }
            set { _strWorkShopGUID = value; }
        }
        private string _bIsBeginWorkFP;

        public string bIsBeginWorkFP
        {
            get { return _bIsBeginWorkFP; }
            set { _bIsBeginWorkFP = value; }
        }
        private string _bIsDir;

        public string bIsDir
        {
            get { return _bIsDir; }
            set { _bIsDir = value; }
        }
        private string _nWorkTimeTypeID;

        public string nWorkTimeTypeID
        {
            get { return _nWorkTimeTypeID; }
            set { _nWorkTimeTypeID = value; }
        }


    }
}
