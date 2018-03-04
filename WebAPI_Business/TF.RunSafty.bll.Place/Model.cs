using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.DutyPlace
{

    public class DutyPlace
    {
        public DutyPlace()
        { }

        //出勤点编号
        public string placeID;

        //出勤点名称
        public string placeName;

    }

    public class DutyPlaceList : List<DutyPlace>
    {
    }
    

}
