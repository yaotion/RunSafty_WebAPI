using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkSteps
{

   


    #region 识别步骤名称
    public class InFlows_Base
    {
        public string strTrainPlanGUID;
        public int nStepIndex;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public int nStepResult;
        public DateTime? dtCreateTime;
        public List<trainmanList> trainmanList;
        public string strWorkShopGUID;
        public string cid;
        public string placeID;
        public string stepChineseName;
        public string stepName;
 
    }

    #endregion

    public class Datas
    {
        public object stepEntity;
        public string stepName;
        public int nWorkTypeID;

    }


    #region 记名式传达 通知通告传入的实体
    public class InFlows_Notice:InFlows_Base
    {
        public int IsRead;
        public List<fileList> fileList;
        public string strFileType;

    }
    public class fileList
    {
        public string fileGUID;
    }

    #endregion

    #region 验卡记录实体类库
    public class InFlows_Card : InFlows_Base
    {
        public int nTestCardResult;
        public string strTestCardResult;
    }
    #endregion

    #region 个性化出勤打印实体类库
    public class InFlows_SpecificOnDutyPrint : InFlows_Base
    {
        public int nIsPrint;
    }
    #endregion

    #region 交付揭示勾画实体类库
    public class InFlows_JiaoFuDraw : InFlows_Base
    {
        public int nIsDraw;
    }
    #endregion

    #region 公布揭示阅读
    public class InFlows_GongBuJieShiRead : InFlows_Base
    {
        public int IsRead;
    }
    #endregion

    #region 退勤转储
    public class InFlows_TQZhuanChu : InFlows_Base
    {
        public MDRunRecordFileMain runRecordFileMain = new MDRunRecordFileMain();
        public string arriveTime = "";
        public string lastEndWorkTime = "";
        public int VerifyID;
        public string Remark;
    }
    #endregion

    #region  人员
    public class trainmanList
    {
        public string strTrainmanNumber;
        public string strTrainmanName;
        public string strTrainmanGUID;
    }
    #endregion

    #region 测酒步骤提交
    public class InSubmitDrink
    {
        //客户端ID
        public string cid;

        //出勤人员GUID
        public string TrainmanGUID;
        //人员值乘的计划信息
        public string TrainPlanGUID;
        //测酒信息
        public DrinkData DrinkInfo = new DrinkData();
        //
        public int VerifyID;
        //
        public string Remark;

        public string arriveTime = "";
        public string lastEndWorkTime = "";
    }
    public class DrinkData
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
        public string nWorkTypeID;

        public string strAreaGUID;
        public Boolean bLocalAreaTrainman;

        //干部相关（部门名称（id），职位名称（id）)
        public string strDepartmentID;//部门id（车间DUID）
        public string strDepartmentName;//部门名称
        public string nCadreTypeID;//职位id
        public string strCadreTypeName;//职位名称

    }

    #endregion

    #region 向步骤表中插入步骤信息需要用的类库
    //步骤的索引
    public class StepIndex
    {
        public string strTrainPlanGUID;
        public string strTrainmanNumber;
        public DateTime? dtStartTime;
        public string strFieldName;
        public int nStepData;
        public DateTime? dtStepData;
        public string strStepData;
        public int nWorkTypeID;
    }

    //步骤的详细信息
    public class StepData
    {
        public string strTrainPlanGUID;
        public string strFieldName;
        public int nStepData;
        public DateTime? dtStepData;
        public string strStepData;
        public string strStepName;
        public string strTrainmanNumber;
        public int nWorkTypeID;
    }

    //步骤的结果信息
    public class StepResult
    {
        public string strTrainPlanGUID;
        public string strStepName;
        public int nStepResult;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public string strStepBrief;
        public DateTime? dtCreateTime;
        public int nStepIndex;
        public string strTrainmanGUID;
        public string strTrainmanNumber;
        public string strTrainmanName;
        public int nWorkTypeID;
    }

    #endregion

    #region 验卡步骤接口
    public class IdIcCard
    {
        public string strTrainPlanGUID;
        public string dtCreateTime;
        public string dtEventTime;
        public string strEventBrief;
        public string strTrainmanName;
        public string strTrainmanNumber;

    }
    #endregion

    #region 通知通过记名传实体对象
    public  class ReadDocPlan
    {
      
        #region Model
        private int _nid;
        private string _strtrainmanguid;
        private string _strfileguid;
        private int? _nreadcount;
        private DateTime? _dtfirstreadtime;
        private DateTime? _dtlastreadtime;
        /// <summary>
        /// 
        /// </summary>
        public int nId
        {
            set { _nid = value; }
            get { return _nid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StrTrainmanGUID
        {
            set { _strtrainmanguid = value; }
            get { return _strtrainmanguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StrFileGUID
        {
            set { _strfileguid = value; }
            get { return _strfileguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NReadCount
        {
            set { _nreadcount = value; }
            get { return _nreadcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DtFirstReadTime
        {
            set { _dtfirstreadtime = value; }
            get { return _dtfirstreadtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DtLastReadTime
        {
            set { _dtlastreadtime = value; }
            get { return _dtlastreadtime; }
        }
        #endregion Model

    }
    #endregion

    #region 判断步骤是否能否被执行
    public class IsExecute
    {
        public int CanExecute;
        public string Remark;

    }
    #endregion

}
