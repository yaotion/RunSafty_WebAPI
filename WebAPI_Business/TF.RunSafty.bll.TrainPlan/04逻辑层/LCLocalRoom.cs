using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using TF.RunSafty.Plan.MD;
using TF.CommonUtility;
using ThinkFreely.DBUtility;


namespace TF.RunSafty.Plan
{
    public class LCLocalRoom
    {
        #region '修改本段入寓时间'


        public class InUpdateInRoomTime
        {
            //入寓记录GUID
            public string strInRoomGUID;
            //新的入寓时间
            public DateTime NewTime;
        }

        /// <summary>
        /// 修改本段入寓时间
        /// </summary>
        public InterfaceOutPut UpdateInRoomTime(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InUpdateInRoomTime InParams = javaScriptSerializer.Deserialize<InUpdateInRoomTime>(Data);
                string strSql = "update TAB_Plan_InRoom set dtInRoomTime = @dtInRoomTime where strInRoomGUID = @strInRoomGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("dtInRoomTime",InParams.NewTime),
                    new SqlParameter("strInRoomGUID",InParams.strInRoomGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.UpdateInRoomTime:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion


        #region '查询出入寓记录'

        public class InQueryInRoomRecord
        {
            //开始时间
            public DateTime BeginTime;
            //结束时间
            public DateTime EndTime;
            //所属车间GUID
            public string WorkShopGUID;
        }

        public class OutQueryInRoomRecord
        {
            //出入寓记录列表
            public RoomSignList Signs = new RoomSignList();
        }

        /// <summary>
        /// 查询本段入寓记录
        /// </summary>
        public InterfaceOutPut QueryInRoomRecord(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InQueryInRoomRecord InParams = javaScriptSerializer.Deserialize<InQueryInRoomRecord>(Data);
                OutQueryInRoomRecord OutParams = new OutQueryInRoomRecord();
                
                string strSql = @"Select TAB_Plan_InRoom.*,TAB_Org_Trainman.strTrainmanName,TAB_Org_Trainman.strWorkShopGUID 
                    from TAB_Plan_InRoom Left Join TAB_Org_Trainman On TAB_Plan_InRoom.strTrainmanGUID = TAB_Org_Trainman.strTrainmanGUID 
                    where dtInRoomTime >= @BeginTime and dtInRoomTime <= @EndTime and strWorkShopGUID = @strWorkShopGUID order by dtInRoomTime Desc";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("BeginTime",InParams.BeginTime),
                    new SqlParameter("EndTime",InParams.EndTime),
                    new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RoomSign sign = new RoomSign();
                    PS.PSPlan.RoomSignFromDB(sign,dt.Rows[i],0);
                    OutParams.Signs.Add(sign);
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.QueryInRoomRecord:" + ex.Message);
                throw ex;
            }
            return output;
        }    


        #endregion
    }
}
