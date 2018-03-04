using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Data;
using TF.RunSafty.NamePlate.MD;

namespace TF.RunSafty.NamePlate.DB
{
    public class DBTx
    {
        #region Add（添加调休）
        public bool Add(string GroupGUID)
        {
            string strSql = "update TAB_Nameplate_Group set nTXState=@nTXState,dtTXBeginTime=@dtTXBeginTime where strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("nTXState",1),
                new SqlParameter("dtTXBeginTime",DateTime.Now.ToString()),
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters) > 0;
        }
        #endregion


        #region Del（结束调休）
        public bool Del(string GroupGUID)
        {
            string strSql = "update TAB_Nameplate_Group set nTXState=@nTXState where strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("nTXState",0),
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters) > 0;
        }
        #endregion


        #region 获取调休状态下的所有机组

        /// <summary>
        /// 获取指定交路下指定出勤点的轮乘机组列表
        /// </summary>
        /// <param name="TrainmanJiaolu">人员交路ID</param>
        /// <param name="PlaceID">出勤点ID</param>
        ///<param name="TrainmanGUID">乘务员GUID列表</param>
        /// <returns></returns>
        public List<Group> GetGroups(string TrainmanJiaolu)
        {
            System.Data.SqlClient.SqlParameter[] sqlParamsJiaolu = {
                                                                new System.Data.SqlClient.SqlParameter("Trainmanjiaolu",TrainmanJiaolu)                                                      
                                                             };

            string sqlText = @"select nJiaoluType from tab_base_trainmanJiaolu where strTrainmanJiaoluGUID = @TrainmanJiaolu";
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsJiaolu);
            int jiaoluType = Convert.ToInt32(obj);

            sqlText = @"select *,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber1) as InRoomTime1,
                (select max(dtOutRoomTime) from TAB_Plan_OutRoom where strTrainmanNumber = strTrainmanNumber1) as OutRoomTime1,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber2) as InRoomTime2, 
                (select max(dtOutRoomTime) from TAB_Plan_OutRoom where strTrainmanNumber = strTrainmanNumber2) as OutRoomTime2,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber3) as InRoomTime3, 
                (select max(dtOutRoomTime) from TAB_Plan_OutRoom where strTrainmanNumber = strTrainmanNumber3) as OutRoomTime3,
                (select max(dtInRoomTime) from TAB_Plan_InRoom where strTrainmanNumber = strTrainmanNumber4) as InRoomTime4, 
                (select max(dtOutRoomTime) from TAB_Plan_OutRoom where strTrainmanNumber = strTrainmanNumber4) as OutRoomTime4
              from VIEW_Nameplate_Group  where 1=1  and nTXState=1 ";
           

            if (!string.IsNullOrEmpty(TrainmanJiaolu))
            {
                sqlText += " and strTrainmanJiaoluGUID = @Trainmanjiaolu";
            }
            switch (jiaoluType)
            {
                //named
                case 2:
                    {
                        sqlText += " order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2";
                    }
                    break;
                //order
                case 3:
                    {
                        sqlText += " order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2";
                    }
                    break;
                //together
                case 4:
                    {
                        sqlText += " order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2";
                    }
                    break;
            }
            System.Data.SqlClient.SqlParameter[] sqlParams = {
                                                                new System.Data.SqlClient.SqlParameter("Trainmanjiaolu",TrainmanJiaolu)
                                                             };
            DataTable dtGroups = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams).Tables[0];
            List<Group> result = new List<Group>();
            for (int k = 0; k < dtGroups.Rows.Count; k++)
            {
                Group group = new Group();
                result.Add(group);
                DBOrderGP.DataRowToGroup(dtGroups.Rows[k], group);
            }
            return result;
        }

        #endregion


    }
}
