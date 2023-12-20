using Newtonsoft.Json;
using Shiratech_Params.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiratech_Params.Models
{
  public class Configuration
  {
    [JsonProperty("shiratechs")]
    public Shiratech[] Shiratechs { get; set; }
    [JsonProperty("mqttServer")]
    public MqttHelper MQTTConfig
    {
      get; set;
    }
    [JsonProperty("readInterval")]
    public int ReadInterval { get; set; }
    [JsonProperty("isRandom")]
    public bool isRandom { get; set; }
  }
}
