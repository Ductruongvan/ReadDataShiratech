using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiratech_Params.Utils
{
  using System;
  using System.Collections.Generic;

  using System.Globalization;
  using System.Text.Json.Serialization;
  using System.Text.Json;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Converters;
  using JsonConverter = Newtonsoft.Json.JsonConverter;
  using System.IO;
  using Shiratech_Params.Models;
  using Newtonsoft.Json.Serialization;
  using System.Reflection;
  using System.Runtime.CompilerServices;

  public partial class Helper
  {

  }
  public partial class Helper<T> where T : class
  {
    public static T FromJson(string json) => JsonConvert.DeserializeObject<T>(json, Converter.Settings);


  }
  public class Serialize<T> where T : class
  {
    public static string ToJson(T self) => JsonConvert.SerializeObject(self, Converter.Settings);
  }
  class CustomResolver : DefaultContractResolver
  {
    protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
      Newtonsoft.Json.Serialization.JsonProperty prop = base.CreateProperty(member, memberSerialization);
      JsonObjectAttribute attr = prop.DeclaringType.GetCustomAttribute<JsonObjectAttribute>();
      if (attr != null && attr.MemberSerialization == MemberSerialization.Fields &&
          member.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
      {
        prop.Ignored = true;
      }
      return prop;
    }
  }
  internal static class Converter
  {
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
      MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
      DateParseHandling = DateParseHandling.None,
      Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }

            },
    };
  }

  internal class ParseStringConverter : JsonConverter
  {
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null) return null;
      var value = serializer.Deserialize<string>(reader);
      long l;
      if (long.TryParse(value, out l))
      {
        return l;
      }
      throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, Newtonsoft.Json.JsonSerializer serializer)
    {
      if (untypedValue == null)
      {
        serializer.Serialize(writer, null);
        return;
      }
      var value = untypedValue;
      serializer.Serialize(writer, value.ToString());
      return;
    }



    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
  }
}
