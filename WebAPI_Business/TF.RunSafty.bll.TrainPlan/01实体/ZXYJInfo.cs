using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
namespace TF.RunSafty.Plan
{
	/// <summary>
	///����: ZXYJInfo
	///˵��: ����Ԥ����Ϣ
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
		/// �ƻ�guid
		/// </summary>
		public string strTrainPlanGUID;

		/// <summary>
		/// ����1
		/// </summary>
		public string strTrainmanNumber1;

		/// <summary>
		/// ����2
		/// </summary>
		public string strTrainmanNumber2;

		/// <summary>
		/// �¼������¼�
		/// </summary>
		public DateTime dtEventTime;

		/// <summary>
		/// �������
		/// </summary>
		public string strXDMC;

		/// <summary>
		/// ����
		/// </summary>
		public string strTrainNo;

		/// <summary>
		/// ����
		/// </summary>
		public string strTrainNumber;

		/// <summary>
		/// ����
		/// </summary>
		public string strTrainTypeName;

		/// <summary>
		/// �κ�
		/// </summary>
		public string strDWH;

		/// <summary>
		/// �����¼�
		/// </summary>
		public DateTime dtCreateTime;

		/// <summary>
		/// tmis��
		/// </summary>
		public string nTMIS;

		/// <summary>
		/// tmis����
		/// </summary>
		public string strTMISName;

	}

    /// <summary>
    ///����: ZXYJInfoList
    ///˵��: ����Ԥ����Ϣ�б�
    /// </summary>
    public class ZXYJInfoList : List<ZXYJInfo>
    {
        public ZXYJInfoList()
        { }

    }  


}
