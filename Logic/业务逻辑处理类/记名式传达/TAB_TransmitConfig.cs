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
namespace ThinkFreely.RunSafty
{

    public class Model_TransmitConfig
    {
        #region 属性
        public int nId { get; set; }
        public string strWorkShopGUID { get; set; }
        public int nPost { get; set; }
        #endregion
    }
    /// <summary>
    ///TAB_TransmitConfig 的摘要说明
    ///记名式传达配置 数据库操作类
    /// </summary>
    public class TAB_TransmitConfig
    {
        #region 属性
        #endregion
        public TAB_TransmitConfig()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region 增删改查
        public bool AddTransmitConfig(Model_TransmitConfig model)
        {
            string newGUID = Guid.NewGuid().ToString();
            string strSql = @"insert into [TAB_System_TransmitConfig] ([strWorkShopGUID],[nPost]) values (@strWorkShopGUID,@nPost)";
            SqlParameter[] sqlParams ={ new SqlParameter("strWorkShopGUID",model.strWorkShopGUID),
                                        new SqlParameter("nPost",model.nPost)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public DataTable GetTransmitConfigByWorkShopGUID(string strWorkShopGUID)
        {
            string Sql = "select * from  TAB_System_TransmitConfig where strWorkShopGUID=@strWorkShopGUID";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];
        }
        public bool UpdateTransmitConfig(Model_TransmitConfig model)
        {
            string newGUID = Guid.NewGuid().ToString();
            string strSql = @"update TAB_System_TransmitConfig set strWorkShopGUID=@strWorkShopGUID,nPost=@nPost where nid=@nid";
            SqlParameter[] sqlParams ={new SqlParameter("strWorkShopGUID",SqlDbType.VarChar,50,model.strWorkShopGUID),
                                       new SqlParameter("@nPost",model.nPost),
                                       new SqlParameter("nid",model.nId)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool DeleteTransmitConfig(string WorkShopGUID)
        {
            string newGUID = Guid.NewGuid().ToString();
            string strSql = @"delete from TAB_System_TransmitConfig where strWorkShopGUID=@strWorkShopGUID";
            SqlParameter[] sqlParams ={
                                                    new SqlParameter("@strWorkShopGUID",SqlDbType.VarChar,50,WorkShopGUID)
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion
    }
}
