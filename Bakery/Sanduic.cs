using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBakery
{
    public class Sanduic
    {
      
            public int Id { get; set; }
            public string Name { get; set; }
            public double BasePrice { get; set; }
            public bool Sold { get; set; } = false;
    }
}
