using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt
{
    internal class DatabaseManager
    {
        private static DatabaseManager instance;

        public static DatabaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseManager();
                }
                return instance;
            }
        }

        private DatabaseManager()
        {
        }

        public void CreateTables()
        {
            string connectionString = "Host=localhost;Username=postgres;Password=xxxxxx;Database=Icecold TD";
            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

            string createTableLoginSystem = "CREATE TABLE IF NOT EXISTS Login_system (Login_id SERIAL PRIMARY KEY, Player_name VARCHAR(20) NOT NULL UNIQUE, Player_password VARCHAR(30) NOT NULL)";
            string createTablePlayer = "CREATE TABLE IF NOT EXISTS Player (Player_id SERIAL PRIMARY KEY, Login_id INTEGER NOT NULL UNIQUE, Name VARCHAR(20) NOT NULL, Health INTEGER, Round INTEGER, Kills INTEGER, Fish_money INTEGER, Total_money INTEGER, Tiles_seed INTEGER NOT NULL, FOREIGN KEY (Login_id) REFERENCES Login_system(Login_id))";
            string createTablePenguin = "CREATE TABLE IF NOT EXISTS Penguin (Penguin_id SERIAL PRIMARY KEY, Player_id INTEGER NOT NULL UNIQUE, Position INTEGER NOT NULL, Levels INTEGER NOT NULL, FOREIGN KEY (Player_id) REFERENCES Player(Player_id))";

            try
            {
                using (NpgsqlConnection conn = dataSource.OpenConnection())
                {
                    using (NpgsqlCommand cmd1 = new NpgsqlCommand(createTableLoginSystem, conn))
                    {
                        cmd1.ExecuteNonQuery();                        
                    }
                    using (NpgsqlCommand cmd2 = new NpgsqlCommand(createTablePlayer, conn))
                    {
                        cmd2.ExecuteNonQuery();                        
                    }
                    using (NpgsqlCommand cmd3 = new NpgsqlCommand(createTablePenguin, conn))
                    {
                        cmd3.ExecuteNonQuery();                        
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void Start()
        {

        }

        public void Registering()
        {

        }

        public void Login()
        {

        }
    }
}
