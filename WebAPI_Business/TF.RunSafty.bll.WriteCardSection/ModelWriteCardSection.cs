using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WriteCardSection
{
   public class WriteCardSection
    {

        private string _strJWDNumber;

        public string strJWDNumber
        {
            get { return _strJWDNumber; }
            set { _strJWDNumber = value; }
        }
        private string _strSectionID;

        public string strSectionID
        {
            get { return _strSectionID; }
            set { _strSectionID = value; }
        }
        private string _strSectionName;

        public string strSectionName
        {
            get { return _strSectionName; }
            set { _strSectionName = value; }
        }


    }
}
