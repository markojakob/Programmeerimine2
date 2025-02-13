using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class RentaklAndCarRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Rentings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rentings_CarId",
                table: "Rentings",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentings_Cars_CarId",
                table: "Rentings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentings_Cars_CarId",
                table: "Rentings");

            migrationBuilder.DropIndex(
                name: "IX_Rentings_CarId",
                table: "Rentings");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Rentings");
        }
    }
}
