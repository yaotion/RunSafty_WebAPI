using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace TF.RunSafty.Plan.MD
{
    /// <summary>
    ///类名: RunRecordFileDetail
    ///说明: 运行记录转储文件明细
    /// </summary>
    public class RunRecordFileDetail
    {
        public RunRecordFileDetail()
        { }

        /// <summary>
        /// 
        /// </summary>
        public int nID;

        /// <summary>
        /// 出勤计划GUID
        /// </summary>
        public string strPlanGUID;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string strFileName;

        /// <summary>
        /// 机车号
        /// </summary>
        public string strTrainNum;

        /// <summary>
        /// 司机工号
        /// </summary>
        public string strTrainmanNumber;

        /// <summary>
        /// 车次
        /// </summary>
        public string strTrainNo;

        /// <summary>
        /// 文件大小
        /// </summary>
        public int nFileSize;

        /// <summary>
        /// 文件时间
        /// </summary>
        public DateTime? dtFileTime;

        /// <summary>
        /// 主记录GUID
        /// </summary>
        public string strRecordGUID;

    }


    /// <summary>
    ///类名: RunRecordFileDetailList
    ///说明: 运行记录转储文件列表
    /// </summary>
    public class RunRecordFileDetailList : List<RunRecordFileDetail>
    {
        public RunRecordFileDetailList()
        { }

    }


    /// <summary>
    ///类名: RunRecordFileMain
    ///说明: 运行记录转储文件主信息
    /// </summary>
    public class RunRecordFileMain
    {
        public RunRecordFileMain()
        { }

        /// <summary>
        /// 
        /// </summary>
        public int nID;

        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;




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

    /// <summary>
    ///类名: RunRecordFileMainList
    ///说明: 运行记录转储列表
    /// </summary>
    public class RunRecordFileMainList : List<RunRecordFileMain>
    {
        public RunRecordFileMainList()
        { }

    }


}