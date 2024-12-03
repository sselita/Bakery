using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBakery
{
    public class Ingredient
    {
        public int  Id { get; set; }
        public string Name { get; private set; }
        public double Price { get; private set; }

        public Ingredient(string name, double price)
        {
            Name = name;
            Price = price;
 
        }

        public string GetName() => Name;
        public double GetPrice() => Price;

        public override string ToString() => $"{Name} (${Price})";
    }
}
