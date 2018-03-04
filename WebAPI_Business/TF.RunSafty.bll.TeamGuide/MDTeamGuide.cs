using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.TeamGuide.MD
{
    #region 人员信息及数组
    /// <summary>
    ///类名: Trainman
    ///说明: 人员信息
    /// </summary>
    public class TeamGuideTrainman
    {
        public TeamGuideTrainman()
        { }

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanNumber;

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanName;

        /// <summary>
        /// 
        /// </summary>
        public int nPostID;

        /// <summary>
        /// 
        /// </summary>
        public string strWorkShopGUID;

        /// <summary>
        /// 
        /// </summary>
        public string strGuideGroupGUID;

        /// <summary>
        /// 
        /// </summary>
        public string strTrainmanGUID;

        /// <summary>
        /// 指导队名称
        /// </summary>
        public string strGuideGroupName;

        /// <summary>
        /// 车间名称
        /// </summary>
        public string strWorkShopName;

    }

    /// <summary>
    ///类名: TrainmanArray
    ///说明: 人员信息数组
    /// </summary>
    public class TeamGuideTrainmanArray : List<TeamGuideTrainman>
    {
        public TeamGuideTrainmanArray()
        { }
    }
    #endregion

    #region 人员信息查询参数
    /// <summary>
    ///类名: RsQueryTrainman
    ///说明: 人员信息查询参数
    /// </summary>
    public class RsQueryTrainman
    {
        public RsQueryTrainman()
        { }

        /// <summary>
        /// 工号
        /// </summary>
        public string strTrainmanNumber = "";

        /// <summary>
        /// 姓名
        /// </summary>
        public string strTrainmanName = "";

        /// <summary>
        /// 机务段ID
        /// </summary>
        public string strAreaGUID = "";

        /// <summary>
        /// 车间ID
        /// </summary>
        public string strWorkShopGUID = "";

        /// <summary>
        /// 区段ID
        /// </summary>
        public string strTrainJiaoluGUID = "";

        /// <summary>
        /// 指导组ID
        /// </summary>
        public string strGuideGroupGUID = "";

        /// <summary>
        /// 已登记指纹数量，-1为所有
        /// </summary>
        public int nFingerCount;

        /// <summary>
        /// 是否有照片，-1为所有
        /// </summary>
        public int nPhotoCount;

    }
    #endregion

    #region 签到信息及数组
    /// <summary>
    ///类名: GuideSignEntity
    ///说明: 指导队签到信息
    /// </summary>
    public class GuideSignEntity
    {
        public GuideSignEntity()
        { }

        /// <summary>
        /// 记录ID
        /// </summary>
        public string strGuideSignGUID = string.Empty;

        /// <summary>
        /// 人员工号
        /// </summary>
        public string strTrainmanNumber = string.Empty;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string strTrainmanName = string.Empty;

        /// <summary>
        /// 车间ID
        /// </summary>
        public string strWorkShopGUID = string.Empty;

        /// <summary>
        /// 车间名称
        /// </summary>
        public string strWorkShopName = string.Empty;

        /// <summary>
        /// 指队组ID
        /// </summary>
        public string strGuideGroupGUID = string.Empty;

        /// <summary>
        /// 指导组名称
        /// </summary>
        public string strGuideGroupName = string.Empty;

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? dtSignInTime;

        /// <summary>
        /// 签到方式
        /// </summary>
        public int nSignInFlag;

        /// <summary>
        /// 签退时间
        /// </summary>
        public DateTime? dtSignOutTime;

        /// <summary>
        /// 签退方式
        /// </summary>
        public int nSignOutFlag;
    }
    /// <summary>
    ///类名: GuideSignArray
    ///说明: 签到信息数组
    /// </summary>
    public class GuideSignArray : List<GuideSignEntity>
    {
        public GuideSignArray()
        { }

    }
    #endregion

    #region 签到信息查询参数
    /// <summary>
    ///类名: GuideSignQryParam
    ///说明: 签到信息查询参数
    /// </summary>
    public class GuideSignQryParam
    {
        public GuideSignQryParam()
        { }

        /// <summary>
        /// 工号，空为所有
        /// </summary>
        public string strTrainmanNumber = string.Empty;

        /// <summary>
        /// 姓名，空为所有
        /// </summary>
        public string strTrainmanName = string.Empty;

        /// <summary>
        /// 所属车间，空为所有
        /// </summary>
        public string strWorkShopGUID = string.Empty;

        /// <summary>
        /// 所属指导组 ，空为所有
        /// </summary>
        public string strGuideGroupGUID = string.Empty;

        /// <summary>
        /// 签到时间-开始查询时间
        /// </summary>
        public DateTime? dtSignTimeBegin;

        /// <summary>
        /// 签到时间-终止查询时间
        /// </summary>
        public DateTime? dtSignTimeEnd;

        /// <summary>
        /// 签到方式，sfNone为所有
        /// </summary>
        public int nSignFlag;

    }
    #endregion
}
