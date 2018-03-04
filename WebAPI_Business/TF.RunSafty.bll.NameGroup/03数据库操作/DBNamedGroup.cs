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
    public class DBNamedGroup
    {
        //添加记名式机组
        public static void MoveGrp(string CCGUID, int CCOrder,TrainmanJiaoluMin TrainmanJiaolu)
        {

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    string strSql = "";


                    #region 通过位置找到它在数据库中的实际排序

                    strSql = " select count(*) from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID='" + TrainmanJiaolu.jiaoluID + "'";
                    int namedCount = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql));
                    int nCheciOrder = 0;
                    if (CCOrder == 1)
                    {
                        strSql = " select top 1 nCheCiOrder from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID='" + TrainmanJiaolu.jiaoluID + "' order by nCheCiOrder ";
                        nCheciOrder = Convert.ToInt32(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0].Rows[0]["nCheCiOrder"].ToString());

                    }
                    else if (CCOrder > 1 && CCOrder <= namedCount)
                    {
                        strSql = @" select top " + CCOrder + " nCheCiOrder from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID='" + TrainmanJiaolu.jiaoluID + "' order by nCheCiOrder";
                        nCheciOrder = Convert.ToInt32(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0].Rows[(CCOrder - 1)]["nCheCiOrder"].ToString());


                    }
                    else if (CCOrder > namedCount)
                    {
                        strSql = " select top 1 nCheCiOrder from TAB_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID='" + CCOrder + "' order by nCheCiOrder desc";
                        nCheciOrder = Convert.ToInt32(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0].Rows[0]["nCheCiOrder"].ToString());
                    }
                    #endregion



                    int OldCCOrder = 0;
                    strSql = @"select * from TAB_Nameplate_TrainmanJiaolu_Named where strCheciGUID = '" + CCGUID + "'";
                    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        OldCCOrder = Convert.ToInt32(dt.Rows[0]["nCheCiOrder"].ToString());
                    }

                    if (nCheciOrder > OldCCOrder)
                    {

                        strSql = @" update  TAB_Nameplate_TrainmanJiaolu_Named set nCheCiOrder=nCheCiOrder+1 where 
                    strTrainmanJiaoluGUID='" + TrainmanJiaolu.jiaoluID + "' and nCheCiOrder>" + nCheciOrder;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql);

                        strSql = @" update  TAB_Nameplate_TrainmanJiaolu_Named set nCheCiOrder=" + (nCheciOrder + 1) + "  where strCheciGUID='" + CCGUID + "'";
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql);
                    }
                    else if (nCheciOrder < OldCCOrder)
                    {
                        strSql = @" update  TAB_Nameplate_TrainmanJiaolu_Named set nCheCiOrder=nCheCiOrder+1 where 
                    strTrainmanJiaoluGUID='" + TrainmanJiaolu.jiaoluID + "' and nCheCiOrder>=" + nCheciOrder;
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql);
                        strSql = @" update  TAB_Nameplate_TrainmanJiaolu_Named set nCheCiOrder=" + nCheciOrder + "  where strCheciGUID='" + CCGUID + "'";
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql);

                    }
                    //设置位置
                
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
