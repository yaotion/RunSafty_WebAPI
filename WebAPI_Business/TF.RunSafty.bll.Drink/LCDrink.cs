using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using TF.RunSafty.Drink.MD;
using TF.RunSafty.Drink.DB;

namespace TF.RunSafty.Drink
{
    #region 测酒接口类
    /// <summary>
    ///类名: LCDrink
    ///说明: 测酒接口类
    /// </summary>
    public class LCDrink
    {
        #region 获取测酒信息
        public class InQueryDrink
        {
            //查询参数\
            public DrinkQueryParam QueryParam = new DrinkQueryParam();
        }

        public class OutQueryDrink
        {
            //测酒结果数组
            public DrinkInfoArray drinkInfoArray = new DrinkInfoArray();
        }

        /// <summary>
        /// 获取测酒信息
        /// </summary>
        public InterfaceOutPut QueryDrink(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InQueryDrink InParams = JsonConvert.DeserializeObject<InQueryDrink>(Data);
                OutQueryDrink OutParams = new OutQueryDrink();
                DBLCDrink db = new DBLCDrink();
                OutParams.drinkInfoArray = db.GetDrinkDataList(InParams.QueryParam);
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

        #region 上传测酒记录
        public class InAddDrinkInfo
        {
            //测酒信息
            public DrinkInfo drinkInfo = new DrinkInfo();
        }

        /// <summary>
        /// 上传测酒记录
        /// </summary>
        public InterfaceOutPut AddDrinkInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InAddDrinkInfo InParams = JsonConvert.DeserializeObject<InAddDrinkInfo>(Data);
                DBLCDrink db = new DBLCDrink();

                //职位信息----- 开始----------
                DrinkLogic.DBDrinkLogic dbdl = new DrinkLogic.DBDrinkLogic();
                DrinkLogic.MDDrinkLogic mddl = new DrinkLogic.MDDrinkLogic();
                mddl = dbdl.GetDrinkCadreEntity(InParams.drinkInfo.strTrainmanNumber);
                if (mddl != null)
                {
                    InParams.drinkInfo.strDepartmentID = mddl.strDepartmentID;
                    InParams.drinkInfo.strDepartmentName = mddl.strDepartmentName;
                    InParams.drinkInfo.nCadreTypeID = mddl.nCadreTypeID;
                    InParams.drinkInfo.strCadreTypeName = mddl.strCadreTypeName;
                }
                else
                {
                    InParams.drinkInfo.strDepartmentID = "";
                    InParams.drinkInfo.strDepartmentName = "";
                    InParams.drinkInfo.nCadreTypeID = "";
                    InParams.drinkInfo.strCadreTypeName = "";
                }
                string DrinkGUID = "";
                //职位信息----- 结束----------
                if (!db.ExistDrinkInfo(InParams.drinkInfo.strTrainmanGUID, InParams.drinkInfo.dtCreateTime.Value, out DrinkGUID))
                {
                    db.AddDrinkInfo(InParams.drinkInfo);
                }
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

        #region 获取测酒明细
        public class InGetDrinkInfo
        {
            //记录ID
            public string strGUID;
        }

        public class OutGetDrinkInfo
        {
            //测酒记录
            public DrinkInfo drinkInfo = new DrinkInfo();
            //是否存在记录
            public Boolean bExist = new Boolean();
        }

        /// <summary>
        /// 获取测酒明细
        /// </summary>
        public InterfaceOutPut GetDrinkInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            Boolean bExist;
            try
            {
                InGetDrinkInfo InParams = JsonConvert.DeserializeObject<InGetDrinkInfo>(Data);
                OutGetDrinkInfo OutParams = new OutGetDrinkInfo();
                DBLCDrink db = new DBLCDrink();
                OutParams.drinkInfo = db.GetDrinkInfo(InParams.strGUID, out bExist);
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

        #region 根据车次和客户端获取测酒记录
        public class InGetTrainNoDrinkInfo
        {
            //开始时间
            public DateTime dtBeginTime;
            //车次
            public string strTrainNo;
            //客户端编号
            public string strSiteNumber;
            //返回记录条数
            public int ncount;
        }

        public class OutGetTrainNoDrinkInfo
        {
            //测酒结果
            public DrinkInfoArray drinkInfoArray = new DrinkInfoArray();
        }

        /// <summary>
        /// 根据车次和客户端获取测酒记录
        /// </summary>
        public InterfaceOutPut GetTrainNoDrinkInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InGetTrainNoDrinkInfo InParams = JsonConvert.DeserializeObject<InGetTrainNoDrinkInfo>(Data);
                OutGetTrainNoDrinkInfo OutParams = new OutGetTrainNoDrinkInfo();
                DBLCDrink db = new DBLCDrink();
                OutParams.drinkInfoArray = db.GetTrainNoDrinkInfo(InParams.dtBeginTime, InParams.strTrainNo, InParams.strSiteNumber, InParams.ncount);
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

        #region 获取没有出勤计划的测酒记录
        public class InQueryNoPlanDrink
        {
            //开始时间
            public DateTime dtBeginTime;
            //结束时间
            public DateTime dtEndTime;
            //人员工号
            public string TrainmanNumber;
            //测酒类型
            public int DrinkTypeID;
        }

        public class OutQueryNoPlanDrink
        {
            //测酒结果
            public DrinkInfoArray drinkInfoArray = new DrinkInfoArray();
        }

        /// <summary>
        /// 获取没有出勤计划的测酒记录
        /// </summary>
        public InterfaceOutPut QueryNoPlanDrink(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InQueryNoPlanDrink InParams = JsonConvert.DeserializeObject<InQueryNoPlanDrink>(Data);
                OutQueryNoPlanDrink OutParams = new OutQueryNoPlanDrink();
                DBLCDrink db = new DBLCDrink();
                OutParams.drinkInfoArray = db.QueryNoPlanDrink(InParams.dtBeginTime, InParams.dtEndTime, InParams.TrainmanNumber, InParams.DrinkTypeID);
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

        #region 据客户端获取从某个时间开始的某个人的最后一条测酒记录
        public class InGetTMLastDrinkInfo
        {
            //客户端编号
            public string strSiteNumber;
            //人员工号
            public string strTrainmanNumber;
            //开始时间
            public DateTime dtStartTime;
        }

        public class OutGetTMLastDrinkInfo
        {
            //测酒记录
            public DrinkInfo drinkInfo = new DrinkInfo();
            //是否存在记录
            public Boolean bExist = new Boolean();
        }

        /// <summary>
        /// 据客户端获取从某个时间开始的某个人的最后一条测酒记录
        /// </summary>
        public InterfaceOutPut GetTMLastDrinkInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            Boolean bExist;
            try
            {
                InGetTMLastDrinkInfo InParams = JsonConvert.DeserializeObject<InGetTMLastDrinkInfo>(Data);
                OutGetTMLastDrinkInfo OutParams = new OutGetTMLastDrinkInfo();
                DBLCDrink db = new DBLCDrink();
                OutParams.drinkInfo = db.GetTMLastDrinkInfo(InParams.strSiteNumber,InParams.strTrainmanNumber,InParams.dtStartTime,out bExist);
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

        #region 获取测酒记录
        public class InGetTrainmanDrinkInfo
        {
            //人员ID
            public string strTrainmanGUID;
            //计划ID
            public string strTrainPlanGUID;
            //工作类型
            public int WorkType;
        }

        public class OutGetTrainmanDrinkInfo
        {
            //测酒记录
            public DrinkInfo drinkInfo = new DrinkInfo();
            //是否存在记录
            public Boolean bExist = new Boolean();
        }

        /// <summary>
        /// 获取测酒记录
        /// </summary>
        public InterfaceOutPut GetTrainmanDrinkInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            Boolean bExist;
            try
            {
                InGetTrainmanDrinkInfo InParams = JsonConvert.DeserializeObject<InGetTrainmanDrinkInfo>(Data);
                OutGetTrainmanDrinkInfo OutParams = new OutGetTrainmanDrinkInfo();
                DBLCDrink db = new DBLCDrink();
                OutParams.drinkInfo = db.GetTrainmanDrinkInfo(InParams.strTrainmanGUID,InParams.strTrainPlanGUID,InParams.WorkType,out bExist);
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
    }
    #endregion
}
