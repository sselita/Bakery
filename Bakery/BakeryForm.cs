using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using RBakery;

public partial class BakeryForm : Form
{
    private Bakery bakery;

    public BakeryForm()
    {
        InitializeComponent();
        bakery = new Bakery("R Bakery", 9);
        UpdateUI();
    }
    public void GetIngridients(object sender, EventArgs e)
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        if (sandwichesListBox.SelectedItem is Sandwich selectedSandwich)
        {
            var id = selectedSandwich.Id;

            ingredients = bakery.GetIngridientBySandwichId(id);

        }
        foreach ( var ingredient in ingredients)
        {
          
                listBox1.Items.Add(ingredient);

        }
    }
    private void SellSandwichButton_Click(object sender, EventArgs e)
    {
        if (sandwichesListBox.SelectedItem is Sandwich selectedSandwich)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    string updateQuery = "UPDATE Sandwich SET Sold = 1 WHERE Name = @Name";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", selectedSandwich.Name);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Sold Sandwich: {selectedSandwich.Name}");
                        }
                        else
                        {
                            MessageBox.Show("Failed to sell the sandwich. It might not exist in the database.");
                        }
                    }
                }
                listBox1.Items.Clear();
                UpdateUI();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Please select a sandwich to sell.");
        }
        UpdateUI();
    }
    private void button1_Click(object sender, EventArgs e)
    {
        var revenue = bakery.GetRevenue();
        MessageBox.Show("Revenue is :" + revenue);
    }

    private void UpdateUI()
    {
        sandwichesListBox.DataSource = null;
        
        sandwichesListBox.DataSource = bakery.GetAllSandwiches();
    }

    
}
