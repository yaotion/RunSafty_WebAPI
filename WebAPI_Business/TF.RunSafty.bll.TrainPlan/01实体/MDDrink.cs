using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Runsafty.Plan.MD
{
    /// <summary>
    /// 测酒记录 数据库操作实体
    /// </summary>
    public class MDDrink : MsgType
    {
        public string strGuid = "";
        public string trainmanID = "";
        public string drinkResult = "0";
        public int workTypeID = 0;
        public string createTime = "";
        public string imagePath = "";

        //人员信息
        public string strTrainmanName;
        public string strTrainmanNumber;

        //车次信息
        public string strTrainNo;
        public string strTrainNumber;
        public string strTrainTypeName;

        //车间信息
        public string strWorkShopGUID;
        public string strWorkShopName;
        //退勤点信息
        public string strPlaceID;
        public string strPlaceName;
        public string strSiteGUID;
        public string strSiteName;
        //酒精度
        public string dwAlcoholicity;
        public int nWorkTypeID;

        public string strAreaGUID;
        public int nLocalAreaTrainman;


        //干部相关（部门名称（id），职位名称（id）)
        public string strDepartmentID="";//部门id（车间DUID）
        public string strDepartmentName="";//部门名称
        public string nCadreTypeID="";//职位id
        public string strCadreTypeName="";//职位名称

        public int verifyID;//验证方式
        public string oPlaceId;//出勤点id
        public string dutyUserID;//用户id
        public string strWorkID;//出退勤id

    }

    public class MsgType
    {
        public int msgType;
    
    }
}
