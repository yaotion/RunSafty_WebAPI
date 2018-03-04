using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Newtonsoft.Json.Utilities;
/// <summary>
///SpecialContractResolver 的摘要说明
/// </summary>
/// 
public class NullableValueProvider : IValueProvider
{
    private readonly object _defaultValue;
    private readonly IValueProvider _underlyingValueProvider;


    public NullableValueProvider(MemberInfo memberInfo, Type underlyingType)
    {
        _underlyingValueProvider = new DynamicValueProvider(memberInfo);
        _defaultValue = Activator.CreateInstance(underlyingType);
    }

    public void SetValue(object target, object value)
    {
        _underlyingValueProvider.SetValue(target, value);
    }

    public object GetValue(object target)
    {
        return _underlyingValueProvider.GetValue(target) ?? _defaultValue;
    }
}
public class SpecialContractResolver : DefaultContractResolver
{
    public SpecialContractResolver()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //

    }
    protected override IValueProvider CreateMemberValueProvider(System.Reflection.MemberInfo member)
    {
        if (member.MemberType == MemberTypes.Property)
        {
            var pi = (PropertyInfo)member;
            if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return new NullableValueProvider(member, pi.PropertyType.GetGenericArguments().First());
            }
        }
        else if (member.MemberType == MemberTypes.Field)
        {
            var fi = (FieldInfo)member;
            if (fi.FieldType.IsGenericType && fi.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                return new NullableValueProvider(member, fi.FieldType.GetGenericArguments().First());
        }
        return base.CreateMemberValueProvider(member);
    }
}

public class NullStringProvider : IValueProvider
{
     private readonly object _defaultValue=string.Empty;
    private readonly IValueProvider _underlyingValueProvider;

    public NullStringProvider() { }
    public NullStringProvider(MemberInfo memberInfo, Type underlyingType)
    {
        _underlyingValueProvider = new DynamicValueProvider(memberInfo);
        _defaultValue = Activator.CreateInstance(underlyingType);
    }
    #region IValueProvider 成员

   public void SetValue(object target, object value)
    {
        _underlyingValueProvider.SetValue(target, value);
    }

    public object GetValue(object target)
    {
        return _underlyingValueProvider.GetValue(target) ?? _defaultValue;
    }

    #endregion
}
public class NullContractResolever : DefaultContractResolver
{
    public NullContractResolever() { }
    public NullContractResolever(bool shareCache){}
    protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
    {
        if (member.MemberType == MemberTypes.Property)
        {
            var pi = (PropertyInfo)member;
            if (pi.PropertyType==typeof(string))
            {
                return new NullStringProvider(member, pi.PropertyType);
            }
        }
        else if (member.MemberType == MemberTypes.Field)
        {
            var fi = (FieldInfo)member;
            if (fi.FieldType == typeof(string))
            {
                return new NullStringProvider(member, fi.FieldType);
            }
        }
        return base.CreateMemberValueProvider(member);
    }
}

public class NullStringConverter : Newtonsoft.Json.JsonConverter
{
    private const string DefaultNullString = "";
    public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
    {
        if (value is string)
        {
            if (value == null)
                writer.WriteValue(DefaultNullString);
        }
    }

    public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
    { 
        if (reader.Value==null || string.IsNullOrEmpty(reader.Value.ToString()))
            return null;
        return reader.Value.ToString();
    }
    public override bool CanWrite
    {
        get
        {
            return base.CanWrite;
        }
    }
    public override bool CanRead
    {
        get
        {
            return base.CanRead;
        }
    }
    public override bool CanConvert(Type objectType)
    {
        return true;
    }
}