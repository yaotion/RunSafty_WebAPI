using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.NamePlate.DB;
using TF.RunSafty.NamePlate.MD;

namespace TF.RunSafty.NamePlate
{
    public class LCOrderGP
    {
        #region 获取交路或出勤点下的轮乘机组列表
        public List<OrderGroup> GetOrderGP(string placeID, string trainmanjiaoluID, string trainmanID)
        {
            if (placeID == null || trainmanjiaoluID == null || trainmanID == null)
                throw new Exception("所传参数不能是null");
           
            List<string> placeIDs = new List<string>();
            placeIDs.AddRange(placeID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            List<OrderGroup> groups = DBOrderGP.GetOrderGroups(trainmanjiaoluID, placeIDs, trainmanID);
            return groups;
        }
        #endregion


    }
}
