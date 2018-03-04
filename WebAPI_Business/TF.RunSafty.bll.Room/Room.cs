using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.Entry;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.Room
{
    public class InRoom
    {

        #region 插入入寓记录

        public InterfaceOutPut Add(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            TAB_Plan_InRoom model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TAB_Plan_InRoom>(data);
                DBRoom db = new DBRoom();
                if (!db.ExistsInRoom(model.strInRoomGUID))
                {
                    if (db.AddInRoom(model))
                    {
                        output.result = 0;
                        output.resultStr = "插入入寓记录成功";
                    }
                    else
                    {
                        output.result = 1;
                        output.resultStr = "插入入寓记录失败";
                    }
                }
                else
                {
                    if (db.UpdateInRoom(model))
                    {
                        output.result = 0;
                        output.resultStr = "已经存在入寓记录，更新成功";
                    }
                    else
                    {
                        output.result = 1;
                        output.resultStr = "已经存在出寓记录，更新失败";
                    }
                }
              
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return output;
        }
        #endregion

        #region 删除出寓记录
        public InterfaceOutPut Delete(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            TF.RunSafty.Model.TAB_Plan_InRoom model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.TAB_Plan_InRoom>(data);
                output.result = 0;
                output.resultStr = "插入出寓记录成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return output;
        }
        #endregion

        #region 修改出寓记录
        public InterfaceOutPut Update(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            TF.RunSafty.Model.TAB_Plan_InRoom model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.TAB_Plan_InRoom>(data);
                output.result = 0;
                output.resultStr = "插入出寓记录成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取出寓记录列表
        public InterfaceOutPut GetList(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            TF.RunSafty.Model.TAB_Plan_InRoom model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.TAB_Plan_InRoom>(data);
                output.result = 0;
                output.resultStr = "插入出寓记录成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return output;
        }
        #endregion

        
    }

    public class OutRoom
    {
        #region 插入出寓记录

        public InterfaceOutPut Add(string data)
        {
            InterfaceOutPut output=new InterfaceOutPut();
            TAB_Plan_OutRoom model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TAB_Plan_OutRoom>(data);
                DBRoom db = new DBRoom();
                if (!db.ExistsOutRoom(model.strOutRoomGUID))
                {
                    if (db.AddOutRoom(model))
                    {
                        output.result = 0;
                        output.resultStr = "插入出寓记录成功";
                    }
                    else
                    {
                        output.result = 1;
                        output.resultStr = "插入出寓记录失败";
                    }
                }
                else
                {
                    if (db.UpdateOutRoom(model))
                    {
                        output.result = 0;
                        output.resultStr = "已经存在出寓记录，更新成功";
                    }
                    else
                    {
                        output.result = 1;
                        output.resultStr = "已经存在出寓记录，更新失败";
                    }
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return output;
        }
        #endregion

        #region 删除出寓记录
        public InterfaceOutPut Delete(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            TF.RunSafty.Model.TAB_Plan_OutRoom model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.TAB_Plan_OutRoom>(data);
                output.result = 0;
                output.resultStr = "插入出寓记录成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return output;
        }
        #endregion

        #region 修改出寓记录
        public InterfaceOutPut Update(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            TF.RunSafty.Model.TAB_Plan_OutRoom model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.TAB_Plan_OutRoom>(data);
                output.result = 0;
                output.resultStr = "插入出寓记录成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return output;
        }
        #endregion

        #region 获取出寓记录列表
        public InterfaceOutPut GetList(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            TF.RunSafty.Model.TAB_Plan_OutRoom model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.TAB_Plan_OutRoom>(data);
                output.result = 0;
                output.resultStr = "插入出寓记录成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return output;
        }
        #endregion

    }
}
