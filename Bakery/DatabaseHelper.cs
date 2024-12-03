using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public static class DatabaseHelper
{
    private static string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    public static DataTable ExecuteQuery(string query)
    {
        using (SqlConnection connection = GetConnection())
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public static void ExecuteNonQuery(string query)
    {
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}