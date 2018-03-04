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
    public class DBChangeTM
    {
        /// <summary>
        /// 判断所有的参数是否为空
        /// </summary>
        /// <param name="InParams"></param>
        /// <param name="checkBrief"></param>
        /// <returns></returns>
        public static void checkIsNull(TF.RunSafty.NamePlate.LCGroup.InChangeTM InParams)
        {
            if (string.IsNullOrEmpty(InParams.DestGrp))
                throw new Exception(string.Format("目标机组ID不能为空"));
            if (InParams.DestPos == 0)
                throw new Exception(string.Format("目标司机位置不能为空"));
            if (string.IsNullOrEmpty(InParams.SrcGrp))
                throw new Exception(string.Format("源机组ID不能为空"));
            if (InParams.SrcPos == 0)
                throw new Exception(string.Format("源司机位置不能为空"));

        }

        /// <summary>
        /// 判断被交换的两位司机是否处于同一区段
        /// </summary>
        /// <param name="TMNumber1"></param>
        /// <param name="TMNumber2"></param>
        public static void CheckIsOneTMJLAndIsLeave(string TMGUID1, string TMGUID2, string Group1, string Group2)
        {
            string strSql = @"Select top 2 * from TAB_Org_Trainman where  strTrainmanGUID = @strTrainmanGUID1 or  strTrainmanGUID = @strTrainmanGUID2";
            SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("strTrainmanGUID1",TMGUID1),
                    new SqlParameter("strTrainmanGUID2",TMGUID2),
                    new SqlParameter("strGroupGUID1",Group1),
                    new SqlParameter("strGroupGUID2",Group2)

            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count == 0)
            {
                throw new Exception(string.Format("找不到司机，请核对司机是否属实！"));
            }
            else if (dt.Rows.Count == 1 && Group1 != Group2) //排除同一个机组内人员的移动
            {
                strSql = @"Select top 2 * from VIEW_Nameplate_Group_TrainmanJiaolu where  strGroupGUID = @strGroupGUID1 or  strGroupGUID = @strGroupGUID2";
                DataTable dtTrainmanJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dtTrainmanJiaolu.Rows.Count != 2)
                    throw new Exception(string.Format("找不到机组，请确认所传机组是否正确！"));
                else
                {
                    if (dtTrainmanJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString() != dtTrainmanJiaolu.Rows[1]["strTrainmanJiaoluGUID"].ToString())
                        throw new Exception(string.Format("被交换的两位司机不属于同一人员区段！"));
                }
            }
            else if (dt.Rows.Count == 2)
            {
                if (dt.Rows[0]["strTrainmanJiaoluGUID"].ToString() != dt.Rows[1]["strTrainmanJiaoluGUID"].ToString())
                    throw new Exception(string.Format("被交换的两位司机不属于同一人员区段！"));

                if (dt.Rows[0]["nTrainmanState"].ToString() == "0" || dt.Rows[1]["nTrainmanState"].ToString() == "0")
                    throw new Exception(string.Format("被交换的两位司机,有一人处于请假状态，不能交换！"));

            }
        }


        /// <summary>
        /// 执行交换人员的主函数
        /// </summary>
        /// <param name="InParams"></param>
        public static void changeTM(TF.RunSafty.NamePlate.LCGroup.InChangeTM InParams)
        {
            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //如果交换的是同一个机组内的人员
                    if (InParams.SrcGrp == InParams.DestGrp)
                    {
                        ChangePos(InParams.SrcPos, InParams.SrcTm, InParams.DestPos, InParams.DestTm, InParams.DestGrp);
                        return;
                    }

                    changeEachTM(InParams.SrcTm, InParams.SrcGrp, trans);
                    changeEachTM(InParams.DestTm, InParams.DestGrp, trans);
                    addNewTM(InParams.DestTm, InParams.SrcPos, InParams.SrcGrp, trans);
                    addNewTM(InParams.SrcTm, InParams.DestPos, InParams.DestGrp, trans);
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


        public static void ChangePos(int pos1,string tm1,int pos2,string tm2,string grp)
        {

          string strSql = "update TAB_Nameplate_Group set strTrainmanGUID" + pos1 + "='" + tm2 + "',strTrainmanGUID" + pos2 + "='" + tm1 + "' where strGroupGUID = '" + grp + "'";
          SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql);
        
        }




        /// <summary>
        /// 清除原有机组中的人员
        /// </summary>
        /// <param name="strTmGUID"></param>
        /// <param name="strGroupGUID"></param>
        /// <param name="trans"></param>
        public static void changeEachTM(string strTmGUID, string strGroupGUID, SqlTransaction trans)
        {

            //获取待加人员原来所属的机组信息
            string strSql = @"select top 1 * from TAB_Nameplate_Group 
                     where  strGroupGUID = @strGroupGUID";
            SqlParameter[] sqlParamsOldGroup = new SqlParameter[]{
                    new SqlParameter("strGroupGUID",strGroupGUID)
                };
            //删除待加人员在原来机组中的信息
            DataTable dtOldGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsOldGroup).Tables[0];


            if (dtOldGroup.Rows[0]["strTrainmanGUID1"].ToString() == strTmGUID)
                strSql = "update TAB_Nameplate_Group set strTrainmanGUID1=@strTrainmanGUID where strGroupGUID = @strGroupGUID";

            if (dtOldGroup.Rows[0]["strTrainmanGUID2"].ToString() == strTmGUID)
                strSql = "update TAB_Nameplate_Group set strTrainmanGUID2=@strTrainmanGUID where strGroupGUID = @strGroupGUID";

            if (dtOldGroup.Rows[0]["strTrainmanGUID3"].ToString() == strTmGUID)
                strSql = "update TAB_Nameplate_Group set strTrainmanGUID3=@strTrainmanGUID where strGroupGUID = @strGroupGUID";

            if (dtOldGroup.Rows[0]["strTrainmanGUID4"].ToString() == strTmGUID)
                strSql = "update TAB_Nameplate_Group set strTrainmanGUID4=@strTrainmanGUID where strGroupGUID = @strGroupGUID";

            SqlParameter[] sqlParamsDel = new SqlParameter[]{
                                new SqlParameter("strTrainmanGUID",""),
                                new SqlParameter("strGroupGUID",strGroupGUID)
                            };

            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsDel);


        }

        /// <summary>
        /// 将原机组的中的人员添加到新的机组中去
        /// </summary>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="TrainmanIndex"></param>
        /// <param name="GroupGUID"></param>
        /// <param name="trans"></param>
        public static void addNewTM(string strTrainmanGUID, int TrainmanIndex, string GroupGUID, SqlTransaction trans)
        {

            string strSql = "";

            //在新机组中添加人员
            switch (TrainmanIndex)
            {
                case 1:
                    {
                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID1=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                        break;
                    }
                case 2:
                    {
                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID2=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                        break;
                    }
                case 3:
                    {
                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID3=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                        break;
                    }
                case 4:
                    {
                        strSql = "update TAB_Nameplate_Group set strTrainmanGUID4=@strTrainmanGUID where strGroupGUID = @strGroupGUID";
                        break;
                    }
            }
            SqlParameter[] sqlParamsAdd = new SqlParameter[]{
                                new SqlParameter("strTrainmanGUID",strTrainmanGUID),
                                new SqlParameter("strGroupGUID",GroupGUID)
                                };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParamsAdd);
        }




    }
}
