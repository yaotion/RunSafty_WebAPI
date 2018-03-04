using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.TeacherXYRel.MD
{
    #region 学员信息
    ///类名: XYStudent
    ///说明: 学员信息
    /// </summary>
    public class XYStudent
    {
        public XYStudent()
        { }

        /// <summary>
        /// 师傅ID
        /// </summary>
        public string strTeacherGUID;

        /// <summary>
        /// 学员ID
        /// </summary>
        public string strStudentGUID;

        /// <summary>
        /// 学员工号
        /// </summary>
        public string strStudentNumber;

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string strStudentName;

    }

    /// <summary>
    ///类名: XYStudentArray
    ///说明: 学员信息数组
    /// </summary>
    public class XYStudentArray : List<XYStudent>
    {
        public XYStudentArray()
        { }

    }
    #endregion

    #region 老师信息
    /// <summary>
    ///类名: XYTeacher
    ///说明: 老师信息
    /// </summary>
    public class XYTeacher
    {
        public XYTeacher()
        { }

        /// <summary>
        /// 人员GUID
        /// </summary>
        public string strTeacherGUID;

        /// <summary>
        /// 人员工号
        /// </summary>
        public string strTeacherNumber;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string strTeacherName;

        /// <summary>
        /// 学员数组
        /// </summary>
        public XYStudentArray StudentArray;

    }
    /// <summary>
    ///类名: XYTeacherArray
    ///说明: 老师数组
    /// </summary>
    public class XYTeacherArray : List<XYTeacher>
    {
        public XYTeacherArray()
        { }
    }
    #endregion

    #region 操作日志
    /// <summary>
    ///类名: XyOperateLog
    ///说明: 操作日志
    /// </summary>
    public class XyOperateLog
    {
        public XyOperateLog()
        { }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? dtCreateTime;

        /// <summary>
        /// 操作类型
        /// </summary>
        public int RelationOP;

        /// <summary>
        /// 师傅ID
        /// </summary>
        public string strTeacherGUID = string.Empty;

        /// <summary>
        /// 师傅工号
        /// </summary>
        public string strTeacherNumber = string.Empty;

        /// <summary>
        /// 师傅姓名
        /// </summary>
        public string strTeacherName = string.Empty;

        /// <summary>
        /// 学员ID
        /// </summary>
        public string strStudentGUID = string.Empty;

        /// <summary>
        /// 学员工号
        /// </summary>
        public string strStudentNumber = string.Empty;

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string strStudentName = string.Empty;

        /// <summary>
        /// 值班员ID
        /// </summary>
        public string strDutyUserGUID = string.Empty;

        /// <summary>
        /// 值班员工号
        /// </summary>
        public string strDutyUserNumber = string.Empty;

        /// <summary>
        /// 值班员姓名
        /// </summary>
        public string strDutyUserName = string.Empty;

    }
    /// <summary>
    ///类名: XyOperateLogArray
    ///说明: 操作日志数组
    /// </summary>
    public class XyOperateLogArray : List<XyOperateLog>
    {
        public XyOperateLogArray()
        { }
    }
    #endregion

    #region 司机简要信息
    /// <summary>
    ///类名: XYSimpleTrainman
    ///说明: 司机简要信息
    /// </summary>
    public class XYSimpleTrainman
    {
        public XYSimpleTrainman()
        { }

        /// <summary>
        /// 人员ID
        /// </summary>
        public string strTrainmanGUID;

        /// <summary>
        /// 人员工号
        /// </summary>
        public string strTrainmanNumber;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string strTrainmanName;
    }
    /// <summary>
    ///类名: XYSimpleTrainmanArray
    ///说明: 司机简要信息数组
    /// </summary>
    public class XYSimpleTrainmanArray : List<XYSimpleTrainman>
    {
        public XYSimpleTrainmanArray()
        { }
    }
    #endregion
}
