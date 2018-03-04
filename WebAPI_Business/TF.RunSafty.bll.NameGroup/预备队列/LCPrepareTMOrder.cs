using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.NamePlate
{
    public class LCPrepareTMOrder
    {
        public static List<PrepareTMOrder> GetPrepareOrders(string TrainmanjiaoluGUID)
        {
            return DBPrepareTMOrder.GetPrepareOrders(TrainmanjiaoluGUID);
        }
        public static List<PrepareTMOrder> GetCJPrepareOrders(string WorkShopGUID)
        {
            return DBPrepareTMOrder.GetCJPrepareOrders(WorkShopGUID);
        }
        public static int GetMaxTMOrder(string TrainmanJiaoluGUID, int PostID)
        {
            return DBPrepareTMOrder.GetMaxTMOrder(TrainmanJiaoluGUID,PostID);
        }
        public static bool GetTrainmanOrder(string TrainmanNumber, PrepareTMOrder TMOrder)
        {
            return DBPrepareTMOrder.GetTrainmanOrder(TrainmanNumber,TMOrder);
        }
        public static bool IsTurnPrepare(string TMJiaoluGUID)
        {
            return DBPrepareTMOrder.IsTurnPrepare(TMJiaoluGUID);
        }
        public static void AddPrepareTMOrder(PrepareTMOrder TMOrder)
        {
          
            //当前位置以后的所有人向后移动一位
            List<PrepareTMOrder> orders = DBPrepareTMOrder.GetTrainmanSubOrders(TMOrder.TrainmanJiaoluGUID,
                    TMOrder.TrainmanNumber, TMOrder.PostID, TMOrder.TrainmanOrder, true);
            for (int i = 0; i < orders.Count; i++)
            {
                DBPrepareTMOrder.ReorderTrainmanOrder(orders[i].nid, TMOrder.TrainmanOrder + i + 1);
            }
            ///添加
            DBPrepareTMOrder.AddTrainmanOrder(TMOrder);

        }

        public static void DeletePrepareTrainmanOrder(PrepareTMOrder TMOrder)
        {
            ///已经存在则修改
            if (DBPrepareTMOrder.DeleteTrainmanOrder(TMOrder))
            {
                //所有位置向前移动一位
                List<PrepareTMOrder> orders = DBPrepareTMOrder.GetTrainmanSubOrders(TMOrder.TrainmanJiaoluGUID,
                    TMOrder.TrainmanNumber, TMOrder.PostID, TMOrder.TrainmanOrder, false);
                for (int i = 0; i < orders.Count; i++)
                {
                    DBPrepareTMOrder.ReorderTrainmanOrder(orders[i].nid, TMOrder.TrainmanOrder + i);
                }
            }
        }

        public static void UpdateTrainmanOrder(PrepareTMOrder TMOrder)
        {
            DBPrepareTMOrder.UpdateTrainmanOrder(TMOrder);
        }

        public static void SetToPrepareOrder(string TMJiaoluGUID, string TMJiaouName, string GroupGUID)
        {
            //将所有机组打散,并将司机和副司机安排到备班表中
            NamePlate.MD.Group g = new NamePlate.MD.Group();
            
            if (NamePlate.LCGroup.GetGroup(GroupGUID, g))
            {            
                int tempOrder;
                PrepareTMOrder TMOrder;
                if (g.trainman1.trainmanID != "")
                {
                    tempOrder = LCPrepareTMOrder.GetMaxTMOrder(TMJiaoluGUID, 1);
                    TMOrder = new PrepareTMOrder();
                    TMOrder.TrainmanJiaoluGUID = TMJiaoluGUID;
                    TMOrder.TrainmanJiaoluName = TMJiaouName;
                    TMOrder.TrainmanName = g.trainman1.trainmanName;
                    TMOrder.TrainmanNumber = g.trainman1.trainmanNumber;
                    TMOrder.TrainmanOrder = tempOrder + 1;
                    TMOrder.PostID = 1;            
                    LCPrepareTMOrder.AddPrepareTMOrder(TMOrder);
                }
                if (g.trainman2.trainmanID != "")
                {
                    tempOrder = LCPrepareTMOrder.GetMaxTMOrder(TMJiaoluGUID, 2);
                    TMOrder = new PrepareTMOrder();
                    TMOrder.TrainmanJiaoluGUID = TMJiaoluGUID;
                    TMOrder.TrainmanJiaoluName = TMJiaouName;
                    TMOrder.TrainmanName = g.trainman2.trainmanName;
                    TMOrder.TrainmanNumber = g.trainman2.trainmanNumber;
                    TMOrder.TrainmanOrder = tempOrder + 1;
                    TMOrder.PostID = 2;                    
                    LCPrepareTMOrder.AddPrepareTMOrder(TMOrder);
                }
            }
            else
            {
                TF.CommonUtility.LogClass.log(string.Format("未获取到机组信息{0}", GroupGUID));
            }
        }

        public static void AddLog(PrepareTMOrderLog Log)
        {
            DBPrepareTMOrder.AddLog(Log);
        }
        public static List<PrepareTMOrderLog> QueryLog(DateTime BeginTime, DateTime EndTime, string TMJiaoluGUID, string LogText)
        {
            return DBPrepareTMOrder.QueryLog(BeginTime,EndTime,TMJiaoluGUID,LogText);
        }

       
    }
}
