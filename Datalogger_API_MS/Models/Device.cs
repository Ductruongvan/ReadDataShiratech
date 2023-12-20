using EasyModbus;
using Newtonsoft.Json;
using Shiratech_Params.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Shiratech_Params.Models
{
  public class Shiratech : IModel
  {


    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string Name { get; set; }

    #region Custom
    [JsonProperty("sensors")]
    public List<Sensor> Sensors = new List<Sensor>();
    [JsonProperty("IPAddress")]
    public string IPAddress { get; set; }
    [JsonProperty("Port")]
    public int Port { get; set; }
    #endregion
    public Shiratech CreateNewDevice(string name)
    {
      this.Id = Guid.NewGuid();
      this.Name = name;

      return this;
    }

  }
}
