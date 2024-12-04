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
