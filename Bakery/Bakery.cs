using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBakery;



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


        public List<Ingredient> GetIngridientBySandwichId(int id)
        {

            List<Ingredient> list = new List<Ingredient>();

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=localhost;Database=BakeryDB;Trusted_Connection=True;"))
                {
                    connection.Open();

                    string queryIng = "SELECT b.Name,b.Price FROM Sandwich JOIN SandwichIngredients c ON @id = c.SandwichId JOIN Ingridient b ON b.Id = c.IngredientId where Sandwich.Id = @Id;";
                    SqlCommand commanding = new SqlCommand(queryIng, connection);
                    commanding.Parameters.AddWithValue("Id", id);

                    using (SqlDataReader reader = commanding.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ingredient ingredient = new Ingredient
                            {
                                Name = reader.GetString(0),
                                Price = reader.GetDouble(1),
                            };
                            list.Add(ingredient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return list;
        }
     

        public List<Sandwich> GetAllSandwiches()
        {
            List<Sandwich> sandwiches = new List<Sandwich>();

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=localhost;Database=BakeryDB;Trusted_Connection=True;"))
                {
                    connection.Open();

                    string query = "SELECT  Name, BasePrice , BreadType FROM Sandwich where Sold = 0 or Sold is null";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sandwich sandwich = new Sandwich
                            {
                                Name = reader.GetString(0),    // Name
                                BasePrice = reader.GetDouble(1), // BasePrice
                                BreadType = (BreadType)Enum.Parse(typeof(BreadType), reader.GetString(2))
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
        public double GetRevenue()
        {     
            double revenue = 0;

            try
            {           
                var sandwiches = GetSoldSandwichess();
                foreach (var sand in sandwiches)
                {
                    revenue += sand.BasePrice;
                    var id = sand.Id;
                    var ingridients = GetIngridientBySandwichId(id);

                    foreach (var ingridient in ingridients)
                    {
                        revenue += ingridient.Price;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }         
            return revenue;
        }
        public List<Sandwich> GetSoldSandwichess()
        {
            List<Sandwich> list = new List<Sandwich> ();
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=localhost;Database=BakeryDB;Trusted_Connection=True;"))
                {
                    connection.Open();

                    string query = "SELECT  Name, BasePrice , BreadType , Id FROM Sandwich where Sold = 1";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sandwich sandwich = new Sandwich
                            {
                                Name = reader.GetString(0),    // Name
                                BasePrice = reader.GetDouble(1), // BasePrice
                                BreadType = (BreadType)Enum.Parse(typeof(BreadType), reader.GetString(2)),
                                Id = reader.GetInt32(3)
                            };
                            list.Add(sandwich);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
         
            return list;
        }

    }
   
    }
