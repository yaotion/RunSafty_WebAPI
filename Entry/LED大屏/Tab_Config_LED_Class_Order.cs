using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Model
{
    public partial class Tab_Config_LED_Class_Order
    {
        #region Model
        private int? _nid;
        private string _strchejianguid;
        private string _strchejianname;
        private string _strCheJianNickName;
        private string _strKeHuDuanGUID;

        public string strKeHuDuanGUID
        {
            get { return _strKeHuDuanGUID; }
            set { _strKeHuDuanGUID = value; }
        }


        public string strCheJianNickName
        {
            get { return _strCheJianNickName; }
            set { _strCheJianNickName = value; }
        }
        private string _strjiaoluguid;
        private string _strjiaoluname;
        private string _strJiaoLuNickName;

        public string strJiaoLuNickName
        {
            get { return _strJiaoLuNickName; }
            set { _strJiaoLuNickName = value; }
        }
        private string _strtitle;
        /// <summary>
        /// 
        /// </summary>
        public int? nid
        {
            set { _nid = value; }
            get { return _nid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCheJianGUID
        {
            set { _strchejianguid = value; }
            get { return _strchejianguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strCheJianName
        {
            set { _strchejianname = value; }
            get { return _strchejianname; }
        }
      
        /// <summary>
        /// 
        /// </summary>
        public string strJiaoLuGUID
        {
            set { _strjiaoluguid = value; }
            get { return _strjiaoluguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string strJiaoLuName
        {
            set { _strjiaoluname = value; }
            get { return _strjiaoluname; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string strTitle
        {
            set { _strtitle = value; }
            get { return _strtitle; }
        }
        #endregion Model
    }
}
