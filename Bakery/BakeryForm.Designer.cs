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
    private Label breadTypeLabel; 
    private CheckBox includeVatCheckBox;
    private Button listSandwichesButton;
    private List<Sandwich> sandwiches = new List<Sandwich>();
    private string connectionString = @"Server=localhost;Database=BakeryDB;Trusted_Connection=True;";



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
        breadTypeComboBox.Items.AddRange(new object[] { "White", "WholeGrain", "Sourdough" });
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
        
        button1.BackColor = Color.LightYellow;
        button1.FlatStyle = FlatStyle.Flat;
        button1.Font = new Font("Arial", 12F);
        button1.Location = new Point(277, 380);
        button1.Name = "button1";
        button1.Size = new Size(200, 40);
        button1.TabIndex = 7;
        button1.Text = "Total Revenue";
        button1.UseVisualStyleBackColor = false;
        button1.Click += button1_Click;
        // 
        // BakeryForm
     
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
        var sandwichForm = new AddSandwichForm(); 
        sandwichForm.ShowDialog();
    }

    private void ListSandwichesButton_Click(object sender, EventArgs e)
    {
        sandwichesListBox.Items.Clear();
       
        var sandwiches=    bakery.GetAllSandwiches();
        foreach (var sandwich in sandwiches)
        {
            sandwichesListBox.Items.Add(sandwich);
        }

    }

    private Button button1;
}
