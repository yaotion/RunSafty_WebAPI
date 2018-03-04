using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using TF.RunSafty.TeacherXYRel.MD;

namespace TF.RunSafty.TeacherXYRel.DB
{
    #region 师徒关系查询条件类
    /// <summary>
    ///类名: XyQueryCondition
    ///说明: 师徒关系查询条件类
    /// </summary>
    public class XyQueryCondition
    {
        //工号
        public string strTrainmanNumber = "";
        // 姓名
        public string strTrainmanName = "";
        //简拼
        public string strJp = "";
        //车间ID
        public string strWorkShopGUID = "";
        public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
        {
            SqlCondition = new StringBuilder();
            SqlCondition.Append(strTrainmanNumber != "" ? " and (strTeacherNumber = @strTrainmanNumber or strStudentNumber = @strTrainmanNumber)" : "");
            SqlCondition.Append(strTrainmanName != "" ? " and (strTeacherName = @strTrainmanName or strStudentName = @strTrainmanName)" : "");
            SqlCondition.Append(strJp != "" ? " and (strTeacherJp = @strJp or strStudentJp = @strJp)" : "");
            SqlCondition.Append(strWorkShopGUID != "" ? " and (strWorkShopGUID = @strWorkShopGUID)" : "");
            SqlParameter[] sqlParams ={
                  new SqlParameter("strTrainmanNumber",strTrainmanNumber),
                  new SqlParameter("strTrainmanName",strTrainmanName),
                  new SqlParameter("strJp",strJp),
                  new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                      };
            Params = sqlParams;
        }
    }
    #endregion

