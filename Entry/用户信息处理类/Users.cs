using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Entry
{
     public class Users
    {

        #region Model
        private int _id;
        private string _usernumber;
        private string _username;
        private string _password;
        private int? _roseid;
        private int? _depid;
        private string _photourl;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userNumber
        {
            set { _usernumber = value; }
            get { return _usernumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string passWord
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? roseId
        {
            set { _roseid = value; }
            get { return _roseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? depId
        {
            set { _depid = value; }
            get { return _depid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string photoUrl
        {
            set { _photourl = value; }
            get { return _photourl; }
        }
        #endregion Model
    }
}
