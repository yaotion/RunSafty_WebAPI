using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Model
{
    public partial class WorkShop
    {

        private string _strWorkShopGUID;

        public string strWorkShopGUID
        {
            get { return _strWorkShopGUID; }
            set { _strWorkShopGUID = value; }
        }
        private string _strAreaGUID;

        public string strAreaGUID
        {
            get { return _strAreaGUID; }
            set { _strAreaGUID = value; }
        }
        private string _strWorkShopName;

        public string strWorkShopName
        {
            get { return _strWorkShopName; }
            set { _strWorkShopName = value; }
        }
        private string _strWorkShopNumber;

        public string strWorkShopNumber
        {
            get { return _strWorkShopNumber; }
            set { _strWorkShopNumber = value; }
        }

             

    }
}
