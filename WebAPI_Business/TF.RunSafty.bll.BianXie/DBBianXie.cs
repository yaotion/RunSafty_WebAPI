using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ThinkFreely.DBUtility;
using ThinkFreely.RunSafty;

namespace TF.RunSafty.BianXie
{
   public class DBBianXie
    {

       public string GetTrainmanGUIDByNumber(string TrainmanNumber)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select top 1  strTrainmanGUID from TAB_Org_Trainman where strTrainmanNumber=@strTrainmanNumber");
           SqlParameter[] sqlParams = new SqlParameter[]{
               new SqlParameter("strTrainmanNumber",TrainmanNumber)
           };
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(),sqlParams).Tables[0];
           if (dt.Rows.Count > 0)
           {
               return dt.Rows[0]["strTrainmanGUID"].ToString();           
           }
           return "";
       }

       public bool IsExit(string dtCreatTime,string strTrainNumber)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select * from TAB_Drink_Information where dtCreateTime=@dtCreateTime and strTrainmanNumber=@strTrainmanNumber");
           SqlParameter[] sqlParams = new SqlParameter[]{
               new SqlParameter("dtCreateTime",Convert.ToDateTime(dtCreatTime)),
                new SqlParameter("strTrainmanNumber",strTrainNumber)
           };
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
           if (dt.Rows.Count > 0)
           {
               return true;
           }
           return false;
       }





        #region 上传测酒记录
        /// <summary>
        /// 添加数据
        /// </summary>
        public bool AddDrinkInfo(MDBianXie.DrinkInfo model)
        {
            SqlTrans sqltrans = new SqlTrans();
            sqltrans.Begin();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into TAB_Drink_Information");
                strSql.Append("( strGUID,bLocalAreaTrainman,strTrainmanGUID,strTrainmanNumber,strTrainmanName ,dwAlcoholicity ,nDrinkResult,dtCreateTime ,");
                strSql.Append(" strTrainNo , strTrainNumber , strTrainTypeName , strPlaceID , strPlaceName, strSiteGUID , strSiteNumber,strSiteName ,");
                strSql.Append(" strWorkShopGUID , strWorkShopName ,strAreaGUID,strDutyNumber,strDutyName,nVerifyID,strWorkID,nWorkTypeID,strImagePath,strDepartmentID,strDepartmentName,nCadreTypeID,strCadreTypeName )");
                strSql.Append("values (newid(),@bLocalAreaTrainman,@strTrainmanGUID,@strTrainmanNumber,@strTrainmanName,@dwAlcoholicity,@nDrinkResult,@dtCreateTime,");
                strSql.Append(" @strTrainNo , @strTrainNumber , @strTrainTypeName , @strPlaceID , @strPlaceName, @strSiteGUID ,@strSiteNumber, @strSiteName ,");
                strSql.Append(" @strWorkShopGUID , @strWorkShopName ,@strAreaGUID,@strDutyNumber,@strDutyName,@nVerifyID,@strWorkID,@nWorkTypeID,@strImagePath,@strDepartmentID,@strDepartmentName,@nCadreTypeID,@strCadreTypeName)");
                SqlParameter[] parameters = {
                  new SqlParameter("@bLocalAreaTrainman", model.bLocalAreaTrainman),
                  new SqlParameter("@strTrainmanGUID", model.strTrainmanGUID),
                  new SqlParameter("@strTrainmanNumber", model.strTrainmanNumber),
                  new SqlParameter("@strTrainmanName", model.strTrainmanName),
                  new SqlParameter("@dwAlcoholicity", model.dwAlcoholicity),
                  new SqlParameter("@nDrinkResult", model.nDrinkResult),
                  new SqlParameter("@dtCreateTime", model.dtCreateTime),
                  new SqlParameter("@strTrainNo", model.strTrainNo),
                  new SqlParameter("@strTrainNumber", model.strTrainNumber),
                  new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
                  new SqlParameter("@strPlaceID", model.strPlaceID),
                  new SqlParameter("@strPlaceName", model.strPlaceName),
                  new SqlParameter("@strSiteGUID", model.strSiteGUID),
                  new SqlParameter("@strSiteNumber", model.strSiteNumber),
                  new SqlParameter("@strSiteName", model.strSiteName),
                  new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID),
                  new SqlParameter("@strWorkShopName", model.strWorkShopName),
                  new SqlParameter("@strAreaGUID", model.strAreaGUID),
                  new SqlParameter("@strDutyNumber", model.strDutyNumber),
                  new SqlParameter("@strDutyName", model.strDutyName),
                  new SqlParameter("@nVerifyID", model.nVerifyID),
                  new SqlParameter("@strWorkID", model.strWorkID),
                  new SqlParameter("@nWorkTypeID", model.nWorkTypeID),
                  new SqlParameter("@strImagePath", model.strImagePath),
                  new SqlParameter("strDepartmentID",model.strDepartmentID),
                  new SqlParameter("strDepartmentName",model.strDepartmentName),
                  new SqlParameter("nCadreTypeID",model.nCadreTypeID),
                  new SqlParameter("strCadreTypeName",model.strCadreTypeName)
                  };

                SqlHelper.ExecuteNonQuery(sqltrans.trans, CommandType.Text, strSql.ToString(), parameters);

                #region 插入消息记录
                string strMsg = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                AttentionMsg msg = new AttentionMsg();
                msg.msgType = MSGTYPE.MSG_DRINK;//测酒消息类别
                msg.param = strMsg;
                msg.CreatMsg(sqltrans.trans);
                #endregion
                sqltrans.Commit();
                return true;

            }
            catch(Exception ex)
            {
                sqltrans.RollBack();
                throw ex;
            }
        }
        #endregion

    }
}
