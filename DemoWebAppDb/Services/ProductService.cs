using DemoWebAppDb.Models;
using System.Data.SqlClient;

namespace DemoWebAppDb.Services
{
    public class ProductService
    {
        public static string db_source = "wenappserver.database.windows.net";
        public static string db_user = "sqladmin";
        public static string db_password = "Azure@1234";
        public static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            List<Product> _product_list = new List<Product>();
            string _statement = "SELECT ProductID,ProductName,Quantity FROM Products";
            SqlConnection _connection = GetConnection();
            
            _connection.Open();
            
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using(SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while(_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };

                    _product_list.Add(_product);
                }
            }

            _connection.Close();
            return _product_list;
        }
    }
}
