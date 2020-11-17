using System;
using FluentMigrator;

namespace Gitbang.Database.Migrations
{
    [Migration(20201411215500)]
    public class InitDatabase : Migration
    {
        public override void Up()
        {
            Create.Table("groups")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("parent_group_id").AsInt32().NotNullable().WithDefaultValue(0);
            Create.Table("repositories")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("group_id").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}