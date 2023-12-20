using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shiratech_Params.Utils
{
  public class MqttHelper
  {
    private static MqttHelper _ins;

    public static MqttHelper Ins
    {
      get { return _ins == null ? _ins = new MqttHelper() : _ins; }
      set { _ins = value; }
    }

    private IMqttClient client_station;
    public bool isConnected => (bool)(client_station?.IsConnected);
    public bool isInited = false;
    [JsonProperty("host")]
    public string host = "";
    [JsonProperty("port")]
    public int port = 1883;
    [JsonProperty("pubTopic")]
    public string pubTopic = "";
    [JsonProperty("subTopic")]
    public string subTopic = "";
    public async void MQTT_Client_Config()
    {

      if (client_station == null)
      {
        try
        {

          var client_option = new MqttClientOptionsBuilder()
            .WithTcpServer(host, port)
            .Build();
          //
          client_station = new MqttFactory().CreateMqttClient();
          client_station.ConnectedAsync += Client_station_ConnectedAsync;

          client_station.DisconnectedAsync += Client_station_DisconnectedAsync;
          await client_station.ConnectAsync(client_option, CancellationToken.None);
          isInited = true;
        }
        catch (Exception ex)
        {

          isInited = false;
        }
      }
    }

    private async Task Client_station_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
    {
      await client_station.ReconnectAsync();
    }

    private async static Task Client_station_ConnectedAsync(MqttClientConnectedEventArgs arg)
    {

    }

    public void SendMsg(byte[] payload, string topic)
    {
      if (client_station.IsConnected)
      {
        //
        client_station.PublishAsync(new MqttApplicationMessage
        {
          Retain = true,
          Topic = topic,
          Payload = payload
        });
      }
    }
    public async Task SendMsg(string payload, string topic)
    {
      if (client_station.IsConnected)
      {
        //
        await client_station.PublishAsync(new MqttApplicationMessage
        {
          Retain = true,
          Topic = topic,
          Payload = Encoding.ASCII.GetBytes(payload)
        });
      }
    }
    public void SendMsg<T>(T obj, string topic, string msg) where T : class
    {
      SendMsg(Encoding.ASCII.GetBytes(msg), topic);
    }
  }
}
