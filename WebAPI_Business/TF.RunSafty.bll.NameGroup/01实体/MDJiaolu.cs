using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///����: Jiaolu
	///˵��: ��·��Ϣ
	/// </summary>
	public class Jiaolu
	{
		public Jiaolu()
		{}

		/// <summary>
		/// ��·GUID
		/// </summary>
		public string JiaoluGUID;

		/// <summary>
		/// ��·����
		/// </summary>
		public string JiaoluName;

		/// <summary>
		/// ��·����
		/// </summary>
		public int JiaoluType;

		/// <summary>
		/// ��ʾ��λ��
		/// </summary>
		public BoardPosition Position = new BoardPosition();

		/// <summary>
		/// ������Ԥ����Ա�б�
		/// </summary>
		public TrainmanLeaveArray TrainmanLeaveArray = new TrainmanLeaveArray();

	}

    /// <summary>
    ///����: BoardPosition
    ///˵��: ����λ��
    /// </summary>
    public class BoardPosition
    {
        public BoardPosition()
        { }

        /// <summary>
        /// ������
        /// </summary>
        public int X;

        /// <summary>
        /// ������
        /// </summary>
        public int Y;

    }

    /// <summary>
    ///����: TrainmanLeaveArray
    ///˵��: ��������Ա��Ϣ�б�
    /// </summary>
    public class TrainmanLeaveArray : List<TrainmanLeaveInfo>
    {
        public TrainmanLeaveArray()
        { }

    }


    /// <summary>
    ///����: OrderJiaolu
    ///˵��: �ֳ˽�·��Ϣ
    /// </summary>
    public class OrderJiaolu : Jiaolu
    {
        public OrderJiaolu()
        { }


        /// <summary>
        /// �ֳ˻����б�
        /// </summary>
        public OrderGroupArray OrderGroupArray = new OrderGroupArray();

    }


    /// <summary>
    ///����: OrderGroupArray
    ///˵��: �ֳ˻����б�
    /// </summary>
    public class OrderGroupArray : List<OrderGroupAdd>
    {
        public OrderGroupArray()
        { }

    }


}
