using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.RunSafty.BLL
{
    public partial  class Workshop
    {
        private readonly TF.RunSafty.DAL.WorkShop dal = new TF.RunSafty.DAL.WorkShop();
        public List<TF.RunSafty.Model.WorkShop> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.WorkShop> modelList = new List<TF.RunSafty.Model.WorkShop>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.WorkShop model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public List<TF.RunSafty.Model.WorkShop> GetCheJianList(string strTrainJiaolu)
        {
            DataSet set = dal.GetList("");
            DataTable table = set.Tables[0];
            DataView view = table.DefaultView;
            table = view.ToTable();
            return DataTableToList(table);
        }
        public List<TF.RunSafty.Model.WorkShop> GetPlaceList(List<TF.RunSafty.Model.WorkShop> placeList)
        {
            if (placeList != null)
            {
                List<TF.RunSafty.Model.WorkShop> resultList = new List<TF.RunSafty.Model.WorkShop>();
                foreach (TF.RunSafty.Model.WorkShop place in placeList)
                {
                    TF.RunSafty.Model.WorkShop model = new TF.RunSafty.Model.WorkShop();
                    model.strAreaGUID = place.strAreaGUID;
                    model.strWorkShopGUID = place.strWorkShopGUID;
                    model.strWorkShopName = place.strWorkShopName;
                    model.strWorkShopNumber = place.strWorkShopNumber;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }
    }
}
