using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///����: OrderGroup
	///˵��: �ֳ˻�����Ϣ
	/// </summary>
	public class OrderGroupAdd
	{
		public OrderGroupAdd()
		{}

		/// <summary>
		/// ����GUID
		/// </summary>
		public string strOrderGUID;

		/// <summary>
		/// ������·GUID
		/// </summary>
		public string strTrainmanJiaoluGUID;

		/// <summary>
		/// ���
		/// </summary>
		public int nOrder;

		/// <summary>
		/// ������Ϣ
		/// </summary>
        public Groupmp Group = new Groupmp();

		/// <summary>
		/// ���鵽��ʱ��
		/// </summary>
		public DateTime dtLastArriveTime;

	}

  
}
