using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBakery
{
    public class Sandwich
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public double BasePrice { get;  set; }
        public BreadType BreadType { get;  set; }
        public bool Sold { get; set; } = false;

        public Sandwich()
        { 
        }

        public Sandwich(string name, double basePrice , BreadType breadType)
        {
            Name = name;
            BasePrice = basePrice;
            BreadType = breadType;
      
        }

        public string GetName() => Name;


        public string GetInfo()
        {
          
            return $"{Name} ({BreadType} Bread)";
        }

        public double GetPrice()
        {
            return BasePrice;

        }

        public override string ToString() => $"{Name} (${GetPrice()})";
    }
}
