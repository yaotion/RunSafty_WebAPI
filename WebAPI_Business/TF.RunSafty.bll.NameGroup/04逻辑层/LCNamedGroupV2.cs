using System;
using System.Text;
using System.Linq;
using TF.CommonUtility;
using ThinkFreely.DBUtility;

using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using TF.RunSafty.Utils.Parse;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.DB;
using System.Data.SqlClient;

namespace TF.RunSafty.NamePlate
{

    public class LCNamedGroupV2
    {


        InterfaceRet _Ret = new InterfaceRet();
        #region   添加机组-------添加记名式交路机组V2
        public class InInsertGrp
        {
            //所属人员交路信息
            public TrainmanJiaoluMin TrainmanJiaolu = new TrainmanJiaoluMin();
            //值班员信息
            public DutyUser DutyUser = new DutyUser();
            //记名式机组的GUID
            public string CheciGUID;
            //int
            public int CheciOrder;
            //车次类型(0,1)
            public int CheciType;
            //往路车次
            public string Checi1;
            //回路车次
            public string Checi2;
            //乘务员1工号

        }

        /// <summary>
        /// 添加记名式交路机组
        /// </summary>
        public InterfaceRet InsertGrp(String Data)
        {
            _Ret.Clear();
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InInsertGrp InParams = javaScriptSerializer.Deserialize<InInsertGrp>(Data);
                //记名式交路机组
                RRsNamedGroup NamedGroup = new RRsNamedGroup();
                NamedGroup.nCheciOrder = InParams.CheciOrder;
                NamedGroup.nCheciType = InParams.CheciType;
                NamedGroup.strCheci1 = InParams.Checi1;
                NamedGroup.strCheci2 = InParams.Checi2;
                NamedGroup.strCheciGUID = InParams.CheciGUID;
                NamedGroup.strTrainmanJiaoluGUID = InParams.TrainmanJiaolu.jiaoluID;
                NamedGroup.dtLastArriveTime = DateTime.Parse("1899-01-01");
                NamedGroup.Group.groupID = Guid.NewGuid().ToString();

                DBNamedGroupV2.InsertGrp(InParams.TrainmanJiaolu.jiaoluID, NamedGroup);
                Group group = NamedGroup.Group;
                TrainmanList trainmanList = new TrainmanList();
                string strContent = string.Format("成功添加一个空的记名式机组");
                DBNameBoard.SaveChangeLog(InParams.TrainmanJiaolu, LBoardChangeType.btcAddGroup, strContent, InParams.DutyUser, trainmanList);
                _Ret.result = 0;
            }
            catch (Exception ex)
            {
                _Ret.resultStr = ex.Message;
                _Ret.result = 1;
                LogClass.log("Interface.AddNamedGroup:" + ex.Message);
                throw ex;
            }
            return _Ret;
        }
        #endregion

        #region 获取指定的记名式机组
        public class InGetNamedGroup
        {
            //记名式机组GUID
            public string CheCiID;
        }

        public class OutGetNamedGroup
        {
            //记名式机组                               
            public RRsNamedGroup Group = new RRsNamedGroup();
            //是否存在
            public int Exist;
        }

        /// <summary>
        /// 获取指定编号的记名式机组
        /// </summary>
        public InterfaceOutPut GetNamedGroupV2(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetNamedGroup InParams = javaScriptSerializer.Deserialize<InGetNamedGroup>(Data);
                OutGetNamedGroup OutParams = new OutGetNamedGroup();
                string strSql = "select * from VIEW_Nameplate_TrainmanJiaolu_Named  where strCheciGUID= @strCheciGUID order by nCheciOrder";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strCheciGUID",InParams.CheCiID)
                };
                OutParams.Exist = 0;
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    OutParams.Exist = 1;
                    PS.PSNameBoard.NamedGroupFromDB(OutParams.Group, dt.Rows[0]);
                }

                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetNamedGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion


    }
}
