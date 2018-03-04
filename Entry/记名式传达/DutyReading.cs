using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Api.Entity
{
    
    public class DutyReading_ReadRecordList
    {

        public string workid
        {
            get;
            set;
        }


        public int worktype
        {
            get;
            set;
        }

        public List<DutyReading_Record> recordList
        {
            get;
            set;
        }

        public String sid
        {
            get;
            set;
        }

        public List<DutyReading_Trainman> trainmanList { get; set; }
    }



    public class DutyReading_Trainman
    {
        public string trainmanguid { get; set; }
    }

    //recordList

    public class DutyReading_Record
    {
        public String rid
        {
            get;
            set;
        }

        public DateTime rtime
        {
            get;
            set;
        }
    }
}
