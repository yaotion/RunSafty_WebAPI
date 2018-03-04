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
    /// <summary>
    ///TrainmanJiaolu 的摘要说明
    /// </summary>
    public class TrainmanJiaolu
    {
        #region 属性
        public string strTrainmanJiaoluGUID = "";
        public string strTrainmanJiaoluName = "";
        public string strLineGUID = "";
        public int nJiaoluType = 0;
        public int nDeleteState = 0;
        public int nTrainmanTypeID = 0;
        public string strTrainJiaoluGUID = "";
        public int nKehuoID = 0;
        public int nDragTypeID = 0;
        public int nTrainmanRunType = 0;
        #endregion 属性

        #region 构造函数
        public TrainmanJiaolu()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public TrainmanJiaolu(string JiaoluGUID)
        {
            string strSql = "select * from TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainmanJiaoluGUID",JiaoluGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strTrainmanJiaoluGUID = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                strTrainmanJiaoluName = dt.Rows[0]["strTrainmanJiaoluName"].ToString();
                strLineGUID = dt.Rows[0]["strLineGUID"].ToString();
                nJiaoluType = int.Parse(dt.Rows[0]["nJiaoluType"].ToString());
                nTrainmanTypeID = int.Parse(dt.Rows[0]["nTrainmanTypeID"].ToString());
                strTrainJiaoluGUID = dt.Rows[0]["strTrainJiaoluGUID"].ToString();
                nKehuoID = int.Parse(dt.Rows[0]["nKehuoID"].ToString());
                nDragTypeID = int.Parse(dt.Rows[0]["nDragTypeID"].ToString());
                nTrainmanRunType = int.Parse(dt.Rows[0]["nTrainmanRunType"].ToString());
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_TrainmanJiaolu (strTrainmanJiaoluGUID,strTrainmanJiaoluName,strLineGUID,nJiaoluType,nTrainmanTypeID,strTrainJiaoluGUID,nKehuoID,nDragTypeID,nTrainmanRunType) " +
                    " values (@strTrainmanJiaoluGUID,@strTrainmanJiaoluName,@strLineGUID,@nJiaoluType,@nTrainmanTypeID,@strTrainJiaoluGUID,@nKehuoID,@nDragTypeID,@nTrainmanRunType)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainmanJiaoluGUID",guid),
                                           new SqlParameter("strTrainmanJiaoluName",strTrainmanJiaoluName),
                                           new SqlParameter("strLineGUID",strLineGUID),
                                           new SqlParameter("nJiaoluType",nJiaoluType),
                                           new SqlParameter("nTrainmanTypeID",nTrainmanTypeID),
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("nKehuoID",nKehuoID),
                                           new SqlParameter("nDragTypeID",nDragTypeID),
                                           new SqlParameter("nTrainmanRunType",nTrainmanRunType)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = @"update TAB_Base_TrainmanJiaolu set strTrainmanJiaoluName = @strTrainmanJiaoluName
                ,nJiaoluType=@nJiaoluType,strLineGUID=@strLineGUID,nTrainmanTypeID=@nTrainmanTypeID,strTrainJiaoluGUID=@strTrainJiaoluGUID,nKehuoID=@nKehuoID,nDragTypeID=@nDragTypeID,nTrainmanRunType=@nTrainmanRunType where strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID";
            SqlParameter[] sqlParams = {
                                            new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID),
                                           new SqlParameter("strTrainmanJiaoluName",strTrainmanJiaoluName),
                                           new SqlParameter("strLineGUID",strLineGUID),
                                           new SqlParameter("nJiaoluType",nJiaoluType),
                                           new SqlParameter("nTrainmanTypeID",nTrainmanTypeID),
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("nKehuoID",nKehuoID),
                                           new SqlParameter("nDragTypeID",nDragTypeID),
                                           new SqlParameter("nTrainmanRunType",nTrainmanRunType)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string JiaoluGUID)
        {
            string strSql = "delete TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainmanJiaoluGUID",JiaoluGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string JiaoluGUID, string LineGUID, string JiaoluName)
        {
            string strSql = "select count(*) from TAB_Base_TrainmanJiaolu where strLineGUID =@strLineGUID  and strTrainmanJiaoluName=@strTrainmanJiaoluName ";
            if (JiaoluGUID != "")
            {
                strSql += " and strTrainmanJiaoluGUID <> @strTrainmanJiaoluGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strLineGUID",LineGUID),
                                           new SqlParameter("strTrainmanJiaoluGUID",JiaoluGUID),
                                           new SqlParameter("strTrainmanJiaoluName",JiaoluName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
       
        #endregion 增删改


        #region 扩展方法

        public static DataTable GetDataWithTjlid(string trainJiaoluGuid)
        {
            string strSql = "select strTrainmanJiaoluGUID,nJiaoluType,strTrainmanJiaoluName from TAB_Base_TrainmanJiaolu where 1=1";
            if (trainJiaoluGuid != "")
            {
                strSql += " and strTrainJiaoluGUID = @trainJiaoluGuid";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("trainJiaoluGuid",trainJiaoluGuid)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //获取人员交路分页数据
        public static DataTable GetTrainmanJiaolus(int Pageindex, int PageCount, string AreaGUID, string LineGUID, int JiaoluType, string TrainJiaoluName)
        {

            string strSqlCondition = " where nJiaoluType > 1";
            if (AreaGUID != "")
            {
                strSqlCondition += " and strAreaGUID = @strAreaGUID ";
            }
            if (LineGUID != "")
            {
                strSqlCondition += " and strLineGUID = @strLineGUID ";
            }
            if (JiaoluType > 0)
            {
                strSqlCondition += " and nJiaoluType = @nJiaoluType ";
            }
            if (TrainJiaoluName != "")
            {
                strSqlCondition += " and strTrainmanJiaoluName like @strTrainmanJiaoluName ";
            }

            string strSql = "select top " + PageCount.ToString() + " * from VIEW_Base_TrainmanJiaolu " + strSqlCondition +
                " and strTrainmanJiaoluGUID not in (select top " + ((Pageindex - 1) * PageCount).ToString() + " strTrainmanJiaoluGUID from VIEW_Base_TrainmanJiaolu " + strSqlCondition +
                "  order by strLineName,strTrainmanJiaoluName)  order by strLineName,strTrainmanJiaoluName";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strAreaGUID",AreaGUID),
                                           new SqlParameter("strLineGUID",LineGUID),
                                           new SqlParameter("nJiaoluType",JiaoluType),
                                           new SqlParameter("strTrainmanJiaoluName","%" +TrainJiaoluName+ "%")
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //获取人员交路数量
        public static int GetTrainmanJiaoluCount(string AreaGUID, string LineGUID, int JiaoluType, string TrainJiaoluName)
        {
            string strSqlCondition = " where nJiaoluType > 1";
            if (AreaGUID != "")
            {
                strSqlCondition += " and strAreaGUID = @strAreaGUID ";
            }
            if (LineGUID != "")
            {
                strSqlCondition += " and strLineGUID = @strLineGUID ";
            }
            if (JiaoluType > 0)
            {
                strSqlCondition += " and nJiaoluType = @nJiaoluType ";
            }
            if (TrainJiaoluName != "")
            {
                strSqlCondition += " and strTrainmanJiaoluName like @strTrainmanJiaoluName ";
            }

            string strSql = "select count(*) from VIEW_Base_TrainmanJiaolu " + strSqlCondition;;

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strAreaGUID",AreaGUID),
                                           new SqlParameter("nJiaoluType",JiaoluType),
                                           new SqlParameter("strTrainmanJiaoluName","%" +TrainJiaoluName+ "%")
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams));
        }

        //获取区域内所有人员交路
        public static DataTable GetAllTrainmanJiaolus(string AreaGUID)
        {
            string strSql = "select * from VIEW_Base_TrainmanJiaolu where strAreaGUID=@strAreaGUID and nJiaoluType > 1 order by strTrainmanJiaoluName";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strAreaGUID",AreaGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //获取区域内所有人员交路的下拉表现形式
        public static DataTable GetAllTrainmanJiaoluDic(string AreaGUID,string DefaultName)
        {
            DataTable dtResult = GetAllTrainmanJiaolus(DefaultName);
            DataRow dr = dtResult.NewRow();
            dr["strTrainmanJiaoluGUID"] = "";
            dr["strTrainmanJiaoluName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }
        //获取站点管辖交路
        public static DataTable GetSiteTrainmanJiaolus(string SiteGUID)
        {
            string strSql = "select * from VIEW_Base_JiaoluInSite where strSiteGUID = @SiteGUID and nJiaoluType > 1 order by strTrainmanJiaoluName";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@SiteGUID",SiteGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 获取线路内包含的人员交路(包括预备和非运转交路)
        /// </summary>
        /// <param name="LineGUID"></param>
        /// <returns></returns>
        public static DataTable GetLineTrainmanJiaolus(string LineGUID)
        {
            string strSql = "select * from VIEW_Base_TrainmanJiaolu where strLineGUID = @LineGUID order by nJiaoluType desc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@LineGUID",LineGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static DataTable GetLineTrainmanJiaoluDic(string LineGUID,string DefaultName)
        {
            DataTable dtResult = GetLineTrainmanJiaolus(LineGUID);
            DataRow dr = dtResult.NewRow();
            dr["strTrainmanJiaoluGUID"] = "";
            dr["strTrainmanJiaoluName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }
        //更新站点管辖交路
        public static void AddSiteTrainmanJiaolus(string SiteGUID, string trainmanJiaolus)
        {
            string strSql = "delete from TAB_Base_JiaoluInSite where strSiteGUID = @strSiteGUID and strTrainmanJiaoluGUID in (select strTrainmanJiaoluGUID from VIEW_Base_JiaoluInSite where strSiteGUID=@strSiteGUID and nJiaoluType > 1)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strSiteGUID",SiteGUID)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            string[] jiaolus = trainmanJiaolus.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < jiaolus.Length; i++)
            {
                strSql = "insert into TAB_Base_JiaoluInSite (strSiteGUID,strTrainmanJiaoluGUID) values (@strSiteGUID,@strTrainmanJiaoluGUID)";
                SqlParameter[] sqlParamsSub = {
                                           new SqlParameter("@strSiteGUID",SiteGUID),
                                           new SqlParameter("@strTrainmanJiaoluGUID",jiaolus[i])
                                       };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSub);
            }

        }
        /// <summary>
        /// 获取所有人员交路的始发站信息，返回值不重复
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllStationOfTrainmanJiaolu()
        {
            string strSql = "select  strStartStation,strStartStationName from VIEW_Base_TrainmanJiaolu where nJiaoluType > 1 group by strStartStation,strStartStationNumber,strStartStationName order by strStartStationNumber";

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        #endregion
    }

}
