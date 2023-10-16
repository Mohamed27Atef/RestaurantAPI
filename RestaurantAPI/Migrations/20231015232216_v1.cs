using System;
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
            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "restaurantId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cateigory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cateigory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeFeedback",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeFeedback", x => x.id);
                    table.ForeignKey(
                        name: "FK_RecipeFeedback_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeFeedback_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantCateigory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantCateigory", x => new { x.CategoryId, x.RestaurantId });
                    table.ForeignKey(
                        name: "FK_RestaurantCateigory_Cateigory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Cateigory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantCateigory_Resturants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Resturants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_categoryId",
                table: "Recipes",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_restaurantId",
                table: "Recipes",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeFeedback_RecipeId",
                table: "RecipeFeedback",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeFeedback_userId",
                table: "RecipeFeedback",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantCateigory_RestaurantId",
                table: "RestaurantCateigory",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cateigory_categoryId",
                table: "Recipes",
                column: "categoryId",
                principalTable: "Cateigory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Resturants_restaurantId",
                table: "Recipes",
                column: "restaurantId",
                principalTable: "Resturants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cateigory_categoryId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Resturants_restaurantId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipeFeedback");

            migrationBuilder.DropTable(
                name: "RestaurantCateigory");

            migrationBuilder.DropTable(
                name: "Cateigory");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_categoryId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_restaurantId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "restaurantId",
                table: "Recipes");
        }
    }
}
