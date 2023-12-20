using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shiratech_Params.Utils
{
  public class Schedule
  {
    public int timeInterval { get; set; } = 10;
    private System.Timers.Timer timer = new System.Timers.Timer();
    public async virtual Task Action()
    {
      Console.WriteLine("-------------Starting Action in Schedule--------------");
    }
    public Schedule()
    {
      timer.Elapsed += Timer_Elapsed;
    }
    public Schedule(int time)
    {
      timeInterval = time;

      timer.Elapsed += Timer_Elapsed;
    }
    public void StartScheduler()
    {
      timer.Start();
    }
    public void StopScheduler() { timer.Stop(); }
    private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      timer.Interval = timeInterval;
      timer.Stop();
      await Action();
      timer.Start();
    }
  }
}
