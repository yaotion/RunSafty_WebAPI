using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using TF.RunSafty.TeamGuide.MD;
using TF.RunSafty.TeamGuide.DB;

namespace TF.RunSafty.TeamGuide
{
    #region 指导队接口类
    /// <summary>
    ///类名: LCGuideGroup
    ///说明: 指导队接口类
    /// </summary>
    public class LCGuideGroup
    {
        #region 获取所有车间
        public class OutGetWorkShopArray
        {
            //车间数组
            public SimpleInfoArray workShopArray = new SimpleInfoArray();
        }

        /// <summary>
        /// 1.10.1    获取所有车间
        /// </summary>
        public InterfaceOutPut GetWorkShopArray(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                OutGetWorkShopArray OutParams = new OutGetWorkShopArray();
                DBTeamGuide db = new DBTeamGuide();
                OutParams.workShopArray = db.GetWorkShopDataList();
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取车间GUID
        public class InGetOwnerWorkShopID
        {
            //指导队ID
            public string GuideGroupGUID;
        }

        public class OutGetOwnerWorkShopID
        {
            //结果
            public string result;
            public int Exist = 0;
        }

        /// <summary>
        /// 1.10.2    获取车间GUID
        /// </summary>
        public InterfaceOutPut GetOwnerWorkShopID(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetOwnerWorkShopID InParams = JsonConvert.DeserializeObject<InGetOwnerWorkShopID>(Data);
                OutGetOwnerWorkShopID OutParams = new OutGetOwnerWorkShopID();
                DBTeamGuide db = new DBTeamGuide();
                string workShopGUID = "";
                if (db.GetWorkShopGUID(InParams.GuideGroupGUID, out workShopGUID))
                {
                    OutParams.Exist = 1;
                    OutParams.result = workShopGUID;
                }
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取指导队名称
        public class InGeName
        {
            //指导队ID
            public string GuideGroupGUID;
        }

        public class OutGeName
        {
            //结果
            public string result;
        }

        /// <summary>
        /// 1.10.3    获取指导队名称
        /// </summary>
        public InterfaceOutPut GeName(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGeName InParams = JsonConvert.DeserializeObject<InGeName>(Data);
                OutGeName OutParams = new OutGeName();
                DBTeamGuide db = new DBTeamGuide();
                OutParams.result = db.GetGuideGroupName(InParams.GuideGroupGUID);
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取车间名称－指导队名称
        public class InGetWorkShop_GuideGroup
        {
            //指导队ID
            public string GuideGroupGUID;
        }

        public class OutGetWorkShop_GuideGroup
        {
            //结果
            public string result;
            public int Exist = 0;
        }

        /// <summary>
        /// 1.10.4    获取车间名称－指导队名称
        /// </summary>
        public InterfaceOutPut GetWorkShop_GuideGroup(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetWorkShop_GuideGroup InParams = JsonConvert.DeserializeObject<InGetWorkShop_GuideGroup>(Data);
                OutGetWorkShop_GuideGroup OutParams = new OutGetWorkShop_GuideGroup();
                DBTeamGuide db = new DBTeamGuide();
                string GuideName = "";
                if (db.GetWorkShop_GuideGroup(InParams.GuideGroupGUID, out GuideName))
                {
                    OutParams.Exist = 1;
                    OutParams.result = GuideName;
                }                
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 根据车间，获取指导队
        public class InGetGroupArray
        {
            //车间ID
            public string WorkShopGUID;
        }

        public class OutGetGroupArray
        {
            //指导队数组
            public SimpleInfoArray guideGroupArray = new SimpleInfoArray();
        }

        /// <summary>
        /// 1.10.5    根据车间，获取指导队
        /// </summary>
        public InterfaceOutPut GetGroupArray(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetGroupArray InParams = JsonConvert.DeserializeObject<InGetGroupArray>(Data);
                OutGetGroupArray OutParams = new OutGetGroupArray();
                DBTeamGuide db = new DBTeamGuide();
                OutParams.guideGroupArray = db.GetGroupDataList(InParams.WorkShopGUID);
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion
    }
    #endregion

    #region 人员接口类
    /// <summary>
    ///类名: LCTrainman
    ///说明: 人员接口类
    /// </summary>
    public class LCTrainman
    {
        #region 按人员ID更新所属指导队ID
        public class InSetGroupByID
        {
            //人员ID
            public string trainmanGUID;
            //指导队ID
            public string guideGroupID;
        }

        /// <summary>
        /// 1.10.6    按人员ID更新所属指导队ID
        /// </summary>
        public InterfaceOutPut SetGroupByID(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InSetGroupByID InParams = JsonConvert.DeserializeObject<InSetGroupByID>(Data);
                DBTeamGuide db = new DBTeamGuide();
                db.UpdateGroupByID(InParams.trainmanGUID, InParams.guideGroupID);

                output.result = 0;
                output.resultStr = "更新成功";
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 按工号更新所属指导队ID
        public class InSetGroupByNumber
        {
            //人员工号
            public string trainmanNumber;
            //指导队ID
            public string GuideGroupID;
            //存在的话，不更新
            public Boolean bNotUpdateExist = new Boolean();
        }

        /// <summary>
        /// 1.10.7    按工号更新所属指导队ID
        /// </summary>
        public InterfaceOutPut SetGroupByNumber(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {

                InSetGroupByNumber InParams = JsonConvert.DeserializeObject<InSetGroupByNumber>(Data);
                DBTeamGuide db = new DBTeamGuide();
                db.UpdateGroupByNumber(InParams.GuideGroupID, InParams.trainmanNumber, InParams.bNotUpdateExist);

                output.result = 0;
                output.resultStr = "更新成功";
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 更新司机职位
        public class InSetPostID
        {
            //人员ID
            public string trainmanGUID;
            //职位ID
            public int nPostID;
        }

        /// <summary>
        /// 1.10.8    更新司机职位
        /// </summary>
        public InterfaceOutPut SetPostID(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InSetPostID InParams = JsonConvert.DeserializeObject<InSetPostID>(Data);
                DBTeamGuide db = new DBTeamGuide();
                db.UpdatePostID(InParams.trainmanGUID, InParams.nPostID);

                output.result = 0;
                output.resultStr = "更新成功";
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 根据查询条件和过滤条件，得到司机列表
        public class InGetList
        {
            //查询条件
            public RsQueryTrainman QueryParam = new RsQueryTrainman();
            //过滤条件
            public RsQueryTrainman FilterParam = new RsQueryTrainman();
        }

        public class OutGetList
        {
            //人员数组
            public TeamGuideTrainmanArray trainmanArray = new TeamGuideTrainmanArray();
        }

        /// <summary>
        /// 1.10.9	根据查询条件和过滤条件，得到司机列表
        /// </summary>
        public InterfaceOutPut GetList(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetList InParams = JsonConvert.DeserializeObject<InGetList>(Data);
                OutGetList OutParams = new OutGetList();
                DBTeamGuide db = new DBTeamGuide();
                OutParams.trainmanArray = db.GetTrainmanDataList(InParams.QueryParam, InParams.FilterParam);
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 按职位获取人员列表
        public class InGetByPost
        {
            //车间ID
            public string WorkShopGUID;
            //职位ID
            public int nPostID;
            //对比类型,0 等于nPostID 1不等于nPostID
            public int cmdType;
        }

        public class OutGetByPost
        {
            //人员数组
            public TeamGuideTrainmanArray trainmanArray = new TeamGuideTrainmanArray();
        }

        /// <summary>
        /// 1.10.10    按职位获取人员列表
        /// </summary>
        public InterfaceOutPut GetByPost(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetByPost InParams = JsonConvert.DeserializeObject<InGetByPost>(Data);
                OutGetByPost OutParams = new OutGetByPost();
                DBTeamGuide db = new DBTeamGuide();
                OutParams.trainmanArray = db.GetTrainmanDataListByPost(InParams.WorkShopGUID, InParams.nPostID, InParams.cmdType);

                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion
    }
    #endregion

    #region 签到信息接口类
    /// <summary>
    ///类名: LCGuideSign
    ///说明: 签到信息接口类
    /// </summary>
    public class LCGuideSign
    {
        #region 添加签到信息
        public class InSignIn
        {
            //签到信息
            public GuideSignEntity guideSignEntity = new GuideSignEntity();
        }

        /// <summary>
        /// 1.9.11    添加签到信息
        /// </summary>
        public InterfaceOutPut SignIn(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InSignIn InParams = JsonConvert.DeserializeObject<InSignIn>(Data);
                DBSign_GuideGroup db = new DBSign_GuideGroup();
                db.AddGuideSignIn(InParams.guideSignEntity);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 添加签退信息
        public class InSignOut
        {
            //签退信息
            public GuideSignEntity guideSignEntity = new GuideSignEntity();
        }

        /// <summary>
        /// 1.9.12    添加签退信息
        /// </summary>
        public InterfaceOutPut SignOut(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InSignOut InParams = JsonConvert.DeserializeObject<InSignOut>(Data);
                DBSign_GuideGroup db = new DBSign_GuideGroup();
                db.AddGuideSignOut(InParams.guideSignEntity);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 查询签到信息
        public class InQuerySignRecord
        {
            //查询参数
            public GuideSignQryParam param = new GuideSignQryParam();
        }

        public class OutQuerySignRecord
        {
            //查询结果
            public GuideSignArray guideSignArray = new GuideSignArray();
        }

        /// <summary>
        /// 1.9.13    查询签到信息
        /// </summary>
        public InterfaceOutPut QuerySignRecord(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InQuerySignRecord InParams = JsonConvert.DeserializeObject<InQuerySignRecord>(Data);
                OutQuerySignRecord OutParams = new OutQuerySignRecord();
                DBSign_GuideGroup db = new DBSign_GuideGroup();
                OutParams.guideSignArray = db.GetSignDataList(InParams.param);
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 查询未签到信息
        public class InQueryNotSignRecord
        {
            //查询参数
            public GuideSignQryParam param = new GuideSignQryParam();
        }

        public class OutQueryNotSignRecord
        {
            //查询结果
            public GuideSignArray guideSignArray = new GuideSignArray();
        }

        /// <summary>
        /// 1.9.14    查询未签到信息
        /// </summary>
        public InterfaceOutPut QueryNotSignRecord(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InQueryNotSignRecord InParams = JsonConvert.DeserializeObject<InQueryNotSignRecord>(Data);
                OutQueryNotSignRecord OutParams = new OutQueryNotSignRecord();
                DBSign_GuideGroup db = new DBSign_GuideGroup();
                OutParams.guideSignArray = db.GetNotSignDataList(InParams.param);
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取指定GUID的签到信息
        public class InGetSignRecord
        {
            //记录ID
            public string strGuideSignGUID;
        }

        public class OutGetSignRecord
        {
            //记录信息
            public GuideSignEntity guideSign = new GuideSignEntity();
            //是否存在记录
            public Boolean bExist;
        }

        /// <summary>
        /// 1.9.15    获取指定GUID的签到信息
        /// </summary>
        public InterfaceOutPut GetSignRecord(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            Boolean bExist;
            try
            {
                InGetSignRecord InParams = JsonConvert.DeserializeObject<InGetSignRecord>(Data);
                OutGetSignRecord OutParams = new OutGetSignRecord();
                DBSign_GuideGroup db = new DBSign_GuideGroup();
                OutParams.guideSign = db.GetSignRecord(InParams.strGuideSignGUID, out bExist);
                OutParams.bExist = bExist;
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取指定司机工号的签到信息
        public class InGetSignRecordByTrainmanNumber
        {
            //人员工号
            public string strTrainmanNumber;
            //指导队ID
            public string strGuideGroupGUID;
        }

        public class OutGetSignRecordByTrainmanNumber
        {
            //记录信息
            public GuideSignEntity guideSign = new GuideSignEntity();
            //是否存在记录
            public Boolean bExist;
        }

        /// <summary>
        /// 1.9.16    获取指定司机工号的签到信息
        /// </summary>
        public InterfaceOutPut GetSignRecordByTrainmanNumber(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            Boolean bExist;
            try
            {
                InGetSignRecordByTrainmanNumber InParams = JsonConvert.DeserializeObject<InGetSignRecordByTrainmanNumber>(Data);
                OutGetSignRecordByTrainmanNumber OutParams = new OutGetSignRecordByTrainmanNumber();
                DBSign_GuideGroup db = new DBSign_GuideGroup();
                OutParams.guideSign = db.GetSignRecordByTrainmanNumber(InParams.strTrainmanNumber, InParams.strGuideGroupGUID, out bExist);
                OutParams.bExist = bExist;
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion
    }
    #endregion
}
