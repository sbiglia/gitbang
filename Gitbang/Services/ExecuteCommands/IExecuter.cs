using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace Gitbang.Services.ExecuteCommands
{
    public interface IExecuter
    {
        void ExecuteCommand(string connStr, Action<SqliteConnection> task);

        T ExecuteCommand<T>(string connStr, Func<SqliteConnection, T> task);
    }
}
