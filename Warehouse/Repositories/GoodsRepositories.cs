using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Warehouse.Entity;

namespace Warehouse.Repositories
{
    class GoodsRepositories : IDisposable
    {
        static readonly string  ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        SqlConnection Connect;

        public GoodsRepositories() => Connect = new SqlConnection(ConnectionString);

        public void Dispose() => Connect.Dispose();

        public IEnumerable<Goods> GetGoods()
        {
            Connect.Open();
            string query = "SELECT * FROM Goods";
            SqlCommand cmd = new SqlCommand(query, Connect);

            List<Goods> goods = new List<Goods>();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    goods.Add(new Goods
                    {
                        GoodsId = (int)reader["GoodsId"],
                        GoodsName = (string)reader["GoodsName"],
                        Unit = (string)reader["Unit"],
                        Price = Convert.ToDouble(reader["Price"]),
                        Quantity = (double)reader["Quantity"]
                    });
                }
            }
            finally
            {
                Connect.Close();
            }
            return goods;
        }

        public void AddGoods(Goods goods)
        {
            Connect.Open();
            try
            {
                string query = "INSERT INTO Goods (GoodsName, Unit, Price, Quantity) VALUES" +
                "(@GoodsName, @Unit, @Price, @Quantity)";
                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@GoodsName", goods.GoodsName);
                cmd.Parameters.AddWithValue("@Unit", goods.Unit);
                cmd.Parameters.AddWithValue("@Price", goods.Price);
                cmd.Parameters.AddWithValue("@Quantity", goods.Quantity);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Connect.Close();
            }
        }
    }
}
