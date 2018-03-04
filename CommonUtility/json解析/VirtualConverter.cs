using System;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Newtonsoft.Json.Converters
{
    /// <summary>
    /// Provides virtual type instatiation based on the value of a type attribute.
    /// </summary>
    /// <typeparam name="T">Base class for any object instantiated based on the type attribute.</typeparam>
    public class JsonVirtualConverter<T> : JsonConverter
        where T: class
    {
        /// <summary>
        /// Initialization Contructor.
        /// </summary>
        /// <param name="typeField">Name of the attribute that contains type information.</param>
        /// <param name="tf">Type-factory function to instantiate a new object based on the value of the type attribute.</param>
        public JsonVirtualConverter(string typeAttr, Func<string, T> tf)
        {
            if (string.IsNullOrEmpty(typeAttr))
                throw new ArgumentException("Non-empty name for the type attribute is required.", "typeAttr");

            this.typeAttr = typeAttr;
            this.tf = tf;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// true, if this instance can convert the specified object type; otherwise, false.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The JsonReader object to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            string value = (string)obj.Property(typeAttr);
            T target = tf(value);
            if (target == null)
            {
                if (value == null)
                    value = "null";
                else
                    value = String.Format("\"{0}\"", value);

                throw new Exception(String.Format("Failed to create a new object of type {0} based on attribute \"{1}\" = {2}", typeof(T).FullName, typeAttr, value));
            }
            serializer.Populate(obj.CreateReader(), target);
            return target;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The JsonWriter object to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("JsonVirtualConverter should only be used while deserializing.");
        }

        /// <summary>
        /// Name of the attribute that contains type information.
        /// </summary>
        public string typeAttr { get; private set; }
                
        /// <summary>
        /// Type-factory function to instantiate a new object based on the value of the type attribute.
        /// </summary>
        public Func<string, T> tf { get; private set; }
    }
}
