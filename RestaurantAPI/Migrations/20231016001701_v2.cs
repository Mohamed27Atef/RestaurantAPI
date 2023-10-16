using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "table_id",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cart_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cateigory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cateigory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryMan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ResturantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryMan_Resturants_ResturantId",
                        column: x => x.ResturantId,
                        principalTable: "Resturants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
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
                name: "RecipeImages",
                columns: table => new
                {
                    Image = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeImages", x => new { x.RecipeId, x.Image });
                    table.ForeignKey(
                        name: "FK_RecipeImages_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResturantFeedback",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResturantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResturantFeedback", x => x.id);
                    table.ForeignKey(
                        name: "FK_ResturantFeedback_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResturantFeedback_Resturants_ResturantId",
                        column: x => x.ResturantId,
                        principalTable: "Resturants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    AvailableState = table.Column<int>(type: "int", nullable: false),
                    TableType = table.Column<int>(type: "int", nullable: false),
                    ResturantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Resturants_ResturantId",
                        column: x => x.ResturantId,
                        principalTable: "Resturants",
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantCateigory_Resturants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Resturants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    DeliveryMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_DeliveryMan_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "DeliveryMan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResturantFeature",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    ResturantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResturantFeature", x => new { x.FeatureId, x.ResturantId });
                    table.ForeignKey(
                        name: "FK_ResturantFeature_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResturantFeature_Resturants_ResturantId",
                        column: x => x.ResturantId,
                        principalTable: "Resturants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Copon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfUser = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Copon", x => x.id);
                    table.ForeignKey(
                        name: "FK_Copon_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
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
                name: "IX_AspNetUsers_CartId",
                table: "AspNetUsers",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_table_id",
                table: "AspNetUsers",
                column: "table_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_userId",
                table: "Cart",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Copon_OrderId",
                table: "Copon",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMan_ResturantId",
                table: "DeliveryMan",
                column: "ResturantId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AddressId",
                table: "Order",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartId",
                table: "Order",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeliveryId",
                table: "Order",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ResturantFeature_ResturantId",
                table: "ResturantFeature",
                column: "ResturantId");

            migrationBuilder.CreateIndex(
                name: "IX_ResturantFeedback_ResturantId",
                table: "ResturantFeedback",
                column: "ResturantId");

            migrationBuilder.CreateIndex(
                name: "IX_ResturantFeedback_UserId",
                table: "ResturantFeedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_ResturantId",
                table: "Table",
                column: "ResturantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cart_CartId",
                table: "AspNetUsers",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Table_table_id",
                table: "AspNetUsers",
                column: "table_id",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cateigory_categoryId",
                table: "Recipes",
                column: "categoryId",
                principalTable: "Cateigory",
                principalColumn: "id",
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
                name: "FK_AspNetUsers_Cart_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Table_table_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cateigory_categoryId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Resturants_restaurantId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Copon");

            migrationBuilder.DropTable(
                name: "RecipeFeedback");

            migrationBuilder.DropTable(
                name: "RecipeImages");

            migrationBuilder.DropTable(
                name: "RestaurantCateigory");

            migrationBuilder.DropTable(
                name: "ResturantFeature");

            migrationBuilder.DropTable(
                name: "ResturantFeedback");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Cateigory");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "DeliveryMan");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_categoryId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_restaurantId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_table_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "restaurantId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "table_id",
                table: "AspNetUsers");
        }
    }
}
