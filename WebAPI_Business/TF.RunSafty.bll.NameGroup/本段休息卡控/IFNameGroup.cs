using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.NamePlate.MD;

namespace TF.RunSafty.NamePlate
{
    public class InGetControlRests
    {
        //车间GUID
        public string TrainmanJiaoluGUID;
        //出勤点，可为空，为空的时候代表所有出勤点
        public string PlaceID;
    }


    public class IFNameGroup
    {
        public InterfaceOutPut GetControlRests(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            InGetControlRests InParams = Newtonsoft.Json.JsonConvert.DeserializeObject<InGetControlRests>(Data);           
            output.data = LCNPControlRest.GetControlResultList(InParams.TrainmanJiaoluGUID,InParams.PlaceID);
            output.result = 0;
            output.resultStr = "获取成功！";           
            return output;
        }
    }
}
