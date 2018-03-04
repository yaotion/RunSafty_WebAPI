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

    }
}
