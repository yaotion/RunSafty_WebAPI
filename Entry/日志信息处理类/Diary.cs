using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Entry
{
   public class Diary
    {
        #region Model
        private int _id;
        private int? _userid;
        private DateTime? _addtime;
        private string _diarycontent;
        private string _filename;
        private string _fileurl;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DiaryContent
        {
            set { _diarycontent = value; }
            get { return _diarycontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FileUrl
        {
            set { _fileurl = value; }
            get { return _fileurl; }
        }
        #endregion Model

    }
}
