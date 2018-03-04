using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Api.Utilities;
namespace TF.RunSafty.Model
{
    public class TAB_Base_TrainNo_Json
    {
        [NotNull]
        public string trainjiaoluID { get; set; }
        [NotNull]
        public string trainjiaoluName { get; set; }
        [NotNull]
        public string placeID { get; set; }
        [NotNull]
        public string placeName { get; set; }
        [NotNull]
        public string trainTypeName { get; set; }
        [NotNull]
        public string trainNumber { get; set; }
        [NotNull]
        public string trainNo { get; set; }
        [NotNull]
        public string remark { get; set; }
        [NotNull]
        public string startTime { get; set; }
        [NotNull]
        public string kaiCheTime { get; set; }
        [NotNull]
        public string startStationID { get; set; }
        [NotNull]
        public string startStationName { get; set; }
        [NotNull]
        public string endStationID { get; set; }
        [NotNull]
        public string endStationName { get; set; }
        [NotNull]
        public string trainmanTypeID { get; set; }
        [NotNull]
        public string trainmanTypeName { get; set; }
        [NotNull]
        public string planTypeID { get; set; }
        [NotNull]
        public string planTypeName { get; set; }
        [NotNull]
        public string dragTypeID { get; set; }
        [NotNull]
        public string dragTypeName { get; set; }
        [NotNull]
        public string kehuoID { get; set; }
        [NotNull]
        public string kehuoName { get; set; }
        [NotNull]
        public string remarkTypeID { get; set; }
        [NotNull]
        public string remarkTypeName { get; set; }
        [NotNull]
        public string trainNoID { get; set; }

        public string dtArriveTime { get; set; }
        public string dtCallTime { get; set; }
        public int? nNeedRest { get; set; }
        public string strWorkDay { get; set; }
    }
}
