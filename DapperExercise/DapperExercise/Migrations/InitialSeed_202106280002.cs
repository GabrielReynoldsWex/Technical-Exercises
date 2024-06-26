﻿using DapperExercise.Entities;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace DapperExercise.Migrations
{
    [Migration(202106280002)]
    public class InitialSeed_202106280002 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("Employee")
                .Row(new Employee
                {
                    Id = 1,
                    Age = 34,
                    Name = "Test Employee",
                    Position = "Test Position",
                    CompanyId = 1
                });
            Delete.FromTable("Company")
                .Row(new Company
                {
                    Id = 1,
                    Address = "Test Address",
                    Country = "USA",
                    Name = "Test Name"
                });
        }
        public override void Up()
        {
            Insert.IntoTable("Company")
                .WithIdentityInsert()
                .Row(new Company
                {
                    Id = 1,
                    Name = "Test Name",
                    Address = "Test Address 2",
                    Country = "USA",
                });
            Insert.IntoTable("Employee")
                .WithIdentityInsert()
                .Row(new Employee
                {
                    Id = 1,
                    Age = 34,
                    Name = "Test Employee",
                    Position = "Test Position",
                    CompanyId = 1,
                });
        }
    }
}
