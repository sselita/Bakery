using System;
using System.Windows.Forms;
using System.Collections.Generic;
using RBakery;
using System.Data.SqlClient;

public partial class BakeryForm : Form
{
    private Button addSandwichButton;
    private ListBox sandwichesListBox;
    private Button sellSandwichButton;
    private ComboBox breadTypeComboBox;
    private Label breadTypeLabel; // Label for Bread Type
    private CheckBox includeVatCheckBox;
    private Button listSandwichesButton;
    private List<Sandwich> sandwiches = new List<Sandwich>();
    private string connectionString = @"Server=your_server_name;Database=BakeryDB;Trusted_Connection=True;";



    private void InitializeComponent()
    {
        addSandwichButton = new Button();
        sandwichesListBox = new ListBox();
        sellSandwichButton = new Button();
        breadTypeComboBox = new ComboBox();
        breadTypeLabel = new Label();
        includeVatCheckBox = new CheckBox();
        listSandwichesButton = new Button();
        button1 = new Button();
        SuspendLayout();
        // 
        // addSandwichButton
        // 
        addSandwichButton.BackColor = Color.LightGreen;
        addSandwichButton.FlatStyle = FlatStyle.Flat;
        addSandwichButton.Font = new Font("Arial", 12F);
        addSandwichButton.Location = new Point(20, 160);
        addSandwichButton.Name = "addSandwichButton";
        addSandwichButton.Size = new Size(200, 40);
        addSandwichButton.TabIndex = 3;
        addSandwichButton.Text = "Add New Sandwich";
        addSandwichButton.UseVisualStyleBackColor = false;
        addSandwichButton.Click += AddSandwichesButton_Click;
        // 
        // sandwichesListBox
        // 
        sandwichesListBox.Font = new Font("Arial", 10F);
        sandwichesListBox.ItemHeight = 19;
        sandwichesListBox.Location = new Point(20, 218);
        sandwichesListBox.Name = "sandwichesListBox";
        sandwichesListBox.Size = new Size(250, 137);
        sandwichesListBox.TabIndex = 4;
        // 
        // sellSandwichButton
        // 
        sellSandwichButton.BackColor = Color.LightCoral;
        sellSandwichButton.FlatStyle = FlatStyle.Flat;
        sellSandwichButton.Font = new Font("Arial", 12F);
        sellSandwichButton.Location = new Point(20, 380);
        sellSandwichButton.Name = "sellSandwichButton";
        sellSandwichButton.Size = new Size(200, 40);
        sellSandwichButton.TabIndex = 5;
        sellSandwichButton.Text = "Sell Sandwich";
        sellSandwichButton.UseVisualStyleBackColor = false;
        sellSandwichButton.Click += SellSandwichButton_Click;
        // 
        // breadTypeComboBox
        // 
        breadTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        breadTypeComboBox.Font = new Font("Arial", 12F);
        breadTypeComboBox.Items.AddRange(new object[] { "White Bread", "Whole Wheat", "Sourdough", "Multigrain" });
        breadTypeComboBox.Location = new Point(20, 60);
        breadTypeComboBox.Name = "breadTypeComboBox";
        breadTypeComboBox.Size = new Size(200, 31);
        breadTypeComboBox.TabIndex = 1;
        // 
        // breadTypeLabel
        // 
        breadTypeLabel.Font = new Font("Arial", 12F);
        breadTypeLabel.Location = new Point(20, 30);
        breadTypeLabel.Name = "breadTypeLabel";
        breadTypeLabel.Size = new Size(200, 20);
        breadTypeLabel.TabIndex = 0;
        breadTypeLabel.Text = "Bread Type:";
        // 
        // includeVatCheckBox
        // 
        includeVatCheckBox.Font = new Font("Arial", 12F);
        includeVatCheckBox.Location = new Point(253, 170);
        includeVatCheckBox.Name = "includeVatCheckBox";
        includeVatCheckBox.Size = new Size(200, 30);
        includeVatCheckBox.TabIndex = 6;
        includeVatCheckBox.Text = "Include VAT";
        // 
        // listSandwichesButton
        // 
        listSandwichesButton.BackColor = Color.LightYellow;
        listSandwichesButton.FlatStyle = FlatStyle.Flat;
        listSandwichesButton.Font = new Font("Arial", 12F);
        listSandwichesButton.Location = new Point(20, 110);
        listSandwichesButton.Name = "listSandwichesButton";
        listSandwichesButton.Size = new Size(200, 40);
        listSandwichesButton.TabIndex = 2;
        listSandwichesButton.Text = "List All Sandwiches";
        listSandwichesButton.UseVisualStyleBackColor = false;
        listSandwichesButton.Click += ListSandwichesButton_Click;
        // 
        // button1
        // 
        button1.BackColor = Color.LightYellow;
        button1.FlatStyle = FlatStyle.Flat;
        button1.Font = new Font("Arial", 12F);
        button1.Location = new Point(277, 380);
        button1.Name = "button1";
        button1.Size = new Size(200, 40);
        button1.TabIndex = 7;
        button1.Text = "TotalRevenue";
        button1.UseVisualStyleBackColor = false;
        button1.Click += button1_Click;
        // 
        // BakeryForm
        // 
        ClientSize = new Size(600, 450);
        Controls.Add(button1);
        Controls.Add(breadTypeLabel);
        Controls.Add(breadTypeComboBox);
        Controls.Add(listSandwichesButton);
        Controls.Add(addSandwichButton);
        Controls.Add(sandwichesListBox);
        Controls.Add(sellSandwichButton);
        Controls.Add(includeVatCheckBox);
        Name = "BakeryForm";
        Text = "Bakery Management";

        ResumeLayout(false);
    }

    private void AddSandwichesButton_Click(object sender, EventArgs e)
    {
        var sandwichForm = new AddSandwichForm(); // Add Sandwich Form (modify to match your design)
        sandwichForm.ShowDialog();
    }

    private void ListSandwichesButton_Click(object sender, EventArgs e)
    {
        sandwichesListBox.Items.Clear();
        LoadSampleSandwiches();
        // Display all sandwiches in the ListBox
        foreach (var sandwich in sandwiches)
        {
            sandwichesListBox.Items.Add(sandwich);
        }

    }


    private void LoadSampleSandwiches()
    {
        // Create ingredients
        var ingredients1 = new List<Ingredient>
    {
        new Ingredient("Cucumber", 0.5),
        new Ingredient("Tomato", 0.15),
        new Ingredient("Cheddar", 0.2)
    };
        var sandwich1 = new Sandwich("Veggie Delight", 5.99, ingredients1);

        var ingredients2 = new List<Ingredient>
    {
        new Ingredient("Chicken", 2.0),
        new Ingredient("Lettuce", 0.3),
        new Ingredient("Tomato", 0.15),
        new Ingredient("Cheddar", 0.2)
    };
        var sandwich2 = new Sandwich("Chicken Sandwich", 7.99, ingredients2);

        var ingredients3 = new List<Ingredient>
    {
        new Ingredient("Turkey", 2.5),
        new Ingredient("Avocado", 1.0),
        new Ingredient("Bacon", 1.2)
    };
        var sandwich3 = new Sandwich("Turkey Avocado", 8.99, ingredients3);

        // Add sandwiches to the list
        sandwiches.Add(sandwich1);
        sandwiches.Add(sandwich2);
        sandwiches.Add(sandwich3);
    }

    private Button button1;
}
