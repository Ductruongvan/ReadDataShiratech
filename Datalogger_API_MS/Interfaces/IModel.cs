using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiratech_Params.Interfaces
{

  internal interface IModel
  {
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string Name { get; set; }
  }
}
