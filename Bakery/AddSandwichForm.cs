using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RBakery
{
    public partial class AddSandwichForm : Form
    {


        public AddSandwichForm()
        {
            InitializeComponent();
        }
           
        

   
        private void AddButtonSandwich_Click(object sender, EventArgs e)
        {
            string sandwichName = sandwichNameTextBox.Text;
            string breadType = breadTypeComboBox.SelectedItem?.ToString();

            // Validation: Check if the fields are filled
            if (string.IsNullOrEmpty(sandwichName) || string.IsNullOrEmpty(breadType))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Collect selected ingredients
            List<string> selectedIngredients = new List<string>();
            foreach (CheckBox checkbox in ingredientsPanel.Controls)
            {
                if (checkbox.Checked)
                {
                    selectedIngredients.Add(checkbox.Text);
                }
            }

            // Validation: Ensure at least one ingredient is selected
            if (selectedIngredients.Count == 0)
            {
                MessageBox.Show("Please select at least one ingredient.");
                return;
            }

      
            // Example: Updating an in-memory list or database could happen here.
            MessageBox.Show($"Sandwich '{sandwichName}' added with ingredients: {string.Join(", ", selectedIngredients)}");

            // Optionally, you can reset the form for further use.
            this.Close();
        }
    }
}
