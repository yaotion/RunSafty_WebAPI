using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using TF.CommonUtility;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.BaseDict
{
    public class LCTrainType
    {
        public class OutGetTrainTypes
        {
            //车型列表
            public List<string> TrainTypes = new List<string>();
        }

        /// <summary>
        /// 获取所有的车型信息
        /// </summary>
        public InterfaceOutPut GetTrainTypes(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                OutGetTrainTypes OutParams = new OutGetTrainTypes();
                output.data = OutParams;
                string strSql = "select * from TAB_System_TrainType order by strTrainTypeName";
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OutParams.TrainTypes.Add(dt.Rows[i]["strTrainTypeName"].ToString());
                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainTypes:" + ex.Message);
                throw ex;
            }
            return output;
        }
    }
}
