using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ThinkFreely.DBUtility;
namespace TF.RunSafty.DAL
{
    //TAB_NoticeFile
    public partial class TAB_NoticeFile
    {

        public bool Exists(int nid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TAB_NoticeFile");
            strSql.Append(" where ");
            strSql.Append(" nid = @nid  ");
            SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
            parameters[0].Value = nid;
            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
            //return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TF.RunSafty.Model.TAB_NoticeFile model)
        { 
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_NoticeFile(");
			strSql.Append("strFileGUID,strWorkShopGUID,StrTypeGUID,strFileName,strFilePath,dtBeginTime,dtEndTime)");
			strSql.Append(" values (");
			strSql.Append("@strFileGUID,@strWorkShopGUID,@StrTypeGUID,@strFileName,@strFilePath,@dtBeginTime,@dtEndTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@strFileGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strWorkShopGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrTypeGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strFileName", SqlDbType.VarChar,50),
					new SqlParameter("@strFilePath", SqlDbType.VarChar,100),
					new SqlParameter("@dtBeginTime", SqlDbType.VarChar,50),
					new SqlParameter("@dtEndTime", SqlDbType.VarChar,50)};
			parameters[0].Value = model.strFileGUID;
			parameters[1].Value = model.strWorkShopGUID;
			parameters[2].Value = model.StrTypeGUID;
			parameters[3].Value = model.strFileName;
			parameters[4].Value = model.strFilePath;
			parameters[5].Value = model.dtBeginTime;
			parameters[6].Value = model.dtEndTime;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TF.RunSafty.Model.TAB_NoticeFile model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_NoticeFile set ");
            strSql.Append("strFileGUID=@strFileGUID,");
            strSql.Append("strWorkShopGUID=@strWorkShopGUID,");
            strSql.Append("StrTypeGUID=@StrTypeGUID,");
            strSql.Append("strFileName=@strFileName,");
            strSql.Append("strFilePath=@strFilePath,");
            strSql.Append("dtBeginTime=@dtBeginTime,");
            strSql.Append("dtEndTime=@dtEndTime");
            strSql.Append(" where nid=@nid");
            SqlParameter[] parameters = {
					new SqlParameter("@strFileGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strWorkShopGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrTypeGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strFileName", SqlDbType.VarChar,50),
					new SqlParameter("@strFilePath", SqlDbType.VarChar,100),
					new SqlParameter("@dtBeginTime", SqlDbType.VarChar,50),
					new SqlParameter("@dtEndTime", SqlDbType.VarChar,50),
					new SqlParameter("@nid", SqlDbType.Int,4)};
            parameters[0].Value = model.strFileGUID;
            parameters[1].Value = model.strWorkShopGUID;
            parameters[2].Value = model.StrTypeGUID;
            parameters[3].Value = model.strFileName;
            parameters[4].Value = model.strFilePath;
            parameters[5].Value = model.dtBeginTime;
            parameters[6].Value = model.dtEndTime;
            parameters[7].Value = model.nid;
            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int nid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_NoticeFile ");
            strSql.Append(" where nid=@nid");
            SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
            parameters[0].Value = nid;


            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string nidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_NoticeFile ");
            strSql.Append(" where ID in (" + nidlist + ")  ");
            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.TAB_NoticeFile GetModel(int nid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nid, strFileGUID, strWorkShopGUID, StrTypeGUID, strFileName,strFilePath, dtBeginTime, dtEndTime  ");
            strSql.Append("  from TAB_NoticeFile ");
            strSql.Append(" where nid=@nid");
            SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
            parameters[0].Value = nid;


            TF.RunSafty.Model.TAB_NoticeFile model = new TF.RunSafty.Model.TAB_NoticeFile();
            DataSet  ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["nid"].ToString() != "")
                {
                    model.nid = int.Parse(ds.Tables[0].Rows[0]["nid"].ToString());
                }
                model.strFileGUID = ds.Tables[0].Rows[0]["strFileGUID"].ToString();
                model.strWorkShopGUID = ds.Tables[0].Rows[0]["strWorkShopGUID"].ToString();
                model.StrTypeGUID = ds.Tables[0].Rows[0]["StrTypeGUID"].ToString();
                model.strFileName = ds.Tables[0].Rows[0]["strFileName"].ToString();
                model.dtBeginTime = ds.Tables[0].Rows[0]["dtBeginTime"].ToString();
                model.dtEndTime = ds.Tables[0].Rows[0]["dtEndTime"].ToString();
                model.strFilePath = ds.Tables[0].Rows[0]["strFilePath"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM TAB_NoticeFile ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM TAB_NoticeFile ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }


    }
}

