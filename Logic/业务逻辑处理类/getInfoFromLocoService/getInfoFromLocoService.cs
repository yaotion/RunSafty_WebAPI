using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.Logic
{
    public class getInfoFromLocoService
    {
        #region 扩展方法
        /// <summary>
        /// 获取所有单位信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static DataTable GetAllUnit(string Id)
        {
            string strSql = "select * from lsDicUnitInfo where 1=1 and unittype='段'";
            if (Id != "")
            {
                strSql += " and Id = @Id ";
            }
            strSql += " order by Id ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("Id",Id)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static DataTable GetAllDepartment(int UnitId,string id)
        {
            string strSql = "select * from lsDicDepartmentInfo where 1=1 and status=1";
            strSql += UnitId == 0 ? "" : " and UnitId = @UnitId";
            strSql += id == "" ? "" : " and Id = @Id";
            strSql += " order by Id ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("UnitId",UnitId),
                                            new SqlParameter("Id",id)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 根据整备场id更新整备场图名
        /// </summary>
        /// <param name="ZbcId"></param>
        /// <returns></returns>
        public static bool UpdateDepartmentStrPhotoName(int ZbcId, string StrPhotoName)
        {
            string strSql = "update lsDicDepartmentInfo set StrPhotoName = @StrPhotoName where Id=@Id";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("Id",ZbcId),
                                           new SqlParameter("StrPhotoName",StrPhotoName)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 获取所有班组
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static DataTable GetAllGroups(string UnitId,string bmid, string id)
        {
            string strSql = "select * from lsDicGroupsInfo where 1=1";
            strSql += bmid == "" ? "" : " and DepartmentId = @bmid";
            strSql += UnitId == "" ? "" : " and UnitId = @UnitId";
            strSql += id == "" ? "" : " and Id = @Id";
            strSql += " order by Id ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("UnitId",UnitId),
                                           new SqlParameter("bmid",bmid),
                                            new SqlParameter("Id",id)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        #endregion
    }
}
