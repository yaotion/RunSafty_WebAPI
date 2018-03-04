using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///����: TogetherTrain
	///˵��: ���˻�����Ϣ
	/// </summary>
	public class TogetherTrain
	{
		public TogetherTrain()
		{}

		/// <summary>
		/// ���˻���GUID
		/// </summary>
		public string strTrainGUID;

		/// <summary>
		/// ������·GUID
		/// </summary>
		public string strTrainmanJiaoluGUID;

		/// <summary>
		/// ��������
		/// </summary>
		public string strTrainTypeName;

		/// <summary>
		/// ������
		/// </summary>
		public string strTrainNumber;

		/// <summary>
		/// �����Ļ���
		/// </summary>
		public OrderGroupInTrainArray Groups = new OrderGroupInTrainArray();

	}

    /// <summary>
    ///����: OrderGroupInTrainArray
    ///˵��: ���˻��������б�
    /// </summary>
    public class OrderGroupInTrainArray : List<OrderGroupInTrain>
    {
        public OrderGroupInTrainArray()
        { }

    }

    /// <summary>
    ///����: OrderGroupInTrain
    ///˵��: ���˻���������Ϣ
    /// </summary>
    public class OrderGroupInTrain
    {
        public OrderGroupInTrain()
        { }

        /// <summary>
        /// ����GUID
        /// </summary>
        public string strOrderGUID;

        /// <summary>
        /// ��������GUID
        /// </summary>
        public string strTrainGUID;

        /// <summary>
        /// ���
        /// </summary>
        public int nOrder;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Groupmp Group = new Groupmp();

        /// <summary>
        /// ��󵽴�ʱ��
        /// </summary>
        public DateTime dtLastArriveTime;

    }

    /// <summary>
    ///����: TogetherTrainJiaoLu
    ///˵��: ���˽�·
    /// </summary>
    public class TogetherTrainJiaoLu : Jiaolu
    {
        public TogetherTrainJiaoLu()
        { }

        /// <summary>
        /// ���˻����б�
        /// </summary>
        public TogetherTrainArray TogetherTrainArray = new TogetherTrainArray();

    }

    /// <summary>
    ///����: TogetherTrainArray
    ///˵��: ���˻����б�
    /// </summary>
    public class TogetherTrainArray : List<TogetherTrain>
    {
        public TogetherTrainArray()
        { }

    }

}
