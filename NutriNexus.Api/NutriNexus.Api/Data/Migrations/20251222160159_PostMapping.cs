using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NutriNexusAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class PostMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropColumn(
                name: "Directions",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Recipes",
                newName: "RecipeId");

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "RecipeIngredient",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "RecipeIngredient",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<float>(
                name: "Calories",
                table: "Ingredients",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.CreateTable(
                name: "Direction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StepNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Instruction = table.Column<string>(type: "TEXT", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direction_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId");
                });

            migrationBuilder.InsertData(
                table: "Direction",
                columns: new[] { "Id", "Instruction", "RecipeId", "StepNumber" },
                values: new object[,]
                {
                    { 1, "cookie 1", 1, 1 },
                    { 2, "cookie 2", 1, 2 },
                    { 3, "cookie 3", 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Direction_RecipeId",
                table: "Direction",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredients_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Ingredients_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "Direction");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "RecipeIngredient");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "Recipes",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Directions",
                table: "Recipes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "RecipeIngredient",
                type: "REAL",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "RecipeIngredient",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "Calories",
                table: "Ingredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10,
                column: "RecipeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Directions",
                value: "[\"cookie 1\",\"cookie 2\",\"cookie 3\"]");

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "cup" },
                    { 2, "egg" },
                    { 3, "tsp" }
                });
        }
    }
}
