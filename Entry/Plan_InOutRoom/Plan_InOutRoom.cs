using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Api.Utilities;

namespace TF.RunSafty.Model
{
    public class Json_Plan_InOutRoom
    {
        [NotNull]
        public string strPlanGUID { get; set; }
        [NotNull]
        public string InOutRoomType { get; set; }
        [NotNull]
        public string strTrainmanGUID { get; set; }
        [NotNull]
        public string dtTime { get; set; }
    }
}
