using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiratech_Params.Models
{
  public static class Extensions
  {
    public static PubDTO TransferDTO(this Shiratech deviceData)
    {
      if (deviceData == null) return null;
      PubDTO ret = new PubDTO();
      ret.msg = new Msg()
      {
        title = "Monitor station",
        sender = "IaDrang1",
        group = "Datalogger",
        date = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
      };
      ret.content = new Content()
      {
        devices = new List<Device>(),
        tanks = new List<object>(),
        controller = new Controller()
        {
          operation_mode = "auto"
        }
      };
      Device de = new Device();
      de.solution = new List<Solution>();
      de.value = "ON";
      de.serial = "IaDrang1.Accelerometer";

      foreach (var s in deviceData.Sensors)
      {

        de.solution.AddRange(s.ToSolutionDTO());
      }
      ret.content.devices.Add(de);
      return ret;

    }

    public static List<Solution> ToSolutionDTO(this Sensor sensorData)
    {
      if(sensorData.Registers== null) return null;
      List<Solution> ret = new List<Solution>();
      foreach (var reg in sensorData.Registers)
      {
        if (reg.Name.Contains("Temperature"))
        {
          reg.Type = eRegisterType.Float32;
        }
        if (reg.Name.Contains("Magnetometer") || reg.Name.Contains("Accelerometer"))
        {
          reg.Type = eRegisterType.Int16;
         
        }
        string val = "0.00";
        switch (reg.Type)
        {
          case eRegisterType.Int32:


            break;
          case eRegisterType.Int16:
            int value=0;
            if (reg.Name.Contains("Magnetometer"))
            {
              reg.Type = eRegisterType.Int16;
              value = (reg.Value[0] + reg.Value[1] * 65536) / 16;
            }
            else if (reg.Name.Contains("Accelerometer"))
            {
              value = (reg.Value[0] + reg.Value[1] * 65536) / 1000;
            }
            val = value.ToString();
            break;
          case eRegisterType.String:
            val = reg.Value.ToString();
            break;
          case eRegisterType.Bit:
            val = reg.Value[0] > 0 ? "ON" : "OFF";
            break;
          case eRegisterType.Float32:
            if (reg.Name.Contains("Temp"))
            {
              if (reg.Value[0] > 40)
              {
                reg.Value[0] = 40;
                

              }
              val = reg.Value[0].ToString();
            }
            
             break;
          default: break;
        }
        ret.Add(new Solution()
        {
          env = reg.Name,
          value = val,
        });
      }
      return ret;
    }
  }
}
