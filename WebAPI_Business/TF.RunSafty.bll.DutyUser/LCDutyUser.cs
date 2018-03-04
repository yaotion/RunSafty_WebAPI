using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using TF.RunSafty.DutyUser.MD;
using TF.RunSafty.DutyUser.DB;
using System.Data;

namespace TF.RunSafty.DutyUser
{
    #region 值班员接口类
    /// <summary>
    ///类名: LCDutyUser
    ///说明: 值班员接口类
    /// </summary>
    public class LCDutyUser
    {
        #region 根据用户名获取登录人员信息
        public class InGetDutyUserByNumber
        {
            //工号
            public string DutyNumber;
        }

        public class OutGetDutyUserByNumber
        {
            //是否存在
            public Boolean bExist;
            //值班员信息
            public RsDutyUser dutyUser = new RsDutyUser();
        }

        /// <summary>
        /// 1.15.1    根据用户名获取登录人员信息
        /// </summary>
        public InterfaceOutPut GetDutyUserByNumber(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            Boolean bExist;
            try
            {
                InGetDutyUserByNumber InParams = JsonConvert.DeserializeObject<InGetDutyUserByNumber>(Data);
                OutGetDutyUserByNumber OutParams = new OutGetDutyUserByNumber();
                DBDutyUser db = new DBDutyUser();
                OutParams.dutyUser = db.GetDutyUserByNumber(InParams.DutyNumber, out bExist);
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

        /// <summary>
        /// 获取值班员列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public InterfaceOutPut GetDytyUserList(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                DBDutyUser db = new DBDutyUser();
                DataTable dt = db.GetDutyUserList();
                output.data = dt;
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

        #region 修改密码
        public class InResetPassword
        {
            //工号
            public string UserNumber;
            //新密码
            public string NewPassword;
        }

        /// <summary>
        /// 1.15.2    修改密码
        /// </summary>
        public InterfaceOutPut ResetPassword(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InResetPassword InParams = JsonConvert.DeserializeObject<InResetPassword>(Data);
                DBDutyUser db = new DBDutyUser();
                db.ResetPassword(InParams.UserNumber, InParams.NewPassword);
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
