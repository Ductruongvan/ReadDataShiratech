using Newtonsoft.Json;
using Shiratech_Params.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiratech_Params.Models
{
  public class Content
  {
    public Controller controller { get; set; }
    public List<object> tanks { get; set; }
    public List<Device> devices { get; set; }
  }

  public class Controller
  {
    public string operation_mode { get; set; }
  }

  public class Device
  {
    public List<Solution> solution { get; set; }
    public string serial { get; set; }
    public string value { get; set; }
  }

  public class Msg
  {
    public string title { get; set; }
    public string sender { get; set; }
    public string group { get; set; }
    public long date { get; set; }
  }

  public class PubDTO
  {
    public Msg msg { get; set; }
    public Content content { get; set; }
  }

  public class Solution
  {
    public string env { get; set; }
    public string value { get; set; }
  }

}