    #region 师徒关系数据操作类
    /// <summary>                             
    ///类名: DBTeacherXYRel
    ///说明: 师徒关系数据操作类
    /// </summary> 
    public class DBTeacherXYRel
    {
        #region 获取所有的师徒关系
        /// <summary>
        /// 获得数据List
        /// </summary>
        public XYTeacherArray GetRelationsDataList(XyQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM VIEW_Base_XYRelation_Student where 1=1 " + strSqlOption.ToString() + " order by strTeacherGUID,dtCreateTime");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            XYTeacherArray list = new XYTeacherArray();
            foreach (DataRow dr in dt.Rows)
            {
                XYTeacher _Base_XYRelation_Teacher = new XYTeacher();
                RelationsDataRowToModel(_Base_XYRelation_Teacher, dr);
                list.Add(_Base_XYRelation_Teacher);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void RelationsDataRowToModel(XYTeacher model, DataRow dr)
        {
            model.strTeacherGUID = ObjectConvertClass.static_ext_string(dr["strTeacherGUID"]);
            model.strTeacherNumber = ObjectConvertClass.static_ext_string(dr["strTeacherNumber"]);
            model.strTeacherName = ObjectConvertClass.static_ext_string(dr["strTeacherName"]);
            //学员数组
            XYStudentArray listStudent = new XYStudentArray();
            XYStudent _XYStudent = new XYStudent();
            _XYStudent.strTeacherGUID = ObjectConvertClass.static_ext_string(dr["strTeacherGUID"]);
            _XYStudent.strStudentGUID = ObjectConvertClass.static_ext_string(dr["strStudentGUID"]);
            _XYStudent.strStudentNumber = ObjectConvertClass.static_ext_string(dr["strStudentNumber"]);
            _XYStudent.strStudentName = ObjectConvertClass.static_ext_string(dr["strStudentName"]);
            listStudent.Add(_XYStudent);
            model.StudentArray = listStudent;
        }
        #endregion

        #region 获取操作日志
        /// <summary>
        /// 获取操作日志数据List
        /// </summary>
        public XyOperateLogArray GetXyOperateLogList(DateTime dtBegin, DateTime dtEnd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_XYRelation_Log ");
            strSql.Append(" where dtCreateTime >= @dtBegin  and dtCreateTime <= @dtEnd order by dtCreateTime DESC");
            SqlParameter[] sqlParams = { 
                                           new SqlParameter("dtBegin",dtBegin),
                                           new SqlParameter("dtEnd",dtEnd)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            XyOperateLogArray list = new XyOperateLogArray();
            foreach (DataRow dr in dt.Rows)
            {
                XyOperateLog _Base_XYRelation_Log = new XyOperateLog();
                OperateLogDataRowToModel(_Base_XYRelation_Log, dr);
                list.Add(_Base_XYRelation_Log);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void OperateLogDataRowToModel(XyOperateLog model, DataRow dr)
        {
            model.RelationOP = ObjectConvertClass.static_ext_int(dr["nRelationOP"]);
            model.strTeacherGUID = ObjectConvertClass.static_ext_string(dr["strTeacherGUID"]);
            model.strTeacherNumber = ObjectConvertClass.static_ext_string(dr["strTeacherNumber"]);
            model.strTeacherName = ObjectConvertClass.static_ext_string(dr["strTeacherName"]);
            model.strStudentGUID = ObjectConvertClass.static_ext_string(dr["strStudentGUID"]);
            model.strStudentNumber = ObjectConvertClass.static_ext_string(dr["strStudentNumber"]);
            model.strStudentName = ObjectConvertClass.static_ext_string(dr["strStudentName"]);
            model.strDutyUserGUID = ObjectConvertClass.static_ext_string(dr["strDutyUserGUID"]);
            model.strDutyUserNumber = ObjectConvertClass.static_ext_string(dr["strDutyNumber"]);
            model.strDutyUserName = ObjectConvertClass.static_ext_string(dr["strDutyName"]);
            model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
        }


        #endregion

        #region 添加操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public int AddLog(XyOperateLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Base_XYRelation_Log");
            strSql.Append("(nRelationOP,strTeacherGUID,strStudentGUID,strDutyUserGUID)");
            strSql.Append("values(@nRelationOP,@strTeacherGUID,@strStudentGUID,@strDutyUserGUID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@nRelationOP", model.RelationOP),
          new SqlParameter("@strTeacherGUID", model.strTeacherGUID),
          new SqlParameter("@strStudentGUID", model.strStudentGUID),
          new SqlParameter("@strDutyUserGUID", model.strDutyUserGUID)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
        }
        #endregion

        #region 添加师傅
        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        public bool ExistTeacher(string strTeacherGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from TAB_Base_XYRelation_Teacher where strTeacherGUID=@strTeacherGUID");
            SqlParameter[] parameters = {
           new SqlParameter("strTeacherGUID",strTeacherGUID)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
        }
        /// <summary>
        /// 添加师傅
        /// </summary>
        public int AddTeacher(string strTeacherGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Base_XYRelation_Teacher (strTeacherGUID) values (@strTeacherGUID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strTeacherGUID", strTeacherGUID)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
        }
        #endregion

        #region 删除师傅
        /// <summary>
        /// 删除数据
        /// </summary>
        public bool DeleteTeacher(string strTeacherGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Base_XYRelation_Student where strTeacherGUID = @strTeacherGUID;");
            strSql.Append(" delete from TAB_Base_XYRelation_Teacher where strTeacherGUID = @strTeacherGUID");
            SqlParameter[] parameters = {
          new SqlParameter("strTeacherGUID",strTeacherGUID)};

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 添加学员
        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        public bool ExistStudent(string strStudentGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from TAB_Base_XYRelation_Student where strStudentGUID=@strStudentGUID");
            SqlParameter[] parameters = {
           new SqlParameter("strStudentGUID",strStudentGUID)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
        }
        /// <summary>
        /// 添加学员数据
        /// </summary>
        public bool AddStudent(string strTeacherGUID, string StudentGUID, string DutyUserGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Base_XYRelation_Student");
            strSql.Append("(strTeacherGUID,strStudentGUID)");
            strSql.Append("values(@strTeacherGUID,@strStudentGUID)");
            SqlParameter[] parameters = {
          new SqlParameter("@strTeacherGUID", strTeacherGUID),
          new SqlParameter("@strStudentGUID", StudentGUID)};

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 删除学员
        /// <summary>
        /// 删除数据
        /// </summary>
        public bool DeleteStudent(string strTeacherGUID, string strStudentGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Base_XYRelation_Student ");
            strSql.Append(" where strTeacherGUID = @strTeacherGUID and strStudentGUID =@strStudentGUID");
            SqlParameter[] parameters = {
                         new SqlParameter("strTeacherGUID",strTeacherGUID),
                         new SqlParameter("strStudentGUID",strStudentGUID)};

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 清除师徒关系
        public bool ClearXYRelations()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Base_XYRelation_Teacher; ");
            strSql.Append(" delete from TAB_Base_XYRelation_Student");
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString()) > 0;
        }
        #endregion

        #region 增加师徒
        public bool AddTeacherAndStudent(string strTeacherGUID, string strStudentGUID)
        {
            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(@"if not exists (select * from TAB_Base_XYRelation_Teacher where strTeacherGUID=@strTeacherGUID) 
             insert into TAB_Base_XYRelation_Teacher(strTeacherGUID) values(@strTeacherGUID)");
                    SqlParameter[] parameters = {
                          new SqlParameter("@strTeacherGUID", strTeacherGUID),
                          new SqlParameter("@strStudentGUID", strStudentGUID),
                                        };
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);

                    StringBuilder strSqlStudent = new StringBuilder();
                    strSqlStudent.Append(@"if not exists (select * from TAB_Base_XYRelation_Student where strTeacherGUID=@strTeacherGUID and strStudentGUID=@strStudentGUID)
             insert into TAB_Base_XYRelation_Student(strTeacherGUID,strStudentGUID) values(@strTeacherGUID,@strStudentGUID)");
                    var obj = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSqlStudent.ToString(), parameters);

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region 获取指定师傅的学员信息
        /// <summary>
        /// 获得数据List
        /// </summary>
        public XYStudentArray GetStudentsDataList(string strTeacherGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_XYRelation_Student where strTeacherGUID=@strTeacherGUID ");
            strSql.Append(" and (not (strStudentGUID is null)) and (strStudentGUID <> '')");
            SqlParameter[] parameters = {
                          new SqlParameter("@strTeacherGUID", strTeacherGUID)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters).Tables[0];
            XYStudentArray list = new XYStudentArray();
            foreach (DataRow dr in dt.Rows)
            {
                XYStudent _Base_XYRelation_Student = new XYStudent();
                StudentsDataRowToModel(_Base_XYRelation_Student, dr);
                list.Add(_Base_XYRelation_Student);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void StudentsDataRowToModel(XYStudent model, DataRow dr)
        {
            model.strTeacherGUID = ObjectConvertClass.static_ext_string(dr["strTeacherGUID"]);
            model.strStudentGUID = ObjectConvertClass.static_ext_string(dr["strStudentGUID"]);
            model.strStudentNumber = ObjectConvertClass.static_ext_string(dr["strStudentNumber"]);
            model.strStudentName = ObjectConvertClass.static_ext_string(dr["strStudentName"]);
        }
        #endregion

        #region 获取简单的司机信息
        /// <summary>
        /// 获得数据List
        /// </summary>
        public XYSimpleTrainmanArray GetSimpleTrainmanDataList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strTrainmanGUID,strTrainmanNumber,strTrainmanName from TAB_Org_Trainman");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            XYSimpleTrainmanArray list = new XYSimpleTrainmanArray();
            foreach (DataRow dr in dt.Rows)
            {
                XYSimpleTrainman _Org_Trainman = new XYSimpleTrainman();
                SimpleTrainmanDataRowToModel(_Org_Trainman, dr);
                list.Add(_Org_Trainman);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void SimpleTrainmanDataRowToModel(XYSimpleTrainman model, DataRow dr)
        {
            model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
            model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
        }
        #endregion
    }
    #endregion
}
