using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;


namespace TF.Utils
{
    /// 用于API接口方法的统一返回值结构
    /// </summary>
    public class APIResult
    {
        public int result;
        public string resultStr;
        public object data;
    }
    public class Reflect
    {      
        /// 将JSON输入数据格式化为指定方法的参数并调用方法执行
        /// 将方法的返回结果格式化为Json字符串返回
        /// </summary>
        /// <param name="AssemblyName"></param>
        /// <param name="FullClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="ParamJson"></param>
        /// <returns></returns>
        public static object ExecAPI(string AssemblyName, string FullClassName, string MethodName, string ParamJson,bool ObjectMode)
        {
            //从bin目录加载程序集dll
            System.Reflection.Assembly apiASM = System.Reflection.Assembly.Load(AssemblyName);

            //从程序集获取类型
            Type apiType = apiASM.GetType(FullClassName, true, true);
            //创建实例
            object apiObject = Activator.CreateInstance(apiType, true);

            //获取方法
            MethodInfo mi = apiType.GetMethod(MethodName);
            //获取方法的参数列表
            ParameterInfo[] Params = mi.GetParameters();
            object[] obj = new object[Params.Length];
            if (ObjectMode)
            {
                Newtonsoft.Json.Linq.JObject jsonObject = Newtonsoft.Json.Linq.JObject.Parse(ParamJson);
                IEnumerable<Newtonsoft.Json.Linq.JProperty> properties = jsonObject.Properties();
                for (int i = 0; i < Params.Length; i++)
                {
                    foreach (Newtonsoft.Json.Linq.JProperty item in properties)
                    {
                        if (item.Name.Trim().ToLower() == Params[i].Name.Trim().ToLower())
                        {
                            Type tType = Params[i].ParameterType;
                            //如果它是值类型,或者String   
                            if (tType.Equals(typeof(string)) || (!tType.IsInterface && !tType.IsClass))
                            {
                                //改变参数类型   
                                obj[i] = Convert.ChangeType(item.Value.ToString(), tType);
                            }
                            else if (tType.IsClass)//如果是类,将它的json字符串转换成对象
                            {
                                obj[i] = Newtonsoft.Json.JsonConvert.DeserializeObject(item.Value.ToString(), tType);
                            }
                            break;
                        }
                    }
                }
            }
            else
            {                
                obj[0] = ParamJson;
            }
            return mi.Invoke(apiObject, obj);  
        }
    }
}
