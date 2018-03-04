using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.RunSafty.BLL
{
    public partial class TrainJiaoLu
    {
        private readonly TF.RunSafty.DAL.TrainJiaoLu dal = new TF.RunSafty.DAL.TrainJiaoLu();
        public List<TF.RunSafty.Model.TrainJiaoLu> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.TrainJiaoLu> modelList = new List<TF.RunSafty.Model.TrainJiaoLu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.TrainJiaoLu model;
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

        public List<TF.RunSafty.Model.TrainJiaoLu> GetTrainJiaoLuList(string strTrainJiaolu)
        {
            DataSet set = dal.GetList("");
            DataTable table = set.Tables[0];
            DataView view = table.DefaultView;
            table = view.ToTable();
            return DataTableToList(table);
        }
        public List<TF.RunSafty.Model.TrainJiaoLu> GetPlaceList(List<TF.RunSafty.Model.TrainJiaoLu> placeList)
        {
            if (placeList != null)
            {
                List<TF.RunSafty.Model.TrainJiaoLu> resultList = new List<TF.RunSafty.Model.TrainJiaoLu>();
                foreach (TF.RunSafty.Model.TrainJiaoLu place in placeList)
                {
                    TF.RunSafty.Model.TrainJiaoLu model = new TF.RunSafty.Model.TrainJiaoLu();
                    model.bIsBeginWorkFP = place.bIsBeginWorkFP;
                    model.bIsDir = place.bIsDir;
                    model.nWorkTimeTypeID = place.nWorkTimeTypeID;
                    model.strEndStation = place.strEndStation;
                    model.strStartStation = place.strStartStation;
                    model.strTrainJiaoluGUID = place.strTrainJiaoluGUID;
                    model.strTrainJiaoluName = place.strTrainJiaoluName;
                    model.strWorkShopGUID = place.strWorkShopGUID;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }
    }
}
