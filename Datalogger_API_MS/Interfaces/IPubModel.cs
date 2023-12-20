using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiratech_Params.Interfaces
{
  internal interface IPubModel : IModel
  {
    public string Payload { get; set; }


  }
}
