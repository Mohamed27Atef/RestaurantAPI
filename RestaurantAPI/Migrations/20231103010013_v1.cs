using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Copons_coponid",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_coponid",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "coponid",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Resturants",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Resturants");

            migrationBuilder.AddColumn<int>(
                name: "coponid",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_coponid",
                table: "Orders",
                column: "coponid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Copons_coponid",
                table: "Orders",
                column: "coponid",
                principalTable: "Copons",
                principalColumn: "id");
        }
    }
}
