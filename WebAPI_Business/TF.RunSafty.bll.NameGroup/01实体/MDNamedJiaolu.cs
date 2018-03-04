using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.NamePlate.MD
{
	/// <summary>
	///����: NamedJiaolu
	///˵��: ����ʽ��·
	/// </summary>
	public class NamedJiaolu:Jiaolu
	{
		public NamedJiaolu()
		{}	
		/// <summary>
		/// ����ʽ�����б�
		/// </summary>
		public NamedGroupArray NamedGroupArray = new NamedGroupArray();

	}

    /// <summary>
    ///����: NamedGroupArray
    ///˵��: ����ʽ�����б�
    /// </summary>
    public class NamedGroupArray : List<NamedGroup>
    {
        public NamedGroupArray()
        { }

    }

    /// <summary>
    ///����: NamedGroup
    ///˵��: ����ʽ������Ϣ
    /// </summary>
    public class NamedGroup
    {
        public NamedGroup()
        { }

        /// <summary>
        /// ����GUID
        /// </summary>
        public string strCheciGUID;

        /// <summary>
        /// ������Ա��·GUID
        /// </summary>
        public string strTrainmanJiaoluGUID;

        /// <summary>
        /// ��·�����
        /// </summary>
        public int nCheciOrder;

        /// <summary>
        /// ��������
        /// </summary>
        public int nCheciType;

        /// <summary>
        /// ֵ�˳���
        /// </summary>
        public string strCheci1;

        /// <summary>
        /// ��·����
        /// </summary>
        public string strCheci2;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Groupmp Group = new Groupmp();

        /// <summary>
        /// ��󵽴�ʱ��
        /// </summary>
        public DateTime dtLastArriveTime;

    }

}
