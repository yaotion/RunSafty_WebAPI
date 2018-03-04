using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///����: JiaoluArray
	///˵��: ��·�����б�
	/// </summary>
	public class JiaoluArray : List<Jiaolu>
	{
		public JiaoluArray()
		{}

	}

    /// <summary>
    ///����: TrainmanLeaveInfo
    ///˵��: ��������Ա��Ϣ
    /// </summary>
    public class TrainmanLeaveInfo
    {
        public TrainmanLeaveInfo()
        { }

        /// <summary>
        /// �������GUID
        /// </summary>
        public string strLeaveTypeGUID;

        /// <summary>
        /// �����������
        /// </summary>
        public string strLeaveTypeName;

        /// <summary>
        /// ��Ա��Ϣ
        /// </summary>
        public Trainmanss Trainman = new Trainmanss();

    }

    /// <summary>
    ///����: Trainman
    ///˵��: ����Ա��Ϣ
    /// </summary>
    public class Trainmanss
    {
        public Trainmanss()
        { }

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID = "";

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber = "";

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName = "";

        /// <summary>
        /// 
        /// </summary>
        public int nPostID = 0;

        /// <summary>
        /// Ԥ����ʱ
        /// </summary>
        public int nReadyOverTime;

        public int isKey;

        //�ɲ������ƻ�״̬
        public int nPlanState;


        public int nDispatching;
        
        /// <summary>
        ///��Ա״̬0 �ա�1�ƻ���2��Ԣ��3��Ԣ��4���ڡ�5�����Ԣ��6�����Ԣ��7����
        ///���ƻ�����Щ״̬ʹ�ã���ʱֻʹ������Ԣ״̬�� ,���ܺ�TrainmanState�ֶγ�ͻ
        ///������ӿڴ���ʱ��ͳһ�滮
        /// </summary>
        public int nWorkState;
    }

}
