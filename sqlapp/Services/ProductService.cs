using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("Default"));
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();

            var sql = "SELECT ProductID, ProductName, Quantity FROM Products";
            using (var conn = GetConnection())
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = sql;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product()
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Quantity = reader.GetInt32(2)
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }
    }
}
