using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace GoodBoy.Services
{
    public static class AntiSpamService
    {
        public static void AddWord(string word)
        {
            SQLiteConnection dbConnection;
            dbConnection = new SQLiteConnection("Data Source=Resources/database.sqlite;Version=3");
            dbConnection.Open();

            string sql = $"INSERT INTO BadWords (badword, CreateDateUTC) values ('{word}', '{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            command.Dispose();
            dbConnection.Close();
        }

        public static void RemoveWord(string word)
        {
            SQLiteConnection dbConnection;
            dbConnection = new SQLiteConnection("Data Source=Resources/database.sqlite;Version=3");
            dbConnection.Open();

            string sql = $"DELETE FROM BadWords WHERE badword = '{word}'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            command.Dispose();
            dbConnection.Close();
        }

        public static string ListWords()
        {
            SQLiteConnection dbConnection;
            dbConnection = new SQLiteConnection("Data Source=Resources/database.sqlite;Version=3");
            dbConnection.Open();

            string sql = "SELECT badword, CreateDateUTC FROM BadWords";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            string builder = "";
            while (reader.Read())
            {
                builder += "bad word `" + reader.GetString(reader.GetOrdinal("badword")) + "` " 
                    + "added on date " + reader.GetDateTime(reader.GetOrdinal("CreateDateUTC"))  + " UTC ";
            }

            command.Dispose();
            dbConnection.Close();
            return builder;
        }

        public static void ClearList()
        {
            SQLiteConnection dbConnection;
            dbConnection = new SQLiteConnection("Data Source=Resources/database.sqlite;Version=3");
            dbConnection.Open();

            string sql = "DELETE FROM BadWords";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            command.Dispose();
            dbConnection.Dispose();
        }

        public static List<string> GetWords()
        {
            SQLiteConnection dbConnection;
            dbConnection = new SQLiteConnection("Data Source=Resources/database.sqlite;Version=3");
            dbConnection.Open();

            string sql = "SELECT badword FROM BadWords";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<string> words = new List<string>();

            while (reader.Read())
            {
                words.Add(reader.GetString(reader.GetOrdinal("badword")));
            }

            command.Dispose();
            dbConnection.Close();
            return words;
        }
    }
}
