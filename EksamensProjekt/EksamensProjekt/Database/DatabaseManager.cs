using LiteDB;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using EksamensProjekt.MapGeneration;

namespace EksamensProjekt.Database
{
    internal class DatabaseManager
    {
        private static DatabaseManager instance;
        private static readonly string _connectionString = "Filename=Icecold TD data.db;";
        private static Player player;

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

        //public void CreateTables()
        //{
        //    string connectionString = "Host=localhost;Username=postgres;Password=xxxxxx;Database=Icecold TD";
        //    NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

        //    string createTableLoginSystem = "CREATE TABLE IF NOT EXISTS Login_system (Login_id SERIAL PRIMARY KEY, Player_name VARCHAR(20) NOT NULL UNIQUE, Player_password VARCHAR(30) NOT NULL)";
        //    string createTablePlayer = "CREATE TABLE IF NOT EXISTS Player (Player_id SERIAL PRIMARY KEY, Login_id INTEGER NOT NULL UNIQUE, Name VARCHAR(20) NOT NULL, Health INTEGER, Round INTEGER, Kills INTEGER, Fish_money INTEGER, Total_money INTEGER, Tiles_seed INTEGER NOT NULL, FOREIGN KEY (Login_id) REFERENCES Login_system(Login_id))";
        //    string createTablePenguin = "CREATE TABLE IF NOT EXISTS Penguin (Penguin_id SERIAL PRIMARY KEY, Player_id INTEGER NOT NULL UNIQUE, Position INTEGER NOT NULL, Levels INTEGER NOT NULL, FOREIGN KEY (Player_id) REFERENCES Player(Player_id))";

        //    try
        //    {
        //        using (NpgsqlConnection conn = dataSource.OpenConnection())
        //        {
        //            using (NpgsqlCommand cmd1 = new NpgsqlCommand(createTableLoginSystem, conn))
        //            {
        //                cmd1.ExecuteNonQuery();                        
        //            }
        //            using (NpgsqlCommand cmd2 = new NpgsqlCommand(createTablePlayer, conn))
        //            {
        //                cmd2.ExecuteNonQuery();                        
        //            }
        //            using (NpgsqlCommand cmd3 = new NpgsqlCommand(createTablePenguin, conn))
        //            {
        //                cmd3.ExecuteNonQuery();                        
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        public static bool RegisterUser(string playername, string password)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var loginsystems = db.GetCollection<LoginSystem>("loginsystems");
                var players = db.GetCollection<Player>("players");
                if (loginsystems.Exists(x => x.PlayerName == playername))
                {
                    return false;
                }

                // Create a new login system
                int newLoginId = 1;
                var highestLoginIDUser = loginsystems.FindAll().OrderByDescending(x => x.LoginId).FirstOrDefault();
                if (highestLoginIDUser != null)
                {
                    newLoginId = highestLoginIDUser.LoginId + 1;
                    Globals.LoginId = newLoginId;
                }

                // Create a new player
                var loginsystem = new LoginSystem
                {
                    LoginId = newLoginId,
                    PlayerName = playername,
                    PlayerPasswordHash = HashPassword(password)
                };      
                
                loginsystems.Insert(loginsystem);

                int newPlayerId = 1;
                var highestPlayerIDUser = players.FindAll().OrderByDescending(x => x.PlayerId).FirstOrDefault();
                if (highestPlayerIDUser != null)
                {
                    newPlayerId = highestPlayerIDUser.PlayerId + 1;
                }

                player = new Player
                {
                    PlayerId = newPlayerId,
                    LoginId = newLoginId,
                    TotalMoney = 0,
                    TotalKills = 0,
                    TotalRound = 0
                };

                players.Insert(player);

                return true;
            }
        }

        private static string HashPassword(string password)
        {
            // Create a SHA256 hash of the password
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool LoginUser(string playername, string password)
        {
            // Check if the user exists and the password is correct
            using var db = new LiteDatabase(_connectionString);

            var loginsystems = db.GetCollection<LoginSystem>("loginsystems");
            var loginsystem = loginsystems.FindOne(x => x.PlayerName == playername);
            var players = db.GetCollection<Player>("players");


            if (loginsystem == null)
            {
                return false;
            }

            var passwordHash = HashPassword(password);

            if (loginsystem.PlayerPasswordHash == passwordHash)
            {
                player = players.FindOne(x => x.LoginId == loginsystem.LoginId);
                Globals.LoginId = player.LoginId;
            }

            return loginsystem.PlayerPasswordHash == passwordHash;
        }

        public static void UpdatePlayerStats(int loginId)
        {
            // Update the player stats in the database
            using (var db = new LiteDatabase(_connectionString))
            {
                var players = db.GetCollection<Player>("players");

                if (player != null)
                {
                    player.TotalMoney += Globals.TotalMoney;
                    player.TotalKills += Globals.TotalKills;
                    player.TotalRound += Globals.TotalRounds;

                    players.Update(player);
                }

                
            }
        }

        //Get player stats
        public Player GetPlayerStats(int loginId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                // Get the player stats from the database
                var players = db.GetCollection<Player>("players");
                return players.FindOne(p => p.LoginId == loginId);
            }
        }
    }
}
