using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace TF.RunSafty.BLL
{
    //TAB_NoticeFile
    public partial class TAB_NoticeFile
    {

        private readonly TF.RunSafty.DAL.TAB_NoticeFile dal = new TF.RunSafty.DAL.TAB_NoticeFile();
        public TAB_NoticeFile()
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
        /// 查看该车间是否上传过该通告
        /// </summary>
        /// <param name="strFileName">文件名称</param>
        /// <param name="strWorkShop">车间GUID</param>
        /// <returns></returns>
        public bool Exists(string strFileName, string strWorkShop)
        {
            string where = string.Format(" strFileName='{0}' and strWorkShopGUID='{1}' ",strFileName,strWorkShop);
            List<TF.RunSafty.Model.TAB_NoticeFile> models = GetModelList(where);
            return models.Count > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TF.RunSafty.Model.TAB_NoticeFile model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TF.RunSafty.Model.TAB_NoticeFile model)
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
        public TF.RunSafty.Model.TAB_NoticeFile GetModel(int nid)
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
        public List<TF.RunSafty.Model.TAB_NoticeFile> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.TAB_NoticeFile> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.TAB_NoticeFile> modelList = new List<TF.RunSafty.Model.TAB_NoticeFile>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.TAB_NoticeFile model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new TF.RunSafty.Model.TAB_NoticeFile();
                    if (dt.Rows[n]["nid"].ToString() != "")
                    {
                        model.nid = int.Parse(dt.Rows[n]["nid"].ToString());
                    }
                    model.strFileGUID = dt.Rows[n]["strFileGUID"].ToString();
                    model.strWorkShopGUID = dt.Rows[n]["strWorkShopGUID"].ToString();
                    model.StrTypeGUID = dt.Rows[n]["StrTypeGUID"].ToString();
                    model.strFileName = dt.Rows[n]["strFileName"].ToString();
                    model.dtBeginTime = dt.Rows[n]["dtBeginTime"].ToString();
                    model.dtEndTime = dt.Rows[n]["dtEndTime"].ToString();


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