using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;
namespace TF.RunSafty.Drink
{
    public class SystemDict
    {
        #region 获取测酒类型数据

        public class Type_Out:JsonOutBase
        {
            public List<TRsDrinkType> data;
        }

        public Type_Out GetDrinkTypeArray(string input)
        {
            Type_Out json=new Type_Out();
            TRsDrinkType drinkType = null;
            json.data=new List<TRsDrinkType>();
            try
            {
                string strSql = "select * from TAB_System_DrinkType order by nDrinkTypeID";
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow  dataRow in table.Rows)
                    {
                        drinkType=new TRsDrinkType();
                        drinkType.nDrinkTypeID = Convert.ToInt32(dataRow["nDrinkTypeID"]);
                        drinkType.strDrinkTypeName = dataRow["nDrinkTypeName"].ToString();
                        json.data.Add(drinkType);
                    }
                    json.result = "0";
                    json.resultStr = "获取饮酒类型成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取饮酒类型失败";
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

        #region 获取验证方式数据

        public class Verify_Out : JsonOutBase
        {
            public List<TRsVerify> data;
        }

        public Verify_Out GetVerifyArray(string input)
        {
            Verify_Out json=new Verify_Out();
            TRsVerify verify = null;
            json.data=new List<TRsVerify>();
            try
            {
                string strSql = "select * from TAB_System_Verify order by nVerifyID";
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in table.Rows)
                    {
                        verify =new TRsVerify();
                        verify.nVerifyID = Convert.ToInt32(dataRow["nVerifyID"]);
                        verify.strVerifyName = dataRow["strVerifyName"].ToString();
                        json.data.Add(verify);
                    }
                    json.resultStr = "获取验证方式成功";
                    json.result = "0";

                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取验证方式失败";
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

        #region 获取测酒结果数据

        public class Result_Out : JsonOutBase
        {
            public List<TRsDrinkResult> data;
        }

        public Result_Out GetDrinkResult(string input)
        {
            Result_Out json=new Result_Out();
            json.data=new List<TRsDrinkResult>();
            TRsDrinkResult result = null;
            try
            {
                string strSql = "select * from TAB_System_DrinkResult order by nDrinkResult";
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in table.Rows)
                    {
                        result = new TRsDrinkResult();
                        result.nDrinkResult = Convert.ToInt32(dataRow["nDrinkResult"]);
                        result.strDrinkResultName = dataRow["strDrinkResultName"].ToString();
                        json.data.Add(result);
                    }
                    json.resultStr = "获取测酒结果成功";
                    json.result = "0";

                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取测酒结果失败";
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




        #region 获取测酒照片
        public class Get_In
        {
            public string strTrainmanGUID;
            public string strTrainPlanGUID;
            public int WorkTypeID;
        }
        public class Get_Out
        {
            public string result = "";
            public string resultStr = "";
            public DrinkImageInfo Content;
        }

        public class DrinkImageInfo
        {
            public int nDrinkResult;
            public DateTime dtCreateTime;
            public string strPictureURL;
        
        }


        public Get_Out ShowDringImage(string data)
        {
            Get_Out json = new Get_Out();
            try
            {
                Get_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In>(data);
                DBDrink db = new DBDrink();
                json.Content = db.GetDrinkImageList(input.strTrainmanGUID,input.strTrainPlanGUID,input.WorkTypeID);
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion


    }
}
