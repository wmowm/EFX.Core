using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace MyCatConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            string ConnectionString = "server=193.112.104.103;database=tibos;uid=root;pwd=Ghosts1t;port=3308;Charset=utf8;";
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM Dict";

                var res = connection.Query(sql);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
