using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.BeginworkFlow
{
    public class RunRecordFileMain
    {
        public RunRecordFileMain()
        { }

        /// <summary>
        /// 
        /// </summary>
        public int nID;

        /// <summary>
        /// 转储时间
        /// </summary>
        public DateTime? dtUploadTime;

        /// <summary>
        /// 出勤计划GUID
        /// </summary>
        public string strPlanGUID;

        /// <summary>
        /// 计划出勤时间
        /// </summary>
        public DateTime? dtPlanChuQinTime;

        /// <summary>
        /// 车次
        /// </summary>
        public string strTrainNo;

        /// <summary>
        /// 车号
        /// </summary>
        public string strTrainNum;

        /// <summary>
        /// 车型
        /// </summary>
        public string strTrainTypeName;

        /// <summary>
        /// 司机1工号
        /// </summary>
        public string strTrainmanNumber1;

        /// <summary>
        /// 司机1姓名
        /// </summary>
        public string strTrainmanName1;

        /// <summary>
        /// 司机2工号
        /// </summary>
        public string strTrainmanNumber2;

        /// <summary>
        /// 司机2姓名
        /// </summary>
        public string strTrainmanName2;

        /// <summary>
        /// 司机3工号
        /// </summary>
        public string strTrainmanNumber3;

        /// <summary>
        /// 司机3姓名
        /// </summary>
        public string strTrainmanName3;

        /// <summary>
        /// 司机4工号
        /// </summary>
        public string strTrainmanNumber4;

        /// <summary>
        /// 司机4姓名
        /// </summary>
        public string strTrainmanName4;

        /// <summary>
        /// IC卡号
        /// </summary>
        public string strCardNumber;

        /// <summary>
        /// 客户端编号
        /// </summary>
        public string strSiteNumber;


        /// <summary>
        /// 客户端名称
        /// </summary>
        public string strSiteName;

        /// <summary>
        /// 运行记录文件列表
        /// </summary>
        public List<RunRecordFileDetail> RunRecordFileDetailList = new List<RunRecordFileDetail>();
    }
}
