using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Warehouse.Entity;

namespace Warehouse.Repositories
{
    class ConsignmentRepositories : IDisposable
    {
        static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        SqlConnection Connect;

        public ConsignmentRepositories()
        {
            Connect = new SqlConnection(ConnectionString);
        } 

        public void Dispose() => Connect.Dispose();
   
        public void AddConsignment(Consignment consignment, IEnumerable<ConsignmentSize> consignmentSize)
        {
            int lastId;
            int var = 1;
            if (consignment.Type =="Take")
            {
                var = -1;
            }
            Connect.Open();
            try
            {
                string query = "INSERT INTO Consignment (Date, Type) VALUES" +
                "(@Date, @Type)";
                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@Date", consignment.Date);
                cmd.Parameters.AddWithValue("@Type", consignment.Type);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@IDENTITY";
                lastId = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var cons in consignmentSize)
                {
                    query = "INSERT INTO ConsignmentSize (GoodsId, Quantity, ConsignmentId) VALUES " +
                    "(@GoodsId, @Quantity, @ConsignmentId);" +
                    "UPDATE Goods SET Quantity = Quantity + @Var WHERE GoodsId = @GoodsId;";
                    cmd = new SqlCommand(query, Connect);
                    cmd.Parameters.AddWithValue("@GoodsId", cons.GoodsId);
                    cmd.Parameters.AddWithValue("@Quantity", cons.Quantity);
                    cmd.Parameters.AddWithValue("@ConsignmentId", lastId);
                    cmd.Parameters.AddWithValue("Var", cons.Quantity * var);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                Connect.Close();
            }
        }
    }
}
