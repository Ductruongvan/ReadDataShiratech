using Newtonsoft.Json;
using Shiratech_Params.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiratech_Params.Models
{
  public enum eRegisterType
  {
    Int32,
    Int16,
    String,
    Bit,
    Float32
  }
  public class Register
  {
    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Address")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Address { get; set; }

    [JsonProperty("Description")]
    public string Description { get; set; }
    public int[] Value { get; set; }

    public eRegisterType Type { get; set; }
    public Register(string name, int address, int value, string description, eRegisterType type)
    {
      Name = name;
      Address = address;

      Description = description;
      Type = type;
    }
    public Register()
    {

    }
  }
}
