using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Api.Utilities;

namespace TF.RunSafty.Model
{
    public class TAB_Base_PlanRest_Json
    {
         [NotNull]
        public int nid { get; set; }
         [NotNull]
        public string strguid { get; set; }
         [NotNull]
        public string strtrainjiaoluguid { get; set; }
         [NotNull]
        public DateTime dtarrivetime { get; set; }
        public DateTime dtcalltime { get; set; }
        public string strtrainno { get; set; }
        public DateTime dtchuqintime { get; set; }
        public DateTime dtstarttime { get; set; }
        public string strtrainnoguid { get; set; }
        public string strworkgrouguid { get; set; }
        public string strtrainmanguid1 { get; set; }
        public string strtrainmanguid2 { get; set; }
        public string strtrainmanguid3 { get; set; }
        public string strtrainmanguid4 { get; set; }
        public int nNeedRest { get; set; }
        public int nFinished { get; set; }
        public DateTime dtStartTrainTime { get; set; }
        public string ePlanState { get; set; }
        public DateTime dtSignTime { get; set; }
    }
}
