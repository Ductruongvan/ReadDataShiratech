using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiratech_Params.Models
{
    public class Sensor
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("registers")]
        public List<Register> Registers { get; set; }
        public string Description { get; set; }
        public Sensor()
        {

        }
        public Sensor(string name, List<Register> list_read_registers, string description)
        {
            Name = name;
            Registers = list_read_registers;
            Description = description;
        }
        public Sensor(string name, List<Register> list_read_registers)
        {
            Name = name;
            Registers = list_read_registers;
        }
    }
}
