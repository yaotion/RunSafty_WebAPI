using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.CommonUtility;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.DutyPlace
{
   public class DBDutyPlace
   {
       #region GetSitePlace
       public DutyPlace GetSitePlace(string siteId)
       {
           DutyPlace result = null;
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select top 1 * from VIEW_Base_Site_DutyPlace");
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           if (dt.Rows.Count > 0)
           {
               result = GetAllPlace_DRToModelDTToList(dt.Rows[0]);
           }

           return result;
          
       }
       #endregion
       #region GetAllPlace方法（获取全部客户端）
       public List<DutyPlace> GetAllPlace(string siteId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_Site_DutyPlace where strSiteGUID = '" + siteId + "' ");
            return GetAllPlace_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

       public List<DutyPlace> GetAllPlace_DTToList(DataTable dt)
        {
            List<DutyPlace> modelList = new List<DutyPlace>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DutyPlace model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetAllPlace_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
       public DutyPlace GetAllPlace_DRToModelDTToList(DataRow dr)
        {
            DutyPlace model = new DutyPlace();
            if (dr != null)
            {
                model.placeID = ObjectConvertClass.static_ext_string(dr["strPlaceID"]);
                model.placeName = ObjectConvertClass.static_ext_string(dr["strPlaceName"]);
               
            }
            return model;
        }
       #endregion 


       #region GetDutyPlaceList方法（）
       public List<DutyPlace> GetDutyPlaceList()
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select * from TAB_Base_DutyPlace");
           return GetDutyPlaceList_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
       }

       public List<DutyPlace> GetDutyPlaceList_DTToList(DataTable dt)
       {
           List<DutyPlace> modelList = new List<DutyPlace>();
           int rowsCount = dt.Rows.Count;
           if (rowsCount > 0)
           {
               DutyPlace model;
               for (int n = 0; n < rowsCount; n++)
               {
                   model = GetDutyPlaceList_DRToModelDTToList(dt.Rows[n]);
                   if (model != null)
                   {
                       modelList.Add(model);
                   }
               }
           }
           return modelList;
       }
       public DutyPlace GetDutyPlaceList_DRToModelDTToList(DataRow dr)
       {
           DutyPlace model = new DutyPlace();
           if (dr != null)
           {
               model.placeID = ObjectConvertClass.static_ext_string(dr["strPlaceID"]);
               model.placeName = ObjectConvertClass.static_ext_string(dr["strPlaceName"]);
           }
           return model;
       }
       #endregion GetDutyPlaceList

   }
}
