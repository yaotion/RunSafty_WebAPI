using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.DrinkLogic
{
    public class MDDrinkLogic
    {

        private string _strDepartmentID;

        public string strDepartmentID
        {
            get { return _strDepartmentID; }
            set { _strDepartmentID = value; }
        }
        private string _strDepartmentName;

        public string strDepartmentName
        {
            get { return _strDepartmentName; }
            set { _strDepartmentName = value; }
        }
        private string _nCadreTypeID;

        public string nCadreTypeID
        {
            get { return _nCadreTypeID; }
            set { _nCadreTypeID = value; }
        }
        private string _strCadreTypeName;

        public string strCadreTypeName
        {
            get { return _strCadreTypeName; }
            set { _strCadreTypeName = value; }
        }

    }
}
