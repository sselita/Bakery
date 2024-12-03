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
        private List<Ingredient> Ingredients { get; set; }

        public Sandwich(string name, double basePrice , List<Ingredient> ingredients)
        {
            Name = name;
            BasePrice = basePrice;
            Ingredients = ingredients;
        }

        public string GetName() => Name;
        public BreadType GetBreadType() => BreadType;

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public string GetInfo()
        {
            var ingredientNames = Ingredients.Count > 0
                ? string.Join(", ", Ingredients.Select(i => i.GetName()))
                : "No ingredients added.";
            return $"{Name} ({BreadType} Bread) - Ingredients: {ingredientNames}";
        }

        public double GetPrice()
        {
            return BasePrice + Ingredients[0].Price;

        }

        public override string ToString() => $"{Name} (${GetPrice()})";
    }
}
