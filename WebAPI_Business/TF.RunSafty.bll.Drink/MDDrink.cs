using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Drink.MD
{
    #region 测酒记录查询参数类
    /// <summary>
    ///类名: DrinkQueryParam
    ///说明: 测酒记录查询参数
    /// </summary>
    public class DrinkQueryParam
    {
        public DrinkQueryParam()
        { }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string dtBeginTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        public string dtEndTime;

        /// <summary>
        /// 机务段ID
        /// </summary>
        public string strJwdId;

        /// <summary>
        /// 地点ID
        /// </summary>
        public string PlaceId;

        /// <summary>
        /// 车间ID
        /// </summary>
        public string WorkShopGUID;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string TrainmanName;

        /// <summary>
        /// 人员工号
        /// </summary>
        public string TrainmanNumber;

        /// <summary>
        /// 验证方式
        /// </summary>
        public int VerifyID;

        /// <summary>
        /// 测酒类型
        /// </summary>
        public int DrinkTypeID;

        /// <summary>
        /// 测酒结果
        /// </summary>
        public int DrinkResultID;


        public string DepartmentID;//部门id（车间DUID）
        public string CadreTypeID;//职位id



    }
    #endregion


    public class MsgType
    {
        public int msgType;
    }
     


    #region 测酒记录信息及数组
    /// <summary>
    ///类名: DrinkInfo
    ///说明: 测酒记录信息
    /// </summary>
    public class DrinkInfo : MsgType
    {
        public DrinkInfo()
        { }

        /// <summary>
        /// 
        /// </summary>
        public string strGUID = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int nDrinkResult;

        /// <summary>
        /// 
        /// </summary>
        public DateTime? dtCreateTime;

        /// <summary>
        /// 
        /// </summary>
        public int nVerifyID;

        /// <summary>
        /// 
        /// </summary>
        public int nWorkTypeID;

        /// <summary>
        /// 
        /// </summary>
        public string strWorkTypeName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strDrinkResultName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strVerifyName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strImagePath = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int nid;

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strTrainNo = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strTrainNumber = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strTrainTypeName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strPlaceID = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strPlaceName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strSiteGUID = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strSiteName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int dwAlcoholicity;

        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strWorkID = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strAreaGUID = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strDutyGUID = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strDutyName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string strDutyNumber = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool bLocalAreaTrainman;

        /// <summary>
        /// 
        /// </summary>
        public string strSiteNumber = string.Empty;


        //干部相关（部门名称（id），职位名称（id）)
        public string strDepartmentID;//部门id（车间DUID）
        public string strDepartmentName;//部门名称
        public string nCadreTypeID;//职位id
        public string strCadreTypeName;//职位名称



    }
    /// <summary>
    ///类名: DrinkInfoArray
    ///说明: 测酒结果数组
    /// </summary>
    public class DrinkInfoArray : List<DrinkInfo>
    {
        public DrinkInfoArray()
        { }

    }
    #endregion
}
