using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ThinkFreely.RunSafty
{


    public class MSGTYPE
    {
        //测酒消息类型
        public static int MSG_DRINK = 10602;
        //请假消息类型
        public static int MSG_LEAVE = 10404;

    }


    /// <summary>
    ///AttentionMsg 的摘要说明
    /// </summary>
    public class AttentionMsg
    {
        #region 属性
        public int msgID = 0;
        public int msgType = 0;
        public string param = "";
        public int clientID = 0;
        #endregion 属性

        public AttentionMsg()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        

       /// <summary>
       /// 普通添加消息的方法，添加钱需要通过消息类型获取客户端id
       /// </summary>
       /// <returns></returns>
        public bool Add()
        {
            string strSql = "insert into TAB_Msg_Mesage (msgType,param,clientID) values (@msgType,@param,@clientID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("param",param),
                                           new SqlParameter("msgType",msgType),
                                           new SqlParameter("clientID",clientID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }

       

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <returns></returns>
        public bool CreatMsg()
        {
            string strSql = @"insert into TAB_Msg_Mesage (msgType,[param],clientID)
 (select @msgType,@param, clientID from TAB_Msg_ClientAttentionConfig where attentionTypeids  like '%" + msgType + "%')";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("param",param),
                                           new SqlParameter("msgType",msgType)
                                       };
            TF.CommonUtility.LogClass.log(strSql);
            TF.CommonUtility.LogClass.log(param + "---" + msgType);
            TF.CommonUtility.LogClass.log("SqlHelper.ConnString:" + SqlHelper.ConnString);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 添加消息（带事务）
        /// </summary>
        /// <returns></returns>
        public bool CreatMsg(SqlTransaction trans)
        {
            string strSql = @"insert into TAB_Msg_Mesage (msgType,[param],clientID)
 (select @msgType,@param, clientID from TAB_Msg_ClientAttentionConfig where ','+attentionTypeids +',' like '%," + msgType + ",%')";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("param",param),
                                           new SqlParameter("msgType",msgType)
                                       };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, sqlParams) > 0;
        }

       /// <summary>
       /// 获取客户端关注消息类别
       /// </summary>
       /// <param name="msgType"></param>
       /// <returns></returns>
        public List<int> GetMsgClients(int msgType)
        {
            List<int> result = new List<int>();
            string sql = "select clientID,attentionTypeids from [TAB_Msg_ClientAttentionConfig]";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //假定所有消息类型ID长度相同，以后需要支持不同长度的ID
                if (dr["attentionTypeids"].ToString().Contains(msgType.ToString()))
                {
                    result.Add(Convert.ToInt32(dr["clientID"]));
                }
            }
            return result;
        }

        public bool Update()
        {
            string strSql = "update TAB_Msg_Mesage set msgType=@msgType,param=@param,clientID=@clientID where msgID=@msgID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("param",param),
                                           new SqlParameter("msgType",msgType),
                                           new SqlParameter("clientID",clientID),
                                           new SqlParameter("msgID",msgID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public static bool Delete(string msgID)
        {
            string strSql = "delete TAB_Msg_Mesage where msgID = @msgID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("msgID",msgID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql,sqlParams) > 0;
        }
        public DataTable GetMsgById(string msgID)
        {
            string strSql = "select * from  TAB_Msg_Mesage where msgID = @msgID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("msgID",msgID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }
       

        public static DataTable GetAllAttentionMsg(int id,int maxCount,int msgID)
        {
            maxCount = maxCount == 0 ? 10 : maxCount;
            string strSql = "select top " + maxCount + " * from TAB_Msg_Mesage where clientID=@clientID and msgID > @msgID order by msgID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("clientID",id),
                                           new SqlParameter("msgID",msgID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static string ReturnStrJson(object o)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(o, timeConverter);
        }


    }
}