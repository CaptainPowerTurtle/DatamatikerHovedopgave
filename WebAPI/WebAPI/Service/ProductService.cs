using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class ProductService
    {
        private static string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=WebAPI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";



        public static async Task getAllProducts(Utf8JsonWriter utf8JsonWriter)
        {
            using DbConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            DbCommand command = connection.CreateCommand();

            command.CommandText = "Select Id, [Name], Price from Product";
            using DbDataReader dbDataReader = await command.ExecuteReaderAsync();
            utf8JsonWriter.WriteStartArray();
            foreach (DbDataRecord item in dbDataReader)
            {
                Guid id = item.GetGuid(0);
                String name = item.GetString(1);
                int price = item.GetInt32(2);
                utf8JsonWriter.WriteStartObject();
                utf8JsonWriter.WriteString("Id", id);
                utf8JsonWriter.WriteString("Name", name);
                utf8JsonWriter.WriteNumber("Price", price);
                utf8JsonWriter.WriteEndObject();
            }
            utf8JsonWriter.WriteEndArray();
        }

        public static async Task CreateProduct(Product product)
        {
            string tempName = product.Name;
            int tempPrice = product.Price;
            using DbConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            DbCommand command = connection.CreateCommand();

            DbParameter dbId = command.CreateParameter();
            DbParameter dbName = command.CreateParameter();
            DbParameter dbPrice = command.CreateParameter();
            product.Id = Guid.NewGuid();
            dbId.ParameterName = "ProductId";
            dbId.Value = product.Id;
            command.Parameters.Add(dbId);


            dbName.ParameterName = "ProductName";
            dbName.Value = product.Name;
            command.Parameters.Add(dbName);

            dbPrice.ParameterName = "ProductPrice";
            dbPrice.Value = product.Price;
            command.Parameters.Add(dbPrice);


            command.CommandText = "INSERT INTO Product(Id, [Name], Price) VALUES (@ProductId,@ProductName,@ProductPrice)";
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }
    }
}