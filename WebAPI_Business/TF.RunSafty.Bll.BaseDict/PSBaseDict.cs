using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.BaseDict
{
    public class PSBaseDict
    {
        #region 输出类 类方法
        public class InterfaceRet
        {
            public int result;
            public string resultStr;
            public object data;
            public void Clear()
            {
                result = 0;
                resultStr = string.Empty;
                data = null;
            }
        }
        #endregion
    }
}
