using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiratech_Params.Controllers
{
  internal class DeviceCommunication : ModbusClient
  {
    public void ModbusConnect()
    {
      try
      {
        if (!this.Connected)
        {
          Console.WriteLine($"-Action: Connecting modbus server --------------");
          this.Connect();

        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"-Error: Modbus connect errror throw: {ex.Message}--------------");

      }
    }
  }
}
