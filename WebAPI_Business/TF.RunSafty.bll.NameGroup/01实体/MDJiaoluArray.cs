using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///类名: JiaoluArray
	///说明: 交路名牌列表
	/// </summary>
	public class JiaoluArray : List<Jiaolu>
	{
		public JiaoluArray()
		{}

	}

    /// <summary>
    ///类名: TrainmanLeaveInfo
    ///说明: 请销假人员信息
    /// </summary>
    public class TrainmanLeaveInfo
    {
        public TrainmanLeaveInfo()
        { }

        /// <summary>
        /// 请假类型GUID
        /// </summary>
        public string strLeaveTypeGUID;

        /// <summary>
        /// 请假类型名称
        /// </summary>
        public string strLeaveTypeName;

        /// <summary>
        /// 人员信息
        /// </summary>
        public Trainmanss Trainman = new Trainmanss();

    }

    /// <summary>
    ///类名: Trainman
    ///说明: 乘务员信息
    /// </summary>
    public class Trainmanss
    {
        public Trainmanss()
        { }

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID = "";

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber = "";

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName = "";

        /// <summary>
        /// 
        /// </summary>
        public int nPostID = 0;

        /// <summary>
        /// 预备超时
        /// </summary>
        public int nReadyOverTime;

        public int isKey;

        //干部所处计划状态
        public int nPlanState;


        public int nDispatching;
        
        /// <summary>
        ///人员状态0 空、1计划、2入寓、3离寓、4出勤、5外段入寓、6外段离寓、7退勤
        ///（计划按这些状态使用，暂时只使用了入寓状态） ,可能和TrainmanState字段冲突
        ///在整理接口代码时再统一规划
        /// </summary>
        public int nWorkState;
    }

}
