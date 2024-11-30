using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace HealthCare.Api
{
    public class CustomJsonConverter : JsonConverter<object>
    {
        
        readonly Dictionary<Type, JsonConverter> _converters = new()
        {
        };

        public override bool CanConvert(Type typeToConvert)
        {
            // Implement your logic to determine if the type can be converted
            throw new NotImplementedException();
        }

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Implement your logic to read and convert the JSON to the desired type
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            // Implement your logic to write the object as JSON
            throw new NotImplementedException();
        }
    }
}
