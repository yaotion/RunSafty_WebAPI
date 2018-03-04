using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using TF.RunSafty.Plan;

namespace ReflectionTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region 使用动态类型代替反射，提高性能
        public delegate object FastInvokeHandler(object target,
                                           object[] paramters);
        public static FastInvokeHandler GetMethodInvoker(MethodInfo methodInfo)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty,
                             typeof(object), new Type[] { typeof(object), 
                     typeof(object[]) },
                             methodInfo.DeclaringType.Module);
            ILGenerator il = dynamicMethod.GetILGenerator();
            ParameterInfo[] ps = methodInfo.GetParameters();
            Type[] paramTypes = new Type[ps.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                paramTypes[i] = ps[i].ParameterType;
            }
            LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                locals[i] = il.DeclareLocal(paramTypes[i]);
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(il, paramTypes[i]);
                il.Emit(OpCodes.Stloc, locals[i]);
            }
            il.Emit(OpCodes.Ldarg_0);
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldloc, locals[i]);
            }
            il.EmitCall(OpCodes.Call, methodInfo, null);
            if (methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                EmitBoxIfNeeded(il, methodInfo.ReturnType);
            il.Emit(OpCodes.Ret);
            FastInvokeHandler invoder =
              (FastInvokeHandler)dynamicMethod.CreateDelegate(
              typeof(FastInvokeHandler));
            return invoder;
        }
        private static void EmitBoxIfNeeded(ILGenerator ilg, Type type)
        {
            if (type.IsValueType)
            {
                ilg.Emit(OpCodes.Box, type);
            }
        }
        private static void EmitFastInt(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }
            if (value > -129 && value < 128)
            {
                il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }
        private static void EmitCastToReference(ILGenerator ilg, Type type)
        {
            if (type.IsValueType)
            {
                ilg.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                ilg.Emit(OpCodes.Castclass, type);
            }
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            int loopCount = 10000;
            string AssemblyName = "TF.Runsafty.bll.Plan";
            string TypeName = "Plan.LCTrainPlan";
            string MethodName = "Delete";
            string data = "{\"strTrainPlanGUID\":\"d\"}";
            System.Diagnostics.Stopwatch watch=new Stopwatch();
            watch.Start();
            Assembly apiASM = Assembly.Load(AssemblyName);
            Type apiType = apiASM.GetType("TF.RunSafty." + TypeName, true, true);
            //创建实例
            object apiObject = Activator.CreateInstance(apiType, true);
            MethodInfo mi;
            string strMethodName = MethodName;
            object[] args = new object[] { data };
            Type[] prmTypes = new Type[] { typeof(string) };
            for (int i = 0; i < loopCount; i++)
            {
                
                //从程序集获取类型 

                //获取方法，方法的参数都只有一个，类型是string，加上prmTypes是避免有函数重构的情况
                mi = apiType.GetMethod(strMethodName, BindingFlags.ExactBinding | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, prmTypes, null);
                //FastInvokeHandler fastInvoker = GetMethodInvoker(mi);

                //调用实例的方法，获取返回值
                object mOut = mi.Invoke(apiObject, args);
            }
            watch.Stop();
            long myUseTime = watch.ElapsedMilliseconds;
            this.listBox1.Items.Add(string.Format("调用反射用时：{0} ms",myUseTime));
            this.listBox1.Refresh();
            watch.Reset();
            watch.Start();

             apiType = typeof(TF.RunSafty.Plan.LCTrainPlan);
            //创建实例
            //object apiObject = Activator.CreateInstance(apiType, true);
             apiObject = new TF.RunSafty.Plan.LCTrainPlan();
             //获取方法，方法的参数都只有一个，类型是string，加上prmTypes是避免有函数重构的情况
            
            mi = apiType.GetMethod(strMethodName, BindingFlags.ExactBinding | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, prmTypes, null);
                
            for (int i = 0; i < loopCount; i++)
            {
                
                //从程序集获取类型  

               FastInvokeHandler fastInvoker = GetMethodInvoker(mi);

                //调用实例的方法，获取返回值
                object mOut = fastInvoker(apiObject, args);
            }
            watch.Stop();
            myUseTime = watch.ElapsedMilliseconds;
            this.listBox1.Items.Add(string.Format("调用动态方法用时：{0} ms", myUseTime));
            this.listBox1.Refresh();
            watch.Reset();
            watch.Start();
            TF.RunSafty.Plan.LCTrainPlan lcPlan = null;
            for (int i = 0; i < loopCount; i++)
            {
                 lcPlan=new LCTrainPlan();
                lcPlan.Delete(data);
            }
            watch.Stop();
            myUseTime = watch.ElapsedMilliseconds;
            this.listBox1.Items.Add(string.Format("调用实例方法用时：{0} ms", myUseTime));
        }
    }
}
