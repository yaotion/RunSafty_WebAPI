using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.RunSafty.DBUtils;

namespace TF.RunSafty.Logic
{
   public class Diary
    {
       TF.RunSafty.DBUtils.Diary DBUtilsDy = new TF.RunSafty.DBUtils.Diary(SqlHelps.SQLConnString);


       /// <summary>
       /// 获取数据列表
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageCount"></param>
       /// <param name="Begintime"></param>
       /// <param name="EndTimeStr"></param>
       /// <returns></returns>
       public DataTable QueryCheckDiary(int pageIndex, int pageCount, string Begintime, string EndTimeStr, string UsersId)
       {
           return DBUtilsDy.QueryCheckDiary(pageIndex, pageCount, Begintime, EndTimeStr, UsersId);
       }
       /// <summary>
       /// 获取数据列表的个数
       /// </summary>
       /// <param name="Begintime"></param>
       /// <param name="EndTimeStr"></param>
       /// <returns></returns>
       public int QueryCheckDiaryCount(string Begintime, string EndTimeStr)
       {
           return DBUtilsDy.QueryCheckDiaryCount(Begintime, EndTimeStr);
       }

       /// <summary>
		/// 得到一个对象实体
		/// </summary>
       public TF.RunSafty.Entry.Diary GetModel(int Id)
		{
            return DBUtilsDy.GetModel(Id);
		}
        /// <summary>
        /// 增加一条数据
        /// </summary>
       public int Add(TF.RunSafty.Entry.Diary model)
        {
            return DBUtilsDy.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TF.RunSafty.Entry.Diary model)
        {
            return DBUtilsDy.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Id)
        {

            return DBUtilsDy.Delete(Id) == 1;
        }

       /// <summary>
       /// 统计时获取每一天写日志的数量
       /// </summary>
       /// <param name="Begintime"></param>
       /// <param name="EndTimeStr"></param>
       /// <returns></returns>
        public DataTable GetAllCount(string Begintime, string EndTimeStr)
        {
            return DBUtilsDy.GetallCount(Begintime, EndTimeStr);
        }


    }
}
