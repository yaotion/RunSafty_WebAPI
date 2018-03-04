using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;

namespace TF.Runsafty.Utility
{
    public class SiteInfo
    {
        #region 获取验证信息

        public class DutyUser_In
        {
            public string DutyNumber;
        }

        public class DutyUser_Out:JsonOutBase
        {
            public object data=new object();
        }

        public DutyUser_Out GetDutyUserByNumber(string input)
        {
            DutyUser_Out json=new DutyUser_Out();
            DutyUser_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<DutyUser_In>(input);
                string strSql = string.Format("select * from TAB_Org_DutyUser where strDutyNumber=@strDutyNumber");
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strDutyNumber",model.DutyNumber), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    TRsDutyUser user = new TRsDutyUser();
                    user.strDutyGUID = row["strDutyGUID"].ToString();
                    user.strDutyName = row["strDutyNumber"].ToString();
                    user.strDutyNumber = row["strDutyName"].ToString();
                    user.strPassword = row["strPassword"].ToString();
                    json.data = user;
                    json.result = "0";
                    json.resultStr = "获取用户信息成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = string.Format("验证登录信息错误，错误信息：{0}',['不存在此值班员工号']",model.DutyNumber);
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 获取客户端岗位信息

        public class Client_In
        {
            public string localIP = "";
        }

        public class Client_Out:JsonOutBase
        {
            public object data;
        }

        public Client_Out GetSiteByIP(string input)
        {
            Client_Out json = new Client_Out();
            Client_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Client_In>(input);
                string strSql = string.Format("select * from TAB_Base_Site where strSiteIP=@strSiteIP");
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strSiteIP", model.localIP),
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    string strSiteGUID = row["strSiteGUID"].ToString();
                    TRsSiteInfo Site = new TRsSiteInfo();
                    ADOQueryToData(row, Site);
                    strSql = string.Format("select * from TAB_Base_TrainJiaoluInSite where strSiteGUID=@strSiteGUID");
                    sqlParameters = new SqlParameter[] { new SqlParameter("strSiteGUID", strSiteGUID), };
                    table=SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                    FillSiteTrainJiaolus(table,Site);

                    strSql = string.Format("select * from TAB_Base_Site_Limit where strSiteGUID=@strSiteGUID");
                    table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                    FillSiteJobLimits(table, Site);
                    json.data = Site;
                    json.result = "0";
                    json.resultStr = "获取用户信息成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "该客户端没有在服务器上注册";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }

        public void ADOQueryToData(DataRow row, TRsSiteInfo Site)
        {
            Site.strSiteGUID = row["strSiteGUID"].ToString();
            Site.strSiteNumber = row["strSiteNumber"].ToString();
            Site.strSiteName = row["strSiteName"].ToString();
            Site.strAreaGUID = row["strAreaGUID"].ToString();
            if (row["nSiteEnable"] != DBNull.Value)
            {
                int.TryParse(row["nSiteEnable"].ToString(), out Site.nSiteEnable);
            }
            Site.strSiteIP = row["strSiteIP"].ToString();
            if (row["nSiteJob"] != DBNull.Value)
            {
                int.TryParse(row["nSiteJob"].ToString(), out Site.nSiteJob);
            }
            Site.strStationGUID = row["strStationGUID"].ToString();
            Site.WorkShopGUID = row["strWorkShopGUID"].ToString();
            Site.Tmis = row["strTMIS"].ToString();
        }

        public void FillSiteTrainJiaolus(DataTable table, TRsSiteInfo Site)
        {
            sTrainJiaolu t = null;
            foreach (DataRow row in table.Rows)
            {
                t=new sTrainJiaolu();
                t.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
                Site.TrainJiaolus.Add(t);
            }
        }

        public void FillSiteJobLimits(DataTable table, TRsSiteInfo Site)
        {
            RRsJobLimit jobLimit=new RRsJobLimit();
            foreach (DataRow row in table.Rows)
            {
                jobLimit=new RRsJobLimit();
                jobLimit.Job = (TRsSiteJob)(Convert.ToInt32(row["nJobID"]));
                jobLimit.Limimt = (TRsJobLimit) (Convert.ToInt32(row["nJobLimit"]));
                Site.JobLimits.Add(jobLimit);
            }
        }
        #endregion

        #region 修改密码

        public class Reset_In
        {
            public string UserID = string.Empty;
            public string NewPassword = string.Empty;
        }

        public class Reset_Out:JsonOutBase
        {
            public object data=new object();
        }

        public Reset_Out ResetPassword(string input)
        {
            Reset_Out json=new Reset_Out();
            Reset_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Reset_In>(input);
                string strSql = "update TAB_Org_DutyUser set strPassword =@strPassword where strDutyNumber=@strDutyNumber";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strPassword",model.NewPassword), new SqlParameter("strDutyNumber",model.UserID), 
                };
                int count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
                if (count > 0)
                {
                    json.result = "0";
                    json.resultStr = "密码修改成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "密码修改失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 切换用户
        #endregion
    }
}
