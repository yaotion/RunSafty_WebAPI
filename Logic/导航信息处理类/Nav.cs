using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.DBUtils;
using System.Data;

namespace TF.RunSafty.Logic
{
   
   public class Nav
    {
       TF.RunSafty.DBUtils.Nav DBNav = new TF.RunSafty.DBUtils.Nav(SqlHelps.SQLConnString);



       /// <summary>
       /// 看是否已经存在该typeid的类别
       /// </summary>
       /// <param name="typeID"></param>
       /// <returns></returns>
       public bool IfExists(string typeID)
       {
           return DBNav.IfExists(typeID);
       }




       /// <summary>
       /// 获取那个最大的typeid
       /// </summary>
       /// <param name="typeID"></param>
       /// <returns></returns>
       public DataSet GetMaxId(string typeID)
       {
           return DBNav.GetMaxId(typeID);
       }


       /// <summary>
       /// 增加一条数据
       /// </summary>
       public bool Add(TF.RunSafty.Entry.nav model)
       {
           return DBNav.Add(model);
       }




       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool Update(TF.RunSafty.Entry.nav model)
       {

           return DBNav.Update(model);
       }


       /// <summary>
       /// 删除一条数据
       /// </summary>
       public bool Delete(string typeID)
       {
           return DBNav.Delete(typeID);
       }


       /// <summary>
       /// 得到一个对象实体
       /// </summary>
       public TF.RunSafty.Entry.nav GetModel()
       {

           return DBNav.GetModel();
       }

       /// <summary>
       /// 获得数据列表
       /// </summary>
       public DataTable GetList(string strWhere)
       {
           return DBNav.GetList(strWhere);
       }


    }
}
