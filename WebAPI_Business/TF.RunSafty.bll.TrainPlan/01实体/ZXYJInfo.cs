using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
namespace TF.RunSafty.Plan
{
	/// <summary>
	///类名: ZXYJInfo
	///说明: 在线预警信息
	/// </summary>
	public class ZXYJInfo
	{
		public ZXYJInfo()
		{}

		/// <summary>
		/// 
		/// </summary>
		public int nid;

		/// <summary>
		/// 计划guid
		/// </summary>
		public string strTrainPlanGUID;

		/// <summary>
		/// 工号1
		/// </summary>
		public string strTrainmanNumber1;

		/// <summary>
		/// 工号2
		/// </summary>
		public string strTrainmanNumber2;

		/// <summary>
		/// 事件发生事件
		/// </summary>
		public DateTime dtEventTime;

		/// <summary>
		/// 项点名称
		/// </summary>
		public string strXDMC;

		/// <summary>
		/// 车次
		/// </summary>
		public string strTrainNo;

		/// <summary>
		/// 车号
		/// </summary>
		public string strTrainNumber;

		/// <summary>
		/// 车型
		/// </summary>
		public string strTrainTypeName;

		/// <summary>
		/// 段号
		/// </summary>
		public string strDWH;

		/// <summary>
		/// 创建事件
		/// </summary>
		public DateTime dtCreateTime;

		/// <summary>
		/// tmis号
		/// </summary>
		public string nTMIS;

		/// <summary>
		/// tmis名称
		/// </summary>
		public string strTMISName;

	}

    /// <summary>
    ///类名: ZXYJInfoList
    ///说明: 在线预警信息列表
    /// </summary>
    public class ZXYJInfoList : List<ZXYJInfo>
    {
        public ZXYJInfoList()
        { }

    }  


}
