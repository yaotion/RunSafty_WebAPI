using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.bll.SynLog.Model
{
    public class ChangeVersion_Model
    {
        public string Identifier { get; set; }
        public int MinVersion { get; set; }
        public int MaxVersion { get; set; }
    }
}
