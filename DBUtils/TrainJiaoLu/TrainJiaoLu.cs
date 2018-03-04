using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;

namespace TF.RunSafty.DAL
{
    public partial class TrainJiaoLu
    {

         /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM TAB_Base_TrainJiaolu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.TrainJiaoLu DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.TrainJiaoLu model = new TF.RunSafty.Model.TrainJiaoLu();
            if (row != null)
            {
                if (row["bIsBeginWorkFP"] != null)
                {
                    model.bIsBeginWorkFP = row["bIsBeginWorkFP"].ToString();
                }
                if (row["bIsDir"] != null)
                {
                    model.bIsDir = row["bIsDir"].ToString();
                }
                if (row["nWorkTimeTypeID"] != null)
                {
                    model.nWorkTimeTypeID = row["nWorkTimeTypeID"].ToString();
                }

                if (row["strEndStation"] != null)
                {
                    model.strEndStation = row["strEndStation"].ToString();
                }

                if (row["strStartStation"] != null)
                {
                    model.strStartStation = row["strStartStation"].ToString();
                }
                if (row["strTrainJiaoluGUID"] != null)
                {
                    model.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
                }
                if (row["strTrainJiaoluName"] != null)
                {
                    model.strTrainJiaoluName = row["strTrainJiaoluName"].ToString();
                }

                if (row["strWorkShopGUID"] != null)
                {
                    model.strWorkShopGUID = row["strWorkShopGUID"].ToString();
                }
              
            }
            return model;
        }
    }
}

