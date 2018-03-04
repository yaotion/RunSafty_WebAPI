using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.RunSafty.DBUtils;
namespace TF.RunSafty.Logic
{
   public class Roles
    {
       TF.RunSafty.DBUtils.Roles DBRl = new TF.RunSafty.DBUtils.Roles(SqlHelps.SQLConnString);


       /// <summary>
       /// 是否存在该记录
       /// </summary>
       public int Exists(string RolesName)
       {
           return DBRl.IsExistCheckRoles(RolesName);
       }

       /// <summary>
       /// 增加一条数据
       /// </summary>
       public int Add(TF.RunSafty.Entry.Roles model)
       {
           return DBRl.Add(model);
       }

       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool Update(TF.RunSafty.Entry.Roles model)
       {
           return DBRl.Update(model);
       }

       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool UpdatePowers(TF.RunSafty.Entry.Roles model)
       {
           return DBRl.UpdatePowers(model);
       }

       /// <summary>
       /// 删除一条数据
       /// </summary>
       public int Delete(int Id)
       {

           return DBRl.Delete(Id);
       }



       public int QueryCheckUsersCount(int id)
       {
           return DBRl.QueryCheckUsersCount(id);
       
       }

       /// <summary>
       /// 获取数据列表
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageCount"></param>
       /// <param name="Begintime"></param>
       /// <param name="EndTimeStr"></param>
       /// <returns></returns>
       public DataTable QueryCheckRoles(int pageIndex, int pageCount, string RolesName)
       {
           return DBRl.QueryCheckRoles(pageIndex, pageCount, RolesName);
       }
       /// <summary>
       /// 获取数据列表的个数
       /// </summary>
       /// <param name="Begintime"></param>
       /// <param name="EndTimeStr"></param>
       /// <returns></returns>
       public int QueryCheckRolesCount(string RolesName)
       {
           return DBRl.QueryCheckRolesCount(RolesName);
       }

       /// <summary>
       /// 得到一个对象实体
       /// </summary>
       public TF.RunSafty.Entry.Roles GetModel(int Id)
       {
           return DBRl.GetModel(Id);
       }

       /// <summary>
       /// 获取所有的角色名称
       /// </summary>
       /// <returns></returns>
       public DataTable getAllRolesName()
       {
           return DBRl.getAllRolesName();
       }

    }
}
