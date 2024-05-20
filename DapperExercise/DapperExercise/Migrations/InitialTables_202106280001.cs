using FluentMigrator;

namespace DapperExercise.Migrations
{
    [Migration(202106280001)]
    public class InitialTables_202106280001 : Migration
    {
        public override void Up()
        {
            Create.Table("Company")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Address").AsString(255).NotNullable()
                .WithColumn("Country").AsString(255).NotNullable();

            Create.Table("Employee")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Age").AsInt32().NotNullable()
                .WithColumn("Position").AsString(255).NotNullable()
                .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("Company", "Id");
        }

        public override void Down()
        {
            Delete.Table("Company");
            Delete.Table("Employee");
        }
    }
}
