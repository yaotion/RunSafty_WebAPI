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
    public class lsDicUserInfo
    {
        #region 属性
        public int Id;
        public int UnitId;
        public int DepartmentId;
        public int GroupsId;
        public int ApanageId;
        public string UserNum;
        public string NickName;
        public string UserName;
        public string UserPwd;
        public int AuthorityId;
        public string Status;
        public string Sex;
        public string ZhiWu;
        public string CreateUser;
        public DateTime? CreateDate;
        public string strPictureFileName;
        public string strBanCi;
        #endregion 属性

        #region 构造函数
        public lsDicUserInfo()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDicUserInfo(string id)
        {
            string strSql = "select * from lsDicUserInfo where UserNum=@Id and Status=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("Id",id)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Id = PageBase.static_ext_int(dt.Rows[0]["Id"].ToString());
                UnitId = PageBase.static_ext_int(dt.Rows[0]["UnitId"].ToString());
                DepartmentId = PageBase.static_ext_int(dt.Rows[0]["DepartmentId"].ToString());
                GroupsId = PageBase.static_ext_int(dt.Rows[0]["GroupsId"].ToString());
                ApanageId = PageBase.static_ext_int(dt.Rows[0]["ApanageId"].ToString());
                UserNum = dt.Rows[0]["UserNum"].ToString();
                NickName = dt.Rows[0]["NickName"].ToString();
                UserName = dt.Rows[0]["UserName"].ToString();
                UserPwd = dt.Rows[0]["UserPwd"].ToString();
                AuthorityId = PageBase.static_ext_int(dt.Rows[0]["AuthorityId"].ToString());
                Status = dt.Rows[0]["Status"].ToString();
                Sex = dt.Rows[0]["Sex"].ToString();
                ZhiWu = dt.Rows[0]["ZhiWu"].ToString();
                CreateUser = dt.Rows[0]["CreateUser"].ToString();
                CreateDate = PageBase.static_ext_date(dt.Rows[0]["CreateDate"].ToString());
                strPictureFileName = dt.Rows[0]["strPictureFileName"].ToString();
                strBanCi = dt.Rows[0]["strBanCi"].ToString();
            }
        }
        #endregion 构造函数
        #region 扩展方法

      /// <summary>
        /// 检查工号是否存在
      /// </summary>
      /// <param name="nApanageID"></param>
      /// <param name="nUnitID"></param>
      /// <param name="Number"></param>
      /// <returns></returns>
        public static bool CheckNumberExist(int nApanageID,int nUnitID,string Number)
        {
            string strSql = "select count(*) from lsDicUserInfo where UserNum=@UserNum and ApanageId=@nApanageID and Status=1";
            strSql += nUnitID == 0 ? "" : " and nUnitID = @nUnitID ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("UserNum",Number),
                                           new SqlParameter("nApanageID",nApanageID),
                                           new SqlParameter("nUnitID",nUnitID)
                                       };
            return PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams))>0;
        }

     
        #endregion
    }
}
