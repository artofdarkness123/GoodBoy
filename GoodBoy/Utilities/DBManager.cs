using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace GoodBoy.Utilities
{
    public static class DBManager
    {
        public static void CreateDatabase()
        {
            if (!DatabaseExists())
            { 
                SQLiteConnection.CreateFile("database.sqlite");
                SQLiteConnection dbConnection;
                dbConnection = new SQLiteConnection("Data Source=database.sqlite;Version=3");
            }
        }

        private static bool DatabaseExists()
        {
            return File.Exists("Resources/database.sqlite");
        }

        public static void BuildTables()
        {
            SQLiteConnection dbConnection;
            dbConnection = new SQLiteConnection("Data Source=database.sqlite;Version=3");
            dbConnection.Open();

            //Quotes..... if I ever want them in the future... I will prob have a better way of doing it by then though..
            //string quotes = "CREATE TABLE Quotes (keyword VARCHAR(100), quote VARCHAR(1000))";
            //SQLiteCommand command = new SQLiteCommand(quotes, dbConnection);
            //command.ExecuteNonQuery();

            string AntiSpam = "CREATE TABLE BadWords (badword VARCHAR(100))";
            SQLiteCommand command = new SQLiteCommand(AntiSpam, dbConnection);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public static void RunCommand(string commandText)
        {
            SQLiteConnection dbConnection;
            dbConnection = new SQLiteConnection("Data Source=database.sqlite;Version=3");
            dbConnection.Open();

            SQLiteCommand command = new SQLiteCommand(commandText, dbConnection);
            command.ExecuteNonQuery();

            command.Dispose();
            dbConnection.Close();
        }

        public static bool TableExists(string tableName)
        {
            SQLiteConnection dbConnection;
            dbConnection = new SQLiteConnection("Data Source=database.sqlite;Version=3");
            dbConnection.Open();

            using (SQLiteCommand command = new SQLiteCommand())
            {
                command.CommandType = CommandType.Text;
                command.Connection = dbConnection;
                command.CommandText = "SELECT * FROM sqlite_master WHERE type = 'table' AND name = @name";
                command.Parameters.AddWithValue("@name", tableName);

                using (SQLiteDataReader sqlDataReader = command.ExecuteReader())
                {
                    if (sqlDataReader.Read())
                    {
                        dbConnection.Close();
                        return true;
                    }
                    else
                    {
                        dbConnection.Close();
                        return false;
                    }
                }
            }
        }
    }
}
