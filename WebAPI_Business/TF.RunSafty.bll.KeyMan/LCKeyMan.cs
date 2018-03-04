using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.CommonUtility;
namespace TF.RunSafty.KeyMan
{
    public class LCKeyMan
    {
        private LCOutput outPut = new LCOutput(); 
        public LCOutput Add(string data)
        {
            outPut.Clear();
            try
            {
                KeyMan keyman = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyMan>(data);

                new DBKeyMan().Add(keyman);
            }
            catch(Exception ex)
            {
                outPut.result = 1;
                outPut.resultStr = ex.Message.ToString();
            }
            

            return outPut;
        }

        public LCOutput Del(string data)
        {
            outPut.Clear();
            try
            {
                KeyMan keyman = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyMan>(data);

                new DBKeyMan().Del(keyman);
            }
            catch (Exception ex)
            {
                outPut.result = 1;
                outPut.resultStr = ex.Message.ToString();
            }


            return outPut;
        }

        public LCOutput Update(string data)
        {
            outPut.Clear();
            try
            {
                KeyMan keyman = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyMan>(data);

                new DBKeyMan().Update(keyman);
            }
            catch (Exception ex)
            {
                outPut.result = 1;
                outPut.resultStr = ex.Message.ToString();
            }


            return outPut;
        }

        public LCOutput Clear(string data)
        {
            outPut.Clear();
            try
            {
                CommonJsonModel jsonModel = new CommonJsonModel(data);
                string WorkShopGUID = jsonModel.GetValue("WorkShopGUID");
                string CheDuiGUID = jsonModel.GetValue("CheDuiGUID");

                new DBKeyMan().Clear(WorkShopGUID, CheDuiGUID);
            }
            catch(Exception ex)
            {
                outPut.result = 1;
                outPut.resultStr = ex.Message.ToString();
            }
            
            return outPut;
        }

        public LCOutput Get(string data)
        {
            outPut.Clear();
            try
            {
                KMQueryQC queryQC = Newtonsoft.Json.JsonConvert.DeserializeObject<KMQueryQC>(data);

                outPut.data = new DBKeyMan().Get(queryQC);
            }
            catch (Exception ex)
            {
                outPut.result = 1;
                outPut.resultStr = ex.Message.ToString();
            }


            return outPut;
        }

        public LCOutput GetHistory(string data)
        {

            outPut.Clear();
            try
            {
                KMQueryQC queryQC = Newtonsoft.Json.JsonConvert.DeserializeObject<KMQueryQC>(data);

                outPut.data = new DBKeyMan().GetHistory(queryQC);
            }
            catch (Exception ex)
            {
                outPut.result = 1;
                outPut.resultStr = ex.Message.ToString();
            }


            return outPut;
        }
    }
}
