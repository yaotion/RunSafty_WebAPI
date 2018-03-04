using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace TF.Utils
{
    public class PropertyInfoNullException:Exception
    {
        public PropertyInfoNullException() { }
        public PropertyInfoNullException(string message) : base(message) { }
        public PropertyInfoNullException(string message, Exception inner) : base(message, inner) { }
    }
    public abstract class ArgumentValidationAttribute : Attribute
    {
        public abstract void Validate(object value, string argumentName);
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullAttribute : ArgumentValidationAttribute
    {
        public override void Validate(object value, string argumentName)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);
        }
    }

    public class Validation
    {
        public bool IsNotNullPropertiesValidated(object obj)
        {
            bool hasException = false;
            StringBuilder builder = new StringBuilder();
            foreach (PropertyInfo proInfo in obj.GetType().GetProperties())
            {
                object[] attrs = proInfo.GetCustomAttributes(typeof(NotNullAttribute), true);
                if (attrs != null && attrs.Length==1)
                {
                    NotNullAttribute attr = (NotNullAttribute)attrs[0];
                    object v=proInfo.GetValue(obj, null);
                    if (v == null)
                    {
                        hasException = true;
                        builder.Append(proInfo.Name);
                        builder.Append("\t");
                    }
                }
            }
            if (hasException)
            {
                string message = string.Format("{0}字段不能为空", builder.ToString());
                throw new ArgumentNullException(message);
            }
            return true;
        }
    }
}
