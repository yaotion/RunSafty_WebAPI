using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///����: Group
	///˵��: ������Ϣ
	/// </summary>
	public class Groupmp
	{
        public Groupmp()
		{}

		/// <summary>
		/// ����GUID
		/// </summary>
		public string strGroupGUID = "";

		/// <summary>
		/// ����״̬
		/// </summary>
		public int GroupState = 0;

		/// <summary>
		/// ֵ�˵ļƻ���GUID
		/// </summary>
		public string strTrainPlanGUID = "";

		/// <summary>
		/// �������ʱ��
		/// </summary>
		public DateTime dtArriveTime = DateTime.MinValue;

		/// <summary>
		/// ˾��1��Ԣʱ��
		/// </summary>
        public DateTime dtLastInRoomTime1 = DateTime.MinValue;

		/// <summary>
		/// ˾��2��Ԣʱ��
		/// </summary>
        public DateTime dtLastInRoomTime2 = DateTime.MinValue;

		/// <summary>
		/// ˾��3��Ԣʱ��
		/// </summary>
        public DateTime dtLastInRoomTime3 = DateTime.MinValue;

		/// <summary>
		/// ˾��4��Ԣʱ��
		/// </summary>
		public DateTime dtLastInRoomTime4 = DateTime.MinValue;
		/// <summary>
		/// ����Ա1
		/// </summary>
		public Trainmanss Trainman1 = new Trainmanss();

		/// <summary>
		/// ����Ա2
		/// </summary>
		public Trainmanss Trainman2 = new Trainmanss();

		/// <summary>
		/// ����Ա3
		/// </summary>
		public Trainmanss Trainman3 = new Trainmanss();

		/// <summary>
		/// ����Ա4
		/// </summary>
		public Trainmanss Trainman4 = new Trainmanss();

	}
}
