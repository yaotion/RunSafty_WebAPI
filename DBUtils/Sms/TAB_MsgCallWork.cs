/**  版本信息模板在安装目录下，可自行修改。
* TAB_MsgCallWork.cs
*
* 功 能： N/A
* 类 名： TAB_MsgCallWork
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014-10-10 13:06:43   N/A    初版
*
* Copyright (c) 2014 thinkfreely Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：郑州畅想高科股份有限公司　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
namespace TF.RunSafty.DAL
{
    /// <summary>
    /// 数据访问类:TAB_MsgCallWork
    /// </summary>
    public partial class TAB_MsgCallWork
    {
        public TAB_MsgCallWork()
        { }
        #region  BasicMethod




        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TF.RunSafty.Model.TAB_MsgCallWork model)
        {
            string sqlText = "delete from TAB_MsgCallWork where strPlanGUID=@strPlanGUID and strTrainmanGUID=@strTrainmanGUID";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_MsgCallWork(");
            strSql.Append("strMsgGUID,strPlanGUID,strTrainmanGUID,strSendMsgContent,strRecvMsgContent,dtCallTime,nCallTimes,nSendCount,nRecvCount,eCallState,eCallType,dtSendTime,strSendUser,strRecvUser,dtRecvTime)");
            strSql.Append(" values (");
            strSql.Append("@strMsgGUID,@strPlanGUID,@strTrainmanGUID,@strSendMsgContent,@strRecvMsgContent,@dtCallTime,@nCallTimes,@nSendCount,@nRecvCount,@eCallState,@eCallType,@dtSendTime,@strSendUser,@strRecvUser,@dtRecvTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@strMsgGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strSendMsgContent", SqlDbType.VarChar,200),
					new SqlParameter("@strRecvMsgContent", SqlDbType.VarChar,200),
					new SqlParameter("@dtCallTime", SqlDbType.DateTime),
					new SqlParameter("@nCallTimes", SqlDbType.Int,4),
					new SqlParameter("@nSendCount", SqlDbType.Int,4),
					new SqlParameter("@nRecvCount", SqlDbType.Int,4),
					new SqlParameter("@eCallState", SqlDbType.Int,4),
                    new SqlParameter("@eCallType", SqlDbType.Int,4),
                    new SqlParameter("@dtSendTime", SqlDbType.DateTime),
                    new SqlParameter("@strSendUser", SqlDbType.VarChar,50),
                    new SqlParameter("@strRecvUser", SqlDbType.VarChar,50),
                    new SqlParameter("@dtRecvTime", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.strMsgGUID;
            parameters[1].Value = model.strPlanGUID;
            parameters[2].Value = model.strTrainmanGUID;
            parameters[3].Value = model.strSendMsgContent;
            parameters[4].Value = model.strRecvMsgContent;
            parameters[5].Value = model.dtCallTime;
            parameters[6].Value = model.nCallTimes;
            parameters[7].Value = model.nSendCount;
            parameters[8].Value = model.nRecvCount;
            parameters[9].Value = model.eCallState;
            parameters[10].Value = model.eCallType;
            parameters[11].Value = model.dtSendTime;
            parameters[12].Value = model.strSendUser;
            parameters[13].Value = model.strRecvUser;
            parameters[14].Value = model.dtRecvTime;


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, parameters);

            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TF.RunSafty.Model.TAB_MsgCallWork model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_MsgCallWork set ");
            strSql.Append("strMsgGUID=@strMsgGUID,");
            strSql.Append("strPlanGUID=@strPlanGUID,");
            strSql.Append("strTrainmanGUID=@strTrainmanGUID,");
            strSql.Append("strSendMsgContent=@strSendMsgContent,");
            strSql.Append("strRecvMsgContent=@strRecvMsgContent,");
            strSql.Append("dtCallTime=@dtCallTime,");
            strSql.Append("nCallTimes=@nCallTimes,");
            strSql.Append("nSendCount=@nSendCount,");
            strSql.Append("nRecvCount=@nRecvCount,");
            strSql.Append("eCallState=@eCallState");
            strSql.Append(" where nId=@nId");
            SqlParameter[] parameters = {
					new SqlParameter("@strMsgGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strSendMsgContent", SqlDbType.VarChar,200),
					new SqlParameter("@strRecvMsgContent", SqlDbType.VarChar,200),
					new SqlParameter("@dtCallTime", SqlDbType.DateTime),
					new SqlParameter("@nCallTimes", SqlDbType.Int,4),
					new SqlParameter("@nSendCount", SqlDbType.Int,4),
					new SqlParameter("@nRecvCount", SqlDbType.Int,4),
					new SqlParameter("@eCallState", SqlDbType.Int,4),
					new SqlParameter("@nId", SqlDbType.Int,4)};
            parameters[0].Value = model.strMsgGUID;
            parameters[1].Value = model.strPlanGUID;
            parameters[2].Value = model.strTrainmanGUID;
            parameters[3].Value = model.strSendMsgContent;
            parameters[4].Value = model.strRecvMsgContent;
            parameters[5].Value = model.dtCallTime;
            parameters[6].Value = model.nCallTimes;
            parameters[7].Value = model.nSendCount;
            parameters[8].Value = model.nRecvCount;
            parameters[9].Value = model.eCallState;
            parameters[10].Value = model.nId;

            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int nId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_MsgCallWork ");
            strSql.Append(" where nId=@nId");
            SqlParameter[] parameters = {
					new SqlParameter("@nId", SqlDbType.Int,4)
			};
            parameters[0].Value = nId;

            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string TrainPlanGUID, string TrainmanGUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_MsgCallWork ");
            strSql.Append(" where strPlanGUID=@strPlanGUID and strTrainmanGUID=@strTrainmanGUID");
            SqlParameter[] parameters = {
					new SqlParameter("@strPlanGUID", TrainPlanGUID),
                    new SqlParameter("@strTrainmanGUID", TrainmanGUID)
			};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string nIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_MsgCallWork ");
            strSql.Append(" where nId in (" + nIdlist + ")  ");
            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.TAB_MsgCallWork GetModel(int nId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 nId,strMsgGUID,strPlanGUID,strTrainmanGUID,strSendMsgContent,strRecvMsgContent,dtCallTime,nCallTimes,nSendCount,nRecvCount,eCallState from TAB_MsgCallWork ");
            strSql.Append(" where nId=@nId");
            SqlParameter[] parameters = {
					new SqlParameter("@nId", SqlDbType.Int,4)
			};
            parameters[0].Value = nId;

            TF.RunSafty.Model.TAB_MsgCallWork model = new TF.RunSafty.Model.TAB_MsgCallWork();
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        public DataSet GetAllMsg(string StartState, string EndState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM VIEW_MsgCallWork ");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.TAB_MsgCallWork DataRowToModelForAllMsg(DataRow row)
        {
            TF.RunSafty.Model.TAB_MsgCallWork model = new TF.RunSafty.Model.TAB_MsgCallWork();
            if (row != null)
            {
                if (row["nId"] != null && row["nId"].ToString() != "")
                {
                    model.nId = int.Parse(row["nId"].ToString());
                }
                if (row["strMsgGUID"] != null)
                {
                    model.strMsgGUID = row["strMsgGUID"].ToString();
                }
                if (row["strPlanGUID"] != null)
                {
                    model.strPlanGUID = row["strPlanGUID"].ToString();
                }



                if (row["strTrainmanGUID"] != null)
                {
                    model.strTrainmanGUID = row["strTrainmanGUID"].ToString();
                }

                if (row["strTrainmanName"] != null)
                {
                    model.strTrainmanName = row["strTrainmanName"].ToString();
                }

                if (row["strTrainmanNumber"] != null)
                {
                    model.strTrainmanNumber = row["strTrainmanNumber"].ToString();
                }

                if (row["strMobileNumber"] != null)
                {
                    model.strMobileNumber = row["strMobileNumber"].ToString();
                }



                if (row["dtSendTime"] != null)
                {
                    model.dtSendTime = row["dtSendTime"].ToString();
                }
                if (row["strSendUser"] != null)
                {
                    model.strSendUser = row["strSendUser"].ToString();
                }
                if (row["strRecvUser"] != null)
                {
                    model.strRecvUser = row["strRecvUser"].ToString();
                }
                if (row["dtRecvTime"] != null)
                {
                    model.dtRecvTime = row["dtRecvTime"].ToString();
                }


                if (row["eCallType"] != null)
                {
                    model.eCallType = row["eCallType"].ToString();
                }


                if (row["dtRealStartTime"] != null)
                {
                    model.dtStartTime = row["dtRealStartTime"].ToString();
                }


                if (row["dtStartTime"] != null)
                {
                    model.dtChuQinTime = row["dtStartTime"].ToString();
                }

                if (row["strTrainNo"] != null)
                {
                    model.strTrainNo = row["strTrainNo"].ToString();
                }



                if (row["strSendMsgContent"] != null)
                {
                    model.strSendMsgContent = row["strSendMsgContent"].ToString();
                }
                if (row["strRecvMsgContent"] != null)
                {
                    model.strRecvMsgContent = row["strRecvMsgContent"].ToString();
                }
                if (row["dtCallTime"] != null && row["dtCallTime"].ToString() != "")
                {
                    model.dtCallTime = DateTime.Parse(row["dtCallTime"].ToString());
                }
                if (row["nCallTimes"] != null && row["nCallTimes"].ToString() != "")
                {
                    model.nCallTimes = int.Parse(row["nCallTimes"].ToString());
                }

                if (row["nSendCount"] != null && row["nSendCount"].ToString() != "")
                {
                    model.nSendCount = int.Parse(row["nSendCount"].ToString());
                }

                if (row["nRecvCount"] != null && row["nRecvCount"].ToString() != "")
                {
                    model.nRecvCount = int.Parse(row["nRecvCount"].ToString());
                }
                if (row["eCallState"] != null && row["eCallState"].ToString() != "")
                {
                    model.eCallState = int.Parse(row["eCallState"].ToString());
                }
            }
            return model;
        }









        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.TAB_MsgCallWork DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.TAB_MsgCallWork model = new TF.RunSafty.Model.TAB_MsgCallWork();
            if (row != null)
            {
                if (row["nId"] != null && row["nId"].ToString() != "")
                {
                    model.nId = int.Parse(row["nId"].ToString());
                }
                if (row["strMsgGUID"] != null)
                {
                    model.strMsgGUID = row["strMsgGUID"].ToString();
                }
                if (row["strPlanGUID"] != null)
                {
                    model.strPlanGUID = row["strPlanGUID"].ToString();
                }
                if (row["strTrainmanGUID"] != null)
                {
                    model.strTrainmanGUID = row["strTrainmanGUID"].ToString();
                }
                if (row["strSendMsgContent"] != null)
                {
                    model.strSendMsgContent = row["strSendMsgContent"].ToString();
                }
                if (row["strRecvMsgContent"] != null)
                {
                    model.strRecvMsgContent = row["strRecvMsgContent"].ToString();
                }
                if (row["dtCallTime"] != null && row["dtCallTime"].ToString() != "")
                {
                    model.dtCallTime = DateTime.Parse(row["dtCallTime"].ToString());
                }
                if (row["nCallTimes"] != null && row["nCallTimes"].ToString() != "")
                {
                    model.nCallTimes = int.Parse(row["nCallTimes"].ToString());
                }

                if (row["nSendCount"] != null && row["nSendCount"].ToString() != "")
                {
                    model.nSendCount = int.Parse(row["nSendCount"].ToString());
                }

                if (row["nRecvCount"] != null && row["nRecvCount"].ToString() != "")
                {
                    model.nRecvCount = int.Parse(row["nRecvCount"].ToString());
                }
                if (row["eCallState"] != null && row["eCallState"].ToString() != "")
                {
                    model.eCallState = int.Parse(row["eCallState"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nId,strGUID,strPlanGUID,strTrainmanGUID,strSendMsgContent,strRecvMsgContent,dtCallTime,nCallTimes,dtSendMsgTime,nSendCount,DtRecvMsgTime,nRecvCount,eCallState ");
            strSql.Append(" FROM TAB_MsgCallWork ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }




        public DataTable QueryMsgCallWorkList(int pageIndex, int pageCount, string dtBeginSendTime, string dtEndSendTime, string IsVisOk)
        {
            StringBuilder strSqlWhere = new StringBuilder();

            if (dtBeginSendTime != "")
                strSqlWhere.Append(" and dtSendTime>'" + dtBeginSendTime + "'");

            if (dtEndSendTime != "")
                strSqlWhere.Append(" and dtSendTime<'" + dtEndSendTime + "'");



            if (IsVisOk == "")
                strSqlWhere.Append(" and nCancel='0'");



            string strSql = @"select top " + pageCount.ToString()
                + " * from VIEW_MsgCallWork where nID not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" nID from VIEW_MsgCallWork where 1=1"
                + strSqlWhere.ToString() + " order by nID desc)" + strSqlWhere.ToString() + " order by nID desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }


        public int QueryMsgCallWorkListCount(string dtBeginSendTime, string dtEndSendTime, string IsVisOk)
        {

            StringBuilder strSqlWhere = new StringBuilder();


            if (dtBeginSendTime != "")
                strSqlWhere.Append(" and dtSendTime>'" + dtBeginSendTime + "'");

            if (dtEndSendTime != "")
                strSqlWhere.Append(" and dtSendTime<'" + dtEndSendTime + "'");

            if (IsVisOk == "")
                strSqlWhere.Append(" and nCancel='0'");





            string strSql = "select count(*) from VIEW_MsgCallWork where 1=1  " + strSqlWhere + "";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }





        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" nId,strGUID,strPlanGUID,strTrainmanGUID,strSendMsgContent,strRecvMsgContent,dtCallTime,nCallTimes,dtSendMsgTime,nSendCount,DtRecvMsgTime,nRecvCount,eCallState ");
            strSql.Append(" FROM TAB_MsgCallWork ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM TAB_MsgCallWork ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.nId desc");
            }
            strSql.Append(")AS Row, T.*  from TAB_MsgCallWork T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "TAB_MsgCallWork";
            parameters[1].Value = "nId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/


        //添加不验卡车次
        public int AddNoCardSet(string checi, string mark)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_NoVeifyCardTrain(");
            strSql.Append("strTrainNo,strMark)");
            strSql.Append(" values (");
            strSql.Append("'" + checi + "','" + mark + "')");

            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        //不验卡车次删除
        public static bool Delete(string nID)
        {
            string strSql = "delete Tab_NoVeifyCardTrain where nID = @nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",nID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }




        //添加消息类别
        public int AddClientFocu(string TypeName, int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Msg_AttentionType(");
            strSql.Append("TypeName,TypeID)");
            strSql.Append(" values (");
            strSql.Append("'" + TypeName + "'," + id + ")");

            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        /// <summary>
        /// 删除消息类别
        /// </summary>
        public static bool DeleteClientFocu(string nid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Msg_AttentionType ");
            strSql.Append(" where nID=" + nid + "");

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString()) > 0;
        }




        #endregion  BasicMethod
        #region  ExtensionMethod


        #endregion  ExtensionMethod
    }
}

