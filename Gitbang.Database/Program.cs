using System;
using System.IO;
using System.Linq;
using Dapper;
using FluentMigrator.Runner;
using Gitbang.Database.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;

namespace Gitbang.Database
{
    class Program
    {
        public static IConfigurationRoot Configuration;

        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            var serviceProvider = CreateServices();
            using var scope = serviceProvider.CreateScope();
            //CheckDatabaseExists();
            Console.WriteLine("Updating database [Gitbang]");
            UpdateDatabase(scope.ServiceProvider);
            Console.WriteLine("Database was upgraded!.");
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddSQLite()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("Gitbang"))
                    .ScanIn(typeof(InitDatabase).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();

        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        /*private static void CheckDatabaseExists()
        {
            Console.WriteLine("Checking if database [Gitbang] exists...");
            var connectionString = Configuration.GetConnectionString("Gitbang");
            using var connection = new SqliteConnection(connectionString);
            try
            {
                connection.Open();
                if (connection.Query("select * from postgres.pg_catalog.pg_database where datname = @name",
                    new {name = "new_beginning"}).Any())
                {
                    Console.WriteLine("Database [new_beginning] already exists!");
                    return;
                }

                Console.WriteLine("Database [new_beginning] not prese`nt!");
                Console.WriteLine("Creating database [new_beginning]");
                connection.Execute("create database new_beginning;");
                Console.WriteLine("Done.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }*/
    }
}