using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///类名: Jiaolu
	///说明: 交路信息
	/// </summary>
	public class Jiaolu
	{
		public Jiaolu()
		{}

		/// <summary>
		/// 交路GUID
		/// </summary>
		public string JiaoluGUID;

		/// <summary>
		/// 交路名称
		/// </summary>
		public string JiaoluName;

		/// <summary>
		/// 交路类型
		/// </summary>
		public int JiaoluType;

		/// <summary>
		/// 显示的位置
		/// </summary>
		public BoardPosition Position = new BoardPosition();

		/// <summary>
		/// 请销假预备人员列表
		/// </summary>
		public TrainmanLeaveArray TrainmanLeaveArray = new TrainmanLeaveArray();

	}

    /// <summary>
    ///类名: BoardPosition
    ///说明: 名牌位置
    /// </summary>
    public class BoardPosition
    {
        public BoardPosition()
        { }

        /// <summary>
        /// 横坐标
        /// </summary>
        public int X;

        /// <summary>
        /// 中坐标
        /// </summary>
        public int Y;

    }

    /// <summary>
    ///类名: TrainmanLeaveArray
    ///说明: 请销假人员信息列表
    /// </summary>
    public class TrainmanLeaveArray : List<TrainmanLeaveInfo>
    {
        public TrainmanLeaveArray()
        { }

    }


    /// <summary>
    ///类名: OrderJiaolu
    ///说明: 轮乘交路信息
    /// </summary>
    public class OrderJiaolu : Jiaolu
    {
        public OrderJiaolu()
        { }


        /// <summary>
        /// 轮乘机组列表
        /// </summary>
        public OrderGroupArray OrderGroupArray = new OrderGroupArray();

    }


    /// <summary>
    ///类名: OrderGroupArray
    ///说明: 轮乘机组列表
    /// </summary>
    public class OrderGroupArray : List<OrderGroupAdd>
    {
        public OrderGroupArray()
        { }

    }


}
