using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Distancify.Migrations.SqlMigration
{
    public partial class MigrationLogTableMigration : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__DistancifyMigrationLog",
                columns: table => new
                {
                    Type = table.Column<string>(nullable: false),
                    RunnedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___DistancifyMigrationLog", x => x.Type);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__DistancifyMigrationLog");
        }
    }
}
