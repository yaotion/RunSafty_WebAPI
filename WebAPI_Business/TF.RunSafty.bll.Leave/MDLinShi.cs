using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Leave.MD
{

    public class TrainmanMin
    {
        public string TrainmanGUID = "";
        public string TrainmanNumber = "";
        public string TrainmanName = "";
        public string TrainmanJLGUID = "";
        public int PostID = 0;
        public int TrainmanState = 0;
    }
    public class TrainmanJLMin
    {
        public string TrainmanJLGUID = "";
        public string TrainmanJLName = "";
        public int TrainmanJLType = 0;
    }
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
    /// <summary>
    /// 返回布尔类型输出类
    /// </summary>
    public class ResultOutPut
    {
        //结果
        public Boolean result = new Boolean();
    }
    #endregion
}
