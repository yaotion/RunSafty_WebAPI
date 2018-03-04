using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.RunSafty.BLL
{

    


    public partial class Tab_Config_LED_Class_Order
    {

        TF.RunSafty.DAL.Tab_Config_LED_Class_Order dal = new DAL.Tab_Config_LED_Class_Order();
        public List<TF.RunSafty.Model.Tab_Config_LED_Class_Order> GetTrainnosOfTrainJiaolu(string strTrainJiaolu)
        {
            DataSet set = dal.GetClass_Order(strTrainJiaolu);
            DataTable table = set.Tables[0];
            DataView view = table.DefaultView;
            table = view.ToTable();
            return DataTableToList(table);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.Tab_Config_LED_Class_Order> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.Tab_Config_LED_Class_Order> modelList = new List<TF.RunSafty.Model.Tab_Config_LED_Class_Order>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.Tab_Config_LED_Class_Order model;
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



        public List<TF.RunSafty.Model.Tab_Config_LED_Class_Order> GetPlaceList(List<TF.RunSafty.Model.Tab_Config_LED_Class_Order> placeList)
        {
            if (placeList != null)
            {
                List<TF.RunSafty.Model.Tab_Config_LED_Class_Order> resultList = new List<TF.RunSafty.Model.Tab_Config_LED_Class_Order>();
                foreach (TF.RunSafty.Model.Tab_Config_LED_Class_Order place in placeList)
                {
                    TF.RunSafty.Model.Tab_Config_LED_Class_Order model = new TF.RunSafty.Model.Tab_Config_LED_Class_Order();

                    model.strCheJianGUID = place.strCheJianGUID;
                    model.strCheJianName = place.strCheJianName.ToString();
                    model.strCheJianNickName = place.strCheJianNickName;
                    model.strJiaoLuGUID = place.strJiaoLuGUID;
                    model.strJiaoLuName = place.strJiaoLuName;
                    model.strJiaoLuNickName = place.strJiaoLuNickName;
                    model.strTitle = place.strTitle;
                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }

    }
}
