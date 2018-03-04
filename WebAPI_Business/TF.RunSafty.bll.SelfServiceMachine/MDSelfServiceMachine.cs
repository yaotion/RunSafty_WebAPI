using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.SelfServiceMachine
{
   public class MDSelfServiceMachine
    {


       public class MDReadHistory
       {
           #region Model
           private int _nid;
           private string _strfileguid;
           private string _strtrainmanguid;
           private string _dtreadtime;
           private string _siteguid;
           /// <summary>
           /// 
           /// </summary>
           public int nId
           {
               set { _nid = value; }
               get { return _nid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public string strFileGUID
           {
               set { _strfileguid = value; }
               get { return _strfileguid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public string strTrainmanGUID
           {
               set { _strtrainmanguid = value; }
               get { return _strtrainmanguid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public string DtReadTime
           {
               set { _dtreadtime = value; }
               get { return _dtreadtime; }
           }
           /// <summary>
           /// 
           /// </summary>
           public string SiteGUID
           {
               set { _siteguid = value; }
               get { return _siteguid; }
           }
           #endregion Model
       }

       public class MDDeliverJSPrint
       {
           #region Model
           private int _nid;
           private string _strtrainmanguid;
           private string _strplanguid;
           private string _strsiteguid;
           private DateTime? _dtprinttime;
           /// <summary>
           /// 
           /// </summary>
           public int nID
           {
               set { _nid = value; }
               get { return _nid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public string StrTrainmanGUID
           {
               set { _strtrainmanguid = value; }
               get { return _strtrainmanguid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public string StrPlanGUID
           {
               set { _strplanguid = value; }
               get { return _strplanguid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public string StrSiteGUID
           {
               set { _strsiteguid = value; }
               get { return _strsiteguid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public DateTime? dtPrintTime
           {
               set { _dtprinttime = value; }
               get { return _dtprinttime; }
           }
           #endregion Model
       }

       public class MDReadDocPlan
       {
           #region Model
           private int _nid;
           private string _strtrainmanguid;
           private string _strfileguid;
           private int? _nreadcount;
           private DateTime? _dtfirstreadtime;
           private DateTime? _dtlastreadtime;
           /// <summary>
           /// 
           /// </summary>
           public int nId
           {
               set { _nid = value; }
               get { return _nid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public string StrTrainmanGUID
           {
               set { _strtrainmanguid = value; }
               get { return _strtrainmanguid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public string StrFileGUID
           {
               set { _strfileguid = value; }
               get { return _strfileguid; }
           }
           /// <summary>
           /// 
           /// </summary>
           public int? NReadCount
           {
               set { _nreadcount = value; }
               get { return _nreadcount; }
           }
           /// <summary>
           /// 
           /// </summary>
           public DateTime? DtFirstReadTime
           {
               set { _dtfirstreadtime = value; }
               get { return _dtfirstreadtime; }
           }
           /// <summary>
           /// 
           /// </summary>
           public DateTime? DtLastReadTime
           {
               set { _dtlastreadtime = value; }
               get { return _dtlastreadtime; }
           }
           #endregion Model
       }



    }
}
