using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.BLL
{
    public class TRsDBTrainmanJiaolu
    {
        public RRsGroup GetTrainmanGroup(string TrainmanGUID)
        {
            
            string strSql = @"select top 1 * from VIEW_Nameplate_Group  where  strTrainmanGUID1 =@strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("strTrainmanGUID",TrainmanGUID), 
            };
            DataTable table =
                SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
            if (table.Rows.Count > 0)
            {
                RRsGroup group = new RRsGroup();
                ADOQueryToGroup(group,table.Rows[0]);
                return group;
            }
            return null;
        }
       
        public void ADOQueryToGroup(RRsGroup Group, DataRow row)
        {
            Group.strGroupGUID = row["strGroupGUID"].ToString();
            Group.strTrainPlanGUID = row["strTrainPlanGUID"].ToString();
            DateTime dtArriveTime;
            if (row["dtLastArriveTime"] != null)
            {
                if (DateTime.TryParse(row["dtLastArriveTime"].ToString(), out dtArriveTime))
                    Group.dtArriveTime = dtArriveTime;
            }
            Group.ZFQJ.strZFQJGUID = row["strZFQJGUID"].ToString();
            Group.ZFQJ.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
            int nQuJianIndex;
            if (row["nQuJianIndex"] != null)
            {
                if (int.TryParse(row["nQuJianIndex"].ToString(), out nQuJianIndex))
                {
                    Group.ZFQJ.nQuJianIndex = nQuJianIndex;
                }
            }
            Group.ZFQJ.strBeginStationGUID = row["strBeginStationGUID"].ToString();
            Group.ZFQJ.strBeginStationName = row["strBeginStationName"].ToString();
            Group.ZFQJ.strEndStationGUID = row["strEndStationGUID"].ToString();
            Group.ZFQJ.strEndStationName = row["strEndStationName"].ToString();

            Group.Trainman1.strTrainmanGUID = row["strTrainmanGUID1"].ToString();
            Group.Trainman1.strTrainmanName = row["strTrainmanName1"].ToString();
            Group.Trainman1.strTrainmanNumber = row["strTrainmanNumber1"].ToString();
            Group.Trainman1.strTelNumber = row["strTelNumber1"].ToString();
            if (row["nTrainmanState1"] == null)
                Group.Trainman1.nTrainmanState = TRsTrainmanState.tsNil;
            else
                Group.Trainman1.nTrainmanState = (TRsTrainmanState) (int.Parse(row["nTrainmanState1"].ToString()));

            Group.Trainman1.nPostID = (TRsPost) (int.Parse(row["nPost1"].ToString()));
            Group.Trainman1.strWorkShopGUID = row["strWorkShopGUID1"].ToString();
            DateTime dtLastEndworkTime;
            if (row["dtLastEndworkTime1"] != null)
            {
                if (DateTime.TryParse(row["dtLastEndworkTime1"].ToString(), out dtLastEndworkTime))
                {
                    Group.Trainman1.dtLastEndworkTime = dtLastEndworkTime;
                }
            }

            Group.Trainman2.strTrainmanGUID = row["strTrainmanGUID2"].ToString();
            Group.Trainman2.strTrainmanName = row["strTrainmanName2"].ToString();
            Group.Trainman2.strTrainmanNumber = row["strTrainmanNumber2"].ToString();
            Group.Trainman2.strTelNumber = row["strTelNumber2"].ToString();

            if (row["nTrainmanState2"] == null)
            {
                Group.Trainman2.nTrainmanState = TRsTrainmanState.tsNil;
            }
            else
                Group.Trainman2.nTrainmanState = (TRsTrainmanState) Convert.ToInt32(row["nTrainmanState2"].ToString());
            int nPostID;
            if (row["nPost2"] != null && int.TryParse(row["nPost2"].ToString(), out nPostID))
            {
                Group.Trainman2.nPostID = (TRsPost) nPostID;
            }
            Group.Trainman2.strWorkShopGUID = row["strWorkShopGUID2"].ToString();
            if (row["dtLastEndworkTime2"] != null &&
                DateTime.TryParse(row["dtLastEndworkTime2"].ToString(), out dtLastEndworkTime))
            {
                Group.Trainman2.dtLastEndworkTime = dtLastEndworkTime;
            }

            Group.Trainman3.strTrainmanGUID = row["strTrainmanGUID3"].ToString();
            Group.Trainman3.strTrainmanName = row["strTrainmanName3"].ToString();
            Group.Trainman3.strTrainmanNumber = row["strTrainmanNumber3"].ToString();
            Group.Trainman3.strTelNumber = row["strTelNumber3"].ToString();
            if (row["nTrainmanState3"] == null)
            {
                Group.Trainman3.nTrainmanState = TRsTrainmanState.tsNil;
            }
            else
            {
                Group.Trainman3.nTrainmanState = (TRsTrainmanState) Convert.ToInt32(row["nTrainmanState3"]);
            }
            if (row["nPost3"] != null)
            {
                Group.Trainman3.nPostID = (TRsPost) Convert.ToInt32(row["nPost3"]);
            }
            Group.Trainman3.strWorkShopGUID = row["strWorkShopGUID3"].ToString();
            if (row["dtLastEndworkTime3"] != null &&
                DateTime.TryParse(row["dtLastEndworkTime3"].ToString(), out dtLastEndworkTime))
            {
                Group.Trainman3.dtLastEndworkTime = dtLastEndworkTime;
            }

            Group.Trainman4.strTrainmanGUID = row["strTrainmanGUID4"].ToString();
            Group.Trainman4.strTrainmanName = row["strTrainmanName4"].ToString();
            Group.Trainman4.strTrainmanNumber = row["strTrainmanNumber4"].ToString();
            Group.Trainman4.strTelNumber = row["strTelNumber4"].ToString();
            if (row["nTrainmanState4"] == null)
            {
                Group.Trainman4.nTrainmanState = TRsTrainmanState.tsNil;
            }
            else
            {
                Group.Trainman4.nTrainmanState = (TRsTrainmanState) Convert.ToInt32(row["nTrainmanState4"]);
            }
            if (row["nPost4"] != null)
            {
                Group.Trainman4.nPostID = (TRsPost) Convert.ToInt32(row["nPost4"]);
            }
            Group.Trainman4.strWorkShopGUID = row["strWorkShopGUID4"].ToString();
            if (row["dtLastEndworkTime4"] != null &&
                DateTime.TryParse(row["dtLastEndworkTime4"].ToString(), out dtLastEndworkTime))
            {
                Group.Trainman4.dtLastEndworkTime = dtLastEndworkTime;
            }
        }

        private void UpdateTrainmanStateToNull(string TrainmanGUID,SqlCommand command)
        {
            command.CommandText = "update Tab_Org_Trainman set nTrainmanState =@nTrainmanState where strTrainmanGUID=@strTrainmanGUID ";
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("nTrainmanState",TRsTrainmanState.tsReady),
                new SqlParameter("strTrainmanGUID",TrainmanGUID), 
            };
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
            command.ExecuteNonQuery();
        }

        private void DeleteTrainmanFromGroup(string TrainmanGUID,SqlCommand command)
        {
            command.CommandText = @"update TAB_Nameplate_Group   set 
strTrainmanGUID1=(case when strTrainmanGUID1=@strTrainmanGUID then '' else strTrainmanGUID1 end),
strTrainmanGUID2=(case when strTrainmanGUID2=@strTrainmanGUID then '' else strTrainmanGUID2 end),
strTrainmanGUID3=(case when strTrainmanGUID3=@strTrainmanGUID then '' else strTrainmanGUID3 end),
strTrainmanGUID4=(case when strTrainmanGUID4=@strTrainmanGUID then '' else strTrainmanGUID4 end)
where strTrainmanGUID1=@strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID
";
            SqlParameter[] sqlParameters = new SqlParameter[]
            { 
                new SqlParameter("strTrainmanGUID",TrainmanGUID), 
            };
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
           int count= command.ExecuteNonQuery();
            if (count > 0)
            {
                command.Transaction.Commit();
            }
        }
        public void DeleteTrainman(string TrainmanGUID)
        {
            SqlTransaction transaction = null;
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            connection.ConnectionString = SqlHelper.ConnString;
            connection.Open();
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                UpdateTrainmanStateToNull(TrainmanGUID, command);
                DeleteTrainmanFromGroup(TrainmanGUID, command);
                transaction.Rollback();
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            finally
            {
                connection.Dispose();
                transaction.Dispose();
            }
        }

        public bool GetGroupInfo(string GroupGUID, RRsGroup Group)
        {
            string strSql = "select top 1 * from VIEW_Nameplate_Group   where  strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (table.Rows.Count > 0)
            {
                ADOQueryToGroup(Group,table.Rows[0]);
                return true;
            }
            return false;
        }


        public void AddTrainman(string GroupGUID, int TrainmanIndex, string TrainmanGUID)
        {
            string strSql="", strTrainmanJiaoluGUID="", strTrainJiaoluGUID="", strWorkShopGUID="";
            SqlTransaction transaction = null;
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();

            try
            {
                command.Connection = connection;
                connection.ConnectionString = SqlHelper.ConnString;
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                strSql = "select strTrainmanJiaoluGUID from VIEW_Nameplate_Group_TrainmanJiaolu where strGroupGUID=@strGroupGUID";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strGroupGUID",GroupGUID), 
                }; 
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    strTrainmanJiaoluGUID = table.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                    strSql =
                        "select strTrainJiaoluGUID,strWorkShopGUID from  VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID";
                    sqlParameters=new SqlParameter[]
                    {
                        new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUID), 
                    };
                    table =
                        SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        strTrainJiaoluGUID = table.Rows[0]["strTrainJiaoluGUID"].ToString();
                        strWorkShopGUID = table.Rows[0]["strWorkShopGUID"].ToString();
                    }
                    //添加人员
                    strSql = string.Format("update TAB_Nameplate_Group set strTrainmanGUID{0}='{1}' where strGroupGUID='{2}' ",TrainmanIndex,TrainmanGUID,GroupGUID);
                    command.CommandText = strSql;
                    if (command.ExecuteNonQuery() == 0)
                    {
                        transaction.Rollback();
                    }
                    if (!string.IsNullOrEmpty(strTrainJiaoluGUID))
                    {
                        //修改人员状态为非运转状态
                        command.CommandText = "update Tab_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strWorkShopGUID=@strWorkShopGUID,nTrainmanState =@nTrainmanState where strTrainmanGUID=@strTrainmanGUID";
                        command.Parameters.Clear();
                        sqlParameters=new SqlParameter[]
                        {
                            new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID), 
                            new SqlParameter("strWorkShopGUID",strWorkShopGUID), 
                            new SqlParameter("nTrainmanState",(int)TRsTrainmanState.tsNormal), 
                            new SqlParameter("strTrainmanGUID",TrainmanGUID), 
                        };
                        command.Parameters.AddRange(sqlParameters);
                        if(command.ExecuteNonQuery()==0)
                            transaction.Rollback();
                    }
                    else
                    {
                        command.CommandText = "update Tab_Org_Trainman set nTrainmanState =@nTrainmanState where strTrainmanGUID=@strTrainmanGUID ";
                        command.Parameters.Clear();
                        sqlParameters = new SqlParameter[] { 
                            new SqlParameter("nTrainmanState",(int)TRsTrainmanState.tsNormal), 
                            new SqlParameter("strTrainmanGUID",TrainmanGUID), };
                        command.Parameters.AddRange(sqlParameters);
                        if (command.ExecuteNonQuery() == 0)
                            transaction.Rollback();
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
        }


        public void AddTrainmanJiaoLuToTrainman(string TrainmanGUID, string TrainmanJiaoLuGUID)
        {
            string strSql = "update TAB_Org_Trainman set strTrainmanJiaoluGUID  =@strTrainmanJiaoluGUID where strTrainmanGUID=@strTrainmanGUID";
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoLuGUID), 
                new SqlParameter("strTrainmanGUID",TrainmanGUID), 
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql,sqlParameters);
        }
    }
}
