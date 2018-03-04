using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Model
{
    public partial class TrainManPlanforLed
    {

        private string _strTrainTypeName;

        public string StrTrainTypeName
        {
            get { return _strTrainTypeName; }
            set { _strTrainTypeName = value; }
        }

        private string _workshopname;

        public string Workshopname
        {
            get { return _workshopname; }
            set { _workshopname = value; }
        }


        private string _strtrainno;

        public string Strtrainno
        {
            get { return _strtrainno; }
            set { _strtrainno = value; }
        }
        private string _strtrainnumber;

        public string Strtrainnumber
        {
            get { return _strtrainnumber; }
            set { _strtrainnumber = value; }
        }

       
        private string _dtstarttaintime;

        public string Dtstarttaintime
        {
            get { return _dtstarttaintime; }
            set { _dtstarttaintime = value; }
        }
        private string _dtchuqintime;

        public string Dtchuqintime
        {
            get { return _dtchuqintime; }
            set { _dtchuqintime = value; }
        }
        private string _strtrainmanname1;

        public string Strtrainmanname1
        {
            get { return _strtrainmanname1; }
            set { _strtrainmanname1 = value; }
        }
        private string _strtrainmanname2;

        public string Strtrainmanname2
        {
            get { return _strtrainmanname2; }
            set { _strtrainmanname2 = value; }
        }
        private string _strstartstation;

        public string Strstartstation
        {
            get { return _strstartstation; }
            set { _strstartstation = value; }
        }

    }
}
