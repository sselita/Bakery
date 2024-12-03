using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RBakery
{
    public class Bakery
    {
        public string Name { get; private set; }
        public double VATPercentage { get; private set; }
        private List<Sandwich> Sandwiches { get; set; } = new List<Sandwich>();
        private List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public Bakery(string name, double vatPercentage)
        {
            Name = name;
            VATPercentage = vatPercentage;
        }

        public string GetName() => Name;

        public void AddSandwich(Sandwich sandwich)
        {
            Sandwiches.Add(sandwich);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public List<Ingredient> GetAvailableIngredients() => Ingredients;

        public List<Sanduic> GetAllSandwiches()
        {
            List<Sanduic> sandwiches = new List<Sanduic>();

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=your_server_name;Database=BakeryDB;Trusted_Connection=True;"))
                {
                    connection.Open();

                    string query = "SELECT Id, Name, BasePrice FROM Sandwich";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sanduic sandwich = new Sanduic
                            {
                                Id = reader.GetInt32(0),        // Id
                                Name = reader.GetString(1),    // Name
                                BasePrice = reader.GetDouble(2) // BasePrice
                            };
                            sandwiches.Add(sandwich);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return sandwiches;
        }

        public string SellSandwich(string sandwichName)
        {
            var sandwich = Sandwiches.FirstOrDefault(s => s.GetName() == sandwichName);
            if (sandwich == null)
                return $"Sandwich '{sandwichName}' not found.";
            Sandwiches.Remove(sandwich);
            double priceWithVAT = sandwich.GetPrice() * (1 + VATPercentage / 100);
            return $"Sold '{sandwichName}' for ${priceWithVAT:F2} (VAT included).";
        }
        public double GetSoldSandwiches()
        {
        
            double revenue = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=your_server_name;Database=BakeryDB;Trusted_Connection=True;"))
                {
                    connection.Open();

                    string query = "SELECT Id, Name, BasePrice FROM Sandwich";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sanduic sandwich = new Sanduic
                            {
                                Id = reader.GetInt32(0),        // Id
                                Name = reader.GetString(1),    // Name
                                BasePrice = reader.GetDouble(2) // BasePrice
                            };
                            revenue = revenue + sandwich.BasePrice;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return revenue;
        }
    }
}
