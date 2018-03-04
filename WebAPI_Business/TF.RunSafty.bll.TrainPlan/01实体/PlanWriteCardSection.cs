using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.ICCard
{
	/// <summary>
	///����: PlanWriteCardSection
	///˵��: �г��ƻ���Ӧ�����д������
	/// </summary>
	public class PlanWriteCardSection
	{
		public PlanWriteCardSection()
		{}

		/// <summary>
		/// �г��ƻ�GUID
		/// </summary>
		public string strTrainPlanGUID;

		/// <summary>
		/// д�����α��
		/// </summary>
        public string strSectionID;

		/// <summary>
		/// д����������
		/// </summary>
        public string strSectionName;

		/// <summary>
		/// ����κ�
		/// </summary>
        public string strJWDNumber;

		/// <summary>
		/// ����ID
		/// </summary>
		public int nID;

	}

    /// <summary>
    ///����: PlanWriteCardSectionList
    ///˵��: �г��ƻ���Ӧ�����д�������б�
    /// </summary>
    public class PlanWriteCardSectionList : List<PlanWriteCardSection>
    {
        public PlanWriteCardSectionList()
        { }

    }
}
