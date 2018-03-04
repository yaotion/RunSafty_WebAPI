/**  版本信息模板在安装目录下，可自行修改。
* TAB_MsgCallWork.cs
*
* 功 能： N/A
* 类 名： TAB_MsgCallWork
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-10-10 13:06:43   N/A    初版
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
	/// <summary>
	/// TAB_MsgCallWork:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class TAB_MsgCallWork
    {
        public TAB_MsgCallWork()
        { }
        #region Model
        private int _nid;
        private string _strmsgguid;
        private string _strplanguid;
        private string _strtrainmanguid;
        private string _strTrainmanName;

        public string strTrainmanName
        {
            get { return _strTrainmanName; }
            set { _strTrainmanName = value; }
        }
        private string _strTrainmanNumber;

        public string strTrainmanNumber
        {
            get { return _strTrainmanNumber; }
            set { _strTrainmanNumber = value; }
        }
        private string _strMobileNumber;

        public string strMobileNumber
        {
            get { return _strMobileNumber; }
            set { _strMobileNumber = value; }
        }



        private string _strsendmsgcontent;
        private string _strrecvmsgcontent;
        private DateTime? _dtcalltime;
        private int? _ncalltimes;

        private int? _nsendcount;

        private int? _nrecvcount;
        private int? _nstate;

        private string _dtStartTime;//开车时间

        public string dtStartTime
        {
            get { return _dtStartTime; }
            set { _dtStartTime = value; }
        }
        private string _dtChuQinTime;//出勤时间

        public string dtChuQinTime
        {
            get { return _dtChuQinTime; }
            set { _dtChuQinTime = value; }
        }

        private string _strTrainNo;

        public string strTrainNo
        {
            get { return _strTrainNo; }
            set { _strTrainNo = value; }
        }





        private string _ntype;

        public string eCallType
        {
            get { return _ntype; }
            set { _ntype = value; }
        }



        private string _dtSendTime;

        public string dtSendTime
        {
            get { return _dtSendTime; }
            set { _dtSendTime = value; }
        }
        private string _strSendUser;

        public string strSendUser
        {
            get { return _strSendUser; }
            set { _strSendUser = value; }
        }
        private string _strRecvUser;

        public string strRecvUser
        {
            get { return _strRecvUser; }
            set { _strRecvUser = value; }
        }
        private string _dtRecvTime;

        public string dtRecvTime
        {
            get { return _dtRecvTime; }
            set { _dtRecvTime = value; }
        }


        private string _strCancelReason;

        public string strCancelReason
        {
            get { return _strCancelReason; }
            set { _strCancelReason = value; }
        }



        /// <summary>
        /// 
        /// </summary>
        public int nId
        {
            set { _nid = value; }
            get { return _nid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strMsgGUID
        {
            set { _strmsgguid = value; }
            get { return _strmsgguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strPlanGUID
        {
            set { _strplanguid = value; }
            get { return _strplanguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID
        {
            set { _strtrainmanguid = value; }
            get { return _strtrainmanguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strSendMsgContent
        {
            set { _strsendmsgcontent = value; }
            get { return _strsendmsgcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strRecvMsgContent
        {
            set { _strrecvmsgcontent = value; }
            get { return _strrecvmsgcontent; }
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
        public int? nCallTimes
        {
            set { _ncalltimes = value; }
            get { return _ncalltimes; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int? nSendCount
        {
            set { _nsendcount = value; }
            get { return _nsendcount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? nRecvCount
        {
            set { _nrecvcount = value; }
            get { return _nrecvcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? eCallState
        {
            set { _nstate = value; }
            get { return _nstate; }
        }
        #endregion Model

    }
}

