using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Runsafty.TestWeb
{
    #region 车站
    public class Station
    {
        private int _nid;

        public int nid
        {
            get { return _nid; }
            set { _nid = value; }
        }
        private string _strStationGUID;

        public string strStationGUID
        {
            get { return _strStationGUID; }
            set { _strStationGUID = value; }
        }
        private int _strStationNumber;

        public int strStationNumber
        {
            get { return _strStationNumber; }
            set { _strStationNumber = value; }
        }
        private string _strStationName;

        public string strStationName
        {
            get { return _strStationName; }
            set { _strStationName = value; }
        }
        private string _strWorkShopGUID;

        public string strWorkShopGUID
        {
            get { return _strWorkShopGUID; }
            set { _strWorkShopGUID = value; }
        }
        private string _strStationPY;

        public string strStationPY
        {
            get { return _strStationPY; }
            set { _strStationPY = value; }
        }

    }

    public class StationInTrainJiaolu
    {
        private string m_strTrainJiaoluGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strTrainJiaoluGUID
        {
            get { return m_strTrainJiaoluGUID; }
            set { m_strTrainJiaoluGUID = value; }
        }
        private string m_strStationGUID;
        /// <summary>
        /// 
        /// </summary>
        public string strStationGUID
        {
            get { return m_strStationGUID; }
            set { m_strStationGUID = value; }
        }
        private int m_strStationNumber;
        /// <summary>
        /// 
        /// </summary>
        public int strStationNumber
        {
            get { return m_strStationNumber; }
            set { m_strStationNumber = value; }
        }
        private string m_strStationName;
        /// <summary>
        /// 
        /// </summary>
        public string strStationName
        {
            get { return m_strStationName; }
            set { m_strStationName = value; }
        }
        private int m_nID;
        /// <summary>
        /// 
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }
        private int m_nSortid;
        /// <summary>
        /// 
        /// </summary>
        public int nSortid
        {
            get { return m_nSortid; }
            set { m_nSortid = value; }
        }
    }
    #endregion
}
