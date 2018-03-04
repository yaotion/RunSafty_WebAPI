using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using TF.RunSafty.TeacherXYRel.MD;
using TF.RunSafty.TeacherXYRel.DB;

namespace TF.RunSafty.TeacherXYRel
{
    #region 师徒关系接口类
    /// <summary>
    ///类名: LCTeacherXYRel
    ///说明: 师徒关系接口类
    /// </summary>
    public class LCTeacherXYRel
    {
        #region 获取所有的师徒关系
        public class InGetRelations
        {
            //查询条件
            public XyQueryCondition param = new XyQueryCondition();
        }

        public class OutGetRelations
        {
            //老师数组
            public XYTeacherArray teacherArray = new XYTeacherArray();
        }

        /// <summary>
        /// 1.13.1    获取所有的师徒关系
        /// </summary>
        public InterfaceOutPut GetRelations(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetRelations InParams = JsonConvert.DeserializeObject<InGetRelations>(Data);
                OutGetRelations OutParams = new OutGetRelations();
                DBTeacherXYRel db = new DBTeacherXYRel();
                OutParams.teacherArray = db.GetRelationsDataList(InParams.param);
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取操作日志
        public class InGetLogs
        {
            //开始时间
            public DateTime dtBegin;
            //结束时间
            public DateTime dtEnd;
        }

        public class OutGetLogs
        {
            //日志数组
            public XyOperateLogArray logArray = new XyOperateLogArray();
        }

        /// <summary>
        /// 1.13.2    获取操作日志
        /// </summary>
        public InterfaceOutPut GetLogs(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetLogs InParams = JsonConvert.DeserializeObject<InGetLogs>(Data);
                OutGetLogs OutParams = new OutGetLogs();
                DBTeacherXYRel db = new DBTeacherXYRel();
                OutParams.logArray = db.GetXyOperateLogList(InParams.dtBegin, InParams.dtEnd);
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 添加师傅
        public class InAddTeacher
        {
            //值班员ID
            public string DutyUserGUID;
            //老师ID
            public string TeacherGUID;
        }

        /// <summary>
        /// 1.13.3    添加师傅
        /// </summary>
        public InterfaceOutPut AddTeacher(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InAddTeacher InParams = JsonConvert.DeserializeObject<InAddTeacher>(Data);
                DBTeacherXYRel db = new DBTeacherXYRel();
                if (!db.ExistTeacher(InParams.TeacherGUID))
                {
                    db.AddTeacher(InParams.TeacherGUID);

                    //添加操作日志
                    XyOperateLog OperateLog = new XyOperateLog();
                    OperateLog.RelationOP = 0;//添加师傅
                    OperateLog.strTeacherGUID = InParams.TeacherGUID;
                    OperateLog.strDutyUserGUID = InParams.DutyUserGUID;
                    db.AddLog(OperateLog);

                    output.result = 0;
                    output.resultStr = "添加成功";

                }
                else
                {
                    output.resultStr = "数据已存在";
                }
            }
            catch (Exception ex)
            {
                output.resultStr = "添加失败：" + ex.Message;

                throw ex;
            }
            return output;
        }
        #endregion

        #region 删除师傅
        public class InDeleteTeacher
        {
            //值班员ID
            public string DutyUserGUID;
            //老师ID
            public string TeacherGUID;
        }

        /// <summary>
        /// 1.13.4    删除师傅
        /// </summary>
        public InterfaceOutPut DeleteTeacher(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InDeleteTeacher InParams = JsonConvert.DeserializeObject<InDeleteTeacher>(Data);
                DBTeacherXYRel db = new DBTeacherXYRel();
                db.DeleteTeacher(InParams.TeacherGUID);

                //添加操作日志
                XyOperateLog OperateLog = new XyOperateLog();
                OperateLog.RelationOP = 1;//删除师傅
                OperateLog.strTeacherGUID = InParams.TeacherGUID;
                OperateLog.strDutyUserGUID = InParams.DutyUserGUID;
                db.AddLog(OperateLog);

                output.result = 0;
                output.resultStr = "删除成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "删除失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 添加学员
        public class InAddStudent
        {
            //值班员ID
            public string DutyUserGUID;
            //老师ID
            public string TeacherGUID;
            //学员ID
            public string StudentGUID;
        }

        /// <summary>
        /// 1.13.5    添加学员
        /// </summary>
        public InterfaceOutPut AddStudent(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InAddStudent InParams = JsonConvert.DeserializeObject<InAddStudent>(Data);
                DBTeacherXYRel db = new DBTeacherXYRel();
                if (!db.ExistStudent(InParams.StudentGUID))
                {
                    db.AddStudent(InParams.TeacherGUID, InParams.StudentGUID, InParams.DutyUserGUID);

                    //添加操作日志
                    XyOperateLog OperateLog = new XyOperateLog();
                    OperateLog.RelationOP = 2;//添加学员
                    OperateLog.strTeacherGUID = InParams.TeacherGUID;
                    OperateLog.strDutyUserGUID = InParams.DutyUserGUID;
                    OperateLog.strStudentGUID = InParams.StudentGUID;
                    db.AddLog(OperateLog);

                    output.result = 0;
                    output.resultStr = "添加成功";
                }
                else
                {
                    output.resultStr = "数据已存在";
                }
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 删除学员
        public class InDeleteStudent
        {
            //值班员ID
            public string DutyUserGUID;
            //老师ID
            public string TeacherGUID;
            //学员ID
            public string StudentGUID;
        }

        /// <summary>
        /// 1.13.6    删除学员
        /// </summary>
        public InterfaceOutPut DeleteStudent(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InDeleteStudent InParams = JsonConvert.DeserializeObject<InDeleteStudent>(Data);
                DBTeacherXYRel db = new DBTeacherXYRel();
                db.DeleteStudent(InParams.TeacherGUID, InParams.StudentGUID);

                //添加操作日志
                XyOperateLog OperateLog = new XyOperateLog();
                OperateLog.RelationOP = 3;//删除学员
                OperateLog.strTeacherGUID = InParams.TeacherGUID;
                OperateLog.strDutyUserGUID = InParams.DutyUserGUID;
                OperateLog.strStudentGUID = InParams.StudentGUID;
                db.AddLog(OperateLog);

                output.result = 0;
                output.resultStr = "删除成功";
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 清除师徒关系
        /// <summary>
        /// 1.13.7    清除师徒关系
        /// </summary>
        public InterfaceOutPut ClearXYRelations(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                DBTeacherXYRel db = new DBTeacherXYRel();
                db.ClearXYRelations();
                output.result = 0;
                output.resultStr = "清除成功";
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 增加师徒
        public class InAddTeacherAndStudent
        {
            //师傅ID
            public string TeacherGUID;
            //学员ID
            public string StudentGUID;
        }

        /// <summary>
        /// 1.13.8    增加师徒
        /// </summary>
        public InterfaceOutPut AddTeacherAndStudent(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InAddTeacherAndStudent InParams = JsonConvert.DeserializeObject<InAddTeacherAndStudent>(Data);
                DBTeacherXYRel db = new DBTeacherXYRel();
                db.AddTeacherAndStudent(InParams.TeacherGUID, InParams.StudentGUID);

                output.result = 0;
                output.resultStr = "增加成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "增加失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取指定师傅的学员信息
        public class InGetStudents
        {
            //师傅ID
            public string TeacherGUID;
        }

        public class OutGetStudents
        {
            //学员数组
            public XYStudentArray XYArray = new XYStudentArray();
        }

        /// <summary>
        /// 1.13.9    获取指定师傅的学员信息
        /// </summary>
        public InterfaceOutPut GetStudents(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetStudents InParams = JsonConvert.DeserializeObject<InGetStudents>(Data);
                OutGetStudents OutParams = new OutGetStudents();
                DBTeacherXYRel db = new DBTeacherXYRel();
                OutParams.XYArray = db.GetStudentsDataList(InParams.TeacherGUID);
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取简单的司机信息
        public class OutGetSimpleTrainmans
        {
            //人员信息数组
            public XYSimpleTrainmanArray trainmanArray = new XYSimpleTrainmanArray();
        }

        /// <summary>
        /// 1.13.10    获取简单的司机信息
        /// </summary>
        public InterfaceOutPut GetSimpleTrainmans(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                OutGetSimpleTrainmans OutParams = new OutGetSimpleTrainmans();
                output.data = OutParams;
                output.result = 0;
                DBTeacherXYRel db = new DBTeacherXYRel();
                OutParams.trainmanArray = db.GetSimpleTrainmanDataList();
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion
    }
    #endregion
}
