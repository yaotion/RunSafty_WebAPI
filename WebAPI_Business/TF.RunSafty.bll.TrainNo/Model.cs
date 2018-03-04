using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.Runsafty.TrainNo
{
	/// <summary>
	//类名: TrainNo
	//说明: 图定车次信息
	/// <summary>
	public class TrainNo
	{
		public TrainNo()
		{}

		/// <summary>
		/// 所属区段编号
		/// <summary>
		public string trainjiaoluID;

		/// <summary>
		/// 所属区段名称
		/// <summary>
		public string trainjiaoluName;

		/// <summary>
		/// 出勤点编号
		/// <summary>
		public string placeID;

		/// <summary>
		/// 出勤点
		/// <summary>
		public string placeName;

		/// <summary>
		/// 机型
		/// <summary>
		public string trainTypeName;

		/// <summary>
		/// 车号
		/// <summary>
		public string trainNumber;

		/// <summary>
		/// 车次
		/// <summary>
		public string trainNo;

		/// <summary>
		/// 备注
		/// <summary>
		public string remark;

		/// <summary>
		/// 计划出勤时间
		/// <summary>
		public DateTime startTime;

		/// <summary>
		/// 计划开车时间
		/// <summary>
		public DateTime kaiCheTime;

		/// <summary>
		/// 发车站编号
		/// <summary>
		public string startStationID;

		/// <summary>
		/// 发车站
		/// <summary>
		public string startStationName;

		/// <summary>
		/// 终到站编号
		/// <summary>
		public string endStationID;

		/// <summary>
		/// 终到站
		/// <summary>
		public string endStationName;

		/// <summary>
		/// 值乘类型编号
		/// <summary>
		public string trainmanTypeID;

		/// <summary>
		/// 支撑类型
		/// <summary>
		public string trainmanTypeName;

		/// <summary>
		/// 计划类型编号
		/// <summary>
		public string planTypeID;

		/// <summary>
		/// 计划类型
		/// <summary>
		public string planTypeName;

		/// <summary>
		/// 牵引类型编号
		/// <summary>
		public string dragTypeID;

		/// <summary>
		/// 牵引类型
		/// <summary>
		public string dragTypeName;

		/// <summary>
		/// 客货编号
		/// <summary>
		public string kehuoID;

		/// <summary>
		/// 客货
		/// <summary>
		public string kehuoName;

		/// <summary>
		/// 备注类型
		/// <summary>
		public string remarkTypeID;

		/// <summary>
		/// 备注
		/// <summary>
		public string remarkTypeName;

		/// <summary>
		/// 图定车次编号
		/// <summary>
		public string trainNoID;

	    public string strWorkDay;
	    public DateTime? dtCallTime;
	    public DateTime? dtArriveTime;
	    public int? nNeedRest;
	}
	/// <summary>
	//类名: TrainnoList
	//说明: 图定车次列表
	/// <summary>
	public class TrainnoList : List<TrainNo>
	{
		public TrainnoList()
		{}

	}


    public  class Train
    {
        public string trainnoID;
    }
   

}
