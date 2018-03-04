using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.DutyUser.MD
{
    #region 值班员信息
    /// <summary>
    ///类名: RsDutyUser
    ///说明: 值班员信息
    /// </summary>
    public class RsDutyUser
    {
        public RsDutyUser()
        { }

        /// <summary>
        /// 值班员GUID
        /// </summary>
        public string strDutyGUID;

        /// <summary>
        /// 工号
        /// </summary>
        public string strDutyNumber;

        /// <summary>
        /// 姓名
        /// </summary>
        public string strDutyName;

        /// <summary>
        /// 密码
        /// </summary>
        public string strPassword;
    }
    #endregion
}
