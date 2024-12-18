﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RBakery
{
    public partial class AddSandwichForm : Form
    {
        private TextBox sandwichNameTextBox;
        private ComboBox breadTypeComboBox;
        private FlowLayoutPanel ingredientsPanel;
        private Button addButton;
        private List<string> ingredientsListAdd;
        private string connectionString = @"Server=localhost;Database=BakeryDB;Trusted_Connection=True;";


        public AddSandwichForm(List<string> ingredients)
        {
            ingredientsListAdd = ingredients;
        }

        private void InitializeComponent()
        {
            this.sandwichNameTextBox = new TextBox();
            this.breadTypeComboBox = new ComboBox();
            this.ingredientsPanel = new FlowLayoutPanel();
            this.addButton = new Button();

            this.Text = "Add New Sandwich";
            this.ClientSize = new Size(450, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 12);
            this.BackColor = Color.White;

        
            var sandwichNameLabel = new Label()
            {
                Text = "Sandwich Name:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Width = 180,
                Height = 30,
                Top = 20
            };
            sandwichNameTextBox.Top = 50;
            sandwichNameTextBox.Left = 10;
            sandwichNameTextBox.Width = 400;
            sandwichNameTextBox.Font = new Font("Segoe UI", 12);
            sandwichNameTextBox.BorderStyle = BorderStyle.FixedSingle;
            sandwichNameTextBox.PlaceholderText = "Enter Sandwich Name";

            this.Controls.Add(sandwichNameLabel);
            this.Controls.Add(sandwichNameTextBox);


       
            var breadTypeLabel = new Label()
            {
                Text = "Bread Type:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Width = 180,
                Height = 30,
                Top = 100
            };
            breadTypeComboBox.Top = 130;
            breadTypeComboBox.Left = 10;
            breadTypeComboBox.Width = 400;
            breadTypeComboBox.Items.AddRange(new string[] { "Ciabatta", "Focaccia", "Sour_dough","Rye","Whole_wheat" });
            breadTypeComboBox.Font = new Font("Segoe UI", 12);
            breadTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            this.Controls.Add(breadTypeLabel);
            this.Controls.Add(breadTypeComboBox);

   
            var ingredientsLabel = new Label()
            {
                Text = "Select Ingredients:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Width = 180,
                Height = 30,
                Top = 180
            };

            ingredientsPanel.Top = 210;
            ingredientsPanel.Left = 10;
            ingredientsPanel.Width = 400;
            ingredientsPanel.Height = 100;
            ingredientsPanel.FlowDirection = FlowDirection.TopDown;
            ingredientsPanel.AutoScroll = true;
            ingredientsPanel.Padding = new Padding(10);

            var ingredientItems = new List<Tuple<string, double>>()
            {
                Tuple.Create("Cucumber", 0.5),
                Tuple.Create("Tomato", 0.15),
                Tuple.Create("Cheddar", 0.2),
                Tuple.Create("Ham", 0.1),
                Tuple.Create("Chorizo", 0.25),
                Tuple.Create("Apple", 0.1),
                Tuple.Create("Tuna", 0.2),
                Tuple.Create("Smoked Salmon", 0.4),
                Tuple.Create("Cream Cheese", 0.15),
                Tuple.Create("Gouda Cheese", 0.1),

            };

            foreach (var ingredient in ingredientItems)
            {
                var checkBox = new CheckBox()
                {
                    Text = ingredient.Item1,
                    Font = new Font("Segoe UI", 12),
                    AutoSize = true,
                    Padding = new Padding(5),
                    Width = 380
                };
                ingredientsPanel.Controls.Add(checkBox);
            }

            this.Controls.Add(ingredientsLabel);
            this.Controls.Add(ingredientsPanel);

        
            addButton.Text = "Add Sandwich";
            addButton.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            addButton.BackColor = Color.LightGreen;
            addButton.ForeColor = Color.White;
            addButton.Top = 330;
            addButton.Left = 10;
            addButton.Width = 400;
            addButton.Height = 45;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.FlatAppearance.BorderSize = 0;
            addButton.Cursor = Cursors.Hand;
            addButton.Click += new EventHandler(this.AddButton_Click);

            this.Controls.Add(addButton);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string sandwichName = sandwichNameTextBox.Text;
            string breadType = breadTypeComboBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(sandwichName) || string.IsNullOrEmpty(breadType))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

           
            List<string> selectedIngredients = new List<string>();
            foreach (CheckBox checkbox in ingredientsPanel.Controls)
            {
                if (checkbox.Checked)
                {
                    selectedIngredients.Add(checkbox.Text);
                }
            }

            if (selectedIngredients.Count == 0)
            {
                MessageBox.Show("Please select at least one ingredient.");
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

              
                    string insertSandwichQuery = "INSERT INTO Sandwich (Name, BasePrice ,BreadType) OUTPUT INSERTED.Id VALUES (@Name, @BasePrice,@BreadType)";
                    SqlCommand sandwichCommand = new SqlCommand(insertSandwichQuery, connection);

                   double basePrice = 2.0; 
                    sandwichCommand.Parameters.AddWithValue("@Name", sandwichName);           
                    sandwichCommand.Parameters.AddWithValue("@BreadType", breadType);
                    sandwichCommand.Parameters.AddWithValue("@BasePrice", basePrice);
           //         sandwichCommand.ExecuteNonQuery();

                    int sandwichId = (int)sandwichCommand.ExecuteScalar(); 

                    // Insert into middle table
                    foreach (string ingredient in selectedIngredients)
                    {
                       
                        string getIngredientIdQuery = "SELECT Id FROM Ingridient WHERE Name = @Name";
                        SqlCommand ingredientCommand = new SqlCommand(getIngredientIdQuery, connection);
                        ingredientCommand.Parameters.AddWithValue("@Name", ingredient);

                        object ingredientIdObj = ingredientCommand.ExecuteScalar();
                        if (ingredientIdObj == null)
                        {
                            MessageBox.Show($"Ingredient '{ingredient}' not found in database.");
                            continue;
                        }
                        int ingredientId = (int)ingredientIdObj;

                        // Insert into Sandwich_Ingredients table
                        string insertSandwichIngredientQuery = "INSERT INTO SandwichIngredients (SandwichId, IngredientId) VALUES (@SandwichId, @IngredientId)";
                        SqlCommand sandwichIngredientCommand = new SqlCommand(insertSandwichIngredientQuery, connection);
                        sandwichIngredientCommand.Parameters.AddWithValue("@SandwichId", sandwichId);
                        sandwichIngredientCommand.Parameters.AddWithValue("@IngredientId", ingredientId);
                        sandwichIngredientCommand.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Sandwich '{sandwichName}' added successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
           
          
            this.Close();
         
        }
   

    }
}
