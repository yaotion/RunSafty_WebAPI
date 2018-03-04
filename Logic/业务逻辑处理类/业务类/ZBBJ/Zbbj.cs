using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using TF.RunSafty.Logic;
using TF.RunSafty.Logic;

namespace TF.RunSafty.Logic
{
    /// <summary>
    ///Zbbj 的摘要说明
    /// </summary>
    public class Zbbj
    {
        public Zbbj()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string GetBjInfo(lsDatJT6JiChe jiche)
        {
            string rtnstr = "";
            DataTable dtCheckType = lsDicJianCeTianShu.GetlsDicJianCeTianShu(jiche.LocoType,jiche.UnitId);
            if (dtCheckType.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCheckType.Rows)
                {
                    switch (dr["FenLei"].ToString())
                    {
                        case "滤网":
                            lsDatLvWang lsdatlw = lsDatLvWang.getLastLvWangJianCeRiQi(jiche.LocoType,jiche.LocoNum,0);
                            string lvbj = MakeBjContent(lsdatlw.genghuanriqi, PageBase.static_ext_int(dr["TianShu"]), PageBase.static_ext_int(dr["BaoJing"]), "滤网");
                            rtnstr += lvbj;
                            rtnstr += lvbj == "" ? "" : ",";
                            break;
                        case "列车管":
                            lsDatLieCheGuan lsdatlcg = lsDatLieCheGuan.getLastLcgJianCeRiQi(jiche.LocoType, jiche.LocoNum, 0);
                            string lcgbj = MakeBjContent(lsdatlcg.JianChaDate, PageBase.static_ext_int(dr["TianShu"]), PageBase.static_ext_int(dr["BaoJing"]), "列车管");
                            rtnstr += lcgbj;
                            rtnstr += lcgbj == "" ? "" : ",";
                            break;
                    }
                }
            }
            rtnstr = PageBase.CutComma(rtnstr);
            return rtnstr;
        }

        private static string MakeBjContent(object lasttime, int ts, int bj, string type)
        {
            if (PageBase.static_ext_string(lasttime) != "")
            {
                TimeSpan timespan = PageBase.diffTimeReturnTimeSpan(Convert.ToDateTime(lasttime), DateTime.Now);
                double yzbts = timespan.TotalDays;
                if (yzbts <= ts)
                {
                    return "";
                }
            }
            return "本次整备需要更换" + type + "!";
        }
    }
}