using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.DAL
{
   public partial class WorkShop
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM TAB_Org_WorkShop ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.WorkShop DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.WorkShop model = new TF.RunSafty.Model.WorkShop();
            if (row != null)
            {
                if (row["strAreaGUID"] != null)
                {
                    model.strAreaGUID = row["strAreaGUID"].ToString();
                }
                if (row["strWorkShopGUID"] != null)
                {
                    model.strWorkShopGUID = row["strWorkShopGUID"].ToString();
                }
                if (row["strWorkShopName"] != null)
                {
                    model.strWorkShopName = row["strWorkShopName"].ToString();
                }

                if (row["strWorkShopNumber"] != null)
                {
                    model.strWorkShopNumber = row["strWorkShopNumber"].ToString();
                }

              
            }
            return model;
        }
    }
}
