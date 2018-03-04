using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Entry
{
    public class nav
    {
        public nav()
        { }
        #region Model
        private string _typeid;
        private string _typeparentid;
        private string _typename;
        private string _navUrl;
        private string _strSort;
        private string _trNavUrl;

        public string TrNavUrl
        {
            get { return _trNavUrl; }
            set { _trNavUrl = value; }
        }

        public string StrSort
        {
            get { return _strSort; }
            set { _strSort = value; }
        }

        public string NavUrl
        {
            get { return _navUrl; }
            set { _navUrl = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeParentID
        {
            set { _typeparentid = value; }
            get { return _typeparentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string typeName
        {
            set { _typename = value; }
            get { return _typename; }
        }


        #endregion Model

    }
}

