#define Dev
using Newtonsoft.Json;
using Shiratech_Params.Controllers;
using Shiratech_Params.Models;
using Shiratech_Params.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Shiratech_Params
{

  public class MainControl : Schedule
  {


    private Shiratech[] shiratechs;
    private static Configuration confiuration = null;
    private bool isRandom = false;
    public void StartService()
    {
      using (StreamReader r = new StreamReader("config.json"))
      {
        string json = r.ReadToEnd();
        confiuration = Helper<Configuration>.FromJson(json);
        this.shiratechs = confiuration.Shiratechs;
        this.isRandom = confiuration.isRandom;
      }
      StartScheduler();
      MqttHelper.Ins = confiuration.MQTTConfig;
      timeInterval = confiuration.ReadInterval;

    }
    public MainControl()
    {

    }
    public MainControl(int time) : base(time)
    {
      timeInterval = time;


    }


    public async override Task Action()
    {

      if (shiratechs == null || shiratechs.Length <= 0)
      {
        return;
      }
      Console.WriteLine("-Action: Starting Action in Shiratech--------------");
      var shiratech_dto = new List<PubDTO>();
      try
      {
        if (isRandom)
        {
          Console.WriteLine($"----------------- Virtual -------------");
          //duc cmt 
          //Random r = new Random();
          //foreach (var shiratech in shiratechs)
          //{

          //  Console.WriteLine($"- connection status: {true}");
          //  shiratech.Sensors.ForEach(sensor =>
          //  {
          //    Console.WriteLine($"*** Read Sensor {sensor.Name}");
          //    sensor.Registers.ForEach(reg =>
          //    {
          //      if (reg.Value == null) reg.Value = new int[2];
          //      if (reg.Name.Contains("Accelerometer"))
          //        reg.Value[0] = r.Next(-100000, 100000);

          //      else
          //      {
          //        reg.Value[0] = r.Next(-10, 100);
          //      }
          //      Console.WriteLine($"***** {reg.Name} - {sensor.Name}: {reg.Value?[0]} ");
          //    });
          //  });
          //  shiratech_dto.Add(shiratech.TransferDTO());

          //}
        }
        else
        {
          foreach (var shiratech in shiratechs)
          {
            var mb = new DeviceCommunication();
            mb.IPAddress = shiratech.IPAddress;
            mb.Port = shiratech.Port;
            mb.Connect();
            Console.WriteLine($"- connection status: {mb.Connected}");
            shiratech.Sensors.ForEach(async sensor =>
            {
              Console.WriteLine($"*** Read Sensor {sensor.Name}");
              var minAdd = sensor.Registers.Min(x => x.Address);
              var max = sensor.Registers.Max(x => x.Address);
              var quantity = max - minAdd + 1;
              int[] values;
              try
              {
                values = mb.ReadHoldingRegisters((int)minAdd, (int)quantity);

              }
              catch (Exception ex)
              {

                Console.WriteLine(ex.Message);
                return;
              }
              sensor.Registers.ForEach(reg =>
              {
                if (reg.Value == null) reg.Value = new int[2];
                reg.Value[0] = values[reg.Address - minAdd];


              });
              await Task.Delay(10);
            });
            var pubdto = shiratech.TransferDTO();
            Console.WriteLine($"*** PUBLIC PAYLOAD: {pubdto}");
            shiratech_dto.Add(pubdto);
            mb.Disconnect();

          }
        }


        //duc cmt nhớ mở cmt nhé :))

        Console.WriteLine($"*** Start publish MQTT");
        var msg = Serialize<PubDTO>.ToJson(shiratech_dto[0]);
        //if (!MqttHelper.Ins.isInited)
        //  MqttHelper.Ins.MQTT_Client_Config();
        //await MqttHelper.Ins.SendMsg(msg, MqttHelper.Ins.pubTopic);

      }
      catch (Exception ex)
      {

        Console.WriteLine($"-Error:  {ex.Message} in {ex.StackTrace}");
      }
      finally
      {
        Console.WriteLine("-Action: End Action in Shiratech--------------");
      }
    }



  }
}
