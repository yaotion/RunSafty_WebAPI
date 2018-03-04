using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.DB;

namespace TF.RunSafty.NamePlate
{
    #region ================调休=======================
    public class TX
    {
        #region 增加调休
        public void Add(string GroupGUID, TrainmanJiaolu TrainmanJiaolu, DutyUser DutyUser)
        {
            DBTx db = new DBTx();
            db.Add(GroupGUID);
        }
        #endregion


        #region 结束调休
        public void Del(string GroupGUID, TrainmanJiaolu TrainmanJiaolu, DutyUser DutyUser)
        {
            DBTx db = new DBTx();
            db.Del(GroupGUID);
        }
        #endregion



        #region 根据交路GUID，查询所有的调休机组
        public List<Group> Get(string JLGuid)
        {
            DBTx db = new DBTx();
            List<Group> groups = db.GetGroups(JLGuid);
            return groups;
        }
        #endregion
    }
    #endregion
}
