/**  版本信息模板在安装目录下，可自行修改。
* TAB_Base_Jwd_Address.cs
*
* 功 能： N/A
* 类 名： TAB_Base_Jwd_Address
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-10-18 10:41:06   N/A    初版
*
* Copyright (c) 2014 thinkfreely Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：郑州畅想高科股份有限公司　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace TF.RunSafty.Model
{
	/// <summary>
	/// TAB_Base_Jwd_Address:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TAB_Base_Jwd_Address
    {
        private int m_nId;
        /// <summary>
        /// 主键NID
        /// <summary>
        public int nId
        {
            get { return m_nId; }
            set { m_nId = value; }
        }
        private string m_strJWDNumber;
        /// <summary>
        /// 段号
        /// <summary>
        public string strJWDNumber
        {
            get { return m_strJWDNumber; }
            set { m_strJWDNumber = value; }
        }
        private string m_strJWDName;
        /// <summary>
        /// 机务段名称
        /// <summary>
        public string strJWDName
        {
            get { return m_strJWDName; }
            set { m_strJWDName = value; }
        }
        private string m_strIP;
        /// <summary>
        /// 机务段IP地址
        /// <summary>
        public string strIP
        {
            get { return m_strIP; }
            set { m_strIP = value; }
        }
        private int m_nWebHtmlPort;
        /// <summary>
        /// Web查询网站端口
        /// <summary>
        public int nWebHtmlPort
        {
            get { return m_nWebHtmlPort; }
            set { m_nWebHtmlPort = value; }
        }
        private int m_nWebApiPort;
        /// <summary>
        /// Web接口网站端口
        /// <summary>
        public int nWebApiPort
        {
            get { return m_nWebApiPort; }
            set { m_nWebApiPort = value; }
        }
	}
}

