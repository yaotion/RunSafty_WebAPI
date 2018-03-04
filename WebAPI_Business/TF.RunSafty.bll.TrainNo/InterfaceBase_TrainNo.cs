using System;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using TF.CommonUtility;
using TF.RunSafty.DBUtils;
using TF.RunSafty.Entry;
using TF.RunSafty.Logic;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;

namespace TF.RunSafty.TrainNo
{
    /// <summary>
    //����: InterfaceBase_TrainNo
    //˵��: ����ͼ��������Ϣ��Ϣ
    /// <summary>
    public class InterfaceBase_TrainNo
    {
        /// <summary>
        /// ������Ϣ
        /// <summary>
        public InterfaceOutPut Save(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 0;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                Base_TrainNo _Base_TrainNo = javaScriptSerializer.Deserialize<Base_TrainNo>(Data);
                DBBase_TrainNo dbBase_TrainNo = new DBBase_TrainNo(ThinkFreely.DBUtility.SqlHelper.ConnString);
                dbBase_TrainNo.Add(_Base_TrainNo);
                output.result = 1;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("InterfaceBase_TrainNo.Save:" + ex.Message);
            }
            return output;
        }
        /// <summary>
        /// ��ö�����Ϣ
        /// <summary>
        public InterfaceOutPut GetEntry(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 0;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                Base_TrainNoQueryCondition _Base_TrainNoQueryCondition = new Base_TrainNoQueryCondition();
                _Base_TrainNoQueryCondition.strGUID = ObjectConvertClass.static_ext_string(cjm.GetValue("strGUID"));
                DBBase_TrainNo dbBase_TrainNo = new DBBase_TrainNo(ThinkFreely.DBUtility.SqlHelper.ConnString);
                Base_TrainNo _Base_TrainNo = dbBase_TrainNo.GetModel(_Base_TrainNoQueryCondition);
                if (_Base_TrainNo == null)
                {
                    output.resultStr = "û�ж�Ӧ����";
                }
                else
                {
                    output.result = 1;
                    List<Base_TrainNo> Base_TrainNoList = new List<Base_TrainNo>();
                    Base_TrainNoList.Add(_Base_TrainNo);
                    output.data = Base_TrainNoList;
                }
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("InterfaceBase_TrainNo.GetEntry:" + ex.Message);
            }
            return output;
        }
        /// <summary>
        /// ��������б�
        /// <summary>
        public InterfaceOutPut GetList(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 0;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);

                Base_TrainNoQueryCondition _Base_TrainNoQueryCondition = new Base_TrainNoQueryCondition();


                DBBase_TrainNo dbBase_TrainNo = new DBBase_TrainNo(ThinkFreely.DBUtility.SqlHelper.ConnString);
                DataTable dt = dbBase_TrainNo.GetDataTable(_Base_TrainNoQueryCondition);
                output.data = dt;
                output.result = 1;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("InterfaceBase_TrainNo.GetList:" + ex.Message);
            }
            return output;
        }
    }
}