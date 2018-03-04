using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///类名: OrderGroup
	///说明: 轮乘机组信息
	/// </summary>
	public class OrderGroupAdd
	{
		public OrderGroupAdd()
		{}

		/// <summary>
		/// 排序GUID
		/// </summary>
		public string strOrderGUID;

		/// <summary>
		/// 所属交路GUID
		/// </summary>
		public string strTrainmanJiaoluGUID;

		/// <summary>
		/// 序号
		/// </summary>
		public int nOrder;

		/// <summary>
		/// 机组信息
		/// </summary>
        public Groupmp Group = new Groupmp();

		/// <summary>
		/// 机组到达时间
		/// </summary>
		public DateTime dtLastArriveTime;

	}

  
}
