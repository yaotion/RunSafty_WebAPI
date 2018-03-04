using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.bll.SynLog.Model
{
    public class ChangeLog_Model
    {
        public int ID { get; set; }
        public int ChangeType { get; set; }
        public string Identifier { get; set; }
        public string Key { get; set; }
        public int Version { get; set; }
        public byte[] Data { get; set; }
    }

    public class ChangeLog_Model_RT
    {
        public int ID { get; set; }
        public int ChangeType { get; set; }
        public string Identifier { get; set; }
        public string Key { get; set; }
        public int Version { get; set; }
        public object Data { get; set; }
    }
}
