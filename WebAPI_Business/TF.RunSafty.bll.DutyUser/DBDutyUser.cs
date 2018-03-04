using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using TF.RunSafty.DutyUser.MD;

namespace TF.RunSafty.DutyUser.DB
{
    #region 值班员数据操作类
    public class DBDutyUser
    {
        #region 根据用户名获取登录人员信息
        /// <summary>
        /// 根据用户名获取登录人员信息
        /// </summary>
        /// <param name="strDutyNumber">工号</param>
        /// <param name="bExist">返回参数，是否存在</param>
        /// <returns></returns>
        public RsDutyUser GetDutyUserByNumber(string strDutyNumber, out Boolean bExist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_Org_DutyUser where strDutyNumber = @strDutyNumber");
            SqlParameter[] sqlParams = { new SqlParameter("strDutyNumber", strDutyNumber) };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            RsDutyUser _Org_DutyUser = null;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
                _Org_DutyUser = new RsDutyUser();
                DutyUserDataRowToModel(_Org_DutyUser, dt.Rows[0]);
            }
            else
            {
                bExist = false;
            }
            return _Org_DutyUser;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void DutyUserDataRowToModel(RsDutyUser model, DataRow dr)
        {
            model.strDutyGUID = ObjectConvertClass.static_ext_string(dr["strDutyGUID"]);
            model.strDutyNumber = ObjectConvertClass.static_ext_string(dr["strDutyNumber"]);
            model.strDutyName = ObjectConvertClass.static_ext_string(dr["strDutyName"]);
            model.strPassword = ObjectConvertClass.static_ext_string(dr["strPassword"]);
        }
        #endregion

        public DataTable GetDutyUserList()
        {
            string sql = "select * from TAB_Org_DutyUser";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
            return dt;
        }

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserNumber">工号</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        public bool ResetPassword(string UserNumber, string NewPassword)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_Org_DutyUser set ");
            strSql.Append(" strPassword = @strPassword ");
            strSql.Append(" where strDutyNumber = @strDutyNumber ");

            SqlParameter[] parameters = {
                  new SqlParameter("@strDutyNumber", UserNumber),
                  new SqlParameter("@strPassword", NewPassword)
                                        };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion
    }
    #endregion
}
