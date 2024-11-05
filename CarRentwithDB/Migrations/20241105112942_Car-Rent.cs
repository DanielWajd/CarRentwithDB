using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentwithDB.Migrations
{
    /// <inheritdoc />
    public partial class CarRent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Rental",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rental_CarId",
                table: "Rental",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Cars_CarId",
                table: "Rental",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Cars_CarId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_CarId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Rental");
        }
    }
}
