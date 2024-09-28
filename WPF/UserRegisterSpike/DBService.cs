using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegisterSpike
{
    public class DBService
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=KurtCob@in#1967;Database=Spikes";
        NpgsqlDataSource dataSource;

        public DBService()
        {
            var DSB = new NpgsqlDataSourceBuilder(connectionString);
            dataSource = DSB.Build();
            CreateTable();
        }

        public bool InsertUser(string username, string password)
        {
            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand($"INSERT INTO users (username, password) values (@u, @p);", conn)
                {
                    Parameters =
                        {
                            new("u", username),
                            new("p", password)
                        }
                };
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) 
            {
               throw;
            }
        }

        private void CreateTable()
        {
            var conn = dataSource.OpenConnection();
            var cmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS users (id SERIAL PRIMARY KEY, username VARCHAR NOT NULL, password VARCHAR NOT NULL);", conn);
            cmd.ExecuteNonQuery();
        }

        
    }
}
