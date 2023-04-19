using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Services
{
    public class DatabaseService
    {
        public static void CreateDatabaseIfNotExists()
        {
            if (!File.Exists("db_ranking.sqlite"))
            {
                SQLiteConnection.CreateFile("db_ranking.sqlite");

                using (var conn = GetConnection())
                using (var cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = @"CREATE TABLE highscores (
                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                nickname TEXT,
                                score INTEGER)";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static SQLiteConnection GetConnection()
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=db_ranking.sqlite;Version=3;");
            conn.Open();
            return conn;
        }

        public static void CreateRecord(string nickname, int score)
        {
            if (!File.Exists("db_ranking.sqlite"))
            {
                CreateDatabaseIfNotExists();
            }

            using (var conn = GetConnection())
            using (var cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = "INSERT INTO highscores (nickname, score) VALUES (@nickname, @score)";
                cmd.Parameters.AddWithValue("@nickname", nickname);
                cmd.Parameters.AddWithValue("@score", score);
                cmd.ExecuteNonQuery();
            }
        }

        public static void GetRecords()
        {
            using (var conn = GetConnection())
            using (var cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = "SELECT nickname, score FROM highscores ORDER BY score DESC";
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nHigh Scores:");
                    Console.WriteLine("════════════");
                    while (reader.Read())
                    {
                        string nickname = reader.GetString(0);
                        int score = reader.GetInt32(1);
                        Console.WriteLine("{0,-15} {1}", nickname, score);
                    }
                }
            }
        }
    }
}
