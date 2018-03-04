using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using TF.RunSafty.DBUtils;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.DAL
{
    public class ExcelAdd
    {
        /// <summary>
        /// 获取那个最大的typeid
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public string GetMaxId()
        {
            string strSql = "select max(nID) as maxId from Tab_Excel";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
            {
                return "0";
            }
            else 
            {
                return ds.Tables[0].Rows[0]["maxId"].ToString();
            }
        }


        //获取所有excel的数量
        public DataTable GetAllExcelDataCount(string strType)
        {
            string strSql = "";
            if (strType == "anquan")
            {
                strSql += "select '安全科两违情况' as cName, 'Tab_SpecificOnDuty_LiangWeiInfo' as eName, count (*) as ncount  from Tab_SpecificOnDuty_LiangWeiInfo";
            }

            if (strType == "chejian")
            {
                strSql += "select '运用车间个性化出勤人员信息' as cName, 'Tab_SpecificOnDuty_RenYuan'  as eName,count (*) as ncount from Tab_SpecificOnDuty_RenYuan UNION ";
                strSql += "select '运用车间天气预报', 'Tab_SpecificOnDuty_TianQi', count (*) from Tab_SpecificOnDuty_TianQi UNION ";
                strSql += "select '运用车间预警信息', 'Tab_SpecificOnDuty_YuJing',count (*) from Tab_SpecificOnDuty_YuJing";
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }



        //通过表名获取该表的所有的列
        public DataTable GetAllExcelCol(string ExcelName)
        {
            string strSql = "select * from Tab_ColName where DateName='" + ExcelName + "'";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }


        //通过表名获取该表的所有数据
        public DataTable GetAllData(int pageIndex, int pageCount, string DataName)
        {
            string strSql = @"select top " + pageCount.ToString() + " * from " + DataName + " where Id not in (select top "
                + ((pageIndex - 1) * pageCount).ToString() + @" Id from " + DataName + ")";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }
        //通过表名获取该表的所有数据
        public int GetAllDataCount(string DataName)
        {

            string strSql = "select count(*) from " + DataName;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }





        //通过表名获取该表的所有的列(关键列)
        public DataTable GetAllExcelKeyCol(string ExcelName)
        {
            string strSql = "select * from Tab_ColName where DateName='" + ExcelName + "' and IsRelation=1";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }


        //设置该表的关键列
        public int SetKeyCol(string nID,string Name)
        {
            string strSql = "update Tab_ColName  set IsRelation='" + Name + "' where nID=" + nID + "";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        public string GetColName(string ID)
        {
            string strSql = "select IsRelation from Tab_ColName where nID=" + ID + "";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                return dt.Rows[0]["IsRelation"].ToString();
            }
            else
            {
                return "";
            }

        }



        //获取所有的列
        public DataTable getAllCols(string Tab_Data)
        {
            string strSql = "select * from Tab_ColName where DateName ='" + Tab_Data + "'";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        
        }



        //获取所有的表
        public DataTable getAllExcelSpecificOnduty()
        {
            string strSql = "select * from Tab_Excel";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

        }


        //获取所有的列
        public DataTable getAllAllcols()
        {
            string strSql = "select * from Tab_ColName";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

        }


        //获取所有的数据
        public DataSet getAllDatas(string str)
        {
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString());

        }



        //判断是否存在
        public bool isExitJiaoluName(string JiaoluName)
        {
            string strSql = "select count(*) from Tab_SpecificOnDuty_JiaoLus where JiaoLuName='" + JiaoluName + "'";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString())) > 0;

        }



        /// <summary>
        /// 增加数据到交路列表
        /// </summary>
        public bool AddJiaolu(string JiaoluName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_SpecificOnDuty_JiaoLus(");
            strSql.Append("JiaoLuName,StrAllCheZhan)");
            strSql.Append(" values (");
            strSql.Append("@JiaoLuName,@StrAllCheZhan)");
            SqlParameter[] parameters = {
					new SqlParameter("@JiaoLuName", SqlDbType.VarChar,50),
					new SqlParameter("@StrAllCheZhan", SqlDbType.VarChar,500)
                                        };
            parameters[0].Value = JiaoluName;
            parameters[1].Value = "";

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //获取所有交路名称
        public DataTable GetAllJiaoLus()
        {
            string strSql = "select * from Tab_SpecificOnDuty_JiaoLus";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 增加数据到车站
        /// </summary>
        public bool upAllCheZhan(int id,string JiaoluName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tab_SpecificOnDuty_JiaoLus set StrAllCheZhan=StrAllCheZhan+@StrAllCheZhan+',' where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@StrAllCheZhan", SqlDbType.VarChar,500)
                                        };
            parameters[0].Value = id;
            parameters[1].Value = JiaoluName;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
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
        /// 修改
        /// </summary>
        public bool upAllCheZhans(int id, string JiaoluName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tab_SpecificOnDuty_JiaoLus set StrAllCheZhan=@StrAllCheZhan where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@StrAllCheZhan", SqlDbType.VarChar,500)
                                        };
            parameters[0].Value = id;
            parameters[1].Value = JiaoluName;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


       


        //获取所有交路名称
        public DataTable GetOneJiaoLu(int id)
        {
            string strSql = "select * from Tab_SpecificOnDuty_JiaoLus where Id='" + id + "'";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }

        //新增天气信息
        public bool AddTianQi(string strCheZhan, string strTianQi, string strWenDu,string strCheJian)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_SpecificOnDuty_TianQi(");
            strSql.Append("strCheZhan,strTianQi,strWenDu,strCheJian)");
            strSql.Append(" values (");
            strSql.Append("@strCheZhan,@strTianQi,@strWenDu,@strCheJian)");
            SqlParameter[] parameters = {
					new SqlParameter("@strCheZhan", SqlDbType.VarChar,150),
					new SqlParameter("@strTianQi", SqlDbType.VarChar,150),
                    new SqlParameter("@strWenDu", SqlDbType.VarChar,2000),
                     new SqlParameter("@strCheJian", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = strCheZhan;
            parameters[1].Value = strTianQi;
            parameters[2].Value = strWenDu;
            parameters[3].Value = strCheJian;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        ////设置该表的关键列
        //public int SetNoKeyCol(string nID)
        //{
        //    string strSql = "update Tab_ColName  set IsRelation='0' where nID=" + nID + "";
        //    return SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString());
        //}


        public int DeleteTable(string TableName)
        {
            string strSql1 = "delete "+TableName+" where 1=1";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql1.ToString());
        }

        public int DeleteTable(string TableName,string  CheJianId)
        {
            string strSql1 = "delete " + TableName + " where strCheJian='" + CheJianId + "'";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql1.ToString());
        }




        public int Delete(string KeyExcelName)
        {

            string strSql1 = "delete Tab_Excel where KeyExcelName= '" + KeyExcelName + "'";
            int rows1 = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql1.ToString());

            string strSql2 = "delete Tab_ColName where DateName= '" + KeyExcelName + "'";
            int rows2 = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql2.ToString());

            string strSql3 = "DROP TABLE " + KeyExcelName + "";
            int rows3 = 0;
            try
            {
                rows3 = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql3.ToString());
            }
            catch
            {
                rows3 = 0;
            }
            return rows1 + rows2 + rows3;

        }



        /// <summary>
        /// 增加一条数据到数据库集合表
        /// </summary>
        public bool Add(string ExcelName,string KeyExcelName,string strFileName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_Excel(");
            strSql.Append("ExcelName,KeyExcelName,StrUrl)");
            strSql.Append(" values (");
            strSql.Append("@ExcelName,@KeyExcelName,@StrUrl)");
            SqlParameter[] parameters = {
					new SqlParameter("@ExcelName", SqlDbType.VarChar,50),
					new SqlParameter("@KeyExcelName", SqlDbType.VarChar,50),
                    new SqlParameter("@StrUrl", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = ExcelName;
            parameters[1].Value = KeyExcelName;
            parameters[2].Value = strFileName;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //插入数据CREATE TABLE [dbo].[Rec_Stepsxx] ([StepId] [nvarchar] (100) ,[StepName] [nvarchar] (100)) 
        //创建表

        public bool CreateTable(ArrayList list,string TabName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("CREATE TABLE [dbo].[" + TabName + "] (");
            for (int i = 0; i < list.Count; i++)
            {
                strSql.Append("[" + list[i] + "] [nvarchar] (100),");
            }
            strSql.Append(")");
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
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
        /// 增加数据到列的集合表
        /// </summary>
        public bool AddCols(string DateName,string ColName,string KeyName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_ColName(");
            strSql.Append("DateName,ColName,KeyName)");
            strSql.Append(" values (");
            strSql.Append("@DateName,@ColName,@KeyName)");
            SqlParameter[] parameters = {
					new SqlParameter("@DateName", SqlDbType.VarChar,50),
					new SqlParameter("@ColName", SqlDbType.VarChar,50),
                    new SqlParameter("@KeyName", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = DateName;
            parameters[1].Value = ColName;
            parameters[2].Value = KeyName;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //新增两违信息
        public bool AddLiangWEi(string strShouPaiDanWei, string strShouPaiRen, string strFaPeiContent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_SpecificOnDuty_LiangWeiInfo(");
            strSql.Append("strShouPaiDanWei,strShouPaiRen,strFaPeiContent)");
            strSql.Append(" values (");
            strSql.Append("@strShouPaiDanWei,@strShouPaiRen,@strFaPeiContent)");
            SqlParameter[] parameters = {
					new SqlParameter("@strShouPaiDanWei", SqlDbType.VarChar,150),
					new SqlParameter("@strShouPaiRen", SqlDbType.VarChar,150),
                    new SqlParameter("@strFaPeiContent", SqlDbType.VarChar,2000)
                                        };
            parameters[0].Value = strShouPaiDanWei;
            parameters[1].Value = strShouPaiRen;
            parameters[2].Value = strFaPeiContent;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        //新增运用车间预警信息
        public bool AddYuJing(string StrQuDuan, string StrChaoZongYaoQiu, string StrYuJing, string StrGuanJianZhan, string strCheJian)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_SpecificOnDuty_YuJing(");
            strSql.Append("JiaoLuName,strYuJin,strCaoZongYaoQiu,strGuanJianZhan,strCheJian)");
            strSql.Append(" values (");
            strSql.Append("@JiaoLuName,@strYuJin,@strCaoZongYaoQiu,@strGuanJianZhan,@strCheJian)");
            SqlParameter[] parameters = {
					new SqlParameter("@JiaoLuName", SqlDbType.VarChar,50),
					new SqlParameter("@strYuJin", SqlDbType.VarChar,500),
                    new SqlParameter("@strCaoZongYaoQiu", SqlDbType.VarChar,500),
                    new SqlParameter("@strGuanJianZhan", SqlDbType.VarChar,500),
                     new SqlParameter("@strCheJian", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = StrQuDuan;
            parameters[1].Value = StrYuJing;
            parameters[2].Value = StrChaoZongYaoQiu;
            parameters[3].Value = StrGuanJianZhan;
            parameters[4].Value=strCheJian;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        //新增零碎修
        public bool AddLinSuiXiu(string StrCheXing, string StrCheHao, string StrTiChuRiQi, string StrGuZhangMiaoShu)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_SpecificOnDuty_LingSuiXiu(");
            strSql.Append("StrCheXing,StrCheHao,StrTiChuRiQi,StrGuZhangMiaoShu)");
            strSql.Append(" values (");
            strSql.Append("@StrCheXing,@StrCheHao,@StrTiChuRiQi,@StrGuZhangMiaoShu)");
            SqlParameter[] parameters = {
					new SqlParameter("@StrCheXing", SqlDbType.VarChar,50),
					new SqlParameter("@StrCheHao", SqlDbType.VarChar,50),
                    new SqlParameter("@StrTiChuRiQi", SqlDbType.VarChar,50),
                    new SqlParameter("@StrGuZhangMiaoShu", SqlDbType.VarChar,500)
                                        };
            parameters[0].Value = StrCheXing;
            parameters[1].Value = StrCheHao;
            parameters[2].Value = StrTiChuRiQi;
            parameters[3].Value = StrGuZhangMiaoShu;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        //新增人员信息
        public bool AddRenYuan(string StrNumber, string StrName, string StrIsZhongDianRen, string StrCheJianYaoQiu, string StrShenTiZhuangKuang, string strCheJian)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_SpecificOnDuty_RenYuan(");
            strSql.Append("strNumber,strName,strZhongDianPerple,strJianKang,strCheJianYaoQiu,strCheJian)");
            strSql.Append(" values (");
            strSql.Append("@strNumber,@strName,@strZhongDianPerple,@strJianKang,@strCheJianYaoQiu,@strCheJian)");
            SqlParameter[] parameters = {
					new SqlParameter("@strNumber", SqlDbType.VarChar,50),
					new SqlParameter("@strName", SqlDbType.VarChar,50),
                    new SqlParameter("@strZhongDianPerple", SqlDbType.VarChar,50),
                    new SqlParameter("@strJianKang", SqlDbType.VarChar,100),
                     new SqlParameter("@strCheJianYaoQiu", SqlDbType.VarChar,500),
                     new SqlParameter("@strCheJian", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = StrNumber;
            parameters[1].Value = StrName;
            parameters[2].Value = StrIsZhongDianRen;
            parameters[3].Value = StrShenTiZhuangKuang;
            parameters[4].Value = StrCheJianYaoQiu;
            parameters[5].Value = strCheJian;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
       }



        //新增两违信息人员列表
        public bool AddLiangWEiPerson(string UserNember, string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_SpecificOnDuty_LiangWei_Person(");
            strSql.Append("UserNember,UserName)");
            strSql.Append(" values (");
            strSql.Append("@UserNember,@UserName)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserNember", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = UserNember;
            parameters[1].Value = UserName;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //获取预警消息
        public DataTable GetYuJing(string strJiaoLu)
        {
            string str = "select * from Tab_SpecificOnDuty_YuJing where JiaoLuName='" + strJiaoLu + "'";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
        }

        //获取临碎修信息
        public DataTable GetLinSuixiu(string strCheXing,string strCheHao)
        {
            string str = "select top 3 * from Tab_SpecificOnDuty_LingSuiXiu where StrCheXing='" + strCheXing + "' and StrCheHao='" + strCheHao + "' order by StrTiChuRiQi desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
        }

        //获取人员消息
        public DataTable GetRenYuan(string strNumber)
        {
            string str = "select top 1 * from Tab_SpecificOnDuty_RenYuan where strNumber='" + strNumber + "'";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
        }

        public DataTable GetAllTianQi(string strJiaoLu)
        {

            string str = "select StrAllCheZhan from Tab_SpecificOnDuty_JiaoLus where JiaoLuName='" + strJiaoLu + "'";
            DataTable dt= SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
            string sp = "";
            if (dt.Rows.Count > 0)
            {
                sp = dt.Rows[0]["StrAllCheZhan"].ToString();
                sp = sp.Substring(0, sp.Length - 1);
            }
            string strAllCol="";
            for (int k = 0; k < sp.Split(',').Length; k++)
            {
                strAllCol += "'" + sp.Split(',')[k] + "',";
            }
            strAllCol = strAllCol.Substring(0, strAllCol.Length - 1);

            string strSql = "select * from Tab_SpecificOnDuty_TianQi where strCheZhan in (" + strAllCol + ")";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }


        public DataTable GetAllLiangWei(string UserNumber)
        {
            string strSql = "select top 3 * from Tab_SpecificOnDuty_LiangWeiInfo where strShouPaiRen in (select UserName from Tab_SpecificOnDuty_LiangWei_Person where UserNember='" + UserNumber + "')";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

            System.Data.DataTable tblDatas = new System.Data.DataTable();
            tblDatas.Columns.Add("strFaPeiContent", Type.GetType("System.String"));
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string FaPeiContent = dt.Rows[i]["strFaPeiContent"].ToString();
                string strFaPeiContent = "";
                if (FaPeiContent.LastIndexOf("发现:") != -1)
                    strFaPeiContent = FaPeiContent.Substring(FaPeiContent.LastIndexOf("发现:") + 3).ToString();
                if (FaPeiContent.LastIndexOf("发现：") != -1)
                    strFaPeiContent = strFaPeiContent.Substring(strFaPeiContent.LastIndexOf("发现：") + 3).ToString();
                tblDatas.Rows.Add(new object[] { strFaPeiContent });

            }
            return tblDatas;
        }




        /// <summary>
        /// 增加数据到列的集合表
        /// </summary>
        public int AddNewTableData(string TabName, ArrayList list, ArrayList ListCols)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + TabName + "(");

            for (int j = 0; j < ListCols.Count; j++)
            {
                if (j != ListCols.Count - 1)
                    strSql.Append("" + ListCols[j] + ",");
                else
                    strSql.Append("" + ListCols[j] + "");
            }
            strSql.Append(") values (");

            for (int i = 0; i < list.Count; i++)
            {
                if (i != list.Count - 1)
                {
                    strSql.Append("'" + list[i] + "',");
                }
                else
                {
                    strSql.Append("'" + list[i] + "'");
                }
            }

            strSql.Append(")");

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
            if (rows > 0)
            {
                return rows;
            }
            else
            {
                return 0;
            }
        } 
    }
}
