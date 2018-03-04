using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///类名: TogetherTrain
	///说明: 包乘机车信息
	/// </summary>
	public class TogetherTrain
	{
		public TogetherTrain()
		{}

		/// <summary>
		/// 包乘机车GUID
		/// </summary>
		public string strTrainGUID;

		/// <summary>
		/// 所属交路GUID
		/// </summary>
		public string strTrainmanJiaoluGUID;

		/// <summary>
		/// 机车类型
		/// </summary>
		public string strTrainTypeName;

		/// <summary>
		/// 机车号
		/// </summary>
		public string strTrainNumber;

		/// <summary>
		/// 包含的机组
		/// </summary>
		public OrderGroupInTrainArray Groups = new OrderGroupInTrainArray();

	}

    /// <summary>
    ///类名: OrderGroupInTrainArray
    ///说明: 包乘机车机组列表
    /// </summary>
    public class OrderGroupInTrainArray : List<OrderGroupInTrain>
    {
        public OrderGroupInTrainArray()
        { }

    }

    /// <summary>
    ///类名: OrderGroupInTrain
    ///说明: 包乘机车机组信息
    /// </summary>
    public class OrderGroupInTrain
    {
        public OrderGroupInTrain()
        { }

        /// <summary>
        /// 排序GUID
        /// </summary>
        public string strOrderGUID;

        /// <summary>
        /// 所属机车GUID
        /// </summary>
        public string strTrainGUID;

        /// <summary>
        /// 序号
        /// </summary>
        public int nOrder;

        /// <summary>
        /// 机组信息
        /// </summary>
        public Groupmp Group = new Groupmp();

        /// <summary>
        /// 最后到达时间
        /// </summary>
        public DateTime dtLastArriveTime;

    }

    /// <summary>
    ///类名: TogetherTrainJiaoLu
    ///说明: 包乘交路
    /// </summary>
    public class TogetherTrainJiaoLu : Jiaolu
    {
        public TogetherTrainJiaoLu()
        { }

        /// <summary>
        /// 包乘机车列表
        /// </summary>
        public TogetherTrainArray TogetherTrainArray = new TogetherTrainArray();

    }

    /// <summary>
    ///类名: TogetherTrainArray
    ///说明: 包乘机车列表
    /// </summary>
    public class TogetherTrainArray : List<TogetherTrain>
    {
        public TogetherTrainArray()
        { }

    }

}
