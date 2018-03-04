using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.NamePlate
{
    public class LCNameplateLimit
    {
        public static bool GetUserLimit(string UserNumber, NameplateLimit Limit)
        {
            return DBNameplateLimit.GetUserLimit(UserNumber, Limit);
        }
    }
}
