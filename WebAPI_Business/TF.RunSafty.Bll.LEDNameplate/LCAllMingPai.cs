using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace TF.RunSafty.LEDNameplate
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
            DBAllMingPai dbmp = new DBAllMingPai();
            dbmp.getTmAndCheDui();
            string strTmJiaoLuGUIDs = "";
            if (!string.IsNullOrEmpty(InParams.strTmJiaoLuGUIDs))
            {
                string[] arr = InParams.strTmJiaoLuGUIDs.Split(',');
                for (int k = 0; k < arr.Length; k++)
                {
                    strTmJiaoLuGUIDs += "'" + arr[k] + "',";
                }

            }
            if (!string.IsNullOrEmpty(strTmJiaoLuGUIDs))
                strTmJiaoLuGUIDs = strTmJiaoLuGUIDs.Substring(0, strTmJiaoLuGUIDs.Length - 1);


            allMingPai.ready = dbmp.ReadyDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs);
            allMingPai.unRun = dbmp.unRunDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs);
            allMingPai.ganBu = dbmp.GanBuDtToList(InParams.WorkShopGUID);
            allMingPai.dutyUser = dbmp.DutyUserDtToList(InParams.WorkShopGUID);

            //获取24小时
            DataTable dtNoDispatching = dbmp.getNoDispatching(InParams.WorkShopGUID);

            grp grp = new grp();
            grp.LunCheng = dbmp.LunChengDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs, dtNoDispatching);
            grp.Named = dbmp.NamedDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs, dtNoDispatching);
            grp.BaoCheng = dbmp.BaoChengDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs,dtNoDispatching);
            grp.TX = dbmp.TXDtToList(InParams.WorkShopGUID, strTmJiaoLuGUIDs);

            allMingPai.grp = grp;


            output.data = allMingPai;
            output.result = 0;
            output.resultStr = "获取成功！";

            return output;
        }
        #endregion
    }
}
