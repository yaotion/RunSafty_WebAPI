using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.SiteLogic
{
    public class PSSite
    {
    }


    #region 输出类 类方法
    public class InterfaceRet
    {
        public int result;
        public string resultStr;
        public object data;
        public void Clear()
        {
            result = 0;
            resultStr = "执行成功！";
            data = null;
        }
    }
    #endregion

}
