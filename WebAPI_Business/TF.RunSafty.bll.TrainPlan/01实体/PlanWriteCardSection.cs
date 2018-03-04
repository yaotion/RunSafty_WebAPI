using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.ICCard
{
	/// <summary>
	///类名: PlanWriteCardSection
	///说明: 行车计划对应调令的写卡区段
	/// </summary>
	public class PlanWriteCardSection
	{
		public PlanWriteCardSection()
		{}

		/// <summary>
		/// 行车计划GUID
		/// </summary>
		public string strTrainPlanGUID;

		/// <summary>
		/// 写卡区段编号
		/// </summary>
        public string strSectionID;

		/// <summary>
		/// 写卡区段名称
		/// </summary>
        public string strSectionName;

		/// <summary>
		/// 机务段号
		/// </summary>
        public string strJWDNumber;

		/// <summary>
		/// 自增ID
		/// </summary>
		public int nID;

	}

    /// <summary>
    ///类名: PlanWriteCardSectionList
    ///说明: 行车计划对应调令的写卡区段列表
    /// </summary>
    public class PlanWriteCardSectionList : List<PlanWriteCardSection>
    {
        public PlanWriteCardSectionList()
        { }

    }
}
