using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.bll.SynLog.Model
{
    public class LogQueue_Model
    {
        public int ID { get; set; }
        public int ChangeType { get; set; }
        public string Identifier { get; set; }
        public string Key { get; set; }
        public byte[] data { get; set; }
        public string strdata { get; set; }
    }
}
