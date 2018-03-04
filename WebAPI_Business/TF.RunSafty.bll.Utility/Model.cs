using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;

namespace TF.Runsafty.Utility
{
    public class TRsDutyUser
    {
        public string strDutyGUID = string.Empty;
        public string strDutyNumber = string.Empty;
        public string strDutyName = string.Empty;
        public string strPassword = string.Empty;
        public string strTokenID = string.Empty;
    }

    public class TRsSiteInfo
    {
        public string strSiteGUID = string.Empty;
        public string strSiteNumber = string.Empty;
        public string strSiteName = string.Empty;
        public string strAreaGUID = string.Empty;
        public int nSiteEnable;
        public string strSiteIP = string.Empty;
        public int nSiteJob;
        public string strStationGUID = string.Empty;
        public string WorkShopGUID = string.Empty;
        public string  Tmis=string.Empty;

        public List<sTrainJiaolu> TrainJiaolus = new List<sTrainJiaolu>();
        //岗位权限列表
        public List<RRsJobLimit> JobLimits = new List<RRsJobLimit>(); 
    }

    public class sTrainJiaolu
    {
        public string strTrainJiaoluGUID;
    }
    //岗位权限
    public class RRsJobLimit
    {
        public TRsSiteJob Job;
        public TRsJobLimit Limimt;
    }

    public class TrainNoBelong
    {
        public string strTrainNoHead = string.Empty;
        public int nBeginNumber ;
        public int nEndNumber  ;
        public string nKehuoID = string.Empty;
        public string strKehuoName = string.Empty;
    }

}
