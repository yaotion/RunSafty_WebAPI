using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Api.Entity
{
    /// <summary>
    /// 考试记录实体类
    /// </summary>
    public class ExamRecordEntity
    {
        public string recordID { get; set; }
        public string trainmanID { get; set; }
        public string flowID { get; set; }
        public string planID { get; set; }
        public int workType { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int questionCount { get; set; }
        public int correctCount { get; set; }
        public int score { get; set; }
        public int totalScore { get; set; }
    }
}
