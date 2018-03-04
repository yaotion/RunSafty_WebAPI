using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkSteps
{
   public class MDPlanBase
    {
        #region Model

        private int _cqcj1;

        public int cqcj1
        {
            get { return _cqcj1; }
            set { _cqcj1 = value; }
        }
        private int _cqcj2;

        public int cqcj2
        {
            get { return _cqcj2; }
            set { _cqcj2 = value; }
        }
        private int _cqcj3;

        public int cqcj3
        {
            get { return _cqcj3; }
            set { _cqcj3 = value; }
        }

        private DateTime? _cqtime1;

        public DateTime? cqtime1
        {
            get { return _cqtime1; }
            set { _cqtime1 = value; }
        }


        private DateTime? _cqtime2;

        public DateTime? cqtime2
        {
            get { return _cqtime2; }
            set { _cqtime2 = value; }
        }


        private DateTime? _cqtime3;

        public DateTime? cqtime3
        {
            get { return _cqtime3; }
            set { _cqtime3 = value; }
        }



        private string _picture1;

        public string picture1
        {
            get { return _picture1; }
            set { _picture1 = value; }
        }
        private string _picture2;

        public string picture2
        {
            get { return _picture2; }
            set { _picture2 = value; }
        }
        private string _picture3;

        public string picture3
        {
            get { return _picture3; }
            set { _picture3 = value; }
        }

        private int _nVerifyID1;

        public int nVerifyID1
        {
            get { return _nVerifyID1; }
            set { _nVerifyID1 = value; }
        }
        private int _nVerifyID2;

        public int nVerifyID2
        {
            get { return _nVerifyID2; }
            set { _nVerifyID2 = value; }
        }
        private int _nVerifyID3;

        public int nVerifyID3
        {
            get { return _nVerifyID3; }
            set { _nVerifyID3 = value; }
        }

        private int _LateTimeDiff1;

        public int lateTimeDiff1
        {
            get { return _LateTimeDiff1; }
            set { _LateTimeDiff1 = value; }
        }
        private int _LateTimeDiff2;

        public int lateTimeDiff2
        {
            get { return _LateTimeDiff2; }
            set { _LateTimeDiff2 = value; }
        }
        private int _LateTimeDiff3;

        public int lateTimeDiff3
        {
            get { return _LateTimeDiff3; }
            set { _LateTimeDiff3 = value; }
        }



        private int _nTestCardResult1;

        public int nTestCardResult1
        {
            get { return _nTestCardResult1; }
            set { _nTestCardResult1 = value; }
        }
        private int _nTestCardResult2;

        public int nTestCardResult2
        {
            get { return _nTestCardResult2; }
            set { _nTestCardResult2 = value; }
        }
        private int _nTestCardResult3;

        public int nTestCardResult3
        {
            get { return _nTestCardResult3; }
            set { _nTestCardResult3 = value; }
        }


        private string _strTestCardResult1;

        public string strTestCardResult1
        {
            get { return _strTestCardResult1; }
            set { _strTestCardResult1 = value; }
        }
        private string _strTestCardResult2;

        public string strTestCardResult2
        {
            get { return _strTestCardResult2; }
            set { _strTestCardResult2 = value; }
        }
        private string _strTestCardResult3;

        public string strTestCardResult3
        {
            get { return _strTestCardResult3; }
            set { _strTestCardResult3 = value; }
        }

        private string _strtrainjiaoluname;
        private string _strtrainmantypename;
        private string _strtrainplanguid;
        private string _strtrainjiaoluguid;
        private string _strtrainno;
        private string _strtrainnumber;
        private DateTime? _dtstarttime;
        private DateTime? _dtchuqintime;
        private string _strstartstation;
        private string _strendstation;
        private DateTime? _dtcreatetime;
        private int? _nplanstate;
        private string _strtraintypename;
        private string _strstartstationname;
        private string _strendstationname;
        private int? _ntrainmantypeid;
        private int _sendplan;
        private int? _nneedrest;
        private DateTime? _dtarrivetime;
        private DateTime? _dtcalltime;
        private int? _nkehuoid;
        private DateTime? _dtrealstarttime;
        private int? _nplantype;
        private int? _ndragtype;
        private int? _nremarktype;
        private string _strwaiqinclientguid;
        private string _strwaiqinclientnumber;
        private string _strwaiqinclientname;
        private string _strremark;

        private string _strcreatesiteguid;
        private string _strcreateuserguid;
        private string _strcreateuserid;
        private string _strcreateusername;
        private string _strcreatesitename;
        private string _strplacename;
        private string _strtrainmannumber1;
        private string _strtrainmanname1;
        private int? _npostid1;
        private string _strworkshopguid1;
        private string _strtelnumber1;
        private DateTime? _dtlastendworktime1;
        private string _strtrainmanguid1;
        private string _strtrainmanguid2;
        private string _strtrainmannumber2;
        private string _strtrainmanname2;
        private string _strworkshopguid2;
        private string _strtelnumber2;
        private string _strtrainmannumber3;
        private string _strtrainmanguid3;
        private DateTime? _dtlastendworktime2;
        private int? _npostid2;
        private string _strtrainmanname3;
        private int? _npostid3;
        private string _strworkshopguid3;
        private string _strtelnumber3;
        private DateTime? _dtlastendworktime3;
        private string _strdutyguid;
        private string _strgroupguid;
        private string _strdutysiteguid;
        private DateTime? _dttrainmancreatetime;
        private DateTime? _dtfirststarttime;
        private int _nid;
        private string _ndragtypename;
        private string _strkehuoname;
        private int? _ntrainmanstate1;
        private int? _ntrainmanstate2;
        private int? _ntrainmanstate3;
        private string _strbak1;
        private DateTime? _dtlastarrivetime;
        private string _strmainplanguid;
        private string _strtrainmanname4;
        private string _strtrainmannumber4;
        private int? _npostid4;
        private string _strtelnumber4;
        private DateTime? _dtlastendworktime4;
        private int? _ntrainmanstate;
        private string _strtrainmanguid4;
        private string _strremarktypename;
        private string _strplantypename;
        private string _strplaceid;
        private string _strstatename;
        private int? _ndrivertype1;
        private int? _ndrivertype2;
        private int? _ndrivertype3;
        private int? _ndrivertype4;
        private string _strabcd1;
        private string _strabcd2;
        private string _strabcd3;
        private string _strabcd4;
        private int? _iskey1;
        private int? _iskey2;
        private int? _iskey3;
        private int? _iskey4;
        private string _strplanstatename;
        private DateTime? _dtSendTime;
        private DateTime? _dtRecvTime;
        public int? nMsgState1;
        public string strMsgID1;

        public int? nMsgState2;
        public string strMsgID2;

        public int? nMsgState3;
        public string strMsgID3;

        public int? nMsgState4;
        public string strMsgID4;

        public string strMobileNumber1;
        public string strMobileNumber2;
        public string strMobileNumber3;
        public string strMobileNumber4;

        public DateTime? _dtBeginWorkTime;

        public DateTime? dtBeginWorkTime
        {
            get { return _dtBeginWorkTime; }
            set { _dtBeginWorkTime = value; }
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
        public string strTrainPlanGUID
        {
            set { _strtrainplanguid = value; }
            get { return _strtrainplanguid; }
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
        public DateTime? dtChuQinTime
        {
            set { _dtchuqintime = value; }
            get { return _dtchuqintime; }
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
        public int? nPlanState
        {
            set { _nplanstate = value; }
            get { return _nplanstate; }
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
        public string strStartStationName
        {
            set { _strstartstationname = value; }
            get { return _strstartstationname; }
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
        public int? nTrainmanTypeID
        {
            set { _ntrainmantypeid = value; }
            get { return _ntrainmantypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SendPlan
        {
            set { _sendplan = value; }
            get { return _sendplan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nNeedRest
        {
            set { _nneedrest = value; }
            get { return _nneedrest; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtArriveTime
        {
            set { _dtarrivetime = value; }
            get { return _dtarrivetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtCallTime
        {
            set { _dtcalltime = value; }
            get { return _dtcalltime; }
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

        public string strWaiQinClientGUID
        {
            set { _strwaiqinclientguid = value; }
            get { return _strwaiqinclientguid; }
        }
        public string strWaiQinClientNumber
        {
            get { return _strwaiqinclientnumber; }
            set { _strwaiqinclientnumber = value; }
        }
        public string strWaiQinClientName
        {
            get { return _strwaiqinclientname; }
            set { _strwaiqinclientname = value; }
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
        public string strCreateUserID
        {
            set { _strcreateuserid = value; }
            get { return _strcreateuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCreateUserName
        {
            set { _strcreateusername = value; }
            get { return _strcreateusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCreateSiteName
        {
            set { _strcreatesitename = value; }
            get { return _strcreatesitename; }
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
        public string strTrainmanNumber1
        {
            set { _strtrainmannumber1 = value; }
            get { return _strtrainmannumber1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName1
        {
            set { _strtrainmanname1 = value; }
            get { return _strtrainmanname1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nPostID1
        {
            set { _npostid1 = value; }
            get { return _npostid1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID1
        {
            set { _strworkshopguid1 = value; }
            get { return _strworkshopguid1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTelNumber1
        {
            set { _strtelnumber1 = value; }
            get { return _strtelnumber1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastEndWorkTime1
        {
            set { _dtlastendworktime1 = value; }
            get { return _dtlastendworktime1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID1
        {
            set { _strtrainmanguid1 = value; }
            get { return _strtrainmanguid1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID2
        {
            set { _strtrainmanguid2 = value; }
            get { return _strtrainmanguid2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber2
        {
            set { _strtrainmannumber2 = value; }
            get { return _strtrainmannumber2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName2
        {
            set { _strtrainmanname2 = value; }
            get { return _strtrainmanname2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID2
        {
            set { _strworkshopguid2 = value; }
            get { return _strworkshopguid2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTelNumber2
        {
            set { _strtelnumber2 = value; }
            get { return _strtelnumber2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber3
        {
            set { _strtrainmannumber3 = value; }
            get { return _strtrainmannumber3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID3
        {
            set { _strtrainmanguid3 = value; }
            get { return _strtrainmanguid3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastEndWorkTime2
        {
            set { _dtlastendworktime2 = value; }
            get { return _dtlastendworktime2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nPostID2
        {
            set { _npostid2 = value; }
            get { return _npostid2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName3
        {
            set { _strtrainmanname3 = value; }
            get { return _strtrainmanname3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nPostID3
        {
            set { _npostid3 = value; }
            get { return _npostid3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID3
        {
            set { _strworkshopguid3 = value; }
            get { return _strworkshopguid3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTelNumber3
        {
            set { _strtelnumber3 = value; }
            get { return _strtelnumber3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastEndWorkTime3
        {
            set { _dtlastendworktime3 = value; }
            get { return _dtlastendworktime3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strDutyGUID
        {
            set { _strdutyguid = value; }
            get { return _strdutyguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strGroupGUID
        {
            set { _strgroupguid = value; }
            get { return _strgroupguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strDutySiteGUID
        {
            set { _strdutysiteguid = value; }
            get { return _strdutysiteguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtTrainmanCreateTime
        {
            set { _dttrainmancreatetime = value; }
            get { return _dttrainmancreatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtFirstStartTime
        {
            set { _dtfirststarttime = value; }
            get { return _dtfirststarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int nid
        {
            set { _nid = value; }
            get { return _nid; }
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
        public string strKehuoName
        {
            set { _strkehuoname = value; }
            get { return _strkehuoname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nTrainmanState1
        {
            set { _ntrainmanstate1 = value; }
            get { return _ntrainmanstate1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nTrainmanState2
        {
            set { _ntrainmanstate2 = value; }
            get { return _ntrainmanstate2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nTrainmanState3
        {
            set { _ntrainmanstate3 = value; }
            get { return _ntrainmanstate3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strBak1
        {
            set { _strbak1 = value; }
            get { return _strbak1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastArriveTime
        {
            set { _dtlastarrivetime = value; }
            get { return _dtlastarrivetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strMainPlanGUID
        {
            set { _strmainplanguid = value; }
            get { return _strmainplanguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName4
        {
            set { _strtrainmanname4 = value; }
            get { return _strtrainmanname4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber4
        {
            set { _strtrainmannumber4 = value; }
            get { return _strtrainmannumber4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nPostID4
        {
            set { _npostid4 = value; }
            get { return _npostid4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTelNumber4
        {
            set { _strtelnumber4 = value; }
            get { return _strtelnumber4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtLastEndWorkTime4
        {
            set { _dtlastendworktime4 = value; }
            get { return _dtlastendworktime4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nTrainmanState
        {
            set { _ntrainmanstate = value; }
            get { return _ntrainmanstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID4
        {
            set { _strtrainmanguid4 = value; }
            get { return _strtrainmanguid4; }
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
        public string strPlanTypeName
        {
            set { _strplantypename = value; }
            get { return _strplantypename; }
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
        public string strStateName
        {
            set { _strstatename = value; }
            get { return _strstatename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nDriverType1
        {
            set { _ndrivertype1 = value; }
            get { return _ndrivertype1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nDriverType2
        {
            set { _ndrivertype2 = value; }
            get { return _ndrivertype2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nDriverType3
        {
            set { _ndrivertype3 = value; }
            get { return _ndrivertype3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? nDriverType4
        {
            set { _ndrivertype4 = value; }
            get { return _ndrivertype4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strABCD1
        {
            set { _strabcd1 = value; }
            get { return _strabcd1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strABCD2
        {
            set { _strabcd2 = value; }
            get { return _strabcd2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strABCD3
        {
            set { _strabcd3 = value; }
            get { return _strabcd3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strABCD4
        {
            set { _strabcd4 = value; }
            get { return _strabcd4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isKey1
        {
            set { _iskey1 = value; }
            get { return _iskey1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isKey2
        {
            set { _iskey2 = value; }
            get { return _iskey2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isKey3
        {
            set { _iskey3 = value; }
            get { return _iskey3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isKey4
        {
            set { _iskey4 = value; }
            get { return _iskey4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strPlanStateName
        {
            set { _strplanstatename = value; }
            get { return _strplanstatename; }
        }

        /// <summary>
        /// 计划下达时间
        /// </summary>
        public DateTime? dtSendTime
        {
            set { _dtSendTime = value; }
            get { return _dtSendTime; }
        }
        /// <summary>
        /// 计划接收时间
        /// </summary>
        public DateTime? dtRecvTime
        {
            set { _dtRecvTime = value; }
            get { return _dtRecvTime; }
        }



        #endregion Model
    }

}
