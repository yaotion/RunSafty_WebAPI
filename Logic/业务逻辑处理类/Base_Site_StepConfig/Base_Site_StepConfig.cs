using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.Logic
{
    /// <summary>
    /// 类名：Base_Site_StepConfig数据库操作类
    /// 描述：Base_Site_StepConfig
    /// </summary>
    public class Base_Site_StepConfig
    {
        #region 属性
        public string strSiteGUID;
        public byte[] StepConfig;
        public string ConfigName;
        #endregion

        #region 构造函数
        public Base_Site_StepConfig()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public Base_Site_StepConfig(string strSiteGUID)
        {
            string strSql = "select * from [TAB_Base_Site_StepConfig] where strSiteGUID=@strSiteGUID";
            SqlParameter sqlParam = new SqlParameter("@strSiteGUID", strSiteGUID);
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam).Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.strSiteGUID = PageBase.static_ext_string(dt.Rows[0]["strSiteGUID"]);
                this.ConfigName = PageBase.static_ext_string(dt.Rows[0]["ConfigName"]);
                this.StepConfig = (byte[])dt.Rows[0]["StepConfig"];
            }
        }
        #endregion


        #region 扩展方法

        #region 插入或更新记录
        /// <summary>
        /// 插入或更新记录
        /// </summary>
        public static bool InsertOrUpdate(Base_Site_StepConfig bss)
        {
            string strSql = "if (Exists(select * from [TAB_Base_Site_StepConfig] where strSiteGUID=@strSiteGUID)) begin update [TAB_Base_Site_StepConfig] set StepConfig=@StepConfig,ConfigName=@ConfigName where strSiteGUID=@strSiteGUID end else begin insert into [TAB_Base_Site_StepConfig] ([strSiteGUID],[StepConfig],[ConfigName]) values (@strSiteGUID,@StepConfig,@ConfigName) end;";
            SqlParameter[] sqlParams = { 
                                                        new SqlParameter("@strSiteGUID",bss.strSiteGUID),
                                                        new SqlParameter("@StepConfig",bss.StepConfig),
                                                        new SqlParameter("@ConfigName",bss.ConfigName)
                                   };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion
        #endregion

        #region 增删改
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            string strSql = "insert into [TAB_Base_Site_StepConfig] ([strSiteGUID],[StepConfig],[ConfigName]) values (@strSiteGUID,@StepConfig,@ConfigName)";
            SqlParameter[] sqlParams = { 
                                                        new SqlParameter("@strSiteGUID",strSiteGUID),
                                                        new SqlParameter("@StepConfig",StepConfig),
                                                        new SqlParameter("@ConfigName",ConfigName)
                                   };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = "update [TAB_Base_Site_StepConfig] SET [StepConfig]=@StepConfig,[ConfigName] =@ConfigName where [strSiteGUID]=@strSiteGUID";
            SqlParameter[] sqlParams = { 
                                                        new SqlParameter("@strSiteGUID",strSiteGUID),
                                                        new SqlParameter("@StepConfig",StepConfig),
                                                        new SqlParameter("@ConfigName",ConfigName)
                                   };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = "delete from [TAB_Base_Site_StepConfig] where [strSiteGUID]=@strSiteGUID";
            SqlParameter[] sqlParams = { 
                                                        new SqlParameter("@strSiteGUID",strSiteGUID)
                                   };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        #endregion

    }
}