using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.bll.SynLog.Model;
using TF.RunSafty.bll.SynLog;

namespace TF.RunSafty.SynLog
{
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

    public class DataVersion
    {
        public string Identifier;
        public int Version;
    }
    public class LCSynLog
    {

        InterfaceRet _ret = new InterfaceRet();
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
            DataVersion version = Newtonsoft.Json.JsonConvert.DeserializeObject<DataVersion>(data);

            //为果传入的版本号为0 则返回结果为-1
            if (version.Version == 0)
            {
                version.Version = -1;
                _ret.data = version;
                return _ret;
            }
            int minVersion, maxVersion;
            DBSynLog.GetVersionRange(version.Identifier, out minVersion, out maxVersion);

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
            _ret.data = version;
            return _ret;
        }


        public class Get_ChangeLog
        {
            public int result;
            public string resultStr;
            public object data;
        }
        /// <summary>
        /// 获取变动记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Get_ChangeLog GetChangeLog(string data)
        {
            DataVersion m = Newtonsoft.Json.JsonConvert.DeserializeObject<DataVersion>(data);

            List<ChangeLog_Model> cm = DBSynLog.GetChangeLog(m.Identifier, m.Version);
            List<ChangeLog_Model_RT> rtmm = new List<ChangeLog_Model_RT>();
            foreach (var c in cm)
            {
                ChangeLog_Model_RT rt = new ChangeLog_Model_RT();
                rt.ChangeType = c.ChangeType;
                rt.ID = c.ID;
                rt.Identifier = c.Identifier;
                rt.Key = c.Key;
                rt.Version = c.Version;
                string strData = System.Text.Encoding.UTF8.GetString(c.Data);
                rt.Data = string.IsNullOrEmpty(strData) ? null : Newtonsoft.Json.Linq.JObject.Parse(System.Text.Encoding.UTF8.GetString(c.Data));
                rtmm.Add(rt);
            }

            Get_ChangeLog rtm = new Get_ChangeLog();
            rtm.result = 0;
            rtm.resultStr = "";
            // rtm.data = DBSynLog.GetChangeLog(m.Identifier, m.Version);
            rtm.data = rtmm;
            return rtm;
            // return null;
        }


        /// <summary>
        /// 获取最新版本
        /// </summary>
        /// <param name="Identifier"></param>
        /// <returns></returns>
        public InterfaceRet GetNewestVersion(string Identifier)
        {
            _ret.Clear();
            int minVersion, maxVersion;
            DBSynLog.GetVersionRange(Identifier, out minVersion, out maxVersion);
            _ret.data = maxVersion;
            return _ret;
        }
    }
}
