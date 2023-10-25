using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        const string _dbSource = "azdb002.database.windows.net";
        const string _dbUser = "sqladmin";
        const string _dbPassword = "v@ReRfSeVL8qq4Q";
        const string _dbName = "app-db";

        private SqlConnection GetConnection()
        {
            var sb = new SqlConnectionStringBuilder();
            sb.DataSource = _dbSource;
            sb.UserID = _dbUser;
            sb.Password = _dbPassword;
            sb.InitialCatalog = _dbName;

            return new SqlConnection(sb.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();

            var sql = "SELECT ProductID, ProductName, Quantity FROM Products";
            using(var conn = GetConnection())
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
