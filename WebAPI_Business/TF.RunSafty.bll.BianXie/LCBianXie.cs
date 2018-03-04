using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.BianXie;
using Newtonsoft.Json;
using TF.RunSafty.DrinkLogic;

namespace TF.RunSafty.BianXie
{

    #region 接口输出类
    /// <summary>
    /// 接口输出类
    /// </summary>
    public class InterfaceOutPut
    {
        public int result { get; set; }
        public string resultStr { get; set; }
        public object data { get; set; }
    }
    #endregion

   public class LCBianXie
    {

        #region 上传测酒记录
        public class InAddDrinkInfo
        {
            //测酒信息
            public MDBianXie.DrinkInfo drinkInfo = new MDBianXie.DrinkInfo();
        }

        /// <summary>
        /// 上传测酒记录
        /// </summary>
        public InterfaceOutPut AddDrinkInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                InAddDrinkInfo InParams = JsonConvert.DeserializeObject<InAddDrinkInfo>(Data);
                DBBianXie db = new DBBianXie();

                //职位信息----- 开始----------
                DBDrinkLogic dbdl = new DBDrinkLogic();
                MDDrinkLogic mddl = new MDDrinkLogic();
                MDDrinkLogic mdl = new MDDrinkLogic(); 
                mddl = dbdl.GetDrinkCadreEntity(InParams.drinkInfo.strTrainmanNumber);
                if (mddl != null)
                {
                    InParams.drinkInfo.strDepartmentID = mddl.strDepartmentID;
                    InParams.drinkInfo.strDepartmentName = mddl.strDepartmentName;
                    InParams.drinkInfo.nCadreTypeID = mddl.nCadreTypeID;
                    InParams.drinkInfo.strCadreTypeName = mddl.strCadreTypeName;
                }
                else
                {
                    InParams.drinkInfo.strDepartmentID = "";
                    InParams.drinkInfo.strDepartmentName = "";
                    InParams.drinkInfo.nCadreTypeID = "";
                    InParams.drinkInfo.strCadreTypeName = "";
                }
                //职位信息----- 结束----------

                if (InParams.drinkInfo.strTrainmanNumber.Trim() != "" && InParams.drinkInfo.dtCreateTime.ToString() != "")
                {
                    if (db.IsExit(InParams.drinkInfo.dtCreateTime.ToString(),InParams.drinkInfo.strTrainmanNumber.Trim()))
                    {
                        return new InterfaceOutPut();
                    }

                }


                if (InParams.drinkInfo.strTrainmanGUID.Trim() == "")
                {
                    InParams.drinkInfo.strTrainmanGUID = db.GetTrainmanGUIDByNumber(InParams.drinkInfo.strTrainmanNumber);
                }

                db.AddDrinkInfo(InParams.drinkInfo);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                throw ex;
            }
            return output;
        }
        #endregion

    }
}
