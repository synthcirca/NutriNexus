using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NutriNexus.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Calories = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrepTime = table.Column<int>(type: "INTEGER", nullable: false),
                    CookTime = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalTime = table.Column<int>(type: "INTEGER", nullable: false),
                    ServingSize = table.Column<int>(type: "INTEGER", nullable: false),
                    Course = table.Column<string>(type: "TEXT", nullable: true),
                    Cuisine = table.Column<string>(type: "TEXT", nullable: true),
                    Calories = table.Column<int>(type: "INTEGER", nullable: false),
                    Carbs = table.Column<int>(type: "INTEGER", nullable: false),
                    Protein = table.Column<int>(type: "INTEGER", nullable: false),
                    Fat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    EquipmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeEquipment_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeEquipment_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<float>(type: "REAL", nullable: true),
                    Unit = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeInstructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    StepNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Instruction = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeInstructions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equipment",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Standard measuring spoons", "https://amzlink.to/az0Tmz0p6RGTA", "measuring spoons" },
                    { 2, "Dry measuring cups", "https://amzlink.to/az0DjP0Sx7AVd", "measuring cups" },
                    { 3, "Hand or stand mixer", "https://amzlink.to/az04r9lAhaKsP", "electric mixer" },
                    { 4, "Rubber spatula for mixing", "https://amzlink.to/az0cHANfPnAKd", "spatula" },
                    { 5, "Standard baking sheet", "https://amzlink.to/az0h3VEQB4BtJ", "baking sheet" },
                    { 6, "For lining baking sheets", "https://amzlink.to/az0e1HAvwbn07", "parchment paper" },
                    { 7, "for scooping cookies", "https://amzlink.to/az0935VbND2Ud", "cookie scoop" },
                    { 8, "silicone baking mat", "https://amzlink.to/az0tZruG7esod", "silicone baking mat" },
                    { 9, "for mixing", "https://amzlink.to/az0GGY6pRrF4W", "hand mixer" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Calories", "Name" },
                values: new object[,]
                {
                    { 1, 40f, "salted butter" },
                    { 2, 40f, "granulated sugar" },
                    { 3, 40f, "light brown sugar" },
                    { 4, 40f, "pure vanilla extract" },
                    { 5, 40f, "egg" },
                    { 6, 40f, "all-purpose flour" },
                    { 7, 40f, "baking soda" },
                    { 8, 40f, "baking powder" },
                    { 9, 40f, "sea salt" },
                    { 10, 40f, "chocolate chips" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "Calories", "Carbs", "CookTime", "Course", "Cuisine", "Description", "Fat", "ImageUrl", "Name", "PrepTime", "Protein", "Rating", "ServingSize", "TotalTime" },
                values: new object[] { 1, 100, 100, 8, "Dessert", "American", "This is the best chocolate chip cookie recipe ever. No funny ingredients, no chilling time, etc. Just a simple, straightforward, amazingly delicious, doughy yet still fully cooked, chocolate chip cookie that turns out perfectly every single time!", 100, "/chocolate-chip-cookie.jpg", "The Best Chocolate Chip Cookie Recipe Ever", 10, 100, 4.99m, 36, 30 });

            migrationBuilder.InsertData(
                table: "RecipeEquipment",
                columns: new[] { "Id", "EquipmentId", "Notes", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 1, 1, null, 1, 1 },
                    { 2, 2, null, 1, 1 },
                    { 3, 3, "The Kitchen Aid is a very popular mizer", 1, 1 },
                    { 4, 4, null, 1, 1 },
                    { 5, 5, null, 3, 1 },
                    { 6, 6, null, 1, 1 },
                    { 7, 7, null, 1, 1 },
                    { 8, 8, null, 1, 1 },
                    { 9, 9, null, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "IngredientId", "Note", "Quantity", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 1, 1, "softned", 1f, 1, "cup" },
                    { 2, 2, null, 1f, 1, "cup" },
                    { 3, 3, "packed", 1f, 1, "cup" },
                    { 4, 4, null, 2f, 1, "tsp" },
                    { 5, 5, null, 2f, 1, "large" },
                    { 6, 6, null, 3f, 1, "cup" },
                    { 7, 7, null, 1f, 1, "tsp" },
                    { 8, 8, null, 0.5f, 1, "tsp" },
                    { 9, 9, null, 1f, 1, "tsp" },
                    { 10, 10, "12 oz", 1f, 1, "cup" }
                });

            migrationBuilder.InsertData(
                table: "RecipeInstructions",
                columns: new[] { "Id", "Instruction", "RecipeId", "StepNumber" },
                values: new object[,]
                {
                    { 1, "Preheat oven to 375 degrees Fahrenheit (190 degrees Celsius). Line three baking sheets with parchment paper and set aside.", 1, 1 },
                    { 2, "In a medium bowl mix flour, baking soda, baking powder and salt. Set aside.", 1, 2 },
                    { 3, "Cream together butter and sugars until combined.", 1, 3 },
                    { 4, "Beat in eggs and vanilla until light (about 1 minute).", 1, 4 },
                    { 5, "Mix in the dry ingredients until combined.", 1, 5 },
                    { 6, "Add chocolate chips and mix well.", 1, 6 },
                    { 7, "Roll 2-3 Tablespoons (depending on how large you like your cookies) of dough at a time into balls and place them evenly spaced on your prepared cookie sheets. ", 1, 7 },
                    { 8, "Bake in preheated oven for approximately 8-10 minutes. Take them out when they are just barely starting to turn brown.", 1, 8 },
                    { 9, "Let them sit on the baking pan for 5 minutes before removing to cooling rack.", 1, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEquipment_EquipmentId",
                table: "RecipeEquipment",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEquipment_RecipeId",
                table: "RecipeEquipment",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeInstructions_RecipeId",
                table: "RecipeInstructions",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeEquipment");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeInstructions");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
