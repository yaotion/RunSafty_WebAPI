using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///类名: NamedJiaolu
	///说明: 记名式交路
	/// </summary>
	public class NamedJiaolu:Jiaolu
	{
		public NamedJiaolu()
		{}	
		/// <summary>
		/// 记名式机组列表
		/// </summary>
		public NamedGroupArray NamedGroupArray = new NamedGroupArray();

	}

    /// <summary>
    ///类名: NamedGroupArray
    ///说明: 记名式机组列表
    /// </summary>
    public class NamedGroupArray : List<NamedGroup>
    {
        public NamedGroupArray()
        { }

    }

    /// <summary>
    ///类名: NamedGroup
    ///说明: 记名式机组信息
    /// </summary>
    public class NamedGroup
    {
        public NamedGroup()
        { }

        /// <summary>
        /// 车次GUID
        /// </summary>
        public string strCheciGUID;

        /// <summary>
        /// 所属人员交路GUID
        /// </summary>
        public string strTrainmanJiaoluGUID;

        /// <summary>
        /// 交路内序号
        /// </summary>
        public int nCheciOrder;

        /// <summary>
        /// 车次类型
        /// </summary>
        public int nCheciType;

        /// <summary>
        /// 值乘车次
        /// </summary>
        public string strCheci1;

        /// <summary>
        /// 回路车次
        /// </summary>
        public string strCheci2;

        /// <summary>
        /// 机组信息
        /// </summary>
        public Groupmp Group = new Groupmp();

        /// <summary>
        /// 最后到达时间
        /// </summary>
        public DateTime dtLastArriveTime;

    }

}
