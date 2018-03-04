using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.RunSafty.Utils.Parse
{
    public class TFParse
    {
        public static bool DBToInt(object DBData,ref int IntValue)
        {
            if (DBData == null)
            {
                return false;
            }
            int nTemp;
            if (!int.TryParse(DBData.ToString(), out nTemp))
            {
                return false;
            }
            IntValue = nTemp;
            return true;
        }

        public static int DBToInt(object DBData, int DefaultValue)
        {
            if (DBData == null)
            {
                return DefaultValue;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return DefaultValue;
            }
            int nTemp;
            if (!int.TryParse(DBData.ToString(), out nTemp))
            {
                return DefaultValue;
            }
            return nTemp;            
        }

        public static DateTime DBToDateTime(object DBData, DateTime DefaultValue)
        {
            if (DBData == null)
            {
                return DefaultValue;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return DefaultValue;
            }
            DateTime dtTemp;
            if (!DateTime.TryParse(DBData.ToString(), out dtTemp))
            {
                return DefaultValue;
            }
            return dtTemp;
        }

        public static string DBTimeToSerialString(object DBData)
        {
            return DBToDateTime(DBData, DateTime.Parse("1899-01-01")).ToString("yyyy-MM-dd HH:mm:ss"); 
        }

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
            resultStr = string.Empty;
            data = null;
        }
    }
    #endregion
}
