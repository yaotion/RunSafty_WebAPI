using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.Data;
using TF.RunSafty.Leave.DB;
using TF.RunSafty.Leave.MD;
namespace TF.RunSafty.Leave
{
    #region LCLeaveType类
    /// <summary>                             
    ///类名: LCLeaveType
    ///说明: 请销假基本数据接口类
    /// </summary>                           
    public class LCLeaveType
    {
        #region 获取请假类别
        public class OutGetLeaveClasses
        {
            //请假类别
            public LeaveClassArray leaveClassArray = new LeaveClassArray();
        }
        /// <summary>
        /// 获取请假类别
        /// </summary>
        public InterfaceOutPut GetLeaveClasses(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                OutGetLeaveClasses OutParams = new OutGetLeaveClasses();
                DBLCLeaveType db = new DBLCLeaveType();
                OutParams.leaveClassArray = db.GetLeaveMgr_LeaveClassDataList();
                output.data = OutParams;
                output.result = 0;
                output.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "返回失败：" + ex.Message;
            }
            return output;
        }
        #endregion

        #region 添加请假类型
        public class InAddLeaveType
        {
            //请假类型
            public LeaveType leaveType = new LeaveType();
        }

        /// <summary>
        /// 添加请假类型
        /// </summary>
        public InterfaceOutPut AddLeaveType(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InAddLeaveType InParams = JsonConvert.DeserializeObject<InAddLeaveType>(Data);
                DBLCLeaveType db = new DBLCLeaveType();
                db.AddLeaveType(InParams.leaveType);
                output.result = 0;
                output.resultStr = "添加成功";
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 删除请假类型
        public class InDeleteLeaveType
        {
            //请假类型ID
            public string LeaveID;
        }

        /// <summary>
        /// 1.8.3 删除请假类型
        /// </summary>                                            
        public InterfaceOutPut DeleteLeaveType(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InDeleteLeaveType input = Newtonsoft.Json.JsonConvert.DeserializeObject<InDeleteLeaveType>(Data);
                DBLCLeaveType db = new DBLCLeaveType();
                db.DeleteLeaveType(input.LeaveID);
                output.result = 0;
                output.resultStr = "删除成功";
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 更新请假类型
        public class InUpdateLeaveType
        {
            //请假类型
            public LeaveType leaveType = new LeaveType();
        }

        /// <summary>
        /// 1.8.4 更新请假类型
        /// </summary>
        public InterfaceOutPut UpdateLeaveType(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InUpdateLeaveType InParams = JsonConvert.DeserializeObject<InUpdateLeaveType>(Data);
                DBLCLeaveType db = new DBLCLeaveType();
                db.UpdateLeaveType(InParams.leaveType);
                output.result = 0;
                output.resultStr = "修改成功";
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 判断是否存在请假类型
        public class InExistLeaveType
        {
            //请假类型
            public LeaveType leaveType = new LeaveType();
        }

        /// <summary>
        /// 1.8.5 判断是否存在请假类型
        /// </summary>
        public InterfaceOutPut ExistLeaveType(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            ResultOutPut res = new ResultOutPut();
            output.result = 1;
            try
            {
                InExistLeaveType InParams = JsonConvert.DeserializeObject<InExistLeaveType>(Data);
                DBLCLeaveType db = new DBLCLeaveType();
                output.result = 0;
                if (db.ExistsLeaveType(InParams.leaveType))
                {
                    res.result = true;
                    output.data = res;
                    output.resultStr = "存在";
                }
                else
                {
                    res.result = false;
                    output.data = res;
                    output.resultStr = "不存在";
                }
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 判断是否存在请假类型(增加ClassID判断)
        public class InExistLeaveTypeWhenEdit
        {
            //请假类型
            public LeaveType leaveType = new LeaveType();
        }

        /// <summary>
        /// 1.8.6 判断是否存在请假类型
        /// </summary>
        public InterfaceOutPut ExistLeaveTypeWhenEdit(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                ResultOutPut res = new ResultOutPut();
                InExistLeaveTypeWhenEdit InParams = JsonConvert.DeserializeObject<InExistLeaveTypeWhenEdit>(Data);
                DBLCLeaveType db = new DBLCLeaveType();
                output.result = 0;
                if (db.ExistsLeaveTypeWhenEdit(InParams.leaveType))
                {
                    res.result = true;
                    output.data = res;
                    output.resultStr = "存在";
                }
                else
                {
                    res.result = false;
                    output.data = res;
                    output.resultStr = "不存在";
                }
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取请假类型列表
        public class OutQueryLeaveTypes
        {
            //请假类型数组
            public LeaveTypeArray leaveTypeArray = new LeaveTypeArray();
        }

        /// <summary>
        /// 1.8.7 获取请假类型列表
        /// </summary>
        public InterfaceOutPut QueryLeaveTypes(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                OutQueryLeaveTypes OutParams = new OutQueryLeaveTypes();
                DBLCLeaveType db = new DBLCLeaveType();
                OutParams.leaveTypeArray = db.GetLeaveTypesDataList();
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

        #region 获取指定名称的请假类型
        public class InGetLeaveType
        {
            //类型名称
            public string strTypeName;
        }

        public class OutGetLeaveType
        {
            //请假类型娄组
            public LeaveTypeArray leaveTypeArray = new LeaveTypeArray();
        }

        /// <summary>
        /// 1.8.8 获取指定名称的请假类型
        /// </summary>
        public InterfaceOutPut GetLeaveType(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetLeaveType InParams = JsonConvert.DeserializeObject<InGetLeaveType>(Data);
                DBLCLeaveType db = new DBLCLeaveType();
                OutGetLeaveType OutParams = new OutGetLeaveType();
                OutParams.leaveTypeArray = db.GetLeaveTypeDataList(InParams.strTypeName);
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

    #region LCAskLeave类
    /// <summary>
    ///类名: LCAskLeave
    ///说明: 请销假接口类
    /// </summary>
    public class LCAskLeave
    {
        #region 根据工号判断该职工是否有未销假的记录
        public class InCheckWhetherAskLeaveByID
        {
            //人员ID
            public string strTrainManID;
        }
        /// <summary>
        /// 1.8.9 判断是否有未销假的记录
        /// </summary>
        public InterfaceOutPut CheckWhetherAskLeaveByID(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            ResultOutPut res = new ResultOutPut();            
            try
            {
                InCheckWhetherAskLeaveByID InParams = JsonConvert.DeserializeObject<InCheckWhetherAskLeaveByID>(Data);
                DBLCAskLeave db = new DBLCAskLeave();
                if (db.CheckWhetherAskLeaveByID(InParams.strTrainManID))
                {
                    res.result = true;
                    output.data = res;
                    output.result = 0;
                    output.resultStr = "已存在";
                }
                else
                {
                    res.result = false;
                    output.data = res;
                    output.resultStr = "不存在";
                }
            }
            catch (Exception ex)
            {
                output.result = 1;
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 请假
        public class InAskLeave
        {
            //请假申请
            public LeaveApplyEntity LeaveApply = new LeaveApplyEntity();
        }
        /// <summary>
        /// 1.8.10    请假
        /// </summary>
        public InterfaceOutPut AskLeave(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InAskLeave InParams = JsonConvert.DeserializeObject<InAskLeave>(Data);
                DBLCAskLeave db = new DBLCAskLeave();

                int nAL = 0;
                nAL=db.AskLeave(InParams.LeaveApply);
                
                if (nAL==0)
                {
                    MD.TrainmanMin tm = new TrainmanMin();
                    MD.TrainmanJLMin tmJL = new TrainmanJLMin();
                    TF.RunSafty.NamePlate.PrepareTMOrder tmOrder = new NamePlate.PrepareTMOrder();
                    DBLinShi.GetTrainman(InParams.LeaveApply.strTrainmanGUID,tm);
                    DBLinShi.GetTrainmanJL(tm.TrainmanJLGUID, tmJL);
                    if (tmJL.TrainmanJLType == 3)
                    {
                        if (TF.RunSafty.NamePlate.LCPrepareTMOrder.IsTurnPrepare(tmJL.TrainmanJLGUID))
                        {
                            if (TF.RunSafty.NamePlate.LCPrepareTMOrder.GetTrainmanOrder(tm.TrainmanNumber, tmOrder))
                            {
                                TF.RunSafty.NamePlate.LCPrepareTMOrder.DeletePrepareTrainmanOrder(tmOrder);
                            }
                        }
                    }
                    output.result = 0;
                    output.resultStr = "请假成功";
                }
                else if (nAL == 1)
                {
                    output.resultStr = "请假失败";
                }
                else if(nAL==2)
                {
                    output.resultStr = "请假失败,该机组人员处于调休状态！";
                }

                //约定年休假类型名为"年休假"
                if (InParams.LeaveApply.strTypeName == "年休假")
                {
                    new DBAnnualLeave().ExecuteLeave(InParams.LeaveApply);
                }
                
            }
            catch (Exception ex)
            {
                output.resultStr = "请假失败:" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 撤销请假记录
        public class InCancelLeave
        {
            //请假记录ID
            public string AskLeaveGUID;
        }
        /// <summary>
        /// 1.8.11 撤销请假记录
        /// </summary>
        public InterfaceOutPut CancelLeave(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InCancelLeave InParams = JsonConvert.DeserializeObject<InCancelLeave>(Data);
                DBLCAskLeave db = new DBLCAskLeave();
                db.CancelLeave(InParams.AskLeaveGUID);

                new DBAnnualLeave().CancelLeave(InParams.AskLeaveGUID);
                output.result = 0;
                output.resultStr = "撤销成功";
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

        #region 查询请假记录
        public class InGetLeaves
        {
            //开始时间
            public string strBeginDateTime = "";
            //结束时间
            public string strEndDateTime = "";
            //请假类型ID
            public string strLeaveTypeGUID = "";
            //状态
            public string strStatus = "";
            //车间ID
            public string strWorkShopGUID = "";
            //职位
            public string strPost = "";
            //机组ID
            public string strGroupGUID = "";
            //工号
            public string strNumber = "";

            public Boolean ShowAllUnEnd = false;
        }

        public class OutGetLeaves
        {
            //请假记录数组
            public AskLeaveWithTypeArray leavesArray = new AskLeaveWithTypeArray();
        }
        /// <summary>
        /// 1.8.12    查询请假记录
        /// </summary>
        public InterfaceOutPut GetLeaves(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetLeaves InParams = JsonConvert.DeserializeObject<InGetLeaves>(Data);
                LeaveMgr_AskLeaveQueryCondition query = new LeaveMgr_AskLeaveQueryCondition();
                query.strBeginDateTime = InParams.strBeginDateTime;
                query.strEndDateTime = InParams.strEndDateTime;
                query.strLeaveTypeGUID = InParams.strLeaveTypeGUID;
                query.strStatus = InParams.strStatus;
                query.strWorkShopGUID = InParams.strWorkShopGUID;
                query.strPost = InParams.strPost;
                query.strGroupGUID = InParams.strGroupGUID;
                query.strNumber = InParams.strNumber;
                query.ShowAllUnEnd = InParams.ShowAllUnEnd;

                OutGetLeaves OutParams = new OutGetLeaves();
                DBLCAskLeave db = new DBLCAskLeave();
                OutParams.leavesArray = db.GetLeavesDataList(query);
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

        #region 给定一个工号，返回该职工的请假信息以及所请假的请假类型名称
        public class InGetValidAskLeaveInfoByID
        {
            //人员ID
            public string strTrainManID;
        }

        public class OutGetValidAskLeaveInfoByID
        {
            //请假信息
            public AskLeaveEntity leaveInfo = new AskLeaveEntity();
            //类型名称
            public string strTypeName;
            //是否存在记录
            public Boolean bExist = new Boolean();
        }
        /// <summary>
        /// 1.8.13    给定一个工号，返回该职工的请假信息以及所请假的请假类型名称
        /// </summary>
        public InterfaceOutPut GetValidAskLeaveInfoByID(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            string strTypeName;
            Boolean bExist;
            try
            {
                InGetValidAskLeaveInfoByID InParams = JsonConvert.DeserializeObject<InGetValidAskLeaveInfoByID>(Data);
                OutGetValidAskLeaveInfoByID OutParams = new OutGetValidAskLeaveInfoByID();
                DBLCAskLeave db = new DBLCAskLeave();
                OutParams.leaveInfo = db.GetLeavesDataListByID(InParams.strTrainManID, out strTypeName, out bExist);
                OutParams.strTypeName = strTypeName;
                OutParams.bExist = bExist;
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

        #region 获取请假明细
        public class InGetAskLeaveDetail
        {
            //请假记录ID
            public string strAskLeaveGUID;
        }

        public class OutGetAskLeaveDetail
        {
            //请假明细
            public AskLeaveDetailEntity AskLeaveDetail = new AskLeaveDetailEntity();
            //是否存在记录
            public Boolean bExist = new Boolean();
        }
        /// <summary>
        /// 1.8.14    获取请假明细
        /// </summary>
        public InterfaceOutPut GetAskLeaveDetail(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            Boolean bExist;
            try
            {
                InGetAskLeaveDetail InParams = JsonConvert.DeserializeObject<InGetAskLeaveDetail>(Data);
                OutGetAskLeaveDetail OutParams = new OutGetAskLeaveDetail();
                DBLCAskLeave db = new DBLCAskLeave();
                OutParams.AskLeaveDetail = db.GetAskLeaveDetailDataList(InParams.strAskLeaveGUID, out bExist);
                OutParams.bExist = bExist;
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

        #region 获取销假明细
        public class InGetCancelLeaveDetail
        {
            //请假记录ID
            public string strAskLeaveGUID;
        }

        public class OutGetCancelLeaveDetail
        {
            //销假明细
            public CancelLeaveDetailEntity CancelLeaveDetail = new CancelLeaveDetailEntity();
            //是否存在记录
            public Boolean bExist = new Boolean();
        }
        /// <summary>
        /// 1.8.15    获取销假明细
        /// </summary>
        public InterfaceOutPut GetCancelLeaveDetail(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            Boolean bExist;
            try
            {
                InGetCancelLeaveDetail InParams = JsonConvert.DeserializeObject<InGetCancelLeaveDetail>(Data);
                OutGetCancelLeaveDetail OutParams = new OutGetCancelLeaveDetail();
                DBLCAskLeave db = new DBLCAskLeave();
                OutParams.CancelLeaveDetail = db.GetCancelLeaveDetailDataList(InParams.strAskLeaveGUID, out bExist);
                OutParams.bExist = bExist;
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

        #region 销假
        public class InAddCancelLeaveDetail
        {
            //销假名细
            public CancelLeaveDetailEntity CancelLeaveDetail = new CancelLeaveDetailEntity();
        }
        /// <summary>
        /// 1.8.16    销假
        /// </summary>
        public InterfaceOutPut AddCancelLeaveDetail(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InAddCancelLeaveDetail InParams = JsonConvert.DeserializeObject<InAddCancelLeaveDetail>(Data);
                DBLCAskLeave db = new DBLCAskLeave();
                db.CancelLeave(InParams.CancelLeaveDetail);
                MD.TrainmanMin tm = new TrainmanMin();
                MD.TrainmanJLMin tmJL = new TrainmanJLMin();
                TF.RunSafty.NamePlate.PrepareTMOrder tmOrder = new NamePlate.PrepareTMOrder();
                DBLinShi.GetTrainman(InParams.CancelLeaveDetail.strTrainmanID, tm);
                DBLinShi.GetTrainmanJL(tm.TrainmanJLGUID, tmJL);
                if (tmJL.TrainmanJLType == 3)
                {
                    if (TF.RunSafty.NamePlate.LCPrepareTMOrder.IsTurnPrepare(tmJL.TrainmanJLGUID))
                    {
                        int maxOrder = TF.RunSafty.NamePlate.LCPrepareTMOrder.GetMaxTMOrder(tmJL.TrainmanJLGUID, tm.PostID);
                        tmOrder.PostID = tm.PostID;
   
                        tmOrder.TrainmanJiaoluGUID = tmJL.TrainmanJLGUID;
                        tmOrder.TrainmanJiaoluName = tmJL.TrainmanJLName;
                        tmOrder.TrainmanName = tm.TrainmanName;
                        tmOrder.TrainmanNumber = tm.TrainmanNumber;
                        tmOrder.TrainmanOrder = maxOrder + 1;
                        
                        TF.RunSafty.NamePlate.LCPrepareTMOrder.AddPrepareTMOrder(tmOrder);
                    }
                }
                output.result = 0;
                output.resultStr = "销假成功";
            }
            catch (Exception ex)
            {
                output.resultStr = "销假失败：" + ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion
    }
    #endregion
}
