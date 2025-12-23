using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriNexus.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImgUrlToSourceUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Equipment",
                newName: "SourceUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SourceUrl",
                table: "Equipment",
                newName: "ImageUrl");
        }
    }
}
