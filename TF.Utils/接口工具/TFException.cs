using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Utils
{

    public class TFException : ApplicationException
    {
        public TFException(string message): base(message)
        {
        }
        public int ErrorCode = 0;
    }
}
