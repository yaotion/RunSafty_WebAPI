using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///类名: Group
	///说明: 机组信息
	/// </summary>
	public class Groupmp
	{
        public Groupmp()
		{}

		/// <summary>
		/// 机组GUID
		/// </summary>
		public string strGroupGUID = "";

		/// <summary>
		/// 机组状态
		/// </summary>
		public int GroupState = 0;

		/// <summary>
		/// 值乘的计划的GUID
		/// </summary>
		public string strTrainPlanGUID = "";

		/// <summary>
		/// 最近到达时间
		/// </summary>
		public DateTime dtArriveTime = DateTime.MinValue;

		/// <summary>
		/// 司机1入寓时间
		/// </summary>
        public DateTime dtLastInRoomTime1 = DateTime.MinValue;

		/// <summary>
		/// 司机2入寓时间
		/// </summary>
        public DateTime dtLastInRoomTime2 = DateTime.MinValue;

		/// <summary>
		/// 司机3入寓时间
		/// </summary>
        public DateTime dtLastInRoomTime3 = DateTime.MinValue;

		/// <summary>
		/// 司机4入寓时间
		/// </summary>
		public DateTime dtLastInRoomTime4 = DateTime.MinValue;
		/// <summary>
		/// 乘务员1
		/// </summary>
		public Trainmanss Trainman1 = new Trainmanss();

		/// <summary>
		/// 乘务员2
		/// </summary>
		public Trainmanss Trainman2 = new Trainmanss();

		/// <summary>
		/// 乘务员3
		/// </summary>
		public Trainmanss Trainman3 = new Trainmanss();

		/// <summary>
		/// 乘务员4
		/// </summary>
		public Trainmanss Trainman4 = new Trainmanss();

	}
}
