using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.NamePlate
{
    public class MDAllMingPai
    {
        public grp grp;
        public List<ganBu> ganBu;
        public List<ready> ready;
        public List<unRun> unRun;
        public List<PrepareTMOrder> readyOrders;
        public List<neiWaiQin> dutyUser;
       
    }
    //预备机组
    public class grp
    {
        public List<BaoCheng> BaoCheng;
        public List<LunCheng> LunCheng;
        public List<Named> Named;
        public List<LunCheng> TX;


    }
    //运转机组下 轮乘
    public class LunCheng
    {
        public Jl Jl;
        public List<grps> grps;
    }
    //运转机组下  轮乘对应的交路信息
    public class Jl
    {
        public string JlGUID;
        public string JlName;
    
    }
    //运转机组下   轮乘对应的机组信息
    public class grps
    {
        public string orderNo;
        public string groupState;
        public string strCheci1;
        public string strCheci2;
        public tms tms;
    }
    public class tms
    {
        public tm1 tm1;
        public tm2 tm2;
        public tm3 tm3;
        public tm4 tm4;
    }
    public class tm1
    {
        public string tmNumber;
        public string tmName;
        public string tmGUID;
        public string post;
        public string tmGuideGroupName;
    
    }
    public class tm2
    {
        public string tmNumber;
        public string tmName;
        public string tmGUID;
        public string post;
        public string tmGuideGroupName;

    }
    public class tm3
    {
        public string tmNumber;
        public string tmName;
        public string tmGUID;
        public string post;
        public string tmGuideGroupName;

    }
    public class tm4
    {
        public string tmNumber;
        public string tmName;
        public string tmGUID;
        public string post;
        public string tmGuideGroupName;
    }
    public class Named
    {
        public JlNamed Jl;
        public List<grps> grps;
    }
    //运转机组下  轮乘对应的交路信息
    public class JlNamed
    {
        public string JlGUID;
        public string JlName;
    }

    public class BaoCheng
    {
        public string CheXing;
        public string CheHao;
        public List<grps> grps;

    }
    //public class grps
    //{
    //    public bTm1 bTm1;
    //    public bTm2 bTm2;
    //    public bTm3 bTm3;
    //    public bTm4 bTm4;
    //    public gropInfo gropInfo;
    //}
    public class bTm1
    {
        public string tmNumber;
        public string tmName;
        public string post;
        public string isKey;
        public string fixedGroupGUID;
    }
    public class bTm2
    {
        public string tmNumber;
        public string tmName;
        public string post;
        public string isKey;
        public string fixedGroupGUID;
    }
    public class bTm3
    {
        public string tmNumber;
        public string tmName;
        public string post;
        public string isKey;
        public string fixedGroupGUID;
    }
    public class bTm4
    {
        public string tmNumber;
        public string tmName;
        public string post;
        public string isKey;
        public string fixedGroupGUID;
    }
    public class gropInfo
    {
        public string groupGUID;
        public string groupState;
        public string order;
    }
    //干部列表
    public class ganBu
    {
        public string strTypeID;
        public string strTypeName;
        public List<trainMan> trainMan;

    }



    public class neiWaiQin
    {
        public string strTypeID;
        public string strTypeName;
        public List<trainMan> trainMan;

    }

    //非运转列表（请假）
    public class unRun
    {

        public string strLeaveTypeGUID;
        public string strLeaveTypeName;
        public List<trainMan> trainMan;
      
    }


    //预备人员
    public class ready
    {
        public string strTrainmanJiaoluGUID;
        public string strTrainmanJiaoluName;
        public List<trainMan> trainMan;

    }
    //人员列表
    public class trainMan
    {
        public string tmNumber;
        public string tmName;
        public string tmGUID;
        public string nPostID;
        public string tmGuideGroupName;
        public string typeName;
    }


    public class tmAndCheDui
    {
        public string strTn;
        public string strChedui;
    }
}
