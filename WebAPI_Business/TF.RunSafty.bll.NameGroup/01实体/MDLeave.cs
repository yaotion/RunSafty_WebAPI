using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.NamePlate.MD
{
    /// <summary>
    ///类名: TrainmanLeave
    ///说明: 非运转人员信息
    /// </summary>
    public class TrainmanLeave
    {
        public TrainmanLeave()
        { }

        /// <summary>
        /// 人员信息
        /// </summary>
        public Trainman Trainman = new Trainman();

        /// <summary>
        /// 请假类型GUID
        /// </summary>
        public string strLeaveTypeGUID;

        /// <summary>
        /// 请假类型名称
        /// </summary>
        public string strLeaveTypeName;

        /// <summary>
        /// 请假类型名称
        /// </summary>
        public string dBeginTime;


        /// <summary>
        /// 请假类型名称
        /// </summary>
        public string dEndTime;

    }


    /// <summary>
    ///类名: TrainmanLeaveList
    ///说明: 非运转人员列表信息
    /// </summary>
    public class TrainmanLeaveList : List<TrainmanLeave>
    {
        public TrainmanLeaveList()
        { }

    }
}
