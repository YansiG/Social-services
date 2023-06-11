using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;

namespace TalentedYouthProgect
{
    internal class DataBase
    {
        public static void Write(string table, string column, params string[] values)
        {
            try
            {
                string items = "";
                for (int i = 0; i < values.Length; i++)
                {
                    if (i == values.Length - 1)
                    {
                        items += "\'" + values[i] + "\'";
                        continue;
                    }
                    items += "\'" + values[i] + "\'" + ", ";
                }
                using (var connection = new SqliteConnection("Data Source=data.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO {table} ({column}) VALUES ({items})";

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static string GetID(string table)
        {
            string id = "";
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand($"SELECT ID FROM {table}", connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            id = reader.GetString("ID");
                        }
                    }
                }
            }
            return id;
        }
        public static string? GetID(string table, string column, string where, string result)
        {
            string? id = null;
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand($"SELECT {column} ID FROM {table} WHERE {where} = '{result}'", connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            try
                            {
                                id = reader.GetString("ID");
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
            return id;
        }
        public static string GetID(string table, string where1, string result1, string where2, string result2)
        {
            string id = "";
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand($"SELECT ID FROM {table} WHERE {where1} = '{result1}' AND {where2} = '{result2}'", connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            id = reader.GetString("ID");
                        }
                    }
                }
            }
            return id;
        }
        public static string Read(string table, string column, string where, string result)
        {
            string value = "1";
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand comm = new SqliteCommand($"SELECT {column} FROM {table} WHERE {where}='{result}'", connection);
                using (SqliteDataReader reader = comm.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            value = reader.GetString(column);// + 1;
                        }
                    }

                }
            }
            return value;
        }
        public static List<string> ReadS(string table, string column, string where, string result)
        {
            string sqlExpression = $"SELECT {column} FROM {table} WHERE {where} = '{result}'";
            List<string> results = new List<string>();
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                if(reader.IsDBNull(i))
                                {
                                    results.Add("NULL");
                                }
                                else
                                {
                                    results.Add(reader.GetString(i));
                                }
                            }
                        }
                    }
                }
            }
            return results;
        }
        public static void Update(string table, string colums, string value_to_update, string where, string result)
        {
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand($"UPDATE {table} SET {colums}='{value_to_update}' WHERE {where}='{result}'", connection);

                command.ExecuteNonQuery();
            }
        }
        public static void Delete(string table, string row, string where, string result)
        {
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand($"DELETE {row} FROM {table} WHERE {where}='{result}'", connection);
                try
                {
                    int number = command.ExecuteNonQuery();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
        }
        public static IEnumerable<List<string>> Search(string table, string where, string key)
        {

            List<string> results = new List<string>();

            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand($"SELECT * FROM {table} WHERE {where} LIKE '%{key}%'", connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            results.Clear();
                            for (int i = 0; i < 9; i++)
                            {
                                results.Add(reader.GetString(i));
                            }
                            yield return results;
                        }
                    }
                }
            }
        }
        public static IEnumerable<int> SearchNull(string table, string where)
        {
            int results = 0;

            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand($"SELECT * FROM {table} WHERE {where} NOT NULL", connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            results = 0;
                            results = int.Parse(reader.GetString(0));
                            yield return results;
                        }
                    }
                }
            }
        }
        public static List<int> findByids(int academicYear, int academicYear1)
        {
            DateTime start = new DateTime(academicYear, 9, 1);
            DateTime end = new DateTime(academicYear1, 8, 31);
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();
                List<int> res = new List<int>();

                SqliteCommand command = new SqliteCommand($"SELECT ID FROM Students WHERE admissionYear BETWEEN '{start.ToString("yyyy.MM.dd")}' AND '{end.ToString("yyyy.MM.dd")}'", connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            res.Add(reader.GetInt32(0));
                        }
                    }
                }
                command.Cancel(); command.Cancel();
                return res;
            }
        }
    }
}

