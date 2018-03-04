using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Api.Entity
{

    /// <summary>
    /// 添乘记录实体类
    /// </summary>
    public class ByTimeRecordEntity
    {
        public string flowID { get; set; }
        public string tid { get; set; }
        public string time { get; set; }
        public int verify { get; set; }
        public string remark { get; set; }
        public string cid { get; set; }
        public int byTimeType { get; set; }
    }

}
