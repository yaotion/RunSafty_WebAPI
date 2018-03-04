using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Api.Entity;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.Api.BLL
{
    public class RunRecord_FileEntityBLL
    {

        #region
        /// <summary>
        /// 添加运行记录文件
        /// </summary>
        /// <param name="runRecordFileEntity">运行记录实体</param>
        /// <param name="exceptInfo">异常信息</param>
        /// <returns></returns>
        public static bool Add(RunRecord_FileEntity runRecordFileEntity, out string exceptInfo)
        {
            exceptInfo = string.Empty;
            foreach (RunRecord_File_FileListEntity runRecordFileListItemEntity in runRecordFileEntity.FileList)
            {
                string strSql = "INSERT INTO TAB_RunRecord_FileEntity "
                                     + "(strWorkID, nWorkType, strSiteGUID, strRunRecordGUID) "
                                     + "VALUES (@strWorkID,@nWorkType,@strSiteGUID,@strRunRecordGUID)";

                SqlParameter[] sqlParams = {
                                                    new SqlParameter("strWorkID",runRecordFileEntity.workid),
                                                    new SqlParameter("nWorkType",runRecordFileEntity.worktype),
                                                    new SqlParameter("strSiteGUID",runRecordFileEntity.sid),
                                                    new SqlParameter("strRunRecordGUID",runRecordFileListItemEntity.fid)
                                                 };
                try
                {
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                }
                catch (Exception ex)
                {
                    exceptInfo = ex.ToString();
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
