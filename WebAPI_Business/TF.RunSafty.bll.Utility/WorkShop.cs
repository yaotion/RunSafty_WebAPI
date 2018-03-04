using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.Utility
{
    public class WorkShop
    {
        #region 获取机务段下所有车间

        public class Area_In
        {
            public string AreaGUID = string.Empty;
        }

        public class Area_Out : JsonOutBase
        {
            public List<RRsWorkShop> data;
        }

        public Area_Out GetWorkShopOfArea(string input)
        {
            Area_Out json=new Area_Out();
            RRsWorkShop workShop = null;
            Area_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Area_In>(input);
                string strSql = "select * from TAB_Org_WorkShop where strAreaGUID = @strAreaGUID order by strWorkShopName";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strAreaGUID",model.AreaGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                json.data=new List<RRsWorkShop>();
                foreach (DataRow row in table.Rows)
                {
                    workShop=new RRsWorkShop();
                    workShop.strAreaGUID = row["strAreaGUID"].ToString();
                    workShop.strWorkShopGUID = row["strWorkShopGUID"].ToString();
                    workShop.strWorkShopName = row["strWorkShopName"].ToString();
                    json.data.Add(workShop);
                }
                json.result = "0";
                json.resultStr = "获取车间成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }

        #endregion

        #region 根据车间名称获取对应的GUID

        public class Name_In
        {
            public string WorkShopName = string.Empty;
        }
       
        public class Name_Out:JsonOutBase
        {
            public RRsWorkShop data;
        }

        public Name_Out GetWorkShopGUIDByName(string input)
        {
            Name_Out json =new Name_Out();
            Name_In model = null;
            RRsWorkShop work=new RRsWorkShop();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Name_In>(input);
                string strSql = "select strWorkShopGUID from TAB_Org_WorkShop where  strWorkShopName =@strWorkShopName";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strWorkShopName",model.WorkShopName), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    work.strWorkShopGUID = row["strWorkShopGUID"].ToString();
                    json.data = work;
                    json.result = "0";
                    json.resultStr = "获取车间GUID成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取车间GUID失败";
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
    }
}
