using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.PS;
using ThinkFreely.DBUtility;
using TF.CommonUtility;

namespace TF.RunSafty.NamePlate.DB
{
    public class DBNamedGroupV2
    {
        //添加记名式机组
        public static void InsertGrp(string TrainmanJiaoluGUID, RRsNamedGroup namedGroup)
        {

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {

                    //创建机组
                    string strSql = @"insert into TAB_Nameplate_Group 
                                (strGroupGUID,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4)
                                values (@strGroupGUID,@strTrainmanGUID1,@strTrainmanGUID2,@strTrainmanGUID3,@strTrainmanGUID4)";
                    SqlParameter[] sqlParamsGroup = new SqlParameter[]{
                            new SqlParameter("strGroupGUID",namedGroup.Group.groupID),
                            new SqlParameter("strTrainmanGUID1",""),
                            new SqlParameter("strTrainmanGUID2",""),
                            new SqlParameter("strTrainmanGUID3",""),
                            new SqlParameter("strTrainmanGUID4","")
                        };
                    if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsGroup) == 0)
                    {
                        throw new Exception("创建机组错误");
                    }


                    strSql = " select count(*) from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID='" + namedGroup.strTrainmanJiaoluGUID + "'";
                    int namedCount = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql));

                    #region 通过位置找到它在数据库中的实际排序
                    int nCheciOrder = 0;
                    int nAddOrder = 0;
                    if (namedGroup.nCheciOrder == 1)
                    {
                        strSql = " select top 1 nCheCiOrder from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID='" + namedGroup.strTrainmanJiaoluGUID + "'";
                        DataTable dt=SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            nCheciOrder = Convert.ToInt32(dt.Rows[0]["nCheCiOrder"].ToString());
                            nAddOrder = nCheciOrder - 1;
                        }

                    }
                    else if (namedGroup.nCheciOrder > 1 && namedGroup.nCheciOrder <= namedCount)
                    {
                        strSql = @" select top " + (namedGroup.nCheciOrder - 1) + " nCheCiOrder from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID='" + namedGroup.strTrainmanJiaoluGUID + "' order by nCheCiOrder";
                        nCheciOrder = Convert.ToInt32(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0].Rows[(namedGroup.nCheciOrder - 2)]["nCheCiOrder"].ToString());
                        nAddOrder = nCheciOrder + 1;


                    }
                    else if (namedGroup.nCheciOrder > namedCount)
                    {
                        strSql = " select top 1 nCheCiOrder from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID='" + namedGroup.strTrainmanJiaoluGUID + "' order by nCheCiOrder desc";
                        nCheciOrder = Convert.ToInt32(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0].Rows[0]["nCheCiOrder"].ToString());
                        nAddOrder = nCheciOrder + 1;
                    }
                    #endregion

                    strSql = " update  TAB_Nameplate_TrainmanJiaolu_Named set nCheCiOrder=nCheCiOrder+1  where strTrainmanJiaoluGUID='" + namedGroup.strTrainmanJiaoluGUID + "' and nCheCiOrder>" + nCheciOrder;
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql);

                    //添加记名式机组
                    strSql = @"insert into TAB_Nameplate_TrainmanJiaolu_Named 
                (strCheciGUID,strTrainmanJiaoluGUID,nCheciOrder,nCheciType,strCheci1,strCheci2,strGroupGUID,dtLastArriveTime) 
               values (@CheciGUID,@strTrainmanJiaoluGUID, @nCheCiOrder ,@CheciType,@Checi1,@Checi2,@GroupGUID,getdate())";
                    SqlParameter[] sqlParamsNamed = new SqlParameter[]{
                new SqlParameter("CheciGUID",namedGroup.strCheciGUID),
                new SqlParameter("strTrainmanJiaoluGUID",namedGroup.strTrainmanJiaoluGUID),
                new SqlParameter("CheciType",namedGroup.nCheciType),
                new SqlParameter("Checi1",namedGroup.strCheci1),
                new SqlParameter("Checi2",namedGroup.strCheci2),
                new SqlParameter("GroupGUID",namedGroup.Group.groupID),
                new SqlParameter("nCheCiOrder",nAddOrder)
                    };
                    if (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsNamed) == 0)
                    {
                        throw new Exception("创建记名式机组失败");
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

        }
    }
}
