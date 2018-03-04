using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ThinkFreely.DBUtility;
namespace TF.RunSafty.DAL  
{
	 	//TAB_NoticeType
		public partial class TAB_NoticeType
	{
   		     
		public bool Exists(int nid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TAB_NoticeType");
			strSql.Append(" where ");
			                                       strSql.Append(" nid = @nid  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;

			return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TF.RunSafty.Model.TAB_NoticeType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TAB_NoticeType(");			
            strSql.Append("strTypeGUID,strTypeName");
			strSql.Append(") values (");
            strSql.Append("@strTypeGUID,@strTypeName");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@strTypeGUID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@strTypeName", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.strTypeGUID;                        
            parameters[1].Value = model.strTypeName;                        
			   
			object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);			
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
		public bool Update(TF.RunSafty.Model.TAB_NoticeType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TAB_NoticeType set ");
			                                                
            strSql.Append(" strTypeGUID = @strTypeGUID , ");                                    
            strSql.Append(" strTypeName = @strTypeName  ");            			
			strSql.Append(" where nid=@nid ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@nid", SqlDbType.Int,4) ,            
                        new SqlParameter("@strTypeGUID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@strTypeName", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.nid;                        
            parameters[1].Value = model.strTypeGUID;                        
            parameters[2].Value = model.strTypeName;                        
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
		public bool Delete(int nid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_NoticeType ");
			strSql.Append(" where nid=@nid");
						SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;


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
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string nidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TAB_NoticeType ");
			strSql.Append(" where ID in ("+nidlist + ")  ");
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
		public TF.RunSafty.Model.TAB_NoticeType GetModel(int nid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select nid, strTypeGUID, strTypeName  ");			
			strSql.Append("  from TAB_NoticeType ");
			strSql.Append(" where nid=@nid");
						SqlParameter[] parameters = {
					new SqlParameter("@nid", SqlDbType.Int,4)
			};
			parameters[0].Value = nid;

			
			TF.RunSafty.Model.TAB_NoticeType model=new TF.RunSafty.Model.TAB_NoticeType();
			DataSet  ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["nid"].ToString()!="")
				{
					model.nid=int.Parse(ds.Tables[0].Rows[0]["nid"].ToString());
				}
                model.strTypeName= ds.Tables[0].Rows[0]["strTypeName"].ToString();
																										
				return model;
			}
			else
			{
				return null;
			}
		}
		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM TAB_NoticeType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM TAB_NoticeType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
		}

   
	}
}

