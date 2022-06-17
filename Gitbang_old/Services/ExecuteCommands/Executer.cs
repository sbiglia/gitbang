using System;
using Microsoft.Data.Sqlite;

namespace Gitbang_old.Services.ExecuteCommands
{
    public class Executer : IExecuter
    {
        public void ExecuteCommand(string connStr, Action<SqliteConnection> task)
        {
            using var conn = new SqliteConnection(connStr);
            conn.Open();
            task(conn);
        }

        public T ExecuteCommand<T>(string connStr, Func<SqliteConnection, T> task)
        {
            using var conn = new SqliteConnection(connStr);
            conn.Open();
            return task(conn);
        }
    }
}
