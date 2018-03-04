using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.RunSafty.DBUtils;

namespace TF.RunSafty.Logic
{
   public class Users
    {
       TF.RunSafty.DBUtils.Users DBUr = new TF.RunSafty.DBUtils.Users(SqlHelps.SQLConnString);

       /// <summary>
       /// 登录
       /// </summary>
       /// <param name="userNumber"></param>
       /// <param name="Password"></param>
       /// <returns></returns>
       public DataTable login(string userNumber, string Password)
       {
           return DBUr.login(userNumber, Password);
       }

       /// <summary>
       /// 是否存在该记录
       /// </summary>
       public int Exists(string userNumber)
       {
           return DBUr.IsExistCheckRoles(userNumber);
       }

       /// <summary>
       /// 增加一条数据
       /// </summary>
       public int Add(TF.RunSafty.Entry.Users model)
       {
           return DBUr.Add(model);
       }

       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool Update(TF.RunSafty.Entry.Users model)
       {
           return DBUr.Update(model);
       }


       /// <summary>
       /// 删除一条数据
       /// </summary>
       public int Delete(int Id)
       {

           return DBUr.Delete(Id);
       }


       /// <summary>
       /// 获取数据列表
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageCount"></param>
       /// <param name="Begintime"></param>
       /// <param name="EndTimeStr"></param>
       /// <returns></returns>
       public DataTable QueryCheckUsers(int pageIndex, int pageCount, string RolesName)
       {
           return DBUr.QueryCheckRoles(pageIndex, pageCount, RolesName);
       }
       /// <summary>
       /// 获取数据列表的个数
       /// </summary>
       /// <param name="Begintime"></param>
       /// <param name="EndTimeStr"></param>
       /// <returns></returns>
       public int QueryCheckUsersCount(string userName)
       {
           return DBUr.QueryCheckRolesCount(userName);
       }

       /// <summary>
       /// 得到一个对象实体
       /// </summary>
       public TF.RunSafty.Entry.Users GetModel(int Id)
       {
           return DBUr.GetModel(Id);
       }



       /// <summary>
       /// 通过真实姓名获取数据列表
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageCount"></param>
       /// <param name="Begintime"></param>
       /// <param name="EndTimeStr"></param>
       /// <returns></returns>
       public DataTable getAllName(string UserName)
       {
           return DBUr.getAllName(UserName);
       }


       /// <summary>
       /// 通过用户名获取数据列表
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageCount"></param>
       /// <param name="Begintime"></param>
       /// <param name="EndTimeStr"></param>
       /// <returns></returns>
       public DataTable getAllNameByuserNumber(string UserNumber)
       {
           return DBUr.getAllNameByuserNumber(UserNumber);
       }


       public bool UpdatePassWord(TF.RunSafty.Entry.Users model)
       {
          return DBUr.UpdatePassWord(model);
       }

    }
}
