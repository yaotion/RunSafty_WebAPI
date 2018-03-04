using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.NamePlate.MD;

namespace TF.RunSafty.NamePlate
{
    public class LCAllMingPai
    {
        #region 获取车间下所有的名牌信息forWeb


        public class InGetWorkshopGUID
        {
            //车间GUID
            public string WorkShopGUID;
            public string strTmJiaoLuGUIDs;
        }


        public InterfaceOutPut GetAllMingPai(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            MDAllMingPai allMingPai = new MDAllMingPai();
            InGetWorkshopGUID InParams = Newtonsoft.Json.JsonConvert.DeserializeObject<InGetWorkshopGUID>(Data);
            DBAllMingPai dbmp=new DBAllMingPai();
            dbmp.getTmAndCheDui();
            string strTmJiaoLuGUIDs = "";
            if (!string.IsNullOrEmpty(InParams.strTmJiaoLuGUIDs))
            {
                string [] arr = InParams.strTmJiaoLuGUIDs.Split(',');
                for (int k = 0; k < arr.Length; k++)
                {
                    strTmJiaoLuGUIDs += "'" + arr[k] + "',";
                }
            
            }
            if (!string.IsNullOrEmpty(strTmJiaoLuGUIDs))
            strTmJiaoLuGUIDs = strTmJiaoLuGUIDs.Substring(0, strTmJiaoLuGUIDs.Length - 1);


            allMingPai.ready = dbmp.ReadyDtToList(InParams.WorkShopGUID,strTmJiaoLuGUIDs);
            allMingPai.unRun = dbmp.unRunDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs);
            allMingPai.ganBu = dbmp.GanBuDtToList(InParams.WorkShopGUID);
            allMingPai.dutyUser = dbmp.DutyUserDtToList(InParams.WorkShopGUID);
            allMingPai.readyOrders = LCPrepareTMOrder.GetCJPrepareOrders(InParams.WorkShopGUID);
            grp grp = new grp();
            grp.LunCheng = dbmp.LunChengDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs);
            grp.Named = dbmp.NamedDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs);
            grp.BaoCheng = dbmp.BaoChengDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs);
            grp.TX = dbmp.TXDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs);


           

            allMingPai.grp = grp;


            output.data = allMingPai;
            output.result = 0;
            output.resultStr = "获取成功！";


            //output.data = reader.ReadNameplates(InParams.WorkShopName);
            return output;
        }
        #endregion
    }
}
