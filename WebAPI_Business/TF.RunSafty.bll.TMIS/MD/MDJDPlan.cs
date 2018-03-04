using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.TMIS
{
    public class MDJDPlan
    {
        //日期 yyyymmdd
        public string Day { get; set; }
        //班别 0白班 1夜班
        public int Shift { get; set; }
        //0基本图 1实际图 2计划
        public int Typ { get; set; }
        //区段
        public string Section_id { get; set; }
        //列车种类
        public int Trainkind { get; set; }
        //运行线id(计划id)
        public string Train_id { get; set; }
        //车次
        public string Train_code { get; set; }
        //出发时间
        public DateTime Time_deptart { get; set; }
        //到达时间
        public DateTime Time_arrived { get; set; }
        //发车站
        public string Station_deptart { get; set; }
        //终到站
        public string Station_arrived { get; set; }
        //机车数
        public int Loco_num { get; set; }
        //总重
        public double Weight { get; set; }
        //辆数
       
        public int Car_count { get; set; }
        //换长
        public double C_length { get; set; }
        //区段名     
        public string Section_name { get; set; }
        //发车站ID  
        public string Station_deptart_id { get; set; }
        //终到站ID  
        public string Station_arrived_id { get; set; }

        //运安计划ID
        public string PlanGUID { get; set; }

        public int IsUpdate { get; set; }

    }
}
