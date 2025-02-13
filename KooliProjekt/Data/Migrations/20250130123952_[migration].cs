using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Rentings_RentingId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RentingId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RentingId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "RentalNo",
                table: "Rentings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Colour",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CarCustomer",
                columns: table => new
                {
                    CarsId = table.Column<int>(type: "int", nullable: false),
                    CarsId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCustomer", x => new { x.CarsId, x.CarsId1 });
                    table.ForeignKey(
                        name: "FK_CarCustomer_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarCustomer_Customers_CarsId1",
                        column: x => x.CarsId1,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarRenting",
                columns: table => new
                {
                    LinesId = table.Column<int>(type: "int", nullable: false),
                    RentingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRenting", x => new { x.LinesId, x.RentingsId });
                    table.ForeignKey(
                        name: "FK_CarRenting_Cars_LinesId",
                        column: x => x.LinesId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRenting_Rentings_RentingsId",
                        column: x => x.RentingsId,
                        principalTable: "Rentings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarCustomer_CarsId1",
                table: "CarCustomer",
                column: "CarsId1");

            migrationBuilder.CreateIndex(
                name: "IX_CarRenting_RentingsId",
                table: "CarRenting",
                column: "RentingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarCustomer");

            migrationBuilder.DropTable(
                name: "CarRenting");

            migrationBuilder.AlterColumn<string>(
                name: "RentalNo",
                table: "Rentings",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Colour",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RentingId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RentingId",
                table: "Cars",
                column: "RentingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Rentings_RentingId",
                table: "Cars",
                column: "RentingId",
                principalTable: "Rentings",
                principalColumn: "Id");
        }
    }
}
