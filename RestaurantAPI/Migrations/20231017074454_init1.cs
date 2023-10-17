using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_customer_idId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_userId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_userId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_customer_idId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "customer_idId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "application_user_id",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_application_user_id",
                table: "Users",
                column: "application_user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_application_user_id",
                table: "Users",
                column: "application_user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_application_user_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_application_user_id",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "application_user_id",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customer_idId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_userId",
                table: "Users",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_customer_idId",
                table: "AspNetUsers",
                column: "customer_idId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_customer_idId",
                table: "AspNetUsers",
                column: "customer_idId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_userId",
                table: "Users",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
