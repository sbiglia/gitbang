using System;
using Microsoft.Data.Sqlite;

namespace Gitbang_old.Services.ExecuteCommands
{
    public interface IExecuter
    {
        void ExecuteCommand(string connStr, Action<SqliteConnection> task);

        T ExecuteCommand<T>(string connStr, Func<SqliteConnection, T> task);
    }
}
