using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkSteps
{
    public class MDRunRecordFileDetail
    {
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
}
