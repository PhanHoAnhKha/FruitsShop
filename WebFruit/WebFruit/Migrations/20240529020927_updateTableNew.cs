using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFruit.Migrations
{
    /// <inheritdoc />
    public partial class updateTableNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentDetail",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "News",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHot",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentDetail",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "IsHot",
                table: "News");
        }
    }
}
