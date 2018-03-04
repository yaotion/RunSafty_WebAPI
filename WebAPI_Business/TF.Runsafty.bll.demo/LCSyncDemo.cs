using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.SyncDemo
{
    //接口返回信息
    public class InterfaceRet
    {
        public int result;
        public string resultStr;
        public object data;
        public void Clear()
        {
            result = 0;
            resultStr = string.Empty;
            data = null;
        }
    }

    //接口基类
    public class InterfaceBase
    {
        protected InterfaceRet _ret = new InterfaceRet();
    }

    
    public class DataVersion
    {
        public string Identifier;
        public int Version;            
    }



    public class SyncAPI : InterfaceBase
    {        
        /// <summary>
        /// 传入需要更新各类数据的版本号，返回最新版本号或是需要整体更新
        /// 输入参数DataVersion包含客户端数据版本号
        /// 返回DataVersion包含最新版本号
        /// 版本号为0时表示没有变动数据
        /// 版本号为-1时表示版本过期
        /// </summary>
        /// <returns></returns>
        public InterfaceRet ReadDataVersion(string data)
        { 
            _ret.Clear();


            List<DataVersion> versions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataVersion>>(data);

            int minVersion, maxVersion;


            foreach (var version in versions)
            {                
                ChangeLogNotifyer.GetVersionRange(version.Identifier, out minVersion, out maxVersion);

                if (minVersion == 0 && maxVersion == 0)//没有数据变动
                {
                    version.Version = 0;
                }
                else
                    if (version.Version < minVersion)//数据版本过期
                    {
                        version.Version = -1;
                    }
                    else
                        if (version.Version > maxVersion)//异常版本
                        {
                            throw new Exception(string.Format("{0} 数据版本{1}大于服务器数据版本{2}", version.Identifier, version.Version, maxVersion));
                        }
                        else
                        {
                            version.Version = maxVersion;                        
                        }


            }
            
            _ret.data = versions;
            return _ret;
        }


        public InterfaceRet GetNewestVersion(string data)
        {
            _ret.Clear();
            int minVersion, maxVersion;
            ChangeLogNotifyer.GetVersionRange(data, out minVersion, out maxVersion);
            _ret.data = maxVersion;
            return _ret;        
        }
    }


    public class RsTM
    {
        public string ID;
        public string Number;
        public string Name;
        public string Tel;        
        public string Jl;
    }

    public class SyncObj
    {
        public ChangeLog Log;
        public object Obj;
    }
    public class SyncTM : InterfaceBase
    {
        private DBTMAccess dbAccess = new DBTMAccess();
        /// <summary>
        /// 传入客户端数据版本号，接口判断版本是否过期，如过期则返回异常
        /// 未过期则返回变动的数据及变动类型
        /// 如变动数据量很大可只返回部分数据
        /// </summary>
        /// <returns></returns>
        public InterfaceRet ReadData(string data)
        {
            DataVersion version = Newtonsoft.Json.JsonConvert.DeserializeObject<DataVersion>(data);
            if (ChangeLogNotifyer.CheckVersionExpires(version))
            {
                throw new Exception("数据版本已过期");
            }


            ChangeLogNotifyer logNotifyer = new ChangeLogNotifyer();
            List<ChangeLog> logs = new List<ChangeLog>();
            logNotifyer.GetChangeLog(version.Identifier, version.Version + 1, 10, logs);

            SyncObj syncobj;

            List<SyncObj> retList = new List<SyncObj>();

            foreach (var log in logs)
            { 
                syncobj = new SyncObj();
                retList.Add(syncobj);
                syncobj.Log = log;

                if (log.ChangeType != ChangeType.ctDel)
                {
                    syncobj.Obj = dbAccess.Get(log.Key);
                }
                
                
            }
            _ret.data = retList;
            return _ret;
        }

        /// <summary>
        /// 更新人员
        /// </summary>
        /// <returns></returns>
        public InterfaceRet Update(string data)
        {
            RsTM tm = Newtonsoft.Json.JsonConvert.DeserializeObject<RsTM>(data);
            dbAccess.Update(tm);
            return _ret;
        }
        /// <summary>
        /// 添加人员
        /// </summary>
        /// <returns></returns>
        public InterfaceRet Add(string data)
        {
            RsTM tm = Newtonsoft.Json.JsonConvert.DeserializeObject<RsTM>(data);
            dbAccess.Add(tm);
            return _ret;
        }

        /// <summary>
        /// 删除人员
        /// </summary>
        /// <returns></returns>
        public InterfaceRet Del(string data)
        {
            //RsTM tm = Newtonsoft.Json.JsonConvert.DeserializeObject<RsTM>(data);
            dbAccess.Del(data);
            return _ret;
        }

        public InterfaceRet GetAll(string data)
        {
            _ret.data = dbAccess.GetAll();
            return _ret;
        }
    }


    public class SyncUser : InterfaceBase
    { 
    
    }

   
}
