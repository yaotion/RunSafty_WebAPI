using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Api.Utilities;

namespace TF.RunSafty.Model
{
    public class TAB_Plan_ToTakes
    {
        [NotNull]
       public string strPlanGUID { get; set; }
       
        public string strCheCi { get; set; }
        public string dtCallWorkTime { get; set; }
        public string dtWaitWorkTime { get; set; }
        public string strTrainmanGUID1 { get; set; }
        public string strTrainmanGUID2 { get; set; }
        public string strTrainmanGUID3 { get; set; }
        public string strTrainmanGUID4 { get; set; }
    }
}
