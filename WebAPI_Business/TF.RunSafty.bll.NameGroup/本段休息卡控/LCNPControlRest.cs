using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.NamePlate
{
    public class LCNPControlRest
    {
        public static List<MDNPControlRest> GetControlResultList(string TrainmanJiaoluGUID, string PlaceID)
        {
            return DBNPControlRest.GetControlResultList(TrainmanJiaoluGUID,PlaceID);
        }
    }
}
