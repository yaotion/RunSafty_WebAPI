using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
namespace TF.RunSafty.DrinkLogic
{
   public class DBDrinkLogic
    {
       public MDDrinkLogic GetDrinkCadreEntity(string strTrainmanNumber)
       {
           MDDrinkLogic mdl = new MDDrinkLogic(); 
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select top 1  g.*,gt.nOrder,d.strWorkShopName from TAB_Org_GanBu g left join View_Org_DepartMentAndWorkShop d on ");
           strSql.Append(" d.strWorkShopGUID=g.strWorkShopGUID left join TAB_Base_GanBuType gt on g.nTypeID=gt.nTypeID");
           strSql.Append(" where strTrainmanNumber='" + strTrainmanNumber + "'");

           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           if (dt.Rows.Count > 0)
           {
               mdl.nCadreTypeID = dt.Rows[0]["nTypeID"].ToString();
               mdl.strCadreTypeName = dt.Rows[0]["strTypeName"].ToString();
               mdl.strDepartmentID = dt.Rows[0]["strWorkShopGUID"].ToString();
               mdl.strDepartmentName = dt.Rows[0]["strWorkShopName"].ToString();
           }
           else
           {
               StringBuilder strSql2 = new StringBuilder();
               strSql2.Append("select top 1 strWorkShopGUID,strWorkShopName,nPostID from VIEW_Org_Trainman where strTrainmanNumber ='" + strTrainmanNumber + "' ");
               DataTable dt2 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql2.ToString()).Tables[0];
               if (dt2.Rows.Count > 0)
               {
                   string nCadreTypeID = dt2.Rows[0]["nPostID"].ToString();
                   if (!string.IsNullOrEmpty(nCadreTypeID))
                   {
                       if (nCadreTypeID.ToString() == "1")
                       {
                           mdl.strCadreTypeName = "司机";
                           mdl.nCadreTypeID = "T-1"; //以'T'开头是为了防止和职位里的类型重复
                       }
                       else if (nCadreTypeID.ToString() == "2")
                       {
                           mdl.strCadreTypeName = "副司机";
                           mdl.nCadreTypeID = "T-2";
                       }

                       else if (nCadreTypeID.ToString() == "3")
                       {
                           mdl.strCadreTypeName = "学员";
                           mdl.nCadreTypeID = "T-3";
                       }
                       else
                       {
                           mdl.strCadreTypeName = "尚无职务";
                           mdl.nCadreTypeID = "T-0";
                       }
                   }
                   mdl.strDepartmentID = dt2.Rows[0]["strWorkShopGUID"].ToString();
                   mdl.strDepartmentName = dt2.Rows[0]["strWorkShopName"].ToString();
               }
           
           }
           return mdl;

       }

    }
}
