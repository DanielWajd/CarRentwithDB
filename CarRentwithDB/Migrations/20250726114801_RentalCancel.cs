using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentwithDB.Migrations
{
    /// <inheritdoc />
    public partial class RentalCancel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCanceled",
                table: "Rental",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCanceled",
                table: "Rental");
        }
    }
}
