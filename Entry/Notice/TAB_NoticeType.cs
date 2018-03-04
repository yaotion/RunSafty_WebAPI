using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace TF.RunSafty.Model{
	 	//TAB_NoticeType
		public class TAB_NoticeType
	{
   		     
      	/// <summary>
		/// nid
        /// </summary>		
		private int _nid;
        public int nid
        {
            get{ return _nid; }
            set{ _nid = value; }
        }        
		/// <summary>
		/// strTypeGUID
        /// </summary>		
		private string _strtypeguid;
        public string strTypeGUID
        {
            get{ return _strtypeguid; }
            set{ _strtypeguid = value; }
        }        
		/// <summary>
		/// strTypeName
        /// </summary>		
		private string _strtypename;
        public string strTypeName
        {
            get{ return _strtypename; }
            set{ _strtypename = value; }
        }        
		   
	}
}

