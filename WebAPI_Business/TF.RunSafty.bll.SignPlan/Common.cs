using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.SignPlan;

namespace TF.RunSafty.Model.InterfaceModel
{

    public class PlanSign
    {
        public string strTrainJiaoLuGUID { get; set; }
        public string dtArriveTime { get; set; }
        public string dtCallTime { get; set; }
        public string strTrainNo { get; set; }
        public string dtChuQinTime { get; set; }
        public string dtStartTrainTime { get; set; }
        public int nNeedRest { get; set; }
        public string strGUID { get; set; }

    }


    public class PlanRestModels
    {
  
        public int nid { get; set; }
        public string strguid { get; set; }
        public string strtrainjiaoluguid { get; set; }
        public DateTime dtarrivetime { get; set; }
        public DateTime dtcalltime { get; set; }
        public string strtrainno { get; set; }
        public DateTime dtchuqintime { get; set; }
        public DateTime dtstarttime { get; set; }
        public string strtrainnoguid { get; set; }
        public string strworkgrouguid { get; set; }
        public string strtrainmanguid1 { get; set; }
        public string strtrainmanguid2 { get; set; }
        public string strtrainmanguid3 { get; set; }
        public string strtrainmanguid4 { get; set; }
        public int nNeedRest { get; set; }
        public int nFinished { get; set; }
        public DateTime dtStartTrainTime { get; set; }
        public string ePlanState { get; set; }
        public DateTime dtSignTime { get; set; }
    }

    /// <summary>
    ///出乘计划
    /// </summary>
    public class PlanBeginWorks
    {

        public string strCheCi = "";
        public string dtCallWorkTime = "";
        public string dtWaitWorkTime = "";
        public string strTrainmanGUID1 = "";
        public string strTrainmanGUID2 = "";
        public string strTrainmanGUID3 = "";
        public string strTrainmanGUID4 = "";
        public string NNeedRest = "";
        public string NPlanState = "";

        public string strTrainmanNumber1 = "";
        public string strTrainmanNumber2 = "";
        public string strTrainmanNumber3 = "";
        public string strTrainmanNumber4 = "";


        public string strTrainmanName1 = "";
        public string strTrainmanName2 = "";
        public string strTrainmanName3 = "";
        public string strTrainmanName4 = "";
    }

}
