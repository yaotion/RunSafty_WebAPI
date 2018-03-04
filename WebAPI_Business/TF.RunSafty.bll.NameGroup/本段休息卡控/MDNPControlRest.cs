using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.NamePlate
{
    /// <summary>
    /// 本段休息卡控设置
    /// </summary>
    public class MDNPControlRest
    {
        /// <summary>
        /// 人员交路
        /// </summary>
        public string TrainmanJiaoluGUID = "";
        //交路名称
        public string TrainmanJiaoluName = "";
        //出勤点编号
        public string PlaceID = "";
        /// <summary>
        ///出勤点名称
        /// </summary>
        public string PlaceName = "";
        //是否开启本段休息卡控
        public int ControlLocalRest = 0;
        /// <summary>
        /// 最小休息时长(分钟)
        /// </summary>
        public int MinLocalRestMinutes = 0;
        /// <summary>
        /// 是否本段
        /// </summary>
        public int LocalPlace = 0;
    }
}
