using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.TeamGuide.MD
{
    #region 日期最小值
    /// <summary>
    /// 日期最小值
    /// </summary>
    public class DateTimeMinValue
    {
        public static readonly DateTime dtMinValue = Convert.ToDateTime("2000-01-01 00:00:00");
    }
    #endregion

    #region 接口输出类
    /// <summary>
    /// 接口输出类
    /// </summary>
    public class InterfaceOutPut
    {
        public int result { get; set; }
        public string resultStr { get; set; }
        public object data { get; set; }
    }
    #endregion

    #region 简单值对、简单值对数组
    ///类名: SimpleInfo
    ///说明: 简单值对
    /// </summary>
    public class SimpleInfo
    {
        public SimpleInfo()
        { }

        /// <summary>
        /// ID
        /// </summary>
        public string strGUID;

        /// <summary>
        /// 名称
        /// </summary>
        public string strName;
    }
    /// <summary>
    ///类名: SimpleInfoArray
    ///说明: 简单值对数组
    /// </summary>
    public class SimpleInfoArray : List<SimpleInfo>
    {
        public SimpleInfoArray()
        { }

    }
    #endregion
}
