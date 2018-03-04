using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace TF.RunSafty.BLL
{
    //TAB_NoticeType
    public partial class TAB_NoticeType
    {

        private readonly TF.RunSafty.DAL.TAB_NoticeType dal = new TF.RunSafty.DAL.TAB_NoticeType();
        public TAB_NoticeType()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int nid)
        {
            return dal.Exists(nid);
        }

        /// <summary>
        /// 判断类型是否存在
        /// </summary>
        /// <param name="noticeType"></param>
        /// <returns></returns>
        public bool Exists(string noticeType)
        {
            string where = string.Format(" strTypeName='{0}'",noticeType);
            IList<TF.RunSafty.Model.TAB_NoticeType> models= GetModelList(where);
            return models.Count > 0;
        }
        /// <summary>
        ///  判断类型是否存在
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="noticeType"></param>
        /// <returns></returns>
        public bool Exists(int nid, string noticeType)
        {
            string where = string.Format(" strTypeName='{0}' and nid<>{1}", noticeType,nid.ToString());
            IList<TF.RunSafty.Model.TAB_NoticeType> models = GetModelList(where);
            return models.Count > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TF.RunSafty.Model.TAB_NoticeType model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TF.RunSafty.Model.TAB_NoticeType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int nid)
        {

            return dal.Delete(nid);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string nidlist)
        {
            return dal.DeleteList(nidlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.TAB_NoticeType GetModel(int nid)
        {

            return dal.GetModel(nid);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.TAB_NoticeType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.TAB_NoticeType> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.TAB_NoticeType> modelList = new List<TF.RunSafty.Model.TAB_NoticeType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.TAB_NoticeType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new TF.RunSafty.Model.TAB_NoticeType();
                    if (dt.Rows[n]["nid"].ToString() != "")
                    {
                        model.nid = int.Parse(dt.Rows[n]["nid"].ToString());
                    }
                    model.strTypeGUID = dt.Rows[n]["strTypeGUID"].ToString();
                    model.strTypeName = dt.Rows[n]["strTypeName"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

    }
}