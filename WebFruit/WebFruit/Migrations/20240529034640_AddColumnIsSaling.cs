using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFruit.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnIsSaling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSaling",
                table: "Products",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSaling",
                table: "Products");
        }
    }
}
